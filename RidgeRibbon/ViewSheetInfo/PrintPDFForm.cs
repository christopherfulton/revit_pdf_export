using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Win32;

namespace RidgeRibbon.ViewSheetInfo
{
    public partial class PrintPDFForm : System.Windows.Forms.Form
    {
        private ExternalCommandData m_command;
        private Document m_doc;
        private List<PrintSheet> m_printSheets;
        private BindingSource bs1 = new BindingSource();
        private BindingSource bs2 = new BindingSource();
        private List<PrintSheet> m_SendToPrint = new List<PrintSheet>();
        private PrintManager m_printMgr;

        public PrintPDFForm(ExternalCommandData commandData, Document doc)
        {
            m_command = commandData;
            m_doc = doc;
            m_printMgr = doc.PrintManager;
            
            InitializeComponent();

            // default to get the directory of the printmanager printtofilename location
            if (m_printMgr.PrintToFileName.EndsWith(@".pdf"))
            {
                int pos = m_printMgr.PrintToFileName.LastIndexOf(@"\");
                tbSaveToDir.Text = m_printMgr.PrintToFileName.Substring(0, pos);
            }
        }
        
        public string PrinterNameDisplay
        {
            get
            {
                return lblCurrentPrinter.Text;
            }
            set
            {
                this.lblCurrentPrinter.Text = value;
            }
        }

        public List<PrintSheet> printSheets
        {
            get
            {
                return this.m_printSheets;
            }
            set
            {
                this.m_printSheets = value;
                dgvSheets.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dgvSheets.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvSheets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvSheets.RowHeadersVisible = false;

                //FASTER
                // let's make a datatable of the list of m_printSheets and bind this to the dgv.
                DataTable dtSheets = new DataTable();
                DataColumn col = new DataColumn("Print", typeof(bool));
                col.DefaultValue = false;
                dtSheets.Columns.Add(col);

                col = new DataColumn("Zone", typeof(string));
                col.DefaultValue = "";
                dtSheets.Columns.Add(col);

                col = new DataColumn("Level", typeof(string));
                col.DefaultValue = "";
                dtSheets.Columns.Add(col);

                col = new DataColumn("Type", typeof(string));
                col.DefaultValue = "";
                dtSheets.Columns.Add(col);

                col = new DataColumn("Role", typeof(string));
                col.DefaultValue = "";
                dtSheets.Columns.Add(col);

                col = new DataColumn("Number", typeof(string));
                col.DefaultValue = "";
                dtSheets.Columns.Add(col);

                col = new DataColumn("Title", typeof(string));
                col.DefaultValue = "";
                dtSheets.Columns.Add(col);

                col = new DataColumn("Current Revision Date", typeof(string));
                col.DefaultValue = "";
                dtSheets.Columns.Add(col);

                col = new DataColumn("PrintSheet", typeof(PrintSheet));
                dtSheets.Columns.Add(col);

                foreach (PrintSheet ps in m_printSheets)
                {
                    dtSheets.Rows.Add(ps.toPrint, ps.Zone, ps.Level, ps.Type, ps.Role, ps.Number, ps.Title, ps.CurrentRevisionDate, ps);
                }

                dgvSheets.DataSource = dtSheets;


                // SLOOOOOWW

                /*
                foreach (PrintSheet ps in this.m_printSheets)
                {
                    bs1.Add(ps);
                }

                dgvSheets.AutoGenerateColumns = false;
                dgvSheets.DataSource = bs1;

                DataGridViewColumn col;

                col = new DataGridViewCheckBoxColumn();
                col.DataPropertyName = "toPrint";
                col.Name = "Print";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Zone";
                col.Name = "Zone";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Level";
                col.Name = "Level";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Type";
                col.Name = "Type";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Role";
                col.Name = "Role";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Number";
                col.Name = "Number";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Rev";
                col.Name = "Rev";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Title";
                col.Name = "Title";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "DrawnBy";
                col.Name = "Drawn By";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "CheckedBy";
                col.Name = "Checked By";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "SheetIssueDate";
                col.Name = "Date";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "CurrentRevisionDate";
                col.Name = "Current Rev Date";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "CurrentRevisionDescription";
                col.Name = "Rev Description";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Width";
                col.Name = "Width";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Height";
                col.Name = "Height";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "ASize";
                col.Name = "Page Size";
                dgvSheets.Columns.Add(col);

                col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = "Orientation";
                col.Name = "Orientation";
                dgvSheets.Columns.Add(col);

                */

                // make all columns except checkbox uneditable
                foreach (DataGridViewColumn column in dgvSheets.Columns)
                {
                    if (column.Name != "Print")
                    {
                        column.ReadOnly = true;
                    }

                    if (column.Name == "PrintSheet")
                    {
                        column.Visible = false;
                    }
                }


                // re-enable resizing: (faster)
                dgvSheets.RowHeadersVisible = true;
                dgvSheets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
        }

        

        private void dgvSheets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvSheets.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //DataGridViewRow row = this.dgvSheets.Rows[e.RowIndex];
            //PrintSheet ps =  (PrintSheet)row.Cells["PrintSheet"].Value;

            this.m_SendToPrint.Clear();
            bs2.Clear();

            foreach (DataGridViewRow row in dgvSheets.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Print"].Value))
                {
                    m_SendToPrint.Add((PrintSheet)row.Cells["PrintSheet"].Value);
                    bs2.Add((PrintSheet)row.Cells["PrintSheet"].Value);
                }
            }

            /*if (Convert.ToBoolean(row.Cells["Print"].Value))
            {
                m_SendToPrint.Add(ps);
                bs2.Add(ps);
            } else
            {
                m_SendToPrint.Remove(ps);
                bs2.Remove(ps);
            } */

            //this.m_SendToPrint.Clear();
            //bs2.Clear();
            
            /*foreach (PrintSheet sheet in m_printSheets)
            {
                if (sheet.toPrint)
                {
                    m_SendToPrint.Add(sheet);
                    bs2.Add(sheet);
                    debugString.AppendLine(sheet.DrawingNumber + "-" + sheet.Title);
                }
            } */

            dgvToPrint.AutoGenerateColumns = false;
            dgvToPrint.Columns.Clear();
            dgvToPrint.DataSource = bs2;

            DataGridViewColumn col;
            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Filename";
            col.Name = "To Print";
            dgvToPrint.Columns.Add(col);
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvRow in dgvSheets.Rows)
            {
                dgvRow.Cells["Print"].Value = "true";
            }
            dgvSheets_CellContentClick(sender, new DataGridViewCellEventArgs(0, 0));
        }

