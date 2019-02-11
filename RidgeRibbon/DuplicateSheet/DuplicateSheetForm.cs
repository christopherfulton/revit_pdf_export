using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB;

namespace RidgeRibbon.DuplicateSheet
{
    public partial class DuplicateSheetForm : System.Windows.Forms.Form
    {

        private Document m_doc;
        private ViewSheet vsActive;

        public DuplicateSheetForm(Document doc)
        {
            m_doc = doc;

            // find out whether current active view is a sheet
            Autodesk.Revit.DB.View currentActiveView = m_doc.ActiveView;

            if (!(currentActiveView is ViewSheet))
            {
                MessageBox.Show("This tool only works when the active view is a Sheet, rather than a view/schedule/legend/etc. Please navigate to a sheet from the project browser and try again.");
                this.Close();
            }

            try
            {
                vsActive = currentActiveView as ViewSheet;
            }
            catch
            {
                // if not, quit with a warning
                MessageBox.Show("This tool only works when the active view is a Sheet, rather than a view/schedule/legend/etc. Please navigate to a sheet from the project browser and try again.");
                this.Close();
            }
            
            InitializeComponent();
        }

        private void DuplicateSheetForm_Load(object sender, EventArgs e)
        {
            // on form load fill in the default values for the Curr textboxes
            tbCurrZone.Text = vsActive.LookupParameter("Title - Zone").AsString();
            tbCurrLevel.Text = vsActive.LookupParameter("Title - Level").AsString();
            tbCurrType.Text = vsActive.LookupParameter("Title - Type").AsString();
            tbCurrRole.Text = vsActive.LookupParameter("Title - Role").AsString();
            tbCurrNumber.Text = vsActive.LookupParameter("Sheet Number").AsString();
            tbCurrTitle.Text = vsActive.Name;



            // also fill in everything except for the number
            tbNewZone.Text = tbCurrZone.Text;
            tbNewLevel.Text = tbCurrLevel.Text;
            tbNewType.Text = tbCurrType.Text;
            tbNewRole.Text = tbCurrRole.Text;
            tbNewNumber.Text = "";
            tbNewTitle.Text = tbCurrTitle.Text;

            // show the relevant icon
            bool valid = IsValidSheetNumber(tbNewNumber.Text) && IsValidSheetName(tbNewTitle.Text);
            pbTick.Visible = valid;
            pbWarning.Visible = !valid;
        }

        private bool IsValidSheetNumber(string NewNumber)
        {
            // is NewNumber empty?
            if (String.IsNullOrEmpty(NewNumber))
            {
                return false;
            }

            // check if there are any other sheets with the same number

            FilteredElementCollector sheets = new FilteredElementCollector(m_doc).OfClass(typeof(ViewSheet));
            foreach (ViewSheet sheet in sheets)
            {
                if (sheet.SheetNumber.Equals(NewNumber))
                {
                    
                    return false;
                }
            }
            
            return true;
        }

        private bool IsValidSheetName(string NewName)
        {
            // is NewName empty?
            if (String.IsNullOrEmpty(NewName))
            {
                return false;
            }

            // check if there are any other sheets with the same name

            FilteredElementCollector sheets = new FilteredElementCollector(m_doc).OfClass(typeof(ViewSheet));
            foreach (ViewSheet sheet in sheets)
            {
                if (sheet.Name.Equals(NewName))
                {
                    return false;
                }
            }
            
            return true;
        }

        private void tbNewNumber_TextChanged(object sender, EventArgs e)
        {
            bool valid = IsValidSheetNumber(tbNewNumber.Text) && IsValidSheetName(tbNewTitle.Text);
            pbTick.Visible = valid;
            pbWarning.Visible = !valid;
            btnDuplicate.Enabled = valid;
        }

