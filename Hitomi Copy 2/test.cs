using Hitomi_Copy_2.GalleryInfo;
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

namespace Hitomi_Copy_2
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string a = wc.DownloadString(new Uri($"https://hitomi.la/galleries/1206005.js"));
            var c = HitomiGalleryInfo.GetImageLink(a);

        }
    }
}
