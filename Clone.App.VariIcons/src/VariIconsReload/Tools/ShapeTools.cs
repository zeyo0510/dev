using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DrawingEx.IconEncoder;
using VariIconsReload.BrushModel;
using VariIconsReload.Components;
using ControlsEx;

namespace VariIconsReload.Tools
{
	/// <summary>
	/// abstract tool for tools that pre-render a shape
	/// and commit it on mouse up
	/// </summary>
	public abstract class ShapeTool : Tool
	{
		#region types
		/// <summary>
		/// layer for constructing a shape bounding box
		/// </summary>
		private class ConstructionLayer : CrossLayer
		{
			#region variables
			private ShapeTool _tool;
			//
			private Rectangle _box = new Rectangle(0, 0, 1, 1);
			#endregion
			public ConstructionLayer(ShapeTool tool)
			{
				if (tool == null)
					throw new ArgumentNullException("tool");
				_tool = tool;
			}
			private Rectangle GetBoxRect()
			{
				Rectangle rect = GraphicsEx.SortedRectangle(_box);
				rect.Width++; rect.Height++;
				return rect;
			}
			//start constructing the box and paint first version
			protected override void OnMouseDown(object sender, MouseEventArgs e)
			{
				Owner.Cursor = Cursors.SizeNWSE;
				_box = new Rectangle(Location, Size.Empty);
				//paint to buffer
				_tool.Draw(e.Button, _box);
				InvalidateCross();
				InvalidateContent(_tool.GetInvalidateBounds(GetBoxRect()));
				Owner.Update();
			}
			//scale bounding box or move cursor
			protected override void OnMouseMove(object sender, MouseEventArgs e)
			{
				if (!ApplyCursorPosition(e))
					return;
				if (e.Button != MouseButtons.None)
				{
					//draw new content version
					InvalidateContent(_tool.GetInvalidateBounds(GetBoxRect()));
					_box.Size = new Size(Location.X - _box.X, Location.Y - _box.Y);
					//paint to buffer
					_tool.Draw(e.Button, _box);
					InvalidateContent(_tool.GetInvalidateBounds(GetBoxRect()));
					_tool.RaiseToolStatusChanged(GetBoxRect());
				}
				else
				{
					_box.Location = Location;
					_tool.RaiseToolStatusChanged(new Rectangle(Location,
						new Size(1, 1)));
				}
				//display changes
				Owner.Update();

			}
			//commit the buffer to content and cleanup
			protected override void OnMouseUp(object sender, MouseEventArgs e)
			{
				Owner.Cursor = Cursors.Default;
				_tool.EndDraw();
				//
				_box.Location = Location;
				_box.Size = new Size(1, 1);
				_tool.RaiseToolStatusChanged(_box);
			}
			protected override void OnMouseLeave(object sender, EventArgs e)
			{
				base.OnMouseLeave(sender, e);
				_tool.RaiseToolStatusChanged();
			}
		}
		#endregion
		#region variables
		private BufferLayer _buffer;
		private ConstructionLayer _construct;
		//
		private AntiAliasOption _antialias;
		private BrushManager _brushes;
		#endregion
		#region ctor
		public ShapeTool()
		{
			Layers.Add(_buffer = new BufferLayer());
			Layers.Add(_construct = new ConstructionLayer(this));
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
		}
		//for padding on invalidate calls
		protected virtual RectangleF GetInvalidateBounds(RectangleF bounds)
		{
			return bounds;
		}
		private void Draw(MouseButtons btn, Rectangle box)
		{
			if (_buffer.Initialized)
			{
				using (PaintEventArgs pevent = _buffer.CreateArgs())
				{
					pevent.Graphics.Clear(Color.Transparent);
					if (_antialias.Value)
						pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
					if ((Control.ModifierKeys & Keys.Shift) != 0)
						box = GetConstrainedBox(box);
					OnDraw(pevent, btn, GetDrawBounds(box));
				}
			}
		}
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
		//restrict to square on shift press
		protected virtual Rectangle GetConstrainedBox(Rectangle box)
		{
			int min = Math.Min(Math.Abs(box.Height), Math.Abs(box.Width));
			box.Height = Math.Sign(box.Height) * min;
			box.Width = Math.Sign(box.Width) * min;
			return box;
		}
		//convert the box coordinates to sorted draw box
		protected virtual Rectangle GetDrawBounds(Rectangle box)
		{
			box = GraphicsEx.SortedRectangle(box);
			box.Width++; box.Height++;
			return box;
		}
		//get a brush with the given bounds
		protected virtual Brush GetBrush(MouseButtons btn, Rectangle bounds)
		{
			if (btn == MouseButtons.Right)
				return _brushes.BrushB.GetOutBrush(bounds);
			return _brushes.BrushA.GetOutBrush(bounds);
		}
		protected virtual void OnDraw(PaintEventArgs e, MouseButtons btn, Rectangle bounds)
		{
		}
		#endregion
	}
	/// <summary>
	/// layer for visual feedback on a tool
	/// </summary>
	public abstract class CrossLayer : NavigatorLayer
	{
		#region variables
		private Point _loc = Point.Empty;
		private bool _inside = false;
		#endregion
		public CrossLayer()
		{
			this.Order = 100;
		}
		#region controller
		//paints the adornment cross
		private void DrawLineCross(Graphics gr, bool pressed, Point pos, Size sz)
		{
			using (Brush hs = new SolidBrush(pressed ?
				Color.FromArgb(127, 255, 0, 0) : Color.FromArgb(127, 0, 0, 255)))
			{
				gr.FillRectangle(hs, 0, pos.Y, pos.X, 1);
				gr.FillRectangle(hs, pos.X + 1, pos.Y, sz.Width - pos.X - 1, 1);
				gr.FillRectangle(hs, pos.X, 0, 1, pos.Y);
				gr.FillRectangle(hs, pos.X, pos.Y + 1, 1, sz.Height - pos.Y - 1);
			}
		}
		//invalidate regions covered by cross
		protected void InvalidateCross()
		{
			if (Owner == null || Owner.Data == null)
				return;
			//vertical
			base.InvalidateContent(new Rectangle(
				Math.Min(0, _loc.X), _loc.Y,
				Math.Max(Owner.Data.Size.Width + Math.Max(0, -_loc.X), _loc.X + 1), 1));
			//horizontal
			base.InvalidateContent(new Rectangle(
				_loc.X, Math.Min(0, _loc.Y), 1,
				Math.Max(Owner.Data.Size.Height + Math.Max(0, -_loc.Y), _loc.Y + 1)));
		}
		//paint cross and buffered image
		protected internal override void OnPaint(PaintEventArgs e)
		{
			if (!_inside || Owner == null || Owner.Data == null)
				return;
			//draw cross
			DrawLineCross(e.Graphics, Control.MouseButtons != MouseButtons.None,
				Location, Owner.Data.Size);
		}
		//hide cursor
		protected override void OnMouseLeave(object sender, EventArgs e)
		{
			_inside = false;
			InvalidateCross();
			Owner.Update();
		}
		//prepare to show cursor
		protected override void OnMouseEnter(object sender, EventArgs e)
		{
			_inside = true;
			_loc = new Point(short.MaxValue, short.MaxValue);
		}
		//on tool change
		protected override void OnParentChange(LayerView value)
		{
			base.OnParentChange(value);
			//case cross invalidation
			_inside = value != null;
			_loc = new Point(short.MaxValue, short.MaxValue);
		}
		//apply cursor position and return true if invalidated
		protected bool ApplyCursorPosition(MouseEventArgs e)
		{
			Point newloc = Owner.ClientToContent(e.Location);
			if (newloc == _loc)
				return false;
			//invalidate cross
			InvalidateCross();
			_loc = newloc;
			InvalidateCross();
			return true;
		}
		protected bool Inside
		{
			get { return _inside; }
		}
		//location of the cross, in content units
		protected Point Location
		{
			get { return _loc; }
		}
		#endregion
	}
	/// <summary>
	/// rastering class for a shape tool that
	/// lies beneath the 
	/// </summary>
	public class BufferLayer : ToolLayer
	{
		private Bitmap _buffer;
		public BufferLayer()
		{
			Order = 25;
		}
		public override void Dispose()
		{
			if (_buffer != null)
			{
				_buffer.Dispose();
				_buffer = null;
			}
		}
		protected internal override void OnPaint(PaintEventArgs e)
		{
			//draw buffer
			if (_buffer != null)
			{
				e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				e.Graphics.DrawImageUnscaled(_buffer, Point.Empty);
			}
		}
		public PaintEventArgs CreateArgs()
		{
			if (_buffer==null)
				return null;
			return new PaintEventArgs(
			Graphics.FromImage(_buffer),
			new Rectangle(Point.Empty, _buffer.Size));
		}
		public void Initialize(IconImage value)
		{
			if (value != null && _buffer != null &&
				value.Bitmap.Size == _buffer.Size)
				return;
			if (_buffer != null)
			{
				_buffer.Dispose();
				_buffer = null;
			}
			if (value != null)
				_buffer = new Bitmap(value.Bitmap.Width, value.Bitmap.Height);
		}
		public void Clear()
		{
			if (_buffer!=null)
			{
				using (Graphics gr = Graphics.FromImage(_buffer))
					gr.Clear(Color.Transparent);
			}
		}
		public bool Initialized
		{
			get { return _buffer != null; }
		}
		public Bitmap Image
		{
			get { return _buffer; }
		}
	}
	/// <summary>
	/// shapetool class with width option
	/// </summary>
	public abstract class DrawShapeTool : ShapeTool
	{
		private WidthOption _width;
		public DrawShapeTool()
		{
			ConfigOptions.Add(_width = new WidthOption());
		}

