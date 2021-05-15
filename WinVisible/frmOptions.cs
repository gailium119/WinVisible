namespace WinVisible
{
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using WinVisible.Properties;

    public class frmOptions : Form
    {
        private const string HKEY_AUTORUN = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private Keys _showKey;
        private Keys _hideKey;
        private Keys _killKey;
        private IContainer components;
        private Button btnOK;
        private TextBox txtHideHotkey;
        private GroupBox groupBox1;
        private Label label2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox3;
        private ComboBox cmbCloseAction;
        private Label label1;
        private CheckBox chkAutoStart;
        private CheckBox chkWarnMe;
        private Button btnCancel;
        private Label label5;
        private TextBox txtShowHotkey;
        private Label label4;
        private CheckBox chkAutoSelect;
        private CheckBox chkRemember;
        private CheckBox chkHideMe;
        private Label label3;
        private CheckBox chkCheckUpdates;
        private GroupBox groupBox2;
        private CheckBox chkStartHidden;
        private Label label6;
        private TextBox txtKillHotkey;
        private PictureBox picKillDelete;
        private ToolTip ttMain;
        private Label label7;
        private CheckBox chkHideOnScreenSaver;

        public frmOptions()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chkWarnMe.Checked)
                {
                    this.WarnUserOnNonRecommended();
                }
                Settings.Default.WarnOnNonRecommended = this.chkWarnMe.Checked;
                Settings.Default.AppCloseAction = this.cmbCloseAction.SelectedIndex;
                Settings.Default.HideIcon = this.chkHideMe.Checked;
                Settings.Default.RememberProcesses = this.chkRemember.Checked;
                Settings.Default.AutoSelect = this.chkAutoSelect.Checked;
                Settings.Default.CheckUpdates = this.chkCheckUpdates.Checked;
                Settings.Default.StartHidden = this.chkStartHidden.Checked;
                Settings.Default.HideWhenScreenSaverActivates = this.chkHideOnScreenSaver.Checked;
                HotkeyManager.HideHotkey.Hotkeys = this._hideKey;
                HotkeyManager.ShowHotkey.Hotkeys = this._showKey;
                HotkeyManager.KillHotkey.Hotkeys = this._killKey;
                Settings.Default.HideHotkey = this._hideKey;
                Settings.Default.ShowHotkey = this._showKey;
                Settings.Default.KillHotKey = this._killKey;
                this.SetAutoStart(this.chkAutoStart.Checked);
                Settings.Default.Save();
            }
            catch (Exception exception1)
            {
                using (frmError error = new frmError(exception1))
                {
                    error.ShowDialog();
                }
                base.DialogResult = DialogResult.None;
                Settings.Default.Reload();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            this._hideKey = HotkeyManager.HideHotkey.KeyCode | HotkeyManager.HideHotkey.Modifiers;
            this._showKey = HotkeyManager.ShowHotkey.KeyCode | HotkeyManager.ShowHotkey.Modifiers;
            this._killKey = HotkeyManager.KillHotkey.KeyCode | HotkeyManager.KillHotkey.Modifiers;
            this.txtHideHotkey.Text = HotkeyManager.HideHotkey.ToString();
            this.txtShowHotkey.Text = HotkeyManager.ShowHotkey.ToString();
            this.txtKillHotkey.Text = HotkeyManager.KillHotkey.ToString();
            this.cmbCloseAction.SelectedIndex = Settings.Default.AppCloseAction;
            this.chkWarnMe.Checked = Settings.Default.WarnOnNonRecommended;
            this.chkHideMe.Checked = Settings.Default.HideIcon;
            this.chkRemember.Checked = Settings.Default.RememberProcesses;
            this.chkAutoSelect.Checked = Settings.Default.AutoSelect;
            this.chkAutoStart.Checked = this.IsAutoRunEnabled();
            this.chkCheckUpdates.Checked = Settings.Default.CheckUpdates;
            this.chkStartHidden.Checked = Settings.Default.StartHidden;
            this.chkHideOnScreenSaver.Checked = Settings.Default.HideWhenScreenSaverActivates;
        }

        private string GetEXEName()
        {
            int num = Application.ExecutablePath.LastIndexOf(@"\");
            string executablePath = Application.ExecutablePath;
            if (num > -1)
            {
                executablePath = Application.ExecutablePath.Substring(num + 1);
            }
            return executablePath;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmOptions));
            this.btnOK = new Button();
            this.txtHideHotkey = new TextBox();
            this.groupBox1 = new GroupBox();
            this.picKillDelete = new PictureBox();
            this.label6 = new Label();
            this.txtKillHotkey = new TextBox();
            this.label5 = new Label();
            this.txtShowHotkey = new TextBox();
            this.label4 = new Label();
            this.label2 = new Label();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.groupBox2 = new GroupBox();
            this.chkStartHidden = new CheckBox();
            this.chkCheckUpdates = new CheckBox();
            this.chkAutoStart = new CheckBox();
            this.groupBox3 = new GroupBox();
            this.label3 = new Label();
            this.chkHideMe = new CheckBox();
            this.chkRemember = new CheckBox();
            this.chkAutoSelect = new CheckBox();
            this.cmbCloseAction = new ComboBox();
            this.label1 = new Label();
            this.tabPage2 = new TabPage();
            this.label7 = new Label();
            this.chkWarnMe = new CheckBox();
            this.btnCancel = new Button();
            this.ttMain = new ToolTip(this.components);
            this.chkHideOnScreenSaver = new CheckBox();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.picKillDelete).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            base.SuspendLayout();
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x1aa, 0x14f);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.txtHideHotkey.AcceptsReturn = true;
            this.txtHideHotkey.AcceptsTab = true;
            this.txtHideHotkey.Location = new Point(0x34, 0x23);
            this.txtHideHotkey.Name = "txtHideHotkey";
            this.txtHideHotkey.ReadOnly = true;
            this.txtHideHotkey.Size = new Size(0x90, 20);
            this.txtHideHotkey.TabIndex = 2;
            this.txtHideHotkey.KeyDown += new KeyEventHandler(this.txtHideHotkey_KeyDown);
            this.groupBox1.Controls.Add(this.picKillDelete);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtKillHotkey);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtShowHotkey);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtHideHotkey);
            this.groupBox1.Location = new Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1d9, 0x60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Manage Hotkeys";
            this.picKillDelete.Cursor = Cursors.Hand;
            this.picKillDelete.Image = Resources.delete16x16;
            this.picKillDelete.Location = new Point(0x194, 0x25);
            this.picKillDelete.Name = "picKillDelete";
            this.picKillDelete.Size = new Size(0x10, 0x10);
            this.picKillDelete.SizeMode = PictureBoxSizeMode.AutoSize;
            this.picKillDelete.TabIndex = 7;
            this.picKillDelete.TabStop = false;
            this.ttMain.SetToolTip(this.picKillDelete, "Disable the kill hotkey");
            this.picKillDelete.Click += new EventHandler(this.picKillDelete_Click);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xd3, 0x26);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x17, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Kill:";
            this.txtKillHotkey.AcceptsReturn = true;
            this.txtKillHotkey.AcceptsTab = true;
            this.txtKillHotkey.Location = new Point(0xfe, 0x23);
            this.txtKillHotkey.Name = "txtKillHotkey";
            this.txtKillHotkey.ReadOnly = true;
            this.txtKillHotkey.Size = new Size(0x90, 20);
            this.txtKillHotkey.TabIndex = 4;
            this.txtKillHotkey.KeyDown += new KeyEventHandler(this.txtKillHotkey_KeyDown);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(9, 0x26);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x20, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Hide:";
            this.txtShowHotkey.AcceptsReturn = true;
            this.txtShowHotkey.AcceptsTab = true;
            this.txtShowHotkey.Location = new Point(0x34, 0x3d);
            this.txtShowHotkey.Name = "txtShowHotkey";
            this.txtShowHotkey.ReadOnly = true;
            this.txtShowHotkey.Size = new Size(0x90, 20);
            this.txtShowHotkey.TabIndex = 6;
            this.txtShowHotkey.KeyDown += new KeyEventHandler(this.txtShowHotkey_KeyDown);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(9, 0x40);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x25, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Show:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x95, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Click the box and press a key:";
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x1ed, 0x13d);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new Point(4, 0x16);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x1e5, 0x123);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.chkStartHidden);
            this.groupBox2.Controls.Add(this.chkCheckUpdates);
            this.groupBox2.Controls.Add(this.chkAutoStart);
            this.groupBox2.Location = new Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1d9, 0x63);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Startup";
            this.chkStartHidden.AutoSize = true;
            this.chkStartHidden.Location = new Point(6, 0x41);
            this.chkStartHidden.Name = "chkStartHidden";
            this.chkStartHidden.Size = new Size(0x75, 0x11);
            this.chkStartHidden.TabIndex = 2;
            this.chkStartHidden.Text = "Always start hidden";
            this.chkStartHidden.UseVisualStyleBackColor = true;
            this.chkCheckUpdates.AutoSize = true;
            this.chkCheckUpdates.Location = new Point(6, 0x2a);
            this.chkCheckUpdates.Name = "chkCheckUpdates";
            this.chkCheckUpdates.Size = new Size(0xe0, 0x11);
            this.chkCheckUpdates.TabIndex = 1;
            this.chkCheckUpdates.Text = "Check for updates when application starts";
            this.chkCheckUpdates.UseVisualStyleBackColor = true;
            this.chkAutoStart.AutoSize = true;
            this.chkAutoStart.Location = new Point(6, 0x13);
            this.chkAutoStart.Name = "chkAutoStart";
            this.chkAutoStart.Size = new Size(0xe2, 0x11);
            this.chkAutoStart.TabIndex = 0;
            this.chkAutoStart.Text = "Start NCS WinVisible when windows starts";
            this.chkAutoStart.UseVisualStyleBackColor = true;
            this.groupBox3.Controls.Add(this.chkHideOnScreenSaver);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.chkHideMe);
            this.groupBox3.Controls.Add(this.chkRemember);
            this.groupBox3.Controls.Add(this.chkAutoSelect);
            this.groupBox3.Controls.Add(this.cmbCloseAction);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new Point(6, 0x6f);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x1d9, 0xae);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Behavior";
            this.label3.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xef, 0x99);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xe4, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "(This only affects processes that are checked.)";
            this.chkHideMe.AutoSize = true;
            this.chkHideMe.Location = new Point(6, 0x41);
            this.chkHideMe.Name = "chkHideMe";
            this.chkHideMe.Size = new Size(0x12f, 0x11);
            this.chkHideMe.TabIndex = 2;
            this.chkHideMe.Text = "Hide application and tray icon when hide hotkey is pressed";
            this.chkHideMe.UseVisualStyleBackColor = true;
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new Point(6, 0x2a);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new Size(0xc5, 0x11);
            this.chkRemember.TabIndex = 1;
            this.chkRemember.Text = "Remember the processes I checked";
            this.chkRemember.UseVisualStyleBackColor = true;
            this.chkAutoSelect.AutoSize = true;
            this.chkAutoSelect.Location = new Point(6, 0x13);
            this.chkAutoSelect.Name = "chkAutoSelect";
            this.chkAutoSelect.Size = new Size(0xc3, 0x11);
            this.chkAutoSelect.TabIndex = 0;
            this.chkAutoSelect.Text = "Automatically check new processes";
            this.chkAutoSelect.UseVisualStyleBackColor = true;
            this.cmbCloseAction.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.cmbCloseAction.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCloseAction.FormattingEnabled = true;
            object[] items = new object[] { "become visible", "remain hidden (not recommended)", "be forced to close (not recommended)" };
            this.cmbCloseAction.Items.AddRange(items);
            this.cmbCloseAction.Location = new Point(0x103, 0x81);
            this.cmbCloseAction.Name = "cmbCloseAction";
            this.cmbCloseAction.Size = new Size(0xd0, 0x15);
            this.cmbCloseAction.TabIndex = 4;
            this.label1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x84);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xf6, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "When application closes, hidden processes should";
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new Point(4, 0x16);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x1e5, 0x123);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Hotkeys";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.label7.Location = new Point(6, 0x69);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1d9, 0x38);
            this.label7.TabIndex = 1;
            this.label7.Text = "Warning: Using the kill hotkey will instantly terminate the processes that are selected in the list. There is no confirmation and any unsaved work will be lost. Use with care.";
            this.chkWarnMe.AutoSize = true;
            this.chkWarnMe.Location = new Point(12, 0x14f);
            this.chkWarnMe.Name = "chkWarnMe";
            this.chkWarnMe.Size = new Size(0x11c, 0x11);
            this.chkWarnMe.TabIndex = 1;
            this.chkWarnMe.Text = "Warn me if the settings I choose are not recommended";
            this.chkWarnMe.UseVisualStyleBackColor = true;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x159, 0x14f);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.chkHideOnScreenSaver.AutoSize = true;
            this.chkHideOnScreenSaver.Location = new Point(6, 0x58);
            this.chkHideOnScreenSaver.Name = "chkHideOnScreenSaver";
            this.chkHideOnScreenSaver.Size = new Size(0x119, 0x11);
            this.chkHideOnScreenSaver.TabIndex = 6;
            this.chkHideOnScreenSaver.Text = "Hide selected processes when screen saver activates";
            this.chkHideOnScreenSaver.UseVisualStyleBackColor = true;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnOK;
            base.ClientSize = new Size(0x205, 370);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.chkWarnMe);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "frmOptions";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Options";
            base.Load += new EventHandler(this.frmOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((ISupportInitialize) this.picKillDelete).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private bool IsAutoRunEnabled()
        {
            try
            {
                return (Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).GetValue(this.GetEXEName()) != null);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void picKillDelete_Click(object sender, EventArgs e)
        {
            this._killKey = Keys.None;
            this.txtKillHotkey.Text = this._killKey.ToString();
        }

        private void SetAutoStart(bool value)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                string eXEName = this.GetEXEName();
                if (value)
                {
                    key.SetValue(eXEName, Application.ExecutablePath + " /invisible");
                }
                else
                {
                    try
                    {
                        key.DeleteValue(eXEName);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception exception1)
            {
                using (frmError error = new frmError(exception1))
                {
                    error.ShowDialog();
                }
            }
        }

        private void txtHideHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            this._hideKey = e.KeyCode | e.Modifiers;
            this.txtHideHotkey.Text = Hotkey.GetKeyComboString(e.KeyCode, e.Modifiers);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void txtKillHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            this._killKey = e.KeyCode | e.Modifiers;
            this.txtKillHotkey.Text = Hotkey.GetKeyComboString(e.KeyCode, e.Modifiers);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void txtShowHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            this._showKey = e.KeyCode | e.Modifiers;
            this.txtShowHotkey.Text = Hotkey.GetKeyComboString(e.KeyCode, e.Modifiers);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void WarnUserOnNonRecommended()
        {
            string text = null;
            if (this.cmbCloseAction.SelectedIndex == 1)
            {
                text = "The action you selected is not recommended. A Hidden process will not be able to become visible again until the process is manually ended.\nAre you sure?";
            }
            else if (this.cmbCloseAction.SelectedIndex == 2)
            {
                text = "The action you selected is not recommended. Forcing a process to close could cause you to lose any unsaved work.\nAre you sure?";
            }
            if ((text != null) && (MessageBox.Show(text, "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No))
            {
                base.DialogResult = DialogResult.None;
            }
        }
    }
}

