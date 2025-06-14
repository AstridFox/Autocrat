namespace Foundry.Autocrat.Everquest2.Abilities.UI
{
    partial class AbilityChainDialog
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
            System.Windows.Forms.Button CancelButton;
            this.AbilityListView = new System.Windows.Forms.ListView();
            this.NumberColumn = new System.Windows.Forms.ColumnHeader();
            this.AbilityNameColumn = new System.Windows.Forms.ColumnHeader();
            this.EnabledColumn = new System.Windows.Forms.ColumnHeader();
            this.KeyComboColumn = new System.Windows.Forms.ColumnHeader();
            this.UseTimeColumn = new System.Windows.Forms.ColumnHeader();
            this.DurationTimeColumn = new System.Windows.Forms.ColumnHeader();
            this.RecastTimeColumn = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.AbilityChainNameTextbox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.UpButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.NewButton = new System.Windows.Forms.Button();
            CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            CancelButton.Location = new System.Drawing.Point(436, 3);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new System.Drawing.Size(75, 23);
            CancelButton.TabIndex = 6;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            // 
            // AbilityListView
            // 
            this.AbilityListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AbilityListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NumberColumn,
            this.AbilityNameColumn,
            this.EnabledColumn,
            this.KeyComboColumn,
            this.UseTimeColumn,
            this.DurationTimeColumn,
            this.RecastTimeColumn});
            this.AbilityListView.FullRowSelect = true;
            this.AbilityListView.GridLines = true;
            this.AbilityListView.HideSelection = false;
            this.AbilityListView.Location = new System.Drawing.Point(12, 32);
            this.AbilityListView.MultiSelect = false;
            this.AbilityListView.Name = "AbilityListView";
            this.AbilityListView.Size = new System.Drawing.Size(499, 192);
            this.AbilityListView.TabIndex = 0;
            this.AbilityListView.UseCompatibleStateImageBehavior = false;
            this.AbilityListView.View = System.Windows.Forms.View.Details;
            this.AbilityListView.SelectedIndexChanged += new System.EventHandler(this.AbilityListView_SelectedIndexChanged);
            this.AbilityListView.DoubleClick += new System.EventHandler(this.AbilityListView_DoubleClick);
            // 
            // NumberColumn
            // 
            this.NumberColumn.Text = "#";
            this.NumberColumn.Width = 20;
            // 
            // AbilityNameColumn
            // 
            this.AbilityNameColumn.Text = "Ability Name";
            this.AbilityNameColumn.Width = 150;
            // 
            // EnabledColumn
            // 
            this.EnabledColumn.Text = "On";
            this.EnabledColumn.Width = 30;
            // 
            // KeyComboColumn
            // 
            this.KeyComboColumn.Text = "Keys";
            this.KeyComboColumn.Width = 90;
            // 
            // UseTimeColumn
            // 
            this.UseTimeColumn.Text = "Activation";
            this.UseTimeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DurationTimeColumn
            // 
            this.DurationTimeColumn.Text = "Duration";
            this.DurationTimeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RecastTimeColumn
            // 
            this.RecastTimeColumn.Text = "Recast";
            this.RecastTimeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // AbilityChainNameTextbox
            // 
            this.AbilityChainNameTextbox.Location = new System.Drawing.Point(53, 6);
            this.AbilityChainNameTextbox.Name = "AbilityChainNameTextbox";
            this.AbilityChainNameTextbox.Size = new System.Drawing.Size(160, 20);
            this.AbilityChainNameTextbox.TabIndex = 0;
            this.AbilityChainNameTextbox.TextChanged += new System.EventHandler(this.AbilityChainNameTextbox_TextChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveButton.Location = new System.Drawing.Point(517, 3);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // UpButton
            // 
            this.UpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpButton.Location = new System.Drawing.Point(517, 87);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(75, 23);
            this.UpButton.TabIndex = 2;
            this.UpButton.Text = "/\\";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // DownButton
            // 
            this.DownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DownButton.Location = new System.Drawing.Point(517, 145);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(75, 23);
            this.DownButton.TabIndex = 4;
            this.DownButton.Text = "\\/";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EditButton.Location = new System.Drawing.Point(517, 116);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(75, 23);
            this.EditButton.TabIndex = 3;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton.Location = new System.Drawing.Point(517, 201);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 5;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // NewButton
            // 
            this.NewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewButton.Location = new System.Drawing.Point(517, 32);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(75, 23);
            this.NewButton.TabIndex = 1;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // AbilityChainDialog
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = CancelButton;
            this.ClientSize = new System.Drawing.Size(601, 233);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.AbilityChainNameTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AbilityListView);
            this.Name = "AbilityChainDialog";
            this.Text = "Edit Ability Chain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView AbilityListView;
        private System.Windows.Forms.ColumnHeader NumberColumn;
        private System.Windows.Forms.ColumnHeader AbilityNameColumn;
        private System.Windows.Forms.ColumnHeader KeyComboColumn;
        private System.Windows.Forms.ColumnHeader UseTimeColumn;
        private System.Windows.Forms.ColumnHeader DurationTimeColumn;
        private System.Windows.Forms.ColumnHeader RecastTimeColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AbilityChainNameTextbox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button NewButton;
        private System.Windows.Forms.ColumnHeader EnabledColumn;

    }
}