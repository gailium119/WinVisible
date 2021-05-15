namespace WinVisible
{
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Configuration;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
            this.btnOK = new System.Windows.Forms.Button();
            this.txtHideHotkey = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picKillDelete = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKillHotkey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtShowHotkey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkStartHidden = new System.Windows.Forms.CheckBox();
            this.chkCheckUpdates = new System.Windows.Forms.CheckBox();
            this.chkAutoStart = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkHideOnScreenSaver = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkHideMe = new System.Windows.Forms.CheckBox();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.chkAutoSelect = new System.Windows.Forms.CheckBox();
            this.cmbCloseAction = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.chkWarnMe = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKillDelete)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(497, 438);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 30);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtHideHotkey
            // 
            this.txtHideHotkey.AcceptsReturn = true;
            this.txtHideHotkey.AcceptsTab = true;
            this.txtHideHotkey.Location = new System.Drawing.Point(61, 45);
            this.txtHideHotkey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHideHotkey.Name = "txtHideHotkey";
            this.txtHideHotkey.ReadOnly = true;
            this.txtHideHotkey.Size = new System.Drawing.Size(167, 23);
            this.txtHideHotkey.TabIndex = 2;
            this.txtHideHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHideHotkey_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picKillDelete);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtKillHotkey);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtShowHotkey);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtHideHotkey);
            this.groupBox1.Location = new System.Drawing.Point(7, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(552, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "管理快捷键";
            // 
            // picKillDelete
            // 
            this.picKillDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picKillDelete.Image = global::WinVisible.Properties.Resources.delete16x16;
            this.picKillDelete.Location = new System.Drawing.Point(471, 48);
            this.picKillDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picKillDelete.Name = "picKillDelete";
            this.picKillDelete.Size = new System.Drawing.Size(16, 16);
            this.picKillDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picKillDelete.TabIndex = 7;
            this.picKillDelete.TabStop = false;
            this.ttMain.SetToolTip(this.picKillDelete, "禁用结束快捷键");
            this.picKillDelete.Click += new System.EventHandler(this.picKillDelete_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(246, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Kill:";
            // 
            // txtKillHotkey
            // 
            this.txtKillHotkey.AcceptsReturn = true;
            this.txtKillHotkey.AcceptsTab = true;
            this.txtKillHotkey.Location = new System.Drawing.Point(296, 45);
            this.txtKillHotkey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtKillHotkey.Name = "txtKillHotkey";
            this.txtKillHotkey.ReadOnly = true;
            this.txtKillHotkey.Size = new System.Drawing.Size(167, 23);
            this.txtKillHotkey.TabIndex = 4;
            this.txtKillHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKillHotkey_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "隐藏:";
            // 
            // txtShowHotkey
            // 
            this.txtShowHotkey.AcceptsReturn = true;
            this.txtShowHotkey.AcceptsTab = true;
            this.txtShowHotkey.Location = new System.Drawing.Point(61, 79);
            this.txtShowHotkey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtShowHotkey.Name = "txtShowHotkey";
            this.txtShowHotkey.ReadOnly = true;
            this.txtShowHotkey.Size = new System.Drawing.Size(167, 23);
            this.txtShowHotkey.TabIndex = 6;
            this.txtShowHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShowHotkey_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "显示:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "单击方框来按下一个键:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(14, 16);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(575, 415);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(567, 385);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = " 主要";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkStartHidden);
            this.groupBox2.Controls.Add(this.chkCheckUpdates);
            this.groupBox2.Controls.Add(this.chkAutoStart);
            this.groupBox2.Location = new System.Drawing.Point(7, 8);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(552, 129);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "启动";
            // 
            // chkStartHidden
            // 
            this.chkStartHidden.AutoSize = true;
            this.chkStartHidden.Location = new System.Drawing.Point(7, 85);
            this.chkStartHidden.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkStartHidden.Name = "chkStartHidden";
            this.chkStartHidden.Size = new System.Drawing.Size(123, 21);
            this.chkStartHidden.TabIndex = 2;
            this.chkStartHidden.Text = "永远在启动时隐藏";
            this.chkStartHidden.UseVisualStyleBackColor = true;
            // 
            // chkCheckUpdates
            // 
            this.chkCheckUpdates.AutoSize = true;
            this.chkCheckUpdates.Location = new System.Drawing.Point(7, 55);
            this.chkCheckUpdates.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkCheckUpdates.Name = "chkCheckUpdates";
            this.chkCheckUpdates.Size = new System.Drawing.Size(147, 21);
            this.chkCheckUpdates.TabIndex = 1;
            this.chkCheckUpdates.Text = "在程序启动时检查更新";
            this.chkCheckUpdates.UseVisualStyleBackColor = true;
            // 
            // chkAutoStart
            // 
            this.chkAutoStart.AutoSize = true;
            this.chkAutoStart.Location = new System.Drawing.Point(7, 25);
            this.chkAutoStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAutoStart.Name = "chkAutoStart";
            this.chkAutoStart.Size = new System.Drawing.Size(75, 21);
            this.chkAutoStart.TabIndex = 0;
            this.chkAutoStart.Text = "开机启动";
            this.chkAutoStart.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkHideOnScreenSaver);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.chkHideMe);
            this.groupBox3.Controls.Add(this.chkRemember);
            this.groupBox3.Controls.Add(this.chkAutoSelect);
            this.groupBox3.Controls.Add(this.cmbCloseAction);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(7, 144);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(552, 228);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "行为";
            // 
            // chkHideOnScreenSaver
            // 
            this.chkHideOnScreenSaver.AutoSize = true;
            this.chkHideOnScreenSaver.Location = new System.Drawing.Point(7, 115);
            this.chkHideOnScreenSaver.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkHideOnScreenSaver.Name = "chkHideOnScreenSaver";
            this.chkHideOnScreenSaver.Size = new System.Drawing.Size(195, 21);
            this.chkHideOnScreenSaver.TabIndex = 6;
            this.chkHideOnScreenSaver.Text = "当屏幕保护开启时隐藏所选进程";
            this.chkHideOnScreenSaver.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "(这只影响选中的进程。)";
            // 
            // chkHideMe
            // 
            this.chkHideMe.AutoSize = true;
            this.chkHideMe.Location = new System.Drawing.Point(7, 85);
            this.chkHideMe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkHideMe.Name = "chkHideMe";
            this.chkHideMe.Size = new System.Drawing.Size(243, 21);
            this.chkHideMe.TabIndex = 2;
            this.chkHideMe.Text = "当按下隐藏快捷键时隐藏应用和托盘图标";
            this.chkHideMe.UseVisualStyleBackColor = true;
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Location = new System.Drawing.Point(7, 55);
            this.chkRemember.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(99, 21);
            this.chkRemember.TabIndex = 1;
            this.chkRemember.Text = "记住我的选择";
            this.chkRemember.UseVisualStyleBackColor = true;
            // 
            // chkAutoSelect
            // 
            this.chkAutoSelect.AutoSize = true;
            this.chkAutoSelect.Location = new System.Drawing.Point(7, 25);
            this.chkAutoSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAutoSelect.Name = "chkAutoSelect";
            this.chkAutoSelect.Size = new System.Drawing.Size(111, 21);
            this.chkAutoSelect.TabIndex = 0;
            this.chkAutoSelect.Text = "自动检查新进程";
            this.chkAutoSelect.UseVisualStyleBackColor = true;
            // 
            // cmbCloseAction
            // 
            this.cmbCloseAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCloseAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCloseAction.FormattingEnabled = true;
            this.cmbCloseAction.Items.AddRange(new object[] {
            "重新显示",
            "保持隐藏 (不推荐)",
            "强行关闭 (不推荐)"});
            this.cmbCloseAction.Location = new System.Drawing.Point(212, 169);
            this.cmbCloseAction.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbCloseAction.Name = "cmbCloseAction";
            this.cmbCloseAction.Size = new System.Drawing.Size(236, 25);
            this.cmbCloseAction.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "当WinVisible关闭时，选中的进程会";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(567, 385);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "快捷键";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(552, 74);
            this.label7.TabIndex = 1;
            this.label7.Text = "警告：使用强行关闭快捷键会直接杀死进程。这没有确认而且所有为保存的工作会丢失。";
            // 
            // chkWarnMe
            // 
            this.chkWarnMe.AutoSize = true;
            this.chkWarnMe.Location = new System.Drawing.Point(14, 438);
            this.chkWarnMe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkWarnMe.Name = "chkWarnMe";
            this.chkWarnMe.Size = new System.Drawing.Size(183, 21);
            this.chkWarnMe.TabIndex = 1;
            this.chkWarnMe.Text = "当我选择不推荐设置时警告我";
            this.chkWarnMe.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(402, 438);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(603, 484);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkWarnMe);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选项";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKillDelete)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
                text = "你选择的选项不推荐。你确定吗？";
            }
            else if (this.cmbCloseAction.SelectedIndex == 2)
            {
                text = "你选择的选项不推荐。你确定吗？";
            }
            if ((text != null) && (MessageBox.Show(text, "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No))
            {
                base.DialogResult = DialogResult.None;
            }
        }
    }
}

