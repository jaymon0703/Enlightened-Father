using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts two rows of test data into the DB
        /// </summary>
        public static void InsertTestData()
        {

            DataTable dt = new DataTable();
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

    }
}
