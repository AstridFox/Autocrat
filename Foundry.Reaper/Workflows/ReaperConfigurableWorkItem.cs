using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Workflows;
using Foundry.Reaper.Configuration;

namespace Foundry.Reaper.Workflows {
	public abstract class ReaperConfigurableWorkItem : WorkItem {
		public ReaperConfiguration Configuration { protected get; set; }
	}
}
