using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AviorInterviewProject
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnClearDB_Click(object sender, EventArgs e)
        {
            TestingFunctions.ClearDB();
        }

        private void btnUploadTestData_Click(object sender, EventArgs e)
        {
            TestingFunctions.InsertTestData();
        }

        private void SingleUpload_Click(object sender, EventArgs e)
        {
            TestingFunctions.ReadExcel("C:/Users/jasen/Personal/aviorinterviewproject/Example Files/Options Traded 20160503");
        }

        //private void MultiUpload_Click(object sender, EventArgs e)
        //{
        //   TestingFunctions.ReadMultiExcel(@"C:\Users\jasen\Personal\aviorinterviewproject\Example Files");
        //}

        private void btnProcessFiles_Click(object sender, EventArgs e)
        {
            TestingFunctions.ReadMultiExcel(@"C:\Users\jasen\Personal\aviorinterviewproject\Example Files");
        }
    }
}
