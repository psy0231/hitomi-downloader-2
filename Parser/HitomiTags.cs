/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Collections.Generic;

namespace hitomi.Parser
{
    public class HitomiArtists : HitomiSearchElement
    {
        public HitomiArtists(List<Tuple<string, int>> list)
            : base(list)
        {
        }
    }
    public class HitomiCharacters : HitomiSearchElement
    {
        public HitomiCharacters(List<Tuple<string, int>> list)
            : base(list)
        {
        }
    }
    public class HitomiTags : HitomiSearchElement
    {
        public HitomiTags(List<Tuple<string, int>> list)
            : base(list)
        {
        }
    }
    public class HitomiGroup : HitomiSearchElement
    {
        public HitomiGroup(List<Tuple<string, int>> list)
            : base(list)
        {
        }
    }
    public class HitomiLanguage : HitomiSearchElement
    {
        public HitomiLanguage(List<Tuple<string, int>> list)
            : base(list)
        {
        }
    }
    public class HitomiSeries : HitomiSearchElement
    {
        public HitomiSeries(List<Tuple<string, int>> list)
            : base(list)
        {
        }
    }
    public class HitomiType : HitomiSearchElement
    {
        public HitomiType(List<Tuple<string, int>> list)
            : base(list)
        {
        }
    }
}
