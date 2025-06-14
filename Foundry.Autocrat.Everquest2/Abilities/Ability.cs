using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundry.Autocrat.Automation.Windows;

namespace Foundry.Autocrat.Everquest2.Abilities
{
    public class Ability
    {
        public Ability(string name, TimeSpan activate, TimeSpan duration, TimeSpan recharge, Keyboard.KeyCombo keyCombo)
        {
            LastActivationTime = DateTime.MinValue;

            ActivateTime = activate;
            DurationTime = duration;
            RechargeTime = recharge;

            KeyCombo = keyCombo;
            Name = name;

            Enabled = true;
        }

        public string Name { get; set; }
        public TimeSpan ActivateTime { get; set; }
        public TimeSpan DurationTime { get; set; }
        public TimeSpan RechargeTime { get; set; }
        public Keyboard.KeyCombo KeyCombo { get; set; }

        public bool Enabled { get; set; }

        public DateTime LastActivationTime { get; private set; }

        public void Activate()
        {
            LastActivationTime = DateTime.Now;
        }

        public void Reset()
        {
            LastActivationTime = DateTime.MinValue;
        }

        public bool IsReady
        {
            get
            {
                var now = DateTime.Now;
                return (now - LastActivationTime) > (ActivateTime + RechargeTime);
            }
        }

        public bool IsActive
        {
            get
            {
                var now = DateTime.Now;
                var dur = now - LastActivationTime;
                return dur > ActivateTime && dur < ActivateTime + DurationTime;
            }
        }

    }
}
