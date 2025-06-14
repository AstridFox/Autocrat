# Global Input Hooks & Windows Automation

This section covers low-level input capture (mouse and keyboard hooks) and basic window control APIs provided in the `Automation.Windows` namespace.

## HookManager: Global Mouse & Keyboard Hooks

`HookManager` installs low-level Windows hooks on demand when the first subscriber attaches, and uninstalls when all subscribers detach. Example of installing the mouse hook:

```csharp
private static void EnsureSubscribedToGlobalMouseEvents()
{
    if (s_MouseHookHandle == 0)
    {
        s_MouseDelegate = MouseHookProc;
        s_MouseHookHandle = SetWindowsHookEx(
            WH_MOUSE_LL,
            s_MouseDelegate,
            Marshal.GetHINSTANCE(
                Assembly.GetExecutingAssembly().GetModules()[0]),
            0);
        if (s_MouseHookHandle == 0)
            throw new Win32Exception(Marshal.GetLastWin32Error());
    }
}
```
【F:Foundry.Autocrat/Automation/Windows/InputHook/HookManager.Callbacks.cs†L214-L234】

Once installed, the hook callback marshals native data and raises managed events:

```csharp
private static int MouseHookProc(int nCode, int wParam, IntPtr lParam)
{
    if (nCode >= 0)
    {
        MouseLLHookStruct data = Marshal.PtrToStructure<MouseLLHookStruct>(lParam);
        s_MouseMoveExt?.Invoke(null, new MouseEventExtArgs(wParam, lParam));
        if (e.Handled) return -1;
    }
    return CallNextHookEx(s_MouseHookHandle, nCode, wParam, lParam);
}
```
【F:Foundry.Autocrat/Automation/Windows/InputHook/HookManager.Callbacks.cs†L86-L105】

## Simulating Keyboard & Mouse

The `Keyboard` class wraps `keybd_event` and provides helper methods for key presses and combos:

```csharp
public void Press(KeyCombo combo)
{
    if (combo.IsAlt) Down(Keys.Menu);
    if (combo.IsControl) Down(Keys.Control);
    if (combo.IsShift) Down(Keys.Shift);
    Thread.Sleep(10);
    Press(combo.Key);
    Thread.Sleep(10);
    if (combo.IsControl) Up(Keys.Control);
    if (combo.IsShift) Up(Keys.Shift);
    if (combo.IsAlt) Up(Keys.Menu);
    Thread.Sleep(10);
}
```
【F:Foundry.Autocrat/Automation/Windows/Keyboard.cs†L445-L467】

Window manipulation and focus control are provided by the `Window` wrapper:

```csharp
public static Window FindWindowByCaption(string caption)
{
    IntPtr hwnd = IntPtr.Zero;
    EnumDesktopWindows(IntPtr.Zero, (h, l) =>
    {
        if (GetWindowText(h).Contains(caption)) { hwnd = h; return false; }
        return true;
    }, 0);
    return new Window(hwnd);
}

public void Activate()
{
    SetCapture(Hwnd);
    SetActiveWindow(Hwnd);
    SetForegroundWindow(Hwnd);
}
```
【F:Foundry.Autocrat/Automation/Windows/Window.cs†L332-L359】