/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy.Data;
using System;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class frmStatistics : Form
    {
        HitomiData hitomi_data;

        public frmStatistics(HitomiData hitomi_data)
        {
            InitializeComponent();

            this.hitomi_data = hitomi_data;
        }

        private void frmStatistics_Load(object sender, EventArgs e)
        {

        }
    }
}
