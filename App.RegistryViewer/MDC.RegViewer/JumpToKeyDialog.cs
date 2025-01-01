using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MDC.RegViewer
{
	internal class JumpToKeyDialog : Form
	{
		private IContainer components;

		private Button btnOK;

		private Button btnCancel;

		private Label label1;

		public TextBox txtKeyPath;

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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtKeyPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			base.SuspendLayout();
			this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(124, 65);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(btnOK_Click);
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(205, 65);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.txtKeyPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtKeyPath.Location = new System.Drawing.Point(12, 28);
			this.txtKeyPath.Name = "txtKeyPath";
			this.txtKeyPath.Size = new System.Drawing.Size(268, 20);
			this.txtKeyPath.TabIndex = 0;
			this.txtKeyPath.TextChanged += new System.EventHandler(txtKeyPath_TextChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Path of the key:";
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(292, 100);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtKeyPath);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "JumpToKeyDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Goto To Key";
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(JumpToKeyDialog_KeyDown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public JumpToKeyDialog()
		{
			InitializeComponent();
		}

		private void JumpToKeyDialog_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				base.CancelButton.PerformClick();
			}
		}

		private void txtKeyPath_TextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = true;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			string text = "";
			string text2 = "";
			if (!string.IsNullOrWhiteSpace(txtKeyPath.Text))
			{
				int num = txtKeyPath.Text.IndexOf("\\", StringComparison.Ordinal);
				if (num > 0)
				{
					text = txtKeyPath.Text.Substring(0, num);
					text2 = txtKeyPath.Text.Replace(text, "");
					if (text.Trim().EndsWith(":"))
					{
						int num2 = text.LastIndexOf(":");
						if (num2 >= 0)
						{
							text = text.Substring(0, num2);
						}
					}
					string text3 = "";
					switch (text)
					{
					case "HKU":
						text3 = "HKEY_USERS" + text2;
						break;
					case "HKLM":
						text3 = "HKEY_LOCAL_MACHINE" + text2;
						break;
					case "HKCU":
						text3 = "HKEY_CURRENT_USER" + text2;
						break;
					case "HKCR":
						text3 = "HKEY_CLASSES_ROOT" + text2;
						break;
					case "HKCC":
						text3 = "HKEY_CURRENT_CONFIG" + text2;
						break;
					case "HKDD":
						text3 = "HKEY_DYN_DATA" + text2;
						break;
					case "HKPD":
						text3 = "HKEY_PERFORMANCE_DATA" + text2;
						break;
					default:
						text3 = text + text2;
						break;
					}
					txtKeyPath.Text = text3;
					btnOK.Enabled = true;
				}
				else if (txtKeyPath.Text.Trim().StartsWith("HKEY_"))
				{
					btnOK.Enabled = true;
				}
				else
				{
					MessageBox.Show("Registry path is incorrect.\nDoes not start with a root hive.\nie. HKEY_LOCAL_MACHINE\\... or HKLM\\... or HKLM:\\...", "Registry Path Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					btnOK.Enabled = false;
				}
			}
			else
			{
				btnOK.Enabled = false;
			}
		}
	}
}
