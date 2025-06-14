 # Foundry.Autocrat.Everquest2

 **Project Type:** Windows Forms Application targeting .NET Framework 3.5

 ## Overview

 `Foundry.Autocrat.Everquest2` builds on the core automation framework to provide a UI-driven toolset for automating tasks in EverQuest 2. Key features include:
 - Ability chaining and execution workflows for combat automation
 - Walkpath recording, serialization, and navigation workflows for movement
 - Game memory integration via specialized pointer definitions
 - Logging of runtime activity and events

 ## Module Breakdown

 | Folder                            | Responsibility                              |
 | --------------------------------- | ------------------------------------------- |
 | `Abilities`                       | Modeling, serialization, and UI for ability chains |
 | `Navigation/Walkpath`             | Walkpath data structures, serialization, navigation workflows |
 | `Memory`                          | `Eq2PointerLibrary` and `TargetNameService` for in-game data access |
 | `LogFiles`                        | `LogService` for writing activity logs       |
 | `Properties`                      | Assembly metadata                           |

 ## Important Classes

 ### `Ability`, `AbilityChain`
 Define and manage a sequence of in-game abilities; select the next available action based on cooldowns.

 ### `AbilityChainSerializer`
 Serializes and deserializes `AbilityChain` instances to XML for persistence.

 ### `Walkpath`, `Waypoint`, `WalkpathSerializer`
 Represent navigable routes as coordinate lists and persist them through XML.

 ### `WalkpathNavigationWorkflow`
 Implements a `WorkflowRunner` workflow to traverse a recorded path with built-in delay and movement steps.

 ### `Eq2PointerLibrary`
 Loads EverQuest 2-specific pointer definitions and integrates with the core `PointerLibrary`.

 ### `TargetNameService`
 Reads the in-game name of the current target by scanning process memory structures.

 ### UI Dialogs
 WinForms dialogs (`AbilityEditorDialog`, `AbilityChainDialog`, `WalkpathManagerDialog`, etc.) for editing ability chains and walkpaths.

 ## Key Methods & Patterns

 - `AbilityChain.GetNextAvailableAbility()` – Returns the next ability ready for activation.
 - `WalkpathNavigationWorkflow.Execute()` – Drives movement through a series of waypoints.
 - `AbilityChainSerializer.Serialize()/Deserialize()` – XML persistence for ability chains.
 - `WalkpathSerializer.Serialize()/Deserialize()` – XML persistence for walkpaths.
 - `LogService.Log()` – Writes timestamped log entries to files.

 ## Legacy Details & Known Issues

 - UI built with Windows Forms Designer (`.Designer.cs` generated code).
 - XML serialization has no schema versioning; changes to data classes require manual updates.
 - Hard-coded file paths in code; adjust pointers data location before running.
 - No dynamic error recovery for broken pointer definitions or out-of-range waypoints.

 ## Usage Notes

 1. Ensure EverQuest 2 is running and visible before starting navigation or ability workflows.
 2. Update the `Eq2PointerLibrary` data file path to match your local `void.dat` definitions.
 3. Use the built-in log viewer or inspect log files in the application folder for troubleshooting.