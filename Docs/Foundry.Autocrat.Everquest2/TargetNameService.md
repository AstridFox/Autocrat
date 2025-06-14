# TargetNameService Deep Dive

This guide unpacks the `TargetNameService`, which hooks into the EverQuest 2 game process to extract the in‑game name of the current target by injecting custom x86 code (a "code cave") into the process memory.

## Purpose

Rather than relying on fragile or undocumented memory offsets alone, `TargetNameService` patches two in‑game functions to copy the Unicode target name into a known buffer, then reads that buffer using standard memory reads.

## Why Injection Instead of Direct Memory Reads?

EverQuest 2 stores target names in private game structures—often behind layers of pointers, custom formats, or obfuscation—so there isn’t a single, stable block of memory you can simply walk. By injecting a small code cave and leveraging the game’s own routines, we:

- Reuse EQ2’s built‑in logic to decode, terminate, and copy the Unicode string correctly.
- Avoid brittle, hard‑coded offsets into evolving internal data layouts that tend to break with patches.
- Gain long‑term resilience: we hook well‑known functions instead of guessing raw memory addresses.

This one‑time stub setup trades initial complexity for reliable, up‑to‑date reading of the current target’s name, even as the game client changes.

## Initialization & Code Cave Allocation

When constructed, the service:
1. Allocates a writable data buffer (`DataCave`) inside the EQ2 process.
2. Determines three injection points (function stub and two call sites) from static pointers.
3. Builds raw machine‑code bytes for the custom function and patches.
4. Applies write+execute permissions and installs the new code.
5. Restores original protections for safety.

```csharp
private void Initialize() {
    // Allocate a 512‑byte data buffer to receive the target name string
    IntPtr dataCave = Eq2Process.AllocateMemory(512, ProtectionType.ReadWrite);
    if (dataCave == IntPtr.Zero)
        throw new ApplicationException("Could not allocate memory inside EverQuest 2 process.");

    // Locate the addresses within EQ2 where we hook and when to invoke our code cave
    IntPtr functionCave = Eq2.Autocrat.TargetNameFunction;
    IntPtr pcFunction   = Eq2.Autocrat.PCTargetFunction;
    IntPtr npcFunction  = Eq2.Autocrat.NPCTargetFunction;

    // Generate the full function stub and two short jumps into it
    byte[] fnBytes  = GetFunctionCode(functionCave, dataCave);
    byte[] pcBytes  = GetInjectionCode(pcFunction,   functionCave, 0x06);
    byte[] npcBytes = GetInjectionCode(npcFunction,  functionCave, 0x09);

    // Write and protect code stub
    Eq2Process.ChangeProtection(functionCave, 512, ProtectionType.ExecuteReadWrite);
    Eq2Process.Write(functionCave, fnBytes);

    // Write and restore protections for each call site
    var oldPc = Eq2Process.ChangeProtection(pcFunction,  32, ProtectionType.ExecuteReadWrite);
    var oldNpc = Eq2Process.ChangeProtection(npcFunction, 32, ProtectionType.ExecuteReadWrite);
    Eq2Process.Write(pcFunction,  pcBytes);
    Eq2Process.Write(npcFunction, npcBytes);
    Eq2Process.ChangeProtection(pcFunction,  32, oldPc);
    Eq2Process.ChangeProtection(npcFunction, 32, oldNpc);

    DataCave = dataCave;
}
```
【F:Foundry.Autocrat.Everquest2/Memory/TargetNameService.cs†L33-L67】

## Building the Injection Stub

### GetInjectionCode

Patches at the specified site to push a marker byte then call into our custom function stub:

```csharp
private byte[] GetInjectionCode(IntPtr injectionSite, IntPtr jumpLocation, byte pushValue) {
    using (var ms = new MemoryStream()) {
        var writer = new X86Writer(ms, injectionSite);
        writer.Push32(pushValue);       // push marker of which call-site invoked
        writer.Call(jumpLocation);      // jump into our code cave function
        return ms.ToArray();
    }
}
```
【F:Foundry.Autocrat.Everquest2/Memory/TargetNameService.cs†L69-L76】

### GetFunctionCode

Generates a loop that reads 16‑bit characters from the game's target‑name pointer and writes them into the data cave until a null terminator:

```csharp
private byte[] GetFunctionCode(IntPtr codeCave, IntPtr dataCave) {
    using (var ms = new MemoryStream()) {
        var w = new X86Writer(ms, codeCave);

        // Preserve registers
        w.Push32(ebx);
        w.Push32(edx);
        w.Push32(edi);
        w.Push32(esi);

        // Setup pointers: ESI = [EAX] (target-name base), EDI = dataCave
        w.Mov32(ebx, new X86Address(esp, 0x10));
        w.Mov32(esi, new X86Address(eax, 0));
        w.Mov32(edi, dataCave.ToInt32());

        // Loop: copy WORD from [ESI] to [EDI], advance until zero
        w.MarkLabel(loopStart);
        w.Mov16(dx, new X86Address(esi, 0));
        w.Mov16(new X86Address(edi, 0), dx);
        w.Add32(esi, 2);
        w.Add32(edi, 2);
        w.Cmp16(dx, 0);
        w.Jmp(X86ConditionCode.NotZero, loopStart);

        // Restore registers and return
        w.Pop32(esi);
        w.Pop32(edi);
        w.Pop32(edx);
        w.Pop32(ebx);
        w.Add32(esp, 4);
        w.Retn();

        return ms.ToArray();
    }
}
```
【F:Foundry.Autocrat.Everquest2/Memory/TargetNameService.cs†L78-L155】

*(Note: actual register variables and label creation are omitted for brevity; see code reference.)*

## Reading the Name

Once installed, calling `TargetName` reads a null‑terminated Unicode string from the data cave:

```csharp
private string GetTargetName() {
    return Eq2Process.ReadString(DataCave, 255, Encoding.Unicode, true);
}

public virtual string TargetName => GetTargetName();
```
【F:Foundry.Autocrat.Everquest2/Memory/TargetNameService.cs†L29-L31】【F:Foundry.Autocrat.Everquest2/Memory/TargetNameService.cs†L23-L27】

## Usage Example

```csharp
// After creating and patching via Initialize()
var tns = new TargetNameService(eq2Process, eq2Helper);
Console.WriteLine("Current target name: " + tns.TargetName);
```
【F:Foundry.Autocrat.Everquest2/Memory/TargetNameService.cs†L17-L21】