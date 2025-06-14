using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Autocrat.Geometry;
using Foundry.Autocrat.Everquest2.Memory;
using Foundry.Reaper.Services;
using Foundry.Autocrat.Automation.Windows;
using System.Threading;
using Foundry.Reaper.Configuration;

namespace Foundry.Reaper.Workflows {
	public class TargetWorkItem : ReaperConfigurableWorkItem {
		// Output
		public bool HasTarget { get; private set; }
		public string TargetName { get; private set; }
		public int TargetSpawnId { get; private set; }
		public Vector3 TargetLocation { get; private set; }

		private int CurrentState { get; set; }
		private DateTime WakeUpTime { get; set; }

		public TargetWorkItem()
			: base() {
			CurrentState = 0;
		}

		public override void Execute() {
			HasTarget = false;
			TargetName = "";
			TargetSpawnId = -1;
			TargetLocation = Vector3.NaV;

			// Little state machine to avoid Thread.Sleep on TargetSelectDelay.
			// I love state machines...
			switch (CurrentState) {
				case 0:
					Configuration.Keyboard.Press(Configuration.TargetSelectKey);
					WakeUpTime = DateTime.Now.AddMilliseconds(Configuration.TargetSelectDelay);
					CurrentState = 1; break;
				case 1:
					if (DateTime.Now >= WakeUpTime) CurrentState = 2;
					else CurrentState = 1;
					break;
				case 2:
					HasTarget = Configuration.Eq2PointerLibrary.CharacterHasTarget;
					if (HasTarget) {
						TargetSpawnId = Configuration.Eq2PointerLibrary.TargetSpawnId;
						TargetLocation = Configuration.Eq2PointerLibrary.TargetLocation;
						TargetName = Configuration.TargetNameService.TargetName;
					}
					CurrentState = 0; break;
			}
		}

	}
}
