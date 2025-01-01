using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MDC.Collections;
using MDC.RegViewer.Registry;

namespace MDC.RegViewer
{
	internal class RemoveFavoritesDialog : Form
	{
		private IContainer components;

		private Label label1;

		private Button btOK;

		private Button btnCancel;

		private ListBox lstKeys;

		private EventDictionary<string, string> favorites;

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
			this.label1 = new System.Windows.Forms.Label();
			this.btOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lstKeys = new System.Windows.Forms.ListBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select Favorite(s):";
			this.btOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btOK.Location = new System.Drawing.Point(205, 12);
			this.btOK.Name = "btOK";
			this.btOK.Size = new System.Drawing.Size(75, 23);
			this.btOK.TabIndex = 1;
			this.btOK.Text = "OK";
			this.btOK.UseVisualStyleBackColor = true;
			this.btOK.Click += new System.EventHandler(btOK_Click);
			this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(205, 53);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.lstKeys.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			this.lstKeys.FormattingEnabled = true;
			this.lstKeys.Location = new System.Drawing.Point(15, 37);
			this.lstKeys.Name = "lstKeys";
			this.lstKeys.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstKeys.Size = new System.Drawing.Size(176, 134);
			this.lstKeys.TabIndex = 3;
			base.AcceptButton = this.btOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(292, 189);
			base.Controls.Add(this.lstKeys);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btOK);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RemoveFavoritesDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Remove Favorites";
			base.Load += new System.EventHandler(RemoveFavoritesDialog_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public RemoveFavoritesDialog(EventDictionary<string, string> favorites)
		{
			InitializeComponent();
			this.favorites = favorites;
		}

		private void RemoveFavoritesDialog_Load(object sender, EventArgs e)
		{
			foreach (string key in favorites.Keys)
			{
				lstKeys.Items.Add(key);
			}
		}

		private void btOK_Click(object sender, EventArgs e)
		{
			RegKey regKey = RegKey.Parse("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Applets\\Regedit\\Favorites", true);
			foreach (object selectedItem in lstKeys.SelectedItems)
			{
				string text = selectedItem.ToString();
				try
				{
					regKey.Key.DeleteValue(text);
					favorites.Remove(text);
				}
				catch
				{
					MessageBox.Show("Cannot delete a permanent favorite.", "Favorite Delete Attempt", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
	}
}
