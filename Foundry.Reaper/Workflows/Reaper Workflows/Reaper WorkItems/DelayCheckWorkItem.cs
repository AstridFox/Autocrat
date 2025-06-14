using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Reaper.Configuration;

namespace Foundry.Reaper.Workflows {
	public class DelayCheckWorkItem : ReaperConfigurableWorkItem {

		private int State = 0;
		// 0 = Delay Ended / Wait Began -> 1
		// 1 = Waiting					-> 2
		// 2 = Wait Ended / Delay Began	-> 3
		// 3 = Delaying					-> 0

		public override void Execute() {
			int msDiffMinMaxWait = (int)(Configuration.MaxTimeBetweenDelays.TotalSeconds - Configuration.MinTimeBetweenDelays.TotalSeconds);
			int msDiffMinMaxDelay = (int)(Configuration.MaxDelay.TotalSeconds - Configuration.MinDelay.TotalSeconds);

			switch (State) {
				case 0:
					// Stop Delaying.
					Configuration.Delaying = false;

					// Determine new delay start time
					Configuration.NextDelayStart = (DateTime.Now + Configuration.MinTimeBetweenDelays) + new TimeSpan(0, 0, Configuration.Random.Next(msDiffMinMaxWait));
					State = 1;
					break;

				case 1:
					// Delay until we meet or exceed the next delay time.
					if (DateTime.Now >= Configuration.NextDelayStart) State = 2;
					break;

				case 2:
					// Start Delaying.
					Configuration.Delaying = true;

					// Determine new delay end time
					Configuration.ThisDelayEnds = (DateTime.Now + Configuration.MinDelay) + new TimeSpan(0, 0, Configuration.Random.Next(msDiffMinMaxDelay));
					State = 3;
					break;

				case 3:
					// Delay until we meet or exceed the next wait time.
					if (DateTime.Now >= Configuration.ThisDelayEnds) State = 0;
					break;

			}
		}
	}
}
