namespace RidgeRibbon.ViewSheetInfo
{
    partial class PrintPDFForm
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
            this.dgvSheets = new System.Windows.Forms.DataGridView();
            this.dgvToPrint = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbVector = new System.Windows.Forms.CheckBox();
            this.cbDbgReport = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbDwgExport = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCheckNone = new System.Windows.Forms.Button();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.btnBrowseForDir = new System.Windows.Forms.Button();
            this.gbUsingPrinter = new System.Windows.Forms.GroupBox();
            this.lblCurrentPrinter = new System.Windows.Forms.Label();
            this.tbSaveToDir = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnFormatSave = new System.Windows.Forms.Button();
            this.btnFormatEdit = new System.Windows.Forms.Button();
            this.btnRidgeDefault = new System.Windows.Forms.Button();
            this.btnParameterAdd = new System.Windows.Forms.Button();
            this.tbFileNameFormat = new System.Windows.Forms.TextBox();
            this.cbParameter = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tbDebug = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSheets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToPrint)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbUsingPrinter.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSheets
            // 
            this.dgvSheets.AllowUserToAddRows = false;
            this.dgvSheets.AllowUserToDeleteRows = false;
            this.dgvSheets.AllowUserToOrderColumns = true;
            this.dgvSheets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSheets.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSheets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSheets.Location = new System.Drawing.Point(3, 22);
            this.dgvSheets.Name = "dgvSheets";
            this.dgvSheets.RowTemplate.Height = 28;
            this.dgvSheets.Size = new System.Drawing.Size(1293, 528);
            this.dgvSheets.TabIndex = 2;
            this.dgvSheets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSheets_CellContentClick);
            // 
            // dgvToPrint
            // 
            this.dgvToPrint.AllowUserToAddRows = false;
            this.dgvToPrint.AllowUserToDeleteRows = false;
            this.dgvToPrint.AllowUserToResizeColumns = false;
            this.dgvToPrint.AllowUserToResizeRows = false;
            this.dgvToPrint.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvToPrint.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvToPrint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvToPrint.Location = new System.Drawing.Point(3, 22);
            this.dgvToPrint.Name = "dgvToPrint";
            this.dgvToPrint.ReadOnly = true;
            this.dgvToPrint.RowTemplate.Height = 28;
            this.dgvToPrint.Size = new System.Drawing.Size(559, 503);
            this.dgvToPrint.TabIndex = 4;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(465, 293);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(121, 43);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(592, 293);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 43);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbDebug);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbVector);
            this.groupBox2.Controls.Add(this.cbDbgReport);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnCheckNone);
            this.groupBox2.Controls.Add(this.btnCheckAll);
            this.groupBox2.Controls.Add(this.btnPrint);
            this.groupBox2.Controls.Add(this.btnBrowseForDir);
            this.groupBox2.Controls.Add(this.gbUsingPrinter);
            this.groupBox2.Controls.Add(this.tbSaveToDir);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(562, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(728, 503);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(296, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "(higher quality but slower)";
            // 
            // cbVector
            // 
            this.cbVector.AutoSize = true;
            this.cbVector.Location = new System.Drawing.Point(300, 44);
            this.cbVector.Name = "cbVector";
            this.cbVector.Size = new System.Drawing.Size(163, 24);
            this.cbVector.TabIndex = 12;
            this.cbVector.Text = "Vector processing";
            this.cbVector.UseVisualStyleBackColor = true;
            this.cbVector.CheckedChanged += new System.EventHandler(this.cbVector_CheckedChanged);
            // 
            // cbDbgReport
            // 
            this.cbDbgReport.AutoSize = true;
            this.cbDbgReport.Checked = true;
            this.cbDbgReport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDbgReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDbgReport.Location = new System.Drawing.Point(545, 244);
            this.cbDbgReport.Name = "cbDbgReport";
            this.cbDbgReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbDbgReport.Size = new System.Drawing.Size(160, 21);
            this.cbDbgReport.TabIndex = 11;
            this.cbDbgReport.Text = "(save debug report)";
            this.cbDbgReport.UseVisualStyleBackColor = true;
            this.cbDbgReport.CheckedChanged += new System.EventHandler(this.cbDbgReport_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbDwgExport);
            this.groupBox5.Location = new System.Drawing.Point(212, 270);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 78);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "DWG";
            // 
            // cbDwgExport
            // 
            this.cbDwgExport.AutoSize = true;
            this.cbDwgExport.Checked = true;
            this.cbDwgExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDwgExport.Location = new System.Drawing.Point(6, 36);
            this.cbDwgExport.Name = "cbDwgExport";
            this.cbDwgExport.Size = new System.Drawing.Size(179, 24);
            this.cbDwgExport.TabIndex = 0;
            this.cbDwgExport.Text = "Include DWG export";
            this.cbDwgExport.UseVisualStyleBackColor = true;
            this.cbDwgExport.CheckedChanged += new System.EventHandler(this.cbDwgExport_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Destination Folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(528, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "(Existing PDFs in this directory with the same file name will be overwritten!)";
            // 
            // btnCheckNone
            // 
            this.btnCheckNone.Location = new System.Drawing.Point(142, 44);
            this.btnCheckNone.Name = "btnCheckNone";
            this.btnCheckNone.Size = new System.Drawing.Size(113, 44);
            this.btnCheckNone.TabIndex = 9;
            this.btnCheckNone.Text = "Check None";
            this.btnCheckNone.UseVisualStyleBackColor = true;
            this.btnCheckNone.Click += new System.EventHandler(this.btnCheckNone_Click);
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Location = new System.Drawing.Point(26, 44);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(101, 44);
            this.btnCheckAll.TabIndex = 8;
            this.btnCheckAll.Text = "Check All";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // btnBrowseForDir
            // 
            this.btnBrowseForDir.Location = new System.Drawing.Point(602, 156);
            this.btnBrowseForDir.Name = "btnBrowseForDir";
            this.btnBrowseForDir.Size = new System.Drawing.Size(93, 39);
            this.btnBrowseForDir.TabIndex = 1;
            this.btnBrowseForDir.Text = "Browse";
            this.btnBrowseForDir.UseVisualStyleBackColor = true;
            this.btnBrowseForDir.Click += new System.EventHandler(this.btnBrowseForDir_Click);
            // 
            // gbUsingPrinter
            // 
            this.gbUsingPrinter.Controls.Add(this.lblCurrentPrinter);
            this.gbUsingPrinter.Location = new System.Drawing.Point(6, 270);
            this.gbUsingPrinter.Name = "gbUsingPrinter";
            this.gbUsingPrinter.Size = new System.Drawing.Size(200, 78);
            this.gbUsingPrinter.TabIndex = 0;
            this.gbUsingPrinter.TabStop = false;
            this.gbUsingPrinter.Text = "Using Printer";
            // 
            // lblCurrentPrinter
            // 
            this.lblCurrentPrinter.AutoSize = true;
            this.lblCurrentPrinter.Location = new System.Drawing.Point(25, 37);
            this.lblCurrentPrinter.Name = "lblCurrentPrinter";
            this.lblCurrentPrinter.Size = new System.Drawing.Size(96, 20);
            this.lblCurrentPrinter.TabIndex = 1;
            this.lblCurrentPrinter.Text = "printerName";
            // 
            // tbSaveToDir
            // 
            this.tbSaveToDir.Location = new System.Drawing.Point(10, 162);
            this.tbSaveToDir.Name = "tbSaveToDir";
            this.tbSaveToDir.Size = new System.Drawing.Size(586, 26);
            this.tbSaveToDir.TabIndex = 0;
            this.tbSaveToDir.Text = "U:\\";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "U:\\";
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvToPrint);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(3, 550);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1293, 528);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnFormatSave);
            this.groupBox4.Controls.Add(this.btnFormatEdit);
            this.groupBox4.Controls.Add(this.btnRidgeDefault);
            this.groupBox4.Controls.Add(this.btnParameterAdd);
            this.groupBox4.Controls.Add(this.tbFileNameFormat);
            this.groupBox4.Controls.Add(this.cbParameter);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1299, 106);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "File Naming Format";
            // 
            // btnFormatSave
            // 
            this.btnFormatSave.Location = new System.Drawing.Point(93, 68);
            this.btnFormatSave.Name = "btnFormatSave";
            this.btnFormatSave.Size = new System.Drawing.Size(72, 28);
            this.btnFormatSave.TabIndex = 15;
            this.btnFormatSave.Text = "Save";
            this.btnFormatSave.UseVisualStyleBackColor = true;
            this.btnFormatSave.Visible = false;
            this.btnFormatSave.Click += new System.EventHandler(this.btnFormatSave_Click);
            // 
            // btnFormatEdit
            // 
            this.btnFormatEdit.Location = new System.Drawing.Point(12, 68);
            this.btnFormatEdit.Name = "btnFormatEdit";
            this.btnFormatEdit.Size = new System.Drawing.Size(75, 29);
            this.btnFormatEdit.TabIndex = 14;
            this.btnFormatEdit.Text = "Edit";
            this.btnFormatEdit.UseVisualStyleBackColor = true;
            this.btnFormatEdit.Click += new System.EventHandler(this.btnFormatEdit_Click);
            // 
            // btnRidgeDefault
            // 
            this.btnRidgeDefault.Location = new System.Drawing.Point(724, 68);
            this.btnRidgeDefault.Name = "btnRidgeDefault";
            this.btnRidgeDefault.Size = new System.Drawing.Size(179, 28);
            this.btnRidgeDefault.TabIndex = 13;
            this.btnRidgeDefault.Text = "Ridge Default Format";
            this.btnRidgeDefault.UseVisualStyleBackColor = true;
            this.btnRidgeDefault.Visible = false;
            this.btnRidgeDefault.Click += new System.EventHandler(this.btnRidgeDefault_Click);
            // 
            // btnParameterAdd
            // 
            this.btnParameterAdd.Location = new System.Drawing.Point(616, 69);
            this.btnParameterAdd.Name = "btnParameterAdd";
            this.btnParameterAdd.Size = new System.Drawing.Size(75, 28);
            this.btnParameterAdd.TabIndex = 12;
            this.btnParameterAdd.Text = "Add";
            this.btnParameterAdd.UseVisualStyleBackColor = true;
            this.btnParameterAdd.Visible = false;
            this.btnParameterAdd.Click += new System.EventHandler(this.btnParameterAdd_Click);
            // 
            // tbFileNameFormat
            // 
            this.tbFileNameFormat.Location = new System.Drawing.Point(12, 34);
            this.tbFileNameFormat.Name = "tbFileNameFormat";
            this.tbFileNameFormat.ReadOnly = true;
            this.tbFileNameFormat.Size = new System.Drawing.Size(1158, 26);
            this.tbFileNameFormat.TabIndex = 11;
            // 
            // cbParameter
            // 
            this.cbParameter.FormattingEnabled = true;
            this.cbParameter.Location = new System.Drawing.Point(193, 69);
            this.cbParameter.Name = "cbParameter";
            this.cbParameter.Size = new System.Drawing.Size(417, 28);
            this.cbParameter.TabIndex = 10;
            this.cbParameter.Visible = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgvSheets);
            this.groupBox6.Controls.Add(this.groupBox3);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 106);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1299, 1081);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            // 
            // tbDebug
            // 
            this.tbDebug.BackColor = System.Drawing.SystemColors.Menu;
            this.tbDebug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDebug.Font = new System.Drawing.Font("Courier New", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDebug.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.tbDebug.Location = new System.Drawing.Point(10, 364);
            this.tbDebug.Multiline = true;
            this.tbDebug.Name = "tbDebug";
            this.tbDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDebug.Size = new System.Drawing.Size(712, 144);
            this.tbDebug.TabIndex = 14;
            this.tbDebug.Text = "Debug Report...";
            // 
            // PrintPDFForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 1187);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintPDFForm";
            this.ShowIcon = false;
            this.Text = "Print PDFs from Sheets";
            this.Load += new System.EventHandler(this.PrintPDFForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSheets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToPrint)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbUsingPrinter.ResumeLayout(false);
            this.gbUsingPrinter.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvSheets;
        private System.Windows.Forms.DataGridView dgvToPrint;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Button btnCheckNone;
        private System.Windows.Forms.Button btnBrowseForDir;
        private System.Windows.Forms.TextBox tbSaveToDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox gbUsingPrinter;
        private System.Windows.Forms.Label lblCurrentPrinter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFormatEdit;
        private System.Windows.Forms.Button btnRidgeDefault;
        private System.Windows.Forms.Button btnParameterAdd;
        private System.Windows.Forms.TextBox tbFileNameFormat;
        private System.Windows.Forms.ComboBox cbParameter;
        private System.Windows.Forms.Button btnFormatSave;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbDwgExport;
        private System.Windows.Forms.CheckBox cbDbgReport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbVector;
        private System.Windows.Forms.TextBox tbDebug;
        private System.Windows.Forms.GroupBox groupBox6;
    }
}