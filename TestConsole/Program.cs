using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

using Foundry.Autocrat.Everquest2.Memory;
using Foundry.Autocrat.Everquest2.Abilities;
using Foundry.Autocrat.Automation.Windows;

using Foundry.Autocrat.Extensions.Time;
using System.Windows.Forms;
using Foundry.Autocrat.Everquest2.Navigation.Walkpath.UI;
using Foundry.Autocrat.Everquest2.Navigation.Walkpath;
using Foundry.Autocrat.Everquest2.Abilities.Serialization;
using Foundry.Autocrat.Everquest2.Navigation.Walkpath.Serialization;
using Foundry.Autocrat.Workflows;
using Foundry.Autocrat.Everquest2.LogFiles;
using System.Text.RegularExpressions;

using Foundry.Autocrat.Extensions.Regex;

namespace TestConsole
{
    class Program
    {
        #region Setup

        private static readonly string hr = new String('-', 79);
        private static AbilityChain combat = new AbilityChain("Combat");

        [STAThread]
        static void Main(string[] args)
        {

            Console.SetWindowSize(80, 40);

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            Process eq2Process = null;
            Eq2 eq2 = null;
            do
            {
                Console.WriteLine("Searching for EQ2...");

                eq2Process = Process.GetProcessesByName("EverQuest2").FirstOrDefault();
                while (eq2Process == null)
                {
                    Thread.Sleep(100);
                    Console.Write(".");
                    eq2Process = Process.GetProcessesByName("EverQuest2").FirstOrDefault();
                }

                eq2 = new Eq2(eq2Process, @"E:\Development\Macrocrafter\EverHarvest Staging\void.dat");
            } while (MainContents(eq2));

        }

        #endregion

        #region Contents

        private static char SelectFromContents()
        {
            Console.Clear();
            Console.WriteLine("Contents:");
            Console.WriteLine("1: Character Information");
            Console.WriteLine("2: Camera Information");
            Console.WriteLine("3: EQ2 Information");
            Console.WriteLine("4: Target Information");
            Console.WriteLine("5: Target Name");
            Console.WriteLine("6: Test Ability Chains");

            Console.WriteLine(hr);
            Console.WriteLine("R: Reload EQ2");
            Console.WriteLine("Q: Quit\r\n");

            return (char)Console.ReadKey().KeyChar;
        }

        private static bool MainContents(Eq2 eq2)
        {
            do
            {
                string selection = SelectFromContents().ToString().ToLowerInvariant();

                switch (selection)
                {
                    case "q": return false;
                    case "r": return true;

                    case "1": ShowCharacterInformation(eq2); break;
                    case "2": ShowCameraInformation(eq2); break;
                    case "3": ShowEQ2Information(eq2); break;
                    case "4": ShowTargetInformation(eq2); break;
                    case "5": ShowTargetName(eq2); break;
                    case "6": TestAbilityChains(eq2); break;

                    default: continue;
                }

            } while (true);
        }

        #endregion

        #region Information Display

        private static void ShowCharacterInformation(Eq2 eq2)
        {
            Console.Clear();
            do
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Character Name:         {0}                          ", eq2.Character.Name);
                Console.WriteLine("Character Surname:      {0}                          ", eq2.Character.Surname);
                Console.WriteLine("Character Server:       {0}                          ", eq2.Character.ServerName);
                Console.WriteLine("Character Zone:         {0}                          ", eq2.Character.ZoneName);
                Console.WriteLine(hr);
                Console.WriteLine("Character Location:     {0}          ", eq2.Character.Location);
                Console.WriteLine("Character Heading:      {0}                          ", eq2.Character.Heading);
                Console.WriteLine("Character Autorunning:  {0}                          ", eq2.Character.IsAutoRunning);
                Console.WriteLine("Character is running:   {0}                          ", eq2.Character.Running);
                Console.WriteLine("Character Speed:        {0}                          ", eq2.Character.Speed);
                Console.WriteLine(hr);
                Console.WriteLine("Character Health / Max: {0} / {1}                    ", eq2.Character.CurrentHealth, eq2.Character.MaxHealth);
                Console.WriteLine("Character Power / Max:  {0} / {1}                    ", eq2.Character.CurrentPower, eq2.Character.MaxPower);
                Console.WriteLine("Character Level:        {0}                          ", eq2.Character.Level);
                Console.WriteLine(hr);
                Console.WriteLine("Character has Target:   {0}                          ", eq2.Character.HasTarget);

                Thread.Sleep(25);
            } while (!Console.KeyAvailable);
            Console.ReadKey();
        }

