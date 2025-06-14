using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using Foundry.Autocrat.Extensions.Fluent;
using Foundry.Autocrat.Extensions.Drawing;

namespace Foundry.Autocrat.Automation.Windows {
	public class Mouse {

		#region INTEROP

		[DllImport("user32.dll")]
		internal static extern bool GetCursorPos(out Point lpPoint);
		[DllImport("user32.dll")]
		internal static extern bool SetCursorPos(int X, int Y);
		[DllImport("user32")]
		internal static extern void mouse_event(MouseEventFlags dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

		[Flags()]
		internal enum MouseEventFlags : int {
			LeftDown = 0x2,
			LeftUp = 0x4,
			MiddleDown = 0x20,
			MiddleUp = 0x40,
			RightDown = 0x8,
			RightUp = 0x10
		}

		#endregion

		public enum MouseButton {
			Left,
			Middle,
			Right
		}

		public int MouseEventDelayMS { get; set; }

		public Mouse() { }
		public Mouse(int mouseEventDelayMS) {
			MouseEventDelayMS = mouseEventDelayMS;
		}

		#region Position-related methods

		public Point GetCursorPosition() {
			Point r;
			GetCursorPos(out r);
			return r;
		}
		public Point GetCursorPosition(Window window, Window.CoordinateSystem coordinates) {
			return window.ConvertCoordinates(GetCursorPosition(), Window.CoordinateSystem.Desktop, coordinates);
		}

		public void SetCursorPosition(Point newPosition, Window window, Window.CoordinateSystem coordinates) {
			SetCursorPosition(window.ConvertCoordinates(newPosition, coordinates, Window.CoordinateSystem.Desktop));
		}
		public void SetCursorPosition(Point newPosition) {
			SetCursorPos(newPosition.X, newPosition.Y);
		}

		public void LinearSmoothMove(Point newPosition, int steps, Window window, Window.CoordinateSystem coordinates) {
			LinearSmoothMove(window.ConvertCoordinates(newPosition, coordinates, Window.CoordinateSystem.Desktop), steps);
		}
	public void LinearSmoothMove(Point newPosition, int steps) {
		Point start = GetCursorPosition();
		PointF iterPoint = start;

		// Find the slope of the line segment defined by start and newPosition
		PointF slope = new PointF(newPosition.X - start.X, newPosition.Y - start.Y);

		// Divide by the number of steps
		slope.X = slope.X / steps;
		slope.Y = slope.Y / steps;

		// Move the mouse to each iterative point.
        for (int i = 0; i < steps; i++)
        {
            iterPoint = new PointF(iterPoint.X + slope.X, iterPoint.Y + slope.Y);
            SetCursorPosition(Point.Round(iterPoint));
            Thread.Sleep(MouseEventDelayMS);
        }

		// Move the mouse to the final destination.
		SetCursorPosition(newPosition);
	}

		#endregion

		#region Clicking-related methods

		public void Down(MouseButton button) {
			switch (button) {
				case MouseButton.Left:
					mouse_event(MouseEventFlags.LeftDown, 0, 0, 0, 0);
					break;
				case MouseButton.Middle:
					mouse_event(MouseEventFlags.MiddleDown, 0, 0, 0, 0);
					break;
				case MouseButton.Right:
					mouse_event(MouseEventFlags.RightDown, 0, 0, 0, 0);
					break;
			}
		}

		public void Up(MouseButton button) {
			switch (button) {
				case MouseButton.Left:
					mouse_event(MouseEventFlags.LeftUp, 0, 0, 0, 0);
					break;
				case MouseButton.Middle:
					mouse_event(MouseEventFlags.MiddleUp, 0, 0, 0, 0);
					break;
				case MouseButton.Right:
					mouse_event(MouseEventFlags.RightUp, 0, 0, 0, 0);
					break;
			}
		}

		public void Click(MouseButton button) {
			Down(button);
			Thread.Sleep(MouseEventDelayMS / 2);
			Up(button);
			Thread.Sleep(MouseEventDelayMS / 2);
		}
		public void Click(Window window, Point point, Window.CoordinateSystem coordinates, MouseButton button) {
			SetCursorPosition(point, window, coordinates);
			Thread.Sleep(MouseEventDelayMS / 3);
			Down(button);
			Thread.Sleep(MouseEventDelayMS / 3);
			Up(button);
			Thread.Sleep(MouseEventDelayMS / 3);
		}

		public void DoubleClick(MouseButton button) {
			Down(button);
			Thread.Sleep(MouseEventDelayMS / 4);
			Up(button);
			Thread.Sleep(MouseEventDelayMS / 4);
			Down(button);
			Thread.Sleep(MouseEventDelayMS / 4);
			Up(button);
			Thread.Sleep(MouseEventDelayMS / 4);
		}
		public void DoubleClick(Window window, Point point, Window.CoordinateSystem coordinates, MouseButton button) {
			SetCursorPosition(point, window, coordinates);
			Thread.Sleep(MouseEventDelayMS / 5);
			Down(button);
			Thread.Sleep(MouseEventDelayMS / 5);
			Up(button);
			Thread.Sleep(MouseEventDelayMS / 5);
			Down(button);
			Thread.Sleep(MouseEventDelayMS / 5);
			Up(button);
			Thread.Sleep(MouseEventDelayMS / 5);
		}

		#endregion

	}
}
