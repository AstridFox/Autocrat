using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Foundry.Autocrat.Geometry;

namespace Foundry.Autocrat.Extensions.Geometry {
	public static class VectorExtensions {
		public static PointF ToPointF(this Vector v) {
			return new PointF(v.X, v.Y);
		}

		public static Vector ToVector(this PointF p) {
			return new Vector(p.X, p.Y);
		}

		public static Vector ToVector(this Point p) {
			return new Vector(p.X, p.Y);
		}

		public static Tuple<float, float> ToTuple(this Vector v) {
			return Tuples.Tuple(v.X, v.Y);
		}

		public static Vector FromTuple(this Tuple<float, float> t) {
			return new Vector(t.Value1, t.Value2);
		}
	}

	public static class Vector3Extensions {
		public static Tuple<float, float, float> ToTuple(this Vector3 v) {
			return Tuples.Tuple(v.X, v.Y, v.Z);
		}

		public static Vector3 FromTuple(this Tuple<float, float, float> t) {
			return new Vector3(t.Value1, t.Value2, t.Value3);
		}
	}
}
