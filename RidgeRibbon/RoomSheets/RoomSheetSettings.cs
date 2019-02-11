using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RidgeRibbon.RoomSheets
{
    public partial class RoomSheetSettings : Form
    {
        public RoomSheetSettings()
        {
            InitializeComponent();
        }

        private void RoomSheetSettings_Load(object sender, EventArgs e)
        {
            tbRoomSheetNumberPrefix.Text = Properties.Settings.Default.RoomSheetNumberPrefix;
            tbRoomSheetScale.Text = Properties.Settings.Default.RoomSheetScale.ToString();
            tbRoomSheetTitleblock.Text = Properties.Settings.Default.RoomSheetTitleblock;
            tbRoomSheetFloorPlanTemplate.Text = Properties.Settings.Default.RoomSheetFloorPlanTemplate;
            tbRoomSheetCeilingPlanTemplate.Text = Properties.Settings.Default.RoomSheetCeilingPlanTemplate;
            tbRoomSheetElevationTemplate.Text = Properties.Settings.Default.RoomSheetElevationTemplate;
            tbExpandElevation.Text = Properties.Settings.Default.ExpandElevation.ToString();
            tbExpandPlans.Text = Properties.Settings.Default.ExpandPlans.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RoomSheetNumberPrefix = tbRoomSheetNumberPrefix.Text;
            Properties.Settings.Default.RoomSheetScale = int.Parse(tbRoomSheetScale.Text);
            Properties.Settings.Default.RoomSheetTitleblock = tbRoomSheetTitleblock.Text;
            Properties.Settings.Default.RoomSheetFloorPlanTemplate = tbRoomSheetFloorPlanTemplate.Text;
            Properties.Settings.Default.RoomSheetCeilingPlanTemplate = tbRoomSheetCeilingPlanTemplate.Text;
            Properties.Settings.Default.RoomSheetElevationTemplate = tbRoomSheetElevationTemplate.Text;
            Properties.Settings.Default.ExpandElevation = int.Parse(tbExpandElevation.Text);
            Properties.Settings.Default.ExpandPlans = int.Parse(tbExpandPlans.Text);
            
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
