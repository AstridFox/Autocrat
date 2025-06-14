using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using Foundry.Autocrat.Automation.Windows;

namespace Foundry.Autocrat.Everquest2.Abilities.Serialization
{
    public static class AbilityChainSerializer
    {
        public static AbilityChain LoadAbilityChain(string filePath)
        {
            var doc = XDocument.Load(filePath);
            var chain = doc.Descendants("AbilityChain").Single();
            var abils = chain.Descendants("Abilities").Single();

            string name = chain.Attribute("Name").Value;
            AbilityChain abchain = new AbilityChain(name);

            var q = from abil in abils.Descendants("Ability")
                    select new Ability(
                        abil.Attribute("Name").Value,
                        TimeSpan.Parse(abil.Element("ActivateTime").Value),
                        TimeSpan.Parse(abil.Element("DurationTime").Value),
                        TimeSpan.Parse(abil.Element("RechargeTime").Value),
                        Keyboard.KeyCombo.Parse(abil.Element("KeyCombo").Value)
                    );

            foreach (var abil in q)
            {
                abchain.Add(abil);
            }

            return abchain;
        }

        public static void SaveAbilityChain(AbilityChain chain, string basePath)
        {
            string fileName = chain.Name;
            foreach (var chr in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(chr.ToString(), "");
            }

            string filePath = Path.Combine(basePath, fileName + ".xml");

            XElement abils = new XElement("Abilities");

            XDocument doc = new XDocument(
                new XElement("AbilityChain",
                    new XAttribute("Name", chain.Name),
                    abils
                )
            );

            foreach (var abil in chain)
            {
                abils.Add(
                    new XElement("Ability",
                        new XAttribute("Name", abil.Name),
                        new XElement("ActivateTime", abil.ActivateTime.ToString()),
                        new XElement("DurationTime", abil.DurationTime.ToString()),
                        new XElement("RechargeTime", abil.RechargeTime.ToString()),
                        new XElement("KeyCombo", abil.KeyCombo)
                    )
                );
            }

            doc.Save(filePath);
        }
    }
}
