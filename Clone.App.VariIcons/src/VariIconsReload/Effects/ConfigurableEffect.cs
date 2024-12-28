using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DrawingEx.IconEncoder;
using VariIconsReload.BrushModel;
using VariIconsSDK.Model;

namespace VariIconsReload.Effects
{
	/// <summary>
	/// configurable effect with gui parameters and async rendering
	/// </summary>
	public abstract class ConfigurableEffect : Effect
	{
		private EventHandler<RenderProgressArgs> callback;
		private IconImage target;
		//show dialog and initialize renderer
		public override void Render(IconImage target, EventHandler<RenderProgressArgs> callback)
		{
			this.callback = callback;
			this.target = target;
			using (ConfigurableRenderer render = CreateRenderer(target))
			{
				render.Completed += new EventHandler<CompletedEventArgs<bool>>(render_Completed);
				//
				using (EffectParamsForm frm = new EffectParamsForm(render.CreateConfig()))
				{
					if (this.Name != null)
						frm.Text = this.Name;
					//
					render.Start();
					if (frm.ShowDialog() == DialogResult.OK)
					{
						//call a commit step
						if (callback != null)
							callback(this, RenderProgressArgs.Commit);
						target = null;
					}
					else
					{
						//copy cache back
						using (Graphics gr = Graphics.FromImage(target.Bitmap))
						{
							//stop renderer
							target = null;
							render.Cancel();
							//
							gr.CompositingMode = CompositingMode.SourceCopy;
							gr.DrawImageUnscaled(render.Original, Point.Empty);
						}
						if (callback != null)
							callback(this, RenderProgressArgs.Refresh);
					}
				}
			}
			callback = null;
		}
		//refresh panel
		void render_Completed(object sender, CompletedEventArgs<bool> e)
		{
			if (target == null || callback == null)
				return;
			ConfigurableRenderer render = (ConfigurableRenderer)sender;
			if (e.Cancelled)
				render.Start();
			else
				callback(this, RenderProgressArgs.Refresh);
		}
		//override this to create custom renderer
		protected abstract ConfigurableRenderer CreateRenderer(IconImage target);
	}
	/// <summary>
	/// configurable renderer which renders to a target
	/// </summary>
	public abstract class ConfigurableRenderer : ImageRenderer
	{
		private Bitmap _original;
		public ConfigurableRenderer(IconImage target, int chunksize)
			: base(target.Bitmap, chunksize)
		{
			_original = (Bitmap)target.Bitmap.Clone();
		}
		public override void Dispose()
		{
			base.Dispose();
			if (_original != null)
			{
				_original.Dispose();
				_original = null;
			}
		}
		public Bitmap Original
		{
			get { return _original; }
		}
		//override to create gui controls
		public abstract Control[] CreateConfig();
	}
}
