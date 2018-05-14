/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Hitomi_Copy_3
{
    public partial class frmStatRate : Form
    {
        public frmStatRate()
        {
            InitializeComponent();
        }

        private void frmStatRate_Load(object sender, EventArgs e)
        {
            Dictionary<string, int> tags_map = new Dictionary<string, int>();

            foreach (var log in HitomiLog.Instance.GetEnumerator())
            {
                if (log.Tags == null) continue;
                foreach (var tag in log.Tags)
                    if (tags_map.ContainsKey(HitomiCommon.LegalizeTag(tag)))
                        tags_map[HitomiCommon.LegalizeTag(tag)] += 1;
                    else
                        tags_map.Add(HitomiCommon.LegalizeTag(tag), 1);
            }

            var list = tags_map.ToList();
            list.Sort((a, b) => b.Value.CompareTo(a.Value));
            foreach (var pair in list)
                clbTags.Items.Add(pair.Key + $" ({pair.Value})", true);
        }
    }
}
