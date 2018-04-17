/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Hitomi_403
{
    public partial class ehen : Form
    {
        
        public ehen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add(HttpRequestHeader.Cookie, "ipb_member_id=1904662;ipb_pass_hash=ff8940e2cc632d601091b8836fca66f5;");
            //MessageBox.Show(wc.DownloadString(new Uri($"https://exhentai.org/g/1212168/421ef300a8/")));

            ExHentaiParser.GetPagesUri(wc.DownloadString(new Uri($"https://exhentai.org/g/1212396/71a853083e/")));
            ExHentaiParser.GetImagesUri(wc.DownloadString(new Uri($"https://exhentai.org/g/1212168/421ef300a8/")));
            ExHentaiParser.TestArticle(wc.DownloadString(new Uri($"https://exhentai.org/g/1212168/421ef300a8/")));
        }

        private void ehen_Load(object sender, EventArgs e)
        {

        }
    }
}
