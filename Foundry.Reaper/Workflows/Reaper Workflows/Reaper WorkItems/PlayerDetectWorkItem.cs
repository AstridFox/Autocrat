using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Autocrat.Geometry;

namespace Foundry.Reaper.Workflows {
	public class PlayerDetectWorkItem : ReaperConfigurableWorkItem {
		public Vector3 PlayerLocation { private get; set; }
		public Vector3 Destination { private get; set; }

		public bool PlayerTooClose { get; private set; }

		public override void Execute() {
			PlayerTooClose = false;
			if (Destination.DistanceTo(PlayerLocation) <= Configuration.PlayerDetectRange) PlayerTooClose = true;
		}
	}
}
