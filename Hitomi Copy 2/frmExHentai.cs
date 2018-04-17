/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy_2.EH;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy_2
{
    public partial class frmExHentai : Form
    {
        public frmExHentai()
        {
            InitializeComponent();
            InitDownloader();
        }

        ExHentaiArticle article;
        string[] pages;

        private static WebClient GetWebClient()
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add(HttpRequestHeader.Cookie, "igneous=fc251d23e;ipb_member_id=1904662;ipb_pass_hash=ff8940e2cc632d601091b8836fca66f5;");
            return wc;
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            string article_source = GetWebClient().DownloadString(new Uri(tbAddress.Text));
            pages = ExHentaiParser.GetPagesUri(article_source);
            article = ExHentaiParser.GetArticleData(article_source);
            lTitle.Text = article.Title;
            lArtist.Text = string.Join(", ", article.artist);
            tbTags.Text += "male: " + string.Join(", ", article.male) + "\r\n";
            tbTags.Text += "female: " + string.Join(", ", article.female) + "\r\n";
            tbTags.Text += "misc: " + string.Join(", ", article.misc) + "\r\n";
            Task.Run(() => download_image(article.Thumbnail));
        }

        private void download_image(string uri)
        {
            string localFilename = Path.GetTempFileName();
            GetWebClient().DownloadFile(uri, localFilename);
            load_image(localFilename);
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
            }
            catch { }
        }


        #region 다운로드

        HitomiQueue download_queue;

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

        private void bDownload_Click(object sender, EventArgs e)
        {
            foreach (string page_uri in pages)
            {
                WebClient wc1 = GetWebClient();
                wc1.DownloadStringCompleted += wc_page_cb;
                wc1.DownloadStringAsync(new Uri(page_uri), article);
            }
        }

        private void wc_page_cb(object sender, DownloadStringCompletedEventArgs e)
        {
            var images = ExHentaiParser.GetImagesUri(e.Result);
            foreach (string image_uri in images)
            {
                WebClient wc = GetWebClient();
                wc.DownloadStringCompleted += wc_image_cb;
                wc.DownloadStringAsync(new Uri(image_uri), e.UserState);
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
            return $"C:\\Hitomi\\{title}\\";
        }

        int count = 0;
        private void ImageLinkCallback(string uri, ExHentaiArticle article)
        {
            lock (lvStandBy)
            {
                Directory.CreateDirectory(MakeDownloadDirectory(article));
                ++count;
                lvStandBy.Items.Add(new ListViewItem(new string[]
                {
                        count.ToString(),
                        article.Title,
                        uri
                }));
                download_queue.Add(uri, MakeDownloadDirectory(article) + uri.Split('/').Last(), count);
                IncrementProgressBarMax();
            }
        }

        #endregion
    }
}
