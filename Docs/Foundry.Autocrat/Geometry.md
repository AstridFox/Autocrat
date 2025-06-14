# Geometry & Vector Math

The `Vector3` struct in `Foundry.Autocrat.Geometry` represents 3D coordinates and provides common spatial operations:

```csharp
public float DistanceTo(Vector3 other, bool ignoreZ)
{
    float dx = Math.Abs(X - other.X);
    float dy = Math.Abs(Y - other.Y);
    return (float)Math.Sqrt(dx*dx + dy*dy);
}

public float HeadingTo(Vector3 destination)
{
    var delta = new Vector(destination.X - X, destination.Y - Y);
    float angle = 90 - (float)(Math.Atan2(delta.Y, delta.X) * D2RADMUL);
    return angle.Wrap(360, false);
}
```
【F:Foundry.Autocrat/Geometry/Vector3.cs†L96-L115】【F:Foundry.Autocrat/Geometry/Vector3.cs†L124-L133】

For 2D vector operations and conversions, see `VectorExtensions`:

```csharp
public static PointF ToPointF(this Vector v) => new PointF(v.X, v.Y);
public static Vector ToVector(this PointF p) => new Vector(p.X, p.Y);
```
【F:Foundry.Autocrat/Geometry/Extensions/VectorExtensions.cs†L7-L16】