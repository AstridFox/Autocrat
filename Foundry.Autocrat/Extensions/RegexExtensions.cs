using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Extensions.Regex
{
    public static class RegexExtensions
    {
        public static bool AnyMatchesFor(this List<System.Text.RegularExpressions.Regex> rList, string text)
        {
            foreach (var rx in rList)
            {
                if (rx.IsMatch(text)) return true;
            }

            return false;
        }
    }
}
