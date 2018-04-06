/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hitomi_Copy.Data
{
    public class HitomiDataSearch
    {
        public static List<HitomiMetadata> Search(HitomiDataQuery query)
        {
            List<HitomiMetadata> result = new List<HitomiMetadata>();
            foreach (var v in HitomiData.Instance.metadata_collection)
            {
                if (v.Language != "korean") continue;
                bool put = true;
                if (query.TagInclude != null)
                    put = IntersectCheck(v.Tags, query.TagInclude);
                if (!put && query.Title != null)
                {
                    if (v.Name != null)
                    {
                        int intersec_count = 0;
                        foreach (var tc in query.Title)
                        {
                            if (v.Name.ToLower().Contains(tc.ToLower()))
                            { intersec_count++; }
                        }
                        if (intersec_count != query.Title.Count)
                            put = false;
                    }
                    else
                    {
                        put = false;
                    }
                }
                if (!put && query.Artists != null)
                    put = IntersectCheck(v.Artists, query.Artists);
                if (!put && query.Groups != null)
                    put = IntersectCheck(v.Groups, query.Groups);
                if (!put && query.Series != null)
                    put = IntersectCheck(v.Parodies, query.Series);
                if (!put && query.Characters != null)
                    put = IntersectCheck(v.Characters, query.Characters);
                if (put && query.TagExclude != null)
                {
                    if (v.Tags != null)
                    {
                        int intersec_count = 0;
                        foreach (var tag in query.TagExclude)
                        {
                            foreach (var vtag in v.Tags)
                                if (vtag.ToLower().Contains(tag.ToLower()))
                                { intersec_count++; break; }
                            if (intersec_count > 0) break;
                        }
                        if (intersec_count > 0)
                            put = false;
                    }
                    else
                    {
                        put = true;
                    }
                }
                if (put) result.Add(v);
            }
            result.Sort((a, b) => a.ID - b.ID);
            return result;
        }

        private static bool IntersectCheck(string[] target, List<string> source)
        {
            if (target != null)
            {
                int intersec_count = 0;
                foreach (var tag in source)
                {
                    foreach (var vtag in target)
                        if (vtag.ToLower().Contains(tag.ToLower()))
                        { intersec_count++; break; }
                }
                if (intersec_count != source.Count)
                    return false;
            }
            else
            {
                return false;
            }
            return true;
        }

        private static bool ExistMetadataId(string id)
        {
            foreach (var v in HitomiData.Instance.metadata_collection)
                if (v.ID.ToString() == id)
                    return true;
            return false;
        }

        private static HitomiMetadata GetMetadataFromId(string id)
        {
            foreach (var v in HitomiData.Instance.metadata_collection)
                if (v.ID.ToString() == id)
                    return v;
            return new HitomiMetadata();
        }

        public static List<HitomiMetadata> Search2(HitomiDataQuery query)
        {
            List<HitomiMetadata> result = new List<HitomiMetadata>();
            foreach (var v in HitomiData.Instance.metadata_collection)
            {
                if (query.Common.Contains(v.ID.ToString()))
                {
                    result.Add(v);
                    continue;
                }
                if (v.Language != "korean") continue;
                if (query.TagExclude != null)
                {
                    if (v.Tags != null)
                    {
                        int intersec_count = 0;
                        foreach (var tag in query.TagExclude)
                        {
                            foreach (var vtag in v.Tags)
                                if (vtag.ToLower().Replace(' ', '_') == tag.ToLower())
                                { intersec_count++; break; }
                            if (intersec_count > 0) break;
                        }
                        if (intersec_count > 0) continue;
                    }
                }
                bool[] check = new bool[query.Common.Count];
                if (query.Common != null)
                {
                    IntersectCountSplit(v.Name.Split(' '), query.Common, ref check);
                    IntersectCountSplit(v.Tags, query.Common, ref check);
                    IntersectCountSplit(v.Artists, query.Common, ref check);
                    IntersectCountSplit(v.Groups, query.Common, ref check);
                    IntersectCountSplit(v.Parodies, query.Common, ref check);
                    IntersectCountSplit(v.Characters, query.Common, ref check);
                }
                bool connect = false;
                if (check.Length == 0) { check = new bool[1]; check[0] = true; }
                if (check[0] && v.Artists != null && query.Artists != null) { check[0] = IsIntersect(v.Artists, query.Artists); connect = true; } else if (query.Artists != null) check[0] = false;
                if (check[0] && v.Tags != null && query.TagInclude != null) { check[0] = IsIntersect(v.Tags, query.TagInclude); connect = true; } else if (query.TagInclude != null) check[0] = false;
                if (check[0] && v.Groups != null && query.Groups != null) { check[0] = IsIntersect(v.Groups, query.Groups); connect = true; } else if (query.Groups != null) check[0] = false;
                if (check[0] && v.Parodies != null && query.Series != null) { check[0] = IsIntersect(v.Parodies, query.Series); connect = true; } else if (query.Series != null) check[0] = false;
                if (check[0] && v.Characters != null && query.Characters != null) { check[0] = IsIntersect(v.Characters, query.Characters); connect = true; } else if (query.Characters != null) check[0] = false;
                if (check.All((x => x)) && ((query.Common.Count == 0 && connect) || query.Common.Count > 0))
                    result.Add(v);
            }
            result.Sort((a, b) => b.ID - a.ID);
            return result;
        }

        private static bool IsIntersect(string[] target, List<string> source)
        {
            bool[] check = new bool[source.Count];
            for (int i = 0; i < source.Count; i++)
                foreach (string e in target)
                    if (e.ToLower().Replace(' ', '_') == source[i].ToLower())
                    {
                        check[i] = true;
                        break;
                    }
            return check.All((x => x));
        }

        private static void IntersectCountSplit(string[] target, List<string> source, ref bool[] check)
        {
            if (target != null)
            {
                for (int i = 0; i < source.Count; i++)
                    foreach (string e in target)
                        if (e.ToLower().Split(' ').Contains(source[i].ToLower()))
                        {
                            check[i] = true;
                            break;
                        }
            }
        }
    }
}
