 # Foundry.Autocrat

 **Project Type:** Class Library targeting .NET Framework 3.5

 ## Overview

 `Foundry.Autocrat` is the core automation framework for Windows-based workflows. It provides:
 - Low-level input capture and simulation (mouse, keyboard, window control)
 - Memory access and pointer management for external processes
 - Basic geometric types and vector math utilities
- A workflow engine to sequence and run discrete tasks
 - Extension methods for common operations (regex, parsing, drawing, time, math, events)

 This library serves as the foundation for higher-level automation scenarios (e.g., game bots).

 ## Module Breakdown

 | Namespace / Folder           | Responsibility                                      |
 | ---------------------------- | --------------------------------------------------- |
 | `Credits`                    | Displaying application credits                      |
 | `Geometry`                   | `Vector`, `Vector3`, camera and math utilities      |
 | `Automation/Windows`         | Wrappers for Windows API (hooks, input, windows)    |
 | `Tracking`                   | Value tracking helpers                              |
 | `Extensions`                 | Fluent, regex, parsing, drawing, time, math extensions |
 | `Workflows`                  | Core workflow engine (`Workflow`, `WorkItem`, runner) |
 | `IO`                         | Stream and file watchers                            |
 | `Memory`                     | `PointerLibrary` and process-memory extensions      |

 ## Important Classes

 ### `WorkflowRunner`
 Coordinates and executes a sequence of `WorkItem` instances, raising events at each step.

 ### `Workflow`
 Base class defining a customizable sequence of named work items.

 ### `WorkItem` / `DelayWorkItem`
 Abstraction for discrete actions; `DelayWorkItem` pauses execution for a specified duration.

 ### `HookManager`
 Installs global keyboard and mouse hooks and dispatches input events.

 ### `Mouse`, `Keyboard`, `Window`
 High-level APIs for simulating and querying user input and window state.

 ### `PointerLibrary`
 Loads pointer definitions and resolves memory addresses for external processes.

 ### `Vector`, `Vector3`
 Simple 2D/3D vector types with operations exposed via extension methods.

 ## Key Methods & Patterns

 - `WorkflowRunner.Run()` – Start the workflow execution loop.
 - `WorkItem.Execute()` – Override to implement work logic.
 - `HookManager.InstallHooks()` – Enable global input hooks.
 - `Keyboard.Press()`, `Mouse.Click()` – Simulate user input.
 - `PointerLibrary.ResolvePointer()` – Get a target memory address by name.
 - `Vector3.DistanceTo()`, `Vector3.Normalize()` – Perform spatial calculations.

## Legacy Details & Known Issues

 - Targets .NET Framework 3.5; built with Visual Studio 2008 era patterns.
 - Heavy use of partial classes and extension methods for code modularity.
 - Workflow engine is single-threaded and not inherently thread-safe.
 - Pointer definitions loaded from external files; mismatches can cause invalid memory access.

## Usage Notes

1. Call `HookManager.InstallHooks()` if input hooks are needed.
2. Load pointer definitions via `PointerLibrary` before accessing process memory.
3. Handle `WorkflowRunner` events (`WorkItemStarting`, `WorkItemCompleted`) for logging or custom logic.

## Detailed Topics

For a deeper dive into core components, see the topic-specific guides:

- [Workflow Engine](Foundry.Autocrat/WorkflowEngine.md)
- [Global Input Hooks & Windows Automation](Foundry.Autocrat/WindowsAutomation.md)
- [Memory Pointer Library](Foundry.Autocrat/Memory.md)
- [Geometry & Vector Math](Foundry.Autocrat/Geometry.md)
- [Extension Methods & Helpers](Foundry.Autocrat/Extensions.md)

## Public API & Examples

Below are simple code examples demonstrating how to use the primary public APIs in `Foundry.Autocrat`.

