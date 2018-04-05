/* Copyright (C) 2018. Hitomi Parser Developers */

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hitomi_Copy.Data
{
    public class HitomiData
    {
        private static readonly Lazy<HitomiData> instance = new Lazy<HitomiData>(() => new HitomiData());
        public static HitomiData Instance => instance.Value;

        public static int max_number_of_results = 10;
        public static int number_of_gallery_jsons = 20;
        
        public static string gllerie_json_uri(int no)=> $"https://ltn.hitomi.la/galleries{no}.json";
        
        public List<HitomiMetadata> metadata_collection;
        
        public async Task DownloadMetadata()
        {
            metadata_collection = new List<HitomiMetadata>();
            await Task.WhenAll(Enumerable.Range(0, number_of_gallery_jsons).Select(no => downloadMetadata(no)));

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "metadata.json")))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, metadata_collection);
            }
        }

        private async Task downloadMetadata(int no)
        {
            HttpClient client = new HttpClient();
            var data = await client.GetStringAsync(gllerie_json_uri(no));
            lock (metadata_collection)
            metadata_collection.AddRange(JsonConvert.DeserializeObject<IEnumerable<HitomiMetadata>>(data));
        }
        
        public void LoadMetadataJson()
        {
            if (CheckMetadataExist())
                metadata_collection = JsonConvert.DeserializeObject<List<HitomiMetadata>>(File.ReadAllText(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "metadata.json")));
        }

        public bool CheckMetadataExist()
        {
            return File.Exists(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "metadata.json"));
        }

        public async Task Synchronization()
        {
            metadata_collection.Clear();
            await Task.WhenAll(Enumerable.Range(0, number_of_gallery_jsons).Select(no => downloadMetadata(no)));

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "metadata.json")))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, metadata_collection);
            }
        }

        #region Autocomplete Helper
        public List<string> GetArtistList(string startswith)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var metadata in metadata_collection)
            {
                if (metadata.Artists != null && metadata.Artists[0].ToLower().Replace(' ', '_').StartsWith(startswith.ToLower()) 
                    && !dic.ContainsKey(metadata.Artists[0].ToLower().Replace(' ', '_')))
                    dic.Add(metadata.Artists[0].ToLower().Replace(' ', '_'), 0);
                if (dic.Count > 50) break;
            }
            List<string> result = new List<string>();
            dic.ToList().ForEach((pair) => result.Add(pair.Key));
            result.Sort();
            return result;
        }

        public List<string> GetTagList(string startswith)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var metadata in metadata_collection)
            {
                if (metadata.Tags != null && metadata.Tags[0].ToLower().Replace(' ', '_').StartsWith(startswith.ToLower())
                    && !dic.ContainsKey(metadata.Tags[0].ToLower().Replace(' ', '_')))
                    dic.Add(metadata.Tags[0].ToLower().Replace(' ', '_'), 0);
                if (dic.Count > 50) break;
            }
            List<string> result = new List<string>();
            dic.ToList().ForEach((pair) => result.Add(pair.Key));
            result.Sort();
            return result;
        }

        public List<string> GetGroupList(string startswith)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var metadata in metadata_collection)
            {
                if (metadata.Groups != null && metadata.Groups[0].ToLower().Replace(' ', '_').StartsWith(startswith.ToLower())
                    && !dic.ContainsKey(metadata.Groups[0].ToLower().Replace(' ', '_')))
                    dic.Add(metadata.Groups[0].ToLower().Replace(' ', '_'), 0);
                if (dic.Count > 50) break;
            }
            List<string> result = new List<string>();
            dic.ToList().ForEach((pair) => result.Add(pair.Key));
            result.Sort();
            return result;
        }

        public List<string> GetSeriesList(string startswith)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var metadata in metadata_collection)
            {
                if (metadata.Parodies != null && metadata.Parodies[0].StartsWith(startswith.ToLower().Replace(' ', '_'))
                    && !dic.ContainsKey(metadata.Parodies[0].ToLower().Replace(' ', '_')))
                    dic.Add(metadata.Parodies[0].ToLower().Replace(' ', '_'), 0);
                if (dic.Count > 50) break;
            }
            List<string> result = new List<string>();
            dic.ToList().ForEach((pair) => result.Add(pair.Key));
            result.Sort();
            return result;
        }

        public List<string> GetCharacterList(string startswith)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var metadata in metadata_collection)
            {
                if (metadata.Characters != null && metadata.Characters[0].StartsWith(startswith.ToLower().Replace(' ', '_'))
                    && !dic.ContainsKey(metadata.Characters[0].ToLower().Replace(' ', '_')))
                    dic.Add(metadata.Characters[0].ToLower().Replace(' ', '_'), 0);
                if (dic.Count > 50) break;
            }
            List<string> result = new List<string>();
            dic.ToList().ForEach((pair) => result.Add(pair.Key));
            result.Sort();
            return result;
        }
        #endregion
    }
}
