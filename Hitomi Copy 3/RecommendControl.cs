/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy;
using Hitomi_Copy.Data;
using Hitomi_Copy_2;
using Hitomi_Copy_2.Analysis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy_3
{
    public partial class RecommendControl : UserControl
    {
        InfoWrapper[] info = new InfoWrapper[5];

        public RecommendControl(int index)
        {
            InitializeComponent();
            
            tbArtist.Text = HitomiAnalysis.Instance.Rank[index].Item1;
            lScore.Text = HitomiAnalysis.Instance.Rank[index].Item2.ToString().Remove(8) + " 점";
            tbScoreDetail.Text = HitomiAnalysis.Instance.Rank[index].Item3;
        }

        private async void RecommendControl_LoadAsync(object sender, System.EventArgs e)
        {
            await LoadThumbnailAsync();
        }

        private async Task LoadThumbnailAsync()
        {
            List<string> magics = new List<string>();

            for (int i = 0, j = 0; i < 5 && j < HitomiData.Instance.metadata_collection.Count; j++)
            {
                if (HitomiData.Instance.metadata_collection[j].Artists != null &&
                    HitomiData.Instance.metadata_collection[j].Language == HitomiSetting.Instance.GetModel().Language &&
                    HitomiData.Instance.metadata_collection[j].Artists.Contains(tbArtist.Text))
                {
                    magics.Add(HitomiData.Instance.metadata_collection[j].ID.ToString());
                    i++;
                }
            }
            
            for (int i = 0; i < magics.Count; i++)
            {
                await Task.Run(() => AddMetadataToPanel(i, magics[i]));
            }
        }

        private async void AddMetadataToPanel(int i, string id)
        {
            string thumbnail = "";
            await Task.Run(() => thumbnail = GetThumbnailAddress(id));

            string temp = Path.GetTempFileName();
            WebClient wc = new WebClient();
            wc.Headers["Accept-Encoding"] = "application/x-gzip";
            wc.Encoding = Encoding.UTF8;
            wc.DownloadFile(new Uri(HitomiDef.HitomiThumbnail + thumbnail), temp);

        RETRY:
            try
            {
                PictureBox[] pbs = { pb1, pb2, pb3, pb4, pb5 };
                using (FileStream fs = new FileStream(temp, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose))
                {
                    pbs[i].Image = Image.FromStream(fs);
                }

                lock (info)
                {
                    info[i] = new InfoWrapper(pbs[i].Image);
                    pbs[i].MouseEnter += info[i].Picture_MouseEnter;
                    pbs[i].MouseMove += info[i].Picture_MouseMove;
                    pbs[i].MouseLeave += info[i].Picture_MouseLeave;
                }
            } catch
            {
                Thread.Sleep(100);
                goto RETRY;
            }
        }
        
        private string GetThumbnailAddress(string id)
        {
            try
            {
                WebClient wc = new WebClient
                {
                    Encoding = Encoding.UTF8
                };
                return HitomiParser.ParseGallery(wc.DownloadString(
                    new Uri($"https://hitomi.la/galleries/{id}.html"))).Thumbnail;
            }
            catch { }
            return "";
        }
        
        private void bDelete_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }

        private void bDetail_Click(object sender, System.EventArgs e)
        {
            (new frmArtistInfo(tbArtist.Text)).Show();
        }
    }

    public class InfoWrapper
    {
        InfoForm info;

        public InfoWrapper(Image image)
        {
            info = new InfoForm(image);
            info.Size = new Size(image.Width * 3 / 4, image.Height * 3 / 4);
        }

        public void Picture_MouseEnter(object sender, EventArgs e)
        { info.Location = Cursor.Position; info.Show(); }
        public void Picture_MouseLeave(object sender, EventArgs e)
        { info.Location = Cursor.Position; info.Hide(); }
        public void Picture_MouseMove(object sender, EventArgs e)
        { info.Location = new Point(Cursor.Position.X + 15, Cursor.Position.Y); }
    }

}
