using System.Drawing;
using System.Windows.Forms;

namespace a
{
	internal class Control1 : Control
	{
		private Image _Image;

		public Image Image
		{
			get
			{
				return _Image;
			}
			set
			{
				_Image = value;
			}
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams obj = base.CreateParams;
				obj.ExStyle |= 32;
				return obj;
			}
		}

		protected override void OnPaintBackground(PaintEventArgs P_0)
		{
		}

		protected override void OnPaint(PaintEventArgs P_0)
		{
			if (Image != null)
			{
				P_0.Graphics.DrawImage(Image, 0, 0);
			}
		}
	}
}
