using System;
using System.Drawing;
using System.Windows.Forms;
using DrawingEx.ColorManagement.ColorModels;
using DrawingEx.IconEncoder;
using DrawingEx.Imaging;
using VariIconsReload.Properties;

namespace VariIconsReload.Effects
{
	/// <summary>
	/// brightness and contrast effect
	/// </summary>
	public class BrightnessEffect : ConfigurableEffect
	{
		//renderer for this effect
		private class BrightnessRenderer : ConfigurableRenderer
		{
			private int _brightness, _contrast;
			public BrightnessRenderer(IconImage target)
				: base(target, target.Bitmap.Height) { }
			//render the whole chunk
			protected override void RenderChunk(Bitmap chunk, Point location)
			{
				using (ImageLock lock_original = new ImageLock(Original))
				{
					using (ImageLock lock_chunk = new ImageLock(chunk))
					{
						if (lock_chunk.Size != lock_original.Size)
							return;
						for (int i = 0; i < lock_chunk.Size; i++)
						{
							ColorBgra value = lock_original[i];
							lock_chunk[i] = new ColorBgra(
								value.A,
								Process(value.R),
								Process(value.G),
								Process(value.B));
						}
					}
				}
			}
			//process single channel
			private byte Process(byte value)
			{
				return
					(byte)Math.Max(0, Math.Min(255, _brightness +
					127 + (_contrast + 100) * ((int)value - 127) / 100));
			}
			// create config controls for this renderer
			public override Control[] CreateConfig()
			{
				ValueChooser val_bright = new ValueChooser();
				val_bright.Text = Resources.BrightnessEffect_Brightness;
				val_bright.Maximum = 255;
				val_bright.Minimum = -255;
				val_bright.DefaultValue = val_bright.Value =
					Settings.Default.BrightnessEffect_Brightness;
				_brightness = val_bright.DefaultValue;
				val_bright.ValueChanged += new System.EventHandler(bright_ValueChanged);
				//
				ValueChooser val_contrast = new ValueChooser();
				val_contrast.Text = Resources.BrightnessEffect_Contrast;
				val_contrast.Maximum = 100;
				val_contrast.Minimum = -100;
				val_contrast.DefaultValue = val_contrast.Value =
					Settings.Default.BrightnessEffect_Contrast;
				_contrast = val_contrast.DefaultValue;
				val_contrast.ValueChanged += new EventHandler(contrast_ValueChanged);
				return new Control[] { val_bright, val_contrast };
			}
			//update rendering param and setting
			void bright_ValueChanged(object sender, EventArgs e)
			{
				Cancel();
				Settings.Default.BrightnessEffect_Brightness =
					_brightness = ((ValueChooser)sender).Value;
				Start();
			}
			//update rendering param and setting
			void contrast_ValueChanged(object sender, EventArgs e)
			{
				Cancel();
				Settings.Default.BrightnessEffect_Contrast =
					_contrast = ((ValueChooser)sender).Value;
				Start();
			}
		}
		//ctor
		public BrightnessEffect()
		{
			this.Image = Resources.ef_negative;
			this.Name = Resources.BrightnessEffect_Name;
		}
		//create brightnessrenderer
		protected override ConfigurableRenderer CreateRenderer(IconImage target)
		{
			return new BrightnessRenderer(target);
		}
	}
}
