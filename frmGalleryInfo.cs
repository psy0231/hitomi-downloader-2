/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class frmGalleryInfo : Form
    {
        PicElement pic;
        Form closed_form;

        public frmGalleryInfo(Form closed, PicElement pic)
        {
            InitializeComponent();

            this.pic = pic;
            closed_form = closed;
        }

        private void frmGalleryInfo_Load(object sender, EventArgs e)
        {
            pbImage.Image = pic.Image;
            lTitle.Text = pic.Article.Title;
            lArtist.Text = pic.Article.Artists;
            lSeries.Text = pic.Article.Series;
            textBox1.Text = string.Join(",", pic.Article.Tags ?? Enumerable.Empty<string>());
            
        }

        private void frmGalleryInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmGalleryInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { closed_form.BringToFront(); } catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = (Application.OpenForms[0] as frmMain).hitomi_data;
            if (data == null)
            {
                MessageBox.Show("데이터를 로드해주세요!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            (new frmArtistInfo(this, pic.Article.Artists)).Show();
        }
    }
}
