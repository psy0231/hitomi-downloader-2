﻿/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy;
using Hitomi_Copy.Data;
using Hitomi_Copy_2;
using Hitomi_Copy_2.Analysis;
using Hitomi_Copy_2.EH;
using MetroFramework;
using MM_Downloader.MM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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

            RecommendPannel.MouseWheel += RecommendPannel_MouseWheel;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Text += UpdateManager.Version;

            tbInfo.Text += "Robust Hitomi Copy Machine Version 3\r\n";
            tbInfo.Text += "Copyright (C) 2018. Hitomi Parser Developers\r\n";
            tbInfo.Text += "E-Mail: koromo.software@gmail.com\r\n";
            tbInfo.Text += "Source-code : https://github.com/dc-koromo/hitomi-downloader-2\r\n";
            tbInfo.Text += "PatchNote : https://github.com/dc-koromo/hitomi-downloader-2/blob/master/PatchNote.md";
            tbInfo.Text += "";
            tbInfo.Text += "\r\n";
        }

        public void OnTab()
        {
            InitDownloader();

            tbSearch.Enabled = true;
            bSearch.Enabled = true;
            bSync.Enabled = true;
            bDetailSetting.Enabled = true;
            bStat.Enabled = true;
            bRTidy.Enabled = true;

            tbDownloadPath.Text = HitomiSetting.Instance.GetModel().Path;
            tbExcludeTag.Text = string.Join(", ", HitomiSetting.Instance.GetModel().ExclusiveTag ?? Enumerable.Empty<string>());

            foreach (var lang in HitomiData.Instance.GetLanguageList())
                cbLanguage.Items.Add(lang);
            cbLanguage.Items.Add("N/A");
            cbLanguage.Items.Add("ALL");
            cbLanguage.Text = HitomiSetting.Instance.GetModel().Language;
            tbLang.Text = cbLanguage.Text;

            vThread.Value = HitomiSetting.Instance.GetModel().Thread;
            lThread.Text = vThread.Value.ToString();
            tgAutoZip.Checked = HitomiSetting.Instance.GetModel().Zip;

            HitomiDate.Initialize();
            LogEssential.Instance.Initialize();

#if !DEBUG
            Task.Run(() => CheckUpdate());
#endif
            Task.Run(() => UpdateStatistics());

            EmitTip();

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        public void EmitTip()
        {
            if (HitomiSetting.Instance.GetModel().Thread > Environment.ProcessorCount)
            {
                LogEssential.Instance.PushLog(() => $"[Tip] The number of threads is greater than the number of cpu cores. In this case,");
                LogEssential.Instance.PushLog(() => $"[Tip] If you download a large amount of galleris, the compression step is likely to be the last, and this can lead to a high CPU load.");
            }
        }

        #region 업데이트 확인
        private void CheckUpdate()
        {
            if (UpdateManager.UpdateRequired())
            {
                if (DialogResult.Yes == MetroMessageBox.Show(this, "새로운 버전이 출시되었습니다. 다운로드할까요? (Yes 버튼을 누르면 잠시후 프로그램을 재시작합니다.)", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    UpdateManager.UpdateProgram();
                }
            }
            else
            {
                LogEssential.Instance.PushLog(() => "This version is latest version!");
            }
        }
        #endregion

        #region 검색
        private async void bSearch_Click(object sender, EventArgs e)
        {
            LogEssential.Instance.PushLog(() => $"Search : \"{tbSearch.Text}\"");

            if (tbSearch.Text.Trim().StartsWith("http://") || tbSearch.Text.Trim().StartsWith("https://"))
            {
                ProcessUrl(tbSearch.Text.Trim());
                return;
            }

            HitomiDataQuery query = new HitomiDataQuery();
            List<string> positive_data = new List<string>();
            List<string> negative_data = new List<string>();
            List<string> request_number = new List<string>();
            int start_element = 0;
            int count_element = 0;
            bool recent = false;
            int recent_count = 0;
            int recent_start = 0;

            tbSearch.Text.Trim().Split(' ').ToList().ForEach((a) => { if (a.StartsWith("/")) start_element = Convert.ToInt32(a.Substring(1)); });
            tbSearch.Text.Trim().Split(' ').ToList().ForEach((a) => { if (a.StartsWith("?")) count_element = Convert.ToInt32(a.Substring(1)); });
            tbSearch.Text.Trim().Split(' ').ToList().ForEach((a) => { if (!a.Contains(":") && !a.StartsWith("/") && !a.StartsWith("?")) positive_data.Add(a.Trim()); });
            tbExcludeTag.Text.Trim().Split(' ').ToList().ForEach((a) => negative_data.Add(Regex.Replace(a.Trim(), ",", "")));
            query.Common = positive_data;
            query.TagExclude = negative_data;
            foreach (var elem in from elem in tbSearch.Text.Trim().Split(' ') where elem.Contains(":") where !elem.StartsWith("/") where !elem.StartsWith("?") select elem)
            {
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
                        MetroMessageBox.Show(this, $"recent 규칙 오류입니다. \"{elem}\"", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    tbSearch.Text = "recent:" + (recent_start + recent_count) + "-" + recent_count;
                }
                else
                {
                    MetroMessageBox.Show(this, $"알 수 없는 규칙입니다. \"{elem}\"", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                query_result = (await HitomiDataSearch.Search3(query));
            }
            lStatusSearch.Text = $"{query_result.Count} 개 항목이 검색됨";

            if (start_element != 0 && start_element <= query_result.Count) query_result.RemoveRange(0, start_element);
            if (count_element != 0 && count_element < query_result.Count) query_result.RemoveRange(count_element, query_result.Count - count_element);

            if (query_result.Count > HitomiSetting.Instance.GetModel().MaximumThumbnailShow)
            {
                MetroMessageBox.Show(this, "검색된 항목이 너무 많습니다. '/'또는 '?' 명령어를 이용해 보세요. '/'는 시작위치, '?'는 가져올 개수입니다.(ex: /100 ?20)", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                query_result.RemoveRange(HitomiSetting.Instance.GetModel().MaximumThumbnailShow, query_result.Count - HitomiSetting.Instance.GetModel().MaximumThumbnailShow);
            }
            else if (query_result.Count == 0)
            {
                MetroMessageBox.Show(this, "검색된 항목이 없습니다.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            pbLoad.Visible = true;
            pbLoad.Maximum += query_result.Count;
            LazyAdd(query_result).Catch();

            foreach (var request in request_number)
                RequestDownloadArticleFormId(request);

            LogEssential.Instance.PushLog(query);
            LogEssential.Instance.PushLog(() => $"Result : {query_result.Count}");
            if (HitomiSetting.Instance.GetModel().DetailedLog)
                LogEssential.Instance.PushLog(query_result);

            if (query.Artists != null || query.Groups != null)
            {
                LogEssential.Instance.PushLog(() => $"[Tip] You can get the most accurate single artist/group list only in the artist/group window.");
            }
        }

        private async Task LazyAdd(List<HitomiMetadata> metadata_result)
        {
            foreach (var v in metadata_result)
            {
                AddMetadataToPanel(v).Catch();
                await Task.Delay(50).ConfigureAwait(false);
            }
        }

        private void ProcessUrl(string url)
        {
            if (url.Contains("exhentai.org"))
            {
                DownloadEXH(url);
            }
            else if (url.Contains("marumaru.in"))
            {
                DownloadMMAsync(url);
            }
            else if (url.Contains("hitomi.la"))
            {
                RemoteDownloadArticleFromId(Regex.Match(url, "\\d+").Value);
            }
        }
        #endregion

        #region 다운로드 - 익헨
        public ExHentaiArticle article;
        public string[] pages;

        private static WebClient GetEXHWebClient()
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add(HttpRequestHeader.Cookie, "igneous=fc251d23e;ipb_member_id=1904662;ipb_pass_hash=ff8940e2cc632d601091b8836fca66f5;");
            return wc;
        }

        private void DownloadEXH(string url)
        {
            string article_source = GetEXHWebClient().DownloadString(new Uri(url));
            pages = ExHentaiParser.GetPagesUri(article_source);
            article = ExHentaiParser.GetArticleData(article_source);
            IncrementProgressBarMax(article.Length);
            Task.Run(() => download_page());
            LogEssential.Instance.PushLog(() => $"EXH {url}");
            LogEssential.Instance.PushLog(article);
        }

        private void download_page()
        {
            foreach (string page_uri in pages)
            {
                WebClient wc1 = GetEXHWebClient();
                wc1.DownloadStringCompleted += wc_page_cb;
                wc1.DownloadStringAsync(new Uri(page_uri), article);
            }
        }

        private void wc_page_cb(object sender, DownloadStringCompletedEventArgs e)
        {
            var images = ExHentaiParser.GetImagesUri(e.Result);
            foreach (string image_uri in images)
            {
                WebClient wc = GetEXHWebClient();
                wc.DownloadStringCompleted += wc_image_cb;
                wc.DownloadStringAsync(new Uri(image_uri), e.UserState);
                System.Threading.Thread.Sleep(500);
            }
            if (HitomiSetting.Instance.GetModel().DetailedLog)
            {
                LogEssential.Instance.PushLog(() => $"EXH Images");
                LogEssential.Instance.PushLog(images);
            }
        }

        private void wc_image_cb(object sender, DownloadStringCompletedEventArgs e)
        {
            ImageLinkCallback(ExHentaiParser.GetImagesAddress(e.Result), e.UserState as ExHentaiArticle);
        }

        private string MakeDownloadDirectory(ExHentaiArticle article)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string title = article.Title ?? "";
            if (title != null)
                foreach (char c in invalid) title = title.Replace(c.ToString(), "");
            return $"{AppDomain.CurrentDomain.BaseDirectory}{title}\\";
        }

        private void ImageLinkCallback(string uri, ExHentaiArticle article) => this.Post(() =>
        {
            Directory.CreateDirectory(MakeDownloadDirectory(article));
            ++count;
            lvStandBy.Items.Add(new ListViewItem(new string[]
            {
                    count.ToString(),
                    article.Title,
                    uri
            }));
            download_check.Add(article.Title);
            lock (download_queue)
                download_queue.Add(uri, MakeDownloadDirectory(article) + uri.Split('/').Last(), count);
        });
        #endregion

        #region 다운로드 - 마루마루
        public List<Tuple<string, MMArticle>> images_uri = new List<Tuple<string, MMArticle>>();

        private async void DownloadMMAsync(string url)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36");

            string html = wc.DownloadString(url);
            var archives = MMParser.ParseManga(html);
            images_uri.Clear();

            LogEssential.Instance.PushLog(() => $"Download MM {url}");
            LogEssential.Instance.PushLog(archives);
            await Task.Run(() => DownloadArchivesAsync(archives, MMParser.GetTitle(html)));
            LogEssential.Instance.PushLog(() => $"Merge Successful!");
            await Task.Run(() => DownloadImages());
        }

        private void DownloadArchivesAsync(List<string> urls, string title)
        {
            List<Task> tasks = new List<Task>();
            foreach (var url in urls)
            {
                tasks.Add(Task.Run(() => DownloadArchives(url, title)));
                Thread.Sleep(500);
            }
            Task.WaitAll(tasks.ToArray());
        }

        private void DownloadArchives(string url, string title)
        {
            WebClient wc = new WebClient();

            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36");
            //wc.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            wc.Headers.Add(HttpRequestHeader.AcceptLanguage, "ko-KR,ko;q=0.9,en-US;q=0.8,en;q=0.7");
            wc.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
            //wc.Headers.Add(HttpRequestHeader.Connection, "keep-alive");
            wc.Headers.Add(HttpRequestHeader.Cookie, "__cfduid=d46fd6709d735a04a08fd60d89582a3911525265471; _ga=GA1.2.335086797.1525265472; _gid=GA1.2.928930778.1525265472; __gads=ID=fe459c0742f63207:T=1525265474:S=ALNI_Mb08qlp3nTYBBz1WptFsP7GviAwEw; impx={%22imp_usy%22:{%22capCount%22:5%2C%22capExpired%22:1525351873}}; PHPSESSID=4bae062279cf21003588d75744ba4ed1");
            wc.Headers.Add(HttpRequestHeader.Host, "wasabisyrup.com");
            wc.Headers.Add(HttpRequestHeader.Referer, "http://203.233.24.233/tm/?a=CR&b=WIN&c=799001634617&d=10003&e=2013&f=d2FzYWJpc3lydXAuY29tL2FyY2hpdmVzLzQyODA2MQ==&g=1525401005814&h=1525401004232&y=0&z=0&x=1&w=2018-02-12&in=2013_00009301&id=20180504");
            wc.Headers.Add(HttpRequestHeader.Upgrade, "1");

            string html = wc.DownloadString(url);
            var images = MMParser.ParseArchives(html);
            MMArticle ta = new MMArticle();
            ta.Title = title;
            ta.Archive = MMParser.GetArchive(html);
            LogEssential.Instance.PushLog(() => $"Download MM Archives {url}");
            LogEssential.Instance.PushLog(ta);
            foreach (var uri in images)
            {
                IncrementProgressBarMax();
                UpdateLabel($"{pbTarget.Value}/{pbTarget.Maximum}");
                lock (images) images_uri.Add(new Tuple<string, MMArticle>(uri, ta));
            }
            if (HitomiSetting.Instance.GetModel().DetailedLog)
                LogEssential.Instance.PushLog(images);
        }

        private void DownloadImages()
        {
            foreach (var tuple in images_uri)
                Task.Run(() => DownloadImage(tuple.Item1, tuple.Item2));
        }

        private string MakeDownloadDirectory(MMArticle article)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string title = article.Title ?? "";
            string archive = article.Archive ?? "";
            if (title != null) foreach (char c in invalid) title = title.Replace(c.ToString(), "");
            if (archive != null) foreach (char c in invalid) archive = archive.Replace(c.ToString(), "");
            return $"{AppDomain.CurrentDomain.BaseDirectory}{title}\\{archive}\\";
        }

        private void DownloadImage(string uri, MMArticle article) => this.Post(() =>
        {
            Directory.CreateDirectory(MakeDownloadDirectory(article));
            ++count;
            lvStandBy.Items.Add(new ListViewItem(new string[]
            {
                    count.ToString(),
                    article.Title,
                    uri
            }));
            lock (download_queue)
                download_queue.Add(@"http://wasabisyrup.com" + uri, MakeDownloadDirectory(article) + uri.Split('/').Last(), count);
        });
        #endregion

        #region 검색창
        public int global_position = 0;
        public string global_text = "";
        public bool selected_part = true;

        private int GetCaretWidthFromTextBox(int pos)
        {
            return TextRenderer.MeasureText(tbSearch.Text.Substring(0, pos), tbSearch.Font).Width;
        }
        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                bSearch.PerformClick();
            else if (e.KeyData == Keys.Escape)
            {
                listBox1.Hide();
                tbSearch.Focus();
            }
            else
            {
                if (listBox1.Visible)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        listBox1.SelectedIndex = 0;
                        listBox1.Focus();
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        listBox1.Focus();
                    }
                }

                if (selected_part)
                {
                    selected_part = false;
                    if (e.KeyCode != Keys.Back)
                    {
                        tbSearch.SelectionStart = global_position;
                        tbSearch.SelectionLength = 0;
                    }
                }

            }
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            int position = tbSearch.SelectionStart;
            while (position > 0 && tbSearch.Text[position - 1] != ' ')
                position -= 1;

            string word = "";
            for (int i = position; i < tbSearch.Text.Length; i++)
            {
                if (tbSearch.Text[i] == ' ') break;
                word += tbSearch.Text[i];
            }

            if (word == "") { listBox1.Visible = false; return; }

            List<HitomiTagdata> match = null;
            if (word.Contains(":"))
            {
                if (word.StartsWith("artist:"))
                {
                    word = word.Substring("artist:".Length);
                    position += "artist:".Length;
                    match = HitomiData.Instance.GetArtistList(word);
                }
                else if (word.StartsWith("tag:"))
                {
                    word = word.Substring("tag:".Length);
                    position += "tag:".Length;
                    match = HitomiData.Instance.GetTagList(word);
                }
                else if (word.StartsWith("tagx:"))
                {
                    word = word.Substring("tagx:".Length);
                    position += "tagx:".Length;
                    match = HitomiData.Instance.GetTagList(word);
                }
                else if (word.StartsWith("character:"))
                {
                    word = word.Substring("character:".Length);
                    position += "character:".Length;
                    match = HitomiData.Instance.GetCharacterList(word);
                }
                else if (word.StartsWith("group:"))
                {
                    word = word.Substring("group:".Length);
                    position += "group:".Length;
                    match = HitomiData.Instance.GetGroupList(word);
                }
                else if (word.StartsWith("series:"))
                {
                    word = word.Substring("series:".Length);
                    position += "series:".Length;
                    match = HitomiData.Instance.GetSeriesList(word);
                }
            }
            else
            {
                string[] match_target = {
                    "artist:",
                    "character:",
                    "group:",
                    "recent:",
                    "series:",
                    "tag:",
                    "tagx:"
                };
                List<HitomiTagdata> data_col = (from ix in match_target where ix.StartsWith(word) select new HitomiTagdata { Tag = ix }).ToList();
                if (data_col.Count > 0)
                    match = data_col;
            }

            if (match != null && match.Count > 0)
            {
                listBox1.Visible = true;
                listBox1.Items.Clear();
                for (int i = 0; i < 30 && i < match.Count; i++)
                {
                    if (match[i].Count > 0)
                        listBox1.Items.Add(match[i].Tag + $" ({match[i].Count})");
                    else
                        listBox1.Items.Add(match[i].Tag);
                }
                listBox1.Location = new Point(tbSearch.Left + GetCaretWidthFromTextBox(position),
                    tbSearch.Top + tbSearch.Font.Height + 5);
                listBox1.MaxColoredTextLength = word.Length;
            }
            else { listBox1.Visible = false; return; }

            global_position = position;
            global_text = word;

            if (e.KeyCode == Keys.Down)
            {
                listBox1.SelectedIndex = 0;
                listBox1.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.Focus();
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                PutStringIntoTextBox(listBox1.Items[0].ToString());
            }
        }

        private void PutStringIntoTextBox(string text)
        {
            text = text.Split('(')[0].Trim();
            tbSearch.Text = tbSearch.Text.Substring(0, global_position) +
                text +
                tbSearch.Text.Substring(global_position + global_text.Length);
            listBox1.Hide();

            tbSearch.SelectionStart = global_position;
            tbSearch.SelectionLength = text.Length;
            tbSearch.Focus();

            global_position = global_position + tbSearch.SelectionLength;
            selected_part = true;
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                if (listBox1.SelectedItems.Count > 0)
                    PutStringIntoTextBox(listBox1.SelectedItem.ToString());
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Escape)
            {
                listBox1.Hide();
                tbSearch.Focus();
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                PutStringIntoTextBox(listBox1.SelectedItem.ToString());
            }
        }
        #endregion

        #region 썸네일
        private void IncrementLoadProgressBarValue()
            => this.Post(() => { pbLoad.Value += 1; });

        private async Task AddMetadataToPanel(HitomiMetadata metadata)
        {
            HitomiArticle article = HitomiCommon.MetadataToArticle(metadata);
            string galleryUri = $"https://hitomi.la/galleries/{article.Magic}.html";
            string html = await Util.PlainWebClient().DownloadStringTaskAsync(galleryUri).ConfigureAwait(false);
            string thumbUri = HitomiParser.ParseGallery(html).Thumbnail;
            article.Thumbnail = thumbUri;
            await AddArticleToPanel(article);
        }

        private async Task AddArticleToPanel(HitomiArticle article)
        {
            if (HitomiSetting.Instance.GetModel().DetailedLog)
                LogEssential.Instance.PushLog(article);

            // https://stackoverflow.com/q/40682131
            // We need the following or Invoke() for creation
            // of winform control in background thread.
            WindowsFormsSynchronizationContext.AutoInstall = false;

            bool isDetailed = HitomiSetting.Instance.GetModel().DetailedSearchResult;
            IPicElement pe = isDetailed ? new PicDetailElement(this) as IPicElement : new PicElement(this);
            pe.Article = article;
            pe.Label = article.Title;
            pe.Font = Font;

            var uri = new Uri(HitomiDef.HitomiThumbnail + article.Thumbnail);
            var ms = new MemoryStream();
            WebClient wc = Util.PlainWebClient();

            LogEssential.Instance.PushLog(() => $"AddArticleToPanel {HitomiDef.HitomiThumbnail + article.Thumbnail}");
            if (HitomiSetting.Instance.GetModel().DetailedLog)
                LogEssential.Instance.PushLog(article);

            Stream stream = await wc.OpenReadTaskAsync(uri).ConfigureAwait(false);
            if (pe is PicElement rpe)
                rpe.SetImage(stream, 150, 200);
            else if (pe is PicDetailElement pde)
                pde.SetImage(Image.FromStream(stream), 150, 200);

            bool isDup = ImagePanel.Controls.OfType<PicElement>().Any(
                thumb => pe.Article.Title == thumb.Article.Title);
            if (isDup)
            {
                pe.Article.Title += " " + pe.Article.Magic;
                pe.Label += " " + pe.Article.Magic;
                pe.Overlap = true;
            }
            AddPanel(pe);
            Application.DoEvents();
        }
        private void AddPanel(IPicElement pe) => this.Post(() =>
        {
            ImagePanel.Controls.Add(pe as Control);
            IncrementLoadProgressBarValue();
            SortThumbnail();
        });
        private void SortThumbnail()
        {
            List<Control> controls = new List<Control>();
            for (int i = 0; i < ImagePanel.Controls.Count; i++)
                controls.Add(ImagePanel.Controls[i]);
            controls.Sort((a, b) => Convert.ToUInt32((b as IPicElement).Article.Magic).CompareTo(Convert.ToUInt32((a as IPicElement).Article.Magic)));
            for (int i = 0; i < controls.Count; i++)
                ImagePanel.Controls.SetChildIndex(controls[i], i);
        }
        #endregion

        #region 다운로드 관련
        public HitomiQueue download_queue;
        public List<string> download_check = new List<string>();
        public List<IPicElement> downloaded_check = new List<IPicElement>();

        private void InitDownloader()
        {
            download_queue = new HitomiQueue(HitomiQueueCallback,
                HitomiQueueDownloadSizeCallback, HitomiQueueDownloadStatusCallback, HitomiRetryCallback)
            {
                timeout_infinite = HitomiSetting.Instance.GetModel().WaitInfinite,
                timeout_ms = HitomiSetting.Instance.GetModel().WaitTimeout
            };
        }

        private void HitomiRetryCallback(string uri) => this.Post(() =>
        {
            lRetry.Text = uri + "항목 다운로드를 재시작합니다.";
            lRetry.Visible = true;
            LogEssential.Instance.PushLog(() => $"Retry downloading... {uri}");
        });
        private void HitomiQueueCallback(string uri, string filename, object obj)
        {
            IncrementProgressBarValue();
            DeleteSpecificItem(((int)obj).ToString());
            UpdateLabel($"{pbTarget.Value}/{pbTarget.Maximum}");
            LogEssential.Instance.PushLog(() => $"Image Download Complete! {uri} {filename}");
        }
        private void IncrementProgressBarMax() => this.Post(() =>
        {
            pbTarget.Maximum += 1;
        });
        private void IncrementProgressBarMax(int value) => this.Post(() =>
        {
            pbTarget.Maximum += value;
        });
        private void IncrementProgressBarValue() => this.Post(() =>
        {
            if (pbTarget.Value != pbTarget.Maximum) pbTarget.Value += 1;
        });
        private void DeleteSpecificItem(string i) => this.Post(() => {
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
                    List<IPicElement> check = downloaded_check.ToList();
                    if (!copy.Contains(title))
                    {
                        foreach (var elem in check)
                            if (elem.Label == title)
                            {
                                elem.Downloading = false;
                                if (HitomiSetting.Instance.GetModel().Zip)
                                    Task.Run(() => ZipArticle(elem.Article));
                                return;
                            }
                    }
                }
        });
        private void UpdateLabel(string v) => this.Post(() =>
        {
            lStatus.Text = v;
        });
        private void ZipArticle(HitomiArticle article)
        {
            LogEssential.Instance.PushLog(() => $"{article.Magic} Zipping article... ");
            string zip_path = MakeDownloadDirectory(article);
            zip_path = zip_path.Remove(zip_path.Length - 1);
            if (File.Exists($"{zip_path}.zip"))
                File.Delete($"{zip_path}.zip");
            ZipFile.CreateFromDirectory(zip_path, $"{zip_path}.zip");
            Directory.Delete(zip_path, true);
            LogEssential.Instance.PushLog(() => $"{article.Magic} Zipping Completed! ");
        }

        long download_size = 0;
        long status_size = 0;
        object size_lock = new object();
        object status_lock = new object();
        private void HitomiQueueDownloadSizeCallback(string uri, long size)
        {
            lock (size_lock) download_size += size;
            UpdateDownloadSize();
        }
        private void HitomiQueueDownloadStatusCallback(string uri, int size)
        {
            lock (size_lock) status_size += size;
            UpdateDownloadStatus();
        }
        private void UpdateDownloadSize()
        {
            if (lDownloadSize.InvokeRequired)
            {
                Invoke(new Action(UpdateDownloadSize));
                return;
            }
            lock (size_lock)
            {
                lDownloadSize.Text = ((double)download_size / 1000 / 1000).ToString("#,#.#") + " MB";
            }
        }
        private void UpdateDownloadStatus()
        {
            if (lDownloadStatusSize.InvokeRequired)
            {
                Invoke(new Action(UpdateDownloadStatus));
                return;
            }
            lock (status_lock)
            {
                lDownloadStatusSize.Text = ((double)status_size / 1000 / 1000).ToString("#,#.#") + " MB";
            }
        }

        private void AddArticle(IPicElement pe)
        {
            HitomiLog.Instance.AddArticle(pe.Article);
            HitomiLog.Instance.Save();
            downloaded_check.Add(pe);
            HitomiCore.DownloadAndSetImageLink(pe, ImageLinkCallback);
        }
        int count = 0;
        private void ImageLinkCallback(IPicElement pe) => this.Post(() =>
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
            LogEssential.Instance.PushLog(() => $"Add ImageLink");
            LogEssential.Instance.PushLog(() => $"Folder Address : {MakeDownloadDirectory(pe.Article)}");
            if (HitomiSetting.Instance.GetModel().DetailedLog)
            {
                LogEssential.Instance.PushLog(pe.Article.ImagesLink);
                LogEssential.Instance.PushLog(() => $"This list is added in download_queue!");
            }
            if (HitomiSetting.Instance.GetModel().SaveJson == true)
            {
                HitomiJson hitomi_json = new HitomiJson(MakeDownloadDirectory(pe.Article));
                hitomi_json.SetModelFromArticle(pe.Article);
                hitomi_json.Save();
            }
        });

        private string MakePathFromAdditionalPath(string target, string additional_path)
        {
            StringBuilder front = new StringBuilder();
            StringBuilder back = new StringBuilder();
            int index = 0;

            for (; target[index] != '{'; index++)
                front.Append(target[index]);

            for (; index < target.Length; index++)
                back.Append(target[index]);

            return front.ToString() + additional_path + '\\' + back.ToString();
        }

        private string MakeDownloadDirectory(HitomiArticle article)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string title = article.Title ?? "";
            string artists = "";
            string type = article.Types ?? "";
            string series = "";
            if (article.Artists != null)
            {
                if (HitomiSetting.Instance.GetModel().ReplaceArtistsWithTitle == false)
                    artists = article.Artists[0];
                else
                {
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    artists = textInfo.ToTitleCase(article.Artists[0]);
                }
            }
            if (article.Series != null) series = article.Series[0];
            if (title != null)
                foreach (char c in invalid) title = title.Replace(c.ToString(), "");
            if (artists != null)
                foreach (char c in invalid) artists = artists.Replace(c.ToString(), "");
            if (series != null)
                foreach (char c in invalid) series = series.Replace(c.ToString(), "");

            string path = tbDownloadPath.Text;
            if (article.ManualPathOrdering)
                path = MakePathFromAdditionalPath(path, article.ManualAdditionalPath);
            path = Regex.Replace(path, "{Title}", title, RegexOptions.IgnoreCase);
            path = Regex.Replace(path, "{Artists}", artists, RegexOptions.IgnoreCase);
            path = Regex.Replace(path, "{Id}", article.Magic, RegexOptions.IgnoreCase);
            path = Regex.Replace(path, "{Type}", type, RegexOptions.IgnoreCase);
            path = Regex.Replace(path, "{Date}", DateTime.Now.ToString(), RegexOptions.IgnoreCase);
            path = Regex.Replace(path, "{Series}", series, RegexOptions.IgnoreCase);
            return path;
        }

        public void RemoteDownloadArticle(PicElement pe)
        {
            AddArticle(pe);
            MainTab.SelectedIndex = 1;
        }
        public void RemoteDownloadArticleFromId(string id, bool add_manual_path = false, string additional_path = "")
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
                MetroMessageBox.Show(this, $"{id} 항목이 데이터베이스에 없습니다. 데이터베이스를 동기화해주세요.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PicElement fake_pe = new PicElement(this);
            HitomiArticle article = new HitomiArticle
            {
                Magic = metadata.ID.ToString(),
                Title = metadata.Name,
                ManualPathOrdering = add_manual_path,
                ManualAdditionalPath = additional_path
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
            LogEssential.Instance.PushLog(() => "Abort");
            LogEssential.Instance.PushLog(uris);
        }

        public long latest_status_size = 0;
        private void speed_timer_Tick(object sender, EventArgs e)
        {
            if (status_size == latest_status_size)
                lDownloadSpeed.Text = "0 KB/S";
            else
                lDownloadSpeed.Text = ((double)(status_size - latest_status_size) / 1000).ToString("#,#.#") + " KB/S";
            latest_status_size = status_size;
        }
        #endregion

        #region 통계
        public int latest_load_count = 0;

        private void UpdateStatistics()
        {
            HitomiAnalysis.Instance.Update();
            Task.Run(() => MoreLoadRecommend());
        }
        public void AddRecommendArtist(string artist)
        {
            AddToPannel(new RecommendControl(artist));
        }
        private void MoreLoadRecommend()
        {
            for (int i = 0; i < HitomiSetting.Instance.GetModel().RecommendPerScroll && latest_load_count < HitomiAnalysis.Instance.Rank.Count; i++, latest_load_count++)
            {
                if (HitomiSetting.Instance.GetModel().UninterestednessArtists != null &&
                    HitomiSetting.Instance.GetModel().UninterestednessArtists.Contains(HitomiAnalysis.Instance.Rank[latest_load_count].Item1))
                {
                    i--;
                    continue;
                }
                AddToPannel(new RecommendControl(latest_load_count));
            }
        }
        private void AddToPannel(RecommendControl control) => this.Post(() =>
        {
            RecommendPannel.SuspendLayout();
            RecommendPannel.Controls.Add(control);
            RecommendPannel.ResumeLayout();
        });
        private void tgFilterArtists_CheckedChanged(object sender, EventArgs e)
        {
            HitomiAnalysis.Instance.FilterArtists = tgFilterArtists.Checked;
            UpdateNewStatistics();
        }
        public void UpdateNewStatistics()
        {
            for (int i = RecommendPannel.Controls.Count - 1; i >= 0; i--)
                if (RecommendPannel.Controls[i] != null)
                    RecommendPannel.Controls[i].Dispose();
            RecommendPannel.Controls.Clear();
            latest_load_count = 0;
            UpdateStatistics();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }
        #endregion

        #region 기타 잡것
        private void lMemoryUsage_Click(object sender, EventArgs e)
        {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            HitomiSetting.Instance.GetModel().Language = cbLanguage.Text;
            HitomiSetting.Instance.Save();
            tbLang.Text = cbLanguage.Text;
        }

        private void bDownload_Click(object sender, EventArgs e)
        {
            cbLanguage.Enabled = false;
            tbDownloadPath.Enabled = false;
            tbExcludeTag.Enabled = false;
            vThread.Enabled = false;

            if (!tbDownloadPath.Text.EndsWith("\\"))
                tbDownloadPath.Text += "\\";

            Task.Run(LazyAddArticle);
        }

        private async Task LazyAddArticle()
        {
            try
            {
                List<IPicElement> pes = ImagePanel
                    .Controls.OfType<IPicElement>()
                    .Where(x => x.Selected).ToList();
                foreach (IPicElement pe in pes)
                {
                    if ((pe as Control)?.IsDisposed != false) continue;
                    pe.Downloaded = pe.Downloading = true;
                    pe.Invalidate();
                    AddArticle(pe);
                    await Task.Delay(100);
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
            if (!(tbDownloadPath.Text.ToLower().Contains("{id}") || tbDownloadPath.Text.ToLower().Contains("{title}")))
                if (MetroMessageBox.Show(this,
                    "다운로드 경로가 잘못 지정되었습니다. " +
                    "{Id} 또는 {Title}를 반드시 하나이상 포함하세요. 경로를 자동으로 보정하시겠습니까?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    if (!tbDownloadPath.Text.EndsWith("\\"))
                        tbDownloadPath.Text += "\\";
                    tbDownloadPath.Text += @"{Artists}\[{Id}] {Title}\";
                }
                else tbDownloadPath.Focus();
            else
            {
                HitomiSetting.Instance.GetModel().Path = tbDownloadPath.Text;
                HitomiSetting.Instance.Save();
            }
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

        private void RecommendPannel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (RecommendPannel.VerticalScroll.Value == RecommendPannel.VerticalScroll.Maximum - RecommendPannel.VerticalScroll.LargeChange + 1)
                Task.Run(() => MoreLoadRecommend());
        }

        private void 이미지로저장SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int width = RecommendPannel.Size.Width;
            int height = RecommendPannel.Size.Height;
            Bitmap bm = new Bitmap(width, height);

            RecommendPannel.DrawToBitmap(bm, new Rectangle(0, 0, width, height));
            bm.Save($"{AppDomain.CurrentDomain.BaseDirectory}\\Image.bmp", ImageFormat.Bmp);
        }

        private void bTidy_Click(object sender, EventArgs e)
        {
            ImagePanel.SuspendLayout();
            ImagePanel
                .Controls.OfType<IPicElement>()
                .Where(x => !x.Selected && !x.Downloading)
                .ToList()
                .ForEach(x => (x as Control)?.Dispose());
            ImagePanel.ResumeLayout();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void bRTidy_Click(object sender, EventArgs e)
        {
            RecommendPannel.SuspendLayout();
            int rc = RecommendPannel.Controls.Count;
            int sp = HitomiSetting.Instance.GetModel().RecommendPerScroll;
            for (int i = 0; i < rc - sp; i++)
            {
                RecommendPannel.Controls[0].Dispose();
            }
            RecommendPannel.ResumeLayout();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        }

        private void bChooseAll_Click(object sender, EventArgs e)
        {
            foreach (var pe in ImagePanel.Controls.OfType<IPicElement>())
                pe.Selected = true;
        }

        private void bCancleAll_Click(object sender, EventArgs e)
        {
            foreach (var pe in ImagePanel.Controls.OfType<IPicElement>())
                pe.Selected = false;
        }

        private void tgAutoZip_CheckedChanged(object sender, EventArgs e)
        {
            HitomiSetting.Instance.GetModel().Zip = tgAutoZip.Checked;
            HitomiSetting.Instance.Save();
        }

        private void bDetailSetting_Click(object sender, EventArgs e)
        {
            (new frmSetting()).Show();
        }

        private void bStat_Click(object sender, EventArgs e)
        {
            (new frmStatistics()).Show();
        }

        private void 제목비슷한작품정리TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> titles = new List<string>();
            ImagePanel.SuspendLayout();
            for (int i = 0; i < ImagePanel.Controls.Count; i++)
            {
                string ttitle = (ImagePanel.Controls[i] as IPicElement).Label.Split('|')[0];
                if ((ImagePanel.Controls[i] as IPicElement).Overlap ||
                    (titles.Count > 0 && !titles.TrueForAll((title) => StringAlgorithms.get_diff(ttitle, title) > HitomiSetting.Instance.GetModel().TextMatchingAccuracy)))
                {
                    (ImagePanel.Controls[i] as IPicElement).Selected = false;
                    continue;
                }

                titles.Add(ttitle);
            }
            ImagePanel.ResumeLayout();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            HitomiSetting.Instance.GetModel().ExclusiveTag = tbExcludeTag.Text.Split(',').Select(x => x.Trim()).ToArray();
            HitomiSetting.Instance.Save();
            Process.GetCurrentProcess().Kill();
        }
        #endregion

    }
}
