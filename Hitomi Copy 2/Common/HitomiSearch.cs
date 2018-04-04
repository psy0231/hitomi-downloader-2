/* Copyright (C) 2018. Hitomi Parser Developers */

namespace hitomi.Parser
{
    public class HitomiSearch
    {
        public const string HitomiTagPrefix = @"https://hitomi.la/tag/";
        public const string HitomiArtistPrefix = @"https://hitomi.la/artist/";
        public const string HitomiTypePrefix = @"https://hitomi.la/type/";
        public const string HitomiSeriesPrefix = @"https://hitomi.la/series/";
        public const string HitomiCharacterPrefix = @"https://hitomi.la/character/";
        public const string HitomiGroupPrefix = @"https://hitomi.la/group/";

        public const string HitomiTagFemale = @"female:";
        public const string HitomiTagMale = @"male:";

        static public string GetWithTags(string tag, string lang = "korean", string page = "1", bool female = true)
        {
            if (female)
                return $"{HitomiTagPrefix}{HitomiTagFemale}{tag}-{lang}-{page}.html";
            else
                return $"{HitomiTagPrefix}{HitomiTagMale}{tag}-{lang}-{page}.html";
        }

        static public string GetWithArtist(string artist, string lang = "korean", string page = "1")
        {
            return $"{HitomiArtistPrefix}{artist}-{lang}-{page}.html";
        }

        static public string GetWithType(string type, string lang = "korean", string page = "1")
        {
            return $"{HitomiTypePrefix}{type}-{lang}-{page}.html";
        }

        static public string GetWithSeries(string series, string lang = "korean", string page = "1")
        {
            return $"{HitomiSeriesPrefix}{series}-{lang}-{page}.html";
        }

        static public string GetWithLanguage(string lang, string page = "1")
        {
            return $"{HitomiDef.HitomiAddress}index-{lang}-{page}.html";
        }

        static public string GetWithCharacter(string character, string lang = "korean", string page = "1")
        {
            return $"{HitomiCharacterPrefix}{character}-{lang}-{page}.html";
        }

        static public string GetWithGroup(string group, string lang = "korean", string page = "1")
        {
            return $"{HitomiGroupPrefix}{group}-{lang}-{page}.html";
        }

        static public string GetWithKorean(string page)
        {
            return GetWithLanguage("korean", page);
        }

    }
}
