using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Reaper.Configuration;

namespace Foundry.Reaper.Workflows {
	public class CheckNodeStatusWorkItem : ReaperConfigurableWorkItem {
		public int NodeSpawnId { private get; set; }

		public bool NodeDepopped { get; private set; }

		public override void Execute() {
			// Check to see if we still have a target, and if that target is the
			// same target we had.
			if (Configuration.Eq2PointerLibrary.CharacterHasTarget) {
				NodeDepopped = Configuration.Eq2PointerLibrary.TargetSpawnId != NodeSpawnId;
			} else {
				NodeDepopped = true;
			}
            
            Console.WriteLine(Configuration.Eq2PointerLibrary.CharacterHasTarget);
            Console.WriteLine(Configuration.Eq2PointerLibrary.TargetSpawnId);
            Console.WriteLine(NodeSpawnId);
            Console.WriteLine(NodeDepopped);
            Console.WriteLine("--------------------");
		}
	}
}
