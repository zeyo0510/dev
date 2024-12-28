using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ControlsEx.ValueControls;
using VariIconsReload.Properties;

namespace VariIconsReload.Forms
{
	/// <summary>
	/// a form for selecting a new size with anchoring
	/// anc scaling information
	/// </summary>
	public partial class DocumentResize : Form
	{
		#region variables
		private const int MAXWIDTH = 256, MAXHEIGHT = 256;
		//
		private Size _size = new Size(1, 1),
			_newsize = new Size(1, 1);
		private AnchorStyles _anchor = AnchorStyles.None;
		private bool _lastchangedwidth = false;
		#endregion
		public DocumentResize()
		{
			InitializeComponent();
		}
		#region controller
		//enable interpolation combo
		private void cbScale_CheckedChanged(object sender, EventArgs e)
		{
			cmbScale.Enabled = cbScale.Checked;
			//disable anchor
			ancCT.Enabled = ancN.Enabled = ancNE.Enabled = ancE.Enabled =
				ancSE.Enabled = ancS.Enabled = ancSW.Enabled = ancW.Enabled =
				ancNW.Enabled = !cbScale.Checked;
		}
		//size changed, validate
		private void ud_ValueChanged(ValueControl sender, ValueChangedEventArgs e)
		{
			_lastchangedwidth = sender == udWidth;
			EnterDocumentSize(sender.Value, _lastchangedwidth);
			Sync();
			ApplyAnchorImages();
		}
		//unit changed, validate
		private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
			_lastchangedwidth = sender == cmbWidthUnit;
			//synchronize units
			if (_lastchangedwidth)
				cmbHeightUnit.SelectedIndex = cmbWidthUnit.SelectedIndex;
			else
				cmbWidthUnit.SelectedIndex = cmbHeightUnit.SelectedIndex;
			//recalculate value
			ApplyDocumentSize();
		}
		//relative changed, revalidate values
		private void cbRelative_CheckedChanged(object sender, EventArgs e)
		{
			ApplyDocumentSize();
		}
		//copy last changed value
		private void cbLock_CheckedChanged(object sender, EventArgs e)
		{
			Sync();
			ApplyAnchorImages();
		}
		//enters a value from the controls to newsize
		private void EnterDocumentSize(int value, bool width)
		{
			int dw = 0, dh = 0;
			if (cbRelative.Checked)
			{
				dw = _size.Width; dh = _size.Height;
			}
			if (cmbHeightUnit.SelectedIndex == 0)
			{
				//pixels
				if (width) _newsize.Width = value + dw;
				else _newsize.Height = value + dh;
			}
			else
			{
				//percent
				if (width)
					_newsize.Width = Math.Max(1, Math.Min(MAXWIDTH,
						_size.Width * (value + dw) / 100));
				else
					_newsize.Height = Math.Max(1, Math.Min(MAXHEIGHT,
						_size.Height * (value + dh) / 100));
			}
		}
		//brings the value in newsize to the controls
		private void ApplyDocumentSize()
		{
			int dw = 0, dh = 0;
			if (cbRelative.Checked)
			{
				dw = _size.Width; dh = _size.Height;
			}
			if (cmbHeightUnit.SelectedIndex == 0)
			{
				//pixels
				udWidth.Minimum = 1 - dw;
				udWidth.Maximum = MAXWIDTH - dw;
				udWidth.Value = _newsize.Width - dw;
				//
				udHeight.Minimum = 1 - dh;
				udHeight.Maximum = MAXHEIGHT - dh;
				udHeight.Value = _newsize.Height - dh;
			}
			else
			{
				//percent
				udWidth.Maximum = (100 * (MAXWIDTH - dw)) / _size.Width;
				udWidth.Minimum = 100 * (1 - dw) / _size.Width;
				udWidth.Value = (100 * (_newsize.Width - dw)) / _size.Width;
				//
				udHeight.Maximum = (100 * (MAXHEIGHT - dh)) / _size.Height;
				udHeight.Minimum = 100 * (1 - dh) / _size.Height;
				udHeight.Value = (100 * (_newsize.Height - dh)) / _size.Height;
			}
		}
		//syncs width and height
		private void Sync()
		{
			if (cbLock.Checked)
			{
				if (_lastchangedwidth)
					_newsize.Height = _newsize.Width;
				else
					_newsize.Width = _newsize.Height;
				ApplyDocumentSize();
			}
		}
		//anchors selected, update images
		private void anc_CheckedChanged(object sender, EventArgs e)
		{
			//only update on checking phase
			if (!((RadioButton)sender).Checked)
				return;
			//combine flags
			_anchor = AnchorStyles.None;
			if (ancNW.Checked || ancN.Checked || ancNE.Checked)
				_anchor |= AnchorStyles.Top;
			if (ancNE.Checked || ancE.Checked || ancSE.Checked)
				_anchor |= AnchorStyles.Right;
			if (ancSE.Checked || ancS.Checked || ancSW.Checked)
				_anchor |= AnchorStyles.Bottom;
			if (ancSW.Checked || ancW.Checked || ancNW.Checked)
				_anchor |= AnchorStyles.Left;
			//
			ApplyAnchorImages();
		}
		//make anchor buttons apply value and update images
		private void ApplyAnchorChecks()
		{
			switch (_anchor)
			{
				case AnchorStyles.Top: ancN.Checked = true; break;
				case AnchorStyles.Top | AnchorStyles.Right: ancNE.Checked = true; break;
				case AnchorStyles.Right: ancE.Checked = true; break;
				case AnchorStyles.Right | AnchorStyles.Bottom: ancSE.Checked = true; break;
				case AnchorStyles.Bottom: ancS.Checked = true; break;
				case AnchorStyles.Bottom | AnchorStyles.Left: ancSW.Checked = true; break;
				case AnchorStyles.Left: ancW.Checked = true; break;
				case AnchorStyles.Left | AnchorStyles.Top: ancNW.Checked = true; break;
				default: ancCT.Checked = true; break;
			}
		}
		//update images by a flagging algorithm
		private void ApplyAnchorImages()
		{
			bool w = _newsize.Width >= _size.Width,
				h = _newsize.Height >= _size.Height;
			//upper row
			ancNW.ImageKey = ancN.ImageKey = ancNE.ImageKey =
				(_anchor & AnchorStyles.Top) != 0 ? "" :
				(_anchor & AnchorStyles.Bottom) != 0 ? " " : (h ? "N" : "S");
			//mid row
			ancW.ImageKey = ancCT.ImageKey = ancE.ImageKey =
				(_anchor & AnchorStyles.Top) != 0 ? (h ? "S" : "N") :
				(_anchor & AnchorStyles.Bottom) != 0 ? (h ? "N" : "S") : "";
			//lower row
			ancSW.ImageKey = ancS.ImageKey = ancSE.ImageKey =
				(_anchor & AnchorStyles.Top) != 0 ? " " :
				(_anchor & AnchorStyles.Bottom) != 0 ? "" : (h ? "S" : "N");
			//left col
			string hor =
				(_anchor & AnchorStyles.Left) != 0 ? "" :
				(_anchor & AnchorStyles.Right) != 0 ? " " : (w ? "W" : "E");
			ancNW.ImageKey += hor; ancW.ImageKey += hor; ancSW.ImageKey += hor;
			//middle col
			hor =
				(_anchor & AnchorStyles.Left) != 0 ? (w ? "E" : "W") :
				(_anchor & AnchorStyles.Right) != 0 ? (w ? "W" : "E") : "";
			ancN.ImageKey += hor; ancCT.ImageKey += hor; ancS.ImageKey += hor;
			//right col
			hor =
				(_anchor & AnchorStyles.Left) != 0 ? " " :
				(_anchor & AnchorStyles.Right) != 0 ? "" : (w ? "E" : "W");
			ancNE.ImageKey += hor; ancE.ImageKey += hor; ancSE.ImageKey += hor;
		}
		#endregion
		#region persistence
		//fetch application settings
		protected override void OnLoad(EventArgs e)
		{
			cmbWidthUnit.SelectedIndex = 0;
			cmbHeightUnit.SelectedIndex = 0;
			//
			DocumentAnchor = Settings.Default.DocumentResize_Anchor;
			cmbScale.SelectedIndex = Math.Min(cmbScale.Items.Count,
				Math.Max(0, Settings.Default.DocumentResize_Interpolation));
			cbScale.Checked = Settings.Default.DocumentResize_Scale;
			cbLock.Checked = Settings.Default.DocumentResize_Lock;
			DocumentSize = Settings.Default.DocumentResize_Size;
			//
			base.OnLoad(e);
		}
		//save application settings
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			Settings.Default.DocumentResize_Anchor = _anchor;
			Settings.Default.DocumentResize_Interpolation = cmbScale.SelectedIndex;
			Settings.Default.DocumentResize_Scale = cbScale.Checked;
			Settings.Default.DocumentResize_Lock = cbLock.Checked;
			Settings.Default.DocumentResize_Size = _newsize;
			//
			base.OnFormClosing(e);
		}
		#endregion
		#region properties
		public Size OldDocumentSize
		{
			get { return _size; }
			set
			{
				if (_size == value)
					return;
				_size = value;
				ApplyAnchorImages();
			}
		}
		/// <summary>
		/// gets or sets the current size
		/// </summary>
		public Size DocumentSize
		{
			get { return _newsize; }
			set
			{
				value = new Size(
					Math.Max(1, Math.Min(MAXWIDTH, value.Width)),
					Math.Max(1, Math.Min(MAXHEIGHT, value.Height)));
				if (value == _newsize)
					return;
				_newsize = value;
				ApplyDocumentSize();
				ApplyAnchorImages();
				Sync();
			}
		}
		/// <summary>
		/// gets or sets the current anchor
		/// </summary>
		public AnchorStyles DocumentAnchor
		{
			get { return _anchor; }
			set
			{
				//only border values permittet
				if ((value & AnchorStyles.Top) != 0 &&
					(value & AnchorStyles.Bottom) != 0)
					return;
				if ((value & AnchorStyles.Left) != 0 &&
					(value & AnchorStyles.Right) != 0)
					return;
				//apply
				if (value == _anchor)
					return;
				_anchor = value;
				ApplyAnchorChecks();
			}
		}
		/// <summary>
		/// gets the scaling flag
		/// </summary>
		public bool DocumentScale
		{
			get { return cbScale.Checked; }
		}
		/// <summary>
		/// gets the selected interpolation mode
		/// </summary>
		public InterpolationMode InterpolationMode
		{
			get
			{
				switch (cmbScale.SelectedIndex)
				{
					case 0: return InterpolationMode.HighQualityBicubic;
					case 1: return InterpolationMode.HighQualityBilinear;
					default: return InterpolationMode.NearestNeighbor;
				}
			}
		}
		#endregion
	}
}
