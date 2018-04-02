/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class frmStatistics : Form
    {
        HitomiData hitomi_data;

        public frmStatistics(HitomiData hitomi_data)
        {
            InitializeComponent();

            this.hitomi_data = hitomi_data;
        }

        List<KeyValuePair<string, int>> tag_rank_list;
        List<Tuple<string, KeyValuePair<string, int>[]>> artist_tag_rank;
        private void frmStatistics_Load(object sender, EventArgs e)
        {
            Dictionary<string, int> my_tag_rank = new Dictionary<string, int>();
            foreach (var v in HitomiLog.Instance.GetEnumerator())
            {
                foreach (var tag in v.Tags)
                {
                    string legalize = HitomiLegalize.LegalizeTag(tag);
                    if (my_tag_rank.ContainsKey(legalize))
                        my_tag_rank[legalize] += 1;
                    else
                        my_tag_rank.Add(legalize, 1);
                }
            }
            tag_rank_list = my_tag_rank.ToList();
            tag_rank_list.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            List<ListViewItem> lvil = new List<ListViewItem>();
            foreach (var v in tag_rank_list)
            {
                lvil.Add(new ListViewItem(new string[]
                {
                    v.Key,
                    v.Value.ToString()
                }));
            }
            lvMyTagRank.Items.AddRange(lvil.ToArray());

            // suprising message
            if (tag_rank_list[0].Value > 10)
            {
                if (tag_rank_list[0].Key.Contains("female:loli") || tag_rank_list[0].Key.Contains("male:shota"))
                    lMessage.Text = "으아아악 패도다!! 자살해!!";
                else if (tag_rank_list[0].Key.Contains("female:big breasts"))
                    lMessage.Text = "가슴큰게 좋아요?";
                else if (tag_rank_list[0].Key.Contains("female:sole female"))
                    lMessage.Text = "진정한 사랑을 강구하는 멋진 남자!";
                else if (tag_rank_list[0].Key.Contains("male:sole male"))
                    lMessage.Text = "진정한 사랑을 강구하는 멋진 게이!";
                else if (tag_rank_list[0].Key.Contains("bondage"))
                    lMessage.Text = "맞는거 좋아해요?";
                else if (tag_rank_list[0].Key.Contains("rape"))
                    lMessage.Text = "상태가 심각합니다.";
                else if (tag_rank_list[0].Key.Contains("female:futanari"))
                    lMessage.Text = "달려있어?!";
                else if (tag_rank_list[0].Key.Contains("anal"))
                    lMessage.Text = "똥꼬충";
            }
            else
            {
                lMessage.Text = "다운로드 수가 적네요 ...";
            }

            if (tag_rank_list.Count > 2)
            Task.Run(() => ArtistsRecommend());
        }
        
        public void ArtistsRecommend()
        {
            var hitomi_data = (Application.OpenForms[0] as frmMain).hitomi_data.metadata_collection;
            Dictionary<string, Dictionary<string, int>> artists_tag_count = new Dictionary<string, Dictionary<string, int>>();
            foreach (var metadata in hitomi_data)
            {
                if (metadata.Language != "korean") continue;
                if (metadata.Artists == null || metadata.Tags == null || metadata.Artists.Length == 0) continue;
                if (!artists_tag_count.ContainsKey(metadata.Artists[0]))
                    artists_tag_count.Add(metadata.Artists[0], new Dictionary<string, int>());
                foreach (var tag in metadata.Tags)
                {
                    if (!artists_tag_count[metadata.Artists[0]].ContainsKey(tag))
                        artists_tag_count[metadata.Artists[0]].Add(tag, 1);
                    else
                        artists_tag_count[metadata.Artists[0]][tag] += 1;
                }
            }
            
            var myList = artists_tag_count.ToList();
            artist_tag_rank = new List<Tuple<string, KeyValuePair<string, int>[]>>();
            for (int i = 0; i < myList.Count; i++)
            {
                artist_tag_rank.Add(new Tuple<string, KeyValuePair<string, int>[]>(myList[i].Key, myList[i].Value.OrderBy(key => key.Value).ToArray()));
            }

            List<Tuple<string, int, string>> result = new List<Tuple<string, int, string>>();
            for (int i = 0; i < artist_tag_rank.Count; i++)
            {
                if (artist_tag_rank[i].Item2.Last().Key != tag_rank_list[0].Key)
                    continue;
                List<Tuple<string, int>> tag_score = new List<Tuple<string, int>>();
                int score = artist_tag_rank[i].Item2.Last().Value * tag_rank_list[0].Value;
                tag_score.Add(new Tuple<string, int>(tag_rank_list[0].Key, artist_tag_rank[i].Item2.Last().Value * tag_rank_list[0].Value));
                for (int j = artist_tag_rank[i].Item2.Count() - 2; j >= 0; j--)
                {
                    for (int k = 1; k < tag_rank_list.Count; k++)
                    {
                        if (artist_tag_rank[i].Item2[j].Key == tag_rank_list[k].Key)
                        {
                            score += artist_tag_rank[i].Item2[j].Value * tag_rank_list[k].Value;
                            tag_score.Add(new Tuple<string, int>(tag_rank_list[k].Key, artist_tag_rank[i].Item2[j].Value * tag_rank_list[k].Value));
                        }
                    }
                }
                var tag_score_list = tag_score.ToList();
                tag_score_list.Sort((pair1, pair2) => pair2.Item2.CompareTo(pair1.Item2));
                StringBuilder builder = new StringBuilder();
                for (int j = 0; j < tag_score_list.Count; j++)
                    builder.Append($"{tag_score_list[j].Item1}({tag_score_list[j].Item2}),");
                result.Add(new Tuple<string, int, string>(artist_tag_rank[i].Item1, score, builder.ToString()));
            }
            
            result.Sort((pair1, pair2) => pair2.Item2.CompareTo(pair1.Item2));

            // 빡세다 ;;
            
            List<ListViewItem> lvil = new List<ListViewItem>();
            for (int i = 0; i < 50 && i < result.Count; i++)
            {
                lvil.Add(new ListViewItem(new string[]
                {
                    result[i].Item1,
                    result[i].Item2.ToString(),
                    result[i].Item3
                }));
            }
            AddToRecommendArtists(lvil.ToArray());
        }

        private void AddToRecommendArtists(ListViewItem[] items)
        {
            if (lvRecommendArtists.InvokeRequired)
            {
                Invoke(new Action<ListViewItem[]>(AddToRecommendArtists), new object[] { items });
                return;
            }
            lvRecommendArtists.Items.AddRange(items);
        }

        private void lvRecommendArtists_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvRecommendArtists.SelectedItems.Count > 0)
            {
                (new frmArtistInfo(lvRecommendArtists.SelectedItems[0].SubItems[0].Text)).Show();
            }
        }
    }
}
