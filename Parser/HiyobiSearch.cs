/* Copyright (C) 2018. Hitomi Parser Developers */

namespace hitomi.Parser
{
    public class HiyobiSearch
    {
        public const string HiyomiSearchPrefix = @"https://hiyobi.me/search/";

        static public string GetWithTags(string tag, string page = "1", bool female = true)
        {
            if (female)
                return $"{HiyomiSearchPrefix}female:{tag}/{page}";
            else
                return $"{HiyomiSearchPrefix}male:{tag}/{page}";
        }

        static public string GetWithArtist(string artist, string page = "1")
        {
            return $"{HiyomiSearchPrefix}artist:{artist}/{page}";
        }

        static public string GetWithType(string type, string page = "1")
        {
            return $"{HiyomiSearchPrefix}type:{type}/{page}";
        }

        static public string GetWithSeries(string series, string page = "1")
        {
            return $"{HiyomiSearchPrefix}series:{series}/{page}";
        }

        static public string GetWithLanguage(string search, string page = "1")
        {
            if (search == "")
                return $"{HiyobiDef.HiyobiAddress}list/{page}";
            else
                return $"{HiyobiDef.HiyobiAddress}{search}/{page}";
        }
        
        static public string GetWithKorean(string search, string page = "1")
        {
            return GetWithLanguage(search, page);
        }
    }
}
