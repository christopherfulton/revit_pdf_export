using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RidgeRibbon.ViewSheetInfo
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, 
            ref string message, 
            ElementSet elements)
        {
            try
            {
                Document doc = commandData.Application.ActiveUIDocument.Document;

                // check if adobe pdf printer is available. Plugin only works for this virtual printer.
                String apdf = "Adobe PDF";

                PrinterSettings.StringCollection printers
                = PrinterSettings.InstalledPrinters;
                string[] printerNames = new string[printers.Count];
                printers.CopyTo(printerNames, 0);

                Boolean hasAdobePDF = false;

                foreach (string name in printerNames)
                {
                    // loop through printerNames looking for Adobe PDF
                    if (name == apdf)
                    {
                        hasAdobePDF = true;
                    }
                }


                //  Exit with an warning to the user if not found.
                if (!hasAdobePDF)
                {
                    message = "Adobe PDF printer not found on this system. Sorry, you cannot use this plugin.";
                    return Result.Failed;
                }

                // set the current printer to Adobe PDF if available, and initialise the PrintManager and UI Form.
                PrintManager pMan = doc.PrintManager;
                try
                {
                    pMan.SelectNewPrintDriver(apdf);
                }
                catch (Exception ex)
                {
                    message = "Error setting the print driver to Adobe PDF. Exiting this plugin. __ " + ex.Message;
                    return Result.Failed;
                }
                
                
                
                FilteredElementCollector coll = new FilteredElementCollector(doc);
                coll.OfClass(typeof(ViewSheet));
                IEnumerable<ViewSheet> vSheets = coll.Cast<ViewSheet>();

                List<PrintSheet> sheets = new List<PrintSheet>();
                
                foreach (ViewSheet vs in vSheets)
                {
                    PrintSheet vSheet = new PrintSheet(vs, doc);
                    sheets.Add(vSheet);
                }

                // Populate a check-box-list of sheets available in the current document, named according to:
                // RIDGE drawing numbers, drawing title, sheet size (e.g. A1, A0), anything else useful (drawn by? checked by? date? project? in-assoc-with? rev?)

                PrintPDFForm form = new PrintPDFForm(commandData, doc);
                form.PrinterNameDisplay = pMan.PrinterName;
                form.printSheets = sheets;
                form.ShowDialog();


                
                // TODO: use some kind of in-session var to save user choices as they go, for now - implement named viewsheetsets later.

                // pre-empt as much PDF printer setup options as possible by reading sheet titleblock data (sheet size, scaling, default settings, etc)


                // expose setup options as required.


                // allow user to browse to file directory destination, and remember their choice.


                // use archi-lab registry rewrite approach to prepare system printer for overwriting file name and destination


                // warning/confirm popup to user showing number of sheets sent to print, and explanation of where they'll go

                // on confirm, send to print


                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
            
        }
    }
}
