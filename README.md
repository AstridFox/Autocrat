# Foundry.Autocrat Suite (Archived)

> **Maintenance Status:** This repository is no longer actively maintained or supported. It is provided solely to preserve historical work.

## Overview

This solution contains a collection of automation libraries and tools originally developed on .NET Framework 3.5 with Visual Studio 2008:

| Project                             | Description                                                                 |
| ----------------------------------- | --------------------------------------------------------------------------- |
| **Foundry.Autocrat**                | Core automation framework: input hooks, memory access, workflow engine       |
| **Foundry.Autocrat.Everquest2**     | UI and automation support for EverQuest 2: ability chains, walkpaths, logging |
| **TestConsole**                     | Console-based test harness for exercising core features                     |
| **[Managed.X86](https://github.com/AstridFox/managed-x86)**                     | Runtime x86 code emission and patching library                              |
| **Foundry.Reaper**                  | Resource-gathering workflows and services for EverQuest 2                    |

Additional content:

- `Docs/` folder: high-level Markdown documentation for each project
- `PointerFinderHelper/`: helper utilities (not part of the main solution)
- `managed-x86/`: related code emitter samples and extensions

## Requirements

- Visual Studio 2008 (or later) with .NET Framework 3.5 support
- Windows OS for automation and memory access APIs

## Getting Started

1. Open `Foundry.Autocrat.sln` in Visual Studio.
2. Build the solution targeting .NET 3.5.
3. Consult the documentation in the `Docs/` folder for detailed project overviews.

## Disclaimer

This code is provided “as is” without warranty. Use at your own risk. No support or updates will be provided.