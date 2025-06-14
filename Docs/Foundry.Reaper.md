 # Foundry.Reaper

 **Project Type:** Class Library targeting .NET Framework 3.5

 ## Overview

 `Foundry.Reaper` builds on the automation framework to automate resource gathering ("reaping") in EverQuest 2. It orchestrates navigation, node detection, and harvest actions through configurable workflows and work items.

 ## Module Breakdown

 | Folder                          | Responsibility                                      |
 | ------------------------------- | --------------------------------------------------- |
 | `Configuration`                 | `ReaperConfiguration` for loading runtime settings   |
 | `Services`                      | `SpawnIgnoreService` to filter undesirable targets   |
 | `Workflows`                     | Configurable and predefined workflows for reaping, navigation, harvesting |
 | `Workflows/Reaper Workflows`    | Grouped subfolders for harvest, navigation, and general reaper tasks |
 | `Properties`                    | Assembly metadata                                   |

 ## Important Classes

 ### `ReaperEngine`
 Initializes the automation environment, loads configuration, and drives the main workflow runner loop.

 ### `ReaperConfiguration`
 Parses application settings (XML or similar) defining target filters, walkpaths, and behavior thresholds.

 ### `ReaperConfigurableWorkflow`
 Base workflow that instantiates and runs work items according to `ReaperConfiguration`.

 ### WorkItems (Harvesting / Navigation / Reaper Tasks)
 Discrete steps encapsulated in classes like:
 - `CheckNodeStatusWorkItem`, `AttemptHarvestWorkItem`
 - `WalkToWaypointWorkItem`, `JumpWorkItem`, `DelayWalkingWorkItem`, `NextWalkpathWorkItem`
 - `PlayerDetectWorkItem`, `DetermineIsActionableTarget`, `RetargetSpawnIDWorkItem`, `StopWalkingWorkItem`, `DelayCheckWorkItem`

 ### `HarvestWorkflow`, `NavigationWorkflow`, `WalkWorkflow`, `ReaperWorkflow`
 Predefined sequences of work items for common harvesting and navigation scenarios.

 ## Key Methods & Patterns

 - `ReaperEngine.Start()` – Entry point to begin automated reaping operations.
 - `ReaperConfiguration.Load()` – Reads and validates configuration settings.
 - `WorkItem.Execute()` – Core action for each work item subclass.
 - `SpawnIgnoreService.ShouldIgnore()` – Determines if a spawn should be skipped based on configuration.

 ## Legacy Details & Known Issues

 - Configuration format is tightly coupled; no version migration or validation schema.
 - Workflow sequences are static; limited runtime extensibility.
 - Minimal error handling; failures in memory reads or invalid node data can terminate the workflow.

 ## Usage Notes

 1. Place a valid configuration file (e.g., `ReaperConfig.xml`) alongside the executable.
 2. Ensure `Eq2PointerLibrary` definitions are up to date for your game client.
 3. Monitor log output (via `LogService`) to diagnose unexpected behavior.