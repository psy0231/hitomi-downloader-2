/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            cbType.Text = "Korean";
        }

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            download_queue = new HitomiQueue(HitomiQueueCallback);
        }

        private string GetHitomiSearchAddress()
        {
            string target = "";
            switch (cbType.Text) {
                case "Korean": target = HitomiSearch.GetWithKorean(numPages.Value.ToString()); break;
                case "Artist": target = HitomiSearch.GetWithArtist(tbSearch.Text, tbLang.Text, numPages.Value.ToString()); break;
                case "Series": target = HitomiSearch.GetWithSeries(tbSearch.Text, tbLang.Text, numPages.Value.ToString()); break;
                case "Tag": target = HitomiSearch.GetWithTags(tbSearch.Text, tbLang.Text, numPages.Value.ToString(), tbSex.Text == "female"); break;
                case "Type": target = HitomiSearch.GetWithType(tbSearch.Text, tbLang.Text, numPages.Value.ToString()); break;
                case "Character": target = HitomiSearch.GetWithCharacter(tbSearch.Text, tbLang.Text, numPages.Value.ToString()); break;
                case "Group": target = HitomiSearch.GetWithGroup(tbSearch.Text, tbLang.Text, numPages.Value.ToString()); break;
            }
            return target;
        }
        
        private void tbSearch_TextChanged(object sender, System.EventArgs e)
        { tbSearchUrl.Text = GetHitomiSearchAddress(); }
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        { tbSearchUrl.Text = GetHitomiSearchAddress(); }

        private void bSearch_Click(object sender, System.EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.DownloadStringCompleted += CallbackSearch;
            wc.DownloadStringAsync(new Uri(GetHitomiSearchAddress()));
            if (numPages.Value != numPages.Maximum)
                numPages.Value += 1;
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.DownloadStringCompleted += CallbackSearch;
            wc.DownloadStringAsync(new Uri(GetHitomiSearchAddress()));
            if (numPages.Value != numPages.Maximum)
                numPages.Value += 1;
        }

        private void CallbackSearch(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null) return;

            string max_page = HitomiParser.ParseMaxPage(e.Result);
            lPages.Text = "/ " + max_page;
            numPages.Maximum = Convert.ToInt32(max_page);
            
            foreach (HitomiArticle ha in HitomiParser.ParseArticles(e.Result))
            {
                string temp = Path.GetTempFileName();
                WebClient wc = new WebClient();
                wc.Headers["Accept-Encoding"] = "application/x-gzip";
                wc.Encoding = Encoding.UTF8;
                wc.DownloadFileCompleted += CallbackThumbnail;
                wc.DownloadFileAsync(new Uri(HitomiDef.HitomiThumbnail + ha.Thumbnail), temp, 
                    new Tuple<string, HitomiArticle> (temp, ha));
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
            pe.SetToolTip(tooltip);
            pe.SetContextMenuStrip(ctxMenu);
            
            pe.Font = this.Font;

            pe.MouseEnter += Thumbnail_MouseEnter;
            pe.MouseLeave += Thumbnail_MouseLeave;
            pe.MouseClick += Thunbnail_MouseClick;
            pe.MouseDoubleClick += Thunbnail_MouseDoubleClick;

            lock (stayed) {
                // 중복되는 항목 처리
                foreach (var a in stayed)
                    if (a.Article.Title == pe.Article.Title)
                    { pe.Article.Title += " " + pe.Article.Magic; break; }
                stayed.Add(pe);
            }
            lock (ImagePanel) ImagePanel.Controls.Add(pe);
            Application.DoEvents();
        }

        private void bTidy_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ImagePanel.Controls.Count; i++)
            {
                if (!(ImagePanel.Controls[i] as PicElement).Downloaded)
                {
                    for (int j = 0; j < stayed.Count; j++)
                    {
                        if (stayed[j] == (ImagePanel.Controls[i] as PicElement))
                        {
                            stayed.RemoveAt(j);
                            break;
                        }
                    }
                    ImagePanel.Controls.RemoveAt(i--);
                }
            }
        }

        private void 이작가로검색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    Control sourceControl = owner.SourceControl;

                    PicElement pic;
                    if (sourceControl as PictureBox != null)
                    {
                        pic = sourceControl.Parent as PicElement;
                    }
                    else
                    {
                        pic = sourceControl as PicElement;
                    }
                    
                    tbSearch.Text = pic.Article.Artists;
                    cbType.SelectedIndex = 1;
                    numPages.Value = 1;
                    tbSearchUrl.Text = GetHitomiSearchAddress();
                    bSearch.PerformClick();
                }
            }
        }

        private void Thumbnail_MouseEnter(object sender, EventArgs e)
        { ((PicElement)sender).MouseIn = true; }
        private void Thumbnail_MouseLeave(object sender, EventArgs e)
        { ((PicElement)sender).MouseIn = false; }
        private void Thunbnail_MouseClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) { ((PicElement)sender).Selected = !((PicElement)sender).Selected; } }
        private void Thunbnail_MouseDoubleClick(object sender, EventArgs e)
        { if (((MouseEventArgs)e).Button == MouseButtons.Left) { ((PicElement)sender).OpenUrl(); } }

        private void bChooseAll_Click(object sender, EventArgs e)
        {
            foreach (PicElement pe in stayed)
                pe.Selected = true;
        }
        private void bCancleAll_Click(object sender, EventArgs e)
        {
            foreach (PicElement pe in stayed)
                pe.Selected = false;
        }

        private void bDownload_Click(object sender, EventArgs e)
        {
            tbDownloadPath.Enabled = false;
            if (!tbDownloadPath.Text.EndsWith("\\"))
                tbDownloadPath.Text += "\\";
            foreach (PicElement pe in stayed)
            {
                if (pe.Selected)
                {
                    if (pe.Downloaded) continue;
                    pe.Downloaded = pe.Downloading = true;
                    pe.Invalidate();
                    AddArticle(pe);
                }
            }
        }

        List<PicElement> downloaded_check = new List<PicElement>();

        public void RemoteDownloadArticle(PicElement pe)
        {
            AddArticle(pe);
            tabControl1.SelectedIndex = 2;
        }

        private void AddArticle(PicElement pe)
        {
            HitomiLog.Instance.AddArticle(pe.Article);
            HitomiLog.Instance.Save();
            downloaded_check.Add(pe);
            HitomiCore.DownloadAndSetImageLink(pe, ImageLinkCallback);
        }
        int count = 0;
        private void ImageLinkCallback(PicElement pe)
        {
            lock (lvStandBy)
            {
                Directory.CreateDirectory(MakeDownloadDirectory(pe.Article));
                for (int i = 0; i < pe.Article.ImagesLink.Count; i++)
                {
                    ++count;
                    lvStandBy.Items.Add(new ListViewItem(new string[]
                    {
                            count.ToString(),
                            pe.Label,
                            HitomiDef.GetDownloadImageAddress(pe.Article.Magic, pe.Article.ImagesLink[i])
                    }));
                    download_queue.Add(HitomiDef.GetDownloadImageAddress(pe.Article.Magic, pe.Article.ImagesLink[i]), Path.Combine(
                        MakeDownloadDirectory(pe.Article), pe.Article.ImagesLink[i]),
                        count);
                    download_check.Add(pe.Label);
                    IncrementProgressBarMax();
                }
                HitomiJson hitomi_json = new HitomiJson(MakeDownloadDirectory(pe.Article));
                hitomi_json.SetModelFromArticle(pe.Article);
                hitomi_json.Save();
            }
        }

        HitomiQueue download_queue;
        List<string> download_check=new List<string>();

        private string MakeDownloadDirectory(HitomiArticle article)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string title = article.Title;
            string artists = article.Artists;
            foreach (char c in invalid) title = title.Replace(c.ToString(), "");
            foreach (char c in invalid) artists = artists.Replace(c.ToString(), "");
            return Regex.Replace(Regex.Replace(tbDownloadPath.Text, "{Title}", title), "{Artists}", artists);
        }

        private void HitomiQueueCallback(string uri, string filename, object obj)
        {
            IncrementProgressBarValue();
            DeleteSpecificItem(((int)obj).ToString());
            UpdateLabel($"{pbTarget.Value}/{pbTarget.Maximum}");
        }

        private void IncrementProgressBarMax()
        {
            if (pbTarget.InvokeRequired)
            {
                Invoke(new Action(IncrementProgressBarMax));
                return;
            }
            pbTarget.Maximum += 1;
        }
        private void IncrementProgressBarValue()
        {
            if (pbTarget.InvokeRequired)
            {
                Invoke(new Action(IncrementProgressBarValue));
                return;
            }
            pbTarget.Value += 1;
        }
        private void DeleteSpecificItem(string i)
        {
            if (lvStandBy.InvokeRequired)
            {
                Invoke(new Action<string>(DeleteSpecificItem), new object[] { i });
                return;
            }
            string title = "";
            for (int j = 0; j < lvStandBy.Items.Count; j++)
            {
                if (lvStandBy.Items[j].SubItems[0].Text == i)
                {
                    title = lvStandBy.Items[j].SubItems[1].Text;
                    lvStandBy.Items.RemoveAt(j);
                    break;
                }
            }
            lock (download_check)
            lock (downloaded_check)
            {
                download_check.Remove(title);
                List<string> copy = download_check.ToList();
                foreach (var elem in downloaded_check)
                {
                    if (elem.Downloading == true)
                    {
                        if (!copy.Contains(elem.Label))
                        { lock (elem) elem.Downloading = false; }
                    }
                }
            }
        }
        private void UpdateLabel(string v)
        {
            if (lStatus.InvokeRequired)
            {
                Invoke(new Action<string>(UpdateLabel), new object[] { v });
                return;
            }
            lStatus.Text = v;
        }

        public HitomiData hitomi_data = new HitomiData();
        private async void bDataNew_ClickAsync(object sender, EventArgs e)
        {
            bDataNew.Enabled = false;
            bDataOpen.Enabled = false;
            pbLoad.Visible = true;
            pbLoad.MarqueeAnimationSpeed = 10;
            await hitomi_data.DownloadTagJson();
            await hitomi_data.DownloadMetadata();
            pbLoad.Visible = false;
            MessageBox.Show("데이터 작성이 완료되었습니다!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            boxData.Enabled = true;
            bStat.Enabled = true;
        }

        private async void bDataOpen_Click(object sender, EventArgs e)
        {
            bDataNew.Enabled = false;
            bDataOpen.Enabled = false;
            pbLoad.Visible = true;
            pbLoad.MarqueeAnimationSpeed = 10;
            await Task.Run(() => hitomi_data.LoadTagJson());
            await Task.Run(() => hitomi_data.LoadMetadataJson());

            if (hitomi_data.tag_collection.female == null || hitomi_data.metadata_collection == null)
            {
                MessageBox.Show("누락된 파일이 있습니다. 데이터를 새로 작성하세요.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                bDataNew.Enabled = true;
                pbLoad.Visible = false;
                return;
            }

            boxData.Enabled = true;
            bStat.Enabled = true;
            pbLoad.Visible = false;
            MessageBox.Show("데이터 로딩 완료!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bDataSearch_Click(object sender, EventArgs e)
        {
            HitomiDataQuery query = new HitomiDataQuery();
            if (tbTagInclude.Text != "")
                query.TagInclude = new List<string>(tbTagInclude.Text.Split(' '));
            if (tbTagExclude.Text != "")
                query.TagExclude = new List<string>(tbTagExclude.Text.Split(' '));
            if (tbArtists.Text != "")
                query.Artists = new List<string>(tbArtists.Text.Split(' '));
            if (tbTitle.Text != "")
                query.Title = new List<string>(tbTitle.Text.Split(' '));

            HitomiDataSearch search = new HitomiDataSearch(hitomi_data);
            List<ListViewItem> lvil = new List<ListViewItem>();
            foreach (var v in search.Search(query))
            {
                lvil.Add(new ListViewItem(new string[]
                {
                            v.ID.ToString(),
                            v.Name,
                            string.Join(",", v.Artists ?? Enumerable.Empty<string>()),
                            string.Join(",", v.Tags ?? Enumerable.Empty<string>())
                }));
            }
            lvSearch.Items.Clear();
            lvSearch.Items.AddRange(lvil.ToArray());

            lIndex.Text = lvil.Count + " 개의 항목이 검색됨";
        }

        private void bAddEntire_Click(object sender, EventArgs e)
        {
            if (lvSearch.Items.Count > 200)
            {
                MessageBox.Show("항목이 너무 많습니다.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (ListViewItem v in lvSearch.Items)
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                wc.DownloadStringCompleted += CallbackSearch2;
                wc.DownloadStringAsync(new Uri($"https://hitomi.la/galleries/{v.SubItems[0].Text}.html"), 
                    new string[] { v.SubItems[0].Text, v.SubItems[1].Text, v.SubItems[2].Text, v.SubItems[3].Text });
            }
            lvSearch.Items.Clear();
            tabControl1.SelectedIndex = 0;
        }
        private void CallbackSearch2(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null) return;

            HitomiArticle ha = HitomiParser.ParseGallery(e.Result);
            ha.Magic = (e.UserState as string[])[0];
            ha.Title = (e.UserState as string[])[1];
            ha.Artists = (e.UserState as string[])[2];
            ha.Tags = new List<string>((e.UserState as string[])[3].Split(','));
            string temp = Path.GetTempFileName();
            WebClient wc = new WebClient();
            wc.Headers["Accept-Encoding"] = "application/x-gzip";
            wc.Encoding = Encoding.UTF8;
            wc.DownloadFileCompleted += CallbackThumbnail;
            wc.DownloadFileAsync(new Uri(HitomiDef.HitomiThumbnail + ha.Thumbnail), temp,
                new Tuple<string, HitomiArticle>(temp, ha));
        }
        
        private void SearchTextBoxEnterKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                bDataSearch.PerformClick();
        }

        private void bStat_Click(object sender, EventArgs e)
        {
            (new frmStatistics(hitomi_data)).Show();
        }
    }
}
