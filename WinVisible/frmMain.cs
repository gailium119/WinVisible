namespace WinVisible
{
    using NCS;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using WinVisible.NCSUpdateService;
    using WinVisible.Properties;

    public class frmMain : Form
    {
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_SCREENSAVE = 0xf140;
        private const int SPI_GETSCREENSAVERRUNNING = 0x72;
        private IContainer components;
        private ListView lvwProcess;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ToolStripContainer toolStripContainer1;
        private ToolStrip tsMain;
        private ToolStripButton tsbtnHide;
        private ToolStripButton tsbtnShow;
        private ToolStripSeparator toolStripSeparator1;
        private NotifyIcon iconTray;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private StatusStrip statusStrip1;
        private ToolStripButton tsbtnKill;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton tsbtnUpdate;
        private ToolStripButton tsbtnOptions;
        private ToolStripButton tsbtnAbout;
        private ContextMenuStrip cmsTray;
        private ToolStripMenuItem cmsShow;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem cmsOptions;
        private ToolStripMenuItem cmsExit;
        private ToolStripMenuItem cmsAbout;
        private ContextMenuStrip cmsItem;
        private ToolStripMenuItem cmsHideProc;
        private ToolStripMenuItem cmsShowProc;
        private ToolStripStatusLabel tslblStatus;
        private ToolStripStatusLabel tslnkError;
        private ToolStripMenuItem cmsCheckUpdates;
        private BackgroundWorker processWorker;

        public frmMain()
        {
            if (Settings.Default.ApplicationVersion != Application.ProductVersion)
            {
                Settings.Default.Upgrade();
                Settings.Default.ApplicationVersion = Application.ProductVersion;
            }
            try
            {
                Settings.Default.RememberList ??= new StringCollection();
                HotkeyManager.Control = this;
                HotkeyManager.HideHotkeyAddedBase += new HotkeyManager.HotkeyAddedHandler(this.HotkeyManager_HideHotkeyAdded);
                HotkeyManager.ShowHotkeyAddedBase += new HotkeyManager.HotkeyAddedHandler(this.HotkeyManager_ShowHotkeyAdded);
                HotkeyManager.KillHotkeyAddedBase += new HotkeyManager.HotkeyAddedHandler(this.HotkeyManager_KillHotkeyAdded);
                Hotkey hotkey = new Hotkey(Settings.Default.HideHotkey, HotkeyManager.Control);
                Hotkey hotkey2 = new Hotkey(Settings.Default.ShowHotkey, HotkeyManager.Control);
                Hotkey hotkey3 = new Hotkey(Settings.Default.KillHotKey, HotkeyManager.Control);
                hotkey.Register();
                hotkey2.Register();
                hotkey3.Register();
                HotkeyManager.HideHotkey = hotkey;
                HotkeyManager.ShowHotkey = hotkey2;
                HotkeyManager.KillHotkey = hotkey3;
                WinVisible.ProcessManager.ProcessItemAddedBase += new EventHandler<ProcessManagerEventArgs>(this.ProcessManager_ProcessItemAdded);
                WinVisible.ProcessManager.ProcessItemRemovedBase += new EventHandler<ProcessManagerEventArgs>(this.ProcessManager_ProcessItemRemoved);
                WinVisible.ProcessManager.ProcessItemUpdateBase += new EventHandler<ProcessManagerEventArgs>(this.ProcessManager_ProcessItemUpdate);
            }
            catch (Exception exception)
            {
                this.ShowError(exception, false);
                Environment.Exit(-1);
            }
            this.InitializeComponent();
            this.ProcessArgs();
        }

        private void AddProcessItemToView(ProcessItem item)
        {
            ListViewItem item2 = new ListViewItem {
                Text = item.Process.ProcessName,
                SubItems = { 
                    item.Process.MainWindowTitle,
                    item.Process.Id.ToString(),
                    item.Visible ? "可见" : "不可见"
                },
                Tag = item,
                Group = this.lvwProcess.Groups["Windowed"]
            };
            if (Settings.Default.RememberProcesses && this.ProcessRemembered(item.Process))
            {
                item2.Checked = true;
            }
            if (Settings.Default.AutoSelect)
            {
                item2.Checked = true;
            }
            this.lvwProcess.Items.Add(item2);
        }

        private void CheckForUpdates(bool suppressNonUpdate = false)
        {
            try
            {
                UpdateService service = new UpdateService(Application.ProductVersion);
                service.CheckForUpdateAsync();
                this.tslblStatus.Text = "正在检查更新...";
                while (true)
                {
                    if (service.AsyncState != UpdateService.AsyncStates.Working)
                    {
                        if ((service.Exceptions.Count > 0) && (service.Exceptions.Peek() != null))
                        {
                            throw service.Exceptions.Pop();
                        }
                        switch (service.NewVersionInfo.Status)
                        {
                            case UpdateResult.CurrentVersion:
                                if (!suppressNonUpdate)
                                {
                                    MessageBox.Show("你的WinVisible是最新版本！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                }
                                break;

                            case UpdateResult.NewerAvailable:
                                if (MessageBox.Show("发现新版本。是否下载下载？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    Services.NavigateToUrl(service.NewVersionInfo.DownloadUrl);
                                }
                                break;

                            default:
                                break;
                        }
                        break;
                    }
                    Application.DoEvents();
                }
            }
            catch (Exception exception)
            {
                if (!suppressNonUpdate)
                {
                    this.ShowError(exception, false);
                }
            }
            this.tslblStatus.Text = "已就位";
        }

        private void cmsAbout_Click(object sender, EventArgs e)
        {
            using (frmAbout about = new frmAbout())
            {
                about.ShowDialog(this);
            }
        }

        private void cmsCheckUpdates_Click(object sender, EventArgs e)
        {
            this.CheckForUpdates(false);
        }

        private void cmsExit_Click(object sender, EventArgs e)
        {
            if (this.ExitApplication())
            {
                Application.Exit();
            }
        }

        private void cmsHideProc_Click(object sender, EventArgs e)
        {
            this.ShowProcess(false);
        }

        private void cmsOptions_Click(object sender, EventArgs e)
        {
            using (frmOptions options = new frmOptions())
            {
                options.ShowDialog(this);
            }
        }

        private void cmsShow_Click(object sender, EventArgs e)
        {
            base.Visible = true;
        }

        private void cmsShowProc_Click(object sender, EventArgs e)
        {
            this.ShowProcess(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExitApplication()
        {
            if (MessageBox.Show("如果你关闭WinVisible，WinVisible就不能监控快捷键。是否要继续？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }
            HotkeyManager.HideHotkey.Unregister();
            HotkeyManager.ShowHotkey.Unregister();
            HotkeyManager.KillHotkey.Unregister();
            switch (Settings.Default.AppCloseAction)
            {
                case 0:
                    this.ShowProcesses(true);
                    break;

                case 2:
                    this.KillProcesses(true);
                    break;

                default:
                    break;
            }
            return true;
        }

        private ListViewItem FindProcessInView(ProcessItem processItem)
        {
            ListViewItem item3;
            IEnumerator enumerator = this.lvwProcess.Items.GetEnumerator();
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        ListViewItem current = (ListViewItem) enumerator.Current;
                        ProcessItem tag = (ProcessItem) current.Tag;
                        if (tag.Process.Id != processItem.Process.Id)
                        {
                            continue;
                        }
                        item3 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return item3;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                base.Visible = false;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.processWorker.RunWorkerAsync();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (Settings.Default.StartHidden)
            {
                base.Visible = false;
            }
            else
            {
                base.WindowState = FormWindowState.Normal;
            }
            if (Settings.Default.CheckUpdates)
            {
                this.CheckForUpdates(true);
            }
        }

        private void hideHotkey_Pressed(object sender, HandledEventArgs e)
        {
            this.ShowProcesses(false);
            if (Settings.Default.HideIcon)
            {
                base.Visible = false;
                this.iconTray.Visible = false;
            }
        }

        private void HotkeyManager_HideHotkeyAdded(Hotkey hotkey)
        {
            if (hotkey != null)
            {
                hotkey.PressedBase += new HandledEventHandler(this.hideHotkey_Pressed);
            }
        }

        private void HotkeyManager_KillHotkeyAdded(Hotkey hotkey)
        {
            if (hotkey != null)
            {
                hotkey.PressedBase += new HandledEventHandler(this.killHotkey_Pressed);
            }
        }

        private void HotkeyManager_ShowHotkeyAdded(Hotkey hotkey)
        {
            if (hotkey != null)
            {
                hotkey.PressedBase += new HandledEventHandler(this.showHotkey_Pressed);
            }
        }

        private void iconTray_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                base.Visible = true;
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("有窗口的进程", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Unwindowed Processes", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lvwProcess = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsHideProc = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsShowProc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslnkError = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbtnHide = new System.Windows.Forms.ToolStripButton();
            this.tsbtnShow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnKill = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAbout = new System.Windows.Forms.ToolStripButton();
            this.iconTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsShow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsExit = new System.Windows.Forms.ToolStripMenuItem();
            this.processWorker = new System.ComponentModel.BackgroundWorker();
            this.cmsItem.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.cmsTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwProcess
            // 
            this.lvwProcess.CheckBoxes = true;
            this.lvwProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvwProcess.ContextMenuStrip = this.cmsItem;
            this.lvwProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwProcess.FullRowSelect = true;
            this.lvwProcess.GridLines = true;
            listViewGroup1.Header = "有窗口的进程";
            listViewGroup1.Name = "Windowed";
            listViewGroup2.Header = "Unwindowed Processes";
            listViewGroup2.Name = "没有窗口的进程";
            this.lvwProcess.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.lvwProcess.HideSelection = false;
            this.lvwProcess.Location = new System.Drawing.Point(0, 0);
            this.lvwProcess.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lvwProcess.MultiSelect = false;
            this.lvwProcess.Name = "lvwProcess";
            this.lvwProcess.Size = new System.Drawing.Size(860, 480);
            this.lvwProcess.TabIndex = 0;
            this.lvwProcess.UseCompatibleStateImageBehavior = false;
            this.lvwProcess.View = System.Windows.Forms.View.Details;
            this.lvwProcess.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwProcess_ItemChecked);
            this.lvwProcess.SelectedIndexChanged += new System.EventHandler(this.lvwProcess_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "进程名称";
            this.columnHeader1.Width = 131;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "标题";
            this.columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ID";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "状态";
            this.columnHeader4.Width = 90;
            // 
            // cmsItem
            // 
            this.cmsItem.BackColor = System.Drawing.Color.White;
            this.cmsItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsHideProc,
            this.cmsShowProc});
            this.cmsItem.Name = "cmsItem";
            this.cmsItem.Size = new System.Drawing.Size(101, 48);
            // 
            // cmsHideProc
            // 
            this.cmsHideProc.Name = "cmsHideProc";
            this.cmsHideProc.Size = new System.Drawing.Size(100, 22);
            this.cmsHideProc.Text = "隐藏";
            this.cmsHideProc.Click += new System.EventHandler(this.cmsHideProc_Click);
            // 
            // cmsShowProc
            // 
            this.cmsShowProc.Name = "cmsShowProc";
            this.cmsShowProc.Size = new System.Drawing.Size(100, 22);
            this.cmsShowProc.Text = "显示";
            this.cmsShowProc.Click += new System.EventHandler(this.cmsShowProc_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.lvwProcess);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(860, 480);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(860, 541);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsMain);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblStatus,
            this.tslnkError});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(860, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // tslblStatus
            // 
            this.tslblStatus.Name = "tslblStatus";
            this.tslblStatus.Size = new System.Drawing.Size(44, 17);
            this.tslblStatus.Text = "已就位";
            // 
            // tslnkError
            // 
            this.tslnkError.Image = global::WinVisible.Properties.Resources.delete16x16;
            this.tslnkError.IsLink = true;
            this.tslnkError.Name = "tslnkError";
            this.tslnkError.Size = new System.Drawing.Size(204, 17);
            this.tslnkError.Text = "发生了一个错误。单击查看详情。";
            this.tslnkError.Visible = false;
            this.tslnkError.Click += new System.EventHandler(this.tslnkError_Click);
            // 
            // tsMain
            // 
            this.tsMain.BackColor = System.Drawing.Color.White;
            this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnHide,
            this.tsbtnShow,
            this.toolStripSeparator1,
            this.tsbtnKill,
            this.toolStripSeparator3,
            this.tsbtnOptions,
            this.toolStripSeparator4,
            this.tsbtnUpdate,
            this.tsbtnAbout});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(860, 39);
            this.tsMain.Stretch = true;
            this.tsMain.TabIndex = 1;
            this.tsMain.Text = "Options";
            // 
            // tsbtnHide
            // 
            this.tsbtnHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnHide.Enabled = false;
            this.tsbtnHide.Image = global::WinVisible.Properties.Resources.hide32x32;
            this.tsbtnHide.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnHide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnHide.Name = "tsbtnHide";
            this.tsbtnHide.Size = new System.Drawing.Size(36, 36);
            this.tsbtnHide.Text = "隐藏选中窗口";
            this.tsbtnHide.Click += new System.EventHandler(this.tsbtnHide_Click);
            // 
            // tsbtnShow
            // 
            this.tsbtnShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnShow.Enabled = false;
            this.tsbtnShow.Image = global::WinVisible.Properties.Resources.show32x32;
            this.tsbtnShow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnShow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnShow.Name = "tsbtnShow";
            this.tsbtnShow.Size = new System.Drawing.Size(36, 36);
            this.tsbtnShow.Text = "显示选中窗口";
            this.tsbtnShow.Click += new System.EventHandler(this.tsbtnShow_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbtnKill
            // 
            this.tsbtnKill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnKill.Enabled = false;
            this.tsbtnKill.Image = global::WinVisible.Properties.Resources.kill32x32;
            this.tsbtnKill.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnKill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnKill.Name = "tsbtnKill";
            this.tsbtnKill.Size = new System.Drawing.Size(36, 36);
            this.tsbtnKill.Text = "结束选中进程";
            this.tsbtnKill.Click += new System.EventHandler(this.tsbtnKill_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbtnOptions
            // 
            this.tsbtnOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOptions.Image = global::WinVisible.Properties.Resources.options32x32;
            this.tsbtnOptions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOptions.Name = "tsbtnOptions";
            this.tsbtnOptions.Size = new System.Drawing.Size(36, 36);
            this.tsbtnOptions.Text = "设置";
            this.tsbtnOptions.Click += new System.EventHandler(this.tsbtnOptions_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbtnUpdate
            // 
            this.tsbtnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnUpdate.Image = global::WinVisible.Properties.Resources.internet32x321;
            this.tsbtnUpdate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUpdate.Name = "tsbtnUpdate";
            this.tsbtnUpdate.Size = new System.Drawing.Size(36, 36);
            this.tsbtnUpdate.Text = "检查更新";
            this.tsbtnUpdate.Click += new System.EventHandler(this.tsbtnUpdate_Click);
            // 
            // tsbtnAbout
            // 
            this.tsbtnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAbout.Image = global::WinVisible.Properties.Resources.about32x32;
            this.tsbtnAbout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAbout.Name = "tsbtnAbout";
            this.tsbtnAbout.Size = new System.Drawing.Size(36, 36);
            this.tsbtnAbout.Text = "关于";
            this.tsbtnAbout.Click += new System.EventHandler(this.tsbtnAbout_Click);
            // 
            // iconTray
            // 
            this.iconTray.ContextMenuStrip = this.cmsTray;
            this.iconTray.Icon = ((System.Drawing.Icon)(resources.GetObject("iconTray.Icon")));
            this.iconTray.Text = "WinVisible";
            this.iconTray.Visible = true;
            this.iconTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.iconTray_MouseClick);
            // 
            // cmsTray
            // 
            this.cmsTray.BackColor = System.Drawing.Color.White;
            this.cmsTray.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsShow,
            this.cmsAbout,
            this.cmsCheckUpdates,
            this.toolStripSeparator5,
            this.cmsOptions,
            this.cmsExit});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsTray.Size = new System.Drawing.Size(134, 120);
            // 
            // cmsShow
            // 
            this.cmsShow.BackColor = System.Drawing.Color.White;
            this.cmsShow.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsShow.Name = "cmsShow";
            this.cmsShow.Size = new System.Drawing.Size(133, 22);
            this.cmsShow.Text = "WinVisible";
            this.cmsShow.Click += new System.EventHandler(this.cmsShow_Click);
            // 
            // cmsAbout
            // 
            this.cmsAbout.Name = "cmsAbout";
            this.cmsAbout.Size = new System.Drawing.Size(133, 22);
            this.cmsAbout.Text = "关于...";
            this.cmsAbout.Click += new System.EventHandler(this.cmsAbout_Click);
            // 
            // cmsCheckUpdates
            // 
            this.cmsCheckUpdates.Name = "cmsCheckUpdates";
            this.cmsCheckUpdates.Size = new System.Drawing.Size(133, 22);
            this.cmsCheckUpdates.Text = "检查更新";
            this.cmsCheckUpdates.Click += new System.EventHandler(this.cmsCheckUpdates_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(130, 6);
            // 
            // cmsOptions
            // 
            this.cmsOptions.Name = "cmsOptions";
            this.cmsOptions.Size = new System.Drawing.Size(133, 22);
            this.cmsOptions.Text = "选项...";
            this.cmsOptions.Click += new System.EventHandler(this.cmsOptions_Click);
            // 
            // cmsExit
            // 
            this.cmsExit.Name = "cmsExit";
            this.cmsExit.Size = new System.Drawing.Size(133, 22);
            this.cmsExit.Text = "退出";
            this.cmsExit.Click += new System.EventHandler(this.cmsExit_Click);
            // 
            // processWorker
            // 
            this.processWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.processWorker_DoWork);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(860, 541);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.Text = "WinVisible";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.cmsItem.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.cmsTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private bool IsScreenSaverActive()
        {
            int retval = 0;
            SystemParametersInfo(0x72, 0, ref retval, 0);
            return (retval == 1);
        }

        private void killHotkey_Pressed(object sender, HandledEventArgs e)
        {
            this.KillProcesses(false);
        }

        private void KillProcess()
        {
            try
            {
                if (this.lvwProcess.SelectedItems.Count != 0)
                {
                    ((ProcessItem) this.lvwProcess.SelectedItems[0].Tag).Process.Kill();
                }
            }
            catch (Exception exception)
            {
                this.ShowError(exception, false);
            }
        }

        private void KillProcesses(bool onlyHidden)
        {
            foreach (ListViewItem item in this.lvwProcess.CheckedItems)
            {
                try
                {
                    ProcessItem tag = (ProcessItem) item.Tag;
                    if ((!tag.Visible && onlyHidden) || !onlyHidden)
                    {
                        tag.Process.Kill();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void lvwProcess_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (Settings.Default.RememberProcesses)
            {
                string processName = ((ProcessItem) e.Item.Tag).Process.ProcessName;
                if (e.Item.Checked)
                {
                    Settings.Default.RememberList.Add(processName);
                }
                else
                {
                    Settings.Default.RememberList.Remove(processName);
                }
                Settings.Default.Save();
            }
        }

        private void lvwProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.lvwProcess.SelectedIndices.Count;
            this.tsbtnHide.Enabled = count > 0;
            this.tsbtnShow.Enabled = count > 0;
            this.tsbtnKill.Enabled = count > 0;
        }

        private void ProcessArgs()
        {
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            for (int i = 0; i < commandLineArgs.Length; i++)
            {
                string text1 = commandLineArgs[i];
            }
        }

        private void ProcessManager_ProcessItemAdded(object sender, ProcessManagerEventArgs e)
        {
            base.Invoke(new MethodInvoker(() => this.AddProcessItemToView(e.Item)));
        }

        private void ProcessManager_ProcessItemRemoved(object sender, ProcessManagerEventArgs e)
        {
            base.Invoke(new MethodInvoker(() => this.RemoveProcessItemFromView(e.Item)));
        }

        private void ProcessManager_ProcessItemUpdate(object sender, ProcessManagerEventArgs e)
        {
            base.Invoke(new MethodInvoker(() => this.UpdateProcessItemInView(e.Item)));
        }

        private bool ProcessRemembered(Process process) => 
            Settings.Default.RememberList.Contains(process.ProcessName);

        private void processWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Action method = null;
            while (!e.Cancel)
            {
                WinVisible.ProcessManager.RefreshProcessCollection();
                if (Settings.Default.HideWhenScreenSaverActivates && this.IsScreenSaverActive())
                {
                    if (method == null)
                    {
                        method = () => this.ShowProcesses(false);
                    }
                    this.Invoke(method);
                }
                Thread.Sleep(0xbb8);
            }
        }

        private void RemoveProcessItemFromView(ProcessItem item)
        {
            ListViewItem item2 = this.FindProcessInView(item);
            if (item2 != null)
            {
                this.lvwProcess.Items.Remove(item2);
            }
        }

        private void ShowError(Exception ex, bool silent = false)
        {
            if (silent)
            {
                this.tslnkError.Visible = true;
                this.tslnkError.Tag = ex;
            }
            else
            {
                using (frmError error = new frmError(ex))
                {
                    error.ShowDialog();
                }
            }
        }

        private void showHotkey_Pressed(object sender, HandledEventArgs e)
        {
            this.ShowProcesses(true);
            if (Settings.Default.HideIcon)
            {
                this.iconTray.Visible = true;
            }
        }

        private void ShowProcess(bool visible)
        {
            try
            {
                if (this.lvwProcess.SelectedItems.Count != 0)
                {
                    ProcessItem tag = (ProcessItem) this.lvwProcess.SelectedItems[0].Tag;
                    tag.Visible = visible;
                    this.UpdateProcessItemInView(tag);
                }
            }
            catch (Exception exception)
            {
                this.ShowError(exception, false);
            }
        }

        private void ShowProcesses(bool visible)
        {
            foreach (ListViewItem item in this.lvwProcess.CheckedItems)
            {
                try
                {
                    ProcessItem tag = (ProcessItem) item.Tag;
                    tag.Visible = visible;
                    this.UpdateProcessItemInView(tag);
                }
                catch (Exception exception1)
                {
                    Console.WriteLine(exception1.Message);
                }
            }
        }

        [DllImport("user32.dll", SetLastError=true)]
        private static extern bool SystemParametersInfo(int action, int param, ref int retval, int updini);
        private void tsbtnAbout_Click(object sender, EventArgs e)
        {
            using (frmAbout about = new frmAbout())
            {
                about.ShowDialog();
            }
        }

        private void tsbtnHide_Click(object sender, EventArgs e)
        {
            this.ShowProcess(false);
        }

        private void tsbtnKill_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要结束进程吗？\n未保存的工作将丢失。", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.KillProcess();
            }
        }

        private void tsbtnOptions_Click(object sender, EventArgs e)
        {
            using (frmOptions options = new frmOptions())
            {
                options.ShowDialog();
            }
        }

        private void tsbtnSendDonation_Click(object sender, EventArgs e)
        {
            Services.SendDonation();
        }

        private void tsbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowProcess(true);
        }

        private void tsbtnUpdate_Click(object sender, EventArgs e)
        {
            this.CheckForUpdates(false);
        }

        private void tslnkError_Click(object sender, EventArgs e)
        {
            this.ShowError((Exception) this.tslnkError.Tag, false);
            this.tslnkError.Tag = null;
            this.tslnkError.Visible = false;
        }

        private void UpdateProcessItemInView(ProcessItem item)
        {
            ListViewItem item2 = this.FindProcessInView(item);
            if (item2 != null)
            {
                item2.Text = item.Process.ProcessName;
                item2.SubItems[1].Text = item.Process.MainWindowTitle;
                item2.SubItems[2].Text = item.Process.Id.ToString();
                item2.SubItems[3].Text = item.Visible ? "可见" : "不可见";
            }
        }

        public delegate void Action();

        private delegate void ProcessItemEventSafeDelegate(ProcessItem item);
    }
}

