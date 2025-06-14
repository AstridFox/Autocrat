using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Foundry.Autocrat.Automation.Windows;
using Foundry.Autocrat.Extensions.Time;

namespace Foundry.Autocrat.Everquest2.Abilities.UI
{
    public partial class AbilityEditorDialog : Form
    {
        private Ability Ability;

        private AbilityEditorDialog(Ability abil)
        {
            InitializeComponent();
            Ability = abil;
            KeyCombo = abil.KeyCombo;

            UpdateUI();
        }

        public static Ability EditAbility(Ability oldAbility)
        {
            Ability a;

            if (oldAbility == null)
                a = new Ability("Unnamed", TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, new Keyboard.KeyCombo());
            else
                a = oldAbility;

            AbilityEditorDialog edg = new AbilityEditorDialog(a);

            if (edg.ShowDialog() == DialogResult.OK)
                return edg.Ability;
            else
                return oldAbility;
        }

        private void UpdateUI()
        {
            AbilityNameTextbox.Text = Ability.Name;
            KeysTextbox.Text = Ability.KeyCombo.ToScreenString();
            ActivateTimeTextbox.Text = Ability.ActivateTime.ToPrettyString();
            DurationTextbox.Text = Ability.DurationTime.ToPrettyString();
            RecastTimeTextbox.Text = Ability.RechargeTime.ToPrettyString();
            EnabledCheckbox.Checked = Ability.Enabled;
        }

        private void AbilityNameTextbox_TextChanged(object sender, EventArgs e)
        {
            Ability.Name = AbilityNameTextbox.Text;
        }

        private void ActivateTimeTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') { e.Handled = true; return; }
            if (!Char.IsLetterOrDigit(e.KeyChar)) return;

            TextBox sT = ((TextBox)sender);
            string curText = sT.Text;
            bool expectingDigit = false;
            bool canAcceptTime = false;
            bool canAcceptOnlyS = false;

            if (curText == "") { expectingDigit = true; canAcceptTime = false; canAcceptOnlyS = false; }
            else if (curText.ToCharArray().All(c => Char.IsNumber(c))) { expectingDigit = true; canAcceptTime = true; canAcceptOnlyS = false; }
            else if (curText.ToCharArray().Last() == 'm') { expectingDigit = false; canAcceptTime = true; canAcceptOnlyS = true; }

            if (e.KeyChar >= '0' && e.KeyChar <= '9' && expectingDigit) return;
            if ((e.KeyChar == 'm' || e.KeyChar == 'h') && (canAcceptTime && !canAcceptOnlyS)) return;
            if (e.KeyChar == 's' && canAcceptTime) return;

            e.Handled = true;
        }

        private void ActivateTimeTextbox_TextChanged(object sender, EventArgs e)
        {
            TimeSpan activate;
            if (!TimeExtensions.TryParsePrettyString(ActivateTimeTextbox.Text, out activate))
            {
                ActivateTimeTextbox.BackColor = Color.Pink;
            }
            else
            {
                ActivateTimeTextbox.BackColor = SystemColors.Window;
            }
        }

        private void DurationTextbox_TextChanged(object sender, EventArgs e)
        {
            TimeSpan duration;
            if (!TimeExtensions.TryParsePrettyString(DurationTextbox.Text, out duration))
            {
                DurationTextbox.BackColor = Color.Pink;
            }
            else
            {
                DurationTextbox.BackColor = SystemColors.Window;
            }
        }

        private void RecastTimeTextbox_TextChanged(object sender, EventArgs e)
        {
            TimeSpan recast;
            if (!TimeExtensions.TryParsePrettyString(RecastTimeTextbox.Text, out recast))
            {
                RecastTimeTextbox.BackColor = Color.Pink;
            }
            else
            {
                RecastTimeTextbox.BackColor = SystemColors.Window;
            }
        }

        private Keyboard.KeyCombo KeyCombo;

        private void KeysTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;

            Keyboard.Keys k = (Keyboard.Keys)e.KeyValue;
            if (k == Keyboard.Keys.Control || k == Keyboard.Keys.Menu || k == Keyboard.Keys.Shift) return;

            KeyCombo = new Keyboard.KeyCombo(k, e.Alt, e.Shift, e.Control);
            KeysTextbox.Text = KeyCombo.ToScreenString();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            TimeSpan activate;
            TimeSpan duration;
            TimeSpan recharge;

            bool aBad, dBad, rBad;

            aBad = !TimeExtensions.TryParsePrettyString(ActivateTimeTextbox.Text, out activate);
            dBad = !TimeExtensions.TryParsePrettyString(DurationTextbox.Text, out duration);
            rBad = !TimeExtensions.TryParsePrettyString(RecastTimeTextbox.Text, out recharge);

            if (aBad | dBad | rBad)
            {
                string pop = "Error parsing the following:\r\n";
                List<string> bads = new List<string>();
                if (aBad) bads.Add("Activate");
                if (dBad) bads.Add("Duration");
                if (rBad) bads.Add("Recast");

                pop += string.Join(", ", bads.ToArray());

                pop += " timer" + (bads.Count > 1 ? "s." : ".");

                ParseErrorTooltip.Show(pop, this, SaveButton.Location);
            }
            else
            {
                Ability.Name = AbilityNameTextbox.Text;
                Ability.KeyCombo = KeyCombo;
                Ability.Enabled = EnabledCheckbox.Checked;
                Ability.ActivateTime = activate;
                Ability.DurationTime = duration;
                Ability.RechargeTime = recharge;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


    }
}
