/* Copyright (C) 2018. Hitomi Parser Developers */

using Newtonsoft.Json;

namespace Hitomi_Copy_2.GalleryInfo
{
    public class HitomiGalleryInfoModel
    {
        [JsonProperty]
        public int width;
        [JsonProperty]
        public string name;
        [JsonProperty]
        public int height;
    }
}
