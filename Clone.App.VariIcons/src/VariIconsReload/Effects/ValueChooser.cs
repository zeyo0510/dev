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
	//To edit this with designer, replace GroupBox temporaryly by UserControl
	/// <summary>
	/// combined value chooser tool
	/// </summary>
	public partial class ValueChooser : GroupBox
	{
		private int _default = 0;

		public ValueChooser()
		{
			InitializeComponent();
		}
		#region controller
		private void reset_Click(object sender, EventArgs e)
		{
			track.Value = upDown.Value = _default;
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}

		private void upDown_ValueChanged(ControlsEx.ValueControls.ValueControl sender, ValueChangedEventArgs e)
		{
			track.Value = upDown.Value;
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}

		private void track_ValueChanged(object sender, EventArgs e)
		{
			upDown.Value = track.Value;
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}
		#endregion
		#region properties
		[DefaultValue(100)]
		public int Maximum
		{
			get { return track.Maximum; }
			set
			{
				track.Maximum = value;
				upDown.Maximum = track.Maximum;
				_default = Math.Max(track.Minimum,
					Math.Min(track.Maximum, _default));
			}
		}
		[DefaultValue(0)]
		public int Minimum
		{
			get { return track.Minimum; }
			set
			{
				track.Minimum = value;
				upDown.Minimum = track.Minimum;
				_default = Math.Max(track.Minimum,
					Math.Min(track.Maximum, _default));
			}
		}
		[DefaultValue(0)]
		public int DefaultValue
		{
			get { return _default; }
			set
			{
				_default = Math.Max(track.Minimum,
					Math.Min(track.Maximum, value));
			}
		}
		[DefaultValue(0)]
		public int Value
		{
			get { return track.Value; }
			set
			{
				track.Value = value;
				upDown.Value = track.Value;
			}
		}
		[DefaultValue(10)]
		public int TickFrequency
		{
			get { return track.TickFrequency; }
			set { track.TickFrequency = value; }
		}
		#endregion
		public event EventHandler ValueChanged;
	}
}
