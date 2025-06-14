using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Foundry.Autocrat.Extensions.Math;

namespace Foundry.Autocrat.Geometry
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Vector3
    {
        private const float PI = 3.1415927F;
        private const float D2RADMUL = 57.295778F;

        public static Vector3 Zero = new Vector3(0, 0, 0);
        public static Vector3 MinValue = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        public static Vector3 MaxValue = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        public static Vector3 NaV = new Vector3(float.NaN, float.NaN, float.NaN);

        [FieldOffset(0x00)]
        private readonly float _x;
        [FieldOffset(0x04)]
        private readonly float _z;
        [FieldOffset(0x08)]
        private readonly float _y;

        public float X { get { return _x; } }
        public float Z { get { return _z; } }
        public float Y { get { return _y; } }

        public Vector3(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector3)) return false;
            Vector3 v2 = (Vector3)obj;

            return this == v2;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hc = 7;
                hc ^= (_x.GetHashCode() * 11);
                hc ^= (_y.GetHashCode() * 13);
                hc ^= (_z.GetHashCode() * 17);

                return hc;
            }
        }

        public static bool IsNaV(Vector3 a)
        {
            return
                float.IsNaN(a.X) &&
                float.IsNaN(a.Y) &&
                float.IsNaN(a.Z);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a._x == b._x && a._y == b._y && a._z == b._z;
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a == b);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static implicit operator Vector3(Vector o)
        {
            return new Vector3(o.X, o.Y, float.NaN);
        }

        public override string ToString()
        {
            return ToString(2);
        }

        public string ToString(int precision)
        {
            return string.Format("[X:{0:F" + precision.ToString() + "}] [Y:{1:F" + precision.ToString() + "}] [Z:{2:F" + precision.ToString() + "}]", X, Y, Z);
        }

        public float DistanceTo(Vector3 other, bool ignoreZ)
        {
            if (!ignoreZ) return DistanceTo(other);
            float dx = Math.Abs(this.X - other.X);
            float dy = Math.Abs(this.Y - other.Y);

            return (float)Math.Sqrt((dx * dx) + (dy * dy));
        }

        public float DistanceTo(Vector3 other)
        {
            float dx = Math.Abs(this.X - other.X);
            float dy = Math.Abs(this.Y - other.Y);
            float dz = Math.Abs(this.Z - other.Z);

            return (float)Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
        }

        public float DistanceSquared(Vector3 other, bool ignoreZ)
        {
            if (!ignoreZ) return DistanceSquared(other);
            float dx = Math.Abs(this.X - other.X);
            float dy = Math.Abs(this.Y - other.Y);

            return (dx * dx) + (dy * dy);
        }

        public float DistanceSquared(Vector3 other)
        {
            float dx = Math.Abs(this.X - other.X);
            float dy = Math.Abs(this.Y - other.Y);
            float dz = Math.Abs(this.Z - other.Z);

            return (dx * dx) + (dy * dy) + (dz * dz);
        }

        public float HeadingTo(Vector3 destination)
        {
            Vector dT = new Vector(destination.X - this.X, destination.Y - this.Y);

            float heading = (90 - (float)(Math.Atan2(dT.Y, dT.X) * D2RADMUL)).Wrap(360, false);
            return heading;
        }

        public Vector ToVector()
        {
            return new Vector(X, Y);
        }

    }
}