		protected override Brush GetBrush(MouseButtons btn, Rectangle bounds)
		{
			return base.GetBrush(btn, Rectangle.Inflate(
				bounds, Width / 2, Width / 2));
		}
		//for line tool: portions may overhang and need to be redrawn
		protected override RectangleF GetInvalidateBounds(RectangleF bounds)
		{
			return RectangleF.Inflate(bounds, Width / 2f, Width / 2f);
		}
		protected override Rectangle GetDrawBounds(Rectangle box)
		{
			return Rectangle.Inflate(
				GraphicsEx.SortedRectangle(box), -Width / 2, -Width / 2);
		}

		protected int Width
		{
			get { return _width.Value; }
		}
	}
	/// <summary>
	/// draws a straight line
	/// </summary>
	public class DrawLineTool : DrawShapeTool
	{
		//keep direction
		protected override Rectangle GetDrawBounds(Rectangle box)
		{
			return box;
		}
		//make line snap to 45 degree points
		protected override Rectangle GetConstrainedBox(Rectangle bounds)
		{
			if (Math.Abs(bounds.Height) < Math.Abs(bounds.Width / 2))
				bounds.Height = 0;
			else if (Math.Abs(bounds.Width) < Math.Abs(bounds.Height / 2))
				bounds.Width = 0;
			else
				bounds = base.GetConstrainedBox(bounds);
			return bounds;
		}
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, Rectangle tip)
		{
			using (Pen pn = new Pen(GetBrush(btn, tip), Width))
				e.Graphics.DrawLine(pn, tip.X, tip.Y, tip.Right, tip.Bottom);
		}
	}
	/// <summary>
	/// fills a rectangle
	/// </summary>
	public class FillRectTool : ShapeTool
	{
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, Rectangle bounds)
		{
			if (bounds.Width < 1 || bounds.Height < 1)
				return;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.FillRectangle(GetBrush(btn, bounds), bounds);
		}
	}
	/// <summary>
	/// draws a rectangle
	/// </summary>
	public class DrawRectTool : DrawShapeTool
	{
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, Rectangle bounds)
		{
			if (bounds.Width < Width || bounds.Height < Width)
				return;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
			using (Pen pn = new Pen(GetBrush(btn, bounds), Width))
				e.Graphics.DrawRectangle(pn, bounds);
		}
	}
	/// <summary>
	/// fills an ellipse
	/// </summary>
	public class FillEllipseTool : ShapeTool
	{
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, Rectangle bounds)
		{
			if (bounds.Width < 1 || bounds.Height < 1)
				return;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.FillEllipse(GetBrush(btn, bounds), bounds);
		}
	}
	/// <summary>
	/// draws an ellipse
	/// </summary>
	public class DrawEllipseTool : DrawShapeTool
	{
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, Rectangle bounds)
		{
			if (bounds.Width < Width || bounds.Height < Width)
				return;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
			using (Pen pn = new Pen(GetBrush(btn, bounds), Width))
				e.Graphics.DrawEllipse(pn, bounds);
		}
	}
	/// <summary>
	/// fills a rounded rectangle
	/// </summary>
	public class FillRoundedRectTool : ShapeTool
	{
		private DiameterOption _diameter;
		public FillRoundedRectTool()
		{
			this.ConfigOptions.Add(_diameter = new DiameterOption());
		}
		/// <summary>
		/// creates a graphicspath with a rounded rectangle in it
		/// </summary>
		public static GraphicsPath CreateRoundedRect(int radius, Rectangle rct)
		{
			rct = GraphicsEx.SortedRectangle(rct);
			radius = Math.Max(1, Math.Min(Math.Min(rct.Width, rct.Height), radius * 2));

			GraphicsPath pth = new GraphicsPath();
			pth.AddArc(rct.X, rct.Y, radius, radius, 180f, 90f);
			pth.AddArc(rct.Right - radius, rct.Y, radius, radius, 270f, 90f);
			pth.AddArc(rct.Right - radius, rct.Bottom - radius, radius, radius, 0f, 90f);
			pth.AddArc(rct.X, rct.Bottom - radius, radius, radius, 90f, 90f);
			pth.CloseFigure();
			return pth;
		}
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, Rectangle bounds)
		{
			if (bounds.Width < 1 || bounds.Height < 1)
				return;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			using (GraphicsPath pth = CreateRoundedRect(_diameter.Value, bounds))
			{
				e.Graphics.FillPath(GetBrush(btn, bounds), pth);
			}
		}
	}
	/// <summary>
	/// draws rounded rect
	/// </summary>
	public class DrawRoundedRectTool : DrawShapeTool
	{
		private DiameterOption _diameter;
		public DrawRoundedRectTool()
		{
			this.ConfigOptions.Add(_diameter = new DiameterOption());
		}
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, Rectangle bounds)
		{
			if (bounds.Width < 1 || bounds.Height < 1)
				return;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
			using (Pen pn = new Pen(GetBrush(btn, bounds), Width))
			{
				using (GraphicsPath pth = FillRoundedRectTool.CreateRoundedRect(
					_diameter.Value, bounds))
				{
					e.Graphics.DrawPath(pn, pth);
				}
			}
		}
	}
	/// <summary>
	/// fills a pie
	/// </summary>
	public class FillPieTool : ShapeTool
	{
		private StartAngleOption _start;
		private SwepAngleOption _swep;
		public FillPieTool()
		{
			this.ConfigOptions.Add(_start = new StartAngleOption());
			this.ConfigOptions.Add(_swep = new SwepAngleOption());
		}
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, Rectangle bounds)
		{
			if (bounds.Width < 1 || bounds.Height < 1)
				return;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.FillPie(GetBrush(btn, bounds), bounds, _start.Value, _swep.Value);
		}
	}
	/// <summary>
	/// draws a pie
	/// </summary>
	public class DrawPieTool : DrawShapeTool
	{
		private StartAngleOption _start;
		private SwepAngleOption _swep;
		public DrawPieTool()
		{
			this.ConfigOptions.Add(_start = new StartAngleOption());
			this.ConfigOptions.Add(_swep = new SwepAngleOption());
		}
		protected override void OnDraw(PaintEventArgs e, MouseButtons btn, Rectangle bounds)
		{
			if (bounds.Width < 1 || bounds.Height < 1)
				return;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
			using (Pen pn = new Pen(GetBrush(btn, bounds), Width))
				e.Graphics.DrawPie(pn, bounds, _start.Value, _swep.Value);
		}
	}
}
