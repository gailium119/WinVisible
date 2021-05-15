namespace WinVisible
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public class Hotkey : IMessageFilter
    {
        private const uint WM_HOTKEY = 0x312;
        private const uint MOD_ALT = 1;
        private const uint MOD_CONTROL = 2;
        private const uint MOD_SHIFT = 4;
        private const uint MOD_WIN = 8;
        private const uint ERROR_HOTKEY_ALREADY_REGISTERED = 0x581;
        private const int maximumID = 0xbfff;
        private static int currentID;
        private Keys _modifiers;
        private Keys _key;
        [XmlIgnore]
        private int id;
        [XmlIgnore]
        private bool registered;
        [XmlIgnore]
        private Control _windowControl;
        private HandledEventHandler Pressed;

        public event HandledEventHandler PressedBase
        {
            add
            {
                HandledEventHandler pressed = this.Pressed;
                while (true)
                {
                    HandledEventHandler comparand = pressed;
                    HandledEventHandler handler3 = comparand + value;
                    pressed = Interlocked.CompareExchange<HandledEventHandler>(ref this.Pressed, handler3, comparand);
                    if (ReferenceEquals(pressed, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                HandledEventHandler pressed = this.Pressed;
                while (true)
                {
                    HandledEventHandler comparand = pressed;
                    HandledEventHandler handler3 = comparand - value;
                    pressed = Interlocked.CompareExchange<HandledEventHandler>(ref this.Pressed, handler3, comparand);
                    if (ReferenceEquals(pressed, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public Hotkey(Keys keys, Control window)
        {
            this._windowControl = window;
            this._modifiers = this.ExtractModifiers(ref keys);
            this._key = keys;
            Application.AddMessageFilter(this);
        }

        private Keys ExtractModifiers(ref Keys keys)
        {
            Keys none = Keys.None;
            if ((keys & Keys.Shift) != Keys.None)
            {
                none |= Keys.Shift;
                keys &= ~Keys.Shift;
            }
            if ((keys & Keys.Alt) != Keys.None)
            {
                none |= Keys.Alt;
                keys &= ~Keys.Alt;
            }
            if ((keys & Keys.Control) != Keys.None)
            {
                none |= Keys.Control;
                keys &= ~Keys.Control;
            }
            return none;
        }

        ~Hotkey()
        {
            if (this.Registered)
            {
                this.Unregister();
            }
        }

        public static string GetKeyComboString(Keys keys, Keys modifiers)
        {
            string name = Enum.GetName(typeof(Keys), keys);
            switch (keys)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    name = name.Substring(1);
                    break;

                default:
                    break;
            }
            string str2 = "";
            if ((modifiers & Keys.Shift) != Keys.None)
            {
                str2 = str2 + "Shift+";
            }
            if ((modifiers & Keys.Control) != Keys.None)
            {
                str2 = str2 + "Control+";
            }
            if ((modifiers & Keys.Alt) != Keys.None)
            {
                str2 = str2 + "Alt+";
            }
            return (str2 + name);
        }

        private bool OnPressed()
        {
            HandledEventArgs e = new HandledEventArgs(false);
            if (this.Pressed != null)
            {
                this.Pressed(this, e);
            }
            return e.Handled;
        }

        public bool PreFilterMessage(ref Message message) => 
            (message.Msg == 0x312L) ? (this.registered && ((message.WParam.ToInt32() == this.id) && this.OnPressed())) : false;

        public void Register()
        {
            if (this.registered)
            {
                throw new InvalidOperationException("This hotkey has already been registered.");
            }
            if (!this.Empty)
            {
                this.id = currentID;
                currentID++;
                uint fsModifiers = (uint) (((((this._modifiers & Keys.Alt) != Keys.None) ? 1 : 0) | (((this._modifiers & Keys.Control) != Keys.None) ? 2 : 0)) | (((this._modifiers & Keys.Shift) != Keys.None) ? 4 : 0));
                if (RegisterHotKey(this._windowControl.Handle, this.id, fsModifiers, this._key) != 0)
                {
                    this.registered = true;
                }
                else
                {
                    if (Marshal.GetLastWin32Error() == 0x581L)
                    {
                        throw new InvalidOperationException("This hotkey has already been registered.");
                    }
                    throw new Win32Exception();
                }
            }
        }

        [DllImport("user32.dll", SetLastError=true)]
        private static extern int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);
        private void Reregister()
        {
            this.Unregister();
            this.Register();
        }

        public override string ToString() => 
            !this.Empty ? GetKeyComboString(this._key, this._modifiers) : "None";

        public bool Unregister()
        {
            if (!this.registered)
            {
                return false;
            }
            if (!this._windowControl.IsDisposed && (UnregisterHotKey(this._windowControl.Handle, this.id) == 0))
            {
                throw new Win32Exception();
            }
            this.registered = false;
            return true;
        }

        [DllImport("user32.dll", SetLastError=true)]
        private static extern int UnregisterHotKey(IntPtr hWnd, int id);

        public bool Empty =>
            this._key == Keys.None;

        public bool Registered =>
            this.registered;

        public Keys KeyCode =>
            this._key;

        public Keys Modifiers =>
            this._modifiers;

        public Keys Hotkeys
        {
            get => 
                this._key | this._modifiers;
            set
            {
                Keys keys = value;
                this._modifiers = this.ExtractModifiers(ref keys);
                this._key = keys;
                this.Reregister();
            }
        }
    }
}

