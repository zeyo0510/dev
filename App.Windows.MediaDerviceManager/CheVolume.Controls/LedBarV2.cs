using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CheVolume.Controls
{
	public class LedBarV2 : UserControl
	{
		private IContainer components;

		public LedBarV2()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool P_0)
		{
			if (P_0 && components != null)
			{
				components.Dispose();
			}
			base.Dispose(P_0);
		}

		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Name = "LedBarV2";
			base.Size = new System.Drawing.Size(85, 421);
			base.ResumeLayout(false);
		}
	}
}
