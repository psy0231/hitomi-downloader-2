using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust_Hitomi_Copy_Machine
{
    public class HitomiDef
    {
        /*
         * Hitomi Defines
         */
        public const string HitomiAddress = @"https://hitomi.la/";
        public const string HitomiGalleryAddress = @"https://hitomi.la/galleries/";
        public const string HitomiReaderAddress = @"https://hitomi.la/reader/";
        public const string HitomiThumbnail = @"https://tn.hitomi.la/";

        public static string GetDownloadImageAddress(
            string gallery,
            string page_with_extension)
        {
            // download.js
            var number_of_frontends = 2;
            char subdomain = Convert.ToChar(97 + (Convert.ToInt32(gallery.Last()) % number_of_frontends));
            if (gallery.Last() == '1')
                subdomain = 'a';
            return $"https://{subdomain}a.hitomi.la/galleries/{gallery}/{page_with_extension}";
        }
    }

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
