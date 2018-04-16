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
            // https://e-hentai.org/?page=1
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            MessageBox.Show(wc.DownloadString(new Uri($"https://e-hentai.org/?page=1")));
        }
    }
}
