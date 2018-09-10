using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;

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
            /*StringBuilder rdgNumber = new StringBuilder();

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
            return rdgNumber.ToString(); */
            return "THIS-IS-OLD-GETRDGNUMBER"; // deprecated
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
                String format = Properties.Settings.Default.FilenameFormat;
                String[] formatParts = format.Split('<');
                String separator = "-"; // currently hard coded, might be nice to make this a user option

                List<String> entityList = new List<String>();

                foreach (String fPart in formatParts)
                {
                    if (fPart.EndsWith(">"))
                    {
                        String partName = fPart.Trim(new char[] { '>', '<' });

                        /*TaskDialog.Show("debug", "Format: " + format + "\r\n"
                            + "Parts: " + string.Join("|", formatParts) + "\r\n"
                            + "This Part: " + partName + "\r\n"
                            + "Filename so far (previous loop): " + string.Join(separator, entityList)); */

                        // cope with standard part names (which match public properties of the PrintSheet class)
                        if (partName == "ProjectNumber")
                        {
                            if (!String.IsNullOrEmpty(this.ProjectNumber))
                            {
                                entityList.Add(this.ProjectNumber);
                            }
                            continue;
                        }
                        if (partName == "RDG")
                        {
                            // this is an organisational-level string, so won't be in the project. Simply return the string.
                            entityList.Add("RDG");
                            continue;
                        }
                        if (partName == "Zone")
                        {
                            if (!String.IsNullOrEmpty(this.Zone))
                            {
                                entityList.Add(this.Zone);
                            }
                            continue;
                        }
                        if (partName == "Level")
                        {
                            if (!String.IsNullOrEmpty(this.Level))
                            {
                                entityList.Add(this.Level);
                            }
                            continue;
                        }
                        if (partName == "Type")
                        {
                            if (!String.IsNullOrEmpty(this.Type))
                            {
                                entityList.Add(this.Type);
                            }
                            continue;
                        }
                        if (partName == "Role")
                        {
                            if (!String.IsNullOrEmpty(this.Role))
                            {
                                entityList.Add(this.Role);
                            }
                            continue;
                        }
                        if (partName == "Number")
                        {
                            if (!String.IsNullOrEmpty(this.Number))
                            {
                                entityList.Add(this.Number);
                            }
                            continue;
                        }
                        if (partName == "Revision")
                        {
                            if (!String.IsNullOrEmpty(this.Rev))
                            {
                                entityList.Add(this.Rev);
                            }
                            continue;
                        }
                        if (partName == "Title")
                        {
                            if (!String.IsNullOrEmpty(this.Title))
                            {
                                entityList.Add(this.Title);
                            }
                            continue;
                        }
                        else
                        {
                            // a non-standard paramater has been requested. check if it exists and if so add it to the list.
                            String tryGetParameter = getParameterAsString(vSheet, partName, "");
                            if (!String.IsNullOrEmpty(tryGetParameter))
                            {
                                entityList.Add(tryGetParameter);
                            } else
                            {
                                //entityList.Add(partName); // don't do anything - this is a parameter with no value.
                            }
                        }
                    }
                }
                if (entityList.Count == 0)
                {
                    throw new Exception("Filename format is invalid - cannot construct any filename for sheet.");
                }
                return SanitiseFilename(string.Join(separator, entityList));
            }
        }
        public string ProjectNumber
        {
            get
            {
                return this.doc.ProjectInformation.Number;
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

        private string SanitiseFilename(string testName)
        {
            var pattern = Path.GetInvalidFileNameChars();
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                testName = testName.Replace(invalidChar, '-');
            }

            return testName;
        }
    }
}
