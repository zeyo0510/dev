using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DrawingEx.IconEncoder;
using VariIconsReload.Properties;

namespace VariIconsReload.Effects
{
	/// <summary>
	/// simple rotate or flip effect
	/// </summary>
	public abstract class RotateFlipEffect : Effect
	{
		private RotateFlipType _type;
		public RotateFlipEffect(RotateFlipType type)
		{
			_type = type;
		}
		public override void Render(IconImage target, EventHandler<RenderProgressArgs> callback)
		{
			target.Bitmap.RotateFlip(_type);
			if (callback != null)
				callback(this, RenderProgressArgs.Commit);
		}
	}
	/// <summary>
	/// rotate 90 degrees ccw
	/// </summary>
	public class RotateCCWEffect : RotateFlipEffect
	{
		public RotateCCWEffect()
			: base(RotateFlipType.Rotate270FlipNone)
		{
			this.Image = Resources.ef_rotleft;
		}
	}
	/// <summary>
	/// rotate 90 degrees cw
	/// </summary>
	public class RotateCWEffect : RotateFlipEffect
	{
		public RotateCWEffect()
			: base(RotateFlipType.Rotate90FlipNone)
		{
			this.Image = Resources.ef_rotright;
		}
	}
	/// <summary>
	/// flip horizontal
	/// </summary>
	public class FlipHEffect : RotateFlipEffect
	{
		public FlipHEffect()
			: base(RotateFlipType.RotateNoneFlipX)
		{
			this.Image = Resources.ef_fliphor;
		}
	}
	/// <summary>
	/// flip vertical
	/// </summary>
	public class FlipVEffect : RotateFlipEffect
	{
		public FlipVEffect()
			: base(RotateFlipType.RotateNoneFlipY)
		{
			this.Image = Resources.ef_flipver;
		}
	}
}
