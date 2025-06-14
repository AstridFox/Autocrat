using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Reaper.Configuration;

namespace Foundry.Reaper.Workflows {

	// HarvestWorkflow assumes the character has already navigated to the node.
	// It also assumes that the character is currently targetting the node.
	// All we have to do here is press the harvesting key, either until the
	// node de-pops, or until the maximum number of attempts has passed. If
	// the former occurs, then we assume success and call the 'SuccessFlag'
	// delegate, which will inform the caller that the workflow was successful.

	public class HarvestWorkflow : ReaperConfigurableWorkflow {
		public HarvestWorkflow(ReaperConfiguration configuration) : base(configuration) { }

		// Input
		public int NodeSpawnId { private get; set; }

		public bool Successful { get; private set; }

		protected override IEnumerable<WorkItem> WorkFlow() {
			Successful = false;

			using (var log = Configuration.LogService.OpenLog(Configuration.Eq2PointerLibrary.CharacterName, Configuration.Eq2PointerLibrary.CharacterServerName)) {
				// TODO: Add harvesting-specific log regexes here.

				for (int i = 0; i < Configuration.HarvestingAttempts; i++) {
					// Attempt harvest.
					yield return new AttemptHarvestWorkItem();

					// Delay.
					for (DelayWorkItem delay = new DelayWorkItem(Configuration.HarvestAttemptDelay); !delay.WaitComplete; ) yield return delay;

					// TODO: Check log for harvesting malfunctions and harvested items

					// Check node status.
					var cns = new CheckNodeStatusWorkItem { NodeSpawnId = NodeSpawnId };
                    yield return cns;
					if (cns.NodeDepopped) {
						// Assume the node depopped because we harvested it out. Doesn't really matter in the long run.
						Successful = true;
						yield break;
					}
				}
			}
		}

	}
}
