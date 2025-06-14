using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Foundry.Autocrat.Credits {
	public static class CreditRoll {
		public static List<string> GetCreditRoll() {
			List<string> r = new List<string>();

			Assembly entryAssembly = Assembly.GetEntryAssembly();
			var assemblies = entryAssembly.GetReferencedAssemblies().ToList();

			foreach (var asmName in assemblies) {
				Assembly asm = Assembly.Load(asmName);
				object[] attrs = asm.GetCustomAttributes(typeof(CreditsAttribute), false);
				foreach (var attr in attrs) {
					CreditsAttribute creds = attr as CreditsAttribute;
					if (creds != null) {
						r.Add(creds.Credits);
					}
				}
			}

			return r;
		}
	}
}
