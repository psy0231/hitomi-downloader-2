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

        public frmGalleryInfo(PicElement pic)
        {
            InitializeComponent();

            this.pic = pic;
        }

        private void frmGalleryInfo_Load(object sender, EventArgs e)
        {
            pbImage.Image = pic.Image;
            lTitle.Text = pic.Article.Title;
            lArtist.Text = pic.Article.Artists;
            lSeries.Text = pic.Article.Series;
            textBox1.Text = string.Join(",", pic.Article.Tags ?? Enumerable.Empty<string>());

            Task.Run(() => loadArtist(1));
        }

        private void loadArtist(int Pages)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.DownloadStringCompleted += CallbackSearch;
            wc.DownloadStringAsync(new Uri(HitomiSearch.GetWithArtist(pic.Article.Artists.Split(',')[0],"korean", Pages.ToString())));
        }

        private void CallbackSearch(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null) return;

            string max_page = HitomiParser.ParseMaxPage(e.Result);
            for (int i = 2; i <= Convert.ToInt32(max_page); i++)
                Task.Run(() => loadArtist(i));

            foreach (HitomiArticle ha in HitomiParser.ParseArticles(e.Result))
            {
                string temp = Path.GetTempFileName();
                WebClient wc = new WebClient();
                wc.Headers["Accept-Encoding"] = "application/x-gzip";
                wc.Encoding = Encoding.UTF8;
                wc.DownloadFileCompleted += CallbackThumbnail;
                wc.DownloadFileAsync(new Uri(HitomiDef.HitomiThumbnail + ha.Thumbnail), temp,
                    new Tuple<string, HitomiArticle>(temp, ha));
            }
        }

        List<PicElement> stayed = new List<PicElement>();
        private void CallbackThumbnail(object sender, AsyncCompletedEventArgs e)
        {
            PicElement pe = new PicElement();
            Tuple<string, HitomiArticle> tuple = (Tuple<string, HitomiArticle>)e.UserState;
            pe.Article = tuple.Item2;
            pe.Label = tuple.Item2.Title;
            pe.Dock = DockStyle.Bottom;
            pe.SetImageFromAddress(tuple.Item1, 150, 200);

            pe.Font = this.Font;

            pe.MouseEnter += Thumbnail_MouseEnter;
            pe.MouseLeave += Thumbnail_MouseLeave;
            pe.MouseClick += Thunbnail_MouseClick;
            pe.MouseDoubleClick += Thunbnail_MouseDoubleClick;

            lock (stayed)
            {
                // 중복되는 항목 처리
                foreach (var a in stayed)
                    if (a.Article.Title == pe.Article.Title)
                    { pe.Article.Title += " " + pe.Article.Magic; break; }
                stayed.Add(pe);
            }
            AddPe(pe);
            Application.DoEvents();
        }

        private void AddPe(PicElement pe)
        {
            try
            {
                if (ImagePanel.InvokeRequired)
                {
                    Invoke(new Action<PicElement>(AddPe), new object[] { pe });
                }
                else
                {
                    ImagePanel.Controls.Add(pe);
                }
            }
            catch { }
        }
        
        private void Thumbnail_MouseEnter(object sender, EventArgs e)
        { ((PicElement)sender).MouseIn = true; }
        private void Thumbnail_MouseLeave(object sender, EventArgs e)
        { ((PicElement)sender).MouseIn = false; }
        private void Thunbnail_MouseClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) { ((PicElement)sender).Selected = !((PicElement)sender).Selected; } }
        private void Thunbnail_MouseDoubleClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) { ((PicElement)sender).OpenUrl(); } }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var pe in stayed)
                (Application.OpenForms[0] as frmMain).RemoteDownloadArticle(pe);
            Close();
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
            (Application.OpenForms[0] as frmMain).BringToFront();
        }
    }
}
