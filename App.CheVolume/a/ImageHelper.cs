using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CheVolume.Properties;

namespace a
{
	internal static class ImageHelper
	{
		internal static ImageList imageList1;

		internal static ImageList imageList2;

		internal static ImageList imageList3;

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
		private static extern bool _DestroyIcon(IntPtr P_0);

		[DllImport("shell32.dll", EntryPoint = "ExtractIconEx")]
		private static extern int _ExtractIconEx(string P_0, int P_1, IntPtr[] P_2, IntPtr[] P_3, int P_4);

		internal static void SetImageSize(float P_0)
		{
			Size imageSize = new Size((int)(32f * P_0), (int)(32f * P_0));
			ImageList imageList = new ImageList();
			imageList.ImageSize = imageSize;
			imageList.ColorDepth = ColorDepth.Depth32Bit;
			imageList1 = imageList;
			ImageList imageList2 = new ImageList();
			imageList2.ImageSize = imageSize;
			imageList2.ColorDepth = ColorDepth.Depth32Bit;
			ImageHelper.imageList2 = imageList2;
			ImageList imageList3 = new ImageList();
			imageList3.ImageSize = imageSize;
			imageList3.ColorDepth = ColorDepth.Depth32Bit;
			ImageHelper.imageList3 = imageList3;
		}

		internal static void Add(string P_0)
		{
			Icon icon = Method1(P_0);
			imageList2.Images.Add(icon);
			imageList1.Images.Add(icon);
			imageList3.Images.Add(IconToImage(icon, Resources.defaultDevice));
		}

		internal static Icon Method1(string P_0)
		{
			string[] array = Environment.ExpandEnvironmentVariables(P_0).Split(',');
			if (File.Exists(array[0]))
			{
				if (array.Length > 1)
				{
					IntPtr[] array2 = new IntPtr[1];
					_ExtractIconEx(array[0], int.Parse(array[1]), array2, null, 1);
					return Icon.FromHandle(array2[0]);
				}
				return new Icon(array[0], 32, 32);
			}
			return Resources.appicon;
		}

		private static Image ResizeImage(Image P_0, Size P_1)
		{
			return new Bitmap(P_0, P_1);
		}

		internal static void Clear()
		{
			imageList1.Images.Clear();
			imageList2.Images.Clear();
			imageList3.Images.Clear();
		}

		private static Image IconToImage(Icon P_0, Image P_1)
		{
			using (Image image = P_0.ToBitmap())
			{
				Bitmap bitmap = new Bitmap(P_0.Width, P_0.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.DrawImage(image, 0, 0);
					graphics.DrawImage(P_1, 0, 0, image.Width, image.Height);
					graphics.Save();
				}
				_DestroyIcon(P_0.Handle);
				return bitmap;
			}
		}

		internal static Bitmap Method3(Bitmap P_0, int P_1, float P_2, float P_3)
		{
			for (int i = 0; i < P_0.Height; i++)
			{
				for (int j = 0; j < P_0.Width; j++)
				{
					Color pixel = P_0.GetPixel(j, i);
					float num = pixel.GetBrightness() + P_3;
					num = ((num < 0f) ? 0f : num);
					num = ((num > 1f) ? 1f : num);
					Color color = Method2(pixel.A, P_1, pixel.GetSaturation() + P_2, num);
					P_0.SetPixel(j, i, color);
				}
			}
			return P_0;
		}

		private static Color Method2(int P_0, float P_1, float P_2, float P_3)
		{
			if (0f == P_2)
			{
				return Color.FromArgb(P_0, Convert.ToInt32(P_3 * 255f), Convert.ToInt32(P_3 * 255f), Convert.ToInt32(P_3 * 255f));
			}
			float num;
			float num2;
			if (0.5 < (double)P_3)
			{
				num = P_3 - P_3 * P_2 + P_2;
				num2 = P_3 + P_3 * P_2 - P_2;
			}
			else
			{
				num = P_3 + P_3 * P_2;
				num2 = P_3 - P_3 * P_2;
			}
			int num3 = (int)Math.Floor(P_1 / 60f);
			if (300f <= P_1)
			{
				P_1 -= 360f;
			}
			P_1 /= 60f;
			P_1 -= 2f * (float)Math.Floor(((float)num3 + 1f) % 6f / 2f);
			float num4 = ((num3 % 2 != 0) ? (num2 - P_1 * (num - num2)) : (P_1 * (num - num2) + num2));
			int num5 = Convert.ToInt32(num * 255f);
			int num6 = Convert.ToInt32(num4 * 255f);
			int num7 = Convert.ToInt32(num2 * 255f);
			switch (num3)
			{
			case 1:
				return Color.FromArgb(P_0, num6, num5, num7);
			case 2:
				return Color.FromArgb(P_0, num7, num5, num6);
			case 3:
				return Color.FromArgb(P_0, num7, num6, num5);
			case 4:
				return Color.FromArgb(P_0, num6, num7, num5);
			case 5:
				return Color.FromArgb(P_0, num5, num7, num6);
			default:
				return Color.FromArgb(P_0, num5, num6, num7);
			}
		}
	}
}
