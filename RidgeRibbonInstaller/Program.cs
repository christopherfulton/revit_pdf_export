using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using Autodesk.RevitAddIns;
using System.IO;
using System.Reflection;

namespace RidgeRibbonInstaller
{
    public class RidgeRibbonInstaller
    {
        static void Main(string[] args)
        {
            try
            {
                if (!Install())
                {
                    MessageBox.Show("RidgeRibbon was not installed. No valid Revit installation was found.", "RidgeRibbon");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RidgeRibbon");
            }
        }

        private static readonly Guid AppGuid = new Guid("604b1052-f742-4951-8576-c261d1993109");
        private const string AppClass = "RidgeRibbon.App";

        public static bool Install()
        {
            if (RevitProductUtility.GetAllInstalledRevitProducts().Count == 0)
            {
                return false;
            }

            foreach (var product in RevitProductUtility.GetAllInstalledRevitProducts())
            {
                if (product.Version == RevitVersion.Unknown) continue;
                var addinFile = product.CurrentUserAddInFolder + "\\RidgeRibbon.addin";
                var pluginFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + string.Format("\\RidgeRibbon{0}.dll", product.Version);
                //MessageBox.Show(Assembly.GetExecutingAssembly().Location + string.Format("\\RidgeRibbon{0}.dll", product.Version), "RidgeRibbon");
                var manifest = File.Exists(addinFile) ? AddInManifestUtility.GetRevitAddInManifest(addinFile) : new RevitAddInManifest();

                if (!File.Exists(pluginFile))
                {
                    MessageBox.Show(string.Format("{0} is not supported by this version of the RidgeRibbon plugin.", product.Version), "RidgeRibbon", MessageBoxButton.OK, MessageBoxImage.Error);
                    continue;
                }

                // search manifest for the app
                RevitAddInApplication app = null;
                foreach (var a in manifest.AddInApplications)
                {
                    if (a.AddInId == AppGuid)
                    {
                        app = a;
                    }
                }

                if (app == null)
                {
                    app = new RevitAddInApplication("RidgeRibbon", pluginFile, AppGuid, AppClass, "Chris Fulton");
                    manifest.AddInApplications.Add(app);
                }
                else
                {
                    app.Assembly = pluginFile;
                    app.FullClassName = AppClass;
                }

                if (manifest.Name == null)
                {
                    manifest.SaveAs(addinFile);
                }
                else
                {
                    manifest.Save();
                }

                MessageBox.Show(string.Format("RidgeRibbon for {0} was successfully installed.", product.Version), "RidgeRibbon");

            }

            return true;

        }
    }
}
