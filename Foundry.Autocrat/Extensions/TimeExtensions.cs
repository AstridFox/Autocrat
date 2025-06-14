using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Extensions.Fluent;

namespace Foundry.Autocrat.Extensions.Time
{
    public static class TimeExtensions
    {
        public static TimeSpan Milliseconds(this int ms)
        {
            return TimeSpan.FromMilliseconds(ms);
        }

        public static TimeSpan Seconds(this int s)
        {
            return TimeSpan.FromSeconds(s);
        }

        public static TimeSpan Minutes(this int m)
        {
            return TimeSpan.FromMinutes(m);
        }

        public static TimeSpan Hours(this int h)
        {
            return TimeSpan.FromHours(h);
        }

        public static string ToPrettyString(this TimeSpan ts)
        {
            bool ms = ts.Milliseconds != 0;
            bool s = ts.Seconds != 0;
            bool m = ts.Minutes != 0;
            bool h = ts.Hours != 0;
            bool d = ts.Days != 0;

            if (ms) return ts.TotalMilliseconds.ToString() + "ms";
            if (s) return ts.TotalSeconds.ToString() + "s";
            if (m) return ts.TotalMinutes.ToString() + "m";
            if (h) return ts.TotalHours.ToString() + "h";
            if (d) return ts.TotalDays.ToString() + " days";

            return "0s";
        }

        public static TimeSpan ParsePrettyString(string s)
        {
            int firstLetterIndex = s.ToCharArray().ToList().FindIndex(c => Char.IsLetter(c));
            string num = s.Substring(0, firstLetterIndex);
            string lt = s.Substring(firstLetterIndex);

            switch (lt)
            {
                case "ms": return TimeSpan.FromMilliseconds(int.Parse(num));
                case "s": return TimeSpan.FromSeconds(int.Parse(num));
                case "m": return TimeSpan.FromMinutes(int.Parse(num));
                case "h": return TimeSpan.FromHours(int.Parse(num));
                case " days": return TimeSpan.FromDays(int.Parse(num));
            }

            return default(TimeSpan);
        }

        public static bool TryParsePrettyString(string s, out TimeSpan ts)
        {
            ts = default(TimeSpan);

            if (s == "") return false;
            int firstLetterIndex = s.ToCharArray().ToList().FindIndex(c => Char.IsLetter(c));
            if (firstLetterIndex == -1) return false;

            string num = s.Substring(0, firstLetterIndex);
            string lt = s.Substring(firstLetterIndex);
            int n;

            if (!int.TryParse(num, out n)) return false;
            if (lt != "ms" && lt != "s" && lt != "m" && lt != "h" && lt != " days") return false;

            try
            {
                ts = ParsePrettyString(s);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
