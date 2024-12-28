using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ControlsEx;
using DrawingEx.ColorManagement.Gradients;
using DrawingEx.Drawing3D;

namespace VariIconsReload.BrushModel
{
	/// <summary>
	/// base class for different styled brushes
	/// </summary>
	public abstract class WrapperBrush : IDisposable
	{
		public virtual void Dispose() { }
		/// <summary>
		/// override this to apply changes in offset and rotation
		/// </summary>
		protected virtual void OnUpdate()
		{
			if (Update != null)
				Update(this, EventArgs.Empty);
		}
		/// <summary>
		/// gets the gdi+ brush to fill an area with the given box
		/// </summary>
		public abstract Brush GetOutBrush(Rectangle bounds);
		#region events
		public event EventHandler Update;
		#endregion
	}
	/// <summary>
	/// encapsulates a solid color brush
	/// </summary>
	public class SolidWrapperBrush : WrapperBrush
	{
		private Color _color;
		private SolidBrush _cache;

		public SolidWrapperBrush() : this(Color.Black) { }
		public SolidWrapperBrush(Color color)
		{
			_color = color;
		}
		public override void Dispose()
		{
			if (_cache != null)
			{
				_cache.Dispose();
				_cache = null;
			}
		}
		/// <summary>
		/// gets the out brush. do not dispose the value returned by this function!
		/// </summary>
		public override Brush GetOutBrush(Rectangle bounds)
		{
			if (_cache == null)
				_cache = new SolidBrush(_color);
			return _cache;
		}

		/// <summary>
		/// gets or sets the color of the brush
		/// </summary>
		public Color Color
		{
			get { return _color; }
			set
			{
				if (value == _color)
					return;
				_color = value;
				if (_cache != null)
					_cache.Color = value;
				OnUpdate();
			}
		}
	}
	/// <summary>
	/// encapsulates a gradient brush
	/// </summary>
	public abstract class GradientWrapperBrush : WrapperBrush
	{
		protected const int Resolution = 600;
		protected const float SQRT2 = 1.414f;
		#region variables
		private Gradient _gradient, _gradientcpy;
		private Matrix _transform, _outtransform;
		private ScaleFactor _scale;
		private Angle _angle;
		private PointF _offset;
		#endregion
		#region ctor
		public GradientWrapperBrush(Gradient gradient) :
			this(gradient, ScaleFactor.Identity, Angle.Empty, Point.Empty) { }

