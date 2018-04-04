/* Copyright (C) 2018. Hitomi Parser Developers */

using System.Collections.Generic;

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
                if (query.TagInclude.Contains(v.ID.ToString()))
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
                                if (vtag.ToLower().Contains(tag.ToLower()))
                                { intersec_count++; break; }
                            if (intersec_count > 0) break;
                        }
                        if (intersec_count > 0) continue;
                    }
                }
                int put = 0;
                if (query.TagInclude != null)
                {
                    put += IntersectCount(v.Tags, query.TagInclude);
                    put += IntersectCount(v.Artists, query.TagInclude);
                    put += IntersectCount(v.Groups, query.TagInclude);
                    put += IntersectCount(v.Parodies, query.TagInclude);
                    put += IntersectCount(v.Characters, query.TagInclude);
                }
                if (put >= query.TagInclude.Count)
                    result.Add(v);
            }
            result.Sort((a, b) => a.ID - b.ID);
            return result;
        }

        private static int IntersectCount(string[] target, List<string> source)
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
                return intersec_count;
            }
            return 0;
        }
    }
}
