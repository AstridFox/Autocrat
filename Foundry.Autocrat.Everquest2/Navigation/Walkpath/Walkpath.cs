using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Everquest2.Navigation.Walkpath
{
    public class Walkpath
    {
        public string Name { get; set; }
        public string Zone { get; set; }

        public Walkpath()
        {
            Waypoints = new List<Waypoint>();
        }

        public List<Waypoint> Waypoints { get; private set; }
    }
}
