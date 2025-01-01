using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Be.Windows.Forms;
using MDC.RegViewer.Registry;

namespace MDC.RegViewer.Editors
{
	internal class BinaryEditor : ValueEditor
	{
		private IContainer components;

		private HexBox txtData;

		private DynamicByteProvider byteProvider;

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
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDC.RegViewer.Editors.BinaryEditor));
			this.txtData = new HexBox();
			base.SuspendLayout();
			base.txtName.Size = new System.Drawing.Size(342, 20);
			base.txtName.TabIndex = 3;
			base.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			base.btnCancel.Location = new System.Drawing.Point(279, 253);
			base.btnCancel.TabIndex = 2;
			base.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			base.btnOK.Location = new System.Drawing.Point(198, 253);
			base.btnOK.TabIndex = 1;
			base.btnOK.Click += new System.EventHandler(btnOK_Click);
			((System.Windows.Forms.Control)(object)this.txtData).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			((System.Windows.Forms.Control)(object)this.txtData).BackColor = System.Drawing.SystemColors.Window;
			this.txtData.BackColorDisabled = System.Drawing.SystemColors.ControlDark;
			this.txtData.BytesPerLine = 8;
			((System.Windows.Forms.Control)(object)this.txtData).Font = new System.Drawing.Font("Courier New", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.txtData.LineInfoVisible = true;
			((System.Windows.Forms.Control)(object)this.txtData).Location = new System.Drawing.Point(12, 81);
			((System.Windows.Forms.Control)(object)this.txtData).Name = "txtData";
			this.txtData.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			this.txtData.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			this.txtData.ShadowSelectionColor = System.Drawing.Color.FromArgb(100, 60, 188, 255);
			((System.Windows.Forms.Control)(object)this.txtData).Size = new System.Drawing.Size(342, 166);
			this.txtData.StringViewVisible = true;
			((System.Windows.Forms.Control)(object)this.txtData).TabIndex = 0;
			this.txtData.UseFixedBytesPerLine = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(363, 288);
			base.Controls.Add((System.Windows.Forms.Control)(object)this.txtData);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "BinaryEditor";
			this.Text = "Edit Binary Value";
			base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtData, 0);
			base.Controls.SetChildIndex(base.btnOK, 0);
			base.Controls.SetChildIndex(base.btnCancel, 0);
			base.Controls.SetChildIndex(base.label1, 0);
			base.Controls.SetChildIndex(base.txtName, 0);
			base.Controls.SetChildIndex(base.label2, 0);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public BinaryEditor(RegValue value)
			: base(value)
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			InitializeComponent();
			byteProvider = new DynamicByteProvider((byte[])value.Data);
			txtData.ByteProvider = (IByteProvider)(object)byteProvider;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
		}
	}
}
