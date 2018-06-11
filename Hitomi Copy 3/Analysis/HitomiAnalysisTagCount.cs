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
            foreach (var legal in from data in HitomiLog.Instance.GetEnumerator() where data.Tags != null from tag in data.Tags select HitomiCommon.LegalizeTag(tag))
            {
                if (dic.ContainsKey(legal))
                    dic[legal]++;
                else
                    dic.Add(legal, 1);
            }

            tag_count = dic.ToList();
            tag_count.Sort((a, b) => b.Value.CompareTo(a.Value));
        }
    }
}
