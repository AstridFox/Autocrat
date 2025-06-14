using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Reaper.Configuration;
using Foundry.Autocrat;
using Foundry.Autocrat.Everquest2.Navigation.Walkpath;
using Foundry.Autocrat.Geometry;

namespace Foundry.Reaper.Workflows {
	public class NavigationWorkflow : ReaperConfigurableWorkflow {
		public NavigationWorkflow(ReaperConfiguration configuration) : base(configuration) { }

		public bool NavigationInterrupted { private get; set; }

		protected override IEnumerable<WorkItem> WorkFlow() {
			foreach (var walkpath in Configuration.WalkpathPlaylist) {
				// Set per-walkpath configuration items in Configuration
				// Configuration.blah = walkpath.Configuration.blah;

				yield return new NextWalkpathWorkItem
				{
					NextWalkpath = walkpath
				};

				Tuple<int, Waypoint> waypoint = new Tuple<int, Waypoint>(-1, null);

				do {
					if (NavigationInterrupted) {
						NavigationInterrupted = false;

						waypoint = GetClosestWaypoint(walkpath);
					} else {
						waypoint = GetNextWaypoint(walkpath, waypoint.Value1);
					}

					if (waypoint.Value1 == -1) break;

					var wtw = new WalkToWaypointWorkItem() { Waypoint = waypoint.Value2 };

					while (!wtw.ReachedWaypoint) {
						while (Configuration.Delaying) yield return new DelayWalkingWorkItem();
						yield return wtw;
					}

					if (waypoint.Value2.JumpWhenReached) yield return new JumpWorkItem();

				} while (!IsLastWaypoint(walkpath, waypoint.Value1));

			}
			yield break;
		}

		private Tuple<int, Waypoint> GetClosestWaypoint(Walkpath walkpath) {
			Waypoint closest = walkpath.Waypoints[0];
			int closestIndex = 0;
			Vector3 charLoc = Configuration.Eq2PointerLibrary.CharacterLocation;
			float distanceToClosest = charLoc.DistanceTo(closest.Destination);

			for (int i = 1; i < walkpath.Waypoints.Count; i++) {
				float curDistance = charLoc.DistanceTo(walkpath.Waypoints[i].Destination);
				if (curDistance < distanceToClosest) {
					distanceToClosest = curDistance;
					closest = walkpath.Waypoints[i];
					closestIndex = i;
				}
			}

			return Tuples.Tuple(closestIndex, closest);
		}

		private Tuple<int, Waypoint> GetNextWaypoint(Walkpath walkpath, int waypointIndex) {
			if (waypointIndex == walkpath.Waypoints.Count - 1) return new Tuple<int, Waypoint>(-1, null);
			return Tuples.Tuple(waypointIndex + 1, walkpath.Waypoints[waypointIndex + 1]);
		}

		private bool IsLastWaypoint(Walkpath walkpath, int waypointIndex) {
			return waypointIndex == walkpath.Waypoints.Count;
		}

	}
}
