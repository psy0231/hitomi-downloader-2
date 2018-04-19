using Pixiv_Downloader.Pixiv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixiv_Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string html =  wc.DownloadString(new Uri("https://www.pixiv.net/member.php?id=3189901"));

            PixivParser.GetImagesAddress(html);
        }
    }
}
