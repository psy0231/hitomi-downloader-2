/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class frmData : Form
    {
        public frmData()
        {
            InitializeComponent();
        }

        HitomiData data = new HitomiData();

        private void button1_Click(object sender, EventArgs e)
        {
            //await data.DownloadTagJson();
            //await data.DownloadMetadata();
            data.LoadTagJson();
            data.LoadMetadataJson();
            MessageBox.Show(data.metadata_collection.Count.ToString());
        }

        private void button2_ClickAsync(object sender, EventArgs e)
        {
            HitomiDataQuery query = new HitomiDataQuery();
            query.TagInclude = new List<string>();
            query.TagInclude.Add("loli");
            query.Artists = new List<string>();
            query.Artists.Add("hisasi");
            HitomiDataSearch search = new HitomiDataSearch(data);

            var result = search.Search(query);
            MessageBox.Show(result.Count.ToString());
        }
    }
}
