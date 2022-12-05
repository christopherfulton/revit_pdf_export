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
using Autodesk.Revit.Attributes;
using Microsoft.Win32;
using Autodesk.Revit.DB.ExtensibleStorage;
using System.Text.RegularExpressions;
using System.Diagnostics;

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

        //private String RidgeDefaultFormat = "<ProjectNumber>-RDG-<Zone>-<Level>-<Type>-<Role>-<Number>-<Revision>-<Title>"; // now stored in a setting
        //private Guid schemaGuid = new Guid("fe05519c-cbd7-4680-ad97-e5ae79c48206");

        

        public PrintPDFForm(ExternalCommandData commandData, Document doc)
        {
            m_command = commandData;
            m_doc = doc;
            m_printMgr = doc.PrintManager;
            
            InitializeComponent();

            

            
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
                
                // make all columns except checkbox uneditable, and hide the PrintSheet object column.
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

                // save the current settings for next time the form loads
                Properties.Settings.Default.OutputDirectory = tbSaveToDir.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            // keep track of the active view before the print button was pressed.
            UIDocument uidoc = m_command.Application.ActiveUIDocument;
            Autodesk.Revit.DB.View originalActiveView = uidoc.ActiveView;
            

            dbgLine("New Print Job started...");
            dbgLine(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
            dbgLine(" ");

            //TaskDialog.Show("Print Settings", m_printMgr.PrintSetup.CurrentPrintSetting.ToString());
            m_printMgr.PrintSetup.CurrentPrintSetting = m_printMgr.PrintSetup.InSession;
            m_printMgr.Apply();

            dbgLine("m_printMgr.PrintSetup.CurrentPrintSetting = " + m_printMgr.PrintSetup.CurrentPrintSetting.ToString());

            // save whatever's in the output directory textbox to the properties
            Properties.Settings.Default.OutputDirectory = tbSaveToDir.Text;
            Properties.Settings.Default.Save();
            

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

            dbgLine("Showed print confirm taskDialog for " + m_SendToPrint.Count() + " sheets.");


            foreach (PrintSheet ps in m_SendToPrint)
            {

                Guid tmpGuid = Guid.NewGuid();

                dbgLine("Created GUID for this sheet, " + tmpGuid.ToString());

                Autodesk.Revit.DB.View thisView = ps.vSheet as Autodesk.Revit.DB.View;

                // set the current active view to be the one you next want to print
                dbgLine("Setting view " + thisView.Name + " to be the active view");
                uidoc.ActiveView = thisView;

                // set the relevant settings in the printmanager for this sheet
                using (Transaction tran = new Transaction(m_doc, "setup"))
                {
                    tran.Start();
                    dbgLine("Starting transaction...");

                    //m_printMgr.PrintSetup.CurrentPrintSetting = m_printMgr.PrintSetup.InSession;
                    //TaskDialog.Show("Print Settings after Transaction start", m_printMgr.PrintSetup.CurrentPrintSetting.ToString());

                    // set the paper source to <default tray> for adobe pdf
                    foreach (PaperSource psource in m_printMgr.PaperSources)
                    {
                        if (psource.Name.Equals("<default tray>"))
                        {
                            m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.PaperSource = psource;
                            dbgLine("Set paper source to " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.PaperSource.Name);
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
                            dbgLine("Set paper size to " + paperSize.Name);
                        }
                    }

                    if (pp.PaperSize.Name != ps.ASize)
                    {
                        TaskDialog taskDialog = new TaskDialog("Paper Size Error");
                        taskDialog.MainContent = "Sheet " + ps.Filename + " with paper size " + ps.ASize + " cannot be printed by this plugin. You need to use standard Ridge titleblocks (or rename your custom ones), with names that start 'A1L', 'A2P' for example. The first two characters define the paper size (A1, A2, A3) and the third character defines the orientation (L for landscape, P for portrait).";
                        taskDialog.Show();
                        dbgLine("Sheet " + ps.Filename + " with paper size " + ps.ASize + " cannot be printed by this plugin. You need to use standard Ridge titleblocks (or rename your custom ones), with names that start 'A1L', 'A2P' for example. The first two characters define the paper size (A1, A2, A3) and the third character defines the orientation (L for landscape, P for portrait).");
                        tran.RollBack();
                        continue;
                    }

                    if (ps.Orientation == "L")
                    {
                        pp.PageOrientation = PageOrientationType.Landscape;
                        dbgLine("Orientation: Landscape");
                    }
                    else if (ps.Orientation == "P")
                    {
                        pp.PageOrientation = PageOrientationType.Portrait;
                        dbgLine("Orientation: Portrait");
                    }
                    else if (ps.Orientation == "")
                    {
                        TaskDialog taskDialog = new TaskDialog("Page Orientation Error");
                        taskDialog.MainContent = "Sheet " + ps.Filename + " has no orientation (landscape or portrait) and cannot be printed by this printer. Please use a Ridge titleblock which begins 'A1L' or similar.";
                        taskDialog.Show();
                        tran.RollBack();
                        dbgLine("Sheet " + ps.Filename + " has no orientation (landscape or portrait) and cannot be printed by this printer. Please use a Ridge titleblock which begins 'A1L' or similar.");
                        dbgLine("ps.Orientation is set to a blank string.");
                        continue;
                    }
                    else
                    {
                        TaskDialog taskDialog = new TaskDialog("Page Orientation Error");
                        taskDialog.MainContent = "Sheet " + ps.Filename + " with orientation '" + ps.Orientation + "' cannot be printed by this plugin. Adobe PDF Printer does not recognise the paper orientation.";
                        taskDialog.Show();
                        tran.RollBack();
                        dbgLine("Sheet " + ps.Filename + " with orientation '" + ps.Orientation + "' cannot be printed by this plugin. Adobe PDF Printer does not recognise the paper orientation.");
                        dbgLine("ps.Orientation is neither a blank string nor a recognised orientation (L or P).");
                        continue;
                    }

                    // set the paper placement to center, no scaling
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.PaperPlacement = PaperPlacementType.Center;
                    dbgLine("Paper Placement Type: " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.PaperPlacement.ToString());
                    // debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.PaperPlacement.ToString();

                    // set the margin type (not needed since we're using Center)
                    //m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.MarginType = MarginType.PrinterLimit;
                    //debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.MarginType.ToString();

                    // set the hidden line view type
                    if (cbVector.Checked)
                    {
                        m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HiddenLineViews = HiddenLineViewsType.VectorProcessing;
                        dbgLine("Hidden line view type: " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HiddenLineViews.ToString());
                    } else
                    {
                        m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HiddenLineViews = HiddenLineViewsType.RasterProcessing;
                        dbgLine("Hidden line view type: " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HiddenLineViews.ToString());
                    }

                    
                    //debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HiddenLineViews.ToString();

                    // set the zoom type
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ZoomType = ZoomType.Zoom;
                    dbgLine("Zoom type: " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ZoomType.ToString());
                    //debug1.Text += " " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ZoomType.ToString();

                    // set the zoom
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.Zoom = 100;
                    dbgLine("Zoom value: " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.Zoom.ToString());

                    // raster quality type
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.RasterQuality = RasterQualityType.High;
                    dbgLine("Raster Quality type: " + m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.RasterQuality.ToString());

                    // color
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ColorDepth = ColorDepthType.Color;
                    dbgLine("Color depth type: "+ m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ColorDepth.ToString());

                    // combined file
                    m_printMgr.CombinedFile = true;
                    dbgLine("Combined file: " + m_printMgr.CombinedFile.ToString());

                    // force printrange to select
                    m_printMgr.PrintRange = PrintRange.Current;
                    dbgLine("Print range: " + m_printMgr.PrintRange.ToString());

                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.ViewLinksinBlue = false;
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HideScopeBoxes = true;
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HideReforWorkPlanes = true;
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HideCropBoundaries = true;
                    m_printMgr.PrintSetup.CurrentPrintSetting.PrintParameters.HideUnreferencedViewTags = true;


                    

                    
                    

                    // use ViewSheetSets to ensure correct print handling. ViewSheetSet support is a bit patchy and buggy across Revit installs and for workshared projects.

                    /*
                    
                    ViewSet viewSet = new ViewSet();

                    viewSet.Insert(thisView);
                    dbgLine("Adding this view (" + thisView.Name + ") to a temporary viewSet");
                    try
                    {
                        ViewSheetSetting viewSheetSetting = m_printMgr.ViewSheetSetting;
                    } catch
                    {
                        dbgLine("Failed to save/update the named viewsheetset");

                    }
                    
                    
                    
                    m_printMgr.ViewSheetSetting.CurrentViewSheetSet = m_printMgr.ViewSheetSetting.InSession;

                    dbgLine("Setting the CurrentViewSheetSet to insession: " + m_printMgr.ViewSheetSetting.CurrentViewSheetSet.ToString());

                    IViewSheetSet viewSheetSet = m_printMgr.ViewSheetSetting.CurrentViewSheetSet;
                    if (viewSheetSet is ViewSheetSet)
                    {
                        // if the CurrentViewSheetSet is one view sheet set of print setup, such as "set 1"
                        viewSheetSet.Views = viewSet;
                        
                        // we save the changes for the current view sheet set
                        try
                        {
                            dbgLine("Trying to save/update a named viewsheetset...");
                            m_printMgr.ViewSheetSetting.Save();
                        } catch {
                            dbgLine("Failed to save/update the named viewsheetset");
                        }
                    }
                    else if (viewSheetSet is InSessionViewSheetSet)
                    {
                        // if the CurrentViewSheetSet is in-session view sheet set of print setup
                        // for in-session view sheet set
                        // cannot use save() method for in-session set

                        try
                        {
                            dbgLine("Trying to SaveAs viewsheetset (from in-session): " + "_tmp" + tmpGuid.ToString() + ps.Filename);
                            //m_printMgr.ViewSheetSetting.SaveAs("_tmp" + tmpGuid.ToString() + ps.Filename);
                        } catch {
                            dbgLine("Couldn't save viewsheetset " + "_tmp" + tmpGuid.ToString() + ps.Filename + ", continuing");
                        }
                        viewSheetSet.Views = viewSet;
                        dbgLine("No error caught.");
                    }
                    */
                    
                    
                    try
                    {
                        m_printMgr.PrintSetup.SaveAs("RidgePluginPrintSettings_DO NOT USE");
                        dbgLine("SavedAs() the printSetup to RidgePluginPrintSettings_ DO NOT USE");
                    }
                    catch (Exception ex) {
                        dbgLine("Trying to SaveAs() this m_printMgr.PrintSetup to RidgePluginPrintSettings_DO NOT USE failed. Continuing without error. Message reads:");
                        dbgLine(ex.Message);
                        try
                        {
                            dbgLine("Current m_printMgr.PrintSetup is " + m_printMgr.PrintSetup.ToString());
                            dbgLine("Saving PrintSetup.");
                            bool printSetupSaveSuccess = m_printMgr.PrintSetup.Save();
                            dbgLine("Result of Print Setup Save is: " + printSetupSaveSuccess.ToString());
                        } catch
                        {
                            dbgLine("Error saving m_printMgr.PrintSetup. Message reads:");
                            dbgLine(ex.Message);
                        }
                    }

                    

                    m_printMgr.Apply();
                    tran.Commit();
                    
                }

                string fullFilePath;

                // check file path isn't too long
                int fileNameLength = tbSaveToDir.Text.Length + ps.Filename.Length + ".pdf".Length;
                dbgLine("File path length is: " + fileNameLength.ToString() + " characters.");

                fullFilePath = Path.Combine(tbSaveToDir.Text, ps.Filename + ".pdf");

                dbgLine("Setting up file path for PDF: " + fullFilePath);

                
                // overwrite the Adobe PDF settings for this filename and dir
                try
                {
                    dbgLine("Overwriting the PDF settings in: Registry.CurrentUser.OpenSubKey(Software/Adobe/Acrobat Distiller/PrinterJobControl");
                    var pjcKey = Registry.CurrentUser.OpenSubKey(@"Software\Adobe\Acrobat Distiller\PrinterJobControl", true);

                    // get the process file path
                    Process p = Process.GetCurrentProcess();
                    string exePath = p.MainModule.FileName;
                    dbgLine("Exe path: " + exePath);

                    pjcKey?.SetValue(exePath, fullFilePath);
                    pjcKey?.SetValue("LastPdfPortFolder - Revit.exe", tbSaveToDir.Text);
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("ERROR printing sheet " + ps.Filename, "Couldn't access PDF driver registry settings to rename file.");
                    dbgLine("Error changing PDF driver registry settings to rename file. Continuing through viewsheets. Error message: ");
                    dbgLine(ex.Message);
                    continue;
                }
                

                




                // delete file if exists. will prevent prompt for override existing
                try
                {
                    if (File.Exists(fullFilePath))
                    {
                        File.Delete(fullFilePath);
                        dbgLine("Found an existing file, this will be deleted if not currently open in Adobe Acrobat Reader etc.");
                    }
                } catch (Exception ex)
                {
                    TaskDialog.Show("Error overwriting file", ex.Message + " Close any open instances of this file (in Adobe Acrobat, for example) and try again." + "\r\n\r\n"
                        + "To avoid getting this error, uncheck \"View Adobe PDF results\" in Start Menu -> Printers & Scanners -> Adobe PDF -> Manage -> Printing Preferences");
                    dbgLine("Error deleting existing pdf file. Continuing through viewsheets. Error message: ");
                    dbgLine(ex.Message);
                    continue;
                }

                try
                {
                    dbgLine("||||| Print Submitted ||||||");
                    m_printMgr.SubmitPrint();
                }
                catch (Exception ex)
                {
                    dbgLine("SubmitPrint() failed. This sheet won't print. Continuing through viewsheets. Message reads:");
                    dbgLine(ex.Message);
                    TaskDialog.Show("Error","Error submitting print for " + ps.Filename + ". Error message reads: " + ex.Message + "\r\n\r\n" + "This one won't print. Continuing through list of sheets.");
                    continue;
                }
                

                using (Transaction cleanupTran = new Transaction(m_doc, "cleanupTransation"))
                {
                    cleanupTran.Start();
                    dbgLine("Starting CleanUp Transaction");
                    /*
                    var viewSheetSettings = m_printMgr.ViewSheetSetting;
                    foreach (var viewSheetSetToDelete in (from element in new FilteredElementCollector(m_doc).OfClass(typeof(ViewSheetSet)).ToElements()
                                                          where element.Name.Contains("_tmp") && element.IsValidObject
                                                          select element as ViewSheetSet).ToList().Distinct())
                    {
                        dbgLine("Cleanup operation on ViewSheetSets. Deleting temp viewsheetset _tmp" + tmpGuid.ToString() + ps.Filename + "...");
                        //viewSheetSettings.CurrentViewSheetSet = m_printMgr.ViewSheetSetting.InSession;
                        viewSheetSettings.CurrentViewSheetSet = viewSheetSetToDelete as ViewSheetSet;
                        try
                        {
                            m_printMgr.ViewSheetSetting.Delete();
                            dbgLine("Deleting ViewSheetSetting");

                        }
                        catch (Exception ex)
                        {
                            //TaskDialog.Show("deleting temp viewsheetsetting", "failed to delete viewsheetset _tmp" + ps.Filename);
                            dbgLine("Error deleting ViewSheetSetting _tmp" + tmpGuid.ToString() + ps.Filename + ". Failing silently. Error message: ");
                            dbgLine(ex.Message);
                        }
                    }
                    */
                    cleanupTran.Commit();

                    
                }

                    


                // once we've printed the PDF, let's export a DWG file into the same location (if the user wants it)
                if (cbDwgExport.Checked)
                {
                    dbgLine("DWG option has been checked by user.");
                    DWGExportOptions dwgExportOptions = new DWGExportOptions();
                    dwgExportOptions.MergedViews = true;
                    dwgExportOptions.SharedCoords = true;
                    dwgExportOptions.Colors = ExportColorMode.TrueColor;
                    dwgExportOptions.FileVersion = ACADVersion.R2013; //assume 2013 here, if not go back to 2007.

                    ICollection<ElementId> views = new List<ElementId>();
                    Autodesk.Revit.DB.View thisViewForExport = ps.vSheet as Autodesk.Revit.DB.View;
                    views.Add(thisViewForExport.Id);

                    bool dwgSuccess = m_doc.Export(tbSaveToDir.Text, ps.Filename + ".dwg", views, dwgExportOptions);
                    dbgLine("DWG export success status is: " + dwgSuccess.ToString());
                }

                dbgLine(" ");
                dbgLine("******* end of viewsheet ******");
                dbgLine(" ");



                

            
                
            }

            // save the file output directory settings for next time (in case location has been pasted in)
            Properties.Settings.Default.OutputDirectory = tbSaveToDir.Text;
            Properties.Settings.Default.Save();

            // close the views that have been opened
            IList<UIView> uiviews = uidoc.GetOpenUIViews();
            foreach (UIView uiview in uiviews)
            {
                // close any that weren't in the original list of open views??
            }

            // when finished, exit the form
            this.Close();

            
            
        }

        private void folderBrowserDialog1_HelpRequest_1(object sender, EventArgs e)
        {

        }

        private void PrintPDFForm_Load(object sender, EventArgs e)
        {
            
            // get all the parameters associated with the viewsheets, for the combobox file format editor dropdown
            List<string> vsParams = new List<string>();
            FilteredElementCollector coll = new FilteredElementCollector(m_doc);
            coll.OfClass(typeof(ViewSheet));
            IEnumerable<ViewSheet> vSheets = coll.Cast<ViewSheet>();

            foreach (ViewSheet vs in coll)
            {
                ParameterSet pSet = vs.Parameters;
                foreach (Parameter p in pSet)
                {
                    if (!vsParams.Where(o => string.Equals(p.Definition.Name, o, StringComparison.OrdinalIgnoreCase)).Any())
                    {
                        vsParams.Add(p.Definition.Name);
                    }
                }
            }

            // sort the list alphabetically
            vsParams.Sort();

            cbParameter.DataSource = vsParams;

            // default to use the saved setting format
            tbFileNameFormat.Text = Properties.Settings.Default.FilenameFormat;

            if (Properties.Settings.Default.OutputDirectory != "")
            {
                tbSaveToDir.Text = Properties.Settings.Default.OutputDirectory;
            } else if (m_printMgr.PrintToFileName.EndsWith(@".pdf"))
            {
                int pos = m_printMgr.PrintToFileName.LastIndexOf(@"\");
                tbSaveToDir.Text = m_printMgr.PrintToFileName.Substring(0, pos);
            }
            
            cbDwgExport.Checked = Properties.Settings.Default.DWGExport;
            cbDbgReport.Checked = Properties.Settings.Default.ProduceDebugReport;
            cbVector.Checked = Properties.Settings.Default.VectorProcessing;


            // this plugin gives us issues in worksharing environments when trying to overwrite print settings or viewsheetsets. So, we switch the user to use in-session print settings.
            m_printMgr.PrintSetup.CurrentPrintSetting = m_printMgr.PrintSetup.InSession;
            //m_printMgr.ViewSheetSetting.CurrentViewSheetSet = m_printMgr.ViewSheetSetting.InSession;

            // by default, sort the datagridview by sheet Number
            dgvSheets.Sort(dgvSheets.Columns["Number"], ListSortDirection.Ascending);
            
        }

        private void btnFormatEdit_Click(object sender, EventArgs e)
        {
            btnFormatEdit.Visible = false;
            btnFormatSave.Visible = true;
            btnParameterAdd.Visible = true;
            cbParameter.Visible = true;
            btnRidgeDefault.Visible = true;
            tbFileNameFormat.ReadOnly = false;
        }

        private void btnFormatSave_Click(object sender, EventArgs e)
        {
            // is the format different to the default?
            if (!tbFileNameFormat.Text.Equals(Properties.Settings.Default.FilenameFormat))
            {
                // if so, save this format against the fileformat setting, overwriting any existing data in this field
                Properties.Settings.Default.FilenameFormat = tbFileNameFormat.Text;
                Properties.Settings.Default.Save();



                // (make sure the form checks for this data on load-time)

            }

            // propagate this new file-naming format to the PrintSheets (to affect their save name) and the dgv ToPrint (change the display of any already added to the to-print list)
            

            // set ui back to in-use mode
            btnFormatEdit.Visible = true;
            btnFormatSave.Visible = false;
            btnParameterAdd.Visible = false;
            cbParameter.Visible = false;
            btnRidgeDefault.Visible = false;
            tbFileNameFormat.ReadOnly = true;
        }

        private void btnParameterAdd_Click(object sender, EventArgs e)
        {
            // is there anything in the combobox?
            if (cbParameter.Text != "")
            {
                // if so, add it at end of the format text box with <carat> separators
                tbFileNameFormat.AppendText("<" + cbParameter.Text + ">");
            }
        }

        private void btnRidgeDefault_Click(object sender, EventArgs e)
        {
            tbFileNameFormat.Text = Properties.Settings.Default.RidgeDefaultFormat;
            btnFormatSave_Click(sender, e);
        }

        private void cbDwgExport_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DWGExport = cbDwgExport.Checked;
            Properties.Settings.Default.Save();
        }

        private void cbDbgReport_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ProduceDebugReport = cbDbgReport.Checked;
            Properties.Settings.Default.Save();
        }

        private string SanitiseFilename(string testName)
        {
            var pattern = Path.GetInvalidFileNameChars();
            foreach (char invalidChar in Path.GetInvalidFileNameChars() )
            {
                testName = testName.Replace(invalidChar, '-');
            }

            return testName;
        }

        private void cbVector_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.VectorProcessing = cbVector.Checked;
            Properties.Settings.Default.Save();
        }

        private void dbgLine(string line)
        {
            tbDebug.AppendText("\r\n");
            tbDebug.AppendText(line);

            if (cbDbgReport.Checked)
            {
                // save and update a text file in the output directory with the contents of the debug report. Constructor below will create the file if it does not exist, or add lines to it if it does.
                string filepath = Path.Combine(tbSaveToDir.Text, "debugReport.txt");

                using (var tw = new StreamWriter(filepath, true))
                {
                    tw.WriteLine(line);
                }
            }
            
            
        }
    }
}