        private void btnCheckNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvRow in dgvSheets.Rows)
            {
                dgvRow.Cells["Print"].Value = "false";
            }
            dgvSheets_CellContentClick(sender, new DataGridViewCellEventArgs(0, 0));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void btnBrowseForDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                //m_printMgr.PrintToFileName = folderBrowserDialog1.SelectedPath;
                tbSaveToDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            PrintParameters pp = m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters;

            if (m_SendToPrint.Count() == 0)
            {
                TaskDialog.Show("No sheets selected", "Please select some sheets to print.");
                return;
            }

            if (tbSaveToDir.Text == "")
            {
                TaskDialog.Show("No save folder selected", "Please select a folder to save PDFs.");
                return;
            }

            if (TaskDialog.Show("Print PDFs","You're about to print " + m_SendToPrint.Count() + " sheets to the folder " + tbSaveToDir.Text + ". Are you sure?", TaskDialogCommonButtons.Ok).Equals(TaskDialogResult.Cancel))
            {
                return;
            }
            

            foreach (PrintSheet ps in m_SendToPrint)
            {
                // set the relevant settings in the printmanager for this sheet
                using (Transaction tran = new Transaction(m_doc, "setup"))
                {
                    tran.Start();

                    // set the paper source to <default tray> for adobe pdf
                    foreach (PaperSource psource in m_printMgr.PaperSources)
                    {
                        if (psource.Name.Equals("<default tray>"))
                        {
                            m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.PaperSource = psource;
                            break;
                        }
                    }
                    //debug1.Text = m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.PaperSource.Name;

                    // ensure the paper size of the titleblock is present in the available papersizes for the AdobePDF printer
                    foreach (PaperSize paperSize in m_printMgr.PaperSizes)
                    {
                        if (paperSize.Name == ps.ASize)
                        {
                            pp.PaperSize = paperSize;
                            //TaskDialog.Show("paper size for sheet " + ps.Filename, paperSize.Name); //debug
                        }
                    }

                    if (pp.PaperSize.Name != ps.ASize)
                    {
                        TaskDialog taskDialog = new TaskDialog("Paper Size Error");
                        taskDialog.MainContent = "Sheet " + ps.Filename + " with paper size " + ps.ASize + " cannot be printed by this plugin.";
                        taskDialog.Show();
                        continue;
                    }

                    if (ps.Orientation == "L")
                    {
                        pp.PageOrientation = PageOrientationType.Landscape;
                    }
                    else if (ps.Orientation == "P")
                    {
                        pp.PageOrientation = PageOrientationType.Portrait;
                    }
                    else if (ps.Orientation == "")
                    {
                        TaskDialog taskDialog = new TaskDialog("Page Orientation Error");
                        taskDialog.MainContent = "Sheet " + ps.Filename + " has no orientation (landscape or portrait) and cannot be printed by this printer. Please use a Ridge titleblock which begins 'A1L' or similar.";
                        taskDialog.Show();
                        continue;
                    }
                    else
                    {
                        TaskDialog taskDialog = new TaskDialog("Page Orientation Error");
                        taskDialog.MainContent = "Sheet " + ps.Filename + " with orientation '" + ps.Orientation + "' cannot be printed by this plugin. Adobe PDF Printer does not recognise the paper orientation.";
                        taskDialog.Show();
                        continue;
                    }

                    // set the paper placement to center, no scaling
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.PaperPlacement = PaperPlacementType.Center;
                    // debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.PaperPlacement.ToString();

                    // set the margin type (not needed since we're using Center)
                    //m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.MarginType = MarginType.PrinterLimit;
                    //debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.MarginType.ToString();

                    // set the hidden line view type
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HiddenLineViews = HiddenLineViewsType.VectorProcessing;
                    //debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HiddenLineViews.ToString();

                    // set the zoom
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.Zoom = 100;
                    //debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.Zoom.ToString();

                    // set the zoom type
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ZoomType = ZoomType.Zoom;
                    //debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ZoomType.ToString();

                    // raster quality type
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.RasterQuality = RasterQualityType.High;
                    //debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.RasterQuality.ToString();

                    // color
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ColorDepth = ColorDepthType.Color;
                    //debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ColorDepth.ToString();

                    // combined file
                    m_printMgr.CombinedFile = true;

                    // force printrange to select
                    m_printMgr.PrintRange = PrintRange.Select;

                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ViewLinksinBlue = false;
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HideScopeBoxes = true;
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HideReforWorkPlanes = true;
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HideCropBoundaries = true;
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HideUnreferencedViewTags = true;

                    Autodesk.Revit.DB.View thisView = ps.vSheet as Autodesk.Revit.DB.View;
                    ViewSet viewSet = new ViewSet();

                    viewSet.Insert(thisView);
                    ViewSheetSetting viewSheetSetting = m_printMgr.ViewSheetSetting;

                    IViewSheetSet viewSheetSet = viewSheetSetting.CurrentViewSheetSet;
                    if (viewSheetSet is ViewSheetSet)
                    {
                        // if the CurrentViewSheetSet is one view sheet set of print setup, such as "set 1"
                        viewSheetSet.Views = viewSet;
                        // we save the changes for the current view sheet set
                        viewSheetSetting.Save();
                    }
                    else if (viewSheetSet is InSessionViewSheetSet)
                    {
                        // if the CurrentViewSheetSet is in-session view sheet set of print setup
                        // for in-session view sheet set
                        // cannot use save() method for in-session set
                        viewSheetSetting.SaveAs(ps.Filename);
                        viewSheetSet.Views = viewSet;
                    }

                    /*if (tbSaveToDir.Text.EndsWith(@"\"))
                    {
                        m_printMgr.PrintToFileName = tbSaveToDir.Text + ps.Filename + ".pdf";
                    } else
                    {
                        m_printMgr.PrintToFileName = tbSaveToDir.Text + @"\" + ps.Filename + ".pdf";
                    }*/

                    

                    try
                    {
                        m_printMgr.PrintSetup.SaveAs("RidgePluginPrintSettings");
                    }
                    catch { }
                    
                    m_printMgr.Apply();
                    tran.Commit();
                }

                string fullFilePath;

                if (tbSaveToDir.Text.EndsWith(@"\"))
                {
                    fullFilePath = tbSaveToDir.Text + ps.Filename + ".pdf";
                }
                else
                {
                    fullFilePath = tbSaveToDir.Text + @"\" + ps.Filename + ".pdf";
                }


                // overwrite the Adobe PDF settings for this filename and dir
                try
                {
                    var pjcKey = Registry.CurrentUser.OpenSubKey(@"Software\Adobe\Acrobat Distiller\PrinterJobControl", true);

#if RELEASE2018
                    var appPath = @"C:\Program Files\Autodesk\Revit 2018\Revit.exe";
#elif RELEASE2017
                    var appPath = @"C:\Program Files\Autodesk\Revit 2017\Revit.exe";
#elif RELEASE2016
                    var appPath = @"C:\Program Files\Autodesk\Revit 2016\Revit.exe";
#else
                    var appPath = "";
#endif
                    pjcKey?.SetValue(appPath, fullFilePath);
                    pjcKey?.SetValue("LastPdfPortFolder - Revit.exe", tbSaveToDir.Text);
                }
                catch (Exception)
                {
                    TaskDialog.Show("ERROR printing sheet " + ps.Filename, "Couldn't access PDF driver registry settings to rename file.");
                    continue;
                }


                // delete file if exists. will prevent prompt for override existing
                try
                {
                    if (File.Exists(fullFilePath))
                    {
                        File.Delete(fullFilePath);
                    }
                } catch (Exception ex)
                {
                    TaskDialog.Show("Error overwriting file", ex.Message + " Close any open instances of this file (in Adobe Acrobat, for example) and try again." + "\r\n\r\n"
                        + "To avoid getting this error, uncheck \"View Adobe PDF results\" in Start Menu -> Printers & Scanners -> Adobe PDF -> Manage -> Printing Preferences" );
                    continue;
                }
                
                m_printMgr.SubmitPrint();
            }

            // when finished, exit the form
            this.Close();
        }

        private void folderBrowserDialog1_HelpRequest_1(object sender, EventArgs e)
        {

        }
    }
}
