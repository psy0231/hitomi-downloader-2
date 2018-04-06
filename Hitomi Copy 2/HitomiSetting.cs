/* Copyright (C) 2018. Hitomi Parser Developers */

using Newtonsoft.Json;
using System;
using System.IO;

namespace Hitomi_Copy_2
{
    public class HitomiSettingModel
    {
        [JsonProperty]
        public string Path;
        [JsonProperty]
        public string[] ExclusiveTag;
        [JsonProperty]
        public bool Zip;
    }

    public class HitomiSetting
    {
        private static readonly Lazy<HitomiSetting> instance = new Lazy<HitomiSetting>(() => new HitomiSetting());
        public static HitomiSetting Instance => instance.Value;
        string log_path = $"{Environment.CurrentDirectory}\\setting.json";

        HitomiSettingModel model;

        public HitomiSetting()
        {
            if (File.Exists(log_path)) model = JsonConvert.DeserializeObject<HitomiSettingModel>(File.ReadAllText(log_path));
            if (model == null)
            {
                model = new HitomiSettingModel();
                model.Path = @"C:\Hitomi\{Artists}\[{Id}] {Title}\";
                model.ExclusiveTag = new string[] { "female:mother", "male:anal" };
                model.Zip = false;
                Save();
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            using (var fs = new StreamWriter(new FileStream(log_path, FileMode.Create, FileAccess.Write)))
            {
                fs.Write(json);
            }
        }
        
        public ref HitomiSettingModel GetModel()
        {
            return ref model;
        }
        
    }
}
