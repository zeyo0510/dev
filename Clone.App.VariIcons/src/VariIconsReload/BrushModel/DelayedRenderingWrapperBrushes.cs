using System;
using System.Drawing;
using ControlsEx;
using DrawingEx.ColorManagement.Gradients;
using DrawingEx.Drawing3D;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using DrawingEx.ColorManagement.ColorModels;
using System.Threading;
using VariIconsSDK.Model;

namespace VariIconsReload.BrushModel
{
	/// <summary>
	/// abstract base class for all brushes that apply exotic forms of gradient
	/// to a bitmap, where rendering is taking very long
	/// </summary>
	public abstract class TextureGradientWrapperBrush : GradientWrapperBrush
	{
		#region types
		/// <summary>
		/// abstract asynchronous renderer for a gradient buffer of the given size
		/// </summary>
		protected abstract class GradientRenderer : ImageRenderer
		{
			#region variables
			//worker thread side
			private Gradient _gradientcache;
			#endregion
			#region ctor
			public GradientRenderer(Size size)
				: base(new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb))
			{ }
			public override void Dispose()
			{
				base.Dispose();
				Buffer.Dispose();
			}
			#endregion
			#region controller
			//override to ensure execution enters only if there is a gradient
			protected override bool Run(bool argument)
			{
				if (_gradientcache == null)
					return false;
				return base.Run(argument);
			}
			//update gradient
			protected override void HandleCoreMessage(object message)
			{
				Gradient grdcache;
				if ((grdcache = message as Gradient) != null)
					_gradientcache = grdcache;
				else
					base.HandleCoreMessage(message);
			}
			/// <summary>
			/// gets the gradient copy belonging to the worker thread for processing
			/// </summary>
			protected Gradient Gradient
			{
				get { return _gradientcache; }
			}
			#endregion
			/// <summary>
			/// sets the current gradient on monitor side.
			/// if NULL is specified, this is skipped
			/// </summary>
			public void SetGradient(Gradient value)
			{
				if (value == null)
					return;
				PostToCore(value.Clone());
			}
		}
		#endregion
		#region variables
		private TextureBrush _cache;
		private GradientRenderer _renderer;
		#endregion
		#region ctor
		public TextureGradientWrapperBrush(Gradient gradient)
			: this(gradient, ScaleFactor.Identity, Angle.Empty, PointF.Empty) { }
		public TextureGradientWrapperBrush(Gradient gradient, ScaleFactor scale, Angle angle, PointF offset)
			: base(gradient, scale, angle, offset)
		{
			_renderer = CreateRenderer();
			_renderer.Completed += new EventHandler<CompletedEventArgs<bool>>(_renderer_Completed);
			HandleGradientChanged(this, EventArgs.Empty);
		}

		public override void Dispose()
		{
			if (_renderer != null)
			{
				_renderer.Dispose();
				_renderer = null;
			}
			if (_cache != null)
			{
				_cache.Dispose();
				_cache = null;
			}
			base.Dispose();
		}
		#endregion
		#region controller
		protected abstract GradientRenderer CreateRenderer();
		//called when rendering thread exits
		void _renderer_Completed(object sender, CompletedEventArgs<bool> e)
		{
			if (!e.Cancelled)
			{
				//rendering completed normally, update all
				if (_cache != null)
					_cache.Dispose();
				_cache = null;
				OnUpdate();
			}
			else
				//rendering was cancelled due to change, re-render
				_renderer.Start();
		}
		// called when gradient or scale changed
		protected override void HandleGradientChanged(object sender, EventArgs e)
		{
			//no gradient buffering needed
			if (_renderer != null)
			{
				_renderer.Cancel();
				_renderer.SetGradient(Gradient);
				//rendering may already be running, then staring has no effect
				_renderer.Start();
			}
		}
		/// <summary>
		/// gets the scaled and updated gdi brush
		/// </summary>
		public override Brush GetOutBrush(Rectangle bounds)
		{
			if (_cache == null)
			{
				_cache = new TextureBrush(_renderer.Buffer);
			}
			UpdateOutTransform(bounds, false);
			//apply all transforms
			_cache.ResetTransform();
			_cache.MultiplyTransform(Transform, MatrixOrder.Append);
			_cache.MultiplyTransform(OutTransform, MatrixOrder.Append);

			return _cache;
		}
		#endregion
	}
	/// <summary>
	/// gradient brush with an angular gradient pattern
	/// </summary>
	public class AngleGradientWrapperBrush : TextureGradientWrapperBrush
	{
		#region types
		//angular bitmap renderer
		private class AngleGradientRenderer : GradientRenderer
		{
			public AngleGradientRenderer()
				: base(new Size(Resolution, Resolution)) { }
			protected unsafe override void RenderChunk(Bitmap chunk, Point location)
			{
				//lock in memory for pixel operation
				BitmapData bd = chunk.LockBits(new Rectangle(0, 0, chunk.Width, chunk.Height),
					ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
				ColorBgra* data = (ColorBgra*)bd.Scan0;
				//
				for (int x = 0; x < bd.Width; x++)
				{
					//position should be within 0f-1f
					float pos = 0.5f + (float)(Math.Atan2(location.Y - Resolution / 2, x - Resolution / 2) / (2.0 * Math.PI));
					data[x] = ColorBgra.FromArgb(
						Gradient.GetColorAt(pos));
				}
				chunk.UnlockBits(bd);
			}
		}
		#endregion
		#region ctor
		public AngleGradientWrapperBrush(Gradient gradient)
			: this(gradient, ScaleFactor.Identity, Angle.Empty, PointF.Empty) { }
		public AngleGradientWrapperBrush(Gradient gradient, ScaleFactor scale, Angle angle, PointF offset)
			: base(gradient, scale, angle, offset)
		{
		}
		#endregion
		#region controller
		protected override TextureGradientWrapperBrush.GradientRenderer CreateRenderer()
		{
			return new AngleGradientRenderer();
		}
		#endregion
	}
}
