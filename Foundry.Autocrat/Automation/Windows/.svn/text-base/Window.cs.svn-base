using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;

namespace Foundry.Autocrat.Automation.Windows {
	public class Window {

		#region INTEROP

		[DllImport("user32")]
		protected static extern int EnumWindows(EnumCallback lpEnumFunc, int lParam);
		[DllImport("user32")]
		protected static extern int EnumDesktopWindows(IntPtr hDesktop, EnumCallback lpEnumFunc, int lParam);
		[DllImport("user32")]
		protected static extern int GetWindowTextLength(IntPtr hwnd);
		[DllImport("user32")]
		protected static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);
		[DllImport("user32")]
		protected static extern int SetForegroundWindow(IntPtr hwnd);
		[DllImport("user32.dll")]
		protected static extern int SetActiveWindow(IntPtr hWnd);
		[DllImport("user32")]
		protected static extern bool SetWindowPos(IntPtr hWnd, TopmostSetting hWndInsertAfter, int X, int Y, int cx, int cy, ShowWindowFlags uFlags);
		[DllImport("user32")]
		protected static extern int GetWindowRect(IntPtr hwnd, ref Rectangle lpRect);
		[DllImport("user32")]
		protected static extern int GetClientRect(IntPtr hwnd, ref Rectangle lpRect);
		[DllImport("user32")]
		protected static extern int MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool bRepaint);
		[DllImport("user32")]
		protected static extern IntPtr GetForegroundWindow();
		[DllImport("user32")]
		protected static extern int ShowWindow(IntPtr hwnd, WindowState nCmdShow);
		[DllImport("user32")]
		protected static extern int ClientToScreen(IntPtr hwnd, ref Point lpPoint);
		[DllImport("user32")]
		protected static extern bool FlashWindow(IntPtr hwnd, bool bInvert);
		[DllImport("user32")]
		protected static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);
		[DllImport("user32", EntryPoint = "GetDesktopWindow")]
		protected static extern IntPtr GetDesktopWindowA();
		[DllImport("user32.dll")]
		protected static extern bool SetWindowText(IntPtr hWnd, string lpString);
		[DllImport("user32.dll")]
		protected static extern IntPtr SetCapture(IntPtr hWnd);

		protected delegate bool EnumCallback(IntPtr hwnd, int lParam);

		protected enum WindowState : int {
			/// <summary>
			/// Hides the window and activates another window.
			/// </summary>
			Hide = 0,
			/// <summary>
			/// Activates and displays a window. If the window is minimized or
			/// maximized, the system restores it to its original size and position.
			/// An application should specify this flag when displaying the window
			/// for the first time.
			/// </summary>
			Normal = 1,
			/// <summary>
			/// Activates the window and displays it as a minimized window.
			/// </summary>
			ShowMinimized = 2,
			/// <summary>
			/// Maximizes the specified window.
			/// </summary>
			Maximize = 3, // is this the right value?
			/// <summary>
			/// Activates the window and displays it as a maximized window.
			/// </summary>      
			ShowMaximized = 3,
			/// <summary>
			/// Displays a window in its most recent size and position. This value
			/// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except
			/// the window is not actived.
			/// </summary>
			ShowNoActivate = 4,
			/// <summary>
			/// Activates the window and displays it in its current size and position.
			/// </summary>
			Show = 5,
			/// <summary>
			/// Minimizes the specified window and activates the next top-level
			/// window in the Z order.
			/// </summary>
			Minimize = 6,
			/// <summary>
			/// Displays the window as a minimized window. This value is similar to
			/// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the
			/// window is not activated.
			/// </summary>
			ShowMinNoActive = 7,
			/// <summary>
			/// Displays the window in its current size and position. This value is
			/// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the
			/// window is not activated.
			/// </summary>
			ShowNA = 8,
			/// <summary>
			/// Activates and displays the window. If the window is minimized or
			/// maximized, the system restores it to its original size and position.
			/// An application should specify this flag when restoring a minimized window.
			/// </summary>
			Restore = 9,
			/// <summary>
			/// Sets the show state based on the SW_* value specified in the
			/// STARTUPINFO structure passed to the CreateProcess function by the
			/// program that started the application.
			/// </summary>
			ShowDefault = 10,
			/// <summary>
			///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
			/// that owns the window is not responding. This flag should only be
			/// used when minimizing windows from a different thread.
			/// </summary>
			ForceMinimize = 11
		}

		[StructLayout(LayoutKind.Sequential)]
		protected struct WINDOWINFO {
			public uint cbSize;
			public Rectangle rcWindow;
			public Rectangle rcClient;
			public uint dwStyle;
			public uint dwExStyle;
			public uint dwWindowStatus;
			public uint cxWindowBorders;
			public uint cyWindowBorders;
			public ushort atomWindowType;
			public ushort wCreatorVersion;
		}

		protected enum TopmostSetting : int {
			HWND_TOPMOST = -1,
			HWND_NOTOPMOST = -2,
			HWND_TOP = 0
		}

		[Flags]
		protected enum ShowWindowFlags : uint {
			SWP_NOSIZE = 0x0001,
			SWP_NOMOVE = 0x0002,
			SWP_NOZORDER = 0x0004,
			SWP_NOREDRAW = 0x0008,
			SWP_NOACTIVATE = 0x0010,
			SWP_FRAMECHANGED = 0x0020,
			SWP_SHOWWINDOW = 0x0040,
			SWP_HIDEWINDOW = 0x0080,
			SWP_NOCOPYBITS = 0x0100,
			SWP_NOOWNERZORDER = 0x0200,
			SWP_NOSENDCHANGING = 0x0400,
			TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE
		}

		#endregion

		#region Static Members

		/// <summary>
		/// Finds a window by the window's caption.
		/// </summary>
		/// <param name="caption">The partial caption for which to find a window.</param>
		/// <returns>A window representing the found window, or the desktop window if no such window matches the specified caption.</returns>
		public static Window FindWindowByCaption(string caption) {
			IntPtr r = IntPtr.Zero;

			EnumDesktopWindows(
				IntPtr.Zero,
				(hwnd, lParam) =>
				{
					string title = GetCaption(hwnd);
					if (title.ToLowerInvariant().Contains(caption.ToLowerInvariant())) {
						r = hwnd;
						return false;
					}
					return true;
				}, 0);

			return new Window(r);
		}

		/// <summary>
		/// Finds a window by its process.
		/// </summary>
		/// <param name="p">The process to search.</param>
		/// <returns>A window representing the main window of the specified process.</returns>
		public static Window FindWindowByProcess(Process p) {
			return new Window(p.MainWindowHandle);
		}

		/// <summary>
		/// Gets the active window.
		/// </summary>
		/// <returns>A window representing the currently active window.</returns>
		public static Window GetActiveWindow() {
			return new Window(GetForegroundWindow());
		}

		/// <summary>
		/// Gets the desktop window.
		/// </summary>
		/// <returns>A window representing the desktop window.</returns>
		public static Window GetDesktopWindow() {
			return new Window(IntPtr.Zero);
		}

		private static string GetCaption(IntPtr hwnd) {
			int tLen = GetWindowTextLength(hwnd);
			var sb = new StringBuilder(tLen + 1);
			GetWindowText(hwnd, sb, tLen + 1);
			return sb.ToString();
		}

		#endregion

		#region Properties

		/// <summary>
		/// This Window's handle.
		/// </summary>
		public IntPtr Hwnd { get; private set; }

		/// <summary>
		/// Gets a value indicating if this window is Active.
		/// </summary>
		public bool IsActive {
			get { return GetForegroundWindow() == Hwnd; }
		}

		/// <summary>
		/// Gets or sets this window's caption.
		/// </summary>
		public string Caption {
			get { return GetCaption(Hwnd); }
			set { SetWindowText(Hwnd, value); }
		}

		/// <summary>
		/// Gets this window's bounding rectangle in Desktop coordinates.
		/// </summary>
		public Rectangle WindowRectangle {
			get {
				var r = new Rectangle();
				GetWindowRect(Hwnd, ref r);
				r.Width = r.Width - r.X;
				r.Height = r.Height - r.Y;
				return r;
			}
		}

		/// <summary>
		/// Gets this window's origin in Desktop coordinates.
		/// </summary>
		public Point WindowPosition {
			get {
				var r = WindowRectangle;
				return new Point(r.X, r.Y);
			}
		}

		/// <summary>
		/// Gets this window's size.
		/// </summary>
		public Size WindowSize {
			get {
				var r = WindowRectangle;
				return new Size(r.Width, r.Height);
			}
		}

		/// <summary>
		/// Gets this window's client space bounds in Desktop coordinates.
		/// </summary>
		public Rectangle ClientRectangle {
			get {
				var r = new Rectangle();
				GetClientRect(Hwnd, ref r);
				var p = ClientPosition;
				r.X = p.X; r.Y = p.Y;
				return r;
			}
		}

		/// <summary>
		/// Gets this window's client origin in Desktop coordinates.
		/// </summary>
		public Point ClientPosition {
			get {
				var w = WindowPosition;
				var o = ClientOffset;
				return new Point(w.X + o.Width, w.Y + o.Height);
			}
		}

		/// <summary>
		/// Gets this window's client size.
		/// </summary>
		public Size ClientSize {
			get {
				var r = ClientRectangle;
				return new Size(r.Width, r.Height);
			}
		}

		/// <summary>
		/// Gets the size of this window's client offset.
		/// </summary>
		public Size ClientOffset {
			get {
				var winPos = WindowPosition;
				var cts = new Point(0, 0);
				ClientToScreen(Hwnd, ref cts);

				return new Size(cts.X - winPos.X, cts.Y - winPos.Y);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating if this Window is always-on-top.
		/// </summary>
		public bool Topmost {
			set {
				if (value) {
					SetWindowPos(Hwnd, TopmostSetting.HWND_TOPMOST, 0, 0, 0, 0, ShowWindowFlags.TOPMOST_FLAGS);
				} else {
					SetWindowPos(Hwnd, TopmostSetting.HWND_NOTOPMOST, 0, 0, 0, 0, ShowWindowFlags.TOPMOST_FLAGS);
				}
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new window initialized to the specified window handle.
		/// </summary>
		/// <param name="hwnd">The window handle to initialize this Window to.</param>
		public Window(IntPtr hwnd) {
			Hwnd = hwnd;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Brings this window to the foreground and gives it focus.
		/// </summary>
		public void Activate() {
			SetCapture(Hwnd);
			SetActiveWindow(Hwnd);
			SetForegroundWindow(Hwnd);
		}

		/// <summary>
		/// Flashes the taskbar icon for this window.
		/// </summary>
		public void Flash() {
			FlashWindow(Hwnd, true);
		}

		/// <summary>
		/// Moves this window to the specified destination in Desktop coordinates.
		/// </summary>
		/// <param name="destination">The location to move this window's origin to.</param>
		public void MoveWindow(Point destination) {
			MoveAndResize(destination, WindowSize);
		}

		/// <summary>
		/// Resizes this window to the specified size.
		/// </summary>
		/// <param name="newSize">The new size of the window.</param>
		public void ResizeWindow(Size newSize) {
			MoveAndResize(WindowPosition, newSize);
		}

		/// <summary>
		/// Moves this window to the specified destination in Desktop coordinates, and resizes this window to the specified size.
		/// </summary>
		/// <param name="destination">The location to move this window's origin to.</param>
		/// <param name="newSize">The new size of the window.</param>
		public void MoveAndResize(Point destination, Size newSize) {
			MoveWindow(Hwnd, destination.X, destination.Y, newSize.Width, newSize.Height, true);
		}

		/// <summary>
		/// Minimizes this window.
		/// </summary>
		public void Minimize() {
			ShowWindow(Hwnd, WindowState.Minimize);
		}

		/// <summary>
		/// Maximizes this window.
		/// </summary>
		public void Maximize() {
			ShowWindow(Hwnd, WindowState.Maximize);
		}

		/// <summary>
		/// Restores this window.
		/// </summary>
		public void Restore() {
			ShowWindow(Hwnd, WindowState.Restore);
		}

		#region Coordinate System Manipulation

		/// <summary>
		/// Represents the coordinate system used for a point on the screen.
		/// </summary>
		public enum CoordinateSystem {
			Desktop,
			Window,
			Client
		}

		/// <summary>
		/// Converts a Window point to a Desktop point for this window.
		/// </summary>
		/// <param name="window">The window point to convert.</param>
		/// <returns>A Desktop point which corresponds to the given Window point.</returns>
		public Point WindowToDesktop(Point window) {
			var w = WindowPosition;
			return new Point(w.X + window.X, w.Y + window.Y);
		}

		/// <summary>
		/// Converts a Client point to a Desktop point for this window.
		/// </summary>
		/// <param name="window">The client point to convert.</param>
		/// <returns>A Desktop point which corresponds to the given Client point.</returns>
		public Point ClientToDesktop(Point client) {
			var c = ClientPosition;
			return new Point(c.X + client.X, c.Y + client.Y);
		}

		/// <summary>
		/// Converts a Desktop point to a Window point for this window.
		/// </summary>
		/// <param name="window">The desktop point to convert.</param>
		/// <returns>A Window point which corresponds to the given Desktop point.</returns>
		public Point DesktopToWindow(Point desktop) {
			var w = WindowPosition;
			return new Point(desktop.X - w.X, desktop.Y - w.Y);
		}

		/// <summary>
		/// Converts a Desktop point to a Client point for this window.
		/// </summary>
		/// <param name="window">The desktop point to convert.</param>
		/// <returns>A Client point which corresponds to the given Desktop point.</returns>
		public Point DesktopToClient(Point desktop) {
			var c = ClientPosition;
			return new Point(desktop.X - c.X, desktop.Y - c.Y);
		}

		/// <summary>
		/// Converts a Window point to a Client point for this window.
		/// </summary>
		/// <param name="window">The window point to convert.</param>
		/// <returns>A Client point which corresponds to the given Window point.</returns>
		public Point WindowToClient(Point window) {
			var d = WindowToDesktop(window);
			return DesktopToClient(d);
		}

		/// <summary>
		/// Converts a Client point to a Window point for this window.
		/// </summary>
		/// <param name="window">The client point to convert.</param>
		/// <returns>A Window point which corresponds to the given Client point.</returns>
		public Point ClientToWindow(Point client) {
			var d = ClientToDesktop(client);
			return DesktopToWindow(d);
		}

		/// <summary>
		/// Converts a point from one coordinate system to another for this window.
		/// </summary>
		/// <param name="p">The point to convert.</param>
		/// <param name="from">The point's original coordinate system.</param>
		/// <param name="to">The point's new coordinate system.</param>
		/// <returns>A point in the 'to' coordinate system which corresponds to the given point in the 'from' coordinate system.</returns>
		public Point ConvertCoordinates(Point p, CoordinateSystem from, CoordinateSystem to) {
			if (from == to) return p;
			
			switch (from) {
				case CoordinateSystem.Desktop:
					if (to == CoordinateSystem.Client) return DesktopToClient(p);
					if (to == CoordinateSystem.Window) return DesktopToWindow(p);
					break;
				case CoordinateSystem.Window:
					if (to == CoordinateSystem.Desktop) return WindowToDesktop(p);
					if (to == CoordinateSystem.Client) return WindowToClient(p);
					break;
				case CoordinateSystem.Client:
					if (to == CoordinateSystem.Desktop) return ClientToDesktop(p);
					if (to == CoordinateSystem.Window) return ClientToWindow(p);
					break;
			}

			return p;
		}
		#endregion

		#endregion

	}
}
