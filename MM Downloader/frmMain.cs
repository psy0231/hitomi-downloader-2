using Hitomi_Copy_2;
using MM_Downloader.MM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MM_Downloader
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            //wc.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            //wc.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            //wc.Headers.Add(HttpRequestHeader.AcceptLanguage, "ko-KR,ko;q=0.9,en-US;q=0.8,en;q=0.7");
            //wc.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
            //wc.Headers.Add(HttpRequestHeader.Cookie, "__cfduid=d2a828dab05d1a3bccc46cc85db82808c1517562328; _ga=GA1.2.93771804.1517562329; PHPSESSID=ajbve7v4fbujpi1rf733plcpn5; _gid=GA1.2.233355720.1525253327");
            //wc.Headers.Add(HttpRequestHeader.Referer, "https://marumaru.in/b/mangaup/296965");
            //wc.Headers.Add(HttpRequestHeader.Upgrade, "1");
            wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36");
            var archives = MMParser.ParseManga(wc.DownloadString(tbAddress.Text));
            Task.Run(() => DownloadArchives(archives));
        }

        private void DownloadArchives(List<string> urls)
        {
            foreach (var url in urls)
            {
                Task.Run(() => DownloadArchives(url));
                Thread.Sleep(100);
            }
        }

        private void DownloadArchives(string url)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36");
            var images = MMParser.ParseArchives(wc.DownloadString(url));
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
            IncrementProgressValue();
            DeleteSpecificItem(((int)obj).ToString());
            UpdateLabel($"{pbTarget.Value}/{pbTarget.Maximum}");
        }
        private void IncrementProgressValue()
        {
            if (pbTarget.InvokeRequired)
            {
                Invoke(new Action(IncrementProgressValue));
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

        #endregion
        
        private string MakeDownloadDirectory(ExHentaiArticle article)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            string title = article.Title ?? "";
            if (title != null)
                foreach (char c in invalid) title = title.Replace(c.ToString(), "");
            return $"{AppDomain.CurrentDomain.BaseDirectory}\\{title}\\";
        }

        private string Download()
        {
            Directory.CreateDirectory(MakeDownloadDirectory(article));
        }
    }
}
