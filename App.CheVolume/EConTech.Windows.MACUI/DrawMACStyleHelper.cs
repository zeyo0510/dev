using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EConTech.Windows.MACUI
{
	public sealed class DrawMACStyleHelper
	{
		private DrawMACStyleHelper()
		{
		}

		public static void DrawAquaPill(Graphics P_0, RectangleF P_1, Color P_2, Orientation P_3)
		{
			ColorBlend colorBlend = new ColorBlend();
			Color color = ColorHelper.OpacityMix(Color.White, ColorHelper.SoftLightMix(P_2, Color.Black, 100), 40);
			Color color2 = ColorHelper.OpacityMix(Color.White, ColorHelper.SoftLightMix(P_2, ColorHelper.CreateColorFromRGB(64, 64, 64), 100), 20);
			Color color3 = ColorHelper.SoftLightMix(P_2, ColorHelper.CreateColorFromRGB(128, 128, 128), 100);
			Color color4 = ColorHelper.SoftLightMix(P_2, ColorHelper.CreateColorFromRGB(192, 192, 192), 100);
			Color color5 = ColorHelper.OverlayMix(ColorHelper.SoftLightMix(P_2, Color.White, 100), Color.White, 75);
			colorBlend.Colors = new Color[5] { color, color2, color3, color4, color5 };
			colorBlend.Positions = new float[5] { 0f, 0.25f, 0.5f, 0.75f, 1f };
			LinearGradientBrush linearGradientBrush = ((P_3 != 0) ? new LinearGradientBrush(new Point((int)P_1.Left - 1, (int)P_1.Top), new Point((int)P_1.Left + (int)P_1.Width + 1, (int)P_1.Top), color, color5) : new LinearGradientBrush(new Point((int)P_1.Left, (int)P_1.Top - 1), new Point((int)P_1.Left, (int)P_1.Top + (int)P_1.Height + 1), color, color5));
			linearGradientBrush.InterpolationColors = colorBlend;
			FillPill(linearGradientBrush, P_1, P_0);
			color2 = Color.White;
			colorBlend.Colors = new Color[4] { color2, color3, color4, color5 };
			colorBlend.Positions = new float[4] { 0f, 0.5f, 0.75f, 1f };
			linearGradientBrush = ((P_3 != 0) ? new LinearGradientBrush(new Point((int)P_1.Left, (int)P_1.Top + 1), new Point((int)P_1.Left + (int)P_1.Width - 1, (int)P_1.Top + 1), color2, color5) : new LinearGradientBrush(new Point((int)P_1.Left + 1, (int)P_1.Top), new Point((int)P_1.Left + 1, (int)P_1.Top + (int)P_1.Height - 1), color2, color5));
			linearGradientBrush.InterpolationColors = colorBlend;
			FillPill(linearGradientBrush, RectangleF.Inflate(P_1, -3f, -3f), P_0);
		}

		public static void DrawAquaPillSingleLayer(Graphics P_0, RectangleF P_1, Color P_2, Orientation P_3)
		{
			ColorBlend colorBlend = new ColorBlend();
			Color color = ControlPaint.Light(P_2);
			Color color2 = ControlPaint.Light(color);
			Color color3 = ControlPaint.Light(color2);
			colorBlend.Colors = new Color[4] { P_2, color, color2, color3 };
			colorBlend.Positions = new float[4] { 0f, 0.25f, 0.65f, 1f };
			LinearGradientBrush linearGradientBrush = ((P_3 != 0) ? new LinearGradientBrush(new Point((int)P_1.Left, (int)P_1.Top), new Point((int)P_1.Left + (int)P_1.Width, (int)P_1.Top), P_2, color3) : new LinearGradientBrush(new Point((int)P_1.Left, (int)P_1.Top), new Point((int)P_1.Left, (int)P_1.Top + (int)P_1.Height), P_2, color3));
			linearGradientBrush.InterpolationColors = colorBlend;
			FillPill(linearGradientBrush, P_1, P_0);
		}

		public static void FillPill(Brush P_0, RectangleF P_1, Graphics P_2)
		{
			if (P_1.Width > P_1.Height)
			{
				P_2.SmoothingMode = SmoothingMode.HighQuality;
				P_2.FillEllipse(P_0, new RectangleF(P_1.Left, P_1.Top, P_1.Height, P_1.Height));
				P_2.FillEllipse(P_0, new RectangleF(P_1.Left + P_1.Width - P_1.Height, P_1.Top, P_1.Height, P_1.Height));
				float width = P_1.Width - P_1.Height;
				float x = P_1.Left + P_1.Height / 2f;
				P_2.FillRectangle(P_0, new RectangleF(x, P_1.Top, width, P_1.Height));
				P_2.SmoothingMode = SmoothingMode.Default;
			}
			else if (P_1.Width < P_1.Height)
			{
				P_2.SmoothingMode = SmoothingMode.HighQuality;
				P_2.FillEllipse(P_0, new RectangleF(P_1.Left, P_1.Top, P_1.Width, P_1.Width));
				P_2.FillEllipse(P_0, new RectangleF(P_1.Left, P_1.Top + P_1.Height - P_1.Width, P_1.Width, P_1.Width));
				float y = P_1.Top + P_1.Width / 2f;
				float height = P_1.Height - P_1.Width;
				P_2.FillRectangle(P_0, new RectangleF(P_1.Left, y, P_1.Width, height));
				P_2.SmoothingMode = SmoothingMode.Default;
			}
			else if (P_1.Width == P_1.Height)
			{
				P_2.SmoothingMode = SmoothingMode.HighQuality;
				P_2.FillEllipse(P_0, P_1);
				P_2.SmoothingMode = SmoothingMode.Default;
			}
		}
	}
}
