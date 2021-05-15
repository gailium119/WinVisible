namespace WinVisible
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using WinVisible.Properties;

    public class frmError : Form
    {
        private Exception _ex;
        private IContainer components;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbtnInspect;
        private ToolStripButton tsbtnCopyMsg;
        private Label label1;
        private RichTextBox rtbError;
        private PictureBox pictureBox1;
        private ToolStripButton tsbtnClose;
        private ToolStripContainer toolStripContainer1;

        public frmError(Exception ex)
        {
            this.InitializeComponent();
            this._ex = ex;
            if (this._ex.InnerException != null)
            {
                this.tsbtnInspect.Enabled = true;
            }
            this.rtbError.Text = this._ex.Message + "\n\n" + this._ex.StackTrace;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.toolStrip1 = new ToolStrip();
            this.tsbtnInspect = new ToolStripButton();
            this.tsbtnCopyMsg = new ToolStripButton();
            this.label1 = new Label();
            this.rtbError = new RichTextBox();
            this.pictureBox1 = new PictureBox();
            this.toolStripContainer1 = new ToolStripContainer();
            this.tsbtnClose = new ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            base.SuspendLayout();
            this.toolStrip1.BackColor = Color.Transparent;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = DockStyle.None;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsbtnInspect, this.tsbtnCopyMsg, this.tsbtnClose };
            this.toolStrip1.Items.AddRange(toolStripItems);
            this.toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = ToolStripRenderMode.System;
            this.toolStrip1.Size = new Size(0x1b1, 0x1f);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            this.tsbtnInspect.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbtnInspect.Enabled = false;
            this.tsbtnInspect.Image = Resources.search24x24;
            this.tsbtnInspect.ImageScaling = ToolStripItemImageScaling.None;
            this.tsbtnInspect.ImageTransparentColor = Color.Magenta;
            this.tsbtnInspect.Name = "tsbtnInspect";
            this.tsbtnInspect.Size = new Size(0x1c, 0x1c);
            this.tsbtnInspect.Text = "Inspect error message";
            this.tsbtnInspect.Click += new EventHandler(this.tsbtnInspect_Click);
            this.tsbtnCopyMsg.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbtnCopyMsg.Image = Resources.copy24x24;
            this.tsbtnCopyMsg.ImageScaling = ToolStripItemImageScaling.None;
            this.tsbtnCopyMsg.ImageTransparentColor = Color.Magenta;
            this.tsbtnCopyMsg.Name = "tsbtnCopyMsg";
            this.tsbtnCopyMsg.Size = new Size(0x1c, 0x1c);
            this.tsbtnCopyMsg.Text = "Copy message to the clipboard";
            this.tsbtnCopyMsg.Click += new EventHandler(this.tsbtnCopyMsg_Click);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0x42, 3);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4a, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Information:";
            this.rtbError.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.rtbError.BorderStyle = BorderStyle.None;
            this.rtbError.Cursor = Cursors.Default;
            this.rtbError.DetectUrls = false;
            this.rtbError.Location = new Point(0x42, 0x13);
            this.rtbError.Name = "rtbError";
            this.rtbError.ReadOnly = true;
            this.rtbError.Size = new Size(0x163, 0x89);
            this.rtbError.TabIndex = 6;
            this.rtbError.Text = "";
            this.pictureBox1.Image = Resources.error48x48;
            this.pictureBox1.Location = new Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x30, 0x30);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pictureBox1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.rtbError);
            this.toolStripContainer1.ContentPanel.Size = new Size(0x1b1, 0xa8);
            this.toolStripContainer1.Dock = DockStyle.Fill;
            this.toolStripContainer1.Location = new Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new Size(0x1b1, 0xc7);
            this.toolStripContainer1.TabIndex = 10;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.tsbtnClose.Alignment = ToolStripItemAlignment.Right;
            this.tsbtnClose.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tsbtnClose.ImageTransparentColor = Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new Size(0x25, 0x1c);
            this.tsbtnClose.Text = "Close";
            this.tsbtnClose.Click += new EventHandler(this.tsbtnClose_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b1, 0xc7);
            base.Controls.Add(this.toolStripContainer1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Name = "frmError";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Error";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tsbtnCopyMsg_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(this._ex.Message);
        }

        private void tsbtnInspect_Click(object sender, EventArgs e)
        {
            new frmError(this._ex.InnerException).ShowDialog();
        }
    }
}

