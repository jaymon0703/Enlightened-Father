namespace AviorInterviewProject
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClearDB = new System.Windows.Forms.Button();
            this.btnUploadTestData = new System.Windows.Forms.Button();
            this.btnProcessFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClearDB
            // 
            this.btnClearDB.Location = new System.Drawing.Point(12, 12);
            this.btnClearDB.Name = "btnClearDB";
            this.btnClearDB.Size = new System.Drawing.Size(75, 72);
            this.btnClearDB.TabIndex = 0;
            this.btnClearDB.Text = "Clear Database";
            this.btnClearDB.UseVisualStyleBackColor = true;
            this.btnClearDB.Click += new System.EventHandler(this.btnClearDB_Click);
            // 
            // btnUploadTestData
            // 
            this.btnUploadTestData.Enabled = false;
            this.btnUploadTestData.Location = new System.Drawing.Point(93, 12);
            this.btnUploadTestData.Name = "btnUploadTestData";
            this.btnUploadTestData.Size = new System.Drawing.Size(75, 72);
            this.btnUploadTestData.TabIndex = 1;
            this.btnUploadTestData.Text = "Upload Test Data";
            this.btnUploadTestData.UseVisualStyleBackColor = true;
            this.btnUploadTestData.Click += new System.EventHandler(this.btnUploadTestData_Click);
            // 
            // btnProcessFiles
            // 
            this.btnProcessFiles.Location = new System.Drawing.Point(174, 12);
            this.btnProcessFiles.Name = "btnProcessFiles";
            this.btnProcessFiles.Size = new System.Drawing.Size(75, 72);
            this.btnProcessFiles.TabIndex = 2;
            this.btnProcessFiles.Text = "Process Files";
            this.btnProcessFiles.UseVisualStyleBackColor = true;
            this.btnProcessFiles.Click += new System.EventHandler(this.btnProcessFiles_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 98);
            this.Controls.Add(this.btnProcessFiles);
            this.Controls.Add(this.btnUploadTestData);
            this.Controls.Add(this.btnClearDB);
            this.Name = "frmMain";
            this.Text = "Avior Interview Project";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClearDB;
        private System.Windows.Forms.Button btnUploadTestData;
        private System.Windows.Forms.Button btnProcessFiles;
    }
}

