using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DrawingEx.IconEncoder;
using VariIconsReload.Components;
using ControlsEx;

namespace VariIconsReload.Tools
{
	/// <summary>
	/// selection tool to select and move or resize an area
	/// </summary>
	public class SelectionTool : Tool
	{
		#region types
		/// <summary>
		/// states for selectionlayer
		/// </summary>
		private enum State
		{
			None = 0, Constructing = 1, Moving = 2,
			ResizeN = 4, ResizeE = 8, ResizeS = 16, ResizeW = 32,
			ResizeNE = 12, ResizeSE = 24, ResizeSW = 48, ResizeNW = 36,
			Destroying = 64
		}
		/// <summary>
		/// layer for creating and modifying a selection overlay
		/// </summary>
		private class ConstructionLayer : CrossLayer
		{
			#region variables
			private Rectangle _box;
			private State _state = State.None;
			private Point _delta;
			//
			private SelectionTool _tool;
			private Selection _selection;
			#endregion
			public ConstructionLayer(SelectionTool tool)
			{
				if (tool == null)
					throw new ArgumentNullException("tool");
				_tool = tool;
				if (_tool._antialias != null)
					_tool._antialias.Changed += new EventHandler(Antialias_Changed);
			}
			#region controller
			//propagate antialias setting
			void Antialias_Changed(object sender, EventArgs e)
			{
				if (_selection != null)
					_selection.AntiAlias = _tool._antialias.Value;
			}
			//grab selection
			protected override void OnParentChange(LayerView value)
			{
				base.OnParentChange(value);
				if (value == null)
					this.Selection = null;
				else
				{
					//search selection by searching a contentlayer
					foreach (Layer layer in value.Layers)
					{
						Selection.SliceLayer sel =
							layer as Selection.SliceLayer;
						if (sel != null)
						{
							this.Selection = sel.Selection;
							return;
						}
					}
					this.Selection = null;
				}
			}
			//draw a single handle
			private void DrawHandle(PaintEventArgs e, float x, float y, float w, Pen pn)
			{
				RectangleF bnd = new RectangleF(x - 2f * w, y - 2f * w, w * 4f, w * 4f);
				e.Graphics.FillRectangle(Brushes.White, bnd);
				e.Graphics.DrawRectangle(pn, bnd.X, bnd.Y, bnd.Width, bnd.Height);
			}
			//draw construction and selection layer
			protected internal override void OnPaint(PaintEventArgs e)
			{
				if (_selection == null)
					return;
				if (_state == State.Constructing ||
					(_state == State.None && _box.Size.IsEmpty))
					base.OnPaint(e);
				//display box
				if (!_box.Size.IsEmpty)
				{
					Rectangle bounds = GraphicsEx.SortedRectangle(_box);
					using (SolidBrush brs = new SolidBrush(
						Color.FromArgb(128, SystemColors.Highlight)))
					{
						e.Graphics.FillRectangle(brs, bounds);
					}
					float w = Owner.RealZoom.Unscale(1f);
					using (Pen pn = new Pen(Color.FromArgb(200, 255, 255, 255), w * 3f))
					{
						//draw base
						e.Graphics.DrawRectangle(pn, bounds);
						//draw top
						pn.Color = Color.Black;
						pn.Width = w;
						pn.DashPattern = new float[] { w * 4f, w * 4f };
						e.Graphics.DrawRectangle(pn, bounds);
						//draw handles
						pn.Width = w;
						pn.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
						pn.Color = Color.Navy;
						//
						DrawHandle(e, bounds.X, bounds.Y, w, pn);
						DrawHandle(e, bounds.Right, bounds.Y, w, pn);
						DrawHandle(e, bounds.Right, bounds.Bottom, w, pn);
						DrawHandle(e, bounds.X, bounds.Bottom, w, pn);
						//mid handles
						DrawHandle(e, bounds.X + bounds.Width / 2f, bounds.Y, w, pn);
						DrawHandle(e, bounds.Right, bounds.Y + bounds.Height / 2f, w, pn);
						DrawHandle(e, bounds.X + bounds.Width / 2f, bounds.Bottom, w, pn);
						DrawHandle(e, bounds.X, bounds.Y + bounds.Height / 2f, w, pn);
					}
				}
			}
			//gets the recommended state for the location
			private State GetNewState(Point client)
			{
				if (_box.IsEmpty)
					return State.Constructing;
				else
				{
					Rectangle clbox = Rectangle.Round(Owner.ContentToClient(_box));
					//check handles first
					if (new Rectangle(clbox.X - 2, clbox.Y - 2, 4, 4).Contains(client))
						return State.ResizeNW;
					else if (new Rectangle(clbox.Right - 2, clbox.Y - 2, 4, 4).Contains(client))
						return State.ResizeNE;
					else if (new Rectangle(clbox.Right - 2, clbox.Bottom - 2, 4, 4).Contains(client))
						return State.ResizeSE;
					else if (new Rectangle(clbox.X - 2, clbox.Bottom - 2, 4, 4).Contains(client))
						return State.ResizeSW;
					//mid handles
					int mw = clbox.X + clbox.Width / 2,
						mh = clbox.Y + clbox.Height / 2;
					if (new Rectangle(mw - 2, clbox.Y - 2, 4, 4).Contains(client))
						return State.ResizeN;
					else if (new Rectangle(clbox.Right - 2, mh - 2, 4, 4).Contains(client))
						return State.ResizeE;
					else if (new Rectangle(mw - 2, clbox.Bottom - 2, 4, 4).Contains(client))
						return State.ResizeS;
					else if (new Rectangle(clbox.X - 2, mh - 2, 4, 4).Contains(client))
						return State.ResizeW;
					//then check box
					if (!clbox.Contains(client))
						return State.Destroying;
					else
						return State.Moving;
				}
			}
			//updates the cursor according to state
			private void UpdateCursor(State state)
			{
				switch (state)
				{
					case State.Constructing:
						Owner.Cursor = Cursors.Cross; break;
					case State.Destroying:
						Owner.Cursor = Cursors.No; break;
					case State.Moving:
						Owner.Cursor = Cursors.SizeAll; break;
					case State.ResizeNW:
					case State.ResizeSE:
						Owner.Cursor = Cursors.SizeNWSE; break;
					case State.ResizeNE:
					case State.ResizeSW:
						Owner.Cursor = Cursors.SizeNESW; break;
					case State.ResizeN:
					case State.ResizeS:
						Owner.Cursor = Cursors.SizeNS; break;
					case State.ResizeE:
					case State.ResizeW:
						Owner.Cursor = Cursors.SizeWE; break;
					default:
						Owner.Cursor = Cursors.Default;
						break;
				}
			}
			//start constructing or moving
			protected override void OnMouseDown(object sender, MouseEventArgs e)
			{
				if (_selection == null)
					return;
				_state = GetNewState(e.Location);
				switch (_state)
				{
					case State.Constructing:
						//construct new box
						_delta = Location;
						_box = new Rectangle(Location, new Size(1, 1));
						Owner.Cursor = Cursors.SizeNWSE;
						break;
					case State.Moving:
					case State.ResizeE:
					case State.ResizeN:
					case State.ResizeNE:
					case State.ResizeNW:
					case State.ResizeS:
					case State.ResizeSE:
					case State.ResizeSW:
					case State.ResizeW:
						//cut out or copy
						if (_selection.Slice == null)
							_selection.Grab(_box,
								(Control.ModifierKeys & Keys.Alt) != 0);
						//moving or resizing
						Rectangle cbox = Rectangle.Round(Owner.ContentToClient(_box));
						//vertical, top middle or bottom
						if ((_state & State.ResizeN) != 0 || _state == State.Moving)
							_delta.Y = e.Y - cbox.Y;
						else if ((_state & State.ResizeS) != 0)
							_delta.Y = e.Y - cbox.Bottom;
						//horizontal, left middle or right
						if ((_state & State.ResizeW) != 0 || _state == State.Moving)
							_delta.X = e.X - cbox.X;
						else if ((_state & State.ResizeE) != 0)
							_delta.X = e.X - cbox.Right;
						break;
				}
				InvalidateCross();
				Owner.Update();
			}
			//transform or construct the selection area
			protected override void OnMouseMove(object sender, MouseEventArgs e)
			{
				if (_selection == null)
					return;
				switch (_state)
				{
					case State.None:
						//mouse pointer feedback
						UpdateCursor(GetNewState(e.Location));
						if (!base.ApplyCursorPosition(e)) return;
						if (_box.Size.IsEmpty)
							_tool.RaiseToolStatusChanged(new Rectangle(Location, new Size(1, 1)));
						break;
					case State.Constructing:
						//resize box and update
						if (!base.ApplyCursorPosition(e)) return;
						InvalidateContent(_box);
						//make positive size
						_box = GraphicsEx.SortedRectangle(
							Rectangle.FromLTRB(_delta.X, _delta.Y, Location.X, Location.Y));
						_box.Width++; _box.Height++;
						InvalidateContent(_box);
						_tool.RaiseToolStatusChanged(_box);
						break;
					case State.Moving:
						//move box and update
						Point pt = Tool.Floor(Owner.ClientToContent(
							new Point(e.X - _delta.X, e.Y - _delta.Y)));
						if (pt == _box.Location)
							return;
						InvalidateContent(_box);
						_box.Location = pt;
						_selection.Bounds = _box;
						InvalidateContent(_box);
						_tool.RaiseToolStatusChanged(_box);
						break;
					case State.ResizeE:
					case State.ResizeN:
					case State.ResizeNE:
					case State.ResizeNW:
					case State.ResizeS:
					case State.ResizeSE:
					case State.ResizeSW:
					case State.ResizeW:
						ApplyCursorPosition(e);
						Rectangle rct = _box;
						//vertical, top middle or bottom
						if ((_state & State.ResizeN) != 0)
						{
							rct.Y = Tool.Floor(Owner.ClientToContent(
								new Point(0, e.Y - _delta.Y))).Y;
							rct.Height = _box.Bottom - rct.Y;
						}
						else if ((_state & State.ResizeS) != 0)
							rct.Height = Tool.Floor(Owner.ClientToContent(
								new Point(0, e.Y - _delta.Y))).Y - rct.Y;
						//horizontal, left middle or right
						if ((_state & State.ResizeW) != 0)
						{
							rct.X = Tool.Floor(Owner.ClientToContent(
								new Point(e.X - _delta.X, 0))).X;
							rct.Width = _box.Right - rct.X;
						}
						else if ((_state & State.ResizeE) != 0)
							rct.Width = Tool.Floor(Owner.ClientToContent(
								new Point(e.X - _delta.X, 0))).X - rct.X;
						//
						if (rct == _box)
							return;
						InvalidateContent(_box);
						_box = rct;
						_selection.Bounds = _box;
						InvalidateContent(_box);
						_tool.RaiseToolStatusChanged(_box);
						break;
				}
				Owner.Update();
			}
			//finish action
			protected override void OnMouseUp(object sender, MouseEventArgs e)
			{
				if (_selection == null)
					return;
				if (_state == State.Destroying)
				{
					_selection.Draw();
					Reset();
				}
				if (_state == State.Constructing)
					_selection.Bounds = _box;
				//refresh
				_state = State.None;
				InvalidateCross();
				UpdateCursor(_state);
				Owner.Update();
			}
			//update tool status
			protected override void OnMouseLeave(object sender, EventArgs e)
			{
				base.OnMouseLeave(sender, e);
				UpdateCursor(State.None);
				_tool.RaiseToolStatusChanged();
			}
			// resets the box
			public void Reset()
			{
				if (_box.Size.IsEmpty)
					return;
				_state = State.None;
				InvalidateContent(_box);
				_box = Rectangle.Empty;
				_tool.RaiseToolStatusChanged();
			}
			#endregion
			//get box from new selection
			public Selection Selection
			{
				get
				{
					return _selection;
				}
				private set
				{
					if (_selection == value)
						return;
					//
					if (_selection != null)
						_selection.BoundsChanged += new EventHandler(Selection_BoundsChanged);
					_selection = value;
					if (_selection != null)
					{
						_selection.AntiAlias = _tool._antialias.Value;
						_selection.BoundsChanged += new EventHandler(Selection_BoundsChanged);
					}
					Selection_BoundsChanged(this, EventArgs.Empty);
				}
			}
			//update the box
			void Selection_BoundsChanged(object sender, EventArgs e)
			{
				if (_state != State.None)
					return;
				InvalidateContent(_box);
				if (_selection != null)
				{
					_box = _selection.Bounds;
					_tool.RaiseToolStatusChanged(_box);
				}
				else
				{
					_box = Rectangle.Empty;
					_tool.RaiseToolStatusChanged();
				}
				InvalidateContent(_box);
			}
		}
		#endregion
		#region variables
		private ConstructionLayer _selection;
		//
		private AntiAliasOption _antialias;
		#endregion
		#region ctor
		public SelectionTool()
		{
			ConfigOptions.Add(_antialias = new AntiAliasOption());
			//
			Layers.Add(_selection = new ConstructionLayer(this));
		}
		#endregion
		protected override void OnContentChange(IconImage value)
		{
			base.OnContentChange(value);
			_selection.Reset();
		}
	}
	/// <summary>
	/// selection class to be used within an editor
	/// </summary>
	public class Selection : IDisposable
	{
		#region types
		/// <summary>
		/// layer that contains the selected image portion
		/// </summary>
		public class SliceLayer : BufferLayer
		{
			#region variables
			private Selection _owner;
			#endregion
			public SliceLayer(Selection owner)
			{
				if (owner == null)
					throw new ArgumentNullException("owner");
				_owner = owner;
			}
			//paint slice at bounds to buffer
			public void Repaint()
			{
				if (!Initialized)
					return;
				using (PaintEventArgs e = CreateArgs())
				{
					e.Graphics.Clear(Color.Transparent);
					if (_owner._slice != null)
					{
						if (_owner._antialias)
							e.Graphics.InterpolationMode = InterpolationMode.Bicubic;
						else
							e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

						e.Graphics.DrawImage(_owner._slice, _owner._bounds);
					}
				}
			}
			/// <summary>
			/// gets the owning selection
			/// </summary>
			public Selection Selection
			{
				get { return _owner; }
			}
		}
		#endregion
		#region variables
		private SliceLayer _slicelayer;
		//
		private Bitmap _slice;
		private Rectangle _bounds;
		private bool _antialias;
		//
		private IconImage _content;
		#endregion
		public Selection()
		{
			_slicelayer = new SliceLayer(this);
		}
		public void Dispose()
		{
			Reset();
			_slicelayer.Dispose();
		}
		#region public members
		private bool Valid(Rectangle bounds)
		{
			return _content != null && bounds.Width > 0 && bounds.Height > 0;
		}
		/// <summary>
		/// grab from the content image and
		/// copy it to slice
		/// </summary>
		public void Grab(Rectangle bounds, bool copy)
		{
			Reset();
			//grab new image
			if (Valid(bounds))
			{
				_bounds = bounds;
				_slice = Copy(bounds);
				//cut out
				if (!copy)
					Delete(bounds);
			}
			_slicelayer.Repaint();
			_slicelayer.InvalidateContent(_bounds);
		}
		public void Paste(Bitmap slice)
		{
			if (slice != null && slice.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
			{
				_slicelayer.InvalidateContent(_bounds);
				Reset();
				_slice = slice;
				Bounds = new Rectangle(_bounds.Location, _slice.Size);
			}
		}
		/// <summary>
		/// copy portion of the content image
		/// </summary>
		public Bitmap Copy(Rectangle bounds)
		{
			if (!Valid(bounds))
				return null;
			Bitmap ret = new Bitmap(bounds.Width, bounds.Height);
			using (Graphics gr = Graphics.FromImage(ret))
				gr.DrawImageUnscaled(_content.Bitmap, -bounds.X, -bounds.Y);
			return ret;
		}
		/// <summary>
		/// delete portion of the content image
		/// </summary>
		public void Delete(Rectangle bounds)
		{
			if (!Valid(bounds))
				return;
			using (Graphics gr = Graphics.FromImage(_content.Bitmap))
			{
				gr.CompositingMode = CompositingMode.SourceCopy;
				gr.FillRectangle(Brushes.Transparent, bounds);
			}
		}
		//commit and reset all
		public void Draw()
		{
			if (_content != null && _slicelayer.Initialized && _slice != null)
			{
				using (Graphics gr = Graphics.FromImage(Content.Bitmap))
					gr.DrawImageUnscaled(_slicelayer.Image, Point.Empty);
				Reset();
				if (ActionCommitted != null)
					ActionCommitted(this, EventArgs.Empty);
			}
			else
				Reset();
		}
		/// <summary>
		/// reset the slice
		/// </summary>
		public void Reset()
		{
			if (_slice != null)
			{
				_slice.Dispose();
				_slice = null;
				_slicelayer.Clear();
			}
			_bounds = Rectangle.Empty;
			if (BoundsChanged != null)
				BoundsChanged(this, EventArgs.Empty);
		}
		#endregion
		#region properties
		/// <summary>
		/// gets the slice layer
		/// </summary>
		public SliceLayer SelectionSliceLayer
		{
			get { return _slicelayer; }
		}
		/// <summary>
		/// gets or sets the content this selection is belonging to
		/// </summary>
		public IconImage Content
		{
			get { return _content; }
			set
			{
				if (value == _content)
					return;
				Draw();
				_content = value;
				_slicelayer.Initialize(value);
			}
		}
		/// <summary>
		/// gets the slice, can be NULL
		/// </summary>
		public Bitmap Slice
		{
			get { return _slice; }
		}
		/// <summary>
		/// gets or sets the bounds to be drawn in
		/// </summary>
		public Rectangle Bounds
		{
			get { return _bounds; }
			set
			{
				if (value == _bounds)
					return;
				_slicelayer.InvalidateContent(_bounds);
				_bounds = value;
				if (_slice != null)
					_slicelayer.Repaint();
				_slicelayer.InvalidateContent(_bounds);
				if (BoundsChanged != null)
					BoundsChanged(this, EventArgs.Empty);
			}
		}
		/// <summary>
		/// gets or sets if antialias is enabled on resizing
		/// </summary>
		public bool AntiAlias
		{
			get { return _antialias; }
			set
			{
				if (_antialias == value)
					return;
				_antialias = value;
				if (_slice != null)
				{
					_slicelayer.Repaint();
					_slicelayer.InvalidateContent(_bounds);
				}
			}
		}
		#endregion
		/// <summary>
		/// raised when selection is drawn
		/// </summary>
		public event EventHandler ActionCommitted;
		public event EventHandler BoundsChanged;
	}
}
