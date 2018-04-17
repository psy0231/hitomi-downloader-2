/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy_2;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_403
{
    public partial class ehen : Form
    {
        
        public ehen()
        {
            InitializeComponent();
        }

        private void ehen_Load(object sender, EventArgs e)
        {
            queue = new HitomiQueue(HitomiQueueCallback, HitomiQueueDownloadSizeCallback, HitomiQueueDownloadStatusCallback);
        }

        private WebClient GetWebClient()
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add(HttpRequestHeader.Cookie, "ipb_member_id=1904662;ipb_pass_hash=ff8940e2cc632d601091b8836fca66f5;");
            return wc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string article_source = GetWebClient().DownloadString(new Uri(textBox1.Text));
            var pages = ExHentaiParser.GetPagesUri(article_source);
            foreach (string page_uri in pages)
            {
                WebClient wc1 = GetWebClient();
                wc1.DownloadStringCompleted += wc_page_cb;
                wc1.DownloadStringAsync(new Uri(page_uri));
            }
        }

        private void wc_page_cb(object sender, DownloadStringCompletedEventArgs e)
        {
            var images = ExHentaiParser.GetImagesUri(e.Result);
            foreach (string image_uri in images)
            {
                WebClient wc = GetWebClient();
                wc.DownloadStringCompleted += wc_image_cb;
                wc.DownloadStringAsync(new Uri(image_uri));
            }
        }

        private void wc_image_cb(object sender, DownloadStringCompletedEventArgs e)
        {
            if (richTextBox1.InvokeRequired)
            {
                Invoke(new Action(() => wc_image_cb(sender, e)));
                return;
            }
            richTextBox1.AppendText(ExHentaiParser.GetImagesAddress(e.Result) + "\n");
        }
        
        private void HitomiQueueDownloadSizeCallback(string uri, long size)
        {
        }
        private void HitomiQueueDownloadStatusCallback(string uri, int size)
        {
        }
        private void HitomiQueueCallback(string uri, string filename, object obj)
        {
            if (richTextBox2.InvokeRequired)
            {
                Invoke(new Action(() => HitomiQueueCallback(uri, filename, obj)));
                return;
            }
            richTextBox2.AppendText(uri + "\n");
        }

        HitomiQueue queue;

        private void button2_Click(object sender, EventArgs e)
        {

            Directory.CreateDirectory("C:\\Hitomi\\tmp\\");
            foreach (var ix in richTextBox1.Lines)
                if (!string.IsNullOrEmpty(ix))
                    queue.Add(ix, $"C:\\Hitomi\\tmp\\{ix.Split('/').Last()}", null);
        }
    }
}
