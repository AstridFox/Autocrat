using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Foundry.Autocrat.Everquest2.Navigation.Walkpath.UI
{
    public partial class RenamePathDialog : Form
    {
        public RenamePathDialog()
        {
            InitializeComponent();
        }

        bool changed = false;

        public static string RenamePath(string oldName)
        {
            var rpd = new RenamePathDialog();
            rpd.NewNameTextbox.Text = oldName;

            if (rpd.ShowDialog() == DialogResult.OK)
            {
                if (rpd.changed) return rpd.NewNameTextbox.Text;
            }

            return oldName;
        }

        private void NewNameTextbox_Enter(object sender, EventArgs e)
        {
            if (!changed) NewNameTextbox.Text = "";
        }

        private void NewNameTextbox_TextChanged(object sender, EventArgs e)
        {
            changed = true;
        }

    }
}
