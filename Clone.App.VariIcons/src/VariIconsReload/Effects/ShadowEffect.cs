using System;
using System.Collections.Generic;
using System.Text;
using VariIconsReload.Properties;
using DrawingEx.IconEncoder;
using System.Windows.Forms;
using System.Drawing;

namespace VariIconsReload.Effects
{
	public class ShadowEffect:ConfigurableEffect
	{
		private class ShadowRenderer : ConfigurableRenderer
		{
			public ShadowRenderer(IconImage target)
				: base(target,4)
			{}
			protected override void RenderChunk(Bitmap chunk, Point location)
			{
				
			}
			public override Control[] CreateConfig()
			{
				return new Control[]{
					new ValueChooser(),
					new OffsetChooser(),
					new ColorChooser()
				};
			}
		}
		public ShadowEffect()
		{
			this.Image = Resources.ef_shadow;
		}
		protected override ConfigurableRenderer CreateRenderer(IconImage target)
		{
			return new ShadowRenderer(target);
		}
	}
}
