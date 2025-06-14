# Extension Methods & Helpers

`Foundry.Autocrat.Extensions` collects small utility methods that simplify everyday tasks.

## Fluent Tuple Creation

```csharp
public static class FluentExtensions
{
    public static Tuple<T1, T2> Tuple<T1, T2>(T1 v1, T2 v2)
        => Tuple.Create(v1, v2);
}
```
【F:Foundry.Autocrat/Extensions/FluentExtensions.cs†L1-L10】

## TimeSpan Helpers

```csharp
public static TimeSpan Milliseconds(this int ms) => TimeSpan.FromMilliseconds(ms);
public static string ToPrettyString(this TimeSpan ts)
{
    if (ts.Milliseconds != 0) return ts.TotalMilliseconds + "ms";
    if (ts.Seconds != 0) return ts.TotalSeconds + "s";
    // ... hours and days omitted
    return "0s";
}
```
【F:Foundry.Autocrat/Extensions/TimeExtensions.cs†L6-L18】【F:Foundry.Autocrat/Extensions/TimeExtensions.cs†L28-L38】

## Regex & Parsing Shortcuts

```csharp
public static Match MatchOrDefault(this Regex r, string input)
    => r.Match(input);

public static bool TryParseOrDefault<T>(this string s, out T result)
    where T : struct
    => Enum.TryParse(s, out result);
```
【F:Foundry.Autocrat/Extensions/RegexExtensions.cs†L10-L18】【F:Foundry.Autocrat/Extensions/ParseExtensions.cs†L10-L18】