using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MDC.RegViewer.Export;
using MDC.RegViewer.Properties;
using MDC.RegViewer.Registry;

namespace MDC.RegViewer
{
	internal class ExportDialog : Form
	{
		private IContainer components;

		private Label label1;

		private Button btnExport;

		private Button btnCancel;

		private SaveFileDialog saveFileDialog1;

		private Button btnBrowse;

		private TextBox txtFile;

		private Label label2;

		public ComboBox cmbBranch;

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
			this.btnExport = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbBranch = new System.Windows.Forms.ComboBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Branch:";
			this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnExport.Enabled = false;
			this.btnExport.Location = new System.Drawing.Point(196, 76);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(75, 23);
			this.btnExport.TabIndex = 3;
			this.btnExport.Text = "Export";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(btnExport_Click);
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(277, 76);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.saveFileDialog1.Filter = "Registry Files (*.reg)|*.reg|XML Files (*.xml)|*.xml|Text Files (*.txt)|*.txt";
			this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.btnBrowse.Location = new System.Drawing.Point(277, 12);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "&Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(btnBrowse_Click);
			this.txtFile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.txtFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.txtFile.Location = new System.Drawing.Point(59, 12);
			this.txtFile.Name = "txtFile";
			this.txtFile.ReadOnly = true;
			this.txtFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtFile.Size = new System.Drawing.Size(212, 20);
			this.txtFile.TabIndex = 0;
			this.txtFile.TextChanged += new System.EventHandler(txtFile_TextChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(27, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(26, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "File:";
			this.cmbBranch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.cmbBranch.FormattingEnabled = true;
			this.cmbBranch.Items.AddRange(new object[5] { "HKEY_CLASSES_ROOT", "HKEY_CURRENT_USER", "HKEY_LOCAL_MACHINE", "HKEY_USERS", "HKEY_CURRENT_CONFIG" });
			this.cmbBranch.Location = new System.Drawing.Point(59, 44);
			this.cmbBranch.Name = "cmbBranch";
			this.cmbBranch.Size = new System.Drawing.Size(293, 21);
			this.cmbBranch.TabIndex = 1;
			this.cmbBranch.TextChanged += new System.EventHandler(cmbBranch_TextChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(364, 114);
			base.Controls.Add(this.cmbBranch);
			base.Controls.Add(this.btnBrowse);
			base.Controls.Add(this.txtFile);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnExport);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ExportDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Export Registry Key";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public ExportDialog()
		{
			InitializeComponent();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			RegKey key;
			if ((key = RegKey.Parse(cmbBranch.Text)) == null)
			{
				UIUtility.DisplayError(this, Resources.Error_InvalidKey, cmbBranch);
				return;
			}
			RegExportFormat exportFormat = GetExportFormat();
			Stream output;
			try
			{
				output = File.OpenWrite(txtFile.Text);
			}
			catch
			{
				UIUtility.DisplayError(this, Resources.Error_FileOpenFail, txtFile);
				return;
			}
			if (!ExportToFile(key, exportFormat, output))
			{
				UIUtility.DisplayError(this, Resources.Error_ExportFail);
				return;
			}
			UIUtility.InformUser(this, Resources.Info_ExportSuccess);
			Close();
		}

		private bool ExportToFile(RegKey key, RegExportFormat format, Stream output)
		{
			bool result = true;
			using (output)
			{
				DisableControls();
				try
				{
					using (new BusyCursor(this))
					{
						using (StreamWriter writer = new StreamWriter(output))
						{
							RegExporter.Export(key, ExportProvider.Create(format, writer));
						}
					}
				}
				catch
				{
					result = false;
				}
				EnableControls();
				return result;
			}
		}

		private RegExportFormat GetExportFormat()
		{
			if (saveFileDialog1.FilterIndex == 1)
			{
				return RegExportFormat.NativeRegFormat;
			}
			if (saveFileDialog1.FilterIndex == 2)
			{
				return RegExportFormat.XmlFormat;
			}
			return RegExportFormat.TextFormat;
		}

		private void EnableControls()
		{
			Button button = btnBrowse;
			Button button2 = btnExport;
			Button button3 = btnCancel;
			bool flag2 = (cmbBranch.Enabled = true);
			bool flag4 = (button3.Enabled = flag2);
			bool enabled = (button2.Enabled = flag4);
			button.Enabled = enabled;
		}

		private void DisableControls()
		{
			Button button = btnBrowse;
			Button button2 = btnExport;
			Button button3 = btnCancel;
			bool flag2 = (cmbBranch.Enabled = false);
			bool flag4 = (button3.Enabled = flag2);
			bool enabled = (button2.Enabled = flag4);
			button.Enabled = enabled;
		}

		private void cmbBranch_TextChanged(object sender, EventArgs e)
		{
			SetExportButtonState();
		}

		private void SetExportButtonState()
		{
			btnExport.Enabled = cmbBranch.Text != string.Empty && txtFile.Text != string.Empty;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				txtFile.Text = saveFileDialog1.FileName;
			}
		}

		private void txtFile_TextChanged(object sender, EventArgs e)
		{
			SetExportButtonState();
		}
	}
}
