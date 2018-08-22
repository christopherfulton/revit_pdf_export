//
// (C) Copyright 2003-2016 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE. AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//

namespace RidgeRibbon.ViewPrinterSDK
{
    partial class PrintMgrForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.printerNameComboBox = new System.Windows.Forms.ComboBox();
            this.printergroupBox = new System.Windows.Forms.GroupBox();
            this.printToFileCheckBox = new System.Windows.Forms.CheckBox();
            this.fileGroupBox = new System.Windows.Forms.GroupBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.printToFileNameTextBox = new System.Windows.Forms.TextBox();
            this.printToFileNameLabel = new System.Windows.Forms.Label();
            this.separateFileRadioButton = new System.Windows.Forms.RadioButton();
            this.singleFileRadioButton = new System.Windows.Forms.RadioButton();
            this.printRangeGroupBox = new System.Windows.Forms.GroupBox();
            this.selectedViewSheetSetButton = new System.Windows.Forms.Button();
            this.selectedViewSheetSetLabel = new System.Windows.Forms.Label();
            this.selectedViewsRadioButton = new System.Windows.Forms.RadioButton();
            this.visiblePortionRadioButton = new System.Windows.Forms.RadioButton();
            this.currentWindowRadioButton = new System.Windows.Forms.RadioButton();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.copiesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.collateCheckBox = new System.Windows.Forms.CheckBox();
            this.orderCheckBox = new System.Windows.Forms.CheckBox();
            this.numberofcoyiesLabel = new System.Windows.Forms.Label();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.setupButton = new System.Windows.Forms.Button();
            this.printSetupNameLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.printergroupBox.SuspendLayout();
            this.fileGroupBox.SuspendLayout();
            this.printRangeGroupBox.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.copiesNumericUpDown)).BeginInit();
            this.settingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // printerNameComboBox
            // 
            this.printerNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.printerNameComboBox.FormattingEnabled = true;
            this.printerNameComboBox.Location = new System.Drawing.Point(124, 20);
            this.printerNameComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.printerNameComboBox.Name = "printerNameComboBox";
            this.printerNameComboBox.Size = new System.Drawing.Size(490, 28);
            this.printerNameComboBox.TabIndex = 1;
            // 
            // printergroupBox
            // 
            this.printergroupBox.Controls.Add(this.printToFileCheckBox);
            this.printergroupBox.Controls.Add(this.printerNameComboBox);
            this.printergroupBox.Controls.Add(this.label1);
            this.printergroupBox.Location = new System.Drawing.Point(18, 18);
            this.printergroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.printergroupBox.Name = "printergroupBox";
            this.printergroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.printergroupBox.Size = new System.Drawing.Size(782, 226);
            this.printergroupBox.TabIndex = 2;
            this.printergroupBox.TabStop = false;
            this.printergroupBox.Text = "Printer";
            // 
            // printToFileCheckBox
            // 
            this.printToFileCheckBox.AutoSize = true;
            this.printToFileCheckBox.Location = new System.Drawing.Point(652, 191);
            this.printToFileCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.printToFileCheckBox.Name = "printToFileCheckBox";
            this.printToFileCheckBox.Size = new System.Drawing.Size(109, 24);
            this.printToFileCheckBox.TabIndex = 2;
            this.printToFileCheckBox.Text = "Print to file";
            this.printToFileCheckBox.UseVisualStyleBackColor = true;
            this.printToFileCheckBox.CheckedChanged += new System.EventHandler(this.printToFileCheckBox_CheckedChanged);
            // 
            // fileGroupBox
            // 
            this.fileGroupBox.Controls.Add(this.browseButton);
            this.fileGroupBox.Controls.Add(this.printToFileNameTextBox);
            this.fileGroupBox.Controls.Add(this.printToFileNameLabel);
            this.fileGroupBox.Controls.Add(this.separateFileRadioButton);
            this.fileGroupBox.Controls.Add(this.singleFileRadioButton);
            this.fileGroupBox.Location = new System.Drawing.Point(18, 254);
            this.fileGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fileGroupBox.Name = "fileGroupBox";
            this.fileGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fileGroupBox.Size = new System.Drawing.Size(782, 165);
            this.fileGroupBox.TabIndex = 3;
            this.fileGroupBox.TabStop = false;
            this.fileGroupBox.Text = "File";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(626, 112);
            this.browseButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(112, 35);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "&Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // printToFileNameTextBox
            // 
            this.printToFileNameTextBox.Location = new System.Drawing.Point(141, 115);
            this.printToFileNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.printToFileNameTextBox.Name = "printToFileNameTextBox";
            this.printToFileNameTextBox.Size = new System.Drawing.Size(474, 26);
            this.printToFileNameTextBox.TabIndex = 2;
            // 
            // printToFileNameLabel
            // 
            this.printToFileNameLabel.AutoSize = true;
            this.printToFileNameLabel.Location = new System.Drawing.Point(75, 120);
            this.printToFileNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.printToFileNameLabel.Name = "printToFileNameLabel";
            this.printToFileNameLabel.Size = new System.Drawing.Size(55, 20);
            this.printToFileNameLabel.TabIndex = 1;
            this.printToFileNameLabel.Text = "Name:";
            // 
            // separateFileRadioButton
            // 
            this.separateFileRadioButton.AutoSize = true;
            this.separateFileRadioButton.Location = new System.Drawing.Point(14, 65);
            this.separateFileRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.separateFileRadioButton.Name = "separateFileRadioButton";
            this.separateFileRadioButton.Size = new System.Drawing.Size(597, 24);
            this.separateFileRadioButton.TabIndex = 0;
            this.separateFileRadioButton.Text = "Create separate files. View/sheet names will be appended to the specified name";
            this.separateFileRadioButton.UseVisualStyleBackColor = true;
            // 
            // singleFileRadioButton
            // 
            this.singleFileRadioButton.AutoSize = true;
            this.singleFileRadioButton.Checked = true;
            this.singleFileRadioButton.Location = new System.Drawing.Point(14, 29);
            this.singleFileRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.singleFileRadioButton.Name = "singleFileRadioButton";
            this.singleFileRadioButton.Size = new System.Drawing.Size(425, 24);
            this.singleFileRadioButton.TabIndex = 0;
            this.singleFileRadioButton.TabStop = true;
            this.singleFileRadioButton.Text = "Combine multiple selected views/sheets into a single file";
            this.singleFileRadioButton.UseVisualStyleBackColor = true;
            // 
            // printRangeGroupBox
            // 
            this.printRangeGroupBox.Controls.Add(this.selectedViewSheetSetButton);
            this.printRangeGroupBox.Controls.Add(this.selectedViewSheetSetLabel);
            this.printRangeGroupBox.Controls.Add(this.selectedViewsRadioButton);
            this.printRangeGroupBox.Controls.Add(this.visiblePortionRadioButton);
            this.printRangeGroupBox.Controls.Add(this.currentWindowRadioButton);
            this.printRangeGroupBox.Location = new System.Drawing.Point(18, 428);
            this.printRangeGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.printRangeGroupBox.Name = "printRangeGroupBox";
            this.printRangeGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.printRangeGroupBox.Size = new System.Drawing.Size(357, 282);
            this.printRangeGroupBox.TabIndex = 4;
            this.printRangeGroupBox.TabStop = false;
            this.printRangeGroupBox.Text = "Print Range";
            // 
            // selectedViewSheetSetButton
            // 
            this.selectedViewSheetSetButton.Enabled = false;
            this.selectedViewSheetSetButton.Location = new System.Drawing.Point(42, 160);
            this.selectedViewSheetSetButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectedViewSheetSetButton.Name = "selectedViewSheetSetButton";
            this.selectedViewSheetSetButton.Size = new System.Drawing.Size(112, 35);
            this.selectedViewSheetSetButton.TabIndex = 2;
            this.selectedViewSheetSetButton.Text = "Select...";
            this.selectedViewSheetSetButton.UseVisualStyleBackColor = true;
            this.selectedViewSheetSetButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // selectedViewSheetSetLabel
            // 
            this.selectedViewSheetSetLabel.AutoSize = true;
            this.selectedViewSheetSetLabel.Location = new System.Drawing.Point(38, 134);
            this.selectedViewSheetSetLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectedViewSheetSetLabel.Name = "selectedViewSheetSetLabel";
            this.selectedViewSheetSetLabel.Size = new System.Drawing.Size(98, 20);
            this.selectedViewSheetSetLabel.TabIndex = 1;
            this.selectedViewSheetSetLabel.Text = "<in-session>";
            // 
            // selectedViewsRadioButton
            // 
            this.selectedViewsRadioButton.AutoSize = true;
            this.selectedViewsRadioButton.Location = new System.Drawing.Point(14, 100);
            this.selectedViewsRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectedViewsRadioButton.Name = "selectedViewsRadioButton";
            this.selectedViewsRadioButton.Size = new System.Drawing.Size(195, 24);
            this.selectedViewsRadioButton.TabIndex = 0;
            this.selectedViewsRadioButton.TabStop = true;
            this.selectedViewsRadioButton.Text = "&Selected views/sheets.";
            this.selectedViewsRadioButton.UseVisualStyleBackColor = true;
            // 
            // visiblePortionRadioButton
            // 
            this.visiblePortionRadioButton.AutoSize = true;
            this.visiblePortionRadioButton.Location = new System.Drawing.Point(14, 65);
            this.visiblePortionRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.visiblePortionRadioButton.Name = "visiblePortionRadioButton";
            this.visiblePortionRadioButton.Size = new System.Drawing.Size(261, 24);
            this.visiblePortionRadioButton.TabIndex = 0;
            this.visiblePortionRadioButton.TabStop = true;
            this.visiblePortionRadioButton.Text = "&Visible portion of current window";
            this.visiblePortionRadioButton.UseVisualStyleBackColor = true;
            // 
            // currentWindowRadioButton
            // 
            this.currentWindowRadioButton.AutoSize = true;
            this.currentWindowRadioButton.Location = new System.Drawing.Point(14, 29);
            this.currentWindowRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.currentWindowRadioButton.Name = "currentWindowRadioButton";
            this.currentWindowRadioButton.Size = new System.Drawing.Size(143, 24);
            this.currentWindowRadioButton.TabIndex = 0;
            this.currentWindowRadioButton.TabStop = true;
            this.currentWindowRadioButton.Text = "Current &window";
            this.currentWindowRadioButton.UseVisualStyleBackColor = true;
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Controls.Add(this.copiesNumericUpDown);
            this.optionsGroupBox.Controls.Add(this.collateCheckBox);
            this.optionsGroupBox.Controls.Add(this.orderCheckBox);
            this.optionsGroupBox.Controls.Add(this.numberofcoyiesLabel);
            this.optionsGroupBox.Location = new System.Drawing.Point(408, 428);
            this.optionsGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optionsGroupBox.Size = new System.Drawing.Size(392, 154);
            this.optionsGroupBox.TabIndex = 4;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // copiesNumericUpDown
            // 
            this.copiesNumericUpDown.Location = new System.Drawing.Point(306, 25);
            this.copiesNumericUpDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.copiesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.copiesNumericUpDown.Name = "copiesNumericUpDown";
            this.copiesNumericUpDown.Size = new System.Drawing.Size(76, 26);
            this.copiesNumericUpDown.TabIndex = 4;
            this.copiesNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.copiesNumericUpDown.ValueChanged += new System.EventHandler(this.copiesNumericUpDown_ValueChanged);
            // 
            // collateCheckBox
            // 
            this.collateCheckBox.AutoSize = true;
            this.collateCheckBox.Location = new System.Drawing.Point(14, 100);
            this.collateCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.collateCheckBox.Name = "collateCheckBox";
            this.collateCheckBox.Size = new System.Drawing.Size(84, 24);
            this.collateCheckBox.TabIndex = 3;
            this.collateCheckBox.Text = "C&ollate";
            this.collateCheckBox.UseVisualStyleBackColor = true;
            // 
            // orderCheckBox
            // 
            this.orderCheckBox.AutoSize = true;
            this.orderCheckBox.Location = new System.Drawing.Point(14, 65);
            this.orderCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.orderCheckBox.Name = "orderCheckBox";
            this.orderCheckBox.Size = new System.Drawing.Size(170, 24);
            this.orderCheckBox.TabIndex = 2;
            this.orderCheckBox.Text = "Reverse print &order";
            this.orderCheckBox.UseVisualStyleBackColor = true;
            // 
            // numberofcoyiesLabel
            // 
            this.numberofcoyiesLabel.AutoSize = true;
            this.numberofcoyiesLabel.Location = new System.Drawing.Point(9, 32);
            this.numberofcoyiesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numberofcoyiesLabel.Name = "numberofcoyiesLabel";
            this.numberofcoyiesLabel.Size = new System.Drawing.Size(137, 20);
            this.numberofcoyiesLabel.TabIndex = 0;
            this.numberofcoyiesLabel.Text = "Number of &copies:";
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.setupButton);
            this.settingsGroupBox.Controls.Add(this.printSetupNameLabel);
            this.settingsGroupBox.Location = new System.Drawing.Point(408, 605);
            this.settingsGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.settingsGroupBox.Size = new System.Drawing.Size(392, 105);
            this.settingsGroupBox.TabIndex = 4;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Settings";
            // 
            // setupButton
            // 
            this.setupButton.Location = new System.Drawing.Point(14, 49);
            this.setupButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.setupButton.Name = "setupButton";
            this.setupButton.Size = new System.Drawing.Size(112, 35);
            this.setupButton.TabIndex = 1;
            this.setupButton.Text = "Se&tup...";
            this.setupButton.UseVisualStyleBackColor = true;
            this.setupButton.Click += new System.EventHandler(this.setupButton_Click);
            // 
            // printSetupNameLabel
            // 
            this.printSetupNameLabel.AutoSize = true;
            this.printSetupNameLabel.Location = new System.Drawing.Point(9, 25);
            this.printSetupNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.printSetupNameLabel.Name = "printSetupNameLabel";
            this.printSetupNameLabel.Size = new System.Drawing.Size(61, 20);
            this.printSetupNameLabel.TabIndex = 0;
            this.printSetupNameLabel.Text = "Default";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(687, 725);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(112, 35);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(444, 725);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(112, 35);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.closeButton.Location = new System.Drawing.Point(566, 725);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(112, 35);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // PrintMgrForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(818, 778);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.settingsGroupBox);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.printRangeGroupBox);
            this.Controls.Add(this.fileGroupBox);
            this.Controls.Add(this.printergroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintMgrForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Print";
            this.Load += new System.EventHandler(this.PrintMgrForm_Load);
            this.printergroupBox.ResumeLayout(false);
            this.printergroupBox.PerformLayout();
            this.fileGroupBox.ResumeLayout(false);
            this.fileGroupBox.PerformLayout();
            this.printRangeGroupBox.ResumeLayout(false);
            this.printRangeGroupBox.PerformLayout();
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.copiesNumericUpDown)).EndInit();
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox printerNameComboBox;
        private System.Windows.Forms.GroupBox printergroupBox;
        private System.Windows.Forms.CheckBox printToFileCheckBox;
        private System.Windows.Forms.GroupBox fileGroupBox;
        private System.Windows.Forms.RadioButton separateFileRadioButton;
        private System.Windows.Forms.RadioButton singleFileRadioButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox printToFileNameTextBox;
        private System.Windows.Forms.Label printToFileNameLabel;
        private System.Windows.Forms.GroupBox printRangeGroupBox;
        private System.Windows.Forms.RadioButton currentWindowRadioButton;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.Label selectedViewSheetSetLabel;
        private System.Windows.Forms.RadioButton selectedViewsRadioButton;
        private System.Windows.Forms.RadioButton visiblePortionRadioButton;
        private System.Windows.Forms.Button selectedViewSheetSetButton;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.Label numberofcoyiesLabel;
        private System.Windows.Forms.CheckBox collateCheckBox;
        private System.Windows.Forms.CheckBox orderCheckBox;
        private System.Windows.Forms.Button setupButton;
        private System.Windows.Forms.Label printSetupNameLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.NumericUpDown copiesNumericUpDown;
        private System.Windows.Forms.Button closeButton;
    }
}