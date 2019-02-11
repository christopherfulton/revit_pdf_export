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
            
            // create push button for Print Sheets
            PushButtonData b2Data = new PushButtonData(
                "cmdPrintSheets",
                "Print Sheets" + System.Environment.NewLine + " to PDF & DWG ",
                thisAssemblyPath,
                "RidgeRibbon.ViewSheetInfo.Command");

            PushButton pb2 = ribbonPanel.AddItem(b2Data) as PushButton;
            pb2.ToolTip = "Print to PDF in a much more clever way, with correctly-named and numbered PDF files";
            BitmapImage pb2Image = new BitmapImage(new Uri("pack://application:,,,/RidgeRibbon;component/Resources/ridgeIcon.png"));
            pb2.LargeImage = pb2Image;

            PushButtonData b3Data = new PushButtonData(
                "cmdDuplicateSheet",
                "Duplicate" + System.Environment.NewLine + " sheet ",
                thisAssemblyPath,
                "RidgeRibbon.DuplicateSheet.Command");

            PushButton pb3 = ribbonPanel.AddItem(b3Data) as PushButton;
            pb3.ToolTip = "Duplicate current sheet with its associated views.";
            BitmapImage pb3Image = new BitmapImage(new Uri("pack://application:,,,/RidgeRibbon;component/Resources/duplicatesheets.png"));
            pb3.LargeImage = pb3Image;


            




            /*PushButtonData b1Data = new PushButtonData(
                "cmdCreateRoomSheets",
                "Create" + System.Environment.NewLine + " room sheet ",
                thisAssemblyPath,
                "RidgeRibbon.RoomSheets.Command");

            PushButton pb1 = ribbonPanel.AddItem(b1Data) as PushButton;
            pb1.ToolTip = "Create views and sheet for a selected room elevs/plans.";
            BitmapImage pb1Image = new BitmapImage(new Uri("pack://application:,,,/RidgeRibbon;component/Resources/room_icon.png"));
            pb1.LargeImage = pb1Image; */


            SplitButtonData group1Data = new  SplitButtonData("RoomSheetGroup", "Room Sheets");
            SplitButton roomSheetGroup = ribbonPanel.AddItem(group1Data) as SplitButton;

            PushButtonData roomSheetBtnData = new PushButtonData(
                "cmdCreateRoomSheets",
                "Create room sheet ",
                thisAssemblyPath,
                "RidgeRibbon.RoomSheets.Command");
            PushButton roomSheetBtn = roomSheetGroup.AddPushButton(roomSheetBtnData) as PushButton;
            roomSheetBtn.ToolTip = "Create views and sheet for a selected room elevs/plans.";
            BitmapImage roomSheetBtnImg = new BitmapImage(new Uri("pack://application:,,,/RidgeRibbon;component/Resources/room_icon.png"));
            roomSheetBtn.LargeImage = roomSheetBtnImg;

            PushButtonData roomSheetStData = new PushButtonData(
                "cmdRoomSheetSettings",
                "Room Sheet Options",
                thisAssemblyPath,
                "RidgeRibbon.RoomSheets.CommandSettings");
            PushButton roomSheetSt = roomSheetGroup.AddPushButton(roomSheetStData) as PushButton;
            roomSheetSt.ToolTip = "Change options for creating room sheets.";
            BitmapImage roomSheetStImg = new BitmapImage(new Uri("pack://application:,,,/RidgeRibbon;component/Resources/room_icon.png"));
            roomSheetSt.LargeImage = roomSheetStImg;
            
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
