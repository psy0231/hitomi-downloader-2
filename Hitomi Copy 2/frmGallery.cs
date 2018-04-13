/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy;
using Hitomi_Copy.Data;
using Hitomi_Copy_2.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hitomi_Copy_2
{
    public partial class frmGallery : Form
    {
        Form closed_form;

        public frmGallery(Form closed)
        {
            InitializeComponent();

            closed_form = closed;
        }

        private void frmGallery_Load(object sender, EventArgs e)
        {
            ColumnInit();

            HitomiAnalysisGallery gallery = new HitomiAnalysisGallery();

            List<ListViewItem> lvil = new List<ListViewItem>();
            for (int i = 0; i < gallery.gallery_data.Count && i < 10000; i++)
            {
                lvil.Add(new ListViewItem(new string[]
                {
                    gallery.gallery_data[i].Value.Item2.ID.ToString(),
                    gallery.gallery_data[i].Value.Item2.Name,
                    gallery.gallery_data[i].Value.Item1.ToString(),
                    gallery.gallery_data[i].Value.Item2.Type,
                    string.Join(",", gallery.gallery_data[i].Value.Item2.Artists ?? Enumerable.Empty<string>()),
                    string.Join(",", gallery.gallery_data[i].Value.Item2.Groups ?? Enumerable.Empty<string>()),
                    string.Join(",", gallery.gallery_data[i].Value.Item2.Parodies ?? Enumerable.Empty<string>()),
                    string.Join(",", gallery.gallery_data[i].Value.Item2.Characters ?? Enumerable.Empty<string>()),
                    string.Join(",", gallery.gallery_data[i].Value.Item2.Tags ?? Enumerable.Empty<string>())
                }));
            }
            lvHistory.Items.AddRange(lvil.ToArray());
        }

        private void ColumnInit()
        {
            ColumnSorter.InitListView(lvHistory);
        }

        private void frmGallery_FormClosed(object sender, FormClosedEventArgs e)
        {
            try { closed_form.BringToFront(); } catch { }
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
    }
}
