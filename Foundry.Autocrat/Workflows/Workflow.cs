using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Workflows {
	
	public abstract class Workflow: IEnumerable<WorkItem> {
		protected abstract IEnumerable<WorkItem> WorkFlow();

		public IEnumerator<WorkItem> GetEnumerator() {
			return this.WorkFlow().GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.WorkFlow().GetEnumerator();
		}
	}

}
