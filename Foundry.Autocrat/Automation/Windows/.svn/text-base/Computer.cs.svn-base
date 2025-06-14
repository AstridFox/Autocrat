using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Management;
using System.Windows.Forms;

namespace Foundry.Autocrat.Automation.Windows {
	/// <summary>
	/// Provides whole-computer related services.
	/// </summary>
	public static class Computer {

		#region INTEROP

		[DllImport("dwmapi.dll", PreserveSig = false)]
		private static extern int DwmEnableComposition(bool enable);
		[DllImport("dwmapi.dll")]
		private static extern void DwmIsCompositionEnabled(ref bool pfEnabled);
		[DllImport("winmm")]
		private static extern int PlaySound(string lpszName, IntPtr hModule, SoundFlags dwFlags);

		[Flags]
		private enum SoundFlags : int {
			SND_FILENAME = 0x20000,
			SND_ASYNC = 0x1,
			SND_PURGE = 0x40
		}

		#endregion

		/// <summary>
		/// Provides services for playing sounds.
		/// </summary>
		public static class Sound {
			/// <summary>
			///     Plays a wave file.
			/// </summary>
			/// <param name="wavePath">The full path of the wave file to play</param>
			/// <param name="async">True to play this wave file asynchronously.</param>
			public static void Play(string wavePath, bool async) {
				if (System.IO.File.Exists(wavePath)) {
					SoundFlags flags = SoundFlags.SND_FILENAME;
					if (async) flags |= SoundFlags.SND_ASYNC;
					PlaySound(wavePath, IntPtr.Zero, flags);
				}
			}

			/// <summary>
			///     Stops all wave files from playing.
			/// </summary>
			public static void StopAll() {
				PlaySound(null, IntPtr.Zero, SoundFlags.SND_PURGE);
			}
		}

		/// <summary>
		/// Provides services for Dwm (or Aero) control in Vista and other supported operating systems.
		/// </summary>
		public static class Dwm {
			public static bool Allowed {
				get {
					if (System.Environment.OSVersion.Version.Major < 6) return false;
					return true;
				}
			}

			public static bool Enabled {
				get {
					if (!Allowed) return false;
					bool en = false;
					DwmIsCompositionEnabled(ref en);
					return en;
				}
				set {
					if (value) EnableAero(); else DisableAero();
				}
			}

			private static void DisableAero() {
				if (Enabled) DwmEnableComposition(false);
			}

			private static void EnableAero() {
				if (Allowed && !Enabled) DwmEnableComposition(true);
			}
		}

		/// <summary>
		/// Provides services that control or report on the computer's environment.
		/// </summary>
		public static class Environment {
			public static void Shutdown() {
				ManagementBaseObject outParameters = null;
				ManagementClass sysOS = new ManagementClass("Win32_OperatingSystem");
				sysOS.Get();
				// enables required security privilege.
				sysOS.Scope.Options.EnablePrivileges = true;
				// get our in parameters
				ManagementBaseObject inParameters = sysOS.GetMethodParameters("Win32Shutdown");
				// pass the flag of 0 = System Shutdown
				inParameters["Flags"] = "1";
				inParameters["Reserved"] = "0";
				foreach (ManagementObject manObj in sysOS.GetInstances()) {
					outParameters = manObj.InvokeMethod("Win32Shutdown", inParameters, null);
				}
			}
		}

	}
}

