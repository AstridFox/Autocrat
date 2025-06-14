# Workflow Engine

The workflow engine in `Foundry.Autocrat` provides a simple, sequential execution model for discrete tasks (`WorkItem`) defined in a custom `Workflow`.

## Core Types

```csharp
// Define a workflow by yielding WorkItem instances in sequence
public abstract class Workflow : IEnumerable<WorkItem>
{
    protected abstract IEnumerable<WorkItem> WorkFlow();
    public IEnumerator<WorkItem> GetEnumerator() => WorkFlow().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => WorkFlow().GetEnumerator();
}

// Base class for all work items; override Execute() for custom behavior
public abstract class WorkItem
{
    public abstract void Execute();
}

// Example of a built-in work item that delays workflow execution
public class DelayWorkItem : WorkItem
{
    public TimeSpan TimeToWait { private get; set; }
    private bool StartedWaiting;
    private DateTime TimeStarted;

    public DelayWorkItem(int msToWait) => TimeToWait = TimeSpan.FromMilliseconds(msToWait);
    public override void Execute()
    {
        if (!StartedWaiting)
        {
            TimeStarted = DateTime.Now;
            StartedWaiting = true;
        }
        // WorkflowRunner will re-invoke until WaitComplete is true
    }

    public bool WaitComplete => StartedWaiting && (DateTime.Now - TimeStarted) >= TimeToWait;
}
```
【F:Foundry.Autocrat/Workflows/Workflow.cs†L1-L20】【F:Foundry.Autocrat/Workflows/WorkItems/DelayWorkItem.cs†L1-L38】

## Runner Execution Loop

The `WorkflowRunner` drives each `WorkItem` in turn and raises events before and after each step:

```csharp
public class WorkflowRunner
{
    public event EventHandler<WorkItemEventArgs> BeforeWorkItemRun, AfterWorkItemRun;
    public event EventHandler<WorkflowRunnerEventArgs> BeforeWorkflowRun, AfterWorkflowRun;

    public void RunWorkflow(Workflow workflow)
    {
        BeforeWorkflowRun?.Invoke(this, new WorkflowRunnerEventArgs(workflow));
        foreach (var item in workflow)
        {
            if (BeforeWorkItemRun?.Invoke(this, new WorkItemEventArgs(workflow, item)) == true)
                return;
            item.Execute();
            if (AfterWorkItemRun?.Invoke(this, new WorkItemEventArgs(workflow, item)) == true)
                return;
        }
        AfterWorkflowRun?.Invoke(this, new WorkflowRunnerEventArgs(workflow));
    }
}
```
【F:Foundry.Autocrat/Workflows/WorkflowRunner.cs†L1-L40】【F:Foundry.Autocrat/Workflows/WorkflowRunner.cs†L60-L80】