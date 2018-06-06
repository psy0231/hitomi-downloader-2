/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy_2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hitomi_Copy_3.Analysis
{
    public class HitomiAnalysisTagCount
    {
        private static readonly Lazy<HitomiAnalysisTagCount> instance = new Lazy<HitomiAnalysisTagCount>(() => new HitomiAnalysisTagCount());
        public static HitomiAnalysisTagCount Instance => instance.Value;

        public List<KeyValuePair<string, int>> tag_count;

        public HitomiAnalysisTagCount()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var data in HitomiLog.Instance.GetEnumerator())
            {
                if (data.Tags == null) continue;
                foreach (var tag in data.Tags)
                {
                    string legal = HitomiCommon.LegalizeTag(tag);
                    if (dic.ContainsKey(legal))
                        dic[legal]++;
                    else
                        dic.Add(legal, 1);
                }
            }

            tag_count = dic.ToList();
            tag_count.Sort((a, b) => b.Value.CompareTo(a.Value));
        }
    }
}
