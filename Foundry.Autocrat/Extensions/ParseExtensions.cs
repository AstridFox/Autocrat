using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Extensions.Parse {
	public static class ParseExtensions {
		public static int? TryParseInt(this string s) {
			int o;
			if (int.TryParse(s, out o)) return o;
			return null;
		}
		public static float? TryParseFloat(this string s) {
			float o;
			if (float.TryParse(s, out o)) return o;
			return null;
		}
		public static long? TryParseLong(this string s) {
			long o;
			if (long.TryParse(s, out o)) return o;
			return null;
		}
		public static double? TryParseDouble(this string s) {
			double o;
			if (double.TryParse(s, out o)) return o;
			return null;
		}
		public static decimal? TryParseDecimal(this string s) {
			decimal o;
			if (decimal.TryParse(s, out o)) return o;
			return null;
		}
	}
}
