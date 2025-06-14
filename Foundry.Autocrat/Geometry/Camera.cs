using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Geometry
{
    public class Camera
    {
        public static Camera Zero = new Camera(0, 0, 0);
        public static Camera MinValue = new Camera(float.MinValue, float.MinValue, float.MinValue);
        public static Camera MaxValue = new Camera(float.MaxValue, float.MaxValue, float.MaxValue);
        public static Camera NaC = new Camera(float.NaN, float.NaN, float.NaN);

        private readonly float _pitch;
        private readonly float _yaw;
        private readonly float _zoom;

        public float Pitch { get { return _pitch; } }
        public float Yaw { get { return _yaw; } }
        public float Zoom { get { return _zoom; } }

        public Camera(float pitch, float yaw, float zoom)
        {
            _pitch = pitch;
            _yaw = yaw;
            _zoom = zoom;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Camera)) return false;
            Camera v2 = (Camera)obj;

            return this == v2;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hc = 7;
                hc ^= (_pitch.GetHashCode() * 11);
                hc ^= (_yaw.GetHashCode() * 13);
                hc ^= (_zoom.GetHashCode() * 17);

                return hc;
            }
        }

        public static bool IsNaC(Camera a)
        {
            return
                float.IsNaN(a.Pitch) &&
                float.IsNaN(a.Yaw) &&
                float.IsNaN(a.Zoom);
        }

        public static bool operator ==(Camera a, Camera b)
        {
            return a._pitch == b._pitch && a._yaw == b._yaw && a._zoom == b._zoom;
        }

        public static bool operator !=(Camera a, Camera b)
        {
            return !(a == b);
        }

        public static Camera operator +(Camera a, Camera b)
        {
            return new Camera(a.Pitch + b.Pitch, a.Yaw + b.Yaw, a.Zoom + b.Zoom);
        }

        public static Camera operator -(Camera a, Camera b)
        {
            return new Camera(a.Pitch - b.Pitch, a.Yaw - b.Yaw, a.Zoom - b.Zoom);
        }

        public static implicit operator Camera(Vector o)
        {
            return new Camera(o.X, o.Y, float.NaN);
        }

        public override string ToString()
        {
            return ToString(2);
        }

        public string ToString(int precision)
        {
            return string.Format("[Pitch:{0:F" + precision.ToString() + "}] [Yaw:{1:F" + precision.ToString() + "}] [Zoom:{2:F" + precision.ToString() + "}]", Pitch, Yaw, Zoom);
        }
    }
}
