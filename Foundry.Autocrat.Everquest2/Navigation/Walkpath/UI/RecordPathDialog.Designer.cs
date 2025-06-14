namespace Foundry.Autocrat.Everquest2.Navigation.Walkpath.UI
{
    partial class RecordPathDialog
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
            this.RecordWorker = new System.ComponentModel.BackgroundWorker();
            this.CurrentZoneUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.WalkpathNameTextbox = new System.Windows.Forms.TextBox();
            this.NumWaypointsLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentZoneLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.MarkAsSafeButton = new System.Windows.Forms.Button();
            this.RecordButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.DetectJumpsCheckbox = new System.Windows.Forms.CheckBox();
            this.Status = new System.Windows.Forms.StatusStrip();
            this.CurrentLocationLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CurrentLocationUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.MarkedSafeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SafeWaypointMarkerTimer = new System.Windows.Forms.Timer(this.components);
            this.Status.SuspendLayout();
            this.SuspendLayout();
            // 
            // RecordWorker
            // 
            this.RecordWorker.WorkerReportsProgress = true;
            this.RecordWorker.WorkerSupportsCancellation = true;
            this.RecordWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.RecordWorker_DoWork);
            this.RecordWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.RecordWorker_ProgressChanged);
            // 
            // CurrentZoneUpdateTimer
            // 
            this.CurrentZoneUpdateTimer.Enabled = true;
            this.CurrentZoneUpdateTimer.Interval = 500;
            this.CurrentZoneUpdateTimer.Tick += new System.EventHandler(this.CurrentZoneUpdateTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Walkpath Name:";
            // 
            // WalkpathNameTextbox
            // 
            this.WalkpathNameTextbox.Location = new System.Drawing.Point(105, 6);
            this.WalkpathNameTextbox.Name = "WalkpathNameTextbox";
            this.WalkpathNameTextbox.Size = new System.Drawing.Size(196, 20);
            this.WalkpathNameTextbox.TabIndex = 8;
            // 
            // NumWaypointsLabel
            // 
            this.NumWaypointsLabel.AutoSize = true;
            this.NumWaypointsLabel.Location = new System.Drawing.Point(102, 31);
            this.NumWaypointsLabel.Name = "NumWaypointsLabel";
            this.NumWaypointsLabel.Size = new System.Drawing.Size(13, 13);
            this.NumWaypointsLabel.TabIndex = 9;
            this.NumWaypointsLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Num Waypoints:";
            // 
            // CurrentZoneLabel
            // 
            this.CurrentZoneLabel.AutoSize = true;
            this.CurrentZoneLabel.Location = new System.Drawing.Point(12, 51);
            this.CurrentZoneLabel.Name = "CurrentZoneLabel";
            this.CurrentZoneLabel.Size = new System.Drawing.Size(72, 13);
            this.CurrentZoneLabel.TabIndex = 10;
            this.CurrentZoneLabel.Text = "Current Zone:";
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(145, 70);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 11;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // MarkAsSafeButton
            // 
            this.MarkAsSafeButton.Enabled = false;
            this.MarkAsSafeButton.Location = new System.Drawing.Point(15, 99);
            this.MarkAsSafeButton.Name = "MarkAsSafeButton";
            this.MarkAsSafeButton.Size = new System.Drawing.Size(286, 23);
            this.MarkAsSafeButton.TabIndex = 11;
            this.MarkAsSafeButton.Text = "Mark As Safe Spot";
            this.MarkAsSafeButton.UseVisualStyleBackColor = true;
            this.MarkAsSafeButton.Click += new System.EventHandler(this.MarkAsSafeButton_Click);
            // 
            // RecordButton
            // 
            this.RecordButton.Location = new System.Drawing.Point(15, 70);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(124, 23);
            this.RecordButton.TabIndex = 12;
            this.RecordButton.Text = "Record";
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.RecordButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(226, 70);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 13;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // DetectJumpsCheckbox
            // 
            this.DetectJumpsCheckbox.AutoSize = true;
            this.DetectJumpsCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DetectJumpsCheckbox.Location = new System.Drawing.Point(210, 30);
            this.DetectJumpsCheckbox.Name = "DetectJumpsCheckbox";
            this.DetectJumpsCheckbox.Size = new System.Drawing.Size(91, 17);
            this.DetectJumpsCheckbox.TabIndex = 14;
            this.DetectJumpsCheckbox.Text = "Detect Jumps";
            this.DetectJumpsCheckbox.UseVisualStyleBackColor = true;
            // 
            // Status
            // 
            this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentLocationLabel,
            this.MarkedSafeLabel});
            this.Status.Location = new System.Drawing.Point(0, 130);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(319, 22);
            this.Status.TabIndex = 15;
            // 
            // CurrentLocationLabel
            // 
            this.CurrentLocationLabel.Name = "CurrentLocationLabel";
            this.CurrentLocationLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // CurrentLocationUpdateTimer
            // 
            this.CurrentLocationUpdateTimer.Enabled = true;
            this.CurrentLocationUpdateTimer.Interval = 25;
            this.CurrentLocationUpdateTimer.Tick += new System.EventHandler(this.CurrentLocationUpdateTimer_Tick);
            // 
            // MarkedSafeLabel
            // 
            this.MarkedSafeLabel.Name = "MarkedSafeLabel";
            this.MarkedSafeLabel.Size = new System.Drawing.Size(128, 17);
            this.MarkedSafeLabel.Text = "Waypoint marked safe!";
            this.MarkedSafeLabel.Visible = false;
            // 
            // SafeWaypointMarkerTimer
            // 
            this.SafeWaypointMarkerTimer.Interval = 2500;
            this.SafeWaypointMarkerTimer.Tick += new System.EventHandler(this.SafeWaypointMarkerTimer_Tick);
            // 
            // RecordPathDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 152);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.DetectJumpsCheckbox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.RecordButton);
            this.Controls.Add(this.MarkAsSafeButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.CurrentZoneLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NumWaypointsLabel);
            this.Controls.Add(this.WalkpathNameTextbox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "RecordPathDialog";
            this.Text = "Walkpath Recorder";
            this.TopMost = true;
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker RecordWorker;
        private System.Windows.Forms.Timer CurrentZoneUpdateTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox WalkpathNameTextbox;
        private System.Windows.Forms.Label NumWaypointsLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CurrentZoneLabel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button MarkAsSafeButton;
        private System.Windows.Forms.Button RecordButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.CheckBox DetectJumpsCheckbox;
        private System.Windows.Forms.StatusStrip Status;
        private System.Windows.Forms.ToolStripStatusLabel CurrentLocationLabel;
        private System.Windows.Forms.Timer CurrentLocationUpdateTimer;
        private System.Windows.Forms.ToolStripStatusLabel MarkedSafeLabel;
        private System.Windows.Forms.Timer SafeWaypointMarkerTimer;
    }
}