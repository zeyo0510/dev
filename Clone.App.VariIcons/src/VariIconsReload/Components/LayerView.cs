using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ControlsEx;

namespace VariIconsReload.Components
{
	public class LayerView : ScrollableView
	{
		#region types
		/// <summary>
		/// used for sorting
		/// </summary>
		public class LayerComparision : Comparer<Layer>
		{
			public override int Compare(Layer x, Layer y)
			{
				return x.Order.CompareTo(y.Order);
			}
		}
		/// <summary>
		/// collection for layers
		/// </summary>
		public class LayerCollection : CollectionBase<LayerView, Layer>
		{
			public LayerCollection(LayerView owner) : base(owner) { }

			protected override void OnValidate(Layer value)
			{
				if (value == null)
					throw new ArgumentNullException("value");
			}
			protected override void OnInsert(int index, Layer value)
			{
				value.SetOwnerCore(Owner);
			}
			protected override void OnSet(int index, Layer oldValue, Layer newValue)
			{
				oldValue.SetOwnerCore(null);
				newValue.SetOwnerCore(Owner);
			}
			protected override void OnRemove(int index, Layer value)
			{
				value.SetOwnerCore(null);
			}
			protected override void OnClear()
			{
				foreach (Layer layer in InnerList)
					layer.SetOwnerCore(null);
			}

