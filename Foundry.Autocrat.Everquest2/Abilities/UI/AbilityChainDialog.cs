using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Foundry.Autocrat.Extensions.Time;

namespace Foundry.Autocrat.Everquest2.Abilities.UI
{
    public partial class AbilityChainDialog : Form
    {
        private AbilityChain AbilityChain;

        public AbilityChainDialog(AbilityChain chain)
        {
            InitializeComponent();
            if (chain == null)
            {
                AbilityChain = new AbilityChain("Unnamed");
            }
            else
            {
                AbilityChain = new AbilityChain(chain.Name);
                AbilityChain.AddRange(chain);
            }

            UpButton.Enabled = false;
            DownButton.Enabled = false;
            EditButton.Enabled = false;
            DeleteButton.Enabled = false;

            UpdateUI();
        }

        public static AbilityChain ShowAbilityChainDialog(AbilityChain chain)
        {
            var acd = new AbilityChainDialog(chain);

            if (acd.ShowDialog() == DialogResult.OK)
            {
                return acd.AbilityChain;
            }
            else
            {
                return chain;
            }
        }

        private void UpdateUI()
        {
            AbilityChainNameTextbox.Text = AbilityChain.Name;

            AbilityListView.BeginUpdate();
            AbilityListView.Items.Clear();

            int ct = 1;
            foreach (var abil in AbilityChain)
            {
                var items = new string[] {
                    ct.ToString(),
                    abil.Name,
                    abil.Enabled ? "Yes" : "No",
                    abil.KeyCombo.ToScreenString(),
                    abil.ActivateTime.ToPrettyString(),
                    abil.DurationTime.ToPrettyString(),
                    abil.RechargeTime.ToPrettyString()
                };

                AbilityListView.Items.Add(new ListViewItem(items));
                ct++;
            }

            AbilityListView.EndUpdate();
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            if (AbilityListView.SelectedItems.Count == 0) return;

            var index = AbilityListView.SelectedIndices[0];
            var abil = AbilityChain[index];

            AbilityChain.RemoveAt(index);
            AbilityChain.Insert(index - 1, abil);

            var lvi = AbilityListView.Items[index];
            AbilityListView.Items.RemoveAt(index);
            AbilityListView.Items.Insert(index - 1, lvi);
            AbilityListView.Items[index - 1].EnsureVisible();
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            if (AbilityListView.SelectedItems.Count == 0) return;

            var index = AbilityListView.SelectedIndices[0];
            var abil = AbilityChain[index];

            AbilityChain.RemoveAt(index);
            AbilityChain.Insert(index + 1, abil);

            var lvi = AbilityListView.Items[index];
            AbilityListView.Items.RemoveAt(index);
            AbilityListView.Items.Insert(index + 1, lvi);
            AbilityListView.Items[index + 1].EnsureVisible();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (AbilityListView.SelectedItems.Count == 0) return;

            var index = AbilityListView.SelectedIndices[0];
            var abil = AbilityChain[index];

            AbilityChain.RemoveAt(index);

            UpdateUI();
            if (AbilityChain.Count == 0)
            {
                DeleteButton.Enabled = false;
                EditButton.Enabled = false;
                return;
            }
            if (index == AbilityChain.Count) index--;
            AbilityListView.Items[index].Selected = true;
        }

        private void AbilityListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpButton.Enabled = true;
            DownButton.Enabled = true;
            EditButton.Enabled = true;
            DeleteButton.Enabled = true;

            if (AbilityListView.SelectedIndices.Count == 0)
            {
                UpButton.Enabled = false;
                DownButton.Enabled = false;
                EditButton.Enabled = false;
                DeleteButton.Enabled = false;
            }
            else
            {
                if (AbilityListView.SelectedIndices[0] == 0)
                {
                    UpButton.Enabled = false;
                }
                if (AbilityListView.SelectedIndices[0] == AbilityChain.Count - 1)
                {
                    DownButton.Enabled = false;
                }
            }



        }

        private void AbilityChainNameTextbox_TextChanged(object sender, EventArgs e)
        {
            AbilityChain.Name = AbilityChainNameTextbox.Text;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (AbilityListView.SelectedItems.Count == 0) return;

            var index = AbilityListView.SelectedIndices[0];
            var oldAbil = AbilityChain[index];

            var newAbil = EditAbility(oldAbil);

            var lvi = AbilityListView.SelectedItems[0];
            
            lvi.SubItems[1].Text =  newAbil.Name;
            lvi.SubItems[2].Text = newAbil.Enabled ? "Yes" : "No";
            lvi.SubItems[3].Text = newAbil.KeyCombo.ToScreenString();
            lvi.SubItems[4].Text = newAbil.ActivateTime.ToPrettyString();
            lvi.SubItems[5].Text = newAbil.DurationTime.ToPrettyString();
            lvi.SubItems[6].Text = newAbil.RechargeTime.ToPrettyString();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            var newAbil = EditAbility(null);
            if (newAbil == null) return;

            AbilityChain.Add(newAbil);

            var items = new string[] {
                    AbilityChain.Count.ToString(),
                    newAbil.Name,
                    newAbil.Enabled ? "Yes" : "No",
                    newAbil.KeyCombo.ToScreenString(),
                    newAbil.ActivateTime.ToPrettyString(),
                    newAbil.DurationTime.ToPrettyString(),
                    newAbil.RechargeTime.ToPrettyString()
                };
            var lvi = new ListViewItem(items);

            AbilityListView.Items.Add(lvi);

            lvi.EnsureVisible();
        }

        private Ability EditAbility(Ability currentAbility)
        {
            return AbilityEditorDialog.EditAbility(currentAbility);
        }

        private void AbilityListView_DoubleClick(object sender, EventArgs e)
        {
            EditButton_Click(sender, e);
        }

    }
}
