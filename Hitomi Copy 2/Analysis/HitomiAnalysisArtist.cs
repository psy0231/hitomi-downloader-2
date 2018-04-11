/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy.Data;
using System.Collections.Generic;
using System.Linq;

namespace Hitomi_Copy_2.Analysis
{
    public class HitomiAnalysisArtist
    {
        Dictionary<string, float> rate = new Dictionary<string, float>();
        int tags_count = 0;
        public string Aritst { get; set; }

        public HitomiAnalysisArtist(string artist, List<HitomiMetadata> metadatas)
        {
            Dictionary<string, int> tags_map = new Dictionary<string, int>();
            Aritst = artist;

            foreach (var metadata in metadatas)
            {
                if (metadata.Tags == null) continue;
                if (metadata.Language != "korean") continue;
                tags_count += metadata.Tags.Length;
                foreach (var tag in metadata.Tags)
                    if (tags_map.ContainsKey(tag))
                        tags_map[tag] += 1;
                    else
                        tags_map.Add(tag, 1);
            }
            
            foreach(var pair in tags_map)
            {
                rate.Add(pair.Key, pair.Value * pair.Value / (float)tags_count);
            }
        }

        public HitomiAnalysisArtist(IEnumerable<HitomiLogModel> logs)
        {
            Dictionary<string, int> tags_map = new Dictionary<string, int>();
            foreach (var log in logs)
            {
                if (log.Tags == null) continue;
                tags_count += log.Tags.Length;
                foreach (var tag in log.Tags)
                    if (tags_map.ContainsKey(tag))
                        tags_map[tag] += 1;
                    else
                        tags_map.Add(tag, 1);
            }

            foreach (var pair in tags_map)
            {
                rate.Add(pair.Key, pair.Value * pair.Value / (float)tags_count);
            }
        }

        public bool IsExsit(string tag)
        {
            return rate.ContainsKey(tag);
        }

        public float GetRate(string tag)
        {
            return rate[tag];
        }

        public int Size()
        {
            return tags_count;
        }

        public Dictionary<string, float> GetDictionary()
        {
            return rate;
        }

        public string GetTopTag()
        {
            float now = rate.ToList()[0].Value;
            string top = rate.ToList()[0].Key;
            foreach(var pair in rate)
                if (pair.Value > now)
                {
                    now = pair.Value;
                    top = pair.Key;
                }
            return top;
        }
    }
}
