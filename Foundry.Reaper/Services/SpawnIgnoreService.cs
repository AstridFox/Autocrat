using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundry.Reaper.Services {
	public class SpawnIgnoreService {
		private Dictionary<int, DateTime> ignoredSpawns = new Dictionary<int, DateTime>();

		public void Ignore(int spawnId) {
			if (ignoredSpawns.ContainsKey(spawnId)) {
				ignoredSpawns[spawnId] = DateTime.MaxValue;
			} else {
				ignoredSpawns.Add(spawnId, DateTime.MaxValue);
			}
		}

		public void Ignore(int spawnId, TimeSpan ignoreDuration) {
			DateTime unignoreTime = DateTime.Now + ignoreDuration;

			if (ignoredSpawns.ContainsKey(spawnId)) {
				if (ignoredSpawns[spawnId] < unignoreTime) ignoredSpawns[spawnId] = unignoreTime;
			} else {
				ignoredSpawns.Add(spawnId, unignoreTime);
			}
		}

		public bool IsIgnored(int spawnId) {
			if (!ignoredSpawns.ContainsKey(spawnId)) return false;
			return DateTime.Now <= ignoredSpawns[spawnId];
		}
	}
}
