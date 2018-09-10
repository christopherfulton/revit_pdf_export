using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;

namespace RidgeRibbon
{
    class App : IExternalApplication
    {
        //define a method that will create our tab and button
        static void AddRibbonPanel(UIControlledApplication application)
        {
            // create a custom ribbon tab
            String tabName = "Ridge";
            application.CreateRibbonTab(tabName);

            // Add a new ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Tools");

            // get dll assembly path
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            // create push button for ViewPrinter
            /*PushButtonData b1Data = new PushButtonData(
                "cmdSheetPrinter",
                "Print" + System.Environment.NewLine + "   sheets to PDF   ",
                thisAssemblyPath,
                "RidgeRibbon.SheetPrinter.Command"); */

            /*PushButton pb1 = ribbonPanel.AddItem(b1Data) as PushButton;
            pb1.ToolTip = "Print to PDF in a much more clever way, with correctly-named and numbered PDF files";
            BitmapImage pb1Image = new BitmapImage(new Uri("pack://application:,,,/RidgeRibbon;component/Resources/ridgeIcon.png"));
            pb1.LargeImage = pb1Image; */

            // create push button for ViewSheetInfo
            PushButtonData b2Data = new PushButtonData(
                "cmdViewSheetInfo",
                "Print Sheets" + System.Environment.NewLine + " to PDF & DWG ",
                thisAssemblyPath,
                "RidgeRibbon.ViewSheetInfo.Command");

            PushButton pb2 = ribbonPanel.AddItem(b2Data) as PushButton;
            pb2.ToolTip = "Print to PDF in a much more clever way, with correctly-named and numbered PDF files";
            BitmapImage pb2Image = new BitmapImage(new Uri("pack://application:,,,/RidgeRibbon;component/Resources/ridgeIcon.png"));
            pb2.LargeImage = pb2Image;
        }

        

        public Result OnShutdown(UIControlledApplication application)
        {
            //do nothing
            return Result.Succeeded;
        }

       

        public Result OnStartup(UIControlledApplication application)
        {
            // call our method that will load up our toolbar
            AddRibbonPanel(application);
            return Result.Succeeded;
        }

    }
}
