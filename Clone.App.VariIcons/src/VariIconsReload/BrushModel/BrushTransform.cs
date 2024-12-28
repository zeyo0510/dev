using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ControlsEx;
using DockingFrames;
using DrawingEx.Drawing3D;

namespace VariIconsReload.BrushModel
{
	/// <summary>
	/// control applying transformations to a given gradientbrush
	/// </summary>
	public class BrushTransform : Control
	{
		#region types
		private enum HitArea
		{
			None = -1, AngleButton = 0, ScaleButton = 1,
			OffsetButton = 2, GradientArea = 3
		}
		#endregion
		#region variables
		private Angle _startangle;
		private ScaleFactor _startscale;
		private PointF _startoffset;
		private GradientWrapperBrush _brush;
		//ui
		private HitArea _hit = HitArea.None;
		private Point _downpos;
		#endregion
		#region ctor
		public BrushTransform()
		{
			this.SetStyle(ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.ResizeRedraw |
				ControlStyles.Selectable |
				ControlStyles.UserMouse |
				ControlStyles.SupportsTransparentBackColor, true);
		}
		#endregion
		#region controller
		//paints brush, header and parameters
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (_brush != null)
			{
				Rectangle box = this.ClientRectangle;
				box.Y += 16; box.Height -= 16;
				//background
				using (HatchBrush hbrs = new HatchBrush(HatchStyle.LargeCheckerBoard,
					Color.Silver, Color.White))
				{
					e.Graphics.FillRectangle(hbrs, box);
				}
				//gradient
				e.Graphics.FillRectangle(_brush.GetOutBrush(box), box);
				//parameters
				String[] parameters = new String[] {
				_brush.Angle.ToDegree().ToString() + "°", 
				_brush.Scale.ToString(),
				"X:"+_brush.Offset.X.ToString("0.00")+
					" Y:"+_brush.Offset.Y.ToString("0.00") };
				//
				Rectangle bounds = new Rectangle(1, 1, this.ClientSize.Width - 2, HeaderHeight);
				Rectangle rct = new Rectangle(bounds.X, bounds.Y,
					bounds.Width / parameters.Length, bounds.Height);
				DockManager.DefaultRenderer.DrawToolBarBackground(e, bounds, false, false);
				for (int i = 0; i < parameters.Length; i++, rct.X += rct.Width)
				{
					if (i == (int)_hit)
						DockManager.DefaultRenderer.DrawButton(e,
							Rectangle.Inflate(rct, -1, -1),
							Control.MouseButtons == MouseButtons.Left ?
								ButtonState.Pushed : ButtonState.Checked, false);
					DockManager.DefaultRenderer.DrawText(e, parameters[i], rct, StringAlignment.Center, false, true);
				}
			}
			//frame
			e.Graphics.DrawRectangle(Pens.Gray,
				0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
		}
		//gets the width of the header
		private int HeaderHeight
		{
			get { return (int)Math.Ceiling(this.Font.Height * 1.2f); }
		}
		//gets which area the specified point is
		private HitArea getHitArea(Point pos)
		{
			if (_brush == null)
				return HitArea.None;
			if (pos.Y > HeaderHeight)
				return HitArea.GradientArea;
			else if (pos.X < this.ClientSize.Width / 3)
				return HitArea.AngleButton;
			else if (pos.X > (2 * this.ClientSize.Width) / 3)
				return HitArea.OffsetButton;
			else return HitArea.ScaleButton;
		}
		//updates the cursor icon according to the current hit area
		private void UpdateCursor()
		{
			switch (_hit)
			{
				case HitArea.ScaleButton:
				case HitArea.AngleButton:
					this.Cursor = Cursors.SizeWE; break;
				case HitArea.GradientArea:
					this.Cursor = Cursors.Hand; break;
				default:
					this.Cursor = Cursors.Default; break;
			}
		}
		//handles parameter change notification from brush
		private void brs_Update(object sender, EventArgs e)
		{
			this.Refresh();
		}
		//start dragging parameters
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (_brush != null)
			{
				_downpos = e.Location;
				_startangle = _brush.Angle;
				_startscale = _brush.Scale;
				_startoffset = _brush.Offset;
				this.Refresh();
			}
		}
		//update dragging parameters or change cursor
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button == MouseButtons.None)
			{
				//change cursor
				HitArea hit = getHitArea(e.Location);
				if (hit != _hit)
				{
					_hit = hit;
					UpdateCursor();
					Refresh();
				}
			}
			else if (_brush != null)
			{
				//update parameters
				switch (_hit)
				{
					case HitArea.AngleButton:
						int ang = _startangle.ToDegree() + e.X - _downpos.X;
						//snap to 45°
						if (Control.ModifierKeys == Keys.Shift)
							ang = (ang / 45) * 45;
						_brush.Angle = Angle.FromDegree(ang);
						break;
					case HitArea.ScaleButton:
						_brush.Scale = ScaleFactor.FromDouble(
							Math.Max(0.1, _startscale.ToDouble() + (double)(e.X - _downpos.X) / 100.0));
						break;
					case HitArea.GradientArea:
						SizeF delta = new SizeF((float)(e.X - _downpos.X) / Math.Max(1f, (float)this.Width),
							(float)(e.Y - _downpos.Y) / Math.Max(1f, (float)(this.Height - HeaderHeight)));
						//lock x or y
						if (Control.ModifierKeys == Keys.Shift)
						{
							if (Math.Abs(delta.Width) > Math.Abs(delta.Height))
								delta.Height = 0;
							else delta.Width = 0;
						}
						_brush.Offset = PointF.Add(_startoffset, delta);
						break;
				}
			}
		}
		//update appearence
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			Refresh();
		}
		//update cursor
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_hit = HitArea.None;
			UpdateCursor();
			Refresh();
		}
		//reset clicked parameter
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick(e);
			if (_brush != null)
			{
				switch (_hit)
				{
					case HitArea.AngleButton:
						_brush.Angle = Angle.Empty;
						break;
					case HitArea.ScaleButton:
						_brush.Scale = ScaleFactor.Identity;
						break;
					case HitArea.OffsetButton:
						_brush.Offset = PointF.Empty;
						break;
				}
			}
		}
		//change angle by 1 degree steps
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			//increase or decrease angle in one degree steps
			if (_brush != null && e.Button == MouseButtons.None)
				_brush.Angle = Angle.FromDegree(_brush.Angle.ToDegree() + Math.Sign(e.Delta));
		}
		#endregion
		#region properties
		/// <summary>
		/// gets or sets the given gradient brush
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GradientWrapperBrush GradientBrush
		{
			get { return _brush; }
			set
			{
				if (value == _brush)
					return;
				if (_brush != null)
					_brush.Update -= new EventHandler(brs_Update);
				_brush = value;
				if (value != null)
					value.Update += new EventHandler(brs_Update);
				this.Refresh();
			}
		}
		#endregion
	}
}