### WorkflowRunner & WorkItem
Define a custom workflow by yielding `WorkItem` instances, then execute it with `WorkflowRunner`:
```csharp
public class ActionItem : WorkItem
{
    private readonly Action _action;
    public ActionItem(Action action) { _action = action; }
    public override void Execute() => _action();
}

public class SampleWorkflow : Workflow
{
    protected override IEnumerable<WorkItem> WorkFlow()
    {
        yield return new DelayWorkItem(500);        // pause 500ms
        yield return new ActionItem(() => Console.WriteLine("Task completed"));
    }
}

var runner = new WorkflowRunner();
runner.BeforeWorkflowRun += (s, e) => Console.WriteLine("Starting workflow");
runner.RunWorkflow(new SampleWorkflow());
```
【F:Foundry.Autocrat/Workflows/Workflow.cs†L8-L17】【F:Foundry.Autocrat/Workflows/WorkflowRunner.cs†L18-L33】【F:Foundry.Autocrat/Workflows/WorkItems/DelayWorkItem.cs†L7-L38】

### Global Input Hooks
Install global mouse or keyboard hooks via `HookManager`, then respond to events:
```csharp
HookManager.MouseClickExt += (s, e) =>
{
    Console.WriteLine($"Mouse clicked at {e.Point}");
    if (e.Button == MouseButtons.Right) e.Handled = true;  // suppress further processing
};
HookManager.InstallHooks();
Application.Run();
```
【F:Foundry.Autocrat/Automation/Windows/InputHook/HookManager.Callbacks.cs†L214-L234】

### Keyboard & Mouse Simulation
Use the `Keyboard` and `Mouse` helpers to simulate input:
```csharp
var kb = new Keyboard();
kb.Press(Keyboard.Keys.F5);                          // press F5 key

var combo = new Keyboard.KeyCombo(Keyboard.Keys.C, alt: true);
kb.Press(combo);                                    // press Alt+C

Mouse.Click(MouseButtons.Left);                     // single left-click
```
【F:Foundry.Autocrat/Automation/Windows/Keyboard.cs†L445-L467】【F:Foundry.Autocrat/Automation/Windows/Mouse.cs†L1-L12】

### Window Manipulation
Find, activate, or move a window using `Window`:
```csharp
var win = Window.FindWindowByCaption("Notepad");
win.Activate();                                     // bring to foreground
win.MoveWindow(new Point(100, 100));                // reposition
win.ResizeWindow(new Size(800, 600));               // resize window
```
【F:Foundry.Autocrat/Automation/Windows/Window.cs†L332-L362】

### Memory Pointer Library
Resolve multi-level pointers and read/write process memory:
```csharp
var doc = XDocument.Load("pointers.xml");
var ptrLib = new PointerLibrary(doc);
IntPtr addr = ptrLib.Resolve(process, "Base/Offset1/Offset2");
int value = process.Read<int>(addr);
ptrLib.Write(process, "Base/Offset1/Offset2", value + 1);
```
【F:Foundry.Autocrat/Memory/PointerLibrary.cs†L34-L46】【F:Foundry.Autocrat/Memory/PointerLibrary.cs†L74-L103】

### Vector3 & Geometry
Perform spatial calculations with `Vector3`:
```csharp
var a = new Vector3(0, 0, 0);
var b = new Vector3(10, 0, 0);
float dist = a.DistanceTo(b, ignoreZ: true);
float heading = a.HeadingTo(b);
```
【F:Foundry.Autocrat/Geometry/Vector3.cs†L96-L115】【F:Foundry.Autocrat/Geometry/Vector3.cs†L124-L133】

### Extension Methods
Use fluent helpers for tuples, time spans, and parsing:
```csharp
var tup = 5.Milliseconds();                // TimeSpan of 5ms
string pretty = tup.ToPrettyString();       // "5ms"
var vector = new Vector(1, 2);
var point = vector.ToPointF();              // System.Drawing.PointF
```
【F:Foundry.Autocrat/Extensions/TimeExtensions.cs†L6-L18】【F:Foundry.Autocrat/Extensions/Geometry/VectorExtensions.cs†L7-L16】