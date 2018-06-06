/* Copyright (C) 2018. Hitomi Parser Developers */

using Hitomi_Copy_2;
using System;

namespace Hitomi_Copy_3.Analysis
{
    public class HitomiAnalysisDateTime
    {
        private static readonly Lazy<HitomiAnalysisDateTime> instance = new Lazy<HitomiAnalysisDateTime>(() => new HitomiAnalysisDateTime());
        public static HitomiAnalysisDateTime Instance => instance.Value;

        public int[] week_count = new int[7];
        public int[] time_count = new int[24];

        public HitomiAnalysisDateTime()
        {
            foreach (var data in HitomiLog.Instance.GetEnumerator())
            {
                week_count[(int)data.Time.DayOfWeek]++;
                time_count[data.Time.TimeOfDay.Hours]++;
            }
        }
    }
}
