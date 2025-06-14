# In-Game Memory & Target Injection

The `Eq2PointerLibrary` decrypts and loads a custom pointer definition file to map game structures, while `TargetNameService` demonstrates inline code injection into the EQ2 process.

## Eq2PointerLibrary Setup

```csharp
public Eq2PointerLibrary(Process eq2Process, string pointerDocumentPath)
{
    this.Eq2Process = eq2Process;
    byte[] key = { /* obfuscated key bytes */ };
    string xml = Encoding.ASCII.GetString(Obfuscator.Decrypt(pointerDocumentPath, key)).Substring(3);
    this.PointerDocument = XDocument.Parse(xml);
}
```
【F:Foundry.Autocrat.Everquest2/Memory/Eq2PointerLibrary.cs†L23-L45】

## TargetNameService: Code Cave Injection

`TargetNameService` allocates a code cave in the EQ2 process, patches game code to copy the target name into that buffer, then reads the Unicode string:

```csharp
// Allocate executable data cave
IntPtr cave = Eq2Process.AllocateMemory(512, ProtectionType.ReadWrite);
// Patch game functions to jump into our cave
Eq2Process.ChangeProtection(funcAddr, 32, ProtectionType.ExecuteReadWrite);
Eq2Process.Write(funcAddr, injectionBytes);
// Later read the Unicode string from cave
return Eq2Process.ReadString(cave, 255, Encoding.Unicode, true);
```
【F:Foundry.Autocrat.Everquest2/Memory/TargetNameService.cs†L38-L62】