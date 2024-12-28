using System;
using System.Drawing;
using System.Windows.Forms;
using ControlsEx;

namespace VariIconsReload.Components
{
	/// <summary>
	/// layer constructing and modifying a box,
	/// for example for selection and such
	/// </summary>
	public class BoxLayer : MouseLayer
	{
		#region variables
		private Rectangle _box;
		private BoxState _state = BoxState.None;
		private Point _delta;
		private Actions _allowed = Actions.All;
		#endregion
		#region controller
		//draw a single handle
		private void DrawHandle(PaintEventArgs e, float x, float y, float w, Pen pn)
		{
			RectangleF bnd = new RectangleF(x - 2f * w, y - 2f * w, w * 4f, w * 4f);
			e.Graphics.FillRectangle(Brushes.White, bnd);
			e.Graphics.DrawRectangle(pn, bnd.X, bnd.Y, bnd.Width, bnd.Height);
		}
		//draw the box outline and inner layer
		protected virtual void DrawBox(PaintEventArgs e, Rectangle bounds, float w)
		{
			using (SolidBrush brs = new SolidBrush(
				Color.FromArgb(128, SystemColors.Highlight)))
			{
				e.Graphics.FillRectangle(brs, bounds);
			}
			using (Pen pn = new Pen(Color.FromArgb(200, 255, 255, 255), w * 3f))
			{
				//draw base
				e.Graphics.DrawRectangle(pn, bounds);
				//draw top
				pn.Color = Color.Black;
				pn.Width = w;
				pn.DashPattern = new float[] { w * 4f, w * 4f };
				e.Graphics.DrawRectangle(pn, bounds);
			}
		}
		//draw construction and selection layer
		protected internal override void OnPaint(PaintEventArgs e)
		{
			//display box
			if (!_box.Size.IsEmpty)
			{
				Rectangle bounds = GraphicsEx.SortedRectangle(_box);
				float w = Owner.RealZoom.Unscale(1f);

				DrawBox(e, bounds, w);
				if (Can(Actions.Resize))
				{
					using (Pen pn = new Pen(Color.Navy, w))
					{
						//draw handles
						DrawHandle(e, bounds.X, bounds.Y, w, pn);
						DrawHandle(e, bounds.Right, bounds.Y, w, pn);
						DrawHandle(e, bounds.Right, bounds.Bottom, w, pn);
						DrawHandle(e, bounds.X, bounds.Bottom, w, pn);
						//mid handles
						if (Can(Actions.Skew))
						{
							DrawHandle(e, bounds.X + bounds.Width / 2f, bounds.Y, w, pn);
							DrawHandle(e, bounds.Right, bounds.Y + bounds.Height / 2f, w, pn);
							DrawHandle(e, bounds.X + bounds.Width / 2f, bounds.Bottom, w, pn);
							DrawHandle(e, bounds.X, bounds.Y + bounds.Height / 2f, w, pn);
						}
					}
				}
			}
		}
		//is desired action valid?
		private bool Can(Actions value)
		{
			return (_allowed & value) != 0;
		}
		//gets the recommended state for the location
		private BoxState GetNewState(Point client)
		{
			if (_box.IsEmpty)
				return Can(Actions.Construct) ? BoxState.Constructing : BoxState.None;
			else
			{
				Rectangle clbox = Rectangle.Round(Owner.ContentToClient(_box));
				//check handles first
				if (Can(Actions.Resize))
				{
					if (new Rectangle(clbox.X - 2, clbox.Y - 2, 4, 4).Contains(client))
						return BoxState.ResizeNW;
					else if (new Rectangle(clbox.Right - 2, clbox.Y - 2, 4, 4).Contains(client))
						return BoxState.ResizeNE;
					else if (new Rectangle(clbox.Right - 2, clbox.Bottom - 2, 4, 4).Contains(client))
						return BoxState.ResizeSE;
					else if (new Rectangle(clbox.X - 2, clbox.Bottom - 2, 4, 4).Contains(client))
						return BoxState.ResizeSW;
					//mid handles
					if (Can(Actions.Skew))
					{
						int mw = clbox.X + clbox.Width / 2,
							mh = clbox.Y + clbox.Height / 2;
						if (new Rectangle(mw - 2, clbox.Y - 2, 4, 4).Contains(client))
							return BoxState.ResizeN;
						else if (new Rectangle(clbox.Right - 2, mh - 2, 4, 4).Contains(client))
							return BoxState.ResizeE;
						else if (new Rectangle(mw - 2, clbox.Bottom - 2, 4, 4).Contains(client))
							return BoxState.ResizeS;
						else if (new Rectangle(clbox.X - 2, mh - 2, 4, 4).Contains(client))
							return BoxState.ResizeW;
					}
				}
				//then check box
				if (!clbox.Contains(client))
					return Can(Actions.Destroy) ? BoxState.Destroying : BoxState.None;
				else
					return Can(Actions.Move) ? BoxState.Moving : BoxState.None;
			}
		}
		//updates the cursor according to state
		private void UpdateCursor(BoxState state)
		{
			switch (state)
			{
				case BoxState.Constructing:
					Owner.Cursor = Cursors.Cross; break;
				case BoxState.Destroying:
					Owner.Cursor = Cursors.No; break;
				case BoxState.Moving:
					Owner.Cursor = Cursors.SizeAll; break;
				case BoxState.ResizeNW:
				case BoxState.ResizeSE:
					Owner.Cursor = Cursors.SizeNWSE; break;
				case BoxState.ResizeNE:
				case BoxState.ResizeSW:
					Owner.Cursor = Cursors.SizeNESW; break;
				case BoxState.ResizeN:
				case BoxState.ResizeS:
					Owner.Cursor = Cursors.SizeNS; break;
				case BoxState.ResizeE:
				case BoxState.ResizeW:
					Owner.Cursor = Cursors.SizeWE; break;
				default:
					Owner.Cursor = Cursors.Default;
					break;
			}
		}
		//start constructing or moving
		protected override void OnMouseDown(object sender, MouseEventArgs e)
		{
			_state = GetNewState(e.Location);
			switch (_state)
			{
				case BoxState.Constructing:
					//construct new box
					_delta = Owner.ClientToContent(e.Location);
					Box = new Rectangle(_delta, new Size(1, 1));
					Owner.Cursor = Cursors.SizeNWSE;
					OnBeginConstruction();
					break;
				case BoxState.Moving:
				case BoxState.ResizeE:
				case BoxState.ResizeN:
				case BoxState.ResizeNE:
				case BoxState.ResizeNW:
				case BoxState.ResizeS:
				case BoxState.ResizeSE:
				case BoxState.ResizeSW:
				case BoxState.ResizeW:
					//cut out or copy
					OnBeginConstruction();
					//moving or resizing
					Rectangle cbox = Rectangle.Round(Owner.ContentToClient(_box));
					//vertical, top middle or bottom
					if ((_state & BoxState.ResizeN) != 0 || _state == BoxState.Moving)
						_delta.Y = e.Y - cbox.Y;
					else if ((_state & BoxState.ResizeS) != 0)
						_delta.Y = e.Y - cbox.Bottom;
					//horizontal, left middle or right
					if ((_state & BoxState.ResizeW) != 0 || _state == BoxState.Moving)
						_delta.X = e.X - cbox.X;
					else if ((_state & BoxState.ResizeE) != 0)
						_delta.X = e.X - cbox.Right;
					break;
			}
		}
		//transform or construct the selection area
		protected override void OnMouseMove(object sender, MouseEventArgs e)
		{
			switch (_state)
			{
				case BoxState.None:
					//mouse pointer feedback
					UpdateCursor(GetNewState(e.Location));
					return;
				case BoxState.Constructing:
					//resize box and update
					Point loc = Owner.ClientToContent(e.Location);
					//make positive size
					Rectangle value = GraphicsEx.SortedRectangle(
						Rectangle.FromLTRB(_delta.X, _delta.Y, loc.X, loc.Y));
					value.Width++; value.Height++;
					Box = value;
					break;
				case BoxState.Moving:
					//move box and update
					Box = new Rectangle(
						Owner.ClientToContent(
							new Point(e.X - _delta.X, e.Y - _delta.Y)),
						_box.Size);
					break;
				case BoxState.ResizeE:
				case BoxState.ResizeN:
				case BoxState.ResizeNE:
				case BoxState.ResizeNW:
				case BoxState.ResizeS:
				case BoxState.ResizeSE:
				case BoxState.ResizeSW:
				case BoxState.ResizeW:
					Point pt = Owner.ClientToContent(
							new Point(e.X - _delta.X, e.Y - _delta.Y));
					Rectangle rct = _box;
					//vertical, top middle or bottom
					if ((_state & BoxState.ResizeN) != 0)
					{
						rct.Y = pt.Y;
						rct.Height = _box.Bottom - rct.Y;
					}
					else if ((_state & BoxState.ResizeS) != 0)
						rct.Height = pt.Y - rct.Y;
					//horizontal, left middle or right
					if ((_state & BoxState.ResizeW) != 0)
					{
						rct.X = pt.X;
						rct.Width = _box.Right - rct.X;
					}
					else if ((_state & BoxState.ResizeE) != 0)
						rct.Width = pt.X - rct.X;

					Box = rct;
					break;
			}
		}
		//finish action
		protected override void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (_state == BoxState.Destroying)
				Box = Rectangle.Empty;
			OnEndConstruction();
			//refresh
			_state = BoxState.None;
			UpdateCursor(_state);
		}
		//update tool status
		protected override void OnMouseLeave(object sender, EventArgs e)
		{
			UpdateCursor(BoxState.None);
		}
		#endregion
		#region virtual
		protected virtual void OnConstrainBox(ref Rectangle value)
		{
			if (ConstrainBox != null)
			{
				RectangleEventArgs e = new RectangleEventArgs(value);
				ConstrainBox(this, e);
				value = e.Rectangle;
			}
		}
		protected virtual void OnBoxChanged()
		{
			if (BoxChanged != null)
				BoxChanged(this, EventArgs.Empty);
		}
		protected virtual void OnBoxChanging()
		{
			if (BoxChanging != null)
				BoxChanging(this, EventArgs.Empty);
		}
		protected virtual void OnBeginConstruction()
		{
			if (BeginConstruction != null)
				BeginConstruction(this, EventArgs.Empty);
		}
		protected virtual void OnEndConstruction()
		{
			if (EndConstruction != null)
				EndConstruction(this, EventArgs.Empty);
		}
		#endregion
		#region properties
		/// <summary>
		/// gets the current state of the layer
		/// </summary>
		public BoxState State
		{
			get { return _state; }
		}
		/// <summary>
		/// gets or sets the allowed states for this layer
		/// </summary>
		public Actions AllowedActions
		{
			get { return _allowed; }
			set
			{
				if (value == _allowed)
					return;
				_allowed = value;
				InvalidateContent(_box);
			}
		}
		/// <summary>
		/// gets or sets the box.
		/// causes boxchanged and constrain events
		/// </summary>
		public Rectangle Box
		{
			get { return _box; }
			set
			{
				OnConstrainBox(ref value);
				if (value == _box)
					return;
				OnBoxChanging();
				InvalidateContent(_box);
				_box = value;
				InvalidateContent(value);
				Update();
				OnBoxChanged();
			}
		}
		#endregion
		#region events
		public event EventHandler<RectangleEventArgs> ConstrainBox;
		public event EventHandler BoxChanged;
		public event EventHandler BoxChanging;
		public event EventHandler BeginConstruction;
		public event EventHandler EndConstruction;
		#endregion
	}
	/// <summary>
	/// states for selectionlayer
	/// </summary>
	public enum BoxState
	{
		None = 0, Constructing = 1, Moving = 2,
		ResizeN = 4, ResizeE = 8, ResizeS = 16, ResizeW = 32,
		ResizeNE = 12, ResizeSE = 24, ResizeSW = 48, ResizeNW = 36,
		ResizeWidth = ResizeE | ResizeW, ResizeHeight = ResizeN | ResizeS,
		Destroying = 64
	}
	/// <summary>
	/// allowed actions for box layer
	/// </summary>
	public enum Actions
	{
		Move = 1, Resize = 2, Skew = 4, Construct = 8, Destroy = 16,
		GrabHandle = Move | Resize | Skew,
		All = GrabHandle | Construct | Destroy
	}
	/// <summary>
	/// event args for constraining
	/// </summary>
	public class RectangleEventArgs : EventArgs
	{
		public Rectangle Rectangle;
		public RectangleEventArgs(Rectangle rct)
		{
			Rectangle = rct;
		}
	}
}
