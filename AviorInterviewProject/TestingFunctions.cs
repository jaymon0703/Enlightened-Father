using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;

namespace AviorInterviewProject
{
    public static class TestingFunctions
    {
        public static void ClearDB()
        {
            // Optional
            //   You have DELETE permission on your DB table. 
            //   There is also a stored procedure set up that clear the table called Clear<YourName> which you have EXEC permission
            // You can choose to implement this function here for your convenience or if you prefer to clear your data directly (say via SSMS) that is also fine.

            //http://stackoverflow.com/questions/11103181/a-fast-way-to-delete-all-rows-of-a-datatable-at-once
                    SqlConnection con = new SqlConnection(DBAccess.ConnectionString);
                    con.Open();
                    //string sqlTrunc = "TRUNCATE TABLE " + DBAccess.TableName;
                    string sqlTrunc = "DELETE FROM " + DBAccess.TableName; //TRUNC did not work with my permissions but DELETE FROM Table did!
                    SqlCommand cmd = new SqlCommand(sqlTrunc, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts two rows of test data into the DB
        /// </summary>
        public static void InsertTestData()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("TradeDate" , typeof(DateTime));
            dt.Columns.Add("TradeTime", typeof(TimeSpan)); //DAN TO DO: Check datatype here...
            dt.Columns.Add("Ticker", typeof(string));
            dt.Columns.Add("Expiry", typeof(DateTime));
            dt.Columns.Add("InstrumentType", typeof(string));
            dt.Columns.Add("Strike", typeof(decimal));
            dt.Columns.Add("Volatility", typeof(decimal));
            dt.Columns.Add("Premium", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Status", typeof(string));

            DataRow dr = dt.NewRow();
            dr["TradeDate"] = DateTime.Today;
            dr["TradeTime"] = DateTime.Now.TimeOfDay;
            dr["Ticker"] = "TestPut";
            dr["Expiry"] = DateTime.Today.AddDays(90);
            dr["InstrumentType"] = "P";
            dr["Strike"] = 1000;
            dr["Volatility"] = 20;
            dr["Premium"] = 1000;
            dr["Quantity"] = 1000;
            dr["Status"] = "Blablabla";
            dt.Rows.Add(dr);

            DataRow dr2 = dt.NewRow();
            dr2.ItemArray = dr.ItemArray.Clone() as object[];
            dr["Ticker"] = "TestCall";
            dr["InstrumentType"] = "C";
            dt.Rows.Add(dr2);

            DBAccess.BulkInsert(DBAccess.ConnectionString, DBAccess.TableName, dt);
        }

        //http://stackoverflow.com/questions/7244971/how-do-i-import-from-excel-to-a-dataset-using-microsoft-office-interop-excel
        //http://stackoverflow.com/questions/7221891/upload-excel-and-import-to-datatable
        
        public static void ReadExcel(String path)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;

            int index = 0;
            object rowIndex = 2;

            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt2.Columns.Add("TradeDate", typeof(DateTime));
            dt2.Columns.Add("TradeTime", typeof(TimeSpan)); //DAN TO DO: Check datatype here...
            dt2.Columns.Add("Ticker", typeof(string));
            dt2.Columns.Add("Expiry", typeof(DateTime));
            dt2.Columns.Add("InstrumentType", typeof(string));
            dt2.Columns.Add("Strike", typeof(decimal));
            dt2.Columns.Add("Volatility", typeof(decimal));
            dt2.Columns.Add("Premium", typeof(decimal));
            dt2.Columns.Add("Quantity", typeof(int));
            dt2.Columns.Add("Status", typeof(string));

            DataRow row;

            while (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null)
            {
                //rowIndex = 2 + index;
                row = dt2.NewRow();
                double d = ((((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2));
                double d_trunc = Math.Truncate((((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2)); //http://stackoverflow.com/questions/13919641/how-to-convert-a-double-value-to-a-datetime-in-c
                row[0] = DateTime.FromOADate(d_trunc);
                row[1] = TimeSpan.FromDays(d % d_trunc);
                //row[1] = TimeSpan.FromDays((((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2));
                //row[1] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2);
                row[2] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2);
                double e_trunc = Math.Truncate((((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 3]).Value2));
                row[3] = DateTime.FromOADate(e_trunc);
                row[4] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 6]).Value2);
                row[5] = Convert.ToDouble(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 5]).Value2);
                row[6] = Convert.ToDouble(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 9]).Value2);
                row[7] = Convert.ToDouble(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 8]).Value2);
                row[8] = Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 4]).Value2);
                row[9] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 10]).Value2);
                index++;
                rowIndex = 2 + index;
                dt2.Rows.Add(row);

                //DBAccess.BulkInsert(DBAccess.ConnectionString, DBAccess.TableName, dt2);
            }
            app.Workbooks.Close();
            DBAccess.BulkInsert(DBAccess.ConnectionString, DBAccess.TableName, dt2);
            //return dt2;
        }

        public static void ReadMultiExcel(String targetDirectory)
        {

            //string targetDirectory = "C:/Users/jasen/Personal/aviorinterviewproject/Example Files";
            //int count = 0;
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (String fi in fileEntries)
            {
                if (fi == @"C:\Users\jasen\Personal\aviorinterviewproject\Example Files\Options Traded 20160524.xls" ||
                    fi == @"C:\Users\jasen\Personal\aviorinterviewproject\Example Files\Options Traded 20160530.xls" ||
                    fi == @"C:\Users\jasen\Personal\aviorinterviewproject\Example Files\Options Traded 20160610.xls")
                {
                    //break;
                    continue;
                }
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(fi, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                    Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;

                    int index = 0;
                    object rowIndex = 2;

                    System.Data.DataTable dt3 = new System.Data.DataTable();
                    dt3.Columns.Add("TradeDate", typeof(DateTime));
                    dt3.Columns.Add("TradeTime", typeof(TimeSpan)); //DAN TO DO: Check datatype here...
                    dt3.Columns.Add("Ticker", typeof(string));
                    dt3.Columns.Add("Expiry", typeof(DateTime));
                    dt3.Columns.Add("InstrumentType", typeof(string));
                    dt3.Columns.Add("Strike", typeof(decimal));
                    dt3.Columns.Add("Volatility", typeof(decimal));
                    dt3.Columns.Add("Premium", typeof(decimal));
                    dt3.Columns.Add("Quantity", typeof(int));
                    dt3.Columns.Add("Status", typeof(string));

                    DataRow row;

                    while (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null)
                    {
                        //rowIndex = 2 + index;
                        row = dt3.NewRow();
                        double d = ((((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2));
                        double d_trunc = Math.Truncate((((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2)); //http://stackoverflow.com/questions/13919641/how-to-convert-a-double-value-to-a-datetime-in-c
                        row[0] = DateTime.FromOADate(d_trunc);
                        row[1] = TimeSpan.FromDays(d % d_trunc);
                        //row[1] = TimeSpan.FromDays((((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2));
                        //row[1] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2);
                        row[2] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2);
                        double e_trunc = Math.Truncate((((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 3]).Value2));
                        row[3] = DateTime.FromOADate(e_trunc);
                        row[4] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 6]).Value2);
                        row[5] = Convert.ToDouble(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 5]).Value2);
                        row[6] = Convert.ToDouble(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 9]).Value2);
                        row[7] = Convert.ToDouble(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 8]).Value);
                        row[8] = Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 4]).Value2);
                        row[9] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 10]).Value2);
                        index++;
                        //count += 1;
                        rowIndex = 2 + index;
                        dt3.Rows.Add(row);
                        //return dt3;
                        //DBAccess.BulkInsert(DBAccess.ConnectionString, DBAccess.TableName, dt2);
                    }
                    app.Workbooks.Close();
                    DBAccess.BulkInsert(DBAccess.ConnectionString, DBAccess.TableName, dt3);
                }
            //DBAccess.BulkInsert(DBAccess.ConnectionString, DBAccess.TableName, dt3);
            //string targetDirectory = "C:/Users/jasen/Personal/aviorinterviewproject/Example Files";
            //string[] fileEntries = Directory.GetFiles(targetDirectory);
            //return dt2;
        }
    }    
}
