using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CheVolume.Controls
{
	public class CheButton : Button
	{
		protected override void OnResize(EventArgs P_0)
		{
			using (GraphicsPath graphicsPath = new GraphicsPath())
			{
				graphicsPath.AddEllipse(new Rectangle(0, 0, base.Width, base.Height));
				base.Region = new Region(graphicsPath);
			}
			base.OnResize(P_0);
		}
	}
}
