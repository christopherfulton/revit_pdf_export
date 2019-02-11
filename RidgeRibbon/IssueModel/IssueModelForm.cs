using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RidgeRibbon.IssueModel
{
    public partial class IssueModelForm : Form
    {
        public IssueModelForm()
        {
            InitializeComponent();
        }

        

        private void IssueModelForm_Load(object sender, EventArgs e)
        {
            cbRVT.Checked = Properties.Settings.Default.IssueRVTEnable;
            cbNWF.Checked = Properties.Settings.Default.IssueNWFEnable;
            cbIFC.Checked = Properties.Settings.Default.IssueIFCEnable;
            cbDWF.Checked = Properties.Settings.Default.IssueDWFEnable;
        }
    }
}
