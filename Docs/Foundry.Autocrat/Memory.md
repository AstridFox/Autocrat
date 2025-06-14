# Memory Pointer Library

The `PointerLibrary` in `Foundry.Autocrat.Memory` loads an XML-based pointer definition file, validates it against an XSD schema, and resolves multi-level pointers at runtime.

```csharp
public IntPtr Resolve(Process process, string pointerPath)
{
    var (baseAddr, offsets) = ParsePointerPath(pointerPath);
    int addr = baseAddr;
    while (offsets.Count > 0)
    {
        addr = process.Read<int>(new IntPtr(addr)) + offsets.Dequeue();
    }
    return new IntPtr(addr);
}
```
【F:Foundry.Autocrat/Memory/PointerLibrary.cs†L34-L46】

Parsing of the `pointerPath` string (e.g. "Base/Child/SubChild") reads the base address and successive offsets from the XML document:

```csharp
private Tuple<int, Queue<int>> ParsePointerPath(string pointerPath)
{
    string[] parts = pointerPath.Split('/');
    var node = PointerDocument.Root.Elements("pointer")
                    .First(e => e.Attribute("name").Value == parts[0]);
    int baseAddr = int.Parse(node.Attribute("address").Value, NumberStyles.HexNumber);
    var offsets = new Queue<int>();
    for (int i = 1; i < parts.Length; i++)
    {
        var off = node.Elements("offset").First(e => e.Attribute("name").Value == parts[i]);
        offsets.Enqueue(int.Parse(off.Attribute("value").Value, NumberStyles.HexNumber));
    }
    return Tuple.Create(baseAddr, offsets);
}
```
【F:Foundry.Autocrat/Memory/PointerLibrary.cs†L74-L103】

Helper methods `Read<T>`, `Write`, and `ReadString` wrap `Resolve()` to simplify memory I/O:

```csharp
public T Read<T>(Process process, string pointerPath) where T : struct
    => process.Read<T>(Resolve(process, pointerPath));

public void Write(Process process, string pointerPath, byte[] buffer)
    => process.Write(Resolve(process, pointerPath), buffer);
```
【F:Foundry.Autocrat/Memory/PointerLibrary.cs†L105-L118】