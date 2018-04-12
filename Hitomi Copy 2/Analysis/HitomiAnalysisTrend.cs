/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Hitomi_Copy_2.Analysis
{
    public class HitomiAnalysisTrendElement
    {
        public string name;
        public List<Point> points;
    }

    public class HitomiAnalysisTrend
    {
        private static readonly Lazy<HitomiAnalysisTrend> instance = new Lazy<HitomiAnalysisTrend>(() => new HitomiAnalysisTrend());
        public static HitomiAnalysisTrend Instance => instance.Value;
        
        public const int interval = 5000;
        
        public List<HitomiAnalysisTrendElement> samples = new List<HitomiAnalysisTrendElement>();

        List<KeyValuePair<int, List<HitomiMetadata>>> datas;

        public HitomiAnalysisTrend()
        {
            Dictionary<int, List<HitomiMetadata>> sorted_gallery_number = new Dictionary<int, List<HitomiMetadata>>();

            foreach (var metadata in HitomiData.Instance.metadata_collection)
                if (sorted_gallery_number.ContainsKey(metadata.ID / interval * interval))
                    sorted_gallery_number[metadata.ID / interval * interval].Add(metadata);
                else
                    sorted_gallery_number.Add(metadata.ID / interval * interval, new List<HitomiMetadata>() { metadata });

            datas = sorted_gallery_number.ToList();
            datas.Sort((p1, p2) => p1.Key.CompareTo(p2.Key));

            UpdateGalleryVariation();
        }

        public void UpdateGalleryVariation()
        {
            samples.Clear();
            HitomiAnalysisTrendElement e = new HitomiAnalysisTrendElement();
            e.name = "업로드 변동";
            e.points = new List<Point>();
            
            foreach (var data in datas)
                e.points.Add(new Point(data.Key, data.Value.Count));

            samples.Add(e);
        }

        public void UpdataGalleryIncrements()
        {
            samples.Clear();
            HitomiAnalysisTrendElement e = new HitomiAnalysisTrendElement();
            e.name = "업로드 수";
            e.points = new List<Point>();

            int nujuk = 0;
            foreach (var data in datas)
            {
                nujuk += data.Value.Count;
                e.points.Add(new Point(data.Key, nujuk));
            }

            samples.Add(e);
        }

        public void UpdateTagIncrements()
        {
            samples.Clear();

            Dictionary<string, Dictionary<int, int>> tag_list = new Dictionary<string, Dictionary<int, int>>();
            foreach (var metadata in HitomiData.Instance.metadata_collection)
                if (metadata.Tags != null)
                    foreach (var tag in metadata.Tags)
                        if (!tag_list.ContainsKey(tag))
                            tag_list.Add(tag, new Dictionary<int, int>());

            foreach (var data in datas)
                foreach (var metadata in data.Value)
                    if (metadata.Tags != null)
                        foreach (var tag in metadata.Tags)
                            if (tag_list[tag].ContainsKey(data.Key))
                                tag_list[tag][data.Key] += 1;
                            else
                                tag_list[tag].Add(data.Key, 1);
            
            foreach (var tag in tag_list)
            {
                HitomiAnalysisTrendElement e = new HitomiAnalysisTrendElement();
                e.name = tag.Key;
                e.points = new List<Point>();
                int nujuk = 0;

                foreach (var pair in tag.Value)
                {
                    nujuk += pair.Value;
                    e.points.Add(new Point(pair.Key, nujuk));
                }
                samples.Add(e);
            }

            samples.Sort((a, b) => b.points.Last().Y.CompareTo(a.points.Last().Y));
            samples.RemoveRange(30, samples.Count - 30);
        }

        public void UpdateTagKoreanIncrements()
        {
            samples.Clear();

            Dictionary<string, Dictionary<int, int>> tag_list = new Dictionary<string, Dictionary<int, int>>();
            foreach (var metadata in HitomiData.Instance.metadata_collection)
                if (metadata.Language == "korean" && metadata.Tags != null)
                    foreach (var tag in metadata.Tags)
                        if (!tag_list.ContainsKey(tag))
                            tag_list.Add(tag, new Dictionary<int, int>());

            foreach (var data in datas)
                foreach (var metadata in data.Value)
                    if (metadata.Language == "korean" && metadata.Tags != null)
                        foreach (var tag in metadata.Tags)
                            if (tag_list[tag].ContainsKey(data.Key))
                                tag_list[tag][data.Key] += 1;
                            else
                                tag_list[tag].Add(data.Key, 1);

            foreach (var tag in tag_list)
            {
                HitomiAnalysisTrendElement e = new HitomiAnalysisTrendElement();
                e.name = tag.Key;
                e.points = new List<Point>();
                int nujuk = 0;

                foreach (var pair in tag.Value)
                {
                    nujuk += pair.Value;
                    e.points.Add(new Point(pair.Key, nujuk));
                }
                samples.Add(e);
            }

            samples.Sort((a, b) => b.points.Last().Y.CompareTo(a.points.Last().Y));
            samples.RemoveRange(30, samples.Count - 30);
        }


        public void UpdateArtistsIncremetns(bool specifictag = false, string tag = "")
        {
            samples.Clear();

            Dictionary<string, Dictionary<int, int>> artist_list = new Dictionary<string, Dictionary<int, int>>();
            foreach (var metadata in HitomiData.Instance.metadata_collection)
                if (metadata.Artists != null && (!specifictag || (metadata.Tags != null && metadata.Tags.Contains(tag))))
                    foreach (var artist in metadata.Artists)
                        if (!artist_list.ContainsKey(artist))
                            artist_list.Add(artist, new Dictionary<int, int>());

            foreach (var data in datas)
                foreach (var metadata in data.Value)
                    if (metadata.Artists != null && (!specifictag || (metadata.Tags != null && metadata.Tags.Contains(tag))))
                        foreach (var artist in metadata.Artists)
                            if (artist_list[artist].ContainsKey(data.Key))
                                artist_list[artist][data.Key] += 1;
                            else
                                artist_list[artist].Add(data.Key, 1);

            foreach (var artist in artist_list)
            {
                HitomiAnalysisTrendElement e = new HitomiAnalysisTrendElement();
                e.name = artist.Key;
                e.points = new List<Point>();
                int nujuk = 0;

                foreach (var pair in artist.Value)
                {
                    nujuk += pair.Value;
                    e.points.Add(new Point(pair.Key, nujuk));
                }
                samples.Add(e);
            }

            samples.Sort((a, b) => b.points.Last().Y.CompareTo(a.points.Last().Y));
            if (samples.Count > 100) samples.RemoveRange(100, samples.Count - 100);
        }

        public void UpdateArtistsKoreanIncremetns(bool specifictag = false, string tag = "")
        {
            samples.Clear();

            Dictionary<string, Dictionary<int, int>> artist_list = new Dictionary<string, Dictionary<int, int>>();
            foreach (var metadata in HitomiData.Instance.metadata_collection)
                if (metadata.Language == "korean" && metadata.Artists != null && (!specifictag || (metadata.Tags != null && metadata.Tags.Contains(tag))))
                    foreach (var artist in metadata.Artists)
                        if (!artist_list.ContainsKey(artist))
                            artist_list.Add(artist, new Dictionary<int, int>());

            foreach (var data in datas)
                foreach (var metadata in data.Value)
                    if (metadata.Language == "korean" && metadata.Artists != null && (!specifictag || (metadata.Tags != null && metadata.Tags.Contains(tag))))
                        foreach (var artist in metadata.Artists)
                            if (artist_list[artist].ContainsKey(data.Key))
                                artist_list[artist][data.Key] += 1;
                            else
                                artist_list[artist].Add(data.Key, 1);

            foreach (var artist in artist_list)
            {
                HitomiAnalysisTrendElement e = new HitomiAnalysisTrendElement();
                e.name = artist.Key;
                e.points = new List<Point>();
                int nujuk = 0;

                foreach (var pair in artist.Value)
                {
                    nujuk += pair.Value;
                    e.points.Add(new Point(pair.Key, nujuk));
                }
                samples.Add(e);
            }

            samples.Sort((a, b) => b.points.Last().Y.CompareTo(a.points.Last().Y));
            if (samples.Count > 100) samples.RemoveRange(100, samples.Count - 100);
        }

    }
}
