using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MDC.UI.Controls;

namespace MDC.RegViewer
{
	internal class AboutBox : Form
	{
		private IContainer components;

		private AboutCtrl aboutCtrl1;

		public AboutBox()
		{
			InitializeComponent();
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
			this.aboutCtrl1 = new MDC.UI.Controls.AboutCtrl();
			base.SuspendLayout();
			this.aboutCtrl1.AutoSize = true;
			this.aboutCtrl1.BackColor = System.Drawing.Color.Transparent;
			this.aboutCtrl1.Beta = false;
			this.aboutCtrl1.CopyrightSince = 2017;
			this.aboutCtrl1.Location = new System.Drawing.Point(12, 12);
			this.aboutCtrl1.Name = "aboutCtrl1";
			this.aboutCtrl1.Size = new System.Drawing.Size(454, 150);
			this.aboutCtrl1.TabIndex = 25;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			base.ClientSize = new System.Drawing.Size(471, 167);
			base.Controls.Add(this.aboutCtrl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AboutBox";
			base.Padding = new System.Windows.Forms.Padding(9);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About RegViewer";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
