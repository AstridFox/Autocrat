using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Reaper.Configuration;
using Foundry.Autocrat.Workflows;

namespace Foundry.Reaper.Workflows {
	public abstract class ReaperConfigurableWorkflow : Workflow {
		public ReaperConfigurableWorkflow(ReaperConfiguration configuration) {
			Configuration = configuration;
		}

		public ReaperConfiguration Configuration { protected get; set; }
	}
}
