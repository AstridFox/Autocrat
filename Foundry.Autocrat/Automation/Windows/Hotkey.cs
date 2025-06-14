using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace Foundry.Autocrat.Automation.Windows {

	/// <summary>
	/// Provides services for registering and unregistering Hotkey notifications.
	/// </summary>
	public static class Hotkey {
		[Flags]
		public enum HotkeyModifierFlags {
			None = 0,
			Alt = 1,
			Control = 2,
			Shift = 4,
			Windows = 8
		}

		private static Random random;
		private static HotkeyInstance instance;
		private static Dictionary<int, Tuple<IntPtr, Action>> registeredKeys;
		private static object locker = new object();

		/// <summary>
		/// Registers a new hotkey for the specified window. If IntPtr.Zero is specified as the window, the
		/// hotkey will be system-wide.
		/// </summary>
		/// <param name="hwnd">The window to add a hotkey to. If IntPtr.Zero is specified, the hotkey will be system-wide.</param>
		/// <param name="keyMods">A combination of modifier keys to include with the hotkey definition.</param>
		/// <param name="key">The key to provide a hotkey handler for.</param>
		/// <param name="action">A parameterless delegate to invoke upon receipt of the hotkey.</param>
		/// <returns>An IntPtr representing this hotkey's ID. This value must be preserved in order to remove the hotkey handler at a later time.</returns>
		public static IntPtr RegisterHotkey(IntPtr hwnd, HotkeyModifierFlags keyMods, Keys key, Action action) {
			lock (locker) {
				if (instance == null) {
					random = new Random();
					instance = new HotkeyInstance();
					instance.HotPressed += HandleHotkeyEvent;
					Application.AddMessageFilter(instance);
					registeredKeys = new Dictionary<int, Tuple<IntPtr, Action>>();
				}

				int id = random.Next();
				while (registeredKeys.ContainsKey(id)) {
					id = random.Next();
				}

				instance.AddHotKey(hwnd, id, keyMods, key);
				registeredKeys.Add(id, Tuples.Tuple(hwnd, action));

				return new IntPtr(id);
			}
		}

		public static IntPtr RegisterHotkey(HotkeyModifierFlags keyMods, Keys key, Action action) {
			return RegisterHotkey(IntPtr.Zero, keyMods, key, action);
		}

		/// <summary>
		/// Unregisters a single hotkey as specified by its ID.
		/// </summary>
		/// <param name="id">The ID of the hotkey handler, as returned by RegisterHotkey.</param>
		public static void UnregisterHotkey(IntPtr id) {
			lock (locker) {
                int idVal = id.ToInt32();
				if (instance == null) return;
				if (registeredKeys == null) return;
                if (!registeredKeys.ContainsKey(idVal)) return;

                IntPtr hwnd = registeredKeys[idVal].Value1;

                instance.RemoveHotKey(hwnd, idVal);
                registeredKeys.Remove(idVal);

				if (registeredKeys.Count == 0) {
					Application.RemoveMessageFilter(instance);
					instance.HotPressed -= HandleHotkeyEvent;
					instance = null;
					registeredKeys = null;
				}
			}
		}

		/// <summary>
		/// Unregisters all hotkeys that were registered via this service.
		/// </summary>
		public static void UnregisterAllHotkeys() {
			lock (locker) {
				if (instance == null) return;
				if (registeredKeys == null) return;

				List<int> ids = registeredKeys.Keys.ToList();
				ids.ForEach(id =>
					{
						IntPtr hwnd = registeredKeys[id].Value1;
						instance.RemoveHotKey(hwnd, id);
					}
				);

				Application.RemoveMessageFilter(instance);
				instance.HotPressed -= HandleHotkeyEvent;
				instance = null;
				registeredKeys = null;
			}
		}

		private static void HandleHotkeyEvent(object sender, HotkeyInstance.HotPressedEventArgs e) {
			lock (locker) {
				if (instance == null) return;
				if (registeredKeys == null) return;
				if (!registeredKeys.ContainsKey(e.KeyID)) return;

				registeredKeys[e.KeyID].Value2.Invoke();
			}
		}

		private class HotkeyInstance : IMessageFilter {
			#region INTEROP

			private const int WM_HOTKEY = 0x312;

			[DllImport("user32.dll")]
			private static extern int RegisterHotKey(IntPtr hwnd, int id, HotkeyModifierFlags fsModifiers, int vk);
			[DllImport("user32.dll")]
			private static extern int UnregisterHotKey(IntPtr hwnd, int id);

			#endregion

			[DebuggerHidden]
			internal void AddHotKey(IntPtr hwnd, int id, HotkeyModifierFlags modifiers, Keys key) {
				RegisterHotKey(hwnd, id, modifiers, Convert.ToInt32(key));
			}

			[DebuggerHidden]
			internal void RemoveHotKey(IntPtr hwnd, int id) {
				UnregisterHotKey(hwnd, id);
			}

			[DebuggerHidden]
			bool IMessageFilter.PreFilterMessage(ref Message m) {
				if (m.Msg == WM_HOTKEY) {
					int keyID = m.WParam.ToInt32();
					this.OnHotPressed(keyID);
					return true;
				}
				return false;
			}

			[DebuggerHidden]
			internal void OnHotPressed(int keyID) {
				if (this.HotPressed != null) {
					this.HotPressed(this, new HotPressedEventArgs { KeyID = keyID });
				}
			}

			internal event HotPressedDelegate HotPressed;
			internal delegate void HotPressedDelegate(object sender, HotPressedEventArgs e);

			internal class HotPressedEventArgs : EventArgs {
				/// <summary>
				/// Represents the ID of the key pressed
				/// </summary>
				[DebuggerHidden]
				internal int KeyID { get; set; }
			}
		}

	}
}