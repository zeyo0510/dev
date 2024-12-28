using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DrawingEx.IconEncoder;
using VariIconsReload.Properties;

namespace VariIconsReload.Effects
{
	/// <summary>
	/// simple offset effect, rendering is executed blocking,
	/// because its very fast
	/// </summary>
	public abstract class MoveEffect:Effect
	{
		private Point _offset;
		public MoveEffect(Point offset)
		{
			_offset = offset;
		}
		//blocking render function
		public override void Render(IconImage target,EventHandler<RenderProgressArgs> callback)
		{
			using (Graphics gr = Graphics.FromImage(target.Bitmap))
			{
				gr.CompositingMode = CompositingMode.SourceCopy;
				gr.DrawImageUnscaled(target.Bitmap, _offset);
				//transparent fill
				if (_offset.X != 0)
					gr.FillRectangle(Brushes.Transparent,
						new Rectangle(_offset.X > 0 ? 0 : target.Bitmap.Width + _offset.X,0,
							Math.Abs(_offset.X), target.Bitmap.Height));
				if (_offset.Y != 0)
					gr.FillRectangle(Brushes.Transparent,
						new Rectangle(0, _offset.Y > 0 ? 0 : target.Bitmap.Height + _offset.Y,
							target.Bitmap.Width, Math.Abs(_offset.Y)));
			}
			//raise action commited
			if (callback != null)
				callback(this, RenderProgressArgs.Commit);
		}
	}
	/// <summary>
	/// one pixel right
	/// </summary>
	public class MoveRightEffect : MoveEffect
	{
		public MoveRightEffect()
			: base(new Point(1, 0))
		{
			this.Image = Resources.movri;
		}
	}
	/// <summary>
	/// one pixel left
	/// </summary>
	public class MoveLeftEffect : MoveEffect
	{
		public MoveLeftEffect()
			: base(new Point(-1, 0))
		{
			this.Image = Resources.movle;
		}
	}
	/// <summary>
	/// one pixel up
	/// </summary>
	public class MoveUpEffect : MoveEffect
	{
		public MoveUpEffect()
			: base(new Point(0, -1))
		{
			this.Image = Resources.movup;
		}
	}
	/// <summary>
	/// one pixel down
	/// </summary>
	public class MoveDownEffect : MoveEffect
	{
		public MoveDownEffect()
			: base(new Point(0, 1))
		{
			this.Image = Resources.movdo;
		}
	}
}
