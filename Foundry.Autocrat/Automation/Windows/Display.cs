using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace Foundry.Autocrat.Automation.Windows {
	public class Display {

		#region INTEROP

		[DllImport("user32")] internal static extern IntPtr GetWindowDC(IntPtr hwnd);
		[DllImport("gdi32")] internal static extern int GetPixel(IntPtr hdc, int x, int y);
		[DllImport("user32")] internal static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

		#endregion

		#region Capture Methods

		/// <summary>
		/// Captures a pixel color from the specified point.
		/// </summary>
		/// <param name="point">The point in Desktop coordinates to capture.</param>
		/// <returns>The color of the specified pixel.</returns>
		public Color CapturePixel(Point point) {
			Window w = new Window(IntPtr.Zero);
			return CapturePixel(w, point, Window.CoordinateSystem.Desktop);
		}

		/// <summary>
		/// Captures a pixel color from the specified window at the specified point in Client coordinates.
		/// </summary>
		/// <param name="point">The point in Desktop coordinates to capture.</param>
		/// <param name="window">The window to capture a pixel from.</param>
		/// <returns>The color of the specified pixel.</returns>
		public Color CapturePixel(Window window, Point point) {
			return CapturePixel(window, point, Window.CoordinateSystem.Client);
		}
		public Color CapturePixel(Window window, Point point, Window.CoordinateSystem coordinates) {
			IntPtr hDC = IntPtr.Zero;

			try {
				hDC = GetWindowDC(window.Hwnd);
				if (hDC == IntPtr.Zero) return Color.Empty;

				Point windowPoint = window.ConvertCoordinates(point, coordinates, Window.CoordinateSystem.Window);

				int c = GetPixel(hDC, windowPoint.X, windowPoint.Y);
				return Color.FromArgb
					(c & 0xFF,
					(c & 0xFF00) >> 8,
					(c & 0xFF0000) >> 16);

			} finally {
				if (hDC != IntPtr.Zero) ReleaseDC(window.Hwnd, hDC);
			}
		}

		public Bitmap CaptureBitmap() {
			return CaptureBitmap(new Window(IntPtr.Zero), Screen.PrimaryScreen.Bounds, Window.CoordinateSystem.Desktop);
		}
		public Bitmap CaptureBitmap(Window window) {
			return CaptureBitmap(window, window.WindowRectangle, Window.CoordinateSystem.Desktop);
		}
		public Bitmap CaptureBitmap(Window window, Window.CoordinateSystem coordinates) {
			if (coordinates == Window.CoordinateSystem.Desktop || coordinates == Window.CoordinateSystem.Window) {
				return CaptureBitmap(window);
			} else {
				Rectangle r = window.ClientRectangle;
				return CaptureBitmap(window, r, Window.CoordinateSystem.Desktop);
			}
		}
		public Bitmap CaptureBitmap(Window window, Rectangle rect, Window.CoordinateSystem coordinates) {
			if (rect.Width == 0 || rect.Height == 0) return null;

			Point origin = window.ConvertCoordinates(new Point(rect.X, rect.Y), coordinates, Window.CoordinateSystem.Desktop);
			Size size = new Size(rect.Width, rect.Height);

			Bitmap b = new Bitmap(size.Width, size.Height);
			using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b)) {
				g.CopyFromScreen(origin, new Point(0, 0), size);
				return b;
			}
		}

		#endregion



	}
}
