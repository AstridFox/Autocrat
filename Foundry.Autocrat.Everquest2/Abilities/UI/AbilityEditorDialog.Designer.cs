namespace Foundry.Autocrat.Everquest2.Abilities.UI
{
    partial class AbilityEditorDialog
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
            System.Windows.Forms.Button CancelButton;
            this.SaveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AbilityNameTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.KeysTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ActivateTimeTextbox = new System.Windows.Forms.TextBox();
            this.DurationTextbox = new System.Windows.Forms.TextBox();
            this.RecastTimeTextbox = new System.Windows.Forms.TextBox();
            this.ParseErrorTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.EnabledCheckbox = new System.Windows.Forms.CheckBox();
            CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            CancelButton.Location = new System.Drawing.Point(206, 82);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new System.Drawing.Size(75, 23);
            CancelButton.TabIndex = 5;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(287, 82);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ability Name";
            // 
            // AbilityNameTextbox
            // 
            this.AbilityNameTextbox.Location = new System.Drawing.Point(15, 25);
            this.AbilityNameTextbox.Name = "AbilityNameTextbox";
            this.AbilityNameTextbox.Size = new System.Drawing.Size(175, 20);
            this.AbilityNameTextbox.TabIndex = 0;
            this.AbilityNameTextbox.TextChanged += new System.EventHandler(this.AbilityNameTextbox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Keys";
            // 
            // KeysTextbox
            // 
            this.KeysTextbox.Location = new System.Drawing.Point(15, 71);
            this.KeysTextbox.Name = "KeysTextbox";
            this.KeysTextbox.Size = new System.Drawing.Size(83, 20);
            this.KeysTextbox.TabIndex = 1;
            this.KeysTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeysTextbox_KeyDown);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(192, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Activate Time";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(192, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Duration";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(192, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Recast Time";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ActivateTimeTextbox
            // 
            this.ActivateTimeTextbox.Location = new System.Drawing.Point(279, 9);
            this.ActivateTimeTextbox.Name = "ActivateTimeTextbox";
            this.ActivateTimeTextbox.Size = new System.Drawing.Size(83, 20);
            this.ActivateTimeTextbox.TabIndex = 2;
            this.ActivateTimeTextbox.TextChanged += new System.EventHandler(this.ActivateTimeTextbox_TextChanged);
            this.ActivateTimeTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ActivateTimeTextbox_KeyPress);
            // 
            // DurationTextbox
            // 
            this.DurationTextbox.Location = new System.Drawing.Point(279, 32);
            this.DurationTextbox.Name = "DurationTextbox";
            this.DurationTextbox.Size = new System.Drawing.Size(83, 20);
            this.DurationTextbox.TabIndex = 3;
            this.DurationTextbox.TextChanged += new System.EventHandler(this.DurationTextbox_TextChanged);
            this.DurationTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ActivateTimeTextbox_KeyPress);
            // 
            // RecastTimeTextbox
            // 
            this.RecastTimeTextbox.Location = new System.Drawing.Point(279, 55);
            this.RecastTimeTextbox.Name = "RecastTimeTextbox";
            this.RecastTimeTextbox.Size = new System.Drawing.Size(83, 20);
            this.RecastTimeTextbox.TabIndex = 4;
            this.RecastTimeTextbox.TextChanged += new System.EventHandler(this.RecastTimeTextbox_TextChanged);
            this.RecastTimeTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ActivateTimeTextbox_KeyPress);
            // 
            // ParseErrorTooltip
            // 
            this.ParseErrorTooltip.IsBalloon = true;
            this.ParseErrorTooltip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            this.ParseErrorTooltip.ToolTipTitle = "Oops";
            // 
            // EnabledCheckbox
            // 
            this.EnabledCheckbox.AutoSize = true;
            this.EnabledCheckbox.Location = new System.Drawing.Point(125, 73);
            this.EnabledCheckbox.Name = "EnabledCheckbox";
            this.EnabledCheckbox.Size = new System.Drawing.Size(65, 17);
            this.EnabledCheckbox.TabIndex = 10;
            this.EnabledCheckbox.Text = "Enabled";
            this.EnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // AbilityEditorDialog
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = CancelButton;
            this.ClientSize = new System.Drawing.Size(374, 117);
            this.Controls.Add(this.EnabledCheckbox);
            this.Controls.Add(this.RecastTimeTextbox);
            this.Controls.Add(this.DurationTextbox);
            this.Controls.Add(this.ActivateTimeTextbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.KeysTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AbilityNameTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Name = "AbilityEditorDialog";
            this.Text = "Edit Ability";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AbilityNameTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox KeysTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ActivateTimeTextbox;
        private System.Windows.Forms.TextBox DurationTextbox;
        private System.Windows.Forms.TextBox RecastTimeTextbox;
        private System.Windows.Forms.ToolTip ParseErrorTooltip;
        private System.Windows.Forms.CheckBox EnabledCheckbox;
    }
}