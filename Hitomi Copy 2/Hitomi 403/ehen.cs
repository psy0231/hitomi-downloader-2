using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using mshtml;
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
            MessageBox.Show(wc.DownloadString(new Uri($"https://exhentai.org/g/1211982/8e0681dc58/")));
            
        }

        private void ehen_Load(object sender, EventArgs e)
        {

        }
    }
}
