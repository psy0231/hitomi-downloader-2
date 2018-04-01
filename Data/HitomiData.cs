/* Copyright (C) 2018. Hitomi Parser Developers */

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        public static int max_number_of_results = 10;
        public static int number_of_gallery_jsons = 20;

        public static string tag_json_uri = @"https://ltn.hitomi.la/tags.json";
        public static string gllerie_json_uri(int no)=> $"https://ltn.hitomi.la/galleries{no}.json";

        public HitomiTagdataCollection tag_collection;
        public List<HitomiMetadata> metadata_collection;
        
        public async Task DownloadTagJson()
        {
            HttpClient client = new HttpClient();
            var data = await client.GetStringAsync(tag_json_uri);
            tag_collection = JsonConvert.DeserializeObject<HitomiTagdataCollection>(data);

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "tagdata.json")))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, tag_collection);
            }
        }

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

        public void LoadTagJson()
        {
            if (File.Exists(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "tagdata.json")))
                tag_collection = JsonConvert.DeserializeObject<HitomiTagdataCollection>(File.ReadAllText(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "tagdata.json")));
        }

        public void LoadMetadataJson()
        {
            if (File.Exists(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "metadata.json")))
                metadata_collection = JsonConvert.DeserializeObject<List<HitomiMetadata>>(File.ReadAllText(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "metadata.json")));
        }
    }
}
