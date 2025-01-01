using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MDC.RegViewer.Registry;

namespace MDC.RegViewer.Editors
{
	internal class MultiStringEditor : ValueEditor
	{
		private IContainer components;

		private TextBox txtData;

		private Label labelRightClickSelectAll;

		public MultiStringEditor(RegValue value)
			: base(value)
		{
			InitializeComponent();
			txtData.Text = string.Join("\r\n", (string[])value.Data);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDC.RegViewer.Editors.MultiStringEditor));
			this.txtData = new System.Windows.Forms.TextBox();
			this.labelRightClickSelectAll = new System.Windows.Forms.Label();
			base.SuspendLayout();
			base.txtName.TabIndex = 3;
			base.label1.Location = new System.Drawing.Point(12, 14);
			base.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			base.btnCancel.TabIndex = 2;
			base.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			base.btnOK.TabIndex = 1;
			base.btnOK.Click += new System.EventHandler(btnOK_Click);
			this.txtData.AcceptsReturn = true;
			this.txtData.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtData.Location = new System.Drawing.Point(12, 81);
			this.txtData.Multiline = true;
			this.txtData.Name = "txtData";
			this.txtData.Size = new System.Drawing.Size(268, 122);
			this.txtData.TabIndex = 0;
			this.labelRightClickSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.labelRightClickSelectAll.AutoSize = true;
			this.labelRightClickSelectAll.Location = new System.Drawing.Point(140, 65);
			this.labelRightClickSelectAll.Name = "labelRightClickSelectAll";
			this.labelRightClickSelectAll.Size = new System.Drawing.Size(137, 13);
			this.labelRightClickSelectAll.TabIndex = 11;
			this.labelRightClickSelectAll.Text = "Tip! Right-click to Select All";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(292, 243);
			base.Controls.Add(this.labelRightClickSelectAll);
			base.Controls.Add(this.txtData);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "MultiStringEditor";
			this.Text = "Edit Multi-String";
			base.Controls.SetChildIndex(base.btnOK, 0);
			base.Controls.SetChildIndex(base.btnCancel, 0);
			base.Controls.SetChildIndex(base.label1, 0);
			base.Controls.SetChildIndex(base.txtName, 0);
			base.Controls.SetChildIndex(base.label2, 0);
			base.Controls.SetChildIndex(this.txtData, 0);
			base.Controls.SetChildIndex(this.labelRightClickSelectAll, 0);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
