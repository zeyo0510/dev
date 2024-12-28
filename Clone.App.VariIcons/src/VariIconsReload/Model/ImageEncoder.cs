using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using VariIconsReload.Effects;
using VariIconsReload.Properties;
using VariIconsSDK.Model;
using VariIconsReload.Forms;

namespace VariIconsReload.Model
{
	public static class ImageEncoder
	{
		/// <summary>
		/// gets all importformats as file filters
		/// </summary>
		public static List<FileFilter> GetImportExtensions()
		{
			return GetExtensions(ImageCodecInfo.GetImageDecoders());
		}
		/// <summary>
		/// gets all export formats as file filters
		/// </summary>
		public static List<FileFilter> GetExportExtensions()
		{
			return GetExtensions(ImageCodecInfo.GetImageEncoders());
		}
		//expand fileextensions to file filters
		private static List<FileFilter> GetExtensions(ImageCodecInfo[] codecs)
		{
			List<FileFilter> ret = new List<FileFilter>();
			foreach (ImageCodecInfo codec in codecs)
			{
				//split multiple extensions
				string[] extensions = codec.FilenameExtension.Split(';');
				for (int i = 0; i < extensions.Length; i++)
					extensions[i] = extensions[i].Trim('*', '.').ToLower();
				//create file filter
				ret.Add(new FileFilter(codec.FormatDescription,
					extensions));
			}
			return ret;
		}
		/// <summary>
		/// imports and resizes an image from the given file
		/// </summary>
		public static Bitmap ImportImage(string filename, Size maxsize)
		{
			using (Image img = Image.FromFile(filename))
			{
				return ImportImage(img, maxsize);
			}
		}
		public static Bitmap ImportImage(Image img, Size maxsize)
		{
			if (img == null)
				throw new ArgumentNullException("img");
			if (img.Width < maxsize.Width && img.Height < maxsize.Height)
			{
				//right size, 
				Bitmap bmp = new Bitmap(img.Width, img.Height,
					PixelFormat.Format32bppArgb);
				using (Graphics gr = Graphics.FromImage(bmp))
					gr.DrawImage(img, 0, 0, img.Width, img.Height);
				return bmp;
			}
			else
			{
				using (PickContent frm = new PickContent())
				{
					frm.Image = img;
					if (frm.ShowDialog() != DialogResult.OK)
						return null;
					//
					Bitmap bmp = new Bitmap(frm.ImageSize.Width, frm.ImageSize.Height,
						PixelFormat.Format32bppArgb);
					using (Graphics gr = Graphics.FromImage(bmp))
						gr.DrawImage(img, frm.Inverse);
					return bmp;
				}
			}
		}
		/// <summary>
		/// saves the given bitmap to the given filename
		/// may throw exceptions.
		/// </summary>
		public static void ExportImage(Bitmap bmp, string filename)
		{
			if (bmp == null || filename == null)
				throw new ArgumentNullException();
			//select codec by extension
			string ext = Path.GetExtension(filename).ToLower();
			ImageCodecInfo selectedcodec = null;
			foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
			{
				if (codec.FilenameExtension.ToLower().Contains(ext))
				{
					selectedcodec = codec;
					break;
				}
			}
			if (selectedcodec == null)
				throw new ArgumentException("invalid file extension");
			//parameter selection
			EncoderParameters param = null;
			if (selectedcodec.FormatID == ImageFormat.Jpeg.Guid)
			{
				ValueChooser chsQuality = new ValueChooser();
				chsQuality.Minimum = 0;
				chsQuality.Maximum = 100;
				chsQuality.Value = chsQuality.DefaultValue =
					Settings.Default.ImageEncoder_JPGQuality;
				//use effect config form
				using (EffectParamsForm frm = new EffectParamsForm(new Control[] { chsQuality }))
				{
					if (frm.ShowDialog() == DialogResult.OK)
						Settings.Default.ImageEncoder_JPGQuality =
							chsQuality.Value;
					else
						chsQuality.Value = chsQuality.DefaultValue;
					//
					param = new EncoderParameters();
					param.Param = new EncoderParameter[]{
						new EncoderParameter(Encoder.Quality,chsQuality.Value)};
				}
			}
			//save image
			FileStream fs = null;
			try
			{
				//use filestream, otherwise bmp.save would produce
				//a lock on the file
				fs = new FileStream(filename, FileMode.Create);
				bmp.Save(fs, selectedcodec, param);
				fs.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (param != null)
					param.Dispose();
				if (fs != null)
					fs.Dispose();
			}
		}
	}
}
