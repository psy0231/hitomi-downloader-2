/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy.Data;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class frmGalleryInfo : Form
    {
        PicElement pic;
        HitomiMetadata metadata;
        string id;
        Form closed_form;

        public frmGalleryInfo(Form closed, PicElement pic)
        {
            InitializeComponent();

            this.pic = pic;
            closed_form = closed;
            id = pic.Article.Magic;
        }

        public frmGalleryInfo(Form closed, HitomiMetadata metadata)
        {
            InitializeComponent();

            this.metadata = metadata;
            closed_form = closed;
            id = metadata.ID.ToString();
        }

        private void frmGalleryInfo_LoadAsync(object sender, EventArgs e)
        {
            if (pic != null)
            {
                pbImage.Image = pic.Image;
                lTitle.Text = pic.Article.Title;
                lArtist.Text = pic.Article.Artists;
                lSeries.Text = pic.Article.Series;
                textBox1.Text = string.Join(",", pic.Article.Tags ?? Enumerable.Empty<string>());
            }
            else
            {
                lTitle.Text = metadata.Name;
                lArtist.Text = string.Join(",", metadata.Artists ?? Enumerable.Empty<string>());
                lSeries.Text = string.Join(",", metadata.Parodies ?? Enumerable.Empty<string>());
                textBox1.Text = string.Join(",", metadata.Tags ?? Enumerable.Empty<string>());

                Task.Run(() => download_image());
            }
        }

        private void download_image()
        {
            string localFilename = Path.GetTempFileName();
            using (WebClient client = new WebClient())
            {
                string set = client.DownloadString(new Uri(HitomiDef.HitomiGalleryAddress + metadata.ID + ".html"));
                client.DownloadFile(HitomiDef.HitomiThumbnail + HitomiParser.ParseGallery(set).Thumbnail, localFilename);
                load_image(localFilename);
            }
        }
        private void load_image(string path)
        {
            try
            {
                if (pbImage.InvokeRequired)
                {
                    Invoke(new Action<string>(load_image), new object[] { path });
                    return;
                }
                pbImage.Image = Image.FromFile(path);
            } catch { }
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
            if (pic != null)
                (new frmArtistInfo(this, pic.Article.Artists)).Show();
            else
                (new frmArtistInfo(this, metadata.Artists[0])).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var data = (Application.OpenForms[0] as frmMain).hitomi_data;
            if (data == null)
            {
                MessageBox.Show("데이터를 로드해주세요!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pic != null)
                (new frmSeriesInfo(this, pic.Article.Series)).Show();
            else
                (new frmSeriesInfo(this, metadata.Parodies[0])).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var data = (Application.OpenForms[0] as frmMain).hitomi_data;
            if (data == null)
            {
                MessageBox.Show("데이터를 로드해주세요!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Close();
            (Application.OpenForms[0] as frmMain).RemoteDownloadArticleFromId(id);
        }
    }
}
