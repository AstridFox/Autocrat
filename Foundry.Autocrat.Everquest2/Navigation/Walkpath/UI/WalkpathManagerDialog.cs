using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using Foundry.Autocrat.Everquest2.Navigation.Walkpath.Serialization;
using Foundry.Autocrat.Geometry;

namespace Foundry.Autocrat.Everquest2.Navigation.Walkpath.UI
{
    public partial class WalkpathManagerDialog : Form
    {
        public WalkpathManagerDialog()
        {
            InitializeComponent();
        }

        private Dictionary<string, List<string>> walkpathsByZone;
        private Func<string> GetCurrentZone;
        private Func<Vector3> GetCurrentLocation;
        private Func<Camera> GetCamera;
        private string BasePath;

        public static Walkpath DisplayWalkpathManager(string basePath, Func<Vector3> getCurrentLocation, Func<string> getCurrentZone, Func<Camera> getCamera)
        {
            // Open Walkpaths meta-file
            // Display list of walkpaths
            // If user chooses one to load, return it
            // Otherwise, return null.

            var wmd = new WalkpathManagerDialog();
            wmd.GetCurrentZone = getCurrentZone;
            wmd.GetCurrentLocation = getCurrentLocation;
            wmd.GetCamera = getCamera;
            wmd.BasePath = basePath;
            wmd.walkpathsByZone = GetWalkpathsMetadata(Path.Combine(basePath, "Walkpaths.xml"));
            wmd.UpdateUI();

            if (wmd.ShowDialog() == DialogResult.OK)
            {
                var items = wmd.PathsListView.SelectedItems;
                if (items != null && items.Count != 0)
                {
                    string wpName = items[0].Text;

                    return WalkpathSerializer.LoadWalkpath(Path.Combine(basePath, wpName + ".dat"));
                }

            }

            return null;

        }

        private static Dictionary<string, List<string>> GetWalkpathsMetadata(string filePath)
        {
            if (File.Exists(filePath))
            {
                var doc = XDocument.Load(filePath);
                var paths = doc.Element("Walkpaths");

                return paths
                    .Elements("Walkpath")
                    .GroupBy(p => p.Attribute("Zone").Value, p => p.Attribute("Name").Value)
                    .ToDictionary(g => g.Key, g => g.ToList());
            }
            else
            {
                return new Dictionary<string, List<string>>();
            }
        }

        private void SaveWalkpathsMetadata()
        {
            var wps = new XElement("Walkpaths");
            var doc = new XDocument(wps);

            foreach (var kvp in walkpathsByZone)
            {
                string zone = kvp.Key;
                foreach (var wp in kvp.Value)
                {
                    wps.Add(
                        new XElement("Walkpath",
                            new XAttribute("Zone", zone),
                            new XAttribute("Name", wp)
                        )
                    );
                }
            }

            doc.Save(Path.Combine(BasePath, "Walkpaths.xml"));
        }

        private void CurrentZoneUpdateTimer_Tick(object sender, EventArgs e)
        {
            CurrentZoneLabel.Text = "Current Zone: " + GetCurrentZone();
        }

        private void UpdateUI()
        {
            // Update Walkpaths ListView
            PathsListView.Items.Clear();

            string curZone = GetCurrentZone();

            if (FilterPathsByZoneCheckbox.Checked)
            {
                ListViewGroup lvg = new ListViewGroup(curZone, curZone);
                PathsListView.Groups.Add(lvg);
                if (!walkpathsByZone.ContainsKey(curZone) || walkpathsByZone[curZone].Count == 0)
                {
                    PathsListView.Items.Add(new ListViewItem { Group = lvg, Text = "None" });
                }
                else
                {
                    foreach (var wpName in walkpathsByZone[curZone])
                    {
                        PathsListView.Items.Add(new ListViewItem { Group = lvg, Text = wpName });
                    }
                }
            }
            else
            {
                foreach (var zone in walkpathsByZone.Keys)
                {
                    ListViewGroup lvg = new ListViewGroup(zone, zone);
                    PathsListView.Groups.Add(lvg);
                    foreach (var wpName in walkpathsByZone[zone])
                    {
                        PathsListView.Items.Add(new ListViewItem { Group = lvg, Text = wpName });
                    }
                }
            }

        }

        private void FilterPathsByZoneCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void DeletePathButton_Click(object sender, EventArgs e)
        {
            var items = PathsListView.SelectedItems;
            if (items != null && items.Count != 0)
            {
                string wpName = items[0].Text;
                string zone = items[0].Group.Name;

                walkpathsByZone[zone].Remove(wpName);

                if (walkpathsByZone[zone].Count == 0)
                {
                    walkpathsByZone.Remove(zone);
                }

                File.Delete(Path.Combine(BasePath, wpName + ".dat"));

                SaveWalkpathsMetadata();
                UpdateUI();
            }
        }

        private void NewPathButton_Click(object sender, EventArgs e)
        {
            Walkpath newWp = RecordPathDialog.RecordPath(GetCurrentLocation, GetCurrentZone, GetCamera);
            if (newWp != null)
            {
                if (!walkpathsByZone.ContainsKey(newWp.Zone))
                {
                    walkpathsByZone.Add(newWp.Zone, new List<string>());
                }

                walkpathsByZone[newWp.Zone].Add(newWp.Name);
                SaveWalkpathsMetadata();

                WalkpathSerializer.SaveWalkpath(newWp, BasePath);

                UpdateUI();
            }
        }

        private void RenamePathButton_Click(object sender, EventArgs e)
        {
            var items = PathsListView.SelectedItems;
            if (items != null && items.Count != 0)
            {
                string wpName = items[0].Text;
                string zone = items[0].Group.Name;

                string newName = RenamePathDialog.RenamePath(wpName);
                if (wpName == newName) return;

                File.Move(Path.Combine(BasePath, wpName + ".dat"), Path.Combine(BasePath, newName + ".dat"));
                
                walkpathsByZone[zone].Remove(wpName);
                walkpathsByZone[zone].Add(newName);

                SaveWalkpathsMetadata();
                UpdateUI();
            }
        }

    }
}