		public GradientWrapperBrush(Gradient gradient, ScaleFactor scale, Angle angle, PointF offset)
		{
			if (gradient == null)
				throw new ArgumentNullException("Gradient");
			if (offset.X < -0.5f || offset.X > 0.5f ||
					offset.Y < -0.5f || offset.Y > 0.5f)
				throw new ArgumentOutOfRangeException("offset");
			if (_scale.ToDouble() < 0.1 || _scale.ToDouble() > 1.0)
				throw new ArgumentOutOfRangeException("scale");
			//
			_gradientcpy = new Gradient();
			_gradient = gradient;
			_gradient.Changed += new EventHandler(HandleGradientChanged);
			//
			_transform = new Matrix();
			_outtransform = new Matrix();
			_angle = angle;
			_scale = scale;
			_offset = offset;

			UpdateTransform();
			HandleGradientChanged(this, EventArgs.Empty);
		}
		public override void Dispose()
		{
			_gradient.Changed -= new EventHandler(HandleGradientChanged);
			if (_transform != null)
			{
				_transform.Dispose();
				_transform = null;
			}
			if (_outtransform != null)
			{
				_outtransform.Dispose();
				_outtransform = null;
			}
			base.Dispose();
		}
		#endregion
		#region controller
		//applies scale transform to gradient copy
		protected virtual void HandleGradientChanged(object sender, EventArgs e)
		{
			UpdateGradient(_scale, 0.5);
			OnUpdate();
		}
		/// <summary>
		/// updates the gradient with the given scale
		/// at the given scalepoint
		/// </summary>
		protected void UpdateGradient(ScaleFactor scale, double scalepoint)
		{
			if (!Gradient.isValid(scalepoint))
				scalepoint = 0.5;
			if (scale.ToDouble() > 1.0)
				scale = ScaleFactor.Identity;
			else if (scale.ToDouble() < 0.1)
				scale = ScaleFactor.FromDouble(0.1);
			//scale and offset alpha points
			_gradientcpy.Alphas.Clear();
			foreach (AlphaPoint ap in _gradient.Alphas)
				_gradientcpy.Alphas.Add(new AlphaPoint(ap.Alpha, ap.Focus,
					scalepoint + scale.Scale(ap.Position - scalepoint)));
			//scale and offset color points
			_gradientcpy.Colors.Clear();
			foreach (ColorPoint cp in _gradient.Colors)
				_gradientcpy.Colors.Add(new ColorPoint(cp.Color, cp.Focus,
					scalepoint + scale.Scale(cp.Position - scalepoint)));
		}
		/// <summary>
		/// applies offset and rotation parameters+
		/// to transform matrix
		/// </summary>
		protected void UpdateTransform()
		{
			PointF middle = new PointF(Resolution / 2, Resolution / 2);
			_transform.Reset();
			_transform.RotateAt((float)Angle.ToDegree(), middle, MatrixOrder.Append);
			_transform.Translate(_offset.X * middle.X / SQRT2 - middle.X,
				_offset.Y * middle.Y / SQRT2 - middle.Y, MatrixOrder.Append);
		}
		/// <summary>
		/// calculates scale and offset so the brush will fit the given
		/// box
		/// </summary>
		protected void UpdateOutTransform(Rectangle bounds, bool isometric)
		{
			_outtransform.Reset();
			if (bounds.Width > 0 && bounds.Height > 0)
			{
				if (isometric)
				{
					//scale width and hieght evenly
					float scale = 2f * (float)Math.Sqrt(bounds.Width * bounds.Width +
						bounds.Height * bounds.Height) / (float)Resolution;
					_outtransform.Scale(scale, scale, MatrixOrder.Append);
				}
				else
				{
					//scale width and height different
					_outtransform.Scale(2f * SQRT2 * (float)bounds.Width / (float)Resolution,
						2f * SQRT2 * (float)bounds.Height / (float)Resolution, MatrixOrder.Append);
				}
			}
			_outtransform.Translate(bounds.X + bounds.Width / 2,
				bounds.Y + bounds.Height / 2, MatrixOrder.Append);
		}
		/// <summary>
		/// gets the prescaled gradient for the gradient cache brushes
		/// </summary>
		protected Gradient GradientCopy
		{
			get { return _gradientcpy; }
		}
		/// <summary>
		/// gets the precalculated parameter transform
		/// </summary>
		protected Matrix Transform
		{
			get { return _transform; }
		}
		/// <summary>
		/// gets the precalculated transform for applying the
		/// brush to a bounding fill rectangle
		/// </summary>
		protected Matrix OutTransform
		{
			get { return _outtransform; }
		}
		#endregion
		#region properties
		/// <summary>
		/// gets or sets the brush gradient. this cannot be null
		/// </summary>
		public Gradient Gradient
		{
			get { return _gradient; }
			set
			{
				if (value == _gradient || value == null)
					return;
				_gradient.Changed -= new EventHandler(HandleGradientChanged);
				_gradient = value;
				_gradient.Changed += new EventHandler(HandleGradientChanged);
				HandleGradientChanged(this, EventArgs.Empty);
			}
		}
		/// <summary>
		/// scale affects the gradient
		/// valid range: 0.1 - 2.0
		/// </summary>
		public ScaleFactor Scale
		{
			get { return _scale; }
			set
			{
				value = ScaleFactor.FromDouble(
					Math.Max(0.1, Math.Min(2.0, value.ToDouble())));
				if (value == _scale)
					return;
				_scale = value;
				HandleGradientChanged(this, EventArgs.Empty);
			}
		}
		/// <summary>
		/// angle affects the master rotation
		/// valid range: 0 - 2 PI
		/// </summary>
		public Angle Angle
		{
			get { return _angle; }
			set
			{
				if (value == _angle)
					return;
				_angle = value;
				UpdateTransform();
				OnUpdate();
			}
		}
		/// <summary>
		/// offset affects the master transform
		/// valid range x/y : -0.5 - 0.5
		/// </summary>
		public PointF Offset
		{
			get { return _offset; }
			set
			{
				if (float.IsInfinity(value.X) || float.IsInfinity(value.X))
					value.X = 0;
				if (float.IsInfinity(value.Y) || float.IsInfinity(value.Y))
					value.Y = 0;
				value.X = Math.Max(-0.5f, Math.Min(0.5f, value.X));
				value.Y = Math.Max(-0.5f, Math.Min(0.5f, value.Y));
				if (value == _offset)
					return;
				_offset = value;
				UpdateTransform();
				OnUpdate();
			}
		}
		#endregion
	}
	/// <summary>
	/// encapsulates a linear gradient brush
	/// </summary>
	public class LinearGradientWrapperBrush : GradientWrapperBrush
	{
		#region variables
		private LinearGradientBrush _cache;
		#endregion

