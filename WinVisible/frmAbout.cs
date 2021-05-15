namespace WinVisible
{
    using NCS;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using WinVisible.Properties;

    public class frmAbout : Form
    {
        private IContainer components;
        private Label lblName;
        private Button btnWeb;
        private Button btnDonate;
        private Label lblVersion;
        private Label lblCompany;
        private Label label1;
        private PictureBox pictureBox1;

        public frmAbout()
        {
            this.InitializeComponent();
            this.lblName.Text = Application.ProductName;
            this.lblVersion.Text = "Version: " + Application.ProductVersion;
            this.lblCompany.Text = Application.CompanyName;
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            Services.SendDonation();
        }

        private void btnWeb_Click(object sender, EventArgs e)
        {
            Services.VisitWebsite();
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
            this.lblName = new Label();
            this.lblVersion = new Label();
            this.lblCompany = new Label();
            this.label1 = new Label();
            this.pictureBox1 = new PictureBox();
            this.btnDonate = new Button();
            this.btnWeb = new Button();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.lblName.AutoSize = true;
            this.lblName.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblName.Location = new Point(0x42, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x4c, 0x18);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "[NAME]";
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new Point(0x43, 0x21);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new Size(0x3d, 13);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "[VERSION]";
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new Point(0xaf, 0x2f);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new Size(0x42, 13);
            this.lblCompany.TabIndex = 4;
            this.lblCompany.Text = "[COMPANY]";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x42, 0x2e);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Software written by: ";
            this.pictureBox1.Image = Resources.icon;
            this.pictureBox1.Location = new Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x30, 0x30);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.btnDonate.Image = Resources.donate32x32;
            this.btnDonate.ImageAlign = ContentAlignment.TopCenter;
            this.btnDonate.Location = new Point(0xd3, 0x86);
            this.btnDonate.Name = "btnDonate";
            this.btnDonate.Size = new Size(0x4b, 0x47);
            this.btnDonate.TabIndex = 2;
            this.btnDonate.Text = "Send Donation";
            this.btnDonate.TextAlign = ContentAlignment.BottomCenter;
            this.btnDonate.UseVisualStyleBackColor = true;
            this.btnDonate.Click += new EventHandler(this.btnDonate_Click);
            this.btnWeb.Image = Resources.web32x32;
            this.btnWeb.ImageAlign = ContentAlignment.TopCenter;
            this.btnWeb.Location = new Point(0x124, 0x86);
            this.btnWeb.Name = "btnWeb";
            this.btnWeb.Size = new Size(0x4b, 0x47);
            this.btnWeb.TabIndex = 1;
            this.btnWeb.Text = "Visit Website";
            this.btnWeb.TextAlign = ContentAlignment.BottomCenter;
            this.btnWeb.UseVisualStyleBackColor = true;
            this.btnWeb.Click += new EventHandler(this.btnWeb_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.ClientSize = new Size(0x17b, 0xd9);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lblCompany);
            base.Controls.Add(this.lblVersion);
            base.Controls.Add(this.btnDonate);
            base.Controls.Add(this.btnWeb);
            base.Controls.Add(this.lblName);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Name = "frmAbout";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "About NCS WinVisible";
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

