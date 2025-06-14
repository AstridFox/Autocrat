 # TestConsole

 **Project Type:** Console Application targeting .NET Framework 3.5

 ## Overview

 `TestConsole` is a lightweight console-based harness for interactively exercising features of the `Foundry.Autocrat` and `Foundry.Autocrat.Everquest2` libraries. It offers a simple text-menu interface to inspect in-game state, camera data, target information, and to test ability chains.

 ## Project Structure

 | File        | Responsibility                                 |
 | ----------- | ---------------------------------------------- |
 | `Program.cs`| Entry point with interactive menu and test routines |
 | `Ext.cs`    | Extension method `ForEachIndex<T>` for indexed enumeration |

 ## Key Methods

 ### `Program.Main(string[] args)`
 - Locates an `EverQuest2` process and initializes the `Eq2` helper with a pointer data file path.
 - Presents a looping menu driven by `SelectFromContents()`.

 ### Menu Operations (`Show*` methods)
 - `ShowCharacterInformation()` – Displays name, location, health, power, and auto-run status.
 - `ShowCameraInformation()` – Shows camera pitch, yaw, and zoom.
 - `ShowEQ2Information()` – Outputs UI focus state and cursor icon ID.
 - `ShowTargetInformation()` – Reports target spawn IDs, position, distance, level, and health.
 - `ShowTargetName()` – Uses `TargetNameService` to read the in-game target’s name.

 ### `TestAbilityChains(Eq2 eq2)`
 Demonstrates combat automation by selecting the next available ability from a predefined chain and simulating key presses via `Keyboard`.

 ### `Ext.ForEachIndex<T>(this IEnumerable<T>, Action<T,int>)`
 Helper extension to iterate with element index.

 ## Dependencies & Configuration

 - Requires a valid `void.dat` pointer definition file; update the hard-coded path in `Main`.
 - Relies on `Foundry.Autocrat.dll` and `Foundry.Autocrat.Everquest2.dll` in the application directory.
 - Must run with sufficient privileges to hook input and read process memory.

 ## Legacy Patterns

 - Simple console UI rendered via `Console.WriteLine` and cursor manipulation.
 - Single-threaded, blocking loop for menu selection and display refresh.