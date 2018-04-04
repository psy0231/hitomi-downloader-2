/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy;
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

namespace Hitomi_Copy_2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            (new frmSplash()).Show();

            InitDownloader();
            InitStatistics();
        }

        public void OnTab()
        {
            MainTab.Enabled = true;
            UpdateStatistics();
        }
        
        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                bSearch.PerformClick();
        }

        private void bSearch_Click(object sender, System.EventArgs e)
        {
            HitomiDataQuery query = new HitomiDataQuery();
            List<string> positive_data = new List<string>();
            List<string> negative_data = new List<string>();

            tbSearch.Text.Split(' ').ToList().ForEach((a) => positive_data.Add(a.Trim()));
            tbExcludeTag.Text.Split(' ').ToList().ForEach((a) => negative_data.Add(a.Trim()));
            query.TagInclude = positive_data;
            query.Series = positive_data;
            query.Groups = positive_data;
            query.Characters = positive_data;
            query.Artists = positive_data;
            query.Title = positive_data;
            query.TagExclude = negative_data;

            var query_result = HitomiDataSearch.Search2(query);
            lStatusSearch.Text = $"{query_result.Count}개 항목이 검색됨";
            if (query_result.Count > 100)
            {
                MessageBox.Show("검색된 항목이 너무 많아 표시할 수 없습니다. 고급검색 기능을 이용해주세요.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (query_result.Count == 0)
            {
                MessageBox.Show("검색된 항목이 없습니다.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            query_result.ForEach((a) => {Task.Run(() => AddMetadataToPanel(a));});
        }

        #region 썸네일 관련
        private async void AddMetadataToPanel(HitomiMetadata metadata)
        {
            HitomiArticle article = HitomiCommon.MetadataToArticle(metadata);
            await Task.Run(() => article.Thumbnail = GetThumbnailAddress(article.Magic));
            AddArticleToPanel(article);
        }

        private string GetThumbnailAddress(string id)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            return HitomiParser.ParseGallery(wc.DownloadString(
                new Uri($"https://hitomi.la/galleries/{id}.html"))).Thumbnail;
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
        }
        #endregion

        #region 다운로드 관련
        HitomiQueue download_queue;
        List<string> download_check = new List<string>();
        List<PicElement> downloaded_check = new List<PicElement>();

        private void InitDownloader()
        {
            download_queue = new HitomiQueue(HitomiQueueCallback, 
                HitomiQueueDownloadSizeCallback, HitomiQueueDownloadStatusCallback);
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
                HitomiJson hitomi_json = new HitomiJson(MakeDownloadDirectory(pe.Article));
                hitomi_json.SetModelFromArticle(pe.Article);
                hitomi_json.Save();
            }
        }
        
        private string MakeDownloadDirectory(HitomiArticle article)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string title = article.Title;
            string artists = article.Artists[0];
            if (title != null)
                foreach (char c in invalid) title = title.Replace(c.ToString(), "");
            if (artists != null)
                foreach (char c in invalid) artists = artists.Replace(c.ToString(), "");
            return Regex.Replace(Regex.Replace(Regex.Replace(tbDownloadPath.Text, "{Title}", title), "{Artists}", artists), "{Id}", article.Magic);
        }

        public void RemoteDownloadArticle(PicElement pe)
        {
            AddArticle(pe);
            MainTab.SelectedIndex = 2;
        }
        public void RemoteDownloadArticleFromId(string id)
        {
            MainTab.SelectedIndex = 2;
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
            HitomiArticle article = new HitomiArticle();
            article.Magic = metadata.ID.ToString();
            article.Title = metadata.Name;
            if (metadata.Artists != null)
                article.Artists = metadata.Artists;
            if (metadata.Tags != null)
                article.Tags = metadata.Tags;
            fake_pe.Article = article;
            fake_pe.Label = metadata.Name;
            RemoteDownloadArticle(fake_pe);
        }
        #endregion

        #region 통계 관련
        List<KeyValuePair<string, int>> tag_rank_list;
        List<Tuple<string, KeyValuePair<string, int>[]>> artist_tag_rank;

        public object HitomiLegalize { get; private set; }

        private void InitStatistics()
        {
            ColumnSorter.InitListView(lvMyTagRank);
            ColumnSorter.InitListView(lvRecommendArtists);
        }
        private void UpdateStatistics()
        {
            Dictionary<string, int> my_tag_rank = new Dictionary<string, int>();
            foreach (var v in HitomiLog.Instance.GetEnumerator())
            {
                if (v.Tags == null) continue;
                foreach (var tag in v.Tags)
                {
                    string legalize = HitomiCommon.LegalizeTag(tag);
                    if (my_tag_rank.ContainsKey(legalize))
                        my_tag_rank[legalize] += 1;
                    else
                        my_tag_rank.Add(legalize, 1);
                }
            }
            tag_rank_list = my_tag_rank.ToList();
            tag_rank_list.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            List<ListViewItem> lvil = new List<ListViewItem>();
            foreach (var v in tag_rank_list)
            {
                lvil.Add(new ListViewItem(new string[]
                {
                    v.Key,
                    v.Value.ToString()
                }));
            }
            lvMyTagRank.Items.Clear();
            lvMyTagRank.Items.AddRange(lvil.ToArray());
            
            if (tag_rank_list.Count > 2)
            Task.Run(() => ArtistsRecommend());
        }

        public void ArtistsRecommend()
        {
            var hitomi_data = HitomiData.Instance.metadata_collection;
            Dictionary<string, Dictionary<string, int>> artists_tag_count = new Dictionary<string, Dictionary<string, int>>();
            foreach (var metadata in hitomi_data)
            {
                if (metadata.Language != "korean") continue;
                if (metadata.Artists == null || metadata.Tags == null || metadata.Artists.Length == 0) continue;
                if (!artists_tag_count.ContainsKey(metadata.Artists[0]))
                    artists_tag_count.Add(metadata.Artists[0], new Dictionary<string, int>());
                foreach (var tag in metadata.Tags)
                {
                    if (!artists_tag_count[metadata.Artists[0]].ContainsKey(tag))
                        artists_tag_count[metadata.Artists[0]].Add(tag, 1);
                    else
                        artists_tag_count[metadata.Artists[0]][tag] += 1;
                }
            }

            var myList = artists_tag_count.ToList();
            artist_tag_rank = new List<Tuple<string, KeyValuePair<string, int>[]>>();
            for (int i = 0; i < myList.Count; i++)
            {
                artist_tag_rank.Add(new Tuple<string, KeyValuePair<string, int>[]>(myList[i].Key, myList[i].Value.OrderBy(key => key.Value).ToArray()));
            }

            List<Tuple<string, int, string>> result = new List<Tuple<string, int, string>>();
            for (int i = 0; i < artist_tag_rank.Count; i++)
            {
                if (artist_tag_rank[i].Item2.Last().Key != tag_rank_list[0].Key && cbRecommendArtistType.Checked)
                    continue;
                List<Tuple<string, int>> tag_score = new List<Tuple<string, int>>();
                int score = artist_tag_rank[i].Item2.Last().Value * tag_rank_list[0].Value;
                tag_score.Add(new Tuple<string, int>(tag_rank_list[0].Key, artist_tag_rank[i].Item2.Last().Value * tag_rank_list[0].Value));
                for (int j = artist_tag_rank[i].Item2.Count() - 1; j >= 0; j--)
                {
                    for (int k = 1; k < tag_rank_list.Count; k++)
                    {
                        if (artist_tag_rank[i].Item2[j].Key == tag_rank_list[k].Key)
                        {
                            score += artist_tag_rank[i].Item2[j].Value * tag_rank_list[k].Value;
                            tag_score.Add(new Tuple<string, int>(tag_rank_list[k].Key, artist_tag_rank[i].Item2[j].Value * tag_rank_list[k].Value));
                        }
                    }
                }
                var tag_score_list = tag_score.ToList();
                tag_score_list.Sort((pair1, pair2) => pair2.Item2.CompareTo(pair1.Item2));
                StringBuilder builder = new StringBuilder();
                for (int j = 0; j < tag_score_list.Count; j++)
                    builder.Append($"{tag_score_list[j].Item1}({tag_score_list[j].Item2}),");
                result.Add(new Tuple<string, int, string>(artist_tag_rank[i].Item1, score, artist_tag_rank[i].Item2.Last().Key));
            }

            result.Sort((pair1, pair2) => pair2.Item2.CompareTo(pair1.Item2));
            
            List<ListViewItem> lvil = new List<ListViewItem>();
            for (int i = 0; i < result.Count && i < 500; i++)
            {
                lvil.Add(new ListViewItem(new string[]
                {
                    (i+1).ToString(),
                    result[i].Item1,
                    result[i].Item2.ToString(),
                    result[i].Item3
                }));
            }
            AddToRecommendArtists(lvil.ToArray());
        }

        private void AddToRecommendArtists(ListViewItem[] items)
        {
            if (lvRecommendArtists.InvokeRequired)
            {
                Invoke(new Action<ListViewItem[]>(AddToRecommendArtists), new object[] { items });
                return;
            }
            lvRecommendArtists.Items.Clear();
            lvRecommendArtists.Items.AddRange(items);
        }
        private void lvRecommendArtists_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvRecommendArtists.SelectedItems.Count > 0)
            {
                (new frmArtistInfo(this, lvRecommendArtists.SelectedItems[0].SubItems[1].Text)).Show();
            }
        }
        private void lvMyTagRank_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvMyTagRank.SelectedItems.Count > 0)
            {
                (new frmTagInfo(this, lvMyTagRank.SelectedItems[0].SubItems[0].Text)).Show();
            }
        }
        #endregion

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

        private void bDownload_Click(object sender, EventArgs e)
        {
            tbDownloadPath.Enabled = false;
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
                UpdateStatistics();
            } catch { }
        }

        private void cbRecommendArtistType_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStatistics();
        }
    }
}
