using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundry.Autocrat.Workflows;
using Foundry.Reaper.Workflows;
using Foundry.Reaper.Configuration;

namespace Foundry.Reaper {
	public class ReaperEngine {

		private ReaperWorkflow ReaperWorkflow;
		private WorkflowRunner WorkflowRunner;
		private ReaperConfiguration Configuration;

		public ReaperEngine(ReaperConfiguration configuration) {
			Configuration = configuration;
			ReaperWorkflow = new ReaperWorkflow(Configuration);
			WorkflowRunner = new WorkflowRunner();

			WorkflowRunner.BeforeWorkItemRun += new EventHandler<WorkItemEventArgs>(
				(sender, e) =>
				{
					var rcwi = e.WorkItem as ReaperConfigurableWorkItem;
					if (rcwi != null) {
						rcwi.Configuration = Configuration;
					}
				});

			WorkflowRunner.AfterWorkflowRun += new EventHandler<WorkflowRunnerEventArgs>(
				(sender, e) =>
				{
					configuration.Eq2PointerLibrary.CharacterIsAutoRunning = false;
				});
		}

		public void RunReaper() {
			WorkflowRunner.RunWorkflow(ReaperWorkflow);
		}

		public IAsyncResult BeginRunReaper() {
			return WorkflowRunner.BeginRunWorkflow(ReaperWorkflow);
		}

		public void EndRunReaper(IAsyncResult ar) {
			WorkflowRunner.EndRunWorkflow(ar);
		}

		public event EventHandler<WorkItemEventArgs> BeforeWorkItemRun {
			add { WorkflowRunner.BeforeWorkItemRun += value; }
			remove { WorkflowRunner.BeforeWorkItemRun -= value; }
		}

		public event EventHandler<WorkItemEventArgs> AfterWorkItemRun {
			add { WorkflowRunner.AfterWorkItemRun += value; }
			remove { WorkflowRunner.AfterWorkItemRun -= value; }
		}

		public event EventHandler<WorkflowRunnerEventArgs> BeforeWorkflowRun {
			add { WorkflowRunner.BeforeWorkflowRun += value; }
			remove { WorkflowRunner.BeforeWorkflowRun -= value; }
		}

		public event EventHandler<WorkflowRunnerEventArgs> AfterWorkflowRun {
			add { WorkflowRunner.AfterWorkflowRun += value; }
			remove { WorkflowRunner.AfterWorkflowRun -= value; }
		}

	}
}
