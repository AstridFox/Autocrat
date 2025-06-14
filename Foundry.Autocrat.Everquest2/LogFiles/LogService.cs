using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using Foundry.Autocrat.IO;
using System.IO;

namespace Foundry.Autocrat.Everquest2.LogFiles {
	public static class LogService {
		public static TextStreamWatcher OpenLog(string eq2BasePath, string characterName, string serverName) {
			string path = Path.Combine(eq2BasePath, "logs");
            path = Path.Combine(path, serverName);
			path = Path.Combine(path, "eq2log_" + characterName + ".txt");

			if (!File.Exists(path)) throw new FileNotFoundException("Log file for character " + characterName + " on server " + serverName + " not found.");

			return new TextStreamWatcher(path);
		}
	}
}
