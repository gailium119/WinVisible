namespace WinVisible
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    internal static class HotkeyManager
    {
        private static HotkeyAddedHandler HideHotkeyAdded;
        private static HotkeyAddedHandler ShowHotkeyAdded;
        private static HotkeyAddedHandler KillHotkeyAdded;
        private static Hotkey _hideHotkey;
        private static Hotkey _showHotkey;
        private static Hotkey _killHotkey;
        private static System.Windows.Forms.Control _control;

        public static event HotkeyAddedHandler HideHotkeyAddedBase
        {
            add
            {
                HotkeyAddedHandler hideHotkeyAdded = HideHotkeyAdded;
                while (true)
                {
                    HotkeyAddedHandler comparand = hideHotkeyAdded;
                    HotkeyAddedHandler handler3 = comparand + value;
                    hideHotkeyAdded = Interlocked.CompareExchange<HotkeyAddedHandler>(ref HideHotkeyAdded, handler3, comparand);
                    if (ReferenceEquals(hideHotkeyAdded, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                HotkeyAddedHandler hideHotkeyAdded = HideHotkeyAdded;
                while (true)
                {
                    HotkeyAddedHandler comparand = hideHotkeyAdded;
                    HotkeyAddedHandler handler3 = comparand - value;
                    hideHotkeyAdded = Interlocked.CompareExchange<HotkeyAddedHandler>(ref HideHotkeyAdded, handler3, comparand);
                    if (ReferenceEquals(hideHotkeyAdded, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static event HotkeyAddedHandler KillHotkeyAddedBase
        {
            add
            {
                HotkeyAddedHandler killHotkeyAdded = KillHotkeyAdded;
                while (true)
                {
                    HotkeyAddedHandler comparand = killHotkeyAdded;
                    HotkeyAddedHandler handler3 = comparand + value;
                    killHotkeyAdded = Interlocked.CompareExchange<HotkeyAddedHandler>(ref KillHotkeyAdded, handler3, comparand);
                    if (ReferenceEquals(killHotkeyAdded, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                HotkeyAddedHandler killHotkeyAdded = KillHotkeyAdded;
                while (true)
                {
                    HotkeyAddedHandler comparand = killHotkeyAdded;
                    HotkeyAddedHandler handler3 = comparand - value;
                    killHotkeyAdded = Interlocked.CompareExchange<HotkeyAddedHandler>(ref KillHotkeyAdded, handler3, comparand);
                    if (ReferenceEquals(killHotkeyAdded, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static event HotkeyAddedHandler ShowHotkeyAddedBase
        {
            add
            {
                HotkeyAddedHandler showHotkeyAdded = ShowHotkeyAdded;
                while (true)
                {
                    HotkeyAddedHandler comparand = showHotkeyAdded;
                    HotkeyAddedHandler handler3 = comparand + value;
                    showHotkeyAdded = Interlocked.CompareExchange<HotkeyAddedHandler>(ref ShowHotkeyAdded, handler3, comparand);
                    if (ReferenceEquals(showHotkeyAdded, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                HotkeyAddedHandler showHotkeyAdded = ShowHotkeyAdded;
                while (true)
                {
                    HotkeyAddedHandler comparand = showHotkeyAdded;
                    HotkeyAddedHandler handler3 = comparand - value;
                    showHotkeyAdded = Interlocked.CompareExchange<HotkeyAddedHandler>(ref ShowHotkeyAdded, handler3, comparand);
                    if (ReferenceEquals(showHotkeyAdded, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static Hotkey HideHotkey
        {
            get => 
                _hideHotkey;
            set
            {
                if (_hideHotkey != null)
                {
                    _hideHotkey.Unregister();
                }
                _hideHotkey = value;
                if (HideHotkeyAdded != null)
                {
                    HideHotkeyAdded(_hideHotkey);
                }
            }
        }

        public static Hotkey ShowHotkey
        {
            get => 
                _showHotkey;
            set
            {
                if (_showHotkey != null)
                {
                    _showHotkey.Unregister();
                }
                _showHotkey = value;
                if (ShowHotkeyAdded != null)
                {
                    ShowHotkeyAdded(_showHotkey);
                }
            }
        }

        public static Hotkey KillHotkey
        {
            get => 
                _killHotkey;
            set
            {
                if (_killHotkey != null)
                {
                    _killHotkey.Unregister();
                }
                _killHotkey = value;
                if (KillHotkeyAdded != null)
                {
                    KillHotkeyAdded(_killHotkey);
                }
            }
        }

        public static System.Windows.Forms.Control Control
        {
            get => 
                _control;
            set => 
                _control = value;
        }

        public delegate void HotkeyAddedHandler(Hotkey hotkey);
    }
}

