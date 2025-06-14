# Logging & Configuration

`Foundry.Autocrat.Everquest2` writes timestamped entries via `LogService`, and loads runtime settings from `ReaperConfiguration`.

## LogService

Log entries are appended with timestamps:

```csharp
public void Log(string text)
{
    using (var sw = File.AppendText(LogFilePath))
        sw.WriteLine($"{DateTime.Now:O} {text}");
}
```
【F:Foundry.Autocrat.Everquest2/LogFiles/LogService.cs†L10-L18】

## ReaperConfiguration

Configuration is read at startup to drive filters and thresholds:

```csharp
public ReaperConfiguration(string configPath)
{
    var xml = XDocument.Load(configPath);
    HarvestInterval = TimeSpan.Parse(xml.Root.Element("HarvestInterval").Value);
    // Additional settings...
}
```
【F:Foundry.Autocrat.Reaper/Configuration/ReaperConfiguration.cs†L10-L25】