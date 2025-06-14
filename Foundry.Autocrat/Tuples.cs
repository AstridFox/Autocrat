/*  Tuples.cs
 *  Erik Forbes
 *  http://shadowcoding.blogspot.com */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundry.Autocrat {
	public struct Tuple<T1, T2> {
		private readonly T1 _value1; public T1 Value1 { get { return _value1; } }
		private readonly T2 _value2; public T2 Value2 { get { return _value2; } }
		public Tuple(T1 value1, T2 value2) { _value1 = value1; _value2 = value2; }

		public override bool Equals(object obj) {
			if (!(obj is Tuple<T1, T2>)) return false;
			if (obj == null) return false;

			Tuple<T1, T2> t = (Tuple<T1, T2>)obj;
			return (Value1.Equals(t.Value1) && Value2.Equals(t.Value2));
		}

		public override int GetHashCode() {
			return Value1.GetHashCode() ^ Value2.GetHashCode();
		}

		public KeyValuePair<T1, T2> AsKeyValuePair() {
			return new KeyValuePair<T1, T2>(Value1, Value2);
		}

		public string FormatWith(string template) {
			return String.Format(template, Value1, Value2);
		}
	}
	public struct Tuple<T1, T2, T3> {
		private readonly T1 _value1; public T1 Value1 { get { return _value1; } }
		private readonly T2 _value2; public T2 Value2 { get { return _value2; } }
		private readonly T3 _value3; public T3 Value3 { get { return _value3; } }
		public Tuple(T1 value1, T2 value2, T3 value3) { _value1 = value1; _value2 = value2; _value3 = value3; }

		public override bool Equals(object obj) {
			if (!(obj is Tuple<T1, T2, T3>)) return false;
			if (obj == null) return false;

			Tuple<T1, T2, T3> t = (Tuple<T1, T2, T3>)obj;
			return (Value1.Equals(t.Value1) && Value2.Equals(t.Value2) && Value3.Equals(t.Value3));
		}

		public override int GetHashCode() {
			return Value1.GetHashCode() ^ Value2.GetHashCode() ^ Value3.GetHashCode();
		}

		public string FormatWith(string template) {
			return String.Format(template, Value1, Value2, Value3);
		}

	}
	public struct Tuple<T1, T2, T3, T4> {
		private readonly T1 _value1; public T1 Value1 { get { return _value1; } }
		private readonly T2 _value2; public T2 Value2 { get { return _value2; } }
		private readonly T3 _value3; public T3 Value3 { get { return _value3; } }
		private readonly T4 _value4; public T4 Value4 { get { return _value4; } }
		public Tuple(T1 value1, T2 value2, T3 value3, T4 value4) { _value1 = value1; _value2 = value2; _value3 = value3; _value4 = value4; }

		public override bool Equals(object obj) {
			if (!(obj is Tuple<T1, T2, T3, T4>)) return false;
			if (obj == null) return false;

			Tuple<T1, T2, T3, T4> t = (Tuple<T1, T2, T3, T4>)obj;
			return (Value1.Equals(t.Value1) && Value2.Equals(t.Value2) && Value3.Equals(t.Value3) && Value4.Equals(t.Value4));
		}

		public override int GetHashCode() {
			return Value1.GetHashCode() ^ Value2.GetHashCode() ^ Value3.GetHashCode() ^ Value4.GetHashCode();
		}

		public string FormatWith(string template) {
			return String.Format(template, Value1, Value2, Value3, Value4);
		}
	}
	public struct Tuple<T1, T2, T3, T4, T5> {
		private readonly T1 _value1; public T1 Value1 { get { return _value1; } }
		private readonly T2 _value2; public T2 Value2 { get { return _value2; } }
		private readonly T3 _value3; public T3 Value3 { get { return _value3; } }
		private readonly T4 _value4; public T4 Value4 { get { return _value4; } }
		private readonly T5 _value5; public T5 Value5 { get { return _value5; } }
		public Tuple(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5) { _value1 = value1; _value2 = value2; _value3 = value3; _value4 = value4; _value5 = value5; }

		public override bool Equals(object obj) {
			if (!(obj is Tuple<T1, T2, T3, T4, T5>)) return false;
			if (obj == null) return false;

			Tuple<T1, T2, T3, T4, T5> t = (Tuple<T1, T2, T3, T4, T5>)obj;
			return (Value1.Equals(t.Value1) && Value2.Equals(t.Value2) && Value3.Equals(t.Value3) && Value4.Equals(t.Value4) && Value5.Equals(t.Value5));
		}

		public override int GetHashCode() {
			return Value1.GetHashCode() ^ Value2.GetHashCode() ^ Value3.GetHashCode() ^ Value4.GetHashCode() ^ Value5.GetHashCode();
		}

		public string FormatWith(string template) {
			return String.Format(template, Value1, Value2, Value3, Value4, Value5);
		}
	}

	public static class Tuples {
		public static Tuple<T1, T2> Tuple<T1, T2>(T1 value1, T2 value2) {
			return new Tuple<T1, T2>(value1, value2);
		}
		public static Tuple<T1, T2, T3> Tuple<T1, T2, T3>(T1 value1, T2 value2, T3 value3) {
			return new Tuple<T1, T2, T3>(value1, value2, value3);
		}
		public static Tuple<T1, T2, T3, T4> Tuple<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4) {
			return new Tuple<T1, T2, T3, T4>(value1, value2, value3, value4);
		}
		public static Tuple<T1, T2, T3, T4, T5> Tuple<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5) {
			return new Tuple<T1, T2, T3, T4, T5>(value1, value2, value3, value4, value5);
		}

		public static Tuple<T1, T2> Default<T1, T2>() {
			return new Tuple<T1, T2>(default(T1), default(T2));
		}
		public static Tuple<T1, T2, T3> Default<T1, T2, T3>() {
			return new Tuple<T1, T2, T3>(default(T1), default(T2), default(T3));
		}
		public static Tuple<T1, T2, T3, T4> Default<T1, T2, T3, T4>() {
			return new Tuple<T1, T2, T3, T4>(default(T1), default(T2), default(T3), default(T4));
		}
		public static Tuple<T1, T2, T3, T4, T5> Default<T1, T2, T3, T4, T5>() {
			return new Tuple<T1, T2, T3, T4, T5>(default(T1), default(T2), default(T3), default(T4), default(T5));
		}

		public static IEnumerable<Tuple<T1, T2>> Zip<T1, T2>(IEnumerable<T1> first, IEnumerable<T2> second) {
			var enum1 = first.GetEnumerator();
			var enum2 = second.GetEnumerator();

			while (enum1.MoveNext() && enum2.MoveNext()) {
				yield return Tuple(enum1.Current, enum2.Current);
			}
		}
		public static IEnumerable<Tuple<T1, T2, T3>> Zip<T1, T2, T3>(IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third) {
			var enum1 = first.GetEnumerator();
			var enum2 = second.GetEnumerator();
			var enum3 = third.GetEnumerator();

			while (enum1.MoveNext() && enum2.MoveNext() && enum3.MoveNext()) {
				yield return Tuple(enum1.Current, enum2.Current, enum3.Current);
			}
		}
		public static IEnumerable<Tuple<T1, T2, T3, T4>> Zip<T1, T2, T3, T4>(IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, IEnumerable<T4> forth) {
			var enum1 = first.GetEnumerator();
			var enum2 = second.GetEnumerator();
			var enum3 = third.GetEnumerator();
			var enum4 = forth.GetEnumerator();

			while (enum1.MoveNext() && enum2.MoveNext() && enum3.MoveNext() && enum4.MoveNext()) {
				yield return Tuple(enum1.Current, enum2.Current, enum3.Current, enum4.Current);
			}
		}
		public static IEnumerable<Tuple<T1, T2, T3, T4, T5>> Zip<T1, T2, T3, T4, T5>(IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, IEnumerable<T4> forth, IEnumerable<T5> fifth) {
			var enum1 = first.GetEnumerator();
			var enum2 = second.GetEnumerator();
			var enum3 = third.GetEnumerator();
			var enum4 = forth.GetEnumerator();
			var enum5 = fifth.GetEnumerator();

			while (enum1.MoveNext() && enum2.MoveNext() && enum3.MoveNext() && enum4.MoveNext() && enum5.MoveNext()) {
				yield return Tuple(enum1.Current, enum2.Current, enum3.Current, enum4.Current, enum5.Current);
			}
		}

		public static Tuple<IEnumerable<T1>, IEnumerable<T2>> Unzip<T1, T2>(this IEnumerable<Tuple<T1, T2>> ienum) {
			var first = new List<T1>();
			var second = new List<T2>();

			foreach (var t in ienum) {
				first.Add(t.Value1);
				second.Add(t.Value2);
			}

			return Tuples.Tuple(first.AsEnumerable(), second.AsEnumerable());
		}
		public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> Unzip<T1, T2, T3>(this IEnumerable<Tuple<T1, T2, T3>> ienum) {
			var first = new List<T1>();
			var second = new List<T2>();
			var third = new List<T3>();

			foreach (var t in ienum) {
				first.Add(t.Value1);
				second.Add(t.Value2);
				third.Add(t.Value3);
			}

			return Tuples.Tuple(first.AsEnumerable(), second.AsEnumerable(), third.AsEnumerable());
		}
		public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> Unzip<T1, T2, T3, T4>(this IEnumerable<Tuple<T1, T2, T3, T4>> ienum) {
			var first = new List<T1>();
			var second = new List<T2>();
			var third = new List<T3>();
			var forth = new List<T4>();

			foreach (var t in ienum) {
				first.Add(t.Value1);
				second.Add(t.Value2);
				third.Add(t.Value3);
				forth.Add(t.Value4);
			}

			return Tuples.Tuple(first.AsEnumerable(), second.AsEnumerable(), third.AsEnumerable(), forth.AsEnumerable());
		}
		public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> Unzip<T1, T2, T3, T4, T5>(this IEnumerable<Tuple<T1, T2, T3, T4, T5>> ienum) {
			var first = new List<T1>();
			var second = new List<T2>();
			var third = new List<T3>();
			var forth = new List<T4>();
			var fifth = new List<T5>();

			foreach (var t in ienum) {
				first.Add(t.Value1);
				second.Add(t.Value2);
				third.Add(t.Value3);
				forth.Add(t.Value4);
				fifth.Add(t.Value5);
			}

			return Tuples.Tuple(first.AsEnumerable(), second.AsEnumerable(), third.AsEnumerable(), forth.AsEnumerable(), fifth.AsEnumerable());
		}

		public static Tuple<T1, T2> AsTuple<T1, T2>(this KeyValuePair<T1, T2> kvp) {
			return Tuples.Tuple(kvp.Key, kvp.Value);
		}
	}
}