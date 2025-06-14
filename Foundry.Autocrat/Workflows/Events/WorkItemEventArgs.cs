using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Workflows {
    public class WorkItemEventArgs : EventArgs {
        public WorkItemEventArgs() : base() { }
        public WorkItemEventArgs(Workflow workflow, WorkItem workItem)
            : base() {
            Workflow = workflow;
            WorkItem = workItem;
        }
        public Workflow Workflow { get; set; }
        public WorkItem WorkItem { get; set; }
		public bool Cancel { get; set; }
    }
}