        private static void ShowCameraInformation(Eq2 eq2)
        {
            Console.Clear();
            do
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Camera Pitch:           {0}                          ", eq2.Camera.Pitch);
                Console.WriteLine("Camera Yaw:             {0}                          ", eq2.Camera.Yaw);
                Console.WriteLine("Camera Zoom:            {0}                          ", eq2.Camera.Zoom);

                Thread.Sleep(25);
            } while (!Console.KeyAvailable);
            Console.ReadKey();
        }

        private static void ShowEQ2Information(Eq2 eq2)
        {
            Console.Clear();
            do
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Cursor Icon ID:         {0}                          ", eq2.UI.CursorIconId);
                Console.WriteLine("Chat Line has Focus:    {0}                          ", eq2.UI.HasChatWindowFocus);

                Thread.Sleep(25);
            } while (!Console.KeyAvailable);
            Console.ReadKey();
        }

        private static void ShowTargetInformation(Eq2 eq2)
        {
            Console.Clear();
            do
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Target Spawn ID:        {0}  {1}                     ", eq2.Target.SpawnId, eq2.Target.SpawnId2);
                Console.WriteLine(hr);
                Console.WriteLine("Target Location:        X:{0}  Z:{1}  Y:{2}          ", eq2.Target.X, eq2.Target.Z, eq2.Target.Y);
                Console.WriteLine("Distance to Target:     {0}                          ", eq2.Target.Distance);
                Console.WriteLine(hr);
                Console.WriteLine("Target Level:           {0}                          ", eq2.Target.Level);
                Console.WriteLine("Target Health %:        {0}%                         ", eq2.Target.Health);
                Console.WriteLine("Target Power %:         {0}%                         ", eq2.Target.Power);
                Console.WriteLine("Target Assist Health %: {0}%                         ", eq2.Target.AssistHealth);

                Thread.Sleep(25);
            } while (!Console.KeyAvailable);
            Console.ReadKey();
        }

        private static void ShowTargetName(Eq2 eq2)
        {
            TargetNameService tns = new TargetNameService(eq2.Eq2Process, eq2);

            Console.Clear();
            do
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Target Name:            {0}                          ", tns.TargetName);

                Thread.Sleep(25);
            } while (!Console.KeyAvailable);
            Console.ReadKey();
        }

        #endregion

        #region Ability Chains

        private static void TestAbilityChains(Eq2 eq2)
        {
            Console.Clear();
            Console.WriteLine("Testing ability chains...");

            Window eq2W = new Window(eq2.Eq2Process.MainWindowHandle);
            Keyboard kb = new Keyboard(10);
            TargetNameService tns = new TargetNameService(eq2.Eq2Process, eq2);

            eq2W.Activate();

            Thread.Sleep(1000);

            do
            {
                if (Window.GetActiveWindow().Hwnd != eq2W.Hwnd) break;

                if (eq2.Character.HasTarget && !tns.TargetName.ToLower().StartsWith("corpse"))
                {
                    var abil = combat.GetNextAvailableAbility(true);
                    if (abil != null)
                    {
                        Keyboard.KeyCombo combo = abil.KeyCombo;
                        Console.WriteLine("Activating: " + combo.ToString());

                        if (combo.IsAlt) kb.Down(Keyboard.Keys.Menu);
                        if (combo.IsShift) kb.Down(Keyboard.Keys.Shift);
                        if (combo.IsControl) kb.Down(Keyboard.Keys.Control);

                        kb.Press(combo.Key);
                        Thread.Sleep(500);
                        abil.Activate();

                        if (combo.IsControl) kb.Up(Keyboard.Keys.Control);
                        if (combo.IsShift) kb.Up(Keyboard.Keys.Shift);
                        if (combo.IsAlt) kb.Up(Keyboard.Keys.Menu);

                        Thread.Sleep(abil.ActivateTime);
                    }
                    else
                    {
                        Thread.Sleep(250);
                    }
                }
                else
                {
                    Thread.Sleep(250);
                }
            } while (true);

        }

        #endregion

    }

    public static class Ext
    {
        public static void ForEachIndex<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            int i = 0;
            foreach (var item in ie)
            {
                int i2 = i;
                action(item, i2);
                i++;
            }
        }
    }

}
