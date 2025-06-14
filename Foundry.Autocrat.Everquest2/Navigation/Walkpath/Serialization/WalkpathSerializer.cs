using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Foundry.Autocrat.Geometry;

namespace Foundry.Autocrat.Everquest2.Navigation.Walkpath.Serialization
{
    public static class WalkpathSerializer
    {
        public static void SaveWalkpath(Walkpath walkpath, string basePath)
        {
            string fileName = walkpath.Name;
            foreach (var chr in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(chr.ToString(), "");
            }

            string filePath = Path.Combine(basePath, fileName + ".dat");

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine(walkpath.Name);
                sw.WriteLine(walkpath.Zone);
                sw.WriteLine(walkpath.Waypoints.Count);

                foreach (var wp in walkpath.Waypoints)
                {
                    sw.WriteLine(wp.Vector.X);
                    sw.WriteLine(wp.Vector.Y);
                    sw.WriteLine(wp.Vector.Z);
                    sw.WriteLine(wp.JumpWhenReached);
                    sw.WriteLine(wp.IsSafe);
                    sw.WriteLine(wp.Camera != null);
                    if (wp.Camera != null)
                    {
                        sw.WriteLine(wp.Camera.Pitch);
                        sw.WriteLine(wp.Camera.Yaw);
                        sw.WriteLine(wp.Camera.Zoom);
                    }
                }
            }
        }

        public static Walkpath LoadWalkpath(string filePath)
        {
            Walkpath r = new Walkpath();

            using (StreamReader sr = new StreamReader(filePath))
            {
                r.Name = sr.ReadLine();
                r.Zone = sr.ReadLine();
                int ct = int.Parse(sr.ReadLine());

                for (int i = 0; i < ct; i++)
                {
                    float x = float.Parse(sr.ReadLine());
                    float y = float.Parse(sr.ReadLine());
                    float z = float.Parse(sr.ReadLine());
                    bool jwr = bool.Parse(sr.ReadLine());
                    bool iss = bool.Parse(sr.ReadLine());
                    bool hsc = bool.Parse(sr.ReadLine());
                    float pitch, yaw, zoom;
                    Camera cam = null;
                    if (hsc)
                    {
                        pitch = float.Parse(sr.ReadLine());
                        yaw = float.Parse(sr.ReadLine());
                        zoom = float.Parse(sr.ReadLine());
                        cam = new Camera(pitch, yaw, zoom);
                    }

                    var wp = new Waypoint(new Vector3(x, y, z), jwr, iss);
                    if (hsc) wp.Camera = cam;
                    r.Waypoints.Add(wp);
                }

                return r;
            }
        }
    }
}
