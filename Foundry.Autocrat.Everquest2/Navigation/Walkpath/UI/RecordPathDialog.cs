using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Foundry.Autocrat.Automation.Windows;
using System.Threading;
using Foundry.Autocrat.Geometry;
using Foundry.Autocrat.Extensions.Geometry;

namespace Foundry.Autocrat.Everquest2.Navigation.Walkpath.UI
{
    public partial class RecordPathDialog : Form
    {
        private Func<string> GetCurrentZone;
        private Func<Vector3> GetCurrentLocation;
        private Func<Camera> GetCamera;
        private Walkpath Walkpath;

        private RecordPathDialog()
        {
            InitializeComponent();
        }

        public static Walkpath RecordPath(Func<Vector3> getCurrentLocation, Func<string> getCurrentZone, Func<Camera> getCamera)
        {
            var rpd = new RecordPathDialog();
            rpd.GetCurrentLocation = getCurrentLocation;
            rpd.GetCurrentZone = getCurrentZone;
            rpd.GetCamera = getCamera;
            rpd.UpdateUI();

            if (rpd.ShowDialog() == DialogResult.OK)
            {
                rpd.Walkpath.Name = rpd.WalkpathNameTextbox.Text;
                rpd.Walkpath.Zone = getCurrentZone();
                return rpd.Walkpath;
            }
            else return null;
        }

        bool recording = false;

        private void UpdateUI()
        {
            if (recording)
            {
                RecordButton.Text = "Stop Recording";
                MarkAsSafeButton.Enabled = true;
                DetectJumpsCheckbox.Enabled = false;
                OKButton.Enabled = false;
            }
            else
            {
                RecordButton.Text = "Record";
                MarkAsSafeButton.Enabled = false;
                DetectJumpsCheckbox.Enabled = true;
                if (Walkpath != null)
                {
                    OKButton.Enabled = true;
                }
            }
        }

        private void RecordButton_Click(object sender, EventArgs e)
        {
            if (!recording)
            {
                if (DetectJumpsCheckbox.Checked)
                {
                    InputHook.KeyPress += new KeyPressEventHandler(InputHook_KeyPress);
                }
                Walkpath = new Walkpath();
                NumWaypointsLabel.Text = "0";

                RecordWorker.RunWorkerAsync();
            }
            else
            {
                RecordWorker.CancelAsync();
            }

            recording = !recording;
            UpdateUI();
        }

        private void InputHook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar) == 32)
            {
                jumpOccurredEvent.Set();
            }
        }

        private AutoResetEvent jumpOccurredEvent = new AutoResetEvent(false);
        private Waypoint lastWaypoint = null;

        private void RecordWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Vector3 last = Vector3.MinValue;
            string startingZone = GetCurrentZone();

            do
            {
                if (GetCurrentZone() != startingZone)
                {
                    Walkpath = null;
                    this.BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show("You switched zones while recording a walkpath.\r\nWalkpath recorded is now invalid.", "Recording Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                    return;
                }

                if (RecordWorker.CancellationPending) return;

                bool jumped = jumpOccurredEvent.WaitOne(250);

                Vector3 cur = GetCurrentLocation();
                if (jumped || cur.DistanceTo(last) >= 2)
                {
                    Waypoint wp = new Waypoint(cur, jumped, false);
                    lastWaypoint = wp;

                    Walkpath.Waypoints.Add(wp);
                    last = cur;
                    RecordWorker.ReportProgress(0);
                }
            } while (true);

        }

        private void RecordWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            NumWaypointsLabel.Text = Walkpath.Waypoints.Count.ToString();
        }

        private void MarkAsSafeButton_Click(object sender, EventArgs e)
        {
            lastWaypoint.IsSafe = true;
            lastWaypoint.Camera = GetCamera();
            MarkedSafeLabel.Visible = true;
            SafeWaypointMarkerTimer.Start();
        }

        private void CurrentZoneUpdateTimer_Tick(object sender, EventArgs e)
        {
            CurrentZoneLabel.Text = "Current Zone: " + GetCurrentZone();
        }

        private void CurrentLocationUpdateTimer_Tick(object sender, EventArgs e)
        {
            CurrentLocationLabel.Text = GetCurrentLocation().ToVector().ToString(2);
        }

        private void SafeWaypointMarkerTimer_Tick(object sender, EventArgs e)
        {
            MarkedSafeLabel.Visible = false;
            SafeWaypointMarkerTimer.Stop();
        }
    }
}
