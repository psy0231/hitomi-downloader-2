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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy_3
{
    public partial class RecommendControl : UserControl
    {
        string artist;
        CancellationTokenSource Abort = new CancellationTokenSource();

        public RecommendControl(int index)
        {
            InitializeComponent();
            
            artist = tbArtist.Text = HitomiAnalysis.Instance.Rank[index].Item1;
            if (HitomiAnalysis.Instance.Rank[index].Item2.ToString().Length > 8)
                lScore.Text = HitomiAnalysis.Instance.Rank[index].Item2.ToString().Remove(8) + " 점";
            else
                lScore.Text = HitomiAnalysis.Instance.Rank[index].Item2.ToString() + " 점";
            tbScoreDetail.Text = HitomiAnalysis.Instance.Rank[index].Item3;

            Disposed += OnDispose;
            LogEssential.Instance.PushLog(() => $"Created RecommendControl! {tbArtist.Text} {lScore.Text}");
        }

        public RecommendControl(string artist)
        {
            InitializeComponent();

            this.artist = tbArtist.Text = artist;
            lScore.Text = "";
            label2.Hide();
            tbScoreDetail.Hide();

            Disposed += OnDispose;
            LogEssential.Instance.PushLog(() => $"Created RecommendControl! {tbArtist.Text}");
        }

        private void OnDispose(object sender, EventArgs e)
        {
            Abort.Cancel();
        }

        private void RecommendControl_LoadAsync(object sender, System.EventArgs e)
        {
            Task.Run(() => LoadThumbnailAsync()).Catch();
        }

        private void LoadThumbnailAsync()
        {
            List<string> titles = new List<string>();
            List<string> magics = new List<string>();

            for (int i = 0, j = 0; i < 5 && j < HitomiData.Instance.metadata_collection.Count; j++)
            {
                HitomiMetadata metadata = HitomiData.Instance.metadata_collection[j];
                HitomiSettingModel setting = HitomiSetting.Instance.GetModel();
                if ((metadata.Language == setting.Language || setting.Language == "ALL") &&
                    metadata.Artists?.Contains(tbArtist.Text) == true)
                {
                    string ttitle = metadata.Name.Split('|')[0];
                    if (titles.Count > 0 && !titles.TrueForAll(title => StringAlgorithms.get_diff(ttitle, title) > setting.TextMatchingAccuracy)) continue;

                    titles.Add(ttitle);
                    magics.Add(metadata.ID.ToString());
                    i++;
                }
            }

            if (HitomiSetting.Instance.GetModel().DetailedLog)
            {
                LogEssential.Instance.PushLog(() => $"This images will be loaded. [RecommendControl]");
                LogEssential.Instance.PushLog(magics);
            }

            for (int i = 0; i < magics.Count; i++)
            {
                if (Abort.IsCancellationRequested) return;
                System.Diagnostics.Debug.WriteLine($"HC: LoadThumb: ${i} ${magics[i]}");
                AddMetadataToPanel(i, magics[i]).Catch();
            }
        }

        private async Task AddMetadataToPanel(int i, string id)
        {
            WebClient client = Util.PlainWebClient();
            var galleryUri = new Uri($"https://hitomi.la/galleries/{id}.html");
            string html = await client.DownloadStringTaskAsync(galleryUri).ConfigureAwait(false);
            if (Abort.IsCancellationRequested) return;

            string thumbPath = HitomiParser.ParseGallery(html).Thumbnail;
            var thumbUri = new Uri(HitomiDef.HitomiThumbnail + thumbPath);
            Stream thumbnail = await client.OpenReadTaskAsync(thumbUri).ConfigureAwait(false);
            if (Abort.IsCancellationRequested) return;

            Image img = Image.FromStream(thumbnail);

            PictureBox[] pbs = { pb1, pb2, pb3, pb4, pb5 };
            PaintEventHandler peh = null;
            peh = (o, e) =>
            {
                pbs[i].Image = img;
                pbs[i].Paint -= peh;
            };
            pbs[i].Paint += peh;
            pbs[i].Invalidate();

            var popupSize = new Size(img.Width * 3 / 4, img.Height * 3 / 4);
            new LazyPicturePopup(pbs[i], popupSize);

            LogEssential.Instance.PushLog(() => $"Load successful! {HitomiDef.HitomiThumbnail + thumbnail} {i} {id}");
        }

        private void bDelete_Click(object sender, System.EventArgs e)
        {
            List<string> list;
            if (HitomiSetting.Instance.GetModel().UninterestednessArtists != null)
                list = HitomiSetting.Instance.GetModel().UninterestednessArtists.ToList();
            else
                list = new List<string>();
            list.Add(tbArtist.Text);
            HitomiSetting.Instance.GetModel().UninterestednessArtists = list.ToArray();
            HitomiSetting.Instance.Save();
            this.Dispose();
        }

        private void bDetail_Click(object sender, System.EventArgs e)
        {
            (new frmArtistInfo(tbArtist.Text)).Show();
        }
    }

    public class StringAlgorithms
    {
        public static int get_diff(string a, string b)
        {
            int x = a.Length;
            int y = b.Length;
            int i, j;

            if (x == 0) return x;
            if (y == 0) return y;
            int[] v0 = new int[(y + 1) << 1];

            for (i = 0; i < y + 1; i++) v0[i] = i;
            for (i = 0; i < x; i++)
            {
                v0[y+1] = i + 1;
                for (j = 0; j < y; j++)
                    v0[y + j + 2] = Math.Min(Math.Min(v0[y+j+1], v0[j + 1]) + 1, v0[j] + ((a[i] == b[j]) ? 0 : 1));
                for (j = 0; j < y + 1; j++) v0[j] = v0[y+j+1];
            }
            return v0[y+y+1];
        }
    }
}