		public LinearGradientWrapperBrush(Gradient gradient, ScaleFactor scale, Angle angle, PointF offset)
			: base(gradient, scale, angle, offset) { }
		public LinearGradientWrapperBrush(Gradient gradient)
			: base(gradient) { }

		public override void Dispose()
		{
			if (_cache != null)
			{
				_cache.Dispose();
				_cache = null;
			}
			base.Dispose();
		}
		protected override void HandleGradientChanged(object sender, EventArgs e)
		{
			//gradient has 2*sqrt(2) spare space to prevent tiling
			UpdateGradient(ScaleFactor.FromDouble(Scale.ToDouble() / (2.0 * SQRT2)), 0.5);
			if (_cache != null)
				_cache.InterpolationColors = base.GradientCopy;
			OnUpdate();
		}

		public override Brush GetOutBrush(Rectangle bounds)
		{
			if (_cache == null)
			{
				_cache = new LinearGradientBrush(new Point(0, 0),
					new Point(Resolution, 0), Color.Black, Color.White);
				_cache.InterpolationColors = base.GradientCopy;
			}
			UpdateOutTransform(bounds, false);
			//
			_cache.ResetTransform();
			_cache.MultiplyTransform(Transform, MatrixOrder.Append);
			_cache.MultiplyTransform(OutTransform, MatrixOrder.Append);
			//
			return _cache;
		}
	}
	/// <summary>
	/// encapsulates a shaped gradient brush
	/// </summary>
	public abstract class PathGradientWrapperBrush : GradientWrapperBrush
	{
		#region variables
		private PathGradientBrush _cache;
		#endregion
		#region ctor
		public PathGradientWrapperBrush(Gradient gradient, ScaleFactor scale, Angle angle, PointF offset)
			: base(gradient, scale, angle, offset) { }
		public PathGradientWrapperBrush(Gradient gradient)
			: base(gradient) { }
		public override void Dispose()
		{
			if (_cache != null)
			{
				_cache.Dispose();
				_cache = null;
			}
			base.Dispose();
		}
		#endregion
		#region controller
		protected override void HandleGradientChanged(object sender, EventArgs e)
		{
			//gradient has 2*sqrt(2) spare space to prevent clipping
			UpdateGradient(ScaleFactor.FromDouble(Scale.ToDouble() / (2.0 * SQRT2)), 1.0);
			if (_cache != null)
				_cache.InterpolationColors = base.GradientCopy;
			OnUpdate();
		}
		protected abstract void AddFigures(GraphicsPath path, Rectangle bounds);
		public override Brush GetOutBrush(Rectangle bounds)
		{
			if (_cache == null)
			{
				//create new brush using temporary graphics path
				using (GraphicsPath path = new GraphicsPath())
				{
					AddFigures(path, new Rectangle(0, 0, Resolution, Resolution));
					_cache = new PathGradientBrush(path);
				}
				_cache.InterpolationColors = base.GradientCopy;
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
	/// encapsulates a path gradient brush in the form of a square
	/// </summary>
	public class DiamondGradientWrapperBrush : PathGradientWrapperBrush
	{

		#region ctor
		public DiamondGradientWrapperBrush(Gradient gradient, ScaleFactor scale, Angle angle, PointF offset)
			: base(gradient, scale, angle, offset) { }
		public DiamondGradientWrapperBrush(Gradient gradient)
			: base(gradient) { }
		#endregion
		protected override void AddFigures(GraphicsPath path, Rectangle bounds)
		{
			path.AddRectangle(bounds);
		}
	}
	/// <summary>
	/// encapsulates a path gradient brush in the form of a circle
	/// </summary>
	public class RadialGradientWrapperBrush : PathGradientWrapperBrush
	{
		#region ctor
		public RadialGradientWrapperBrush(Gradient gradient, ScaleFactor scale, Angle angle, PointF offset)
			: base(gradient, scale, angle, offset) { }
		public RadialGradientWrapperBrush(Gradient gradient)
			: base(gradient) { }
		#endregion
		protected override void AddFigures(GraphicsPath path, Rectangle bounds)
		{
			bounds.X -= 16; bounds.Y -= 8;
			bounds.Width += 16; bounds.Height += 16;
			path.AddEllipse(bounds);
		}
	}
}
