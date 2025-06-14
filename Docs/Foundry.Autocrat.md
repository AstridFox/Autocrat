 # Foundry.Autocrat

 **Project Type:** Class Library targeting .NET Framework 3.5

 ## Overview

 `Foundry.Autocrat` is the core automation framework for Windows-based workflows. It provides:
 - Low-level input capture and simulation (mouse, keyboard, window control)
 - Memory access and pointer management for external processes
 - Basic geometric types and vector math utilities
 - A workflow engine to sequence and run discrete tasks (see [Yield and the C# state machine](statemachine.pdf))
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