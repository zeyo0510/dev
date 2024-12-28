using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VariIconsReload.Components;
using System.ComponentModel;

namespace VariIconsReload.BrushModel
{
	/// <summary>
	/// toolbar switcher control for displaying the contents of two wrapperbrushes
	/// </summary>
	public class ToolBarBrushSwitcher : ToolBarColorSwitcherBase
	{
		#region variables
		private WrapperBrush _brs_A, _brs_B;
		#endregion
		#region controller
		protected override void FillSwatchA(PaintEventArgs e, GraphicsPath pth)
		{
			if (_brs_A == null)
				base.FillSwatchEmpty(e, pth);
			else
				e.Graphics.FillPath(_brs_A.GetOutBrush(Rectangle.Round(pth.GetBounds())), pth);
		}
		protected override void FillSwatchB(PaintEventArgs e, GraphicsPath pth)
		{
			if (_brs_B == null)
				base.FillSwatchEmpty(e, pth);
			else
				e.Graphics.FillPath(_brs_B.GetOutBrush(Rectangle.Round(pth.GetBounds())), pth);
		}
		private void _brs_Update(object sender, EventArgs e)
		{
			this.Refresh();
		}
		#endregion
		#region properties
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WrapperBrush BrushA
		{
			get { return _brs_A; }
			set
			{
				if (_brs_A == value)
					return;
				if (_brs_A != null)
					_brs_A.Update -= new EventHandler(_brs_Update);
				_brs_A = value;
				if (value != null)
					value.Update += new EventHandler(_brs_Update);
				this.Refresh();
			}
		}
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WrapperBrush BrushB
		{
			get { return _brs_B; }
			set
			{
				if (_brs_B == value)
					return;
				if (_brs_B != null)
					_brs_B.Update -= new EventHandler(_brs_Update);
				_brs_B = value;
				if (value != null)
					value.Update += new EventHandler(_brs_Update);
				this.Refresh();
			}
		}
		#endregion
	}
}
