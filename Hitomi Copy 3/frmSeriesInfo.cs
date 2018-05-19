/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy.Data;
using Hitomi_Copy_2;
using Hitomi_Copy_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class frmSeriesInfo : Form
    {
        string series;
        Form closed_form;
        List<string> id = new List<string>();

        public frmSeriesInfo(Form closed, string series)
        {
            InitializeComponent();

            this.series = series;
            closed_form = closed;
            Text += series;
        }

        private void frmTagInfo_Load(object sender, EventArgs e)
        {
            ColumnInit();

            List<HitomiMetadata> result = new List<HitomiMetadata>();
            var hitomi_data = HitomiData.Instance.metadata_collection;
            foreach (var data in hitomi_data)
            {
                if (data.Parodies != null && data.Parodies.Contains(series))
                {
                    result.Add(data);
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
                id.Add(result[i].ID.ToString());
            }
            lvil.Sort((a, b) => Convert.ToUInt32(b.SubItems[0].Text).CompareTo(Convert.ToUInt32(a.SubItems[0].Text)));
            lvHistory.Items.AddRange(lvil.ToArray());
        }

        private void ColumnInit()
        {
            ColumnSorter.InitListView(lvHistory);
        }

        private void lvHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvHistory.SelectedItems.Count > 0)
            {
                var hitomi_data = HitomiData.Instance.metadata_collection;
                string target = lvHistory.SelectedItems[0].SubItems[0].Text;
                foreach (var metadata in hitomi_data)
                {
                    if (metadata.ID.ToString() == target)
                    {
                        (new frmGalleryInfo(this, metadata)).Show();
                        return;
                    }
                }
            }
        }

        private void frmTagInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { closed_form.BringToFront(); } catch { }
        }

        private void bDownloadAll_Click(object sender, EventArgs e)
        {
            foreach (var pe in id)
                (Application.OpenForms[0] as frmMain).RemoteDownloadArticleFromId(pe);
            (Application.OpenForms[0] as frmMain).BringToFront();
        }

        private void bDownload_Click(object sender, EventArgs e)
        {
            foreach (var id in lvHistory.SelectedItems)
                (Application.OpenForms[0] as frmMain).RemoteDownloadArticleFromId((id as ListViewItem).SubItems[0].Text);
            (Application.OpenForms[0] as frmMain).BringToFront();
        }
    }
}
