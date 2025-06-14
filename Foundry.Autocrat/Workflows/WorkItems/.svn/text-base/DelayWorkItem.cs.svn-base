using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Workflows {
	public class DelayWorkItem : WorkItem {
		// Input
		public TimeSpan TimeToWait { private get; set; }
		
		// Output
		public bool StartedWaiting { get; private set; }
		public DateTime TimeStarted { get; private set; }

		// Active
		public virtual bool WaitComplete {
			get {
				return StartedWaiting && (DateTime.Now - TimeStarted) >= TimeToWait;
			}
		}

		public DelayWorkItem(TimeSpan toWait) {
			TimeToWait = toWait;
		}
		public DelayWorkItem(int msToWait) {
			TimeToWait = new TimeSpan(0, 0, 0, 0, msToWait);
		}

		protected bool Start() {
			if (StartedWaiting) return false;
			TimeStarted = DateTime.Now;
			StartedWaiting = true;
			return true;
		}

		public override void Execute() {
			Start();
		}
	}
}
