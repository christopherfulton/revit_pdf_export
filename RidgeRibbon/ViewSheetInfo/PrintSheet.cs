using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RidgeRibbon.ViewSheetInfo
{
    public class PrintSheet
    {
        public ViewSheet vSheet;
        private Document doc;
        private Boolean printMe;

        // instantiate method
        public PrintSheet(ViewSheet vs, Document d)
        {
            vSheet = vs;
            doc = d;
        }

        public Boolean toPrint
        {
            get
            {
                return this.printMe;
            }
            set
            {
                this.printMe = value;
            }
        }

        private String getRdgNumber()
        {
            StringBuilder rdgNumber = new StringBuilder();

            String name = this.vSheet.Name.ToString();

            String separator = "-";
            String projectNumber = this.doc.ProjectInformation.Number;
            String org = "RDG";

            if (this.Rev == "" || this.Rev == "-")
            {
                rdgNumber.AppendFormat("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}",
                separator,
                projectNumber,
                org,
                this.Zone,
                this.Level,
                this.Type,
                this.Role,
                this.Number
                );
            } else
            {
                rdgNumber.AppendFormat("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                separator,
                projectNumber,
                org,
                this.Zone,
                this.Level,
                this.Type,
                this.Role,
                this.Number,
                this.Rev
                );
            }
            return rdgNumber.ToString();
        }

        private String getParameterAsString(ViewSheet vs, String paramName, String defaultVal)
        {
            //String param = defaultVal;

            try
            {
                Parameter p = vs.LookupParameter(paramName);
                if (p.AsString() != "")
                {
                    return p.AsString();
                } else
                {
                    return defaultVal;
                }
            } catch
            {
                return defaultVal;
            }
            
        }

        public string DrawingNumber
        {
            get
            {
                return getRdgNumber();
            }
        }
        public string Filename
        {
            get
            {
                return DrawingNumber + "-" + Title;
            }
        }
        public string Zone
        {
            get
            {
                return getParameterAsString(vSheet, "Title - Zone", "XX");
            }
        }
        public string Level
        {
            get
            {
                return getParameterAsString(vSheet, "Title - Level", "XX");
            }
        }
        public string Type
        {
            get
            {
                return getParameterAsString(vSheet, "Title - Type", "XX");
            }
        }
        public string Role
        {
            get
            {
                return getParameterAsString(vSheet, "Title - Role", "X");
            }
        }
        public string Number
        {
            get
            {
                return getParameterAsString(vSheet, "Sheet Number", "");
            }
        }
        public string Rev
        {
            get
            {
                return getParameterAsString(vSheet, "Current Revision", "");
            }
        }
        public string Title
        {
            get
            {
                return vSheet.Name.ToString();
            }
        }
        public string SheetIssueDate
        {
            get
            {
                return getParameterAsString(vSheet, "Sheet Issue Date", "");
            }
        }
        public string CurrentRevisionDate
        {
            get
            {
                return getParameterAsString(vSheet, "Current Revision Date", "");
            }
        }
        public string CurrentRevisionDescription
        {
            get
            {
                return getParameterAsString(vSheet, "Current Revision Description", "");
            }
        }
        public string DrawnBy
        {
            get
            {
                return getParameterAsString(vSheet, "Drawn By", "");
            }
        }
        public string CheckedBy
        {
            get
            {
                return getParameterAsString(vSheet, "Checked By", "");
            }
        }
        public string Status
        {
            get
            {
                return getParameterAsString(vSheet, "RS_Sheet Status", "");
            }
        }
        public string Scale
        {
            get
            {
                return getParameterAsString(vSheet, "Scale", "");
            }
        }
        public string TitleBlockName
        {
            get
            {
                string returnName = "";
                FilteredElementCollector col = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_TitleBlocks)
                    .OfClass(typeof(FamilyInstance));
                foreach (FamilyInstance fi in col)
                {
                    String vsNumberString = fi.get_Parameter(BuiltInParameter.SHEET_NUMBER).AsString();
                    if (vsNumberString == vSheet.SheetNumber)
                    {
                        returnName = fi.Name;
                        return returnName;
                    }
                }
                return returnName;
            }
        }
        /*public string[] SheetSize
        {
            get
            {
                string[] whsize = new string[2];
                FilteredElementCollector col = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_TitleBlocks)
                    .OfClass(typeof(FamilyInstance));
                foreach (FamilyInstance fi in col)
                {
                    String vsNumberString = fi.get_Parameter(BuiltInParameter.SHEET_NUMBER).AsString();
                    if (vsNumberString == vSheet.SheetNumber)
                    {
                        whsize[0] = fi.get_Parameter(BuiltInParameter.SHEET_WIDTH).AsValueString();
                        whsize[1] = fi.get_Parameter(BuiltInParameter.SHEET_HEIGHT).AsValueString();
                    }
                }
                return whsize;
            }
        }*/
        /*public string Width
        {
            get
            {
                return this.SheetSize[0];
            }
        }
        public string Height
        {
            get
            {
                return this.SheetSize[1];
            }
        }*/
        public string ASize
        {
            get
            {
                
                /*string[] a0l = new string[] { "1190", "840" };
                string[] a0p = new string[] { "840", "1190" };
                string[] a1l = new string[] { "841", "594" };
                string[] a1p = new string[] { "594", "841" };
                string[] a2l = new string[] { "594", "420" };
                string[] a2p = new string[] { "420", "594" };
                string[] a3l = new string[] { "420", "297" };
                string[] a3p = new string[] { "297", "420" };
                string[] a4l = new string[] { "297", "210" };
                string[] a4p = new string[] { "210", "297" };*/

                if (TitleBlockName.Length > 1)
                {
                    return TitleBlockName.Substring(0, 2);
                }
                else
                {
                    return "";
                }
            }
        }
        public string Orientation
        {
            get
            {
                if (TitleBlockName.Length > 2)
                {
                    return TitleBlockName.Substring(2, 1);
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