			protected override void OnInsertComplete(int index, Layer value)
			{
				UpdateOrder();
			}
			protected override void OnSetComplete(int index, Layer oldValue, Layer newValue)
			{
				UpdateOrder();
			}
			protected override void OnRemoveComplete(int index, Layer value)
			{
				UpdateOrder();
			}
			protected override void OnClearComplete()
			{
				UpdateOrder();
			}
			//used when or layer list updates and needs refreshing
			internal void UpdateOrder()
			{
				this.InnerList.Sort(new LayerComparision());
				Owner.Refresh();
			}
		}
		/// <summary>
		/// offers an extended functionality for
		/// the corner of a control
		/// </summary>
		public class ExtenderCorner : Control
		{
			public ExtenderCorner()
			{
				this.SetStyle(ControlStyles.AllPaintingInWmPaint |
					ControlStyles.DoubleBuffer |
					ControlStyles.UserPaint, true);
			}
			#region controller
			//draw cross and border
			protected override void OnPaint(PaintEventArgs e)
			{
				e.Graphics.Clear(Color.White);
				using (HatchBrush hbrs = new HatchBrush(
						  HatchStyle.Percent50, Color.Blue, Color.Transparent))
				{
					using (Pen pn = new Pen(hbrs, 1f))
					{
						int mid = this.Width / 2 - 1;
						e.Graphics.DrawLine(pn, 0, mid, this.Width, mid);
						e.Graphics.DrawLine(pn, mid, 0, mid, this.Height);
					}
				}
				e.Graphics.DrawLine(Pens.Black, this.Width - 1, 0, this.Width - 1, this.Height);
				e.Graphics.DrawLine(Pens.Black, 0, this.Height - 1, this.Width, this.Height - 1);
			}
			//make sure sqareshape
			protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
			{
				x = y = Math.Max(x, y);
				base.SetBoundsCore(x, y, width, height, specified);
			}
			#endregion
		}
		#endregion
		#region variables
		/// <summary>
		/// use this constant to hide the ruler mark
		/// </summary>
		public static readonly RectangleF InvalidMark = new RectangleF(float.NaN, float.NaN, float.NaN, float.NaN);
		//
		private Color _checker_fore = Color.Silver,
			_checker_back = Color.White,
			_grid_col = Color.FromArgb(128, 0, 0, 0);
		//controls
		private Ruler _hruler, _vruler;
		private ExtenderCorner _corner;
		//settings
		private ScaleFactor _zoom = ScaleFactor.Identity;
		private DisplayMode _mode = DisplayMode.Normal;
		//data
		private LayerCollection _layers;
		private Content _content;
		//
		private int _suspendedrefresh = 0;
		#endregion
		/// <summary>
		/// ctor
		/// </summary>
		public LayerView()
		{
			this.InitGUI();

			_layers = new LayerCollection(this);
		}
		/// <summary>
		/// place controls
		/// </summary>
		private void InitGUI()
		{
			_hruler = new Ruler();
			_vruler = new Ruler();
			_corner = new ExtenderCorner();
			//corner
			_corner.Location = new Point(0, 0);
			_corner.Size = new Size(16, 16);
			this.Controls.Add(_corner);
			//hruler
			_hruler.Location = new Point(16, 0);
			_hruler.Size = new Size(this.Width - 16, 16);
			this.Controls.Add(_hruler);
			//vruler
			_vruler.Orientation = Orientation.Vertical;
			_vruler.Location = new Point(0, 16);
			_vruler.Size = new Size(16, this.Height - 16);
			this.Controls.Add(_vruler);
			//properties
			this.SetStyle(ControlStyles.AllPaintingInWmPaint |
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.UserMouse |
				ControlStyles.Selectable |
				ControlStyles.ResizeRedraw, true);
			//this.SetScrollFlags(ScrollFlags.All, false);
			base.BackColor = SystemColors.AppWorkspace;
			this.AutoScrollMinSize = new Size(400, 400);
		}
		#region helpers
		/// <summary>
		/// adjusts zoom in case of autozoom
		/// or scrollbars
		/// </summary>
		private void AdjustZoom()
		{
			if (CanSetZoom)
			{
				//copy user zoom to actual zoom
				_hruler.Zoom = _vruler.Zoom = _zoom;
				AutoScrollMinSize = Size.Add(GetImageSize(),
					new Size(_vruler.Visible ? _vruler.Width + 2 : 2,
						_hruler.Visible ? _hruler.Height + 2 : 2));
			}
			else
			{
				//adapt zoom to editor size
				if (_content != null && _content.Size.Width > 0 && _content.Size.Height > 0)
				{
					ScaleFactor zoom = ScaleFactor.FromDouble(
						Math.Max(1f, Math.Floor(Math.Min(
						(double)(this.ContentSize.Width - 8) / (float)_content.Size.Width,
						(double)(this.ContentSize.Height - 8) / (float)_content.Size.Height))));
					if (zoom > 1.0) zoom = ScaleFactor.FromDouble(Math.Floor(zoom));
					_hruler.Zoom = _vruler.Zoom = zoom;
				}
				this.AutoScrollMinSize = Size.Empty;
			}
		}
		/// <summary>
		/// makes the ruler have the actual offset, either
		/// calculated when picture is centered or retrieved
		/// by scrollbars
		/// </summary>
		private void AdjustOffset()
		{
			if ((_mode & DisplayMode.Center) != 0)
			{
				Size sz = GetImageSize();
				_hruler.Offset = this.HScroll ?//horizontal offset
						this.AutoScrollPosition.X + 1 :
						(this.ContentSize.Width - sz.Width) / 2;
				_vruler.Offset = this.VScroll ?//vertical offset
						this.AutoScrollPosition.Y + 1 :
						(this.ContentSize.Height - sz.Height) / 2;
			}
			else
			{
				_hruler.Offset = this.AutoScrollPosition.X;
				_vruler.Offset = this.AutoScrollPosition.Y;
			}
		}
		/// <summary>
		/// returns the actual content size without scrollbars
		/// </summary>
		public Size ContentSize
		{
			get
			{
				Size sz = this.ClientSize;
				if (_vruler.Visible) sz.Width -= _vruler.Width;
				if (_hruler.Visible) sz.Height -= _hruler.Height;
				return sz;
			}
		}
		/// <summary>
		/// gets the image size after zoom applied
		/// </summary>
		public Size GetImageSize()
		{
			if (_content == null) return Size.Empty;
			return new Size(
				(int)(_hruler.Zoom.Scale(_content.Size.Width)),
				(int)(_vruler.Zoom.Scale(_content.Size.Height)));
		}
		/// <summary>
		/// gets the transform for painting, calculated by zoom
		/// and offset stored in the rulers
		/// </summary>
		public Matrix GetTransform()
		{
			return new Matrix(_hruler.Zoom, 0f, 0f, _hruler.Zoom,
						_hruler.Offset + 16, _vruler.Offset + 16);
		}
		/// <summary>
		/// maps a rectangle on screen, presented in client coordinates
		/// to rectangle on screen
		/// </summary>
		public RectangleF ClientToContent(RectangleF value)
		{
			Matrix mat = GetTransform();
			if (!mat.IsInvertible)
				return value;
			mat.Invert();
			return TransformRectangle(value, mat);
		}
		/// <summary>
		/// maps a point on screen, presented in client coordinates
		/// to point on content
		/// </summary>
		public PointF ClientToContent(PointF value)
		{
			Matrix mat = GetTransform();
			if (!mat.IsInvertible)
				return value;
			mat.Invert();
			return TransformPoint(value, mat);
		}
		/// <summary>
		/// maps a point on screen to the point on content.
		/// as rounding method floor is used.
		/// </summary>
		public Point ClientToContent(Point value)
		{
			PointF ret = ClientToContent((PointF)value);
			return new Point((int)Math.Floor(ret.X), (int)Math.Floor(ret.Y));
		}
		/// <summary>
		/// maps a rectangle on screen, presented in client coordinates
		/// to rectangle on screen
		/// </summary>
		public RectangleF ContentToClient(RectangleF value)
		{
			return TransformRectangle(value, GetTransform());
		}
		/// <summary>
		/// maps a point on screen, presented in client coordinates
		/// to point on screen
		/// </summary>
		public PointF ContentToClient(PointF value)
		{
			return TransformPoint(value, GetTransform());
		}
		//maps a pointF by the given transform
		private PointF TransformPoint(PointF value, Matrix transform)
		{
			PointF[] pts = new PointF[] { value };
			transform.TransformPoints(pts);
			return pts[0];
		}
		//maps a rectangleF by the given transform
		private RectangleF TransformRectangle(RectangleF value, Matrix transform)
		{
			PointF[] endpoints = new PointF[] { value.Location, new PointF(value.Right, value.Bottom) };
			transform.TransformPoints(endpoints);
			return new RectangleF(endpoints[0],
				new SizeF(endpoints[1].X - endpoints[0].X,
					endpoints[1].Y - endpoints[0].Y));
		}
		/// <summary>
		/// returns if zoom can be set
		/// </summary>
		private bool CanSetZoom
		{
			get { return (_mode & DisplayMode.Stretch) == 0; }
		}
		/// <summary>
		/// draws a frame with cut edges. you should fill the area after
		/// it, cause the edges are touched 25% each
		/// </summary>
		public static void DrawCutFrame(Graphics gr, Color col, float width, Rectangle rct)
		{
			if (gr == null)
				throw new ArgumentNullException("graphics");
			//config
			gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
			//draw
			using (Pen lp = new Pen(col, width))
			{
				float w = lp.Width * 0.5f;
				gr.DrawPolygon(lp, new PointF[]{
						new PointF(rct.X+w,rct.Y-w),new PointF(rct.Right-w,rct.Y-w),
						new PointF(rct.Right+w,rct.Y+w),new PointF(rct.Right+w,rct.Bottom-w),
						new PointF(rct.Right-w,rct.Bottom+w),new PointF(rct.X+w,rct.Bottom+w),
						new PointF(rct.X-w,rct.Bottom-w),new PointF(rct.X-w,rct.Y+w)
					});
			}
		}
		/// <summary>
		/// draws a scaling checkerboard
		/// </summary>
		public static void DrawCheckerBoard(Graphics gr, Color fore, Color back, ScaleFactor zoom, Rectangle rct)
		{
			if (gr == null)
				throw new ArgumentNullException("graphics");
			gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
			//background
			using (SolidBrush brs = new SolidBrush(back))
			{
				gr.FillRectangle(brs, rct);
			}
			if (back == fore)
				return;
			//squares
			bool odd = false;
			int delta = Math.Max(4, zoom.Unscale(4));
			List<RectangleF> rcts = new List<RectangleF>();
			for (int x = rct.X; x < rct.Right; x += delta)
			{
				//odd columns are offsetted
				int y = (odd = !odd) ? delta : 0;
				//fill every column
				for (y += rct.Y; y < rct.Bottom; y += delta << 1)
					rcts.Add(new RectangleF(x, y,
						Math.Min(delta, rct.Right - x),
						Math.Min(delta, rct.Bottom - y)));
			}
			//draw squares
			if (rcts.Count > 0)
			{
				using (SolidBrush brs = new SolidBrush(fore))
				{
					gr.FillRectangles(brs, rcts.ToArray());
				}
			}
		}
		#endregion
		#region controller
		//size changed, rearrange controls and update zoom / offset
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			_hruler.Size = new Size(this.ClientSize.Width - 16, 16);
			_vruler.Size = new Size(16, this.ClientSize.Height - 16);
			//
			AdjustZoom();
			AdjustOffset();
		}
		//overridden to prevent flickering
		protected override void SyncScrollbars()
		{
			if (_suspendedrefresh == 0)
				base.SyncScrollbars();
		}
		//overridden to prevent flickering
		public override void Refresh()
		{
			if (_suspendedrefresh == 0)
				base.Refresh();
		}
		//adapt scrollbar positions to rulers
		protected override void SetDisplayRectLocation(int x, int y)
		{
			base.SetDisplayRectLocation(x, y);
			AdjustOffset();
		}
		//draw editor frame, content and layers
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.Transform = GetTransform();
			e.Graphics.RenderingOrigin = new Point(_hruler.Offset, _vruler.Offset);
			//frame / content
			if (_content != null)
			{
				Rectangle rct = new Rectangle(Point.Empty, _content.Size);
				//rectangle with cut corners
				DrawCheckerBoard(e.Graphics, _checker_fore, _checker_back, RealZoom, rct);
				DrawCutFrame(e.Graphics, _grid_col, RealZoom.Unscale(1f), rct);
				//draw content
				GraphicsState gstate = e.Graphics.Save();
				_content.OnPaint(e);
				e.Graphics.Restore(gstate);
			}
			//layers
			foreach (Layer layer in _layers)
			{
				GraphicsState gstate = e.Graphics.Save();
				layer.OnPaint(e);
				e.Graphics.Restore(gstate);
			}
		}
		#endregion
		#region public members
		/// <summary>
		/// supresses automatic redrawing
		/// </summary>
		public void SuspendRefresh()
		{
			_suspendedrefresh++;
		}
		/// <summary>
		/// resumes automatic redrawing
		/// </summary>
		public void ResumeRefresh()
		{
			_suspendedrefresh--;
			if (_suspendedrefresh == 0)
			{
				base.SyncScrollbars();
				base.Refresh();
			}
		}
		#endregion
		#region properties
		/// <summary>
		/// real zoom value, used for painting
		/// </summary>
		[Browsable(false)]
		public ScaleFactor RealZoom
		{
			get { return _hruler.Zoom; }
		}
		/// <summary>
		/// real offset, used for painting
		/// </summary>
		[Browsable(false)]
		public Point RealOffset
		{
			get { return new Point(_hruler.Offset, _vruler.Offset); }
		}
		[DefaultValue(typeof(Color), "AppWorkspace")]
		public override Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}
		/// <summary>
		/// user zoom, may be overridden when stretching
		/// is activated
		/// </summary>
		[Browsable(false)]
		public ScaleFactor Zoom
		{
			get { return _zoom; }
			set
			{
				if (value == _zoom) return;
				_zoom = value;
				//update size
				AdjustZoom();
				AdjustOffset();
				this.Refresh();
				//
				if (ZoomChanged != null)
					ZoomChanged(this, EventArgs.Empty);
			}
		}
		/// <summary>
		/// core data to be displayed
		/// </summary>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Content Data
		{
			get { return _content; }
			set
			{
				if (value == _content) return;
				if (_content != null) _content.SetOwnerCore(null);
				_content = value;
				if (value != null) value.SetOwnerCore(this);
				//
				AdjustZoom();
				AdjustOffset();
				this.Refresh();
			}
		}
		/// <summary>
		/// overlays that have to be drawn above content
		/// </summary>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LayerCollection Layers
		{
			get { return _layers; }
		}
		/// <summary>
		/// specifies the displaymode
		/// </summary>
		[Description("specifies the displaymode"),
		DefaultValue(typeof(DisplayMode), "Normal")]
		public DisplayMode DisplayMode
		{
			get { return _mode; }
			set
			{
				if (_mode == value) return;
				_mode = value;
				AdjustZoom();
				AdjustOffset();
				this.Refresh();
			}
		}
		/// <summary>
		/// gets or sets the first color of the background checker
		/// </summary>
		[Description("gets or sets the first color of the background checker"),
		DefaultValue(typeof(Color), "White")]
		public Color BackgroundColorA
		{
			get { return _checker_back; }
			set
			{
				if (value == _checker_back)
					return;
				_checker_back = value;
				this.Refresh();
			}
		}
		/// <summary>
		/// gets or sets the second color of the background checker
		/// </summary>
		[Description("gets or sets the second color of the background checker"),
		DefaultValue(typeof(Color), "Silver")]
		public Color BackgroundColorB
		{
			get { return _checker_fore; }
			set
			{
				if (value == _checker_fore)
					return;
				_checker_fore = value;
				this.Refresh();
			}
		}
		/// <summary>
		/// gets the highlight point of the rulers.
		/// specify InvalidMarker to hide
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RectangleF RulerMark
		{
			get
			{
				return new RectangleF(_hruler.Value, _vruler.Value,
					_hruler.Length, _vruler.Length);
			}
			set
			{
				_hruler.SetMarker(value.X, value.Width);
				_vruler.SetMarker(value.Y, value.Height);
			}
		}
		#endregion
		#region events
		/// <summary>
		/// raised when a new zoom level is set
		/// </summary>
		public event EventHandler ZoomChanged;
		#endregion
	}
	/// <summary>
	/// represents a paintable layer without dimension
	/// </summary>
	public abstract class Layer
	{
		private LayerView _owner;
		private int _order = 0;
		#region controller

		//called before parent is changed
		protected virtual void OnParentChange(LayerView value)
		{
		}
		//called when painting required,
		//do not call when not assigned to a layerview!
		protected internal virtual void OnPaint(PaintEventArgs e)
		{
		}
		public virtual void InvalidateContent(RectangleF rect)
		{
			if (Owner != null)
				Owner.Invalidate(Rectangle.Inflate(Rectangle.Round(
					Owner.ContentToClient(GraphicsEx.SortedRectangle(rect))), 4, 4));
		}
		public void Update()
		{
			if (Owner != null)
				Owner.Update();
		}
		#endregion
		internal void SetOwnerCore(LayerView value)
		{
			OnParentChange(value);
			_owner = value;
		}
		/// <summary>
		/// gets the currently owning layerview
		/// </summary>
		public LayerView Owner
		{
			get { return _owner; }
		}
		/// <summary>
		/// the preferred ordinal number. if two layers have
		/// the same ordinal number they stay in the order they are
		/// added in, i.e. the last is on top.
		/// </summary>
		public int Order
		{
			get { return _order; }
			set
			{
				if (value == _order)
					return;
				_order = value;
				if (_owner != null)
					_owner.Layers.UpdateOrder();
			}
		}
	}
	/// <summary>
	/// paintable layer which registers some predefined mouse handlers
	/// </summary>
	public abstract class MouseLayer : Layer
	{
		protected override void OnParentChange(LayerView value)
		{
			if (Owner != null)
			{
				Owner.MouseDown -= new MouseEventHandler(OnMouseDown);
				Owner.MouseMove -= new MouseEventHandler(OnMouseMove);
				Owner.MouseUp -= new MouseEventHandler(OnMouseUp);
				Owner.MouseEnter -= new EventHandler(OnMouseEnter);
				Owner.MouseLeave -= new EventHandler(OnMouseLeave);
				Owner.MouseWheel -= new MouseEventHandler(OnMouseWheel);
				Owner.DoubleClick -= new EventHandler(OnDoubleClick);
			}
			if (value != null)
			{
				value.MouseDown += new MouseEventHandler(OnMouseDown);
				value.MouseMove += new MouseEventHandler(OnMouseMove);
				value.MouseUp += new MouseEventHandler(OnMouseUp);
				value.MouseEnter += new EventHandler(OnMouseEnter);
				value.MouseLeave += new EventHandler(OnMouseLeave);
				value.MouseWheel += new MouseEventHandler(OnMouseWheel);
				value.DoubleClick += new EventHandler(OnDoubleClick);
			}
		}

		protected virtual void OnDoubleClick(object sender, EventArgs e)
		{
		}

		protected virtual void OnMouseLeave(object sender, EventArgs e)
		{
		}

		protected virtual void OnMouseWheel(object sender, MouseEventArgs e)
		{
		}

		protected virtual void OnMouseMove(object sender, MouseEventArgs e)
		{
		}

		protected virtual void OnMouseEnter(object sender, EventArgs e)
		{
		}

		protected virtual void OnMouseUp(object sender, MouseEventArgs e)
		{
		}

		protected virtual void OnMouseDown(object sender, MouseEventArgs e)
		{
		}
	}
	/// <summary>
	/// abstract layer catching mouse and keyboard events from layerview
	/// </summary>
	public abstract class MouseKeyboardLayer : MouseLayer
	{
		protected override void OnParentChange(LayerView value)
		{
			if (Owner != null)
			{
				Owner.KeyDown -= new KeyEventHandler(OnKeyDown);
				Owner.KeyPress -= new KeyPressEventHandler(OnKeyPress);
				Owner.KeyUp -= new KeyEventHandler(OnKeyUp);
			}
			if (value != null)
			{
				value.KeyDown += new KeyEventHandler(OnKeyDown);
				value.KeyPress += new KeyPressEventHandler(OnKeyPress);
				value.KeyUp += new KeyEventHandler(OnKeyUp);
			}
			base.OnParentChange(value);
		}
		protected virtual void OnKeyDown(object sender, KeyEventArgs e)
		{
		}
		protected virtual void OnKeyUp(object sender, KeyEventArgs e)
		{
		}
		protected virtual void OnKeyPress(object sender, KeyPressEventArgs e)
		{
		}
	}
	/// <summary>
	/// display modes for layerview
	/// </summary>
	public enum DisplayMode
	{
		Normal = 0, Center = 1, Stretch = 2,
		CenterStretch = 3
	}
	/// <summary>
	/// adds the dimension role to a layer
	/// </summary>
	public abstract class Content : Layer
	{
		public abstract Size Size { get; }
	}
	/// <summary>
	/// content of a layerview control hosting an image, such as
	/// Bitmap or Icon
	/// </summary>
	public class ImageContent<T> : Content where T : Image
	{
		private T _data;
		public ImageContent(T data)
		{
			if (data == null)
				throw new ArgumentNullException("data");
			_data = data;
		}
		protected internal override void OnPaint(PaintEventArgs e)
		{
			if (Owner == null) return;
			//make big pixels when zoomed in, and smooth when zoomed out
			if (Owner.RealZoom > 1.0)
				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			else
				e.Graphics.InterpolationMode = InterpolationMode.Bicubic;
			//remove half pixel on begin
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.DrawImage(_data, 0, 0, _data.Width, _data.Height);
		}
		public T Image
		{
			get { return _data; }
		}
		public override Size Size
		{
			get { return _data.Size; }
		}
	}
	/// <summary>
	/// logical layer displaying a pixel grid on the image
	/// </summary>
	public class GridLayer : Layer
	{
		private static readonly float[] multipliers =
			new float[] { 2f, 5f };
		public GridLayer()
		{
			Order = 50;
		}
		/// <summary>
		/// calculate the number of pixels to take between each
		/// grid line to be syncronized with rulers and not
		/// messing up the image with too much grid
		/// </summary>
		/// <returns></returns>
		private int GetUnit()
		{
			int unit = 1,//starting unit count
				index = 0;//subdivision count
			float unitwidth = Owner.RealZoom.Scale((float)unit);
			if (unitwidth == 0f)
				return 1;//error
			for (; unitwidth <= 3f && index < 1000; index++)
			{
				unitwidth *= multipliers[index % multipliers.Length];
				unit = (int)(unit * multipliers[index % multipliers.Length]);
			}
			return unit;
		}
		protected internal override void OnPaint(PaintEventArgs e)
		{
			if (Owner == null || Owner.Data == null) return;
			//
			Size sz = Owner.Data.Size;
			int step = GetUnit();
			//
			using (Pen lnpn = new Pen(Color.FromArgb(128, 0, 0, 0)))
			{
				using (GraphicsPath _grid = new GraphicsPath())
				{
					//create columns
					for (int x = step; x < sz.Width; x += step)
					{
						_grid.StartFigure();
						_grid.AddLine(x, 0, x, sz.Height);
					}
					//create rows
					for (int y = step; y < sz.Height; y += step)
					{
						_grid.StartFigure();
						_grid.AddLine(0, y, sz.Width, y);
					}
					//draw
					lnpn.Width = Owner.RealZoom.Unscale(1f);
					e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
					e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
					e.Graphics.DrawPath(lnpn, _grid);
				}
			}
		}
	}
}