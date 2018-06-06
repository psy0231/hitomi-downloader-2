/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy_2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hitomi_Copy_3.Analysis
{
    public class HitomiAnalysisArtistCount
    {
        private static readonly Lazy<HitomiAnalysisArtistCount> instance = new Lazy<HitomiAnalysisArtistCount>(() => new HitomiAnalysisArtistCount());
        public static HitomiAnalysisArtistCount Instance => instance.Value;

        public List<KeyValuePair<string, int>> artist_count;

        public HitomiAnalysisArtistCount()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var data in HitomiLog.Instance.GetEnumerator())
            {
                if (data.Artists == null) continue;
                foreach (var artist in data.Artists)
                {
                    if (dic.ContainsKey(artist))
                        dic[artist]++;
                    else
                        dic.Add(artist, 1);
                }
            }

            artist_count = dic.ToList();
            artist_count.Sort((a, b) => b.Value.CompareTo(a.Value));
        }
    }
}
