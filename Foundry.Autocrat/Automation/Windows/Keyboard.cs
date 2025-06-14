using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

using Foundry.Autocrat.Extensions.Fluent;

namespace Foundry.Autocrat.Automation.Windows {
	public class Keyboard {

		#region INTEROP

		[DllImport("user32.dll")]
		internal static extern byte MapVirtualKey(Keys uCode, uint uMapType);
		[DllImport("user32.dll")]
		internal static extern byte MapVirtualKey(char uCode, uint uMapType);
		[DllImport("user32.dll")]
		internal static extern byte MapVirtualKey(byte uCode, uint uMapType);
		[DllImport("user32.dll")]
		internal static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
		[DllImport("user32.dll")]
		internal static extern void keybd_event(char bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
		[DllImport("user32.dll")]
		internal static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
		[DllImport("user32.dll")]
		internal static extern short VkKeyScan(char ch);

		private const uint KEYEVENTF_KEYDOWN = 0x00;
		private const uint KEYEVENTF_KEYUP = 0x02;

		#endregion

		public enum Keys : byte {
			LeftButton = 0x01,
			RightButton = 0x02,
			Cancel = 0x03,
			MiddleButton = 0x04,
			ExtraButton1 = 0x05,
			ExtraButton2 = 0x06,
			Back = 0x08,
			Tab = 0x09,
			Clear = 0x0C,
			Return = 0x0D,
			Shift = 0x10,
			Control = 0x11,
			Menu = 0x12,
			Pause = 0x13,
			CapsLock = 0x14,
			Kana = 0x15,
			Hangeul = 0x15,
			Hangul = 0x15,
			Junja = 0x17,
			Final = 0x18,
			Hanja = 0x19,
			Kanji = 0x19,
			Escape = 0x1B,
			Convert = 0x1C,
			NonConvert = 0x1D,
			Accept = 0x1E,
			ModeChange = 0x1F,
			Space = 0x20,
			Prior = 0x21,
			PageUp = 0x21,
			Next = 0x22,
			PageDown = 0x22,
			End = 0x23,
			Home = 0x24,
			Left = 0x25,
			Up = 0x26,
			Right = 0x27,
			Down = 0x28,
			Select = 0x29,
			Print = 0x2A,
			Execute = 0x2B,
			Snapshot = 0x2C,
			Insert = 0x2D,
			Delete = 0x2E,
			Help = 0x2F,
			N0 = 0x30,
			N1 = 0x31,
			N2 = 0x32,
			N3 = 0x33,
			N4 = 0x34,
			N5 = 0x35,
			N6 = 0x36,
			N7 = 0x37,
			N8 = 0x38,
			N9 = 0x39,
			A = 0x41,
			B = 0x42,
			C = 0x43,
			D = 0x44,
			E = 0x45,
			F = 0x46,
			G = 0x47,
			H = 0x48,
			I = 0x49,
			J = 0x4A,
			K = 0x4B,
			L = 0x4C,
			M = 0x4D,
			N = 0x4E,
			O = 0x4F,
			P = 0x50,
			Q = 0x51,
			R = 0x52,
			S = 0x53,
			T = 0x54,
			U = 0x55,
			V = 0x56,
			W = 0x57,
			X = 0x58,
			Y = 0x59,
			Z = 0x5A,
			LeftWindows = 0x5B,
			RightWindows = 0x5C,
			Application = 0x5D,
			Sleep = 0x5F,
			Numpad0 = 0x60,
			Numpad1 = 0x61,
			Numpad2 = 0x62,
			Numpad3 = 0x63,
			Numpad4 = 0x64,
			Numpad5 = 0x65,
			Numpad6 = 0x66,
			Numpad7 = 0x67,
			Numpad8 = 0x68,
			Numpad9 = 0x69,
			Multiply = 0x6A,
			Add = 0x6B,
			Separator = 0x6C,
			Subtract = 0x6D,
			Decimal = 0x6E,
			Divide = 0x6F,
			F1 = 0x70,
			F2 = 0x71,
			F3 = 0x72,
			F4 = 0x73,
			F5 = 0x74,
			F6 = 0x75,
			F7 = 0x76,
			F8 = 0x77,
			F9 = 0x78,
			F10 = 0x79,
			F11 = 0x7A,
			F12 = 0x7B,
			F13 = 0x7C,
			F14 = 0x7D,
			F15 = 0x7E,
			F16 = 0x7F,
			F17 = 0x80,
			F18 = 0x81,
			F19 = 0x82,
			F20 = 0x83,
			F21 = 0x84,
			F22 = 0x85,
			F23 = 0x86,
			F24 = 0x87,
			NumLock = 0x90,
			ScrollLock = 0x91,
			NEC_Equal = 0x92,
			Fujitsu_Jisho = 0x92,
			Fujitsu_Masshou = 0x93,
			Fujitsu_Touroku = 0x94,
			Fujitsu_Loya = 0x95,
			Fujitsu_Roya = 0x96,
			LeftShift = 0xA0,
			RightShift = 0xA1,
			LeftControl = 0xA2,
			RightControl = 0xA3,
			LeftMenu = 0xA4,
			RightMenu = 0xA5,
			BrowserBack = 0xA6,
			BrowserForward = 0xA7,
			BrowserRefresh = 0xA8,
			BrowserStop = 0xA9,
			BrowserSearch = 0xAA,
			BrowserFavorites = 0xAB,
			BrowserHome = 0xAC,
			VolumeMute = 0xAD,
			VolumeDown = 0xAE,
			VolumeUp = 0xAF,
			MediaNextTrack = 0xB0,
			MediaPrevTrack = 0xB1,
			MediaStop = 0xB2,
			MediaPlayPause = 0xB3,
			LaunchMail = 0xB4,
			LaunchMediaSelect = 0xB5,
			LaunchApplication1 = 0xB6,
			LaunchApplication2 = 0xB7,
			OEM1 = 0xBA,
			OEMPlus = 0xBB,
			OEMComma = 0xBC,
			OEMMinus = 0xBD,
			OEMPeriod = 0xBE,
			OEM2 = 0xBF,
			OEM3 = 0xC0,
			OEM4 = 0xDB,
			OEM5 = 0xDC,
			OEM6 = 0xDD,
			OEM7 = 0xDE,
			OEM8 = 0xDF,
			OEMAX = 0xE1,
			OEM102 = 0xE2,
			ICOHelp = 0xE3,
			ICO00 = 0xE4,
			ProcessKey = 0xE5,
			ICOClear = 0xE6,
			Packet = 0xE7,
			OEMReset = 0xE9,
			OEMJump = 0xEA,
			OEMPA1 = 0xEB,
			OEMPA2 = 0xEC,
			OEMPA3 = 0xED,
			OEMWSCtrl = 0xEE,
			OEMCUSel = 0xEF,
			OEMATTN = 0xF0,
			OEMFinish = 0xF1,
			OEMCopy = 0xF2,
			OEMAuto = 0xF3,
			OEMENLW = 0xF4,
			OEMBackTab = 0xF5,
			ATTN = 0xF6,
			CRSel = 0xF7,
			EXSel = 0xF8,
			EREOF = 0xF9,
			Play = 0xFA,
			Zoom = 0xFB,
			Noname = 0xFC,
			PA1 = 0xFD,
			OEMClear = 0xFE
		}

        public static string KeysToString(Keys keys)
        {
            switch (keys)
            {
                case default(Keys): return "";
                case Keys.Tab: return "[Tab]";
                case Keys.Return: return "[Enter]";
                case Keys.Shift: return "[Shift]";
                case Keys.Control: return "[Control]";
                case Keys.Menu: return "[Alt]";
                case Keys.Pause: return "[Pause]";
                case Keys.CapsLock: return "[CapsLock]";
                case Keys.Escape: return "[Esc]";
                case Keys.Space: return "[ ]";
                case Keys.PageUp: return "[Page Up]";
                case Keys.PageDown: return "[Page Down]";
                case Keys.End: return "[End]";
                case Keys.Home: return "[Home]";
                case Keys.Left: return "[Left]";
                case Keys.Up: return "[Up]";
                case Keys.Right: return "[Right]";
                case Keys.Down: return "[Down]";
                case Keys.Insert: return "[Insert]";
                case Keys.Delete: return "[Delete]";
                case Keys.N0: return "[0]";
                case Keys.N1: return "[1]";
                case Keys.N2: return "[2]";
                case Keys.N3: return "[3]";
                case Keys.N4: return "[4]";
                case Keys.N5: return "[5]";
                case Keys.N6: return "[6]";
                case Keys.N7: return "[7]";
                case Keys.N8: return "[8]";
                case Keys.N9: return "[9]";
                case Keys.A: return "[A]";
                case Keys.B: return "[B]";
                case Keys.C: return "[C]";
                case Keys.D: return "[D]";
                case Keys.E: return "[E]";
                case Keys.F: return "[F]";
                case Keys.G: return "[G]";
                case Keys.H: return "[H]";
                case Keys.I: return "[I]";
                case Keys.J: return "[J]";
                case Keys.K: return "[K]";
                case Keys.L: return "[L]";
                case Keys.M: return "[M]";
                case Keys.N: return "[N]";
                case Keys.O: return "[O]";
                case Keys.P: return "[P]";
                case Keys.Q: return "[Q]";
                case Keys.R: return "[R]";
                case Keys.S: return "[S]";
                case Keys.T: return "[T]";
                case Keys.U: return "[U]";
                case Keys.V: return "[V]";
                case Keys.W: return "[W]";
                case Keys.X: return "[X]";
                case Keys.Y: return "[Y]";
                case Keys.Z: return "[Z]";
                case Keys.Numpad0: return "[Numpad 0]";
                case Keys.Numpad1: return "[Numpad 1]";
                case Keys.Numpad2: return "[Numpad 2]";
                case Keys.Numpad3: return "[Numpad 3]";
                case Keys.Numpad4: return "[Numpad 4]";
                case Keys.Numpad5: return "[Numpad 5]";
                case Keys.Numpad6: return "[Numpad 6]";
                case Keys.Numpad7: return "[Numpad 7]";
                case Keys.Numpad8: return "[Numpad 8]";
                case Keys.Numpad9: return "[Numpad 9]";
                case Keys.Multiply: return "[Numpad *]";
                case Keys.Add: return "[Numpad +]";
                case Keys.Subtract: return "[Numpad -]";
                case Keys.Decimal: return "[Numpad .]";
                case Keys.Divide: return "[Numpad /]";
                case Keys.F1: return "[F1]";
                case Keys.F2: return "[F2]";
                case Keys.F3: return "[F3]";
                case Keys.F4: return "[F4]";
                case Keys.F5: return "[F5]";
                case Keys.F6: return "[F6]";
                case Keys.F7: return "[F7]";
                case Keys.F8: return "[F8]";
                case Keys.F9: return "[F9]";
                case Keys.F10: return "[F10]";
                case Keys.F11: return "[F11]";
                case Keys.F12: return "[F12]";
                case Keys.F13: return "[F13]";
                case Keys.F14: return "[F14]";
                case Keys.F15: return "[F15]";
                case Keys.F16: return "[F16]";
                case Keys.F17: return "[F17]";
                case Keys.F18: return "[F18]";
                case Keys.F19: return "[F19]";
                case Keys.F20: return "[F20]";
                case Keys.F21: return "[F21]";
                case Keys.F22: return "[F22]";
                case Keys.F23: return "[F23]";
                case Keys.F24: return "[F24]";
                case Keys.NumLock: return "[NumLock]";
                case Keys.ScrollLock: return "[ScrollLock]";
                case Keys.NEC_Equal: return "[NEC_Equal]";
                case Keys.OEMPlus: return "[+]";
                case Keys.OEMComma: return "[,]";
                case Keys.OEMMinus: return "[-]";
                case Keys.OEMPeriod: return "[.]";
                
                default:
                    return "???";
            }
        }

        public struct KeyCombo
        {
            public readonly Keys Key;
            public readonly bool IsAlt;
            public readonly bool IsShift;
            public readonly bool IsControl;

            public KeyCombo(Keys key) : this(key, false, false, false) { }
            
            public KeyCombo(Keys key, bool isAlt, bool isShift, bool isControl)
            {
                Key = key;
                IsAlt = isAlt;
                IsShift = isShift;
                IsControl = isControl;
            }

            public override string ToString()
            {
                return
                    (IsAlt ? "Alt+" : "") +
                    (IsControl ? "Ctrl+" : "") +
                    (IsShift ? "Shift+" : "") +
                    Key.ToString();
            }

            public string ToScreenString()
            {
                return
                    (IsAlt ? "[Alt]+" : "") +
                    (IsControl ? "[Ctrl]+" : "") +
                    (IsShift ? "[Shift]+" : "") +
                    KeysToString(Key);
            }

            public static KeyCombo Parse(string s)
            {
                //// All text up to ": " is the Keys enum:
                //string keystr = s.Substring(0, s.IndexOf(':'));
                //Keys key = (Keys)Enum.Parse(typeof(Keys), keystr);

                //s = s.Substring(s.IndexOf(' '));

                //// Now if 's' contains Alt, Shift, and/or Ctrl, they're modifiers.
                //bool alt = s.Contains("[Alt]");
                //bool shift = s.Contains("[Shift]");
                //bool ctrl = s.Contains("[Ctrl]");

                //return new KeyCombo(key, alt, shift, ctrl);

                string p = s.ToLowerInvariant();
                bool alt = p.Contains("alt+");
                bool ctrl = p.Contains("ctrl+");
                bool shift = p.Contains("shift+");
                string k = "";

                if (alt | ctrl | shift)
                    k = s.Substring(s.LastIndexOf('+') + 1);
                else
                    k = s;

                Keys key = (Keys)Enum.Parse(typeof(Keys), k);

                return new KeyCombo(key, alt, shift, ctrl);
            }

        }

		public int KeyboardEventDelayMS { get; set; }

		public Keyboard() { }
		public Keyboard(int keyboardEventDelayMS) {
			KeyboardEventDelayMS = keyboardEventDelayMS;
		}

		public void Down(Keys key) {
			byte scan = MapVirtualKey(key, 0);
			keybd_event(key, scan, KEYEVENTF_KEYDOWN, 0);
		}
		public void Down(char key) {
			short code = VkKeyScan(key);
			byte keycode = (byte)(code & 0xFF);
			byte scan = MapVirtualKey(keycode, 0);
			keybd_event(keycode, scan, KEYEVENTF_KEYDOWN, 0);
		}

		public void Up(Keys key) {
			byte scan = MapVirtualKey(key, 0);
			keybd_event(key, scan, KEYEVENTF_KEYUP, 0);
		}
		public void Up(char key) {
			short code = VkKeyScan(key);
			byte keycode = (byte)(code & 0xFF);
			byte scan = MapVirtualKey(keycode, 0);
			keybd_event(keycode, scan, KEYEVENTF_KEYUP, 0);
		}

        public void Press(KeyCombo combo)
        {
            if (combo.IsAlt) Down(Keys.Menu);
            if (combo.IsControl) Down(Keys.Control);
            if (combo.IsShift) Down(Keys.Shift);

            Thread.Sleep(10);

            Press(combo.Key);

            Thread.Sleep(10);
            
            if (combo.IsAlt) Up(Keys.Menu);
            if (combo.IsControl) Up(Keys.Control);
            if (combo.IsShift) Up(Keys.Shift);

            Thread.Sleep(10);
        }

		public void Press(Keys key) {
			Down(key);
			Thread.Sleep(KeyboardEventDelayMS / 2);
			Up(key);
			Thread.Sleep(KeyboardEventDelayMS / 2);
		}
		public void Press(char key) {
			short code = VkKeyScan(key);
			byte keycode = (byte)(code & 0xFF);
			byte scan = MapVirtualKey(keycode, 0);
			byte shift = (byte)((code & 0xFF00) / 0x100);
			bool didSpecial = false;
			int delayDiv = 2;

			if ((byte)(shift & 0x01) == 0x01) {
				Down(Keys.Shift);
				didSpecial = true;
				delayDiv = 4;
			}
			if ((byte)(shift & 0x02) == 0x02) {
				Down(Keys.Control);
				didSpecial = true;
				delayDiv = 4;
			}
			if ((byte)(shift & 0x04) == 0x04) {
				Down(Keys.Menu);
				didSpecial = true;
				delayDiv = 4;
			}

			if (didSpecial)
				Thread.Sleep(KeyboardEventDelayMS / delayDiv);

			Down(key);
			Thread.Sleep(KeyboardEventDelayMS / delayDiv);
			Up(key);
			Thread.Sleep(KeyboardEventDelayMS / delayDiv);

			if ((byte)(shift & 0x01) == 0x01) {
				Up(Keys.Shift);
			}
			if ((byte)(shift & 0x02) == 0x02) {
				Up(Keys.Control);
			}
			if ((byte)(shift & 0x04) == 0x04) {
				Up(Keys.Menu);
			}

			if (didSpecial)
				Thread.Sleep(KeyboardEventDelayMS / delayDiv);
		}

		public void SimpleSendKeys(string keys) {
			keys.ToCharArray().ForEach(c => Press(c));
		}

		public void SendKeys(string keys) {
			char curChar;
			int index = 0;

			string specialSection = "";
			bool inSpecialSection = false;

			bool shift = false;
			bool control = false;
			bool alt = false;

			do {
				curChar = keys[index];
				index++;

				if (curChar == '{') {
					// Begin special section
					specialSection = "";
					inSpecialSection = true;
					continue;
				}

				if (inSpecialSection && curChar == '}') {
					// End special section
					inSpecialSection = false;

					if (shift) { Down(Keys.Shift); }
					if (control) { Down(Keys.Control); }
					if (alt) { Down(Keys.Menu); }

					if (shift || control || alt) Thread.Sleep(KeyboardEventDelayMS / 2);

					#region Special Command Handling

					// Handle special section here
					switch (specialSection.ToUpper()) {
						case "+":
							Press('+');
							break;
						case "^":
							Press('^');
							break;
						case "%":
							Press('%');
							break;
						case "~":
							Press('~');
							break;
						case "{":
							Press('{');
							break;
						case "}":
							Press('}');
							break;
						case "[":
							Press('[');
							break;
						case "]":
							Press(']');
							break;
						case "alt":
							Press(Keys.Menu);
							break;
						case "BACKSPACE":
						case "BS":
						case "BKSP":
							Press(Keys.Back);
							break;
						case "BREAK":
							Press(Keys.Pause);
							break;
						case "CAPSLOCK":
							Press(Keys.CapsLock);
							break;
						case "DELETE":
						case "DEL":
							Press(Keys.Delete);
							break;
						case "DOWN":
							Press(Keys.Down);
							break;
						case "END":
							Press(Keys.End);
							break;
						case "ENTER":
							Press(Keys.Return);
							break;
						case "ESC":
							Press(Keys.Escape);
							break;
						case "HELP":
							Press(Keys.Help);
							break;
						case "HOME":
							Press(Keys.Home);
							break;
						case "INSERT":
						case "INS":
							Press(Keys.Insert);
							break;
						case "LEFT":
							Press(Keys.Left);
							break;
						case "NUMLOCK":
							Press(Keys.NumLock);
							break;
						case "PGDN":
							Press(Keys.PageDown);
							break;
						case "PGUP":
							Press(Keys.PageUp);
							break;
						case "PRTSC":
							Press(Keys.Print);
							break;
						case "RIGHT":
							Press(Keys.Right);
							break;
						case "SCROLLLOCK":
							Press(Keys.ScrollLock);
							break;
						case "TAB":
							Press(Keys.Tab);
							break;
						case "UP":
							Press(Keys.Up);
							break;
						case "F1":
							Press(Keys.F1);
							break;
						case "F2":
							Press(Keys.F2);
							break;
						case "F3":
							Press(Keys.F3);
							break;
						case "F4":
							Press(Keys.F4);
							break;
						case "F5":
							Press(Keys.F5);
							break;
						case "F6":
							Press(Keys.F6);
							break;
						case "F7":
							Press(Keys.F7);
							break;
						case "F8":
							Press(Keys.F8);
							break;
						case "F9":
							Press(Keys.F9);
							break;
						case "F10":
							Press(Keys.F10);
							break;
						case "F11":
							Press(Keys.F11);
							break;
						case "F12":
							Press(Keys.F12);
							break;
						case "F13":
							Press(Keys.F13);
							break;
						case "F14":
							Press(Keys.F14);
							break;
						case "F15":
							Press(Keys.F15);
							break;
						case "F16":
							Press(Keys.F16);
							break;
						default:

							// Correct syntax is '$ #.'
							// single character, space, any number of digits

							if (specialSection.Length < 3) {
								// Can't be correct - skip it
								break;
							}

							if (specialSection[1] != ' ') {
								// not correct format - skip it
								break;
							}

							string[] parms = specialSection.Split(' ');
							int numIters;
							if (!int.TryParse(parms[1], out numIters)) {
								// not correct format - skip it
								break;
							}

							char repeatChar = parms[0][0];

							// Okay - correct format, let's apply the repeat.
							for (int i = 0; i < numIters; i++) {
								Press(repeatChar);
							}

							break;
					}

					#endregion

					if (shift) { Up(Keys.Shift); }
					if (control) { Up(Keys.Control); }
					if (alt) { Up(Keys.Menu); }

					if (shift || control || alt) Thread.Sleep(KeyboardEventDelayMS / 2);

					shift = false; control = false; alt = false;

					continue;
				}

				if (inSpecialSection) {
					specialSection += curChar;
					continue;
				}

				// Check to see if this character is a modifier
				switch (curChar) {
					case '+':
						shift = true;
						continue;
					case '^':
						control = true;
						continue;
					case '%':
						alt = true;
						continue;
				}

				if (shift) { Down(Keys.Shift); }
				if (control) { Down(Keys.Control); }
				if (alt) { Down(Keys.Menu); }

				if (shift || control || alt) Thread.Sleep(KeyboardEventDelayMS / 2);

				if (curChar == '~') {
					Press(Keys.Return);
				} else {
					Press(curChar);
				}

				if (shift) { Up(Keys.Shift); }
				if (control) { Up(Keys.Control); }
				if (alt) { Up(Keys.Menu); }

				if (shift || control || alt) Thread.Sleep(KeyboardEventDelayMS / 2);

				shift = false; control = false; alt = false;

			} while (index <= keys.Length - 1);
		}

	}
}
