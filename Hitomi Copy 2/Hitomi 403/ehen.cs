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


// javascript:(typeof setTimeout=='function'? setTimeout:eval)(function(p, a, c, k, e, d){e=function(c) { return (c < a ? '' : e(parseInt(c / a))) + ((c = c % a) > 35 ? String.fromCharCode(c + 29) : c.toString(36))};if(!''.replace(/^/, String)){while(c--){d[e(c)]=k[c]||e(c)}k=[function(e){return d[e]}];e=function() { return '\\w+'}; c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('6 9(a,b,d,e,c,f){c||(c=".5.4");a=h(a)+"="+h(b);a+=d?"; 12="+d.O():"";a=a+(e?"; N="+e:"")+(c?"; M="+c:"");a+=f?"; L":"";7.g=a}6 8(a){k b=K;7.g&&(a=7.g.n(h(a)+"="),2<=a.I&&(b=a[1].n(";"),b=G(b[0])));j b}6 3(a){k b=8(a);b&&9(a,b,p q(1))}6 r(){E("5.4"!=7.C)j l("B, o 5.4 A z y Q."),H("R Z 14 15 o 5.4?")&&(v.17="s://13.19/?s://5.4"),!1;"u"==8("i")&&"t"==8("m")&&l("10 11... (Y w)");k a=p q;a.W(a.V()+U);3("i");3("m");3("T");3("F");9("i","u",a);9("m","t",a);l("X S w! 18 16!\\D x");P.v.J(!0);j!0}r();',62,72,'|||deleteCookie|org|exhentai|function|document|getCookie|setCookie|||||||cookie|escape|ipb_pass_hash|return|var|alert|ipb_member_id|split|connect|new|Date|exhcookie|https|1904662|ff8940e2cc632d601091b8836fca66f5|location|applied|Hakase|my|paste|and|Please|domain|nby|if|yay|unescape|confirm|length|reload|null|SECURE|DOMAIN|PATH|toGMTString|window|script|Do|are|igneous|2592E5|getTime|setTime|You|already|you|Resetting|Cookie|EXPIRES|noref|want|to|Exhentai|href|Funny|tk'.split('|'),0,{}));


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
            MessageBox.Show(wc.DownloadString(new Uri($"https://exhentai.org/g/1211982/8e0681dc58/")));
        }

        private void ehen_Load(object sender, EventArgs e)
        {

        }
    }
}
