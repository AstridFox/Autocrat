# Walkpath & Navigation Workflows

Walkpaths represent recorded routes through the game world and drive automated movement.

## Walkpath Data

```csharp
public class Walkpath
{
    public string Name { get; set; }
    public string Zone { get; set; }
    public List<Waypoint> Waypoints { get; } = new List<Waypoint>();
}
```
【F:Foundry.Autocrat.Everquest2/Navigation/Walkpath/Walkpath.cs†L1-L18】

```csharp
public class Waypoint
{
    public Vector3 Vector { get; set; }
    public bool JumpWhenReached { get; set; }
    public bool IsSafe { get; set; }
    public Camera Camera { get; set; }
}
```
【F:Foundry.Autocrat.Everquest2/Navigation/Walkpath/Waypoint.cs†L1-L18】

## Serialization to .dat Format

```csharp
public static Walkpath LoadWalkpath(string filePath)
{
    using (var sr = new StreamReader(filePath))
    {
        var wp = new Walkpath();
        wp.Name = sr.ReadLine();
        wp.Zone = sr.ReadLine();
        int count = int.Parse(sr.ReadLine());
        while (count-- > 0)
        {
            float x = float.Parse(sr.ReadLine());
            float y = float.Parse(sr.ReadLine());
            float z = float.Parse(sr.ReadLine());
            bool j = bool.Parse(sr.ReadLine());
            bool s = bool.Parse(sr.ReadLine());
            bool hasCam = bool.Parse(sr.ReadLine());
            Camera cam = hasCam ? new Camera(float.Parse(sr.ReadLine()), float.Parse(sr.ReadLine()), float.Parse(sr.ReadLine())) : null;
            var point = new Waypoint { Vector = new Vector3(x, y, z), JumpWhenReached = j, IsSafe = s, Camera = cam };
            wp.Waypoints.Add(point);
        }
        return wp;
    }
}
```
【F:Foundry.Autocrat.Everquest2/Navigation/Walkpath/Serialization/WalkpathSerializer.cs†L35-L70】

## Navigation Workflow

```csharp
protected override IEnumerable<WorkItem> WorkFlow()
{
    for (int i = 0; i < Context.Walkpath.Waypoints.Count; i++)
    {
        CurrentWaypoint = Context.Walkpath.Waypoints[i];
        yield return WaypointChanged.Singleton;
        var walk = new WalkToWaypoint { Context = Context, Destination = CurrentWaypoint };
        do { yield return walk; } while (!walk.ReachedDestination);
        if (Context.JumpingAllowed && CurrentWaypoint.JumpWhenReached)
            yield return new Jump { Context = Context };
    }
    yield return new StopWalking { Context = Context };
}
```
【F:Foundry.Autocrat.Everquest2/Navigation/Walkpath/WalkpathNavigationWorkflow.cs†L20-L54】