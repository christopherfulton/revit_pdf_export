using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RidgeRibbon.ViewSheetInfo
{
    public partial class Debug : Form
    {
        private PrintPDFForm _ParentForm;

        public Debug(String report, PrintPDFForm parentForm)
        {
            InitializeComponent();
            tbDebug.Text = report;
            _ParentForm = parentForm;
        }

        public void AddLine(String line)
        {
            tbDebug.AppendText("\r\n");
            tbDebug.AppendText(line);
        }
    }
}
