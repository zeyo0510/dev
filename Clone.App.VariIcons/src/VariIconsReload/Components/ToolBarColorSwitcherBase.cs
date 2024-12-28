using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DockingFrames;

namespace VariIconsReload.Components
{
	/// <summary>
	/// toolbar control displaying the current foregorund
	/// and background color allowing to switch between them
	/// </summary>
	[DefaultEvent("SwatchClicked")]
	public abstract class ToolBarColorSwitcherBase : ToolBarControl, IToolBarSpanControl
	{
		#region types
		/// <summary>
		/// area inside the switcher
		/// </summary>
		public enum HitArea
		{
			None = 0, Swap = 1, SwatchA = 2, SwatchB = 3, SwatchSmall = 4
		}
		/// <summary>
		/// eventargs for default event
		/// </summary>
		public class SwatchClickedEventArgs : EventArgs
		{
			private HitArea _hitarea;
			public SwatchClickedEventArgs(HitArea hitarea)
			{
				_hitarea = hitarea;
			}
			public HitArea HitArea
			{
				get { return _hitarea; }
			}
		}
		#endregion
		#region variables
		private Image _swapbutton;
		private HitArea _over = HitArea.None, _down = HitArea.None,
			_selected = HitArea.None;
		private bool _outline_a = true, _outline_b = true;
		#endregion
		public ToolBarColorSwitcherBase()
		{
			//load image resource
			ComponentResourceManager resources =
				new ComponentResourceManager(typeof(ToolBarColorSwitcherBase));
			_swapbutton = resources.GetObject("swap") as Image;
		}
		#region controller
		//centered area to be drawn in
		private Rectangle ContentRectangle
		{
			get
			{
				//evaluate side length of square
				int side = Math.Min(ClientRectangle.Width,
					ClientRectangle.Height) - 2;
				//centered rectangle
				return new Rectangle(
					ClientRectangle.X + (ClientRectangle.Width - side) / 2,
					ClientRectangle.Y + (ClientRectangle.Height - side) / 2,
					side, side);
			}
		}
		//rectangle around swap icon
		private Rectangle GetButtonRectangle(Rectangle rct)
		{
			rct.Y += 2; rct.X = rct.Right - _swapbutton.Width - 1;
			rct.Size = _swapbutton.Size;
			return Rectangle.Inflate(rct, 2, 2);
		}
		//rectangle around swatch A
		private Rectangle GetSwatchARectangle(Rectangle rct)
		{
			return new Rectangle(rct.X + 16, rct.Y + 14, rct.Width - 16, rct.Height - 16);
		}
		//rectangle around swatch B
		private Rectangle GetSwatchBRectangle(Rectangle rct)
		{
			return new Rectangle(rct.X + 2, rct.Y, rct.Width - 16, rct.Height - 16);
		}
		/// <summary>
		/// fills the path with a striketrhough
		/// </summary>
		protected void FillSwatchEmpty(PaintEventArgs e, GraphicsPath pth)
		{
			e.Graphics.FillPath(Brushes.White, pth);
			RectangleF bounds = pth.GetBounds();
			e.Graphics.DrawLine(Pens.Red, bounds.Right, bounds.Y, bounds.X, bounds.Bottom);
		}
		/// <summary>
		/// override this to fill the swatch of color A
		/// </summary>
		protected virtual void FillSwatchA(PaintEventArgs e, GraphicsPath pth)
		{
			FillSwatchEmpty(e, pth);
		}
		/// <summary>
		/// override this to fill the swatch of color B
		/// </summary>
		protected virtual void FillSwatchB(PaintEventArgs e, GraphicsPath pth)
		{
			FillSwatchEmpty(e, pth);
		}
		//paint all
		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle rct = ContentRectangle;
			using (HatchBrush hbrs = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.LightGray, Color.White))
			{
				using (GraphicsPath pth = new GraphicsPath())
				{
					//draw background square
					if (rct.Width > 30)
					{
						Rectangle swatch = GetSwatchBRectangle(rct);
						pth.AddRectangle(swatch);
						if (_outline_b)//leave area in middle
							pth.AddRectangle(Rectangle.Inflate(swatch, -7, -7));
						e.Graphics.FillPath(Brushes.White, pth);
						e.Graphics.DrawPath(Pens.Gray, pth);
						//button if mouse action
						DockManager.DefaultRenderer.DrawButton(e,
							new Rectangle(swatch.X, swatch.Y, swatch.Width + 1, swatch.Height + 1),
							GetButtonState(HitArea.SwatchB), Vertical);
						//fill
						pth.Reset();
						swatch.Inflate(-2, -2); swatch.Width++; swatch.Height++;
						pth.AddRectangle(swatch);
						if (_outline_b)
							pth.AddRectangle(Rectangle.Inflate(swatch, -4, -4));
						e.Graphics.FillPath(hbrs, pth);
						FillSwatchB(e, pth);
					}
					//draw little background square
					pth.Reset();
					pth.AddRectangle(new Rectangle(rct.X + 4, rct.Bottom - 8, 8, 8));
					e.Graphics.FillPath(hbrs, pth);
					FillSwatchB(e, pth);
					e.Graphics.DrawPath(Pens.Gray, pth);
					//
					if (rct.Width > 30)
					{
						//draw foreground square
						Rectangle swatch = GetSwatchARectangle(rct);
						pth.Reset();
						pth.AddRectangle(swatch);
						if (_outline_a)//leave area in middle
							pth.AddRectangle(Rectangle.Inflate(swatch, -7, -7));
						e.Graphics.FillPath(Brushes.White, pth);
						e.Graphics.DrawPath(Pens.Gray, pth);
						//button if mouse action
						DockManager.DefaultRenderer.DrawButton(e,
							new Rectangle(swatch.X, swatch.Y, swatch.Width + 1, swatch.Height + 1),
							GetButtonState(HitArea.SwatchA), Vertical);
						//fill
						pth.Reset();
						swatch.Inflate(-2, -2); swatch.Width++; swatch.Height++;
						pth.AddRectangle(swatch);
						if (_outline_a)
							pth.AddRectangle(Rectangle.Inflate(swatch, -4, -4));
						e.Graphics.FillPath(hbrs, pth);
						FillSwatchA(e, pth);
					}
					//draw little foreground square
					pth.Reset();
					pth.AddRectangle(new Rectangle(rct.X, rct.Bottom - 12, 8, 8));
					e.Graphics.FillPath(hbrs, pth);
					FillSwatchA(e, pth);
					e.Graphics.DrawPath(Pens.Gray, pth);
					//draw swap button
					DockManager.DefaultRenderer.DrawButton(e, GetButtonRectangle(rct),
						GetButtonState(HitArea.Swap), Vertical);
					if (_swapbutton != null)
						e.Graphics.DrawImageUnscaled(_swapbutton, rct.Right - _swapbutton.Width - 1, rct.Y + 2);
				}
			}
		}
		//make sure 40px are available
		public override Size GetPreferredSize(Size constraint)
		{
			if (this.Vertical)
				return new Size(constraint.Width, 40);
			return new Size(40, constraint.Height);
		}
		//stretch across lines
		public bool Stretch
		{
			get { return true; }
		}
		/*
		 * mouse management for swap button
		 */
		//gets the area which the cursor is currently over
		private HitArea GetHitArea(Point location)
		{
			Rectangle rct = ContentRectangle;
			if (GetSwatchARectangle(rct).Contains(location))
				return HitArea.SwatchA;
			else if (GetSwatchBRectangle(rct).Contains(location))
				return HitArea.SwatchB;
			else if (GetButtonRectangle(rct).Contains(location))
				return HitArea.Swap;
			return HitArea.None;
		}
		//gets the current buttonstate for the given area
		private ButtonState GetButtonState(HitArea target)
		{
			if (_over == target)
				return _down == target ? ButtonState.Pushed : ButtonState.Checked;
			else
				return _down == target ? ButtonState.Checked :
					(_selected == target) ? ButtonState.Checked | ButtonState.Pushed :
					ButtonState.Normal;
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && _over != HitArea.None)
			{
				_down = _over;
				Refresh();
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			HitArea over = GetHitArea(e.Location);
			if (over != _over)
			{
				//if pushed and dragged out, become hot
				if (_down == HitArea.None || _down == over)
					_over = over;
				else _over = HitArea.None;
				Refresh();
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (_down != HitArea.None)
			{
				if (_down == _over && SwatchClicked != null)
					SwatchClicked(this, new SwatchClickedEventArgs(_down));
				_down = HitArea.None;
				Refresh();
			}
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			if (_over != HitArea.None)
			{
				_over = HitArea.None;
				Refresh();
			}
		}
		#endregion
		#region properties
		/// <summary>
		/// gets or sets if swatch A is outline mode
		/// </summary>
		[DefaultValue(true)]
		public bool OutlineA
		{
			get { return _outline_a; }
			set
			{
				if (value == _outline_a)
					return;
				_outline_a = value;
				this.Refresh();
			}
		}
		/// <summary>
		/// gets or sets if swatch B is outline mode
		/// </summary>
		[DefaultValue(true)]
		public bool OutlineB
		{
			get { return _outline_b; }
			set
			{
				if (value == _outline_b)
					return;
				_outline_b = value;
				this.Refresh();
			}
		}
		/// <summary>
		/// gets or sets if swatch A is selected
		/// </summary>
		[DefaultValue(typeof(HitArea), "None")]
		public HitArea SelectedArea
		{
			get { return _selected; }
			set
			{
				if (value == HitArea.Swap)
					value = HitArea.None;
				if (value == _selected)
					return;
				_selected = value;
				this.Refresh();
			}
		}
		#endregion
		/// <summary>
		/// raised when the little switch button is clicked
		/// </summary>
		public event EventHandler<SwatchClickedEventArgs> SwatchClicked;
	}
	/// <summary>
	/// toolbar control displaying two colors
	/// </summary>
	public class ToolBarColorSwitcher : ToolBarColorSwitcherBase
	{
		#region variables
		private Color _color_a = Color.Black, _color_b = Color.White;
		#endregion
		#region controller
		protected override void FillSwatchA(PaintEventArgs e, GraphicsPath pth)
		{
			if (_color_a == Color.Transparent)
				base.FillSwatchEmpty(e, pth);
			else
				using (SolidBrush brs = new SolidBrush(_color_a))
				{
					e.Graphics.FillPath(brs, pth);
				}
		}
		protected override void FillSwatchB(PaintEventArgs e, GraphicsPath pth)
		{
			if (_color_b == Color.Transparent)
				base.FillSwatchEmpty(e, pth);
			else
				using (SolidBrush brs = new SolidBrush(_color_b))
				{
					e.Graphics.FillPath(brs, pth);
				}
		}
		#endregion
		#region properties
		/// <summary>
		/// gets or sets the foreground color
		/// </summary>
		[DefaultValue(typeof(Color), "Black")]
		public Color ColorA
		{
			get { return _color_a; }
			set
			{
				if (value == _color_a) return;
				_color_a = value;
				this.Refresh();
			}
		}
		/// <summary>
		/// gets or sets the background color
		/// </summary>
		[DefaultValue(typeof(Color), "White")]
		public Color ColorB
		{
			get { return _color_b; }
			set
			{
				if (value == _color_b) return;
				_color_b = value;
				this.Refresh();
			}
		}
		#endregion
	}
}