        private void tbNewTitle_TextChanged(object sender, EventArgs e)
        {
            bool valid = IsValidSheetNumber(tbNewNumber.Text) && IsValidSheetName(tbNewTitle.Text);
            pbTick.Visible = valid;
            pbWarning.Visible = !valid;
            btnDuplicate.Enabled = valid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            // where the magic happens

            // check the validity of the data entered
            if (!IsValidSheetNumber(tbNewNumber.Text))
            {
                MessageBox.Show("Invalid Sheet Number");
                return;
            }

            if (!IsValidSheetName(tbNewTitle.Text))
            {
                MessageBox.Show("Invalid Sheet Name");
                return;
            }

            using (Transaction t = new Transaction(m_doc, "Duplicate Sheet"))
            {
                t.Start();

                // find out the id of the titleblock used on the current sheet
                FamilyInstance titleblock = new FilteredElementCollector(m_doc).OfClass(typeof(FamilyInstance))
                    .OfCategory(BuiltInCategory.OST_TitleBlocks).Cast<FamilyInstance>()
                    .First(q => q.OwnerViewId == vsActive.Id);

                ViewSheet newSheet = ViewSheet.Create(m_doc, titleblock.GetTypeId());
                newSheet.SheetNumber = tbNewNumber.Text;
                newSheet.Name = tbNewTitle.Text;
                newSheet.LookupParameter("Title - Zone").Set(tbNewZone.Text);
                newSheet.LookupParameter("Title - Level").Set(tbNewLevel.Text);
                newSheet.LookupParameter("Title - Type").Set(tbNewType.Text);
                newSheet.LookupParameter("Title - Role").Set(tbNewRole.Text);

                // all views except schedules
                foreach (ElementId eid in vsActive.GetAllPlacedViews())
                {
                    Autodesk.Revit.DB.View ev = m_doc.GetElement(eid) as Autodesk.Revit.DB.View;

                    Autodesk.Revit.DB.View newView = null;

                    // legends
                    if (ev.ViewType == ViewType.Legend)
                    {
                        newView = ev;
                    }

                    // non-legend and non-schedule views
                    else
                    {
                        ElementId newViewId = ev.Duplicate(ViewDuplicateOption.WithDetailing);
                        newView = m_doc.GetElement(newViewId) as Autodesk.Revit.DB.View;
                        newView.Name = ev.Name + " Copy";
                    }

                    foreach (Viewport vp in new FilteredElementCollector(m_doc).OfClass(typeof(Viewport)))
                    {
                        // viewport is on active sheet
                        if (vp.SheetId == vsActive.Id && vp.ViewId == ev.Id)
                        {
                            BoundingBoxXYZ vpBb = vp.get_BoundingBox(vsActive);
                            XYZ initialCenter = (vpBb.Max + vpBb.Min) / 2;
                            Viewport newVp = Viewport.Create(m_doc, newSheet.Id, newView.Id, XYZ.Zero);

                            BoundingBoxXYZ newVpBb = newVp.get_BoundingBox(newSheet);
                            XYZ newCenter = (newVpBb.Max + newVpBb.Min) / 2;

                            ElementTransformUtils.MoveElement(m_doc, newVp.Id, new XYZ(
                                initialCenter.X - newCenter.X,
                                initialCenter.Y - newCenter.Y,
                                0));
                        }
                    }

                }

                // schedules
                foreach (ScheduleSheetInstance si in (new FilteredElementCollector(m_doc).OfClass(typeof(ScheduleSheetInstance))))
                {
                    if (si.OwnerViewId == vsActive.Id)
                    {
                        if (!si.IsTitleblockRevisionSchedule)
                        {
                            foreach (ViewSchedule vsc in new FilteredElementCollector(m_doc).OfClass(typeof(ViewSchedule)))
                            {
                                if (si.ScheduleId == vsc.Id)
                                {
                                    BoundingBoxXYZ sibb = si.get_BoundingBox(vsActive);
                                    XYZ initialCenter = (sibb.Max + sibb.Min) / 2;

                                    ScheduleSheetInstance newssi = ScheduleSheetInstance.Create(m_doc, newSheet.Id, vsc.Id, XYZ.Zero);

                                    BoundingBoxXYZ newsibb = newssi.get_BoundingBox(newSheet);
                                    XYZ newCenter = (newsibb.Max + newsibb.Min) / 2;

                                    ElementTransformUtils.MoveElement(m_doc, newssi.Id, new XYZ(
                                        initialCenter.X - newCenter.X,
                                        initialCenter.Y - newCenter.Y,
                                        0));
                                }
                            }
                        }
                    }
                }

                t.Commit();

                this.Close();
            }
        }

        
    }
}
