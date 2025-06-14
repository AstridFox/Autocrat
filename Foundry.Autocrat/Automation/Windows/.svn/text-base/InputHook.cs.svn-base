using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Foundry.Autocrat.Automation.Windows {

	public static class InputHook {

        private static event MouseEventHandler m_MouseMove;
        public static event MouseEventHandler MouseMove
        {
            add
            {
                if (m_MouseMove == null)
                {
                    HookManager.MouseMove += HookManager_MouseMove;
                }
                m_MouseMove += value;
            }

            remove
            {
                m_MouseMove -= value;
                if (m_MouseMove == null)
                {
                    HookManager.MouseMove -= HookManager_MouseMove;
                }
            }
        }

        static void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MouseMove != null)
            {
                m_MouseMove.Invoke(sender, e);
            }
        }

        private static event MouseEventHandler m_MouseClick;
        public static event MouseEventHandler MouseClick
        {
            add
            {
                if (m_MouseClick == null)
                {
                    HookManager.MouseClick += HookManager_MouseClick;
                }
                m_MouseClick += value;
            }

            remove
            {
                m_MouseClick -= value;
                if (m_MouseClick == null)
                {
                    HookManager.MouseClick -= HookManager_MouseClick;
                }
            }
        }

        static void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_MouseClick != null)
            {
                m_MouseClick.Invoke(sender, e);
            }
        }

        private static event MouseEventHandler m_MouseDown;
        public static event MouseEventHandler MouseDown
        {
            add
            {
                if (m_MouseDown == null)
                {
                    HookManager.MouseDown += HookManager_MouseDown;
                }
                m_MouseDown += value;
            }

            remove
            {
                m_MouseDown -= value;
                if (m_MouseDown == null)
                {
                    HookManager.MouseDown -= HookManager_MouseDown;
                }
            }
        }

        static void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_MouseDown != null)
            {
                m_MouseDown.Invoke(sender, e);
            }
        }

        private static event MouseEventHandler m_MouseUp;
        public static event MouseEventHandler MouseUp
        {
            add
            {
                if (m_MouseUp == null)
                {
                    HookManager.MouseUp += HookManager_MouseUp;
                }
                m_MouseUp += value;
            }

            remove
            {
                m_MouseUp -= value;
                if (m_MouseUp == null)
                {
                    HookManager.MouseUp -= HookManager_MouseUp;
                }
            }
        }

        static void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_MouseUp != null)
            {
                m_MouseUp.Invoke(sender, e);
            }
        }

        private static event MouseEventHandler m_MouseDoubleClick;
        public static event MouseEventHandler MouseDoubleClick
        {
            add
            {
                if (m_MouseDoubleClick == null)
                {
                    HookManager.MouseDoubleClick += HookManager_MouseDoubleClick;
                }
                m_MouseDoubleClick += value;
            }

            remove
            {
                m_MouseDoubleClick -= value;
                if (m_MouseDoubleClick == null)
                {
                    HookManager.MouseDoubleClick -= HookManager_MouseDoubleClick;
                }
            }
        }

        static void HookManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (m_MouseDoubleClick != null)
            {
                m_MouseDoubleClick.Invoke(sender, e);
            }
        }


        private static event EventHandler<MouseEventExtArgs> m_MouseMoveExt;
        public static event EventHandler<MouseEventExtArgs> MouseMoveExt
        {
            add
            {
                if (m_MouseMoveExt == null)
                {
                    HookManager.MouseMoveExt += HookManager_MouseMoveExt;
                }
                m_MouseMoveExt += value;
            }

            remove
            {
                m_MouseMoveExt -= value;
                if (m_MouseMoveExt == null)
                {
                    HookManager.MouseMoveExt -= HookManager_MouseMoveExt;
                }
            }
        }

        static void HookManager_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            if (m_MouseMoveExt != null)
            {
                m_MouseMoveExt.Invoke(sender, e);
            }
        }

        private static event EventHandler<MouseEventExtArgs> m_MouseClickExt;
        public static event EventHandler<MouseEventExtArgs> MouseClickExt
        {
            add
            {
                if (m_MouseClickExt == null)
                {
                    HookManager.MouseClickExt += HookManager_MouseClickExt;
                }
                m_MouseClickExt += value;
            }

            remove
            {
                m_MouseClickExt -= value;
                if (m_MouseClickExt == null)
                {
                    HookManager.MouseClickExt -= HookManager_MouseClickExt;
                }
            }
        }

        static void HookManager_MouseClickExt(object sender, MouseEventExtArgs e)
        {
            if (m_MouseClickExt != null)
            {
                m_MouseClickExt.Invoke(sender, e);
            }
        }


        private static event KeyPressEventHandler m_KeyPress;
        public static event KeyPressEventHandler KeyPress
        {
            add
            {
                if (m_KeyPress==null)
                {
                    HookManager.KeyPress +=HookManager_KeyPress;
                }
                m_KeyPress += value;
            }
            remove
            {
                m_KeyPress -= value;
                if (m_KeyPress == null)
                {
                    HookManager.KeyPress -= HookManager_KeyPress;
                }
            }
        }

        static void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (m_KeyPress != null)
            {
                m_KeyPress.Invoke(sender, e);
            }
        }

        private static event KeyEventHandler m_KeyUp;
        public static event KeyEventHandler KeyUp
        {
            add
            {
                if (m_KeyUp == null)
                {
                    HookManager.KeyUp += HookManager_KeyUp;
                }
                m_KeyUp += value;
            }
            remove
            {
                m_KeyUp -= value;
                if (m_KeyUp == null)
                {
                    HookManager.KeyUp -= HookManager_KeyUp;
                }
            }
        }

        static private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            if (m_KeyUp != null)
            {
                m_KeyUp.Invoke(sender, e);
            }
        }

        private static event KeyEventHandler m_KeyDown;
        public static event KeyEventHandler KeyDown
        {
            add
            {
                if (m_KeyDown == null)
                {
                    HookManager.KeyDown += HookManager_KeyDown;
                }
                m_KeyDown += value;
            }
            remove
            {
                m_KeyDown -= value;
                if (m_KeyDown == null)
                {
                    HookManager.KeyDown -= HookManager_KeyDown;
                }
            }
        }

        static private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            m_KeyDown.Invoke(sender, e);
        }

	}

}
