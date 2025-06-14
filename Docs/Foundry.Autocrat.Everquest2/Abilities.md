# Abilities & Chains

Ability chains define ordered sequences of in-game actions (spells, attacks) and handle cooldowns and durations.

## Ability & AbilityChain

```csharp
public class Ability
{
    public string Name { get; set; }
    public TimeSpan ActivateTime { get; set; }
    public TimeSpan DurationTime { get; set; }
    public TimeSpan RechargeTime { get; set; }
    public Keyboard.KeyCombo KeyCombo { get; set; }

    public void Activate() => LastActivationTime = DateTime.Now;
    public bool IsReady => (DateTime.Now - LastActivationTime) > (ActivateTime + RechargeTime);
    public bool IsActive => (DateTime.Now - LastActivationTime) > ActivateTime && (DateTime.Now - LastActivationTime) < (ActivateTime + DurationTime);
}
```
【F:Foundry.Autocrat.Everquest2/Abilities/Ability.cs†L20-L44】

```csharp
public class AbilityChain : List<Ability>
{
    public string Name { get; }
    public Ability GetNextAvailableAbility(bool ignoreDuration)
        => this.FirstOrDefault(a => a.Enabled && a.IsReady && (ignoreDuration || !a.IsActive));
}
```
【F:Foundry.Autocrat.Everquest2/Abilities/AbilityChain.cs†L1-L20】

## Serialization

Chains are loaded/saved to XML via LINQ to XML:

```csharp
public static AbilityChain LoadAbilityChain(string filePath)
{
    var doc = XDocument.Load(filePath);
    var chain = doc.Element("AbilityChain");
    var ab = new AbilityChain(chain.Attribute("Name").Value);
    foreach (var x in chain.Element("Abilities").Elements("Ability"))
    {
        ab.Add(new Ability(
            x.Attribute("Name").Value,
            TimeSpan.Parse(x.Element("ActivateTime").Value),
            TimeSpan.Parse(x.Element("DurationTime").Value),
            TimeSpan.Parse(x.Element("RechargeTime").Value),
            Keyboard.KeyCombo.Parse(x.Element("KeyCombo").Value)
        ));
    }
    return ab;
}
```
【F:Foundry.Autocrat.Everquest2/Abilities/Serialization/AbilityChainSerializer.cs†L1-L30】