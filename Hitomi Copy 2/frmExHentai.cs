/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy_2.EH;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy_2
{
    public partial class frmExHentai : Form
    {
        public frmExHentai()
        {
            InitializeComponent();
        }

        ExHentaiArticle article;

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

        private void bDownload_Click(object sender, EventArgs e)
        {

        }
    }
}
