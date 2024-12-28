using System;
using DrawingEx.ColorManagement.ColorModels;
using DrawingEx.IconEncoder;
using DrawingEx.Imaging;
using VariIconsReload.Properties;

namespace VariIconsReload.Effects
{
	/// <summary>
	/// desaturation effect grayscales an image in one step
	/// </summary>
	public class DesaturateEffect : Effect
	{
		//ctor
		public DesaturateEffect()
		{
			this.Image = Resources.ef_contrast;
			this.Name = Resources.DesaturateEffect_Name;
		}
		//render blocking and in-place here
		public override void Render(IconImage target, EventHandler<RenderProgressArgs> callback)
		{
			using (ImageLock target_lock = new ImageLock(target.Bitmap))
			{
				for (int i = 0; i < target_lock.Size; i++)
				{
					ColorBgra value = target_lock[i];
					value.R = value.G = value.B =
						(byte)(((int)value.R + (int)value.G + (int)value.B) / 3);
					target_lock[i] = value;
				}
			}
			if (callback != null)
				callback(this, RenderProgressArgs.Commit);
		}
	}
}
