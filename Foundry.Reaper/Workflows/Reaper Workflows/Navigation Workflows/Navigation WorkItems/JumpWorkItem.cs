using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Autocrat.Automation.Windows;

namespace Foundry.Reaper.Workflows {
	public class JumpWorkItem : ReaperConfigurableWorkItem {
		public override void Execute() {
			Configuration.Keyboard.Press(Configuration.JumpKey);
		}
	}
}
