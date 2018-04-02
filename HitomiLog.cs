/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hitomi_Copy
{
    public class HitomiTagModel
    {
        [JsonProperty]
        public string Title;
        [JsonProperty]
        public string Artists;
        [JsonProperty]
        public List<string> Tags;
    }

    public class HitomiLog
    {
        private static readonly Lazy<HitomiLog> instance = new Lazy<HitomiLog>(()=>new HitomiLog());
        public static HitomiLog Instance => instance.Value;
        string log_path = $"{Environment.CurrentDirectory}\\log.json";
        
        List<HitomiTagModel> model;

        public HitomiLog()
        {
            if (File.Exists(log_path)) model = JsonConvert.DeserializeObject<List<HitomiTagModel>>(File.ReadAllText(log_path));
            if (model == null) model = new List<HitomiTagModel>();
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            using (var fs = new StreamWriter(new FileStream(log_path, FileMode.Create, FileAccess.Write)))
            {
                fs.Write(json);
            }
        }

        public void AddArticle(HitomiArticle article)
        {
            HitomiTagModel mm = new HitomiTagModel();
            mm.Title = article.Title;
            mm.Artists = article.Artists;
            mm.Tags = article.Tags;
            model.Add(mm);
        }

        public IEnumerable<HitomiTagModel> GetEnumerator()
        {
            return model;
        }
    }
}
