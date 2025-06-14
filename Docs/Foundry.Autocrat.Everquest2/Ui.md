# UI Dialogs & Editors

This project uses WinForms dialogs to edit abilities, walkpaths, and chains.

## Ability Editor Dialog

```csharp
public static Ability EditAbility(Ability oldAbility)
{
    var a = oldAbility ?? new Ability("Unnamed", TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, new KeyCombo());
    using (var dlg = new AbilityEditorDialog(a))
    {
        return dlg.ShowDialog() == DialogResult.OK ? dlg.Ability : oldAbility;
    }
}
```
【F:Foundry.Autocrat.Everquest2/Abilities/UI/AbilityEditorDialog.cs†L20-L30】

On save, the dialog applies validated values back to the `Ability` object:

```csharp
Ability.Name = AbilityNameTextbox.Text;
Ability.KeyCombo = KeyCombo;
Ability.ActivateTime = activate;
Ability.DurationTime = duration;
Ability.RechargeTime = recharge;
DialogResult = DialogResult.OK;
Close();
```
【F:Foundry.Autocrat.Everquest2/Abilities/UI/AbilityEditorDialog.cs†L150-L158】