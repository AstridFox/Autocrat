 # Managed.X86

 **Project Type:** Class Library targeting .NET Framework 3.5 (ToolsVersion 3.5)

 ## Overview

 `Managed.X86` is a low-level code emission library that allows runtime generation and patching of x86 machine code. It provides a fluent API to write instructions directly into executable buffers or to overwrite existing code segments.

 ## Module Breakdown

 | Folder            | Responsibility                                    |
 | ----------------- | ------------------------------------------------- |
 | `ControlFlow`     | `Call`, `Jump`, and code-patch operations          |
 | `Arithmetic`      | Basic arithmetic instructions (`Add`, `Subtract`, etc.) |
 | `Logical`         | Bitwise operations (`And`, `Or`, `Xor`)             |
 | `Shift`           | Shift instructions (`ShiftLeft`, `ShiftRight`)      |
 | `Prefixes`        | Instruction prefixes (`Lock`, `Repeat`)             |
 | `IO`              | Port I/O instructions (`In`, `Out`)                 |
 | `Interlock`       | Atomic compare-and-exchange (`CompareExchange`)     |
 | `Stack`           | Stack manipulation (`Push`, `Pop`)                  |
 | `General`         | Move and compare instructions (`Move`, `Compare`)   |
 | `DataTypes`       | Definitions for registers, labels, addresses, and condition codes |
 | `Properties`      | Assembly metadata                                  |

 ## Important Classes & Types

 ### `X86Writer`
 Core class exposing methods to emit instructions, manage labels, and commit machine code to memory or byte buffers.

 ### `X86Label`
 Represents a named jump target to enable forward/backward branches within emitted code.

 ### Register & Address Types
 - `X86Register8`, `X86Register16`, `X86Register32`
 - `X86Address`
 - `X86ConditionCode`, `X86ShiftOpCode`

 ## Key Methods & Examples

 - `writer.Add(dst, src)` – Emit an ADD instruction.
 - `writer.Call(address)` – Generate a call to a function address.
 - `writer.Jump(label)` – Emit a branch to a previously defined `X86Label`.
 - `writer.Patch(address, byte[])` – Overwrite existing code at a given address.
 - `writer.Push(reg)`, `writer.Pop(reg)` – Stack push/pop.

 ## Legacy Details & Limitations

 - Supports x86 (32-bit) code only; no x64 or ARM support.
 - Unsafe memory operations; user must ensure target buffers are executable and properly sized.
 - No internal buffer overflow checks; invalid usage can corrupt process memory.

 ## Usage Notes

 1. Instantiate `X86Writer` and reserve an executable memory block or target address.
 2. Define `X86Label` instances before using `Jump` for forward branches.
 3. Emit instructions in sequence and call `writer.Commit()` (or equivalent) to apply.