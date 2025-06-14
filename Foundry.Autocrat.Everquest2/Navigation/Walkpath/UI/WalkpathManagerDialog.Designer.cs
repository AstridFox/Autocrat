namespace Foundry.Autocrat.Everquest2.Navigation.Walkpath.UI
{
    partial class WalkpathManagerDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("mesquite catch weed");
            this.PathsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.CurrentZoneLabel = new System.Windows.Forms.Label();
            this.FilterPathsByZoneCheckbox = new System.Windows.Forms.CheckBox();
            this.CurrentZoneUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.RenamePathButton = new System.Windows.Forms.Button();
            this.DeletePathButton = new System.Windows.Forms.Button();
            this.NewPathButton = new System.Windows.Forms.Button();
            this.LoadPathButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PathsListView
            // 
            this.PathsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PathsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.PathsListView.FullRowSelect = true;
            this.PathsListView.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.PathsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.PathsListView.LabelEdit = true;
            this.PathsListView.Location = new System.Drawing.Point(12, 28);
            this.PathsListView.MultiSelect = false;
            this.PathsListView.Name = "PathsListView";
            this.PathsListView.Size = new System.Drawing.Size(553, 154);
            this.PathsListView.TabIndex = 6;
            this.PathsListView.UseCompatibleStateImageBehavior = false;
            this.PathsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Path Name";
            this.columnHeader1.Width = 500;
            // 
            // CurrentZoneLabel
            // 
            this.CurrentZoneLabel.AutoSize = true;
            this.CurrentZoneLabel.Location = new System.Drawing.Point(12, 9);
            this.CurrentZoneLabel.Name = "CurrentZoneLabel";
            this.CurrentZoneLabel.Size = new System.Drawing.Size(72, 13);
            this.CurrentZoneLabel.TabIndex = 11;
            this.CurrentZoneLabel.Text = "Current Zone:";
            // 
            // FilterPathsByZoneCheckbox
            // 
            this.FilterPathsByZoneCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterPathsByZoneCheckbox.AutoSize = true;
            this.FilterPathsByZoneCheckbox.Checked = true;
            this.FilterPathsByZoneCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FilterPathsByZoneCheckbox.Location = new System.Drawing.Point(437, 8);
            this.FilterPathsByZoneCheckbox.Name = "FilterPathsByZoneCheckbox";
            this.FilterPathsByZoneCheckbox.Size = new System.Drawing.Size(128, 17);
            this.FilterPathsByZoneCheckbox.TabIndex = 12;
            this.FilterPathsByZoneCheckbox.Text = "Filter By Current Zone";
            this.FilterPathsByZoneCheckbox.UseVisualStyleBackColor = true;
            this.FilterPathsByZoneCheckbox.CheckedChanged += new System.EventHandler(this.FilterPathsByZoneCheckbox_CheckedChanged);
            // 
            // CurrentZoneUpdateTimer
            // 
            this.CurrentZoneUpdateTimer.Enabled = true;
            this.CurrentZoneUpdateTimer.Interval = 500;
            this.CurrentZoneUpdateTimer.Tick += new System.EventHandler(this.CurrentZoneUpdateTimer_Tick);
            // 
            // RenamePathButton
            // 
            this.RenamePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RenamePathButton.Location = new System.Drawing.Point(384, 188);
            this.RenamePathButton.Name = "RenamePathButton";
            this.RenamePathButton.Size = new System.Drawing.Size(85, 23);
            this.RenamePathButton.TabIndex = 15;
            this.RenamePathButton.Text = "Rename Path";
            this.RenamePathButton.UseVisualStyleBackColor = true;
            this.RenamePathButton.Click += new System.EventHandler(this.RenamePathButton_Click);
            // 
            // DeletePathButton
            // 
            this.DeletePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeletePathButton.Location = new System.Drawing.Point(298, 188);
            this.DeletePathButton.Name = "DeletePathButton";
            this.DeletePathButton.Size = new System.Drawing.Size(75, 23);
            this.DeletePathButton.TabIndex = 14;
            this.DeletePathButton.Text = "Delete Path";
            this.DeletePathButton.UseVisualStyleBackColor = true;
            this.DeletePathButton.Click += new System.EventHandler(this.DeletePathButton_Click);
            // 
            // NewPathButton
            // 
            this.NewPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NewPathButton.Location = new System.Drawing.Point(212, 188);
            this.NewPathButton.Name = "NewPathButton";
            this.NewPathButton.Size = new System.Drawing.Size(75, 23);
            this.NewPathButton.TabIndex = 13;
            this.NewPathButton.Text = "New Path";
            this.NewPathButton.UseVisualStyleBackColor = true;
            this.NewPathButton.Click += new System.EventHandler(this.NewPathButton_Click);
            // 
            // LoadPathButton
            // 
            this.LoadPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadPathButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.LoadPathButton.Location = new System.Drawing.Point(480, 188);
            this.LoadPathButton.Name = "LoadPathButton";
            this.LoadPathButton.Size = new System.Drawing.Size(85, 23);
            this.LoadPathButton.TabIndex = 15;
            this.LoadPathButton.Text = "Load Path";
            this.LoadPathButton.UseVisualStyleBackColor = true;
            // 
            // WalkpathManagerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 221);
            this.Controls.Add(this.LoadPathButton);
            this.Controls.Add(this.RenamePathButton);
            this.Controls.Add(this.DeletePathButton);
            this.Controls.Add(this.NewPathButton);
            this.Controls.Add(this.FilterPathsByZoneCheckbox);
            this.Controls.Add(this.CurrentZoneLabel);
            this.Controls.Add(this.PathsListView);
            this.Name = "WalkpathManagerDialog";
            this.Text = "Walkpath Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView PathsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label CurrentZoneLabel;
        private System.Windows.Forms.CheckBox FilterPathsByZoneCheckbox;
        private System.Windows.Forms.Timer CurrentZoneUpdateTimer;
        private System.Windows.Forms.Button RenamePathButton;
        private System.Windows.Forms.Button DeletePathButton;
        private System.Windows.Forms.Button NewPathButton;
        private System.Windows.Forms.Button LoadPathButton;
    }
}