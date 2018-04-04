/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy.Data;
using Hitomi_Copy_2;
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
                lArtist.Text = string.Join(",", pic.Article.Artists ?? Enumerable.Empty<string>());
                lSeries.Text = string.Join(",", pic.Article.Series ?? Enumerable.Empty<string>());
                lGroup.Text = string.Join(",", pic.Article.Groups ?? Enumerable.Empty<string>());
                lCharacter.Text = string.Join(",", pic.Article.Characters ?? Enumerable.Empty<string>());
                textBox1.Text = string.Join(",", pic.Article.Tags ?? Enumerable.Empty<string>());
            }
            else
            {
                lTitle.Text = metadata.Name;
                lArtist.Text = string.Join(",", metadata.Artists ?? Enumerable.Empty<string>());
                lSeries.Text = string.Join(",", metadata.Parodies ?? Enumerable.Empty<string>());
                textBox1.Text = string.Join(",", metadata.Tags ?? Enumerable.Empty<string>());
                lGroup.Text = string.Join(",", metadata.Groups ?? Enumerable.Empty<string>());
                lCharacter.Text = string.Join(",", metadata.Characters ?? Enumerable.Empty<string>());

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
            if (pic != null)
                (new frmArtistInfo(this, pic.Article.Artists[0])).Show();
            else
                (new frmArtistInfo(this, metadata.Artists[0])).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pic != null)
                (new frmSeriesInfo(this, pic.Article.Series[0])).Show();
            else
                (new frmSeriesInfo(this, metadata.Parodies[0])).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
            (Application.OpenForms[0] as frmMain).RemoteDownloadArticleFromId(id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pic != null)
                System.Diagnostics.Process.Start($"https://hitomi.la/galleries/{pic.Article.Magic}.html");
            else
                System.Diagnostics.Process.Start($"https://hitomi.la/galleries/{metadata.ID}.html");
        }
    }
}
