using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MDC.UI.Controls
{
	public class AboutCtrl : UserControl
	{
		private const string COPYRIGHT_TEXT = "100% adware && malware free.\nMetadataConsulting.ca, {0}";

		private int copyrightSince = DateTime.Now.Year;

		private bool beta;

		private IContainer components;

		private Label lblRights;

		private Label lblCopyright;

		private Label lblVersion;

		private Label lblAppName;

		private PictureBox pictureBox1;

		private Label labelDescription;

		private PictureBox pictureBox2;

		private Label label1;

		private LinkLabel linkLabelPro;

		public int CopyrightSince
		{
			get
			{
				return copyrightSince;
			}
			set
			{
				copyrightSince = value;
				lblCopyright.Text = GetCopyright();
			}
		}

		public bool Beta
		{
			get
			{
				return beta;
			}
			set
			{
				beta = value;
				lblVersion.Text = GetAppVersion();
			}
		}

		public AboutCtrl()
		{
			InitializeComponent();
			UpdateInfo();
		}

		private void UpdateInfo()
		{
			lblAppName.Text = GetAppName();
			lblVersion.Text = GetAppVersion();
			lblCopyright.Text = GetCopyright();
		}

		private string GetCopyright()
		{
			return string.Format("100% adware && malware free.\nMetadataConsulting.ca, {0}", copyrightSince);
		}

		private string GetAppName()
		{
			return base.ProductName;
		}

		private string GetAppVersion()
		{
			return "Version " + (Beta ? (base.ProductVersion + " (BETA)") : base.ProductVersion);
		}

		private void AboutCtrl_AutoSizeChanged(object sender, EventArgs e)
		{
			AdjustSize();
		}

		private void AdjustSize()
		{
			if (!AutoSize)
			{
				return;
			}
			int num = 0;
			foreach (Control control in base.Controls)
			{
				if (control.Width > num)
				{
					num = control.Width;
				}
			}
			base.Width = num;
			base.Height = lblRights.Top + lblRights.Height;
		}

		private void linkLabelPro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			linkLabelPro.LinkVisited = true;
			Process.Start("http://metadataconsulting.blogspot.ca/2017/04/Registry-Viewer-App-a-read-only-registry-viewer.html");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDC.UI.Controls.AboutCtrl));
			this.lblRights = new System.Windows.Forms.Label();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblAppName = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelDescription = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabelPro = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
			base.SuspendLayout();
			this.lblRights.AutoSize = true;
			this.lblRights.BackColor = System.Drawing.Color.Transparent;
			this.lblRights.Font = new System.Drawing.Font("Segoe UI Mono", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.lblRights.Location = new System.Drawing.Point(241, 87);
			this.lblRights.Name = "lblRights";
			this.lblRights.Size = new System.Drawing.Size(160, 15);
			this.lblRights.TabIndex = 32;
			this.lblRights.Text = "Requires .NET 4.0";
			this.lblCopyright.AutoSize = true;
			this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
			this.lblCopyright.Font = new System.Drawing.Font("Segoe UI", 11f);
			this.lblCopyright.Location = new System.Drawing.Point(240, 107);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(197, 20);
			this.lblCopyright.TabIndex = 31;
			this.lblCopyright.Text = "2017 MetadataConsulting.ca";
			this.lblVersion.AutoSize = true;
			this.lblVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.lblVersion.Location = new System.Drawing.Point(239, 23);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(81, 20);
			this.lblVersion.TabIndex = 30;
			this.lblVersion.Text = "Version 1.0";
			this.lblAppName.AutoSize = true;
			this.lblAppName.BackColor = System.Drawing.Color.Transparent;
			this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 15.75f);
			this.lblAppName.Location = new System.Drawing.Point(237, -8);
			this.lblAppName.Name = "lblAppName";
			this.lblAppName.Size = new System.Drawing.Size(180, 30);
			this.lblAppName.TabIndex = 29;
			this.lblAppName.Text = "Application Name";
			this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new System.Drawing.Point(156, 83);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(68, 62);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 33;
			this.pictureBox1.TabStop = false;
			this.labelDescription.AutoSize = true;
			this.labelDescription.Font = new System.Drawing.Font("Segoe UI", 9.25f);
			this.labelDescription.Location = new System.Drawing.Point(241, 46);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(202, 17);
			this.labelDescription.TabIndex = 34;
			this.labelDescription.Text = "Unlicensed for Personal Use Only";
			this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
			this.pictureBox2.Location = new System.Drawing.Point(0, 10);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(157, 142);
			this.pictureBox2.TabIndex = 35;
			this.pictureBox2.TabStop = false;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 9.25f);
			this.label1.Location = new System.Drawing.Point(242, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 17);
			this.label1.TabIndex = 36;
			this.label1.Text = "PRO edition";
			this.linkLabelPro.AutoSize = true;
			this.linkLabelPro.Location = new System.Drawing.Point(316, 64);
			this.linkLabelPro.Name = "linkLabelPro";
			this.linkLabelPro.Size = new System.Drawing.Size(28, 13);
			this.linkLabelPro.TabIndex = 37;
			this.linkLabelPro.TabStop = true;
			this.linkLabelPro.Text = "here";
			this.linkLabelPro.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabelPro_LinkClicked);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(this.linkLabelPro);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.pictureBox2);
			base.Controls.Add(this.labelDescription);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.lblVersion);
			base.Controls.Add(this.lblRights);
			base.Controls.Add(this.lblCopyright);
			base.Controls.Add(this.lblAppName);
			base.Name = "AboutCtrl";
			base.Size = new System.Drawing.Size(441, 148);
			base.AutoSizeChanged += new System.EventHandler(AboutCtrl_AutoSizeChanged);
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
