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
	/// colorize effect
	/// </summary>
	public class ColorEffect : ConfigurableEffect
	{
		//renderer for this effect
		private class ColorRenderer : ConfigurableRenderer
		{
			private int _hue, _saturation, _bright;
			public ColorRenderer(IconImage target)
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
							lock_chunk[i] = Process(
								lock_original[i]);
						}
					}
				}
			}
			//process color
			private ColorBgra Process(ColorBgra value)
			{
				//convert to hsv, apply modifications and convert back
				HSV hsv = HSV.FromRGB(new RGB(value.ToArgb()));
				hsv.H = (hsv.H + (double)_hue / 360.0) % 1.0;
				hsv.S = hsv.S + (double)_saturation / 100.0;
				hsv.V = hsv.V + (double)_bright / 100.0;
				Color outcol = hsv.ToRGB().ToArgb();
				return new ColorBgra(value.A, outcol.R, outcol.G, outcol.B);
			}
			// create config controls for this renderer
			public override Control[] CreateConfig()
			{
				ValueChooser val_hue = new ValueChooser();
				val_hue.Text = Resources.ColorEffect_Hue;
				val_hue.Maximum = 360;
				val_hue.Minimum = 0;
				val_hue.DefaultValue = val_hue.Value =
					Settings.Default.ColorEffect_Hue;
				_hue = val_hue.DefaultValue;
				val_hue.ValueChanged += new System.EventHandler(hue_ValueChanged);
				//
				ValueChooser val_saturation = new ValueChooser();
				val_saturation.Text = Resources.ColorEffect_Saturation;
				val_saturation.Maximum = 100;
				val_saturation.Minimum = -100;
				val_saturation.DefaultValue = val_saturation.Value =
					Settings.Default.ColorEffect_Saturation;
				_saturation = val_saturation.DefaultValue;
				val_saturation.ValueChanged += new EventHandler(saturation_ValueChanged);
				//
				ValueChooser val_bright = new ValueChooser();
				val_bright.Text = Resources.ColorEffect_Brightness;
				val_bright.Maximum = 100;
				val_bright.Minimum = -100;
				val_bright.DefaultValue = val_bright.Value =
					Settings.Default.ColorEffect_Brightness;
				_bright = val_bright.DefaultValue;
				val_bright.ValueChanged += new System.EventHandler(bright_ValueChanged);
				//
				return new Control[] { val_hue, val_saturation, val_bright };
			}
			//update rendering param and setting
			void hue_ValueChanged(object sender, EventArgs e)
			{
				Cancel();
				Settings.Default.ColorEffect_Hue =
					_hue = ((ValueChooser)sender).Value;
				Start();
			}
			//update rendering param and setting
			void saturation_ValueChanged(object sender, EventArgs e)
			{
				Cancel();
				Settings.Default.ColorEffect_Saturation =
					_saturation = ((ValueChooser)sender).Value;
				Start();
			}
			//update rendering param and setting
			void bright_ValueChanged(object sender, EventArgs e)
			{
				Cancel();
				Settings.Default.ColorEffect_Brightness =
					_bright = ((ValueChooser)sender).Value;
				Start();
			}
		}
		//ctor
		public ColorEffect()
		{
			this.Image = Resources.ef_replacecol;
			this.Name = Resources.ColorEffect_Name;
		}
		//create colorrenderer
		protected override ConfigurableRenderer CreateRenderer(IconImage target)
		{
			return new ColorRenderer(target);
		}
	}
}
