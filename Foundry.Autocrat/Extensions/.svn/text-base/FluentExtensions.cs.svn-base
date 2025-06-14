using System;
using System.Collections.Generic;

namespace Foundry.Autocrat.Extensions.Fluent {
	public static class FluentExtensions {
		public static void Times(this int times, Action action) {
			for (int i = 0; i < times; i++) action();
		}
		public static void Times(this int times, Action<int> action) {
			for (int i = 0; i < times; i++) action(i);
		}
		public static void Times(this long times, Action action) {
			for (long i = 0; i < times; i++) action();
		}
		public static void Times(this long times, Action<long> action) {
			for (long i = 0; i < times; i++) action(i);
		}

		public static void ForEach<T>(this IEnumerable<T> ienum, Action<T> action) {
			foreach (var i in ienum) action(i);
		}

        public static bool IsGreaterThan<T>(this IComparable<T> comp, T other)
        {
            return comp.CompareTo(other) > 0;
        }

        public static bool Equals<T>(this IComparable<T> comp, T other)
        {
            return comp.CompareTo(other) == 0;
        }

        public static bool IsLessThan<T>(this IComparable<T> comp, T other)
        {
            return comp.CompareTo(other) < 0;
        }

        public static bool IsGreaterThanOrEqualTo<T>(this IComparable<T> comp, T other)
        {
            return comp.IsGreaterThan(other) || comp.Equals(other);
        }

        public static bool IsLessThanOrEqualTo<T>(this IComparable<T> comp, T other)
        {
            return comp.IsLessThan(other) || comp.Equals(other);
        }

		public static T As<T>(this object o) {
			return (T)o;
		}
		public static T AsOrDefault<T>(this object o) {
			if (!(o is T)) return default(T);
			return o.As<T>();
		}
		public static T AsOrDefault<T>(this object o, T def) {
			if (!(o is T)) return def;
			return o.As<T>();
		}
	}
}
