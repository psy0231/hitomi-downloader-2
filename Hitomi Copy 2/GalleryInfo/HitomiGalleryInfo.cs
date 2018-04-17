/* Copyright (C) 2018. Hitomi Parser Developers */

using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Hitomi_Copy_2.GalleryInfo
{
    public class HitomiGalleryInfo
    {
        public static List<string> GetImageLink(string json)
        {
            JArray arr = JArray.Parse(json.Substring(json.IndexOf('[')));
            List<string> result = new List<string>();
            foreach (var obj in arr)
                result.Add(obj.Value<string>("name"));
            return result;
        }
    }
}
