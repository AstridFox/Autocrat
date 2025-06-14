using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Reaper.Configuration;

namespace Foundry.Reaper.Workflows {
	public class AttemptHarvestWorkItem : ReaperConfigurableWorkItem {
		public override void Execute() {
			// Press the harvesting key
			Configuration.Keyboard.Press(Configuration.HarvestingKey);
		}
	}
}
