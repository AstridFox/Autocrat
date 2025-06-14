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

## Detailed Topics

Dive deeper into key areas with topic-specific guides:

- [Abilities & Chains](Foundry.Autocrat.Everquest2/Abilities.md)
- [Walkpath & Navigation Workflows](Foundry.Autocrat.Everquest2/Walkpath.md)
- [In-Game Memory & Target Injection](Foundry.Autocrat.Everquest2/Memory.md)
- [UI Dialogs & Editors](Foundry.Autocrat.Everquest2/Ui.md)

- [Logging & Configuration](Foundry.Autocrat.Everquest2/Logging.md)

## Public API & Examples

Examples of using the public APIs in `Foundry.Autocrat.Everquest2`.

### EverQuest 2 Helper (`Eq2`)
Wraps pointer library and exposes in-game data accessors:
```csharp
var eq2 = new Eq2(process, @"C:\path\to\void.dat");
string playerName = eq2.Character.Name;
float x = eq2.Character.Location.X;
bool hasTarget = eq2.Character.HasTarget;
```
【F:Foundry.Autocrat.Everquest2/Memory/Eq2PointerLibrary.cs†L1-L20】【F:Foundry.Autocrat.Everquest2/Memory/Eq2PointerLibrary.cs†L100-L120】

### Ability Chains
Load an ability chain from XML and execute combat sequence:
```csharp
var chain = AbilityChainSerializer.LoadAbilityChain("Combat.xml");
var next = chain.GetNextAvailableAbility(ignoreDuration: false);
if (next != null)
{
    next.Activate();
    Keyboard.Press(next.KeyCombo);
}
```
【F:Foundry.Autocrat.Everquest2/Abilities/AbilityChain.cs†L1-L16】【F:Foundry.Autocrat.Everquest2/Abilities/Serialization/AbilityChainSerializer.cs†L1-L30】

### Walkpath Navigation
Execute a walkpath workflow with `WorkflowRunner`:
```csharp
var context = new WalkpathNavigationContext
{
    Eq2 = eq2,
    Keyboard = new Keyboard(),
    Walkpath = WalkpathSerializer.LoadWalkpath("path.dat"),
    WaypointPrecision = 1.0f
};
var wf = new WalkpathNavigationWorkflow(context);
new WorkflowRunner().RunWorkflow(wf);
```
【F:Foundry.Autocrat.Everquest2/Navigation/Walkpath/WalkpathNavigationWorkflow.cs†L1-L20】【F:Foundry.Autocrat.Everquest2/Navigation/Walkpath/Serialization/WalkpathSerializer.cs†L1-L30】

### Target Name Injection
Read the current target's name by injecting into the process:
```csharp
var tns = new TargetNameService(process, eq2);
string targetName = tns.TargetName;
Console.WriteLine("Current target: " + targetName);
```
【F:Foundry.Autocrat.Everquest2/Memory/TargetNameService.cs†L1-L20】【F:Foundry.Autocrat.Everquest2/Memory/TargetNameService.cs†L50-L70】

### UI Editors
Show the ability editor dialog and persist changes:
```csharp
var ability = chain.First();
var updated = AbilityEditorDialog.EditAbility(ability);
if (updated != ability)
    AbilityChainSerializer.SaveAbilityChain(chain, "Abilities");
```
【F:Foundry.Autocrat.Everquest2/Abilities/UI/AbilityEditorDialog.cs†L20-L30】【F:Foundry.Autocrat.Everquest2/Abilities/Serialization/AbilityChainSerializer.cs†L1-L20】

### Logging & Configuration
Write a log entry and load reaper settings:
```csharp
var log = new LogService("app.log");
log.Log("Workflow started");

var cfg = new ReaperConfiguration("ReaperConfig.xml");
```
【F:Foundry.Autocrat.Everquest2/LogFiles/LogService.cs†L10-L18】【F:Foundry.Autocrat.Reaper/Configuration/ReaperConfiguration.cs†L10-L25】