using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Autocrat.Everquest2.Abilities
{
    public class AbilityChain: List<Ability>
    {
        public string Name { get; set; }

        public AbilityChain(string name)
        {
            Name = name;
        }

        public Ability GetNextAvailableAbility(bool ignoreDuration)
        {
            foreach (var a in this)
            {
                if (a.Enabled && a.IsReady && (ignoreDuration | !a.IsActive)) return a;
            }

            return null;
        }
    }
}
