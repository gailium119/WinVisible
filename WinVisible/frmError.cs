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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmError));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbError = new System.Windows.Forms.RichTextBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tsbtnInspect = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCopyMsg = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnInspect,
            this.tsbtnCopyMsg,
            this.tsbtnClose});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(446, 31);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(36, 28);
            this.tsbtnClose.Text = "关闭";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "信息:";
            // 
            // rtbError
            // 
            this.rtbError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbError.BackColor = System.Drawing.Color.White;
            this.rtbError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbError.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbError.DetectUrls = false;
            this.rtbError.Location = new System.Drawing.Point(77, 25);
            this.rtbError.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rtbError.Name = "rtbError";
            this.rtbError.ReadOnly = true;
            this.rtbError.Size = new System.Drawing.Size(355, 133);
            this.rtbError.TabIndex = 6;
            this.rtbError.Text = "";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pictureBox1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.rtbError);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(446, 174);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(446, 205);
            this.toolStripContainer1.TabIndex = 10;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WinVisible.Properties.Resources.error48x48;
            this.pictureBox1.Location = new System.Drawing.Point(14, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // tsbtnInspect
            // 
            this.tsbtnInspect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnInspect.Enabled = false;
            this.tsbtnInspect.Image = global::WinVisible.Properties.Resources.search24x24;
            this.tsbtnInspect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnInspect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnInspect.Name = "tsbtnInspect";
            this.tsbtnInspect.Size = new System.Drawing.Size(28, 28);
            this.tsbtnInspect.Text = "错误信息";
            this.tsbtnInspect.Click += new System.EventHandler(this.tsbtnInspect_Click);
            // 
            // tsbtnCopyMsg
            // 
            this.tsbtnCopyMsg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCopyMsg.Image = global::WinVisible.Properties.Resources.copy24x24;
            this.tsbtnCopyMsg.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnCopyMsg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCopyMsg.Name = "tsbtnCopyMsg";
            this.tsbtnCopyMsg.Size = new System.Drawing.Size(28, 28);
            this.tsbtnCopyMsg.Text = "复制信息到剪切板";
            this.tsbtnCopyMsg.Click += new System.EventHandler(this.tsbtnCopyMsg_Click);
            // 
            // frmError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(446, 205);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmError";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "错误";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

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

