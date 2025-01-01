using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MDC.RegViewer.Registry;

namespace MDC.RegViewer.Editors
{
	internal class StringEditor : ValueEditor
	{
		private IContainer components;

		private TextBox txtData;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDC.RegViewer.Editors.StringEditor));
			this.txtData = new System.Windows.Forms.TextBox();
			base.SuspendLayout();
			base.txtName.TabIndex = 3;
			base.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			base.btnCancel.Location = new System.Drawing.Point(207, 112);
			base.btnCancel.TabIndex = 2;
			base.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			base.btnOK.Location = new System.Drawing.Point(126, 112);
			base.btnOK.TabIndex = 1;
			base.btnOK.Click += new System.EventHandler(btnOK_Click);
			this.txtData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtData.Location = new System.Drawing.Point(12, 81);
			this.txtData.Name = "txtData";
			this.txtData.Size = new System.Drawing.Size(268, 20);
			this.txtData.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(292, 145);
			base.Controls.Add(this.txtData);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "StringEditor";
			this.Text = "Edit String";
			base.Controls.SetChildIndex(base.btnCancel, 0);
			base.Controls.SetChildIndex(base.btnOK, 0);
			base.Controls.SetChildIndex(this.txtData, 0);
			base.Controls.SetChildIndex(base.txtName, 0);
			base.Controls.SetChildIndex(base.label1, 0);
			base.Controls.SetChildIndex(base.label2, 0);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public StringEditor(RegValue value)
			: base(value)
		{
			InitializeComponent();
			txtData.Text = value.Data.ToString();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
		}
	}
}
