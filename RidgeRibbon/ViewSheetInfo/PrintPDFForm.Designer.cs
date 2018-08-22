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
            this.gbUsingPrinter = new System.Windows.Forms.GroupBox();
            this.debug1 = new System.Windows.Forms.Label();
            this.lblCurrentPrinter = new System.Windows.Forms.Label();
            this.dgvSheets = new System.Windows.Forms.DataGridView();
            this.dgvToPrint = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseForDir = new System.Windows.Forms.Button();
            this.tbSaveToDir = new System.Windows.Forms.TextBox();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.btnCheckNone = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.gbUsingPrinter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSheets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToPrint)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUsingPrinter
            // 
            this.gbUsingPrinter.Controls.Add(this.debug1);
            this.gbUsingPrinter.Controls.Add(this.lblCurrentPrinter);
            this.gbUsingPrinter.Location = new System.Drawing.Point(12, 12);
            this.gbUsingPrinter.Name = "gbUsingPrinter";
            this.gbUsingPrinter.Size = new System.Drawing.Size(854, 78);
            this.gbUsingPrinter.TabIndex = 0;
            this.gbUsingPrinter.TabStop = false;
            this.gbUsingPrinter.Text = "Using Printer";
            // 
            // debug1
            // 
            this.debug1.AutoSize = true;
            this.debug1.Location = new System.Drawing.Point(150, 37);
            this.debug1.Name = "debug1";
            this.debug1.Size = new System.Drawing.Size(54, 20);
            this.debug1.TabIndex = 0;
            this.debug1.Text = "debug";
            this.debug1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.debug1.Visible = false;
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
            // dgvSheets
            // 
            this.dgvSheets.AllowUserToAddRows = false;
            this.dgvSheets.AllowUserToDeleteRows = false;
            this.dgvSheets.AllowUserToOrderColumns = true;
            this.dgvSheets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSheets.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSheets.Location = new System.Drawing.Point(12, 96);
            this.dgvSheets.MultiSelect = false;
            this.dgvSheets.Name = "dgvSheets";
            this.dgvSheets.RowTemplate.Height = 28;
            this.dgvSheets.Size = new System.Drawing.Size(1249, 486);
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
            this.dgvToPrint.Location = new System.Drawing.Point(12, 596);
            this.dgvToPrint.Name = "dgvToPrint";
            this.dgvToPrint.ReadOnly = true;
            this.dgvToPrint.RowTemplate.Height = 28;
            this.dgvToPrint.Size = new System.Drawing.Size(532, 367);
            this.dgvToPrint.TabIndex = 4;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(1021, 920);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(121, 43);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1148, 920);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 43);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnBrowseForDir);
            this.groupBox2.Controls.Add(this.tbSaveToDir);
            this.groupBox2.Location = new System.Drawing.Point(550, 596);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(705, 129);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Save to folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(518, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Existing PDFs in this directory with the same file name will be overwritten.";
            // 
            // btnBrowseForDir
            // 
            this.btnBrowseForDir.Location = new System.Drawing.Point(592, 33);
            this.btnBrowseForDir.Name = "btnBrowseForDir";
            this.btnBrowseForDir.Size = new System.Drawing.Size(93, 39);
            this.btnBrowseForDir.TabIndex = 1;
            this.btnBrowseForDir.Text = "Browse";
            this.btnBrowseForDir.UseVisualStyleBackColor = true;
            this.btnBrowseForDir.Click += new System.EventHandler(this.btnBrowseForDir_Click);
            // 
            // tbSaveToDir
            // 
            this.tbSaveToDir.Location = new System.Drawing.Point(20, 39);
            this.tbSaveToDir.Name = "tbSaveToDir";
            this.tbSaveToDir.Size = new System.Drawing.Size(557, 26);
            this.tbSaveToDir.TabIndex = 0;
            this.tbSaveToDir.Text = "U:\\";
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Location = new System.Drawing.Point(1026, 37);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(101, 44);
            this.btnCheckAll.TabIndex = 8;
            this.btnCheckAll.Text = "Check All";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // btnCheckNone
            // 
            this.btnCheckNone.Location = new System.Drawing.Point(1142, 37);
            this.btnCheckNone.Name = "btnCheckNone";
            this.btnCheckNone.Size = new System.Drawing.Size(113, 44);
            this.btnCheckNone.TabIndex = 9;
            this.btnCheckNone.Text = "Check None";
            this.btnCheckNone.UseVisualStyleBackColor = true;
            this.btnCheckNone.Click += new System.EventHandler(this.btnCheckNone_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "U:\\";
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest_1);
            // 
            // PrintPDFForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 975);
            this.Controls.Add(this.btnCheckNone);
            this.Controls.Add(this.btnCheckAll);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.dgvToPrint);
            this.Controls.Add(this.dgvSheets);
            this.Controls.Add(this.gbUsingPrinter);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintPDFForm";
            this.ShowIcon = false;
            this.Text = "Print PDFs from Sheets";
            this.gbUsingPrinter.ResumeLayout(false);
            this.gbUsingPrinter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSheets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToPrint)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUsingPrinter;
        private System.Windows.Forms.Label lblCurrentPrinter;
        private System.Windows.Forms.DataGridView dgvSheets;
        private System.Windows.Forms.DataGridView dgvToPrint;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Button btnCheckNone;
        private System.Windows.Forms.Label debug1;
        private System.Windows.Forms.Button btnBrowseForDir;
        private System.Windows.Forms.TextBox tbSaveToDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
    }
}