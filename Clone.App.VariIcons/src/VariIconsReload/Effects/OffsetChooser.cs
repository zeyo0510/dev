using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ControlsEx.ValueControls;

namespace VariIconsReload.Effects
{
	/// <summary>
	/// effect config for a point offset value
	/// </summary>
	public partial class OffsetChooser : GroupBox
	{
		private Point _default;
		public OffsetChooser()
		{
			InitializeComponent();
		}
		#region controller
		private Point Clamp(Point value)
		{
			return new Point(
				Math.Max(xUpDown.Minimum, Math.Min(xUpDown.Maximum, value.X)),
				Math.Max(yUpDown.Minimum, Math.Min(yUpDown.Maximum, value.Y)));
		}
		private void updown_ValueChanged(ValueControl sender, ValueChangedEventArgs e)
		{
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			Value = _default;
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}

		#endregion
		#region properties
		public Rectangle ValueBounds
		{
			get
			{
				return new Rectangle(xUpDown.Minimum, yUpDown.Minimum,
					xUpDown.Maximum - xUpDown.Minimum, yUpDown.Maximum - yUpDown.Minimum);
			}
			set
			{
				xUpDown.Minimum = value.X;
				xUpDown.Maximum = value.Right;
				yUpDown.Minimum = value.Y;
				yUpDown.Maximum = value.Bottom;
				_default = Clamp(_default);
			}
		}
		public Point DefaultValue
		{
			get { return _default; }
			set { _default = Clamp(value); }
		}
		public Point Value
		{
			get { return new Point(xUpDown.Value, yUpDown.Value); }
			set
			{
				xUpDown.Value = value.X;
				yUpDown.Value = value.Y;
			}
		}
		#endregion
		public event EventHandler ValueChanged;
	}
}
