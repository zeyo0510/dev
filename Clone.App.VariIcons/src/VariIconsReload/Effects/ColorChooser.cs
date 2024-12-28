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
	/// chooser for a color and alpha value
	/// </summary>
	public partial class ColorChooser : GroupBox
	{
		private Color _default;
		public ColorChooser()
		{
			InitializeComponent();
		}
		#region controller
		private void btnReset_Click(object sender, EventArgs e)
		{
			Value = _default;
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}

		private void cbtn_Click(object sender, EventArgs e)
		{
			cDialog.Color = cbtn.Color;
			if (cDialog.ShowDialog() == DialogResult.OK)
			{
				cbtn.Color = cDialog.Color;
				if (ValueChanged != null)
					ValueChanged(this, EventArgs.Empty);
			}
		}

		private void cbtn_ColorChanged(object sender, EventArgs e)
		{
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}

		private void scrAlpha_ValueChanged(ValueControl sender, ValueChangedEventArgs e)
		{
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}
		#endregion
		#region properties
		public Color DefaultValue
		{
			get { return _default; }
			set { _default = value; }
		}
		public Color Value
		{
			get { return Color.FromArgb(scrAlpha.Value, cbtn.Color); }
			set
			{
				cbtn.Color = Color.FromArgb(255, value);
				scrAlpha.Value = value.A;
			}
		}
		#endregion
		public event EventHandler ValueChanged;
	}
}
