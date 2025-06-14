using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Automation.Windows;
using System.Diagnostics;
using Foundry.Autocrat.Everquest2.Memory;

namespace Foundry.Autocrat.Everquest2.Navigation.Walkpath
{
    public class WalkpathNavigationContext
    {
        public Window EQ2Window { get; set; }
        public Process EQ2Process { get; set; }
        public Eq2 Eq2 { get; set; }
        public Keyboard Keyboard { get; set; }

        public Walkpath Walkpath { get; set; }
        public bool JumpingAllowed { get; set; }
        public Keyboard.Keys JumpKey { get; set; }
        public float WaypointPrecision { get; set; }

        public Func<int, List<Waypoint>, int> GetClosestWaypointIndex { get; set; }
    }
}
