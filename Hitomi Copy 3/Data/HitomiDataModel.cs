/* Copyright (C) 2018. Hitomi Parser Developers */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hitomi_Copy.Data
{
    public struct HitomiTagdata
    {
        [JsonProperty(PropertyName = "s")]
        public string Tag { get; set; }
        [JsonProperty(PropertyName = "t")]
        public int Count { get; set; }
    }

    public struct HitomiTagdataCollection
    {
        [JsonProperty(PropertyName = "language")]
        public List<HitomiTagdata> language { get; set; }
        [JsonProperty(PropertyName = "female")]
        public List<HitomiTagdata> female { get; set; }
        [JsonProperty(PropertyName = "series")]
        public List<HitomiTagdata> series { get; set; }
        [JsonProperty(PropertyName = "character")]
        public List<HitomiTagdata> character { get; set; }
        [JsonProperty(PropertyName = "artist")]
        public List<HitomiTagdata> artist { get; set; }
        [JsonProperty(PropertyName = "group")]
        public List<HitomiTagdata> group { get; set; }
        [JsonProperty(PropertyName = "tag")]
        public List<HitomiTagdata> tag { get; set; }
        [JsonProperty(PropertyName = "male")]
        public List<HitomiTagdata> male { get; set; }
    }

    public struct HitomiMetadata
    {
        [JsonProperty(PropertyName = "a")]
        public string[] Artists { get; set; }
        [JsonProperty(PropertyName = "g")]
        public string[] Groups { get; set; }
        [JsonProperty(PropertyName = "p")]
        public string[] Parodies { get; set; }
        [JsonProperty(PropertyName = "t")]
        public string[] Tags { get; set; }
        [JsonProperty(PropertyName = "c")]
        public string[] Characters { get; set; }
        [JsonProperty(PropertyName = "l")]
        public string Language { get; set; }
        [JsonProperty(PropertyName = "n")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
    }
}
