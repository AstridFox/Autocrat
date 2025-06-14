using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Reaper.Configuration;
using Foundry.Autocrat.Geometry;

namespace Foundry.Reaper.Workflows {
	public class DetermineIsActionableTarget : ReaperConfigurableWorkItem {
		public int SpawnId { private get; set; }
		public Vector3 Location { private get; set; }
		public string Name { private get; set; }

		public bool IsHarvestable { get; private set; }
		public bool IsHuntable { get; private set; }

		public override void Execute() {
			IsHarvestable = false;
			IsHuntable = false;

            Console.WriteLine("\r\n\r\nHave target");

			// Check against ignore list
			if (Configuration.IgnoredSpawns.IsIgnored(SpawnId)) return;
            Console.WriteLine("Not ignored.");

			// Checks against position
			var cl = Configuration.Eq2PointerLibrary.CharacterLocation;
			if (cl.DistanceTo(Location) > Configuration.MaxActionableTargetDistance) return;

            Console.WriteLine("Not too far away.");

			if (Math.Abs(cl.Z - Location.Z) > Configuration.MaxActionableTargetHeightVariance) return;

            Console.WriteLine("Not too high or low.");

			// Checks against target name
			if (Configuration.HarvestingAllowed) {
				if (Configuration.AllowHarvestingListSubstring) {
					IsHarvestable = Configuration.HarvestingItems.Any(n => n.ToLowerInvariant().Contains(Name.ToLowerInvariant()));
				} else {
					IsHarvestable = Configuration.HarvestingItems.Contains(Name, StringComparer.InvariantCultureIgnoreCase);
				}
			}
			if (IsHarvestable) return;

            Console.WriteLine("Not in the harvestable list.");

			if (Configuration.HuntingAllowed) {
				if (Configuration.AllowHuntingListSubstring) {
					IsHuntable = Configuration.HuntingItems.Any(n => n.ToLowerInvariant().Contains(Name.ToLowerInvariant()));
				} else {
					IsHuntable = Configuration.HuntingItems.Contains(Name, StringComparer.InvariantCultureIgnoreCase);
				}
			}

            if (!IsHuntable) Console.WriteLine("Not in the huntable list.");

		}

	}
}
