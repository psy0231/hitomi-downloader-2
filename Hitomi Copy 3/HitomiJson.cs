/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Newtonsoft.Json;
using System.IO;

namespace Hitomi_Copy_2
{
    public class HitomiJsonModel
    {
        [JsonProperty]
        public string Id;
        [JsonProperty]
        public string Title;
        [JsonProperty]
        public string[] Artists;
        [JsonProperty]
        public string[] Series;
        [JsonProperty]
        public string Types;
        [JsonProperty]
        public int Pages;
        [JsonProperty]
        public string[] Tags;
    }

    public class HitomiJson
    {
        string path;
        HitomiJsonModel model;

        public HitomiJson(string path)
        {
            this.path = Path.Combine(path, "Info.json");
            if (File.Exists(this.path)) model = JsonConvert.DeserializeObject<HitomiJsonModel>(File.ReadAllText(this.path));
            if (model == null) model = new HitomiJsonModel();
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            using (var fs = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
            {
                fs.Write(json);
            }
        }

        public void SetModelFromArticle(HitomiArticle article)
        {
            model.Id = article.Magic;
            model.Title = article.Title;
            model.Artists = article.Artists;
            model.Series = article.Series ;
            model.Types = article.Types;
            model.Pages = article.ImagesLink.Count;
            model.Tags = article.Tags;
        }

        public ref HitomiJsonModel GetModel()
        {
            return ref model;
        }
    }
}
