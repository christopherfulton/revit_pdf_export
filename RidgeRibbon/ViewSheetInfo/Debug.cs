﻿using System;
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
        public Debug(String report)
        {
            InitializeComponent();
            tbDebug.Text = report;
        }
    }
}
