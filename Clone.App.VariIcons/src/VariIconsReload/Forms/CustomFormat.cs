using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ControlsEx.ValueControls;
using VariIconsReload.Properties;

namespace VariIconsReload.Forms
{
	/// <summary>
	/// Zusammenfassung für CustomFormat.
	/// </summary>
	public partial class CustomFormat : System.Windows.Forms.Form
	{
		#region variables
		private short _bitsperpixel = 32;
		private Size _size = new Size(16, 16);
		private bool _editedwidth = false;
		#endregion
		public CustomFormat()
		{
			InitializeComponent();
		}
		#region ui
		//helper
		private bool TryParse(string text, out int result)
		{
			double res = .0;
			bool b =
				double.TryParse(text, System.Globalization.NumberStyles.Integer,
				null, out res);
			result = (int)res;
			return b;
		}

		private void cbWidth_TextChanged(object sender, System.EventArgs e)
		{
			int w;
			if (TryParse(cbWidth.Text, out w))
				ImageWidth = w;
		}

		private void cbWidth_Leave(object sender, EventArgs e)
		{
			cbWidth.Text = _size.Width.ToString();
		}
		private void cbHeight_TextChanged(object sender, System.EventArgs e)
		{
			int h;
			if (TryParse(cbHeight.Text, out h))
				ImageHeight = h;
		}

		private void cbHeight_Leave(object sender, EventArgs e)
		{
			cbHeight.Text = _size.Height.ToString();
		}

		private void scrWidth_ValueChanged(ValueControl sender, ValueChangedEventArgs e)
		{
			ImageWidth = scrWidth.Value;
		}

		private void scrHeight_ValueChanged(ValueControl sender, ValueChangedEventArgs e)
		{
			ImageHeight = scrHeight.Value;
		}
		private void rd_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rd1.Checked)
				_bitsperpixel = 1;
			else if (rd4.Checked)
				_bitsperpixel = 4;
			else if (rd8.Checked)
				_bitsperpixel = 8;
			else if (rd24.Checked)
				_bitsperpixel = 24;
			else if (rd32.Checked)
				_bitsperpixel = 32;
		}

		private void cbLock_CheckedChanged(object sender, EventArgs e)
		{
			if (cbLock.Checked)
			{
				if (_editedwidth)
					ImageHeight = ImageWidth;
				else
					ImageWidth = ImageHeight;
			}
		}
		#endregion
		#region persistence
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			//
			cbLock.Checked = Settings.Default.CustomFormat_Lock;
			BitsPerPixel = Settings.Default.CustomFormat_Bpp;
			ImageWidth = Settings.Default.CustomFormat_ImageWidth;
			ImageHeight = Settings.Default.CustomFormat_ImageHeight;
		}
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			//
			Settings.Default.CustomFormat_Lock = cbLock.Checked;
			Settings.Default.CustomFormat_Bpp = BitsPerPixel;
			Settings.Default.CustomFormat_ImageWidth = ImageWidth;
			Settings.Default.CustomFormat_ImageHeight = ImageHeight;
		}
		#endregion
		/// <summary>
		/// gets selected bits per pixel
		/// </summary>
		[DefaultValue(32)]
		public short BitsPerPixel
		{
			get { return _bitsperpixel; }
			set
			{
				switch (value)
				{
					case 1: rd1.Checked = true; break;
					case 4: rd4.Checked = true; break;
					case 8: rd8.Checked = true; break;
					case 24: rd24.Checked = true; break;
					case 32: rd32.Checked = true; break;
					default: return;
				}
				_bitsperpixel = value;
			}
		}
		/// <summary>
		/// gets or sets the selected width
		/// </summary>
		[DefaultValue(16)]
		public int ImageWidth
		{
			get { return _size.Width; }
			set
			{
				value = Math.Max(1, Math.Min(256, value));
				if (value == _size.Width) return;
				_size.Width = value;
				cbWidth.Text = value.ToString();
				scrWidth.Value = value;
				//size lock
				if (cbLock.Checked && ImageHeight != value)
					ImageHeight = value;
				//last edit
				_editedwidth = true;
			}
		}
		/// <summary>
		/// gets or sets the selected height
		/// </summary>
		[DefaultValue(16)]
		public int ImageHeight
		{
			get { return _size.Height; }
			set
			{
				value = Math.Max(1, Math.Min(256, value));
				if (value == _size.Height) return;
				_size.Height = value;
				cbHeight.Text = value.ToString();
				scrHeight.Value = value;
				//size lock
				if (cbLock.Checked && ImageWidth != value)
					ImageWidth = value;
				//last edit
				_editedwidth = false;
			}
		}
	}
}
