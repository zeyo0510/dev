using System;
using System.Drawing;

namespace EConTech.Windows.MACUI
{
	internal class ColorHelper
	{
		public static Color CreateColorFromRGB(int P_0, int P_1, int P_2)
		{
			int num = P_0;
			if (num > 255)
			{
				num = 255;
			}
			if (num < 0)
			{
				num = 0;
			}
			int num2 = P_1;
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			int num3 = P_2;
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			return Color.FromArgb(num, num2, num3);
		}

		public static Color OpacityMix(Color P_0, Color P_1, int P_2)
		{
			byte r = P_0.R;
			int g = P_0.G;
			int b = P_0.B;
			int r2 = P_1.R;
			int g2 = P_1.G;
			int b2 = P_1.B;
			int num = (int)((float)(int)r * ((float)P_2 / 100f) + (float)r2 * (1f - (float)P_2 / 100f));
			int num2 = (int)((float)g * ((float)P_2 / 100f) + (float)g2 * (1f - (float)P_2 / 100f));
			int num3 = (int)((float)b * ((float)P_2 / 100f) + (float)b2 * (1f - (float)P_2 / 100f));
			return CreateColorFromRGB(num, num2, num3);
		}

		public static Color SoftLightMix(Color P_0, Color P_1, int P_2)
		{
			byte r = P_0.R;
			int g = P_0.G;
			int b = P_0.B;
			int r2 = P_1.R;
			int g2 = P_1.G;
			int b2 = P_1.B;
			int num = SoftLightMath(r, r2);
			int num2 = SoftLightMath(g, g2);
			int num3 = SoftLightMath(b, b2);
			return OpacityMix(CreateColorFromRGB(num, num2, num3), P_0, P_2);
		}

		public static Color OverlayMix(Color P_0, Color P_1, int P_2)
		{
			byte r = P_0.R;
			byte g = P_0.G;
			byte b = P_0.B;
			byte r2 = P_1.R;
			byte g2 = P_1.G;
			byte b2 = P_1.B;
			int num = OverlayMath(P_0.R, P_1.R);
			int num2 = OverlayMath(P_0.G, P_1.G);
			int num3 = OverlayMath(P_0.B, P_1.B);
			return OpacityMix(CreateColorFromRGB(num, num2, num3), P_0, P_2);
		}

		private static int SoftLightMath(int P_0, int P_1)
		{
			float num = (float)P_0 / 255f;
			float num2 = (float)P_1 / 255f;
			if ((double)num2 < 0.5)
			{
				return (int)(((double)(2f * num * num2) + Math.Pow(num, 2.0) * (double)(1f - 2f * num2)) * 255.0);
			}
			return (int)((Math.Sqrt(num) * (double)(2f * num2 - 1f) + (double)(2f * num * (1f - num2))) * 255.0);
		}

		public static int OverlayMath(int P_0, int P_1)
		{
			double num = (double)P_0 / 255.0;
			double num2 = (double)P_1 / 255.0;
			if (num < 0.5)
			{
				return (int)(2.0 * num * num2 * 255.0);
			}
			return (int)((1.0 - 2.0 * (1.0 - num) * (1.0 - num2)) * 255.0);
		}
	}
}
