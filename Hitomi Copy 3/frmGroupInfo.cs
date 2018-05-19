/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy.Data;
using Hitomi_Copy_2;
using Hitomi_Copy_3;
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
    public partial class frmGroupInfo : Form
    {
        string group;
        Form closed_form;

        public frmGroupInfo(Form closed, string group)
        {
            InitializeComponent();

            this.group = group;
            closed_form = closed;
        }

        private void frmArtistInfo_Load(object sender, EventArgs e)
        {
            Text += group;
            var hitomi_data = HitomiData.Instance.metadata_collection;
            Dictionary<string, int> tag_count = new Dictionary<string, int>();
            int gallery_count = 0;
            foreach (var metadata in hitomi_data)
                if (metadata.Groups != null && metadata.Tags != null && (metadata.Language == HitomiSetting.Instance.GetModel().Language || HitomiSetting.Instance.GetModel().Language == "ALL") && metadata.Groups.Contains(group))
                {
                    gallery_count += 1;
                    foreach (var tag in metadata.Tags)
                        if (tag_count.ContainsKey(tag))
                            tag_count[tag] += 1;
                        else
                            tag_count.Add(tag, 1);
                }

            var result = tag_count.ToList();
            result.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            List<ListViewItem> lvil = new List<ListViewItem>();
            for (int i = 0; i < result.Count; i++)
            {
                lvil.Add(new ListViewItem(new string[]
                {
                    result[i].Key,
                    result[i].Value.ToString()
                }));
            }
            lvMyTagRank.Items.AddRange(lvil.ToArray());

            pbLoad.Maximum = gallery_count;
            Task.Run(() => loadArtist(1));
            gallery_count -= 25;
            int gallery_c = 2;
            while (gallery_count > 0)
            {
                Task.Run(() => loadArtist(gallery_c++));
                gallery_count -= 25;
            }
        }

        private void loadArtist(int Pages)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.DownloadStringCompleted += CallbackSearch;
            wc.DownloadStringAsync(new Uri(HitomiSearch.GetWithGroup(group, HitomiSetting.Instance.GetModel().Language, Pages.ToString())));
        }

        private void CallbackSearch(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null) return;
            
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
            PicElement pe = new PicElement(this);
            Tuple<string, HitomiArticle> tuple = (Tuple<string, HitomiArticle>)e.UserState;
            pe.Article = tuple.Item2;
            pe.Label = tuple.Item2.Title;
            pe.Dock = DockStyle.Bottom;
            pe.SetImageFromAddress(tuple.Item1, 150, 200);

            pe.Font = this.Font;
            
            lock (stayed)
            {
                // 중복되는 항목 처리
                foreach (var a in stayed)
                    if (a.Article.Title == pe.Article.Title)
                    { pe.Article.Title += " " + pe.Article.Magic; pe.Label += " " + pe.Article.Magic; break; }
                stayed.Add(pe);
            }
            AddPe(pe);
            IncrementProgressBarValue();
            Application.DoEvents();
        }
        private void IncrementProgressBarValue()
        {
            if (pbLoad.InvokeRequired)
            {
                // form 꺼지면 오류남
                try { Invoke(new Action(IncrementProgressBarValue)); } catch { }
                return;
            }
            try
            {
                pbLoad.Value += 1;
                if (pbLoad.Value == pbLoad.Maximum)
                    pbLoad.Visible = false;
            }
            catch { }
            
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
                    SortThumbnail();
                }
            }
            catch { }
        }

        private void SortThumbnail()
        {
            List<Control> controls = new List<Control>();
            for (int i = 0; i < ImagePanel.Controls.Count; i++)
                controls.Add(ImagePanel.Controls[i]);
            controls.Sort((a, b) => Convert.ToUInt32((b as PicElement).Article.Magic).CompareTo(Convert.ToUInt32((a as PicElement).Article.Magic)));
            for (int i = 0; i < controls.Count; i++)
                ImagePanel.Controls.SetChildIndex(controls[i], i);
        }

        private void bDownloadAll_Click(object sender, EventArgs e)
        {
            foreach (var pe in stayed)
            {
                pe.Downloading = true;
                if (pe.Article.Artists != null)
                    pe.Article.Artists[0] = group;
                (Application.OpenForms[0] as frmMain).RemoteDownloadArticle(pe);
            }
            Close();
            (Application.OpenForms[0] as frmMain).BringToFront();
        }

        private void bDownload_Click(object sender, EventArgs e)
        {
            foreach (var pe in stayed)
                if (pe.Selected)
                {
                    pe.Downloading = true;
                    if (pe.Article.Artists != null)
                        pe.Article.Artists[0] = group;
                    (Application.OpenForms[0] as frmMain).RemoteDownloadArticle(pe);
                }
            (Application.OpenForms[0] as frmMain).BringToFront();
        }

        private void frmArtistInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { closed_form.BringToFront(); } catch { }

            foreach (var pe in ImagePanel.Controls)
                (pe as PicElement).Dispose();
        }
    }
}
