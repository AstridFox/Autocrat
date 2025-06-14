using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundry.Autocrat.Workflows;
using Foundry.Autocrat.Everquest2.Navigation.Walkpath;
using Foundry.Autocrat.Everquest2.Memory;
using Foundry.Autocrat.Geometry;
using Foundry.Autocrat.Extensions.Geometry;
using Foundry.Autocrat.Extensions.Math;
using Foundry.Reaper.Configuration;

namespace Foundry.Reaper.Workflows {
	public class WalkToWaypointWorkItem : ReaperConfigurableWorkItem {
		public Waypoint Waypoint { private get; set; }

		public bool ReachedWaypoint { get; private set; }

		public override void Execute() {
			float newHeading = Configuration.Eq2PointerLibrary.CharacterLocation.HeadingTo(Waypoint.Destination);

			Configuration.Eq2PointerLibrary.CharacterHeading = newHeading;
			Configuration.Eq2PointerLibrary.CharacterIsAutoRunning = true;

			// Speed boost hack - use caution! Should have a limiter on this value so as not to allow the user to hang himself...
			if (Configuration.BoostSpeed) Configuration.Eq2PointerLibrary.CharacterSpeed = Configuration.RunSpeed;
			// Compare distance to see if we reached the waypoint. Ignore the Z coordinate in this calculation because that can result in buggy operation during jumps.
			ReachedWaypoint = Configuration.Eq2PointerLibrary.CharacterLocation.DistanceTo(Waypoint.Destination, true) <= Configuration.NavigationAccuracyThreshold;

			// Teleport to the waypoint to increase walkpath accuracy - also use caution here, although the implications are
			// less severe than with the speed boost.
			if (ReachedWaypoint && Configuration.TeleportToWaypointUponReaching) Configuration.Eq2PointerLibrary.CharacterLocation = Waypoint.Destination;
		}
	}
}
