using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI.Selection;

namespace RidgeRibbon.RoomSheets
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
                // prompt user to select a room (assuming they're on a plan view), use the point at which they click as the centre.
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Document doc = uidoc.Document;
                ISelectionFilter selFilter = new RoomSelectionFilter(doc);


                Reference pickedRoomRef = uidoc.Selection.PickObject(ObjectType.Element, selFilter, "Select a room");

                Room room = doc.GetElement(pickedRoomRef) as Room;

                LocationPoint locPt = room.Location as LocationPoint;
                XYZ pt = locPt.Point;
                TaskDialog.Show("Room Selected", "Creating sheet for " + room.Name, TaskDialogCommonButtons.Ok);

                // get the view family type for floor plan
                IEnumerable<ViewFamilyType> viewFamilyTypesFloorPlans = from elem in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                                                              let type = elem as ViewFamilyType
                                                              where type.ViewFamily == ViewFamily.FloorPlan
                                                              select type;

                ViewFamilyType floorPlanFamilyType = null;
                foreach (ViewFamilyType familyType in viewFamilyTypesFloorPlans)
                {
                    if (familyType.Name == "Floor Plan")
                    {
                        floorPlanFamilyType = familyType;
                        break;
                    }
                }

                IEnumerable<ViewFamilyType> viewFamilyTypesCeilingPlans = from elem in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                                                              let type = elem as ViewFamilyType
                                                              where type.ViewFamily == ViewFamily.CeilingPlan
                                                              select type;

                ViewFamilyType ceilingPlanFamilyType = null;
                foreach (ViewFamilyType familyType in viewFamilyTypesCeilingPlans)
                {
                    if (familyType.Name == "Ceiling Plan")
                    {
                        ceilingPlanFamilyType = familyType;
                        break;
                    }
                }

                IEnumerable<ViewFamilyType> viewFamilyTypesElevations = from elem in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                                                                          let type = elem as ViewFamilyType
                                                                          where type.ViewFamily == ViewFamily.Elevation
                                                                          select type;

                ViewFamilyType elevationViewFamilyType = null;
                foreach (ViewFamilyType familyType in viewFamilyTypesElevations)
                {
                    if (familyType.Name == "Int (Room Elevations)")
                    {
                        elevationViewFamilyType = familyType;
                        break;
                    }
                }

                // get the View Template to apply
                ElementId templateId = null;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.RoomSheetFloorPlanTemplate))
                {
                    IEnumerable<View> views = new FilteredElementCollector(doc).OfClass(typeof(View))
                        .Cast<View>()
                        .Where(v => v.Name.Equals(Properties.Settings.Default.RoomSheetFloorPlanTemplate));
                    View template = views.FirstOrDefault();
                    templateId = template.Id;
                }

                


                //Floor plan
                ViewPlan roomFloorPlan;
                try
                {
                    using (Transaction tran = new Transaction(doc, "generate floor plan"))
                    {
                        tran.Start();

                        // generate floor plan view for the room, using bounding box extension
                        roomFloorPlan = ViewPlan.Create(doc, floorPlanFamilyType.Id, room.LevelId);
                        roomFloorPlan.Name = room.Id.ToString() + " " + room.Name + " - Floor Plan (autogenerated)";
                        roomFloorPlan.LookupParameter("Title on Sheet").Set(room.Name + " - Floor Plan");

                        Double expandFeet = UnitUtils.Convert(Properties.Settings.Default.ExpandPlans, DisplayUnitType.DUT_MILLIMETERS, DisplayUnitType.DUT_DECIMAL_FEET);

                        BoundingBoxXYZ bbx = room.get_BoundingBox(roomFloorPlan);
                        XYZ MaxPoint = new XYZ(bbx.Max.X + expandFeet, bbx.Max.Y + expandFeet, bbx.Max.Z);
                        XYZ MinPoint = new XYZ(bbx.Min.X - expandFeet, bbx.Min.Y - expandFeet, bbx.Min.Z);
                        bbx.Max = MaxPoint;
                        bbx.Min = MinPoint;
                        roomFloorPlan.CropBoxActive = true;
                        roomFloorPlan.CropBox = bbx;
                        roomFloorPlan.CropBoxVisible = true;
                        roomFloorPlan.Scale = Properties.Settings.Default.RoomSheetScale;
                        roomFloorPlan.DetailLevel = ViewDetailLevel.Fine;
                        if (templateId != null)
                        {
                            roomFloorPlan.ViewTemplateId = templateId;
                        }
                        
                        
                        tran.Commit();
                    }
                        
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error creating Floor Plan", ex.Message);
                    return Result.Failed;
                }

                // get the View Template to apply to the ceiling plan
                ElementId ceilingtemplateId = null;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.RoomSheetFloorPlanTemplate))
                {
                    IEnumerable<View> views = new FilteredElementCollector(doc).OfClass(typeof(View))
                        .Cast<View>()
                        .Where(v => v.Name.Equals(Properties.Settings.Default.RoomSheetCeilingPlanTemplate));
                    View template = views.FirstOrDefault();
                    ceilingtemplateId = template.Id;
                }
                



                //Ceiling Plan


                ViewPlan roomCeilingPlan;
                try
                {
                    using (Transaction tran = new Transaction(doc, "generate ceiling plan"))
                    {
                        tran.Start();

                        // generate ceiling plan view for the room, using bounding box extension
                        roomCeilingPlan = ViewPlan.Create(doc, ceilingPlanFamilyType.Id, room.LevelId);
                        roomCeilingPlan.Name = room.Id.ToString() + " " + room.Name + " - Ceiling Plan (autogenerated)";
                        roomCeilingPlan.LookupParameter("Title on Sheet").Set(room.Name + " - Ceiling Plan");

                        Double expandFeet = UnitUtils.Convert(Properties.Settings.Default.ExpandPlans, DisplayUnitType.DUT_MILLIMETERS, DisplayUnitType.DUT_DECIMAL_FEET);

                        BoundingBoxXYZ bbx = room.get_BoundingBox(roomCeilingPlan);
                        XYZ MaxPoint = new XYZ(bbx.Max.X + expandFeet, bbx.Max.Y + expandFeet, bbx.Max.Z);
                        XYZ MinPoint = new XYZ(bbx.Min.X - expandFeet, bbx.Min.Y - expandFeet, bbx.Min.Z);
                        bbx.Max = MaxPoint;
                        bbx.Min = MinPoint;
                        roomCeilingPlan.CropBoxActive = true;
                        roomCeilingPlan.CropBox = bbx;
                        roomCeilingPlan.CropBoxVisible = true;
                        roomCeilingPlan.Scale = Properties.Settings.Default.RoomSheetScale;
                        roomCeilingPlan.DetailLevel = ViewDetailLevel.Fine;
                        if (ceilingtemplateId != null)
                        {
                            roomCeilingPlan.ViewTemplateId = ceilingtemplateId;
                        }

                        tran.Commit();
                    }

                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error creating Ceiling Plan", ex.Message);
                    return Result.Failed;
                }




                // create elevation marker at room "centre" point and create views

                // get the View Template to apply to the elevation views
                ElementId elevationtemplateId = null;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.RoomSheetElevationTemplate))
                {
                    IEnumerable<View> views = new FilteredElementCollector(doc).OfClass(typeof(View))
                        .Cast<View>()
                        .Where(v => v.Name.Equals(Properties.Settings.Default.RoomSheetElevationTemplate));
                    View template = views.FirstOrDefault();
                    elevationtemplateId = template.Id;
                }

                List<ViewSection> eleViews = new List<ViewSection>();

                Transaction tran2 = new Transaction(doc, "generate elevation marker and views");
                tran2.Start();
                ElevationMarker marker = ElevationMarker.CreateElevationMarker(doc, elevationViewFamilyType.Id, pt, 20);
                for (int i = 0; i < 4; i++)
                {
                    ViewSection elevationView = marker.CreateElevation(doc, roomFloorPlan.Id, i);
                    elevationView.Name = room.Id.ToString() + " " + room.Name + " - Elevation " + i.ToString() + " (autogenerated)";
                    String compass = "";
                    if (i == 0)
                    {
                        compass = "West";
                    }
                    else if (i == 1)
                    {
                        compass = "North";
                    }
                    else if (i == 2)
                    {
                        compass = "East";
                    }
                    else if (i == 3)
                    {
                        compass = "South";
                    }
                    elevationView.LookupParameter("Title on Sheet").Set(room.Name + " - Elevation " + compass);
                    elevationView.CropBoxActive = true;
                    elevationView.CropBoxVisible = true;
                    elevationView.Scale = Properties.Settings.Default.RoomSheetScale;
                    elevationView.DetailLevel = ViewDetailLevel.Fine;
                    if (elevationtemplateId != null)
                    {
                        elevationView.ViewTemplateId = elevationtemplateId;
                    }

                    eleViews.Add(elevationView);
                }

                tran2.Commit();

                Transaction tran3;
                foreach (ViewSection elev in eleViews)
                {
                    tran3 = new Transaction(doc, "expand the cropbox of the elevation view");
                    tran3.Start();

                    // get the bounding box of the section. Use the unbounded height of the room to set top of crop for elevations.
                    Double expandFeet = UnitUtils.Convert(Properties.Settings.Default.ExpandElevation, DisplayUnitType.DUT_MILLIMETERS, DisplayUnitType.DUT_DECIMAL_FEET);

                    BoundingBoxXYZ bb = elev.CropBox;
                    XYZ max = bb.Max;
                    XYZ min = bb.Min;
                    max = new XYZ(max.X + expandFeet, min.Y + room.UnboundedHeight + expandFeet, max.Z);
                    min = new XYZ(min.X - expandFeet, min.Y - expandFeet, min.Z);
                    
                    bb.Max = max;
                    bb.Min = min;
                    elev.CropBox = bb;
                    doc.Regenerate();

                    tran3.Commit();
                }


                // create a sheet
                Transaction tran4 = new Transaction(doc, "creating sheet");
                tran4.Start();

                FamilyInstance titleblock = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance))
                    .OfCategory(BuiltInCategory.OST_TitleBlocks).Cast<FamilyInstance>()
                    .First(q => q.Name.Contains(Properties.Settings.Default.RoomSheetTitleblock));

                String newSheetNumber = Properties.Settings.Default.RoomSheetNumberPrefix + room.Number;
                String newSheetName = room.Name + " Plans and Elevations";
                if (IsValidSheetNumber(doc, newSheetNumber) && IsValidSheetName(doc, newSheetName))
                {
                    ViewSheet newSheet = ViewSheet.Create(doc, titleblock.GetTypeId());
                    newSheet.Name = newSheetName;
                    newSheet.SheetNumber = newSheetNumber;

                    Double sheetBottomLeftU = newSheet.Outline.Min.U;
                    Double sheetBottomLeftV = newSheet.Outline.Min.V;
                    Double sheetTopRightU = newSheet.Outline.Max.U;
                    Double sheetTopRightV = newSheet.Outline.Max.V;
                    Double sheetCentreU = (sheetBottomLeftU + sheetTopRightU) / 2.0;
                    Double sheetCentreV = (sheetBottomLeftV + sheetTopRightV) / 2.0;


                    // move floor plan into place
                    Viewport floorPlanVp = Viewport.Create(doc, newSheet.Id, roomFloorPlan.Id, new XYZ(0, 0, 0));
                    BoundingBoxXYZ bbFloorPlanVp = floorPlanVp.get_BoundingBox(newSheet);
                    Double bbFloorPlanVpWidth = bbFloorPlanVp.Max.X - bbFloorPlanVp.Min.X;
                    Double bbFloorPlanVpHeight = bbFloorPlanVp.Max.Y - bbFloorPlanVp.Min.Y;

                    ElementTransformUtils.MoveElement(doc, floorPlanVp.Id, new XYZ(sheetCentreU - (bbFloorPlanVpWidth / 2) - 0.05, sheetCentreV, 0));
                    
                    // move ceiling plan into place
                    Viewport ceilingPlanVp = Viewport.Create(doc, newSheet.Id, roomCeilingPlan.Id, new XYZ(0, 0, 0));
                    BoundingBoxXYZ bbCeilingPlanVp = ceilingPlanVp.get_BoundingBox(newSheet);
                    Double bbCeilingPlanVpWidth = bbCeilingPlanVp.Max.X - bbCeilingPlanVp.Min.X;
                    Double bbCeilingPlanVpHeight = bbCeilingPlanVp.Max.Y - bbCeilingPlanVp.Min.Y;

                    ElementTransformUtils.MoveElement(doc, ceilingPlanVp.Id, new XYZ(sheetCentreU + (bbFloorPlanVpWidth / 2) + 0.05, sheetCentreV, 0));

                    int i = 0;
                    foreach (ViewSection elev in eleViews)
                    {
                        Viewport elevVP = Viewport.Create(doc, newSheet.Id, elev.Id, new XYZ(0, 0, 0));
                        BoundingBoxXYZ bbelevVP = elevVP.get_BoundingBox(newSheet);
                        Double bbelevVPWidth = bbelevVP.Max.X - bbelevVP.Min.X;
                        Double bbelevVPHeight = bbelevVP.Max.Y - bbelevVP.Min.Y;

                        int multiplier = 0;
                        if (i == 0)
                        {
                            multiplier = 5;
                        }
                        else if (i == 1)
                        {
                            multiplier = 4;
                        }
                        else if (i == 2)
                        {
                            multiplier = 2;
                        }
                        else if (i == 3)
                        {
                            multiplier = 1;
                        }

                        ElementTransformUtils.MoveElement(doc, elevVP.Id, new XYZ(sheetCentreU, ((sheetTopRightV - sheetBottomLeftV) / 6) * multiplier, 0));   

                        i++;
                    }




                } else
                {
                    TaskDialog.Show("Error", "Sheet with name " + newSheetName + " and number " + newSheetNumber + " already exists. Trying to tidy up created views...", TaskDialogCommonButtons.Ok);
                    doc.Delete(roomFloorPlan.Id);
                    doc.Delete(roomCeilingPlan.Id);
                    foreach (ViewSection elev in eleViews)
                    {
                        doc.Delete(elev.Id);
                    }
                    return Result.Failed;
                }

                tran4.Commit();

                
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }

        public class RoomSelectionFilter : ISelectionFilter
        {
            Document doc = null;
            public RoomSelectionFilter(Document document)
            {
                doc = document;
            }

            public bool AllowElement(Element element)
            {
                if (element is Room)
                {
                    // returns true if the object is a room
                    return true;
                }
                return false;
            }

            public bool AllowReference(Reference refer, XYZ point)
            {
                
                return false;
            }
        }

        private bool IsValidSheetNumber(Document doc, string NewNumber)
        {
            // is NewNumber empty?
            if (String.IsNullOrEmpty(NewNumber))
            {
                return false;
            }

            // check if there are any other sheets with the same number

            FilteredElementCollector sheets = new FilteredElementCollector(doc).OfClass(typeof(ViewSheet));
            foreach (ViewSheet sheet in sheets)
            {
                if (sheet.SheetNumber.Equals(NewNumber))
                {

                    return false;
                }
            }

            return true;
        }

        private bool IsValidSheetName(Document doc, string NewName)
        {
            // is NewName empty?
            if (String.IsNullOrEmpty(NewName))
            {
                return false;
            }

            // check if there are any other sheets with the same name

            FilteredElementCollector sheets = new FilteredElementCollector(doc).OfClass(typeof(ViewSheet));
            foreach (ViewSheet sheet in sheets)
            {
                if (sheet.Name.Equals(NewName))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
