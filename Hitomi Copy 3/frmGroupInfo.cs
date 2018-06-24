/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy.Data;
using Hitomi_Copy_2;
using Hitomi_Copy_3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class frmGroupInfo : Form
    {
        string group;
        Form closed_form;
        CancellationTokenSource Abort = new CancellationTokenSource();

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

            if (Abort.IsCancellationRequested) return;
            LoadArtist(1).Catch();
            gallery_count -= 25;
            int gallery_c = 2;
            while (gallery_count > 0)
            {
                if (Abort.IsCancellationRequested) return;
                LoadArtist(gallery_c++).Catch();
                gallery_count -= 25;
            }
        }

        private async Task LoadArtist(int Pages)
        {
            string lang = HitomiSetting.Instance.GetModel().Language.ToLower();
            var uri = new Uri(HitomiSearch.GetWithGroup(group, lang, Pages.ToString()));
            LogEssential.Instance.PushLog(() => $"Load artist pages {uri}");

            if (Abort.IsCancellationRequested) return;
            string html = await Util.PlainWebClient().DownloadStringTaskAsync(uri);

            foreach (HitomiArticle ha in HitomiParser.ParseArticles(html))
            {
                if (Abort.IsCancellationRequested) return;
                LoadThumbnail(ha).Catch();
            }
        }

        List<PicElement> stayed = new List<PicElement>();
        private async Task LoadThumbnail(HitomiArticle article)
        {
            var thumbnailUri = new Uri(HitomiDef.HitomiThumbnail + article.Thumbnail);
            Stream thumbnail = await Util.PlainWebClient().OpenReadTaskAsync(thumbnailUri);
            if (Abort.IsCancellationRequested) return;

            PicElement pe = new PicElement(this) {
                Article = article,
                Label = article.Title,
                Dock = DockStyle.Bottom,
                Font = Font
            };
            pe.SetImage(thumbnail, 150, 200);

            lock (stayed)
            {
                // 중복되는 항목 처리
                foreach (var a in stayed)
                    if (a.Article.Title == pe.Article.Title)
                    { pe.Article.Title += " " + pe.Article.Magic; pe.Label += " " + pe.Article.Magic; break; }
                stayed.Add(pe);
            }

            if (Abort.IsCancellationRequested) return;
            AddPe(pe);
            IncrementProgressBarValue();
            Application.DoEvents();
            LogEssential.Instance.PushLog(() => $"Downloaded image! {thumbnailUri}");
        }
        private void IncrementProgressBarValue()
        {
            if (Abort.IsCancellationRequested) return;
            this.Post(() => {
                pbLoad.Value += 1;
                if (pbLoad.Value == pbLoad.Maximum)
                    pbLoad.Visible = false;
            });
        }
        private void AddPe(PicElement pe)
        {
            if (Abort.IsCancellationRequested) return;
            this.Post(() => {
                ImagePanel.Controls.Add(pe);
                SortThumbnail();
            });
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
            Abort.Cancel();

            try { closed_form.BringToFront(); } catch { }

            ImagePanel.Controls.OfType<Control>().ToList().ForEach(x => x.Dispose());
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void bTidy_Click(object sender, EventArgs e)
        {
            var titles = new List<string>();
            ImagePanel.SuspendLayout();
            for (int i = 0; i < ImagePanel.Controls.Count; i++)
            {
                string ttitle = (ImagePanel.Controls[i] as PicElement).Label.Split('|')[0];
                if ((ImagePanel.Controls[i] as PicElement).Overlap ||
                    (titles.Count > 0 && !titles.TrueForAll((title) => StringAlgorithms.get_diff(ttitle, title) > HitomiSetting.Instance.GetModel().TextMatchingAccuracy)))
                {
                    stayed.Remove(ImagePanel.Controls[i] as PicElement);
                    ImagePanel.Controls.RemoveAt(i--);
                    continue;
                }

                titles.Add(ttitle);
            }
            ImagePanel.ResumeLayout();
        }

        private void 모두선택AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ImagePanel.Controls.Count; i++)
            {
                (ImagePanel.Controls[i] as PicElement).Selected = true;
            }
        }

        private void 모두선택취소CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ImagePanel.Controls.Count; i++)
            {
                (ImagePanel.Controls[i] as PicElement).Selected = false;
            }
        }

        private void 제목비슷한작품선택취소SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> titles = new List<string>();
            ImagePanel.SuspendLayout();
            for (int i = 0; i < ImagePanel.Controls.Count; i++)
            {
                string ttitle = (ImagePanel.Controls[i] as PicElement).Label.Split('|')[0];
                if ((ImagePanel.Controls[i] as PicElement).Overlap ||
                    (titles.Count > 0 && !titles.TrueForAll((title) => StringAlgorithms.get_diff(ttitle, title) > HitomiSetting.Instance.GetModel().TextMatchingAccuracy)))
                {
                    (ImagePanel.Controls[i] as PicElement).Selected = false;
                    continue;
                }

                titles.Add(ttitle);
            }
            ImagePanel.ResumeLayout();
        }
    }
}
