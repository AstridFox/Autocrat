using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Extensions.Math {
	public static class MathExtensions {
		public static int Wrap(this int i, int value, bool allowNegative) {
			int n = i % value;
			if (!allowNegative && n < 0) n += value;
			return n;
		}
		public static long Wrap(this long i, short value, bool allowNegative) {
			long n = (long)(i % value);
			if (!allowNegative && n < 0) n += value;
			return n;
		}
		public static short Wrap(this short i, short value, bool allowNegative) {
			short n = (short)(i % value);
			if (!allowNegative && n < 0) n += value;
			return n;
		}
		public static byte Wrap(this byte i, byte value) {
			byte n = (byte)(i % value);
			return n;
		}
		public static float Wrap(this float i, float value, bool allowNegative) {
			float n = (float)(i % value);
			if (!allowNegative && n < 0) n += value;
			return n;
		}
		public static double Wrap(this double i, double value, bool allowNegative) {
			double n = (double)(i % value);
			if (!allowNegative && n < 0) n += value;
			return n;
		}
		public static decimal Wrap(this decimal i, decimal value, bool allowNegative) {
			decimal n = (decimal)(i % value);
			if (!allowNegative && n < 0) n += value;
			return n;
		}

	}
}
