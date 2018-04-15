/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy.Data;
using Hitomi_Copy_2;
using Hitomi_Copy_2.Analysis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
            comboBox1.SelectedIndex = 0;
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

        #region Ranking
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
                if (metadata.Language != HitomiSetting.Instance.GetModel().Language) continue;
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

        #endregion

        #region History

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
        #endregion
        
        #region Chart

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateChart();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateChart();
        }

        public void UpdateChart()
        {
            if (comboBox1.SelectedIndex == 0)
                UpdateGalleryVariationChart();
            else if (comboBox1.SelectedIndex == 1)
                UpdateGalleryIncrements();
            else if (comboBox1.SelectedIndex == 2)
                UpdateTagIncrements();
            else if (comboBox1.SelectedIndex == 3)
                UpdateTagKoreanIncrements();
            else if (comboBox1.SelectedIndex == 4)
                UpdateTagKoreanVariation();
            else if (comboBox1.SelectedIndex == 5)
                UpdateArtistsIncremetns();
            else if (comboBox1.SelectedIndex == 6)
                UpdateArtistsKoreanIncremetns();
            else if (comboBox1.SelectedIndex == 7)
                UpdateArtistsKoreanVariation();
            else if (comboBox1.SelectedIndex == 8)
                UpdateGroupsKoreanIncremetns();
            else if (comboBox1.SelectedIndex == 9)
                UpdateGroupsKoreanVariation();
            else if (comboBox1.SelectedIndex == 10)
                UpdateSeriesKoreanIncremetns();
            else if (comboBox1.SelectedIndex == 11)
                UpdateSeriesKoreanVariation();
            else if (comboBox1.SelectedIndex == 12)
                UpdateCharactersKoreanIncremetns();
            else if (comboBox1.SelectedIndex == 13)
                UpdateCharactersKoreanVariation();
        }

        public void UpdateGalleryVariationChart()
        {
            HitomiAnalysisTrend.Instance.UpdateGalleryVariation();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 100000 / 2;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "작품 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 500;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Spline;
                series.Color = Color.LightPink;
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 3;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateGalleryIncrements()
        {
            HitomiAnalysisTrend.Instance.UpdataGalleryIncrements();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "누적 작품 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 50000;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Spline;
                series.Color = Color.LightPink;
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 3;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateTagIncrements()
        {
            HitomiAnalysisTrend.Instance.UpdateTagIncrements();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "누적 작품 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 10000;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Spline;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateTagKoreanIncrements()
        {
            HitomiAnalysisTrend.Instance.UpdateTagKoreanIncrements();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "누적 작품 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 1000;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Spline;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }


        public void UpdateTagKoreanVariation()
        {
            HitomiAnalysisTrend.Instance.UpdateTagKoreanVariation();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 1125000;//0;
            chart1.ChartAreas[0].AxisX.Interval = 10000;//100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "작품 변동 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 50;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Spline;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateArtistsIncremetns()
        {
            HitomiAnalysisTrend.Instance.UpdateArtistsIncremetns(checkBox1.Checked, textBox1.Text);

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "누적 작품 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 100;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Line;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateArtistsKoreanIncremetns()
        {
            HitomiAnalysisTrend.Instance.UpdateArtistsKoreanIncremetns(checkBox1.Checked, textBox1.Text);

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "누적 작품 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 10;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Line;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateArtistsKoreanVariation()
        {
            HitomiAnalysisTrend.Instance.UpdateArtistsKoreanVariation();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 1125000;//0;
            chart1.ChartAreas[0].AxisX.Interval = 10000;//100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "작품 변동 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 5;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 20;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Spline;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateGroupsKoreanIncremetns()
        {
            HitomiAnalysisTrend.Instance.UpdateGroupsKoreanIncremetns(checkBox1.Checked, textBox1.Text);

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "누적 작품 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 10;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Line;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateGroupsKoreanVariation()
        {
            HitomiAnalysisTrend.Instance.UpdateGroupsKoreanVariation();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 1125000;//0;
            chart1.ChartAreas[0].AxisX.Interval = 10000;//100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "작품 변동 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 5;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 15;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Spline;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateSeriesKoreanIncremetns()
        {
            HitomiAnalysisTrend.Instance.UpdateSeriesKoreanIncremetns(checkBox1.Checked, textBox1.Text);

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "누적 작품 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 100;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Line;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateSeriesKoreanVariation()
        {
            HitomiAnalysisTrend.Instance.UpdateSeriesKoreanVariation();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 1125000;//0;
            chart1.ChartAreas[0].AxisX.Interval = 10000;//100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "작품 변동 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 5;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 30;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Spline;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }


        public void UpdateCharactersKoreanIncremetns()
        {
            HitomiAnalysisTrend.Instance.UpdateCharactersKoreanIncremetns(checkBox1.Checked, textBox1.Text);

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "누적 작품 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 50;
            chart1.ChartAreas[0].AxisY.Maximum = 300;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Line;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }

        public void UpdateCharactersKoreanVariation()
        {
            HitomiAnalysisTrend.Instance.UpdateCharactersKoreanVariation();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("ChartArea1");

            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.IsPositionedInside = true;

            chart1.ChartAreas[0].AxisX.Title = "아이디 간격";
            chart1.ChartAreas[0].AxisX.TitleFont = Font;
            chart1.ChartAreas[0].AxisX.Minimum = 1125000;//0;
            chart1.ChartAreas[0].AxisX.Interval = 10000;//100000 / 4 * 3;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].AxisY.Title = "작품 변동 수";
            chart1.ChartAreas[0].AxisY.TitleFont = Font;
            chart1.ChartAreas[0].AxisY.Interval = 5;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 20;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas[0].BackColor = Color.Gray;

            foreach (var sample in HitomiAnalysisTrend.Instance.samples)
            {
                Series series = new Series();
                series.Name = sample.name;
                series.Font = Font;
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Spline;
                Random rm = new Random(sample.name.GetHashCode());
                series.Color = Color.FromArgb(rm.Next(256), rm.Next(256), rm.Next(256));
                series.LabelBackColor = Color.Gray;
                series.BorderWidth = 2;

                foreach (var point in sample.points)
                    series.Points.Add(new DataPoint(point.X, point.Y));

                chart1.Series.Add(series);
            }
        }
        #endregion

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chart1.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        try
                        {
                            var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                            var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                            tooltip.Show(result.Series.Name + ", X=" + prop.XValue + ", Y=" + prop.YValues[0], this.chart1,
                                                pos.X, pos.Y - 15);
                        } catch { }
                    }
                }
            }
        }
    }
}
