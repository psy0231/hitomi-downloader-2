/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy;
using Hitomi_Copy.Data;
using Hitomi_Copy_2;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy_3
{
    public partial class frmMain : MetroFramework.Forms.MetroForm
    {
        public frmMain()
        {
            InitializeComponent();
            (new frmSplash()).Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            tbInfo.Text += "Robust Hitomi Copy Machine Version 3\r\n";
            tbInfo.Text += "Copyright (C) 2018. Hitomi Parser Developers\r\n";
            tbInfo.Text += "\r\n";
            tbInfo.Text += "오늘도 저희 Hitomi Copy를 이용해주셔서 감사합니다.\r\n";
        }

        public void OnTab()
        {
            InitDownloader();

            tbSearch.Enabled = true;
            bSearch.Enabled = true;
            bSync.Enabled = true;

            tbDownloadPath.Text = HitomiSetting.Instance.GetModel().Path;
            tbExcludeTag.Text = string.Join(", ", HitomiSetting.Instance.GetModel().ExclusiveTag ?? Enumerable.Empty<string>());

            foreach (var lang in HitomiData.Instance.GetLanguageList())
                cbLanguage.Items.Add(lang);
            cbLanguage.Items.Add("N/A");
            cbLanguage.Text = HitomiSetting.Instance.GetModel().Language;
            tbLang.Text = cbLanguage.Text;

            vThread.Value = HitomiSetting.Instance.GetModel().Thread;
            lThread.Text = vThread.Value.ToString();
        }

        #region 검색
        private void bSearch_Click(object sender, EventArgs e)
        {
            HitomiDataQuery query = new HitomiDataQuery();
            List<string> positive_data = new List<string>();
            List<string> negative_data = new List<string>();
            List<string> request_number = new List<string>();
            int start_element = 0;
            bool recent = false;
            int recent_count = 0;
            int recent_start = 0;

            tbSearch.Text.Trim().Split(' ').ToList().ForEach((a) => { if (a.StartsWith("/")) start_element = Convert.ToInt32(a.Substring(1)); });
            tbSearch.Text.Trim().Split(' ').ToList().ForEach((a) => { if (!a.Contains(":") && !a.StartsWith("/")) positive_data.Add(a.Trim()); });
            query.Common = positive_data;
            query.TagExclude = negative_data;
            foreach (var elem in tbSearch.Text.Trim().Split(' '))
            {
                if (!elem.Contains(":")) continue;
                if (elem.StartsWith("/")) continue;
                if (elem.StartsWith("tag:"))
                    if (query.TagInclude == null)
                        query.TagInclude = new List<string>() { elem.Substring("tag:".Length) };
                    else
                        query.TagInclude.Add(elem.Substring("tag:".Length));
                else if (elem.StartsWith("artist:"))
                    if (query.Artists == null)
                        query.Artists = new List<string>() { elem.Substring("artist:".Length) };
                    else
                        query.Artists.Add(elem.Substring("artist:".Length));
                else if (elem.StartsWith("series:"))
                    if (query.Series == null)
                        query.Series = new List<string>() { elem.Substring("series:".Length) };
                    else
                        query.Series.Add(elem.Substring("series:".Length));
                else if (elem.StartsWith("group:"))
                    if (query.Groups == null)
                        query.Groups = new List<string>() { elem.Substring("group:".Length) };
                    else
                        query.Groups.Add(elem.Substring("group:".Length));
                else if (elem.StartsWith("character:"))
                    if (query.Characters == null)
                        query.Characters = new List<string>() { elem.Substring("character:".Length) };
                    else
                        query.Characters.Add(elem.Substring("character:".Length));
                else if (elem.StartsWith("tagx:"))
                    if (query.TagExclude == null)
                        query.TagExclude = new List<string>() { elem.Substring("tagx:".Length) };
                    else
                        query.TagExclude.Add(elem.Substring("tagx:".Length));
                else if (elem.StartsWith("request:"))
                    request_number.Add(elem.Substring("request:".Length));
                else if (elem.StartsWith("recent:"))
                {
                    recent = true;
                    try
                    {
                        if (elem.Substring("recent:".Length).Contains("-"))
                        {
                            recent_start = Convert.ToInt32(elem.Substring("recent:".Length).Split('-')[0]);
                            recent_count = Convert.ToInt32(elem.Substring("recent:".Length).Split('-')[1]);
                        }
                        else
                            recent_count = Convert.ToInt32(elem.Substring("recent:".Length));
                    }
                    catch
                    {
                        MessageBox.Show($"recent 규칙 오류입니다. \"{elem}\"", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    tbSearch.Text = "recent:" + (recent_start + recent_count) + "-" + recent_count;
                }
                else
                {
                    MessageBox.Show($"알 수 없는 규칙입니다. \"{elem}\"", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            List<HitomiMetadata> query_result;
            if (recent == true)
            {
                query_result = HitomiDataSearch.GetSubsetOf(recent_start, recent_count);
            }
            else
            {
                query_result = HitomiDataSearch.Search2(query);
            }
            lStatusSearch.Text = $"{query_result.Count} 개 항목이 검색됨";
            if (query_result.Count > HitomiSetting.Instance.GetModel().MaximumThumbnailShow)
            {
                MessageBox.Show("검색된 항목이 너무 많습니다. 모든 결과를 보려면 고급검색 기능을 이용해주세요.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                query_result.RemoveRange(HitomiSetting.Instance.GetModel().MaximumThumbnailShow, query_result.Count - HitomiSetting.Instance.GetModel().MaximumThumbnailShow);
            }
            else if (query_result.Count == 0)
            {
                MessageBox.Show("검색된 항목이 없습니다.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (start_element != 0 && start_element <= query_result.Count) query_result.RemoveRange(0, start_element);

            pbLoad.Visible = true;
            pbLoad.Maximum += query_result.Count;
            Task.Run(() => LazyAdd(query_result));

            foreach (var request in request_number)
                RequestDownloadArticleFormId(request);
        }

        private void LazyAdd(List<HitomiMetadata> metadata_result)
        {
            foreach (var v in metadata_result)
            {
                Thread.Sleep(100);
                Task.Run(() => AddMetadataToPanel(v));
            }
        }
        #endregion

        #region 썸네일
        private void IncrementLoadProgressBarValue()
        {
            if (pbLoad.InvokeRequired)
            {
                Invoke(new Action(IncrementLoadProgressBarValue));
                return;
            }
            pbLoad.Value += 1;
        }

        private async void AddMetadataToPanel(HitomiMetadata metadata)
        {
            HitomiArticle article = HitomiCommon.MetadataToArticle(metadata);
            await Task.Run(() => article.Thumbnail = GetThumbnailAddress(article.Magic));
            AddArticleToPanel(article);
        }

        private string GetThumbnailAddress(string id)
        {
            try
            {
                WebClient wc = new WebClient
                {
                    Encoding = Encoding.UTF8
                };
                return HitomiParser.ParseGallery(wc.DownloadString(
                    new Uri($"https://hitomi.la/galleries/{id}.html"))).Thumbnail;
            }
            catch { }
            return "";
        }

        private void AddArticleToPanel(HitomiArticle article)
        {
            string temp = Path.GetTempFileName();
            WebClient wc = new WebClient();
            wc.Headers["Accept-Encoding"] = "application/x-gzip";
            wc.Encoding = Encoding.UTF8;
            wc.DownloadFileCompleted += CallbackThumbnail;
            wc.DownloadFileAsync(new Uri(HitomiDef.HitomiThumbnail + article.Thumbnail), temp,
                new Tuple<string, HitomiArticle>(temp, article));
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
                    { pe.Article.Title += " " + pe.Article.Magic; break; }
                stayed.Add(pe);
            }
            AddPanel(pe);
            Application.DoEvents();
        }
        private void AddPanel(PicElement pe)
        {
            if (ImagePanel.InvokeRequired)
            {
                Invoke(new Action<PicElement>(AddPanel), new object[] { pe }); return;
            }
            ImagePanel.Controls.Add(pe);
            IncrementLoadProgressBarValue();
            SortThumbnail();
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
        #endregion
        
        #region 다운로드 관련
        HitomiQueue download_queue;
        List<string> download_check = new List<string>();
        List<PicElement> downloaded_check = new List<PicElement>();

        private void InitDownloader()
        {
            download_queue = new HitomiQueue(HitomiQueueCallback,
                HitomiQueueDownloadSizeCallback, HitomiQueueDownloadStatusCallback, HitomiRetryCallback)
            {
                timeout_infinite = HitomiSetting.Instance.GetModel().WaitInfinite,
                timeout_ms = HitomiSetting.Instance.GetModel().WaitTimeout
            };
        }

        private void HitomiRetryCallback(string uri)
        {
            if (lRetry.InvokeRequired)
            {
                Invoke(new Action<string>(HitomiRetryCallback), new object[] { uri });
                return;
            }
            lRetry.Text = uri + "항목 다운로드를 재시작합니다.";
            lRetry.Visible = true;
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
            if (pbTarget.Value != pbTarget.Maximum) pbTarget.Value += 1;
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
                    List<PicElement> check = downloaded_check.ToList();
                    if (!copy.Contains(title))
                        foreach (var elem in check)
                            if (elem.Label == title)
                                elem.Downloading = false;
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

        long download_size = 0;
        long status_size = 0;
        object size_lock = new object();
        private void HitomiQueueDownloadSizeCallback(string uri, long size)
        {
            lock (size_lock) download_size += size;
            Task.Run(() => UpdateDownloadStatus());
        }
        private void HitomiQueueDownloadStatusCallback(string uri, int size)
        {
            lock (size_lock) status_size += size;
            Task.Run(() => UpdateDownloadStatus());
        }
        private void UpdateDownloadStatus()
        {
            if (lDownloadSize.InvokeRequired || lDownloadStatusSize.InvokeRequired)
            {
                Invoke(new Action(UpdateDownloadStatus));
                return;
            }
            lock (size_lock)
            {
                lDownloadSize.Text = ((double)download_size / 1000 / 1000).ToString("#,#.#") + " MB";
                lDownloadStatusSize.Text = ((double)status_size / 1000 / 1000).ToString("#,#.#") + " MB";
            }
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
                if (HitomiSetting.Instance.GetModel().SaveJson == true)
                {
                    HitomiJson hitomi_json = new HitomiJson(MakeDownloadDirectory(pe.Article));
                    hitomi_json.SetModelFromArticle(pe.Article);
                    hitomi_json.Save();
                }
            }
        }

        private string MakeDownloadDirectory(HitomiArticle article)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string title = article.Title ?? "";
            string artists = "";
            string type = article.Types ?? "";
            if (article.Artists != null) artists = article.Artists[0];
            if (title != null)
                foreach (char c in invalid) title = title.Replace(c.ToString(), "");
            if (artists != null)
                foreach (char c in invalid) artists = artists.Replace(c.ToString(), "");
            return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(tbDownloadPath.Text, "{Title}", title), "{Artists}", artists), "{Id}", article.Magic), "{Type}", type);
        }

        public void RemoteDownloadArticle(PicElement pe)
        {
            AddArticle(pe);
            MainTab.SelectedIndex = 1;
        }
        public void RemoteDownloadArticleFromId(string id)
        {
            MainTab.SelectedIndex = 1;
            HitomiMetadata metadata = new HitomiMetadata();
            foreach (var v in HitomiData.Instance.metadata_collection)
            {
                if (v.ID.ToString() == id)
                { metadata = v; break; }
            }
            if (metadata.ID == 0)
            {
                MessageBox.Show($"{id} 항목이 데이터베이스에 없습니다. 데이터베이스를 동기화해주세요.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PicElement fake_pe = new PicElement(this);
            HitomiArticle article = new HitomiArticle
            {
                Magic = metadata.ID.ToString(),
                Title = metadata.Name
            };
            if (metadata.Artists != null)
                article.Artists = metadata.Artists;
            if (metadata.Tags != null)
                article.Tags = metadata.Tags;
            fake_pe.Article = article;
            fake_pe.Label = metadata.Name;
            RemoteDownloadArticle(fake_pe);
        }
        public void RequestDownloadArticleFormId(string id)
        {
            PicElement fake_pe = new PicElement(this);
            HitomiArticle article = new HitomiArticle
            {
                Magic = id,
                Title = ""
            };
            fake_pe.Article = article;
            RemoteDownloadArticle(fake_pe);
        }

        private void bAbort_Click(object sender, EventArgs e)
        {
            List<string> uris = new List<string>();
            if (lvStandBy.SelectedItems.Count > 0)
                foreach (ListViewItem lvi in lvStandBy.SelectedItems)
                {
                    uris.Add(lvi.SubItems[2].Text);
                    lvi.Remove();
                }

            foreach (var uri in uris)
                download_queue.Abort(uri);
        }
        #endregion

        private void bDownload_Click(object sender, EventArgs e)
        {
            cbLanguage.Enabled = false;
            tbDownloadPath.Enabled = false;
            tbExcludeTag.Enabled = false;
            vThread.Enabled = false;

            if (!tbDownloadPath.Text.EndsWith("\\"))
                tbDownloadPath.Text += "\\";
            try
            {
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
            catch { }
        }

        private void MemoryUsageUpdateTimer_Tick(object sender, EventArgs e)
        {
            Process proc = Process.GetCurrentProcess();
            lMemoryUsage.Text = (proc.PrivateMemorySize64 / 1000).ToString("#,#") + " KB";
        }

        private void tbDownloadPath_Leave(object sender, EventArgs e)
        {
            if (!(tbDownloadPath.Text.Contains("{Id}") || tbDownloadPath.Text.Contains("{Title}")))
                if (MetroMessageBox.Show(this,
                    "다운로드 경로가 잘못 지정되었습니다. " +
                    "{Id} 또는 {Title}를 반드시 하나이상 포함하세요. 경로를 자동으로 보정하시겠습니까?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    if (!tbDownloadPath.Text.EndsWith("\\"))
                        tbDownloadPath.Text += "\\";
                    tbDownloadPath.Text += @"{Artists}\[{Id}] {Title}\";
                }
                else tbDownloadPath.Focus();
        }

        private void vThread_Scroll(object sender, ScrollEventArgs e)
        {
            lThread.Text = vThread.Value.ToString();
            download_queue.capacity = vThread.Value;
            HitomiSetting.Instance.GetModel().Thread = vThread.Value;
            HitomiSetting.Instance.Save();
        }

        private async void bSync_ClickAsync(object sender, EventArgs e)
        {
            pbSync.Visible = true;
            bSync.Enabled = false;
            await HitomiData.Instance.Synchronization();
            pbSync.Visible = false;
            bSync.Enabled = true;
            MetroMessageBox.Show(this, "데이터가 동기화되었습니다!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
    }
}
