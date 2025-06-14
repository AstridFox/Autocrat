using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Reaper.Configuration;
using Foundry.Autocrat.Workflows;
using Foundry.Autocrat.Geometry;
using Foundry.Autocrat.Everquest2.Navigation.Walkpath;

namespace Foundry.Reaper.Workflows {
	public class WalkWorkflow : ReaperConfigurableWorkflow {
		public WalkWorkflow(ReaperConfiguration configuration) : base(configuration) { }

		public Vector3 Destination { private get; set; }

		protected override IEnumerable<WorkItem> WorkFlow() {
			var wtw = new WalkToWaypointWorkItem { Waypoint = new Waypoint { Destination = Destination, JumpWhenReached = false } };

			while (!wtw.ReachedWaypoint) {
				while (Configuration.Delaying) yield return new DelayWalkingWorkItem();
				yield return wtw;
			}
		}
	}
}
