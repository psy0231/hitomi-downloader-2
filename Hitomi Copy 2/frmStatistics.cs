/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy.Data;
using Hitomi_Copy_2;
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
        public frmStatistics()
        {
            InitializeComponent();
        }
        
        private void frmStatistics_Load(object sender, EventArgs e)
        {
            ColumnInit();
            
            Task.Run(() => RankingProcess());
            Task.Run(() => UpdateHistory());
        }

        private void ColumnInit()
        {
            ColumnSorter.InitListView(lvRankArtists);
            ColumnSorter.InitListView(lvRankCharacters);
            ColumnSorter.InitListView(lvRankGroup);
            ColumnSorter.InitListView(lvRankSeries);
            ColumnSorter.InitListView(lvRankTag);
            ColumnSorter.InitListView(lvHistory);
        }
        
        private void RankingProcess()
        {
            Dictionary<string, int> rank_tag = new Dictionary<string, int>();
            Dictionary<string, int> rank_artists = new Dictionary<string, int>();
            Dictionary<string, int> rank_series = new Dictionary<string, int>();
            Dictionary<string, int> rank_characters = new Dictionary<string, int>();
            Dictionary<string, int> rank_group = new Dictionary<string, int>();

            var hitomi_data = HitomiData.Instance.metadata_collection;
            foreach (var metadata in hitomi_data)
            {
                if (metadata.Language != "korean") continue;
                if (metadata.Tags != null)
                    metadata.Tags.ToList().ForEach((tag) => { if (rank_tag.ContainsKey(tag)) rank_tag[tag] += 1; else rank_tag.Add(tag, 1); });
                if (metadata.Artists != null)
                    metadata.Artists.ToList().ForEach((artist) => { if (rank_artists.ContainsKey(artist)) rank_artists[artist] += 1; else rank_artists.Add(artist, 1); });
                if (metadata.Parodies != null)
                    metadata.Parodies.ToList().ForEach((series) => { if (rank_series.ContainsKey(series)) rank_series[series] += 1; else rank_series.Add(series, 1); });
                if (metadata.Characters != null)
                    metadata.Characters.ToList().ForEach((character) => { if (rank_characters.ContainsKey(character)) rank_characters[character] += 1; else rank_characters.Add(character, 1); });
                if (metadata.Groups != null)
                    metadata.Groups.ToList().ForEach((group) => { if (rank_group.ContainsKey(group)) rank_group[group] += 1; else rank_group.Add(group, 1); });
            }

            var l1 = rank_tag.ToList();
            l1.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            var l2 = rank_artists.ToList();
            l2.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            var l3 = rank_series.ToList();
            l3.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            var l4 = rank_characters.ToList();
            l4.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            var l5 = rank_group.ToList();
            l5.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            AddTo(lvRankTag, l1);
            AddTo(lvRankArtists, l2);
            AddTo(lvRankSeries, l3);
            AddTo(lvRankCharacters, l4);
            AddTo(lvRankGroup, l5);
        }

        private void AddTo(ListView lv, List<KeyValuePair<string,int>> result)
        {
            List<ListViewItem> lvil = new List<ListViewItem>();
            for (int i = 0; i < result.Count; i++)
            {
                lvil.Add(new ListViewItem(new string[]
                {
                    (i+1).ToString(),
                    result[i].Key,
                    result[i].Value.ToString()
                }));
            }
            AddToRank(lv, lvil.ToArray());
        }
        private void AddToRank(ListView lv, ListViewItem[] items)
        {
            if (lv.InvokeRequired)
            {
                Invoke(new Action<ListView,ListViewItem[]>(AddToRank), new object[] { lv, items });
                return;
            }
            lv.Items.AddRange(items);
        }

        private void UpdateHistory()
        {
            List<HitomiMetadata> result = new List<HitomiMetadata>();
            var hitomi_data = HitomiData.Instance.metadata_collection;
            foreach (var elem in HitomiLog.Instance.GetEnumerator().Reverse())
            {
                foreach (var data in hitomi_data)
                {
                    if (elem.Id == data.ID.ToString())
                    {
                        result.Add(data);
                        break;
                    }
                }
            }
            HitomiLog.Instance.Save();

            List<ListViewItem> lvil = new List<ListViewItem>();
            for (int i = 0; i < result.Count; i++)
            {
                lvil.Add(new ListViewItem(new string[]
                {
                    result[i].ID.ToString(),
                    result[i].Name,
                    result[i].Type,
                    string.Join(",", result[i].Artists ?? Enumerable.Empty<string>()),
                    string.Join(",", result[i].Groups ?? Enumerable.Empty<string>()),
                    string.Join(",", result[i].Parodies ?? Enumerable.Empty<string>()),
                    string.Join(",", result[i].Characters ?? Enumerable.Empty<string>()),
                    string.Join(",", result[i].Tags ?? Enumerable.Empty<string>())
                }));
            }
            AddToHistory(lvil.ToArray());

        }
        private void AddToHistory(ListViewItem[] items)
        {
            if (lvHistory.InvokeRequired)
            {
                Invoke(new Action<ListViewItem[]>(AddToHistory), new object[] { items });
                return;
            }
            lvHistory.Items.AddRange(items);
        }

        private void lvHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvHistory.SelectedItems.Count > 0)
            {
                var hitomi_data = HitomiData.Instance.metadata_collection;
                foreach (var metadata in hitomi_data)
                {
                    if (metadata.ID.ToString() == lvHistory.SelectedItems[0].SubItems[0].Text)
                    {
                        (new frmGalleryInfo(this, metadata)).Show();
                        return;
                    }
                }
            }
        }

        private void lvRankArtists_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvRankArtists.SelectedItems.Count > 0)
            {
                (new frmArtistInfo(this, lvRankArtists.SelectedItems[0].SubItems[1].Text)).Show();
            }
        }

        private void lvRankGroup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvRankGroup.SelectedItems.Count > 0)
            {
                (new frmGroupInfo(this, lvRankGroup.SelectedItems[0].SubItems[1].Text)).Show();
            }
        }

        private void lvRankTag_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvRankTag.SelectedItems.Count > 0)
            {
                (new frmTagInfo(this, lvRankTag.SelectedItems[0].SubItems[1].Text)).Show();
            }
        }
        
        private void lvRankSeries_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvRankSeries.SelectedItems.Count > 0)
            {
                (new frmSeriesInfo(this, lvRankSeries.SelectedItems[0].SubItems[1].Text)).Show();
            }
        }

        private void lvRankCharacters_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvRankCharacters.SelectedItems.Count > 0)
            {
                (new frmCharacterInfo(this, lvRankCharacters.SelectedItems[0].SubItems[1].Text)).Show();
            }
        }
    }
}
