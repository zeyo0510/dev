using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ControlsEx;
using DrawingEx.IconEncoder;
using VariIconsReload.BrushModel;

namespace VariIconsReload.Tools
{
	/// <summary>
	/// point stack with helper functions
	/// </summary>
	public class PointList : Stack<Point>
	{
		private Rectangle _boundscache = Rectangle.Empty;
		public new void Push(Point pt)
		{
			_boundscache = Rectangle.Union(GetBounds(),
				new Rectangle(pt, Size.Empty));
			base.Push(pt);
		}
		public new Point Pop()
		{
			_boundscache = Rectangle.Empty;
			return base.Pop();
		}
		public new void Clear()
		{
			_boundscache = Rectangle.Empty;
			base.Clear();
		}
		/// <summary>
		/// gets the bounds around all point in the collection
		/// </summary>
		/// <returns></returns>
		public Rectangle GetBounds()
		{
			if (_boundscache == Rectangle.Empty)
			{
				int maxx = int.MinValue,
					minx = int.MaxValue,
					maxy = int.MinValue,
					miny = int.MaxValue;
				foreach (Point p in this)
				{
					if (p.X > maxx) maxx = p.X;
					if (p.Y > maxy) maxy = p.Y;
					if (p.X < minx) minx = p.X;
					if (p.Y < miny) miny = p.Y;
				}
				if (maxx >= minx && maxy >= miny)
					_boundscache = new Rectangle(minx, miny,
						maxx - minx, maxy - miny);
			}
			return _boundscache;
		}

	}
	/// <summary>
	/// brush tool drawing a closed freehand line
	/// </summary>
	public class BrushTool : PencilToolBase
	{
		#region variables
		private PointList _points;
		private BufferLayer _buffer;
		#endregion
		public BrushTool()
		{
			_points = new PointList();
			Layers.Add(_buffer = new BufferLayer());
		}
		#region controller
		//get squared distance
		private int DistanceSq(Point a, Point b)
		{
			int deltaX = a.X - b.X, deltaY = a.Y - b.Y;
			return deltaX * deltaX + deltaY * deltaY;
		}
		//initialize buffer
		protected override void OnContentChange(DrawingEx.IconEncoder.IconImage value)
		{
			_buffer.Initialize(value);
		}
		//add to point list and redraw
		protected override void Draw(MouseButtons btn, Rectangle tip)
		{
			if (ContentGraphics == null || !_buffer.Initialized)
				return;
			Point pt = new Point(tip.X + tip.Width / 2, tip.Y + tip.Height / 2);
			if (_points.Count > 0 && DistanceSq(pt, _points.Peek()) < 4)
				return;
			//add and draw
			_points.Push(pt);
			_buffer.Clear();
			if (_points.Count < 2)
				return;
			using (Pen pn = new Pen(GetBrush(btn, _points.GetBounds()), Width))
			{
				if (TipShape == TipShape.Square)
					pn.SetLineCap(LineCap.Square, LineCap.Square, DashCap.Flat);
				else
					pn.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Flat);
				//
				using (PaintEventArgs pevent = _buffer.CreateArgs())
				{
					pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
					pevent.Graphics.DrawCurve(pn, _points.ToArray());
				}
			}
			//refresh
			Pen.InvalidateContent(new Rectangle(Point.Empty, _buffer.Image.Size));
		}
		//clear buffers and commit
		protected override void EndDraw()
		{
			if (_buffer.Initialized)
			{
				ContentGraphics.DrawImageUnscaled(_buffer.Image, Point.Empty);
				_buffer.Clear();
				_points.Clear();
			}
			base.EndDraw();
		}
		#endregion
	}
	/// <summary>
	/// base class for all tools drawing shapes that
	/// are defined through a list of points
	/// </summary>
	public abstract class PointListTool : Tool
	{
		#region types
		/// <summary>
		/// layer for managing a pointlist
		/// </summary>
		private class PointListLayer : CrossLayer
		{
			/// <summary>
			/// filter for registering clicks outside the layercontrol
			/// and resetting the
			/// </summary>
			private class MouseFilter : IMessageFilter
			{
				private PointListLayer _owner;
				public MouseFilter(PointListLayer owner)
				{
					if (owner == null)
						throw new ArgumentNullException("owner");
					_owner = owner;
				}
				//finish tool if mouse is not inside
				public bool PreFilterMessage(ref Message m)
				{
					switch (m.Msg)
					{
						case Win32.WM_LBUTTONDOWN:
						case Win32.WM_MBUTTONDOWN:
						case Win32.WM_RBUTTONDOWN:
							if (!_owner.Inside)
								_owner.Finish();
							break;
					}
					return false;
				}
			}
			#region variables
			private PointListTool _tool;
			private PointList _points;
			//
			private MouseButtons _btn;
			private MouseFilter _filter;
			#endregion
			public PointListLayer(PointListTool tool)
			{
				if (tool == null)
					throw new ArgumentNullException("tool");
				_tool = tool;
				_points = new PointList();
				_filter = new MouseFilter(this);
			}
			#region controller
			//draw point list and cross
			protected internal override void OnPaint(PaintEventArgs e)
			{
				base.OnPaint(e);
				// draw point list
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				float rad = Owner.RealZoom.Unscale(3f);
				using (SolidBrush brs = new SolidBrush(Color.FromArgb(200, 255, 255, 255)))
				{
					DrawPoints(e, brs, rad);
					brs.Color = Color.Black;
					rad /= 2f;
					DrawPoints(e, brs, rad);
				}
			}
			//circle for each point
			private void DrawPoints(PaintEventArgs e, Brush brs, float rad)
			{
				foreach (Point pt in _points)
					e.Graphics.FillEllipse(brs, pt.X + 0.5f - rad, pt.Y + 0.5f - rad, 2f * rad, 2f * rad);
			}
			//add location once or twice and store mousebutton
			protected override void OnMouseDown(object sender, MouseEventArgs e)
			{
				_points.Push(Location);
				if (_points.Count < 2)
				{
					_points.Push(Location);
					Application.AddMessageFilter(_filter);
				}
				_btn = e.Button;
			}
			//move last point and redraw
			protected override void OnMouseMove(object sender, MouseEventArgs e)
			{
				if (!ApplyCursorPosition(e))
					return;
				//move last point and redraw
				if (_points.Count > 0)
				{
					InvalidateContent(
						_tool.GetDrawBounds(_points));
					//
					_points.Pop();
					_points.Push(Location);
					_tool.Draw(_btn, _points);
					//refresh
					InvalidateContent(
						_tool.GetDrawBounds(_points));
					//update status
					_tool.RaiseToolStatusChanged(
						_points.GetBounds());
				}
				else
					_tool.RaiseToolStatusChanged(
						new Rectangle(Location, new Size(1, 1)));
				Owner.Update();

			}
			//remove the last 2 points (added at double mousedown) and commit
			protected override void OnDoubleClick(object sender, EventArgs e)
			{
				if (_points.Count >= 4)
				{
					_points.Pop();
					_points.Pop();
				}
				Finish();
			}
			//finish shape on enter key
			protected override void OnKeyDown(object sender, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Return)
				{
					e.Handled = true;
					Finish();
				}
				base.OnKeyDown(sender, e);
			}
			//if already designing a shape, refresh (width may have changed)
			protected override void OnMouseEnter(object sender, EventArgs e)
			{
				base.OnMouseEnter(sender, e);
				if (_points.Count > 0)
					Owner.Invalidate();
			}
			//reset status
			protected override void OnMouseLeave(object sender, EventArgs e)
			{
				base.OnMouseLeave(sender, e);
				_tool.RaiseToolStatusChanged();
			}
			//draw and reset
			public void Finish()
			{
				if (_points.Count > 2)
				{
					_tool.Draw(_btn, _points);
					Reset();
					_tool.EndDraw();
				}
				else
				{
					Reset();
					Owner.Refresh();
				}
			}
			//reset point list
			public void Reset()
			{
				Application.RemoveMessageFilter(_filter);
				_points.Clear();
				_tool.RaiseToolStatusChanged();
			}
			#endregion
		}
		#endregion
		#region variables
		private BufferLayer _buffer;
		private PointListLayer _points;
		//
		private AntiAliasOption _antialias;
		private BrushManager _brushes;
		#endregion
		#region ctor
		public PointListTool()
		{
			Layers.Add(_buffer = new BufferLayer());
			Layers.Add(_points = new PointListLayer(this));
			//
			ConfigOptions.Add(_antialias = new AntiAliasOption());
			_brushes = BrushManager.RegisterInstance(this);
		}
		public override void Dispose()
		{
			BrushManager.UnregisterInstance(this);
			_brushes = null;
			base.Dispose();
		}
		#endregion
		#region controller
		//create a bitmap buffer that is the same size then content
		protected override void OnContentChange(IconImage value)
		{
			_buffer.Initialize(value);
			//reset points to avoid unfinished shapes
			_points.Reset();
			if (_points.Owner != null)
				_points.Owner.Invalidate();
		}
		//convert the box coordinates to sorted draw box
		protected virtual Rectangle GetDrawBounds(PointList points)
		{
			Rectangle bounds = points.GetBounds();
			bounds.Width++; bounds.Height++;
			return bounds;
		}
		//readraw buffer contents
		private void Draw(MouseButtons btn, PointList points)
		{
			if (_buffer.Initialized)
			{
				using (PaintEventArgs pevent = _buffer.CreateArgs())
				{
					pevent.Graphics.Clear(Color.Transparent);
					if (_antialias.Value)
						pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
					if (points.Count > 1)
						OnDraw(pevent, btn, points);
				}
			}
		}
		//copy to image and commit
		private void EndDraw()
		{
			if (_buffer.Initialized)
			{
				using (Graphics gr = Graphics.FromImage(Content.Bitmap))
					gr.DrawImageUnscaled(_buffer.Image, Point.Empty);
				_buffer.Clear();
				//
				RaiseActionCommitted();
			}
		}
		#endregion
		#region rendering
		//get a brush with the given bounds
		protected Brush GetBrush(MouseButtons btn, PointList points)
		{
			Rectangle bounds = GetDrawBounds(points);
			if (btn == MouseButtons.Right)
				return _brushes.BrushB.GetOutBrush(bounds);
			return _brushes.BrushA.GetOutBrush(bounds);
		}
		protected virtual void OnDraw(PaintEventArgs e, MouseButtons btn, PointList points)
		{
		}
		#endregion
	}
	/// <summary>
	/// abstract class for a pointlist outline tool
	/// measuring is done with a grphics path
	/// </summary>
	public abstract class DrawPointListTool : PointListTool
	{
		private WidthOption _width;
		public DrawPointListTool()
		{
			ConfigOptions.Add(_width = new WidthOption());
		}
		//measure with pen width
		protected override Rectangle GetDrawBounds(PointList points)
		{
			using (GraphicsPath pth = new GraphicsPath())
			{
				AddMeasureFigure(pth, points);
				using (Matrix mat = new Matrix())
				{
					using (Pen pn = new Pen(Color.Black, Width))
					{
						pn.MiterLimit = Width / 2f;
						return Rectangle.Round(RectangleF.Inflate(
							pth.GetBounds(mat, pn), Width / 2f, Width / 2f));
					}
				}
			}
		}
		//override to add figure that is to be measured
		protected abstract void AddMeasureFigure(GraphicsPath pth, PointList points);
		protected int Width
		{
			get { return _width.Value; }
		}
	}
	/// <summary>
	/// draws a bicubic spline
	/// </summary>
	public class DrawSplineTool : DrawPointListTool
	{
		#region variables
		private TensionOption _tension;
		#endregion
		public DrawSplineTool()
		{
			ConfigOptions.Add(_tension = new TensionOption());
		}
		protected override void AddMeasureFigure(GraphicsPath pth, PointList points)
		{
			if (_tension.Value == 0)
				pth.AddLines(points.ToArray());
			else
				pth.AddCurve(points.ToArray(), _tension.Value / 100f);
		}
		//draw spline
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, PointList points)
		{
			using (Pen pn = new Pen(GetBrush(btn, points), Width))
			{
				pn.MiterLimit = Width / 2f;
				if (_tension.Value == 0)
					e.Graphics.DrawLines(pn, points.ToArray());
				else
					e.Graphics.DrawCurve(pn, points.ToArray(), _tension.Value / 100f);
			}
		}
	}
	/// <summary>
	/// tool that fills a polygon
	/// </summary>
	public class FillPolygonTool : PointListTool
	{
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, PointList points)
		{
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
			e.Graphics.FillPolygon(
				GetBrush(btn, points), points.ToArray());
		}
	}
	/// <summary>
	/// tool that draws a polygon
	/// </summary>
	public class DrawPolygonTool : DrawPointListTool
	{
		protected override void AddMeasureFigure(GraphicsPath pth, PointList points)
		{
			if (points.Count > 2)
				pth.AddPolygon(points.ToArray());
			else
				pth.AddLines(points.ToArray());
		}
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, PointList points)
		{
			using (Pen pn = new Pen(GetBrush(btn, points), Width))
			{
				pn.MiterLimit = Width / 2f;
				e.Graphics.DrawPolygon(pn, points.ToArray());
			}
		}
	}
}
