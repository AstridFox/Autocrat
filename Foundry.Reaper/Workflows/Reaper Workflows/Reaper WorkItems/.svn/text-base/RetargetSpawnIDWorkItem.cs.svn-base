using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Autocrat.Automation.Windows;
using Foundry.Autocrat.Everquest2.Memory;
using System.Threading;
using Foundry.Reaper.Configuration;

namespace Foundry.Reaper.Workflows {
	public class RetargetSpawnIDWorkItem : ReaperConfigurableWorkItem {
		public int TargetSpawnId { private get; set; }

		public bool ReacquiredTarget { get; private set; }

		public override void Execute() {
			Configuration.Keyboard.Press(Configuration.TargetSelectKey);
			Thread.Sleep(Configuration.Keyboard.KeyboardEventDelayMS);
			if (Configuration.Eq2PointerLibrary.TargetSpawnId == TargetSpawnId) ReacquiredTarget = true;
		}
	}
}
