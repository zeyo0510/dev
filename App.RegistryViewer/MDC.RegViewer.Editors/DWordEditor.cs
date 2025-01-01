using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MDC.RegViewer.Registry;
using MDC.UI.Controls;
using Microsoft.Win32;

namespace MDC.RegViewer.Editors
{
	internal class DWordEditor : ValueEditor
	{
		private IContainer components;

		private GroupBox groupBox1;

		private RadioButton rdoDecimal;

		private RadioButton rdoHex;

		private NumericTextBox txtData;

		public DWordEditor(RegValue value)
			: base(value)
		{
			InitializeComponent();
			string text = ((value.Kind != RegistryValueKind.DWord) ? ((long)value.Data).ToString("x") : ((int)value.Data).ToString("x"));
			txtData.Text = text;
		}

		private void base_CheckedChanged(object sender, EventArgs e)
		{
			txtData.HexNumber = rdoHex.Checked;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDC.RegViewer.Editors.DWordEditor));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdoDecimal = new System.Windows.Forms.RadioButton();
			this.rdoHex = new System.Windows.Forms.RadioButton();
			this.txtData = new MDC.UI.Controls.NumericTextBox();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			base.txtName.TabIndex = 5;
			base.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			base.btnCancel.Location = new System.Drawing.Point(205, 204);
			base.btnCancel.TabIndex = 4;
			base.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			base.btnOK.Location = new System.Drawing.Point(124, 204);
			base.btnOK.TabIndex = 3;
			base.btnOK.Click += new System.EventHandler(btnOK_Click);
			this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.groupBox1.Controls.Add(this.rdoDecimal);
			this.groupBox1.Controls.Add(this.rdoHex);
			this.groupBox1.Location = new System.Drawing.Point(139, 121);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(138, 68);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Base";
			this.rdoDecimal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.rdoDecimal.AutoSize = true;
			this.rdoDecimal.Location = new System.Drawing.Point(11, 45);
			this.rdoDecimal.Name = "rdoDecimal";
			this.rdoDecimal.Size = new System.Drawing.Size(63, 17);
			this.rdoDecimal.TabIndex = 2;
			this.rdoDecimal.Text = "&Decimal";
			this.rdoDecimal.UseVisualStyleBackColor = true;
			this.rdoDecimal.CheckedChanged += new System.EventHandler(base_CheckedChanged);
			this.rdoHex.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.rdoHex.AutoSize = true;
			this.rdoHex.Checked = true;
			this.rdoHex.Location = new System.Drawing.Point(11, 22);
			this.rdoHex.Name = "rdoHex";
			this.rdoHex.Size = new System.Drawing.Size(86, 17);
			this.rdoHex.TabIndex = 1;
			this.rdoHex.TabStop = true;
			this.rdoHex.Text = "&Hexadecimal";
			this.rdoHex.UseVisualStyleBackColor = true;
			this.rdoHex.CheckedChanged += new System.EventHandler(base_CheckedChanged);
			this.txtData.AllowDecimal = false;
			this.txtData.AllowGrouping = false;
			this.txtData.AllowNegative = false;
			this.txtData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtData.HexNumber = true;
			this.txtData.Location = new System.Drawing.Point(12, 84);
			this.txtData.MaxLength = 8;
			this.txtData.Name = "txtData";
			this.txtData.Size = new System.Drawing.Size(265, 20);
			this.txtData.TabIndex = 0;
			this.txtData.Text = "0";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(292, 242);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.txtData);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "DWordEditor";
			this.Text = "Edit DWORD Value";
			base.Controls.SetChildIndex(this.txtData, 0);
			base.Controls.SetChildIndex(base.btnOK, 0);
			base.Controls.SetChildIndex(base.btnCancel, 0);
			base.Controls.SetChildIndex(this.groupBox1, 0);
			base.Controls.SetChildIndex(base.label1, 0);
			base.Controls.SetChildIndex(base.txtName, 0);
			base.Controls.SetChildIndex(base.label2, 0);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
