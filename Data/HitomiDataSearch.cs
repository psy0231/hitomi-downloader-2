/* Copyright (C) 2018. Hitomi Parser Developers */

using System.Collections.Generic;

namespace Hitomi_Copy.Data
{
    public class HitomiDataSearch
    {
        HitomiData data;

        public HitomiDataSearch(HitomiData data)
        {
            this.data = data;
        }

        public List<HitomiMetadata> Search(HitomiDataQuery query)
        {
            List<HitomiMetadata> result = new List<HitomiMetadata>();
            foreach (var v in data.metadata_collection)
            {
                if (v.Language != "korean") continue;
                bool put = true;
                if (query.TagInclude != null)
                {
                    if (v.Tags !=null)
                    {
                        int intersec_count = 0;
                        foreach (var tag in query.TagInclude)
                        {
                            foreach (var vtag in v.Tags)
                                if (vtag.ToLower().Contains(tag.ToLower()))
                                { intersec_count++; break; }
                        }
                        if (intersec_count != query.TagInclude.Count)
                            put = false;
                    }
                    else
                    {
                        put = false;
                    }
                }
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
                if (put && query.Title != null)
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
                if (put && query.Artists !=null)
                {
                    if (v.Artists != null)
                    {
                        int intersec_count = 0;
                        foreach (var tc in query.Artists)
                        {
                            foreach (var vtag in v.Artists)
                                if (vtag.ToLower().Contains(tc.ToLower()))
                                { intersec_count++; break; }
                        }
                        if (intersec_count != query.Artists.Count)
                            put = false;
                    }
                    else
                    {
                        put = false;
                    }
                }
                if (put) result.Add(v);
            }
            result.Sort((a, b) => a.ID - b.ID);
            return result;
        }
    }
}
