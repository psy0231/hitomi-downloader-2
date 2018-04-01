/* Copyright (C) 2018. Hitomi Parser Developers */

namespace hitomi.Parser
{
    class HiyobiDef
    {
        /*
         * Hiyobi Defines
         */
        public static string HiyobiAddress = @"https://hiyobi.me/";
        public static string HiyobiInfo = @"https://hiyobi.me/info/";
        public static string HiyobiGalleryAddress = @"https://hiyobi.me/reader/";
        public static string HiyobiReaderAddress = @"https://hiyobi.me/reader/";
        public static string HiyobiImage1 = @"https://aa.hiyobi.me/data/";

        public static string GetHiyobiDownloadImageAddress(
            string gallery,
            string page_with_extension)
        { return $"{HiyobiImage1}{gallery}/{page_with_extension}"; }

    }
}
