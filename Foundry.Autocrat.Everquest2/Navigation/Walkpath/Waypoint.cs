using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Geometry;

namespace Foundry.Autocrat.Everquest2.Navigation.Walkpath
{
    public class Waypoint
    {
        public Waypoint(Vector3 vector, bool jumpWhenReached, bool isSafe)
        {
            Vector = vector;
            JumpWhenReached = jumpWhenReached;
            IsSafe = IsSafe;
        }

        public Vector3 Vector { get; private set; }
        public Camera Camera { get; set; }
        public bool JumpWhenReached { get; private set; }
        public bool IsSafe { get; set; }
    }
}
