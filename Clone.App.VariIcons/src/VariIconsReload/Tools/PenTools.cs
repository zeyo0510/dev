using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DrawingEx.ColorManagement.ColorModels;
using DrawingEx.Imaging;
using VariIconsReload.BrushModel;
using VariIconsReload.Components;

namespace VariIconsReload.Tools
{
	/// <summary>
	/// base class for all tools wich draw step by step
	/// </summary>
	public abstract class PenTool : Tool
	{
		/// <summary>
		/// pen layer that displays adornments and propagates paint events
		/// </summary>
		protected class PenLayer : NavigatorLayer
		{
			#region variables
			private Rectangle _tip = new Rectangle(0, 0, 1, 1);
			private TipShape _tipshape = TipShape.Square;
			private bool _inside;
			private PenTool _tool;
			#endregion
			public PenLayer(PenTool owner)
			{
				if (owner == null)
					throw new ArgumentNullException("owner");
				_tool = owner;
				Order = 100;
			}
			#region controller
			private void PaintTip(Graphics gr, Pen pn)
			{
				if (_tipshape == TipShape.Square)
					gr.DrawRectangle(pn, _tip);
				else gr.DrawEllipse(pn, _tip);
			}
			protected internal override void OnPaint(PaintEventArgs e)
			{
				if (!_inside)
					return;
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				float lwidth = Owner.RealZoom.Unscale(1f);
				using (Pen pn = new Pen(Color.FromArgb(200, 255, 255, 255), lwidth * 3f))
				{
					PaintTip(e.Graphics, pn);
					//inner black line
					pn.Width = lwidth;
					pn.Color = Color.Black;
					PaintTip(e.Graphics, pn);
				}
			}
			protected override void OnMouseDown(object sender, MouseEventArgs e)
			{
				Owner.Cursor = Cursors.Cross;
				//draw starting point
				_tool.BeginDraw();
				_tool.Draw(e.Button, _tip);
				InvalidateContent(_tip);
				Owner.Update();
			}
			protected override void OnMouseMove(object sender, MouseEventArgs e)
			{
				Point newpos = Floor(PointF.Subtract(
					Owner.ClientToContent(e.Location),
					new SizeF((float)(_tip.Width - 1) / 2f, (float)(_tip.Height - 1) / 2f)));
				if (newpos == _tip.Location)
					return;
				InvalidateContent(_tip);
				_tip.Location = newpos;
				//update marks
				if (e.Button != MouseButtons.None)
					_tool.Draw(e.Button, _tip);
				//draw new position
				_tool.RaiseToolStatusChanged(_tip);
				InvalidateContent(_tip);
				Owner.Update();
			}
			protected override void OnMouseUp(object sender, MouseEventArgs e)
			{
				Owner.Cursor = Cursors.Default;
				_tool.EndDraw();
			}
			protected override void OnMouseLeave(object sender, EventArgs e)
			{
				_inside = false;
				_tool.RaiseToolStatusChanged();
				InvalidateContent(_tip);
				Owner.Update();
			}
			protected override void OnMouseEnter(object sender, EventArgs e)
			{
				_inside = true;
				_tip.Location = new Point(short.MaxValue, short.MaxValue);
			}
			#endregion
			public Size TipSize
			{
				get { return _tip.Size; }
				set { _tip.Size = value; }
			}
			public TipShape TipShape
			{
				get { return _tipshape; }
				set { _tipshape = value; }
			}
		}
		#region variables
		private PenLayer _pen;
		private WidthOption _width;
		private TipShapeOption _tipshape;
		#endregion
		public PenTool()
		{
			Layers.Add(_pen = new PenLayer(this));
			//
			ConfigOptions.Add(_width = new WidthOption());
			_width.Changed += new EventHandler(Width_Changed);
			//
			ConfigOptions.Add(_tipshape = new TipShapeOption());
			_tipshape.Changed += new EventHandler(TipShape_Changed);
		}
		#region controller
		//update box size
		protected virtual void Width_Changed(object sender, EventArgs e)
		{
			_pen.TipSize = new Size(_width.Value, _width.Value);
		}
		//update box shape
		protected virtual void TipShape_Changed(object sender, EventArgs e)
		{
			_pen.TipShape = _tipshape.Value;
		}
		//store a graphics object
		protected virtual void BeginDraw() { }
		//draw now
		protected virtual void Draw(MouseButtons btn, Rectangle tip) { }
		//commit actions
		protected virtual void EndDraw() { }
		//for invalidating and such
		protected PenLayer Pen
		{
			get { return _pen; }
		}
		#endregion
		#region properties
		protected TipShape TipShape
		{
			get { return _tipshape.Value; }
		}
		protected int Width
		{
			get { return _width.Value; }
		}
		#endregion
	}
	/// <summary>
	/// base class for pencil tools
	/// </summary>
	public abstract class PencilToolBase : PenTool
	{
		#region variables
		private BrushManager _brushes;
		//
		private Graphics _gr;
		#endregion
		public PencilToolBase()
		{
			_brushes = BrushManager.RegisterInstance(this);
		}
		public override void Dispose()
		{
			BrushManager.UnregisterInstance(this);
			_brushes = null;
			if (_gr != null)
			{
				_gr.Dispose();
				_gr = null;
			}
			base.Dispose();
		}
		#region controller
		//store a graphics object
		protected override void BeginDraw()
		{
			if (Content == null)
				return;
			_gr = Graphics.FromImage(Content.Bitmap);
		}
		//commit actions
		protected override void EndDraw()
		{
			if (_gr != null)
			{
				_gr.Dispose();
				_gr = null;
				RaiseActionCommitted();
			}
		}
		//for drawing
		protected Graphics ContentGraphics
		{
			get { return _gr; }
		}
		protected BrushManager Brushes
		{
			get { return _brushes; }
		}
		protected virtual Brush GetBrush(MouseButtons btn, Rectangle bounds)
		{
			if (btn == MouseButtons.Right)
				return _brushes.BrushB.GetOutBrush(bounds);
			return _brushes.BrushA.GetOutBrush(bounds);
		}
		#endregion
	}
	/// <summary>
	/// pencil tool for drawing with foreground or background
	/// brush.
	/// - line gets scattered if mouse is moved to quick
	/// - gradient is applied to box rather than to the line
	/// - single point painting is possible
	/// </summary>
	public class PencilTool : PencilToolBase
	{
		protected override void Draw(MouseButtons btn, Rectangle tip)
		{
			if (ContentGraphics == null)
				return;
			ContentGraphics.SmoothingMode = SmoothingMode.AntiAlias;
			ContentGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			//select brush by mouse button
			Brush brs;
			if (btn == MouseButtons.Right)
				brs = Brushes.BrushB.GetOutBrush(tip);
			else
				brs = Brushes.BrushA.GetOutBrush(tip);
			//draw box shape
			if (this.TipShape == TipShape.Circle)
				ContentGraphics.FillEllipse(brs, tip);
			else if (this.TipShape == TipShape.Square)
				ContentGraphics.FillRectangle(brs, tip);
		}
	}
	/// <summary>
	/// eraser tool for single point or scattered line erasing.
	/// </summary>
	public class EraserTool : PenTool
	{
		//blend for pixel operation
		private class EraseBlend : BlendMode
		{
			public override ColorBgra GetColor(ColorBgra target, ColorBgra mask)
			{
				target.A = Math.Min((byte)(255 - mask.A), target.A);
				return target;
			}
		}
		#region variables
		private StrengthOption _strength;
		private Bitmap _mask;
		#endregion
		public EraserTool()
		{
			ConfigOptions.Add(_strength = new StrengthOption());
		}
		public override void Dispose()
		{
			if (_mask != null)
			{
				_mask.Dispose();
				_mask = null;
			}
			base.Dispose();
		}
		#region controller
		//create brush box mask
		protected override void BeginDraw()
		{
			_mask = new Bitmap(Width, Width);
			Rectangle bounds = new Rectangle(0, 0, Width, Width);
			using (Graphics gr = Graphics.FromImage(_mask))
			{
				gr.SmoothingMode = SmoothingMode.AntiAlias;
				gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
				//setup brush
				GraphicsPath pth = new GraphicsPath();
				if (TipShape == TipShape.Circle)
					pth.AddEllipse(bounds);
				else
					pth.AddRectangle(bounds);
				//setup gradient
				using (PathGradientBrush pbrs = new PathGradientBrush(pth))
				{
					pbrs.CenterColor = Color.White;
					pbrs.SurroundColors = new Color[] { Color.Transparent };
					//strength value
					pbrs.FocusScales = new PointF(
						(float)_strength.Value / 100f,
						(float)_strength.Value / 100f);
					//draw
					if (TipShape == TipShape.Circle)
						gr.FillEllipse(pbrs, bounds);
					else
						gr.FillRectangle(pbrs, bounds);
				}
			}
		}
		//erase content with brush box
		protected override void Draw(MouseButtons btn, Rectangle tip)
		{
			if (_mask == null || Content == null)
				return;
			PixelOperations.ApplyMask<EraseBlend>(
				Content.Bitmap, _mask, tip.Location);
		}
		//dispose brush box
		protected override void EndDraw()
		{
			if (_mask != null)
			{
				_mask.Dispose();
				_mask = null;
				RaiseActionCommitted();
			}
		}
		#endregion
	}
	/// <summary>
	/// air brush tool drawing scatters of lines
	/// </summary>
	public class AirBrushTool : PencilToolBase
	{
		#region variables
		private Timer _timer;
		private Random _rnd;
		//
		private Rectangle _tip;
		private Brush _brs;
		#endregion
		public AirBrushTool()
		{
			_timer = new Timer();
			_timer.Interval = 40;
			_timer.Tick += new EventHandler(Timer_Tick);
			_rnd = new Random();
		}
		public override void Dispose()
		{
			if (_timer != null)
			{
				_timer.Dispose();
				_timer = null;
			}
			base.Dispose();
		}
		#region controller
		//draw line scats
		void Timer_Tick(object sender, EventArgs e)
		{
			if (ContentGraphics == null || _brs == null)
			{
				_timer.Stop();
				return;
			}
			for (int i = 0; i < _timer.Interval / 5; i++)
			{
				switch (this.TipShape)
				{
					case TipShape.Square:
						int w = Math.Max(0, _tip.Width - 1);
						ContentGraphics.FillRectangle(_brs,
							_tip.X + (float)(_rnd.NextDouble() * w),
							_tip.Y + (float)(_rnd.NextDouble() * w),
							1f, 1f);
						break;
					default:
						double rad = _tip.Width / 2.0,
							rand = _rnd.NextDouble() * Math.Max(0, rad - 1.0),
							angle = _rnd.NextDouble() * 2.0 * Math.PI;
						ContentGraphics.FillRectangle(_brs,
							_tip.X + (float)(rad + Math.Sin(angle) * rand - 0.5),
							_tip.Y + (float)(rad + Math.Cos(angle) * rand - 0.5),
							1f, 1f);
						break;
				}
			}
			Pen.InvalidateContent(_tip);
		}
		//start timer
		protected override void BeginDraw()
		{
			base.BeginDraw();
			ContentGraphics.SmoothingMode = SmoothingMode.AntiAlias;
			ContentGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			_timer.Start();
		}
		//update tip and brush
		protected override void Draw(MouseButtons btn, Rectangle tip)
		{
			_tip = tip;
			if (btn == MouseButtons.Left)
				_brs = Brushes.BrushA.GetOutBrush(tip);
			else
				_brs = Brushes.BrushB.GetOutBrush(tip);
		}
		//stop timer and commit
		protected override void EndDraw()
		{
			_timer.Stop();
			_brs = null;
			base.EndDraw();
		}
		#endregion
	}
}
