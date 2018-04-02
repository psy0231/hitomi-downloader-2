/* Copyright (C) 2018. Hitomi Parser Developers */

namespace Hitomi_Copy
{
    public class HitomiLegalize
    {
        public static string LegalizeTag(string tag)
        {
            if (tag.Trim().EndsWith("♀")) return "female:" + tag.Trim('♀').Trim();
            if (tag.Trim().EndsWith("♂")) return "male:" + tag.Trim('♂').Trim();
            return tag.Trim();
        }
    }
}
