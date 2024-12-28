using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DrawingEx.IconEncoder;
using VariIconsReload.Properties;

namespace VariIconsReload.Effects
{
	/// <summary>
	/// effect that rotates free in one degree steps
	/// </summary>
	public class RotateFreeEffect : ConfigurableEffect
	{
		//renderer class
		private class RotateFreeRenderer : ConfigurableRenderer
		{
			private int _angle;
			public RotateFreeRenderer(IconImage target)
				: base(target, target.Bitmap.Height) { }
			//render the whole image
			protected override void RenderChunk(Bitmap chunk, Point location)
			{
				using (Graphics gr = Graphics.FromImage(chunk))
				{
					gr.Clear(Color.Transparent);
					using (Matrix mat = new Matrix())
					{
						mat.RotateAt(_angle, new Point(chunk.Width / 2, chunk.Height / 2));
						gr.Transform = mat;
						gr.DrawImageUnscaled(Original, Point.Empty);
					}
				}
			}
			// create config controls for this renderer
			public override Control[] CreateConfig()
			{
				ValueChooser val_angle = new ValueChooser();
				val_angle.Text = Resources.RotateFreeEffect_Angle;
				val_angle.Maximum = 360;
				val_angle.Minimum = 0;
				val_angle.DefaultValue = val_angle.Value =
					Settings.Default.RotateFreeEffect_Angle;
				_angle = val_angle.DefaultValue;
				val_angle.ValueChanged += new System.EventHandler(angle_ValueChanged);
				return new Control[] { val_angle };
			}
			//update rendering param and setting
			void angle_ValueChanged(object sender, EventArgs e)
			{
				Cancel();
				Settings.Default.RotateFreeEffect_Angle =
					_angle = ((ValueChooser)sender).Value;
				Start();
			}
		}
		//ctor
		public RotateFreeEffect()
		{
			this.Image = Resources.ef_rotatefree;
			this.Name = Resources.RotateFreeEffect_Name;
		}
		//creating the renderer
		protected override ConfigurableRenderer CreateRenderer(IconImage target)
		{
			return new RotateFreeRenderer(target);
		}
	}
}
