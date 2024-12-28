using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ControlsEx;
using VariIconsReload.Components;
using VariIconsReload.Properties;
using VariIconsReload.Tools;

namespace VariIconsReload.Forms
{
	public partial class PickContent : Form
	{
		#region types
		private class CropLayer : BoxLayer
		{
			protected override void DrawBox(PaintEventArgs e, Rectangle bounds, float w)
			{
				if (Owner.Data == null)
					return;
				using (GraphicsPath pth = new GraphicsPath())
				{
					pth.AddRectangle(new Rectangle(
						Point.Empty, Owner.Data.Size));
					pth.AddRectangle(bounds);
					using (SolidBrush brs = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
					{
						e.Graphics.FillPath(brs, pth);
					}
				}
				using (Pen pn = new Pen(Color.White, w))
					e.Graphics.DrawRectangle(pn, bounds);
			}
			protected override void OnBoxChanging()
			{
				if (Owner != null && Owner.Data != null || Box.Size.IsEmpty)
					Owner.Invalidate();
			}
		}
		#endregion
		#region variables
		private static readonly int[] CommonSizes = new int[]{
			16,32,48,128,256};
		//
		private Image _in = null;
		private CropLayer _crop;
		//
		private int maxh = 256, maxw = 256;
		private Size _size = new Size(16, 16);
		private Rectangle _source = new Rectangle(0, 0, 16, 16);
		private bool _editedwidth = false;
		#endregion
		#region ctor
		public PickContent()
		{
			InitializeComponent();
			//layerview config
			layerView.Layers.Add(new NavigatorLayer());
			_crop = new CropLayer();
			_crop.ConstrainBox += new EventHandler<RectangleEventArgs>(_crop_ConstrainBox);
			_crop.BoxChanged += new EventHandler(_crop_BoxChanged);
			_crop.EndConstruction += new EventHandler(_crop_EndConstruction);
			layerView.Layers.Add(_crop);
			//
			UpdateEnabled();
		}
		#endregion
		#region persistence
		//load settings
		protected override void OnLoad(EventArgs e)
		{
			cmbScale.SelectedIndex = Math.Min(cmbScale.Items.Count,
				Math.Max(0, Settings.Default.DocumentResize_Interpolation));
			cbScale.Checked = Settings.Default.DocumentResize_Scale;
			cbLock.Checked = Settings.Default.DocumentResize_Lock;
			_size = Settings.Default.DocumentResize_Size;
			//
			UpdateEnabled();
			UpdateParameters(null);
			//
			base.OnLoad(e);
		}
		//save settings
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			Settings.Default.DocumentResize_Interpolation = cmbScale.SelectedIndex;
			Settings.Default.DocumentResize_Scale = cbScale.Checked;
			Settings.Default.DocumentResize_Lock = cbLock.Checked;
			Settings.Default.DocumentResize_Size = _size;
			//
			base.OnFormClosing(e);
		}
		#endregion
		#region helper
		//parse integer
		private bool TryParse(string text, out int result)
		{
			double res = .0;
			bool b =
				double.TryParse(text, System.Globalization.NumberStyles.Integer,
				null, out res);
			result = (int)res;
			return b;
		}
		#endregion
		#region controller
		//flag for updating lock
		//needed because numericupdowns
		//fire valuechanged events if
		//it is set programmatically
		bool updating = false;
		private void UpdateParameters(object sender)
		{
			if (_in == null || updating)
				return;
			updating = true;
			#region new size
			//size lock
			if (cbLock.Checked)
			{
				if (_editedwidth) _size.Height = _size.Width;
				else _size.Width = _size.Height;
			}
			//check size bounds
			_size.Width = Math.Max(1, Math.Min(maxw, _size.Width));
			_size.Height = Math.Max(1, Math.Min(maxh, _size.Height));
			//push size to controls
			if (sender != newHeight)
				newHeight.Text = _size.Height.ToString();
			if (sender != newWidth)
				newWidth.Text = _size.Width.ToString();
			#endregion
			#region source rectangle
			if (cbLock.Checked)
				_source.Width = _source.Height =
					Math.Min(_source.Width, _source.Height);
			//check source rectangle bounds
			Rectangle outer, inner;
			GetSourceConstrains(sender == sourceWidth, sender == sourceHeight,
				out outer, out inner);
			_source = Rectangle.Intersect(outer, Rectangle.Union(inner, _source));
			//push source rectangle to controls
			try
			{
				if (sender != sourceX)
				{
					sourceX.Maximum = _in.Width - _source.Width;
					sourceX.Value = _source.X;
				}
				if (sender != sourceY)
				{
					sourceY.Maximum = _in.Height - _source.Height;
					sourceY.Value = _source.Y;
				}
				if (sender != sourceWidth)
				{
					sourceWidth.Minimum = _size.Width;
					sourceWidth.Maximum = _in.Width - _source.X;
					sourceWidth.Value = _source.Width;
				}
				if (sender != sourceHeight)
				{
					sourceHeight.Minimum = _size.Height;
					sourceHeight.Maximum = _in.Height - _source.Y;
					sourceHeight.Value = _source.Height;
				}
			}
			catch { }
			#endregion
			#region crop layer
			//push source rectangle to layerview
			if (sender != _crop)
			{
				_crop.Box = _source;
			}
			#endregion
			updating = false;
		}
		//update min and max of new size fields
		private void UpdateSizeConstrains()
		{
			if (_in == null)
				return;
			maxw = Math.Min(256, _in.Width);
			maxh = Math.Min(256, _in.Height);
			if (cbLock.Checked)
				maxh = maxw = Math.Min(maxw, maxh);
			//
			newWidth.Items.Clear();
			foreach (int sz in CommonSizes)
				if (sz < maxw) newWidth.Items.Add(sz);
			//
			newHeight.Items.Clear();
			foreach (int sz in CommonSizes)
				if (sz < maxh) newHeight.Items.Add(sz);
		}
		//get two rectangles, being the inner and outer borders
		//for source rectangle. can be applied by
		//_source = Rectangle.Intersect(outer, Rectangle.Union(inner, _source));
		private void GetSourceConstrains(bool w, bool h,
			out Rectangle outer, out Rectangle inner)
		{
			//w denotes wheter width of source is changing
			//h denotes wheter height of source is changing
			Size sz = _source.Size;
			if (!cbScale.Checked)
				sz = _size;
			//outer: x/y is min of source x/y, r/b is max of source r/b
			outer = Rectangle.FromLTRB(0, 0, _in.Width, _in.Height);
			//inner: x/y is max of source x/y, r/b is min of source r/b
			inner = Rectangle.FromLTRB(
				Math.Min(_in.Width - _size.Width,
					w ? _source.X + sz.Width - _size.Width : _in.Width - sz.Width),
				Math.Min(_in.Height - _source.Height,
					h ? _source.Y + sz.Height - _size.Height : _in.Height - sz.Height),
				Math.Max(_size.Width,
					w ? _source.X + _size.Width : sz.Width),
				Math.Max(_size.Height,
					h ? _source.Y + _size.Height : sz.Height));
		}
		//update the enabling of the contols
		private void UpdateEnabled()
		{
			Actions allowed = Actions.Move;
			if (cbScale.Checked) allowed |= Actions.Resize;
			if (!cbLock.Checked) allowed |= Actions.Skew;
			_crop.AllowedActions = allowed;
			//
			cmbScale.Enabled = sourceWidth.Enabled =
				sourceHeight.Enabled = cbScale.Checked;
		}
		#region new size
		//height
		private void newHeight_TextChanged(object sender, EventArgs e)
		{
			_editedwidth = false;
			int h;
			if (TryParse(newHeight.Text, out h))
				_size.Height = h;
			UpdateParameters(sender);
		}
		private void newHeight_Leave(object sender, EventArgs e)
		{
			UpdateParameters(null);
		}
		//width
		private void newWidth_TextChanged(object sender, EventArgs e)
		{
			_editedwidth = true;
			int w;
			if (TryParse(newWidth.Text, out w))
				_size.Width = w;
			UpdateParameters(sender);
		}

		private void newWidth_Leave(object sender, EventArgs e)
		{
			UpdateParameters(null);
		}
		#endregion
		#region source rectangle
		private void sourceX_ValueChanged(object sender, EventArgs e)
		{
			_source.X = (int)sourceX.Value;
			UpdateParameters(sender);
		}

		private void sourceY_ValueChanged(object sender, EventArgs e)
		{
			_source.Y = (int)sourceY.Value;
			UpdateParameters(sender);
		}

		private void sourceWidth_ValueChanged(object sender, EventArgs e)
		{
			_source.Width = (int)sourceWidth.Value;
			UpdateParameters(sender);
		}

		private void sourceHeight_ValueChanged(object sender, EventArgs e)
		{
			_source.Height = (int)sourceHeight.Value;
			UpdateParameters(sender);
		}
		#endregion
		#region crop control
		//copy box to other controls
		void _crop_EndConstruction(object sender, EventArgs e)
		{
			UpdateParameters(sender);
		}

		void _crop_BoxChanged(object sender, EventArgs e)
		{
			_source = _crop.Box;
		}
		//constrain user dragged box
		void _crop_ConstrainBox(object sender, RectangleEventArgs e)
		{
			if (_in == null)
				return;
			//aspect lock to square
			if (cbLock.Checked)
			{
				int s = Math.Min(e.Rectangle.Width, e.Rectangle.Height);
				if ((_crop.State & (BoxState.ResizeN | BoxState.ResizeW)) != 0)
				{
					e.Rectangle.X = e.Rectangle.Right - s;
					e.Rectangle.Y = e.Rectangle.Bottom - s;
				}
				e.Rectangle.Width = e.Rectangle.Height = s;
			}
			//respect bounds
			Rectangle outer, inner;
			GetSourceConstrains(
				(_crop.State & (BoxState.ResizeW | BoxState.ResizeE)) != 0,
				(_crop.State & (BoxState.ResizeN | BoxState.ResizeS)) != 0,
				out outer, out inner);
			e.Rectangle = Rectangle.Intersect(outer, Rectangle.Union(inner, e.Rectangle));
		}
		#endregion
		#region checks
		//lock aspect of source rectangle to aspect of new size
		private void cbLock_CheckedChanged(object sender, EventArgs e)
		{
			UpdateSizeConstrains();
			UpdateEnabled();
			UpdateParameters(sender);
		}
		//enable scaling up
		private void cbScale_CheckedChanged(object sender, EventArgs e)
		{
			UpdateEnabled();
			UpdateParameters(sender);
		}
		#endregion
		#endregion
		#region properties
		/// <summary>
		/// gets or sets the image to crop out
		/// </summary>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Image Image
		{
			get { return _in; }
			set
			{
				if (value == _in)
					return;
				_in = value;
				if (value != null)
				{
					//ideal zoom level
					layerView.Data = new ImageContent<Image>(value);
					layerView.Zoom = ScaleFactor.CommonZooms[
						Math.Min(ScaleFactor.GetNearestCommonZoom(
								new ScaleFactor(layerView.Width, _in.Width)),
							ScaleFactor.GetNearestCommonZoom(
								new ScaleFactor(layerView.Height, _in.Height)))];
				}
				else
				{
					layerView.Data = null;
					layerView.Zoom = ScaleFactor.Identity;
				}
				UpdateSizeConstrains();
				UpdateParameters(null);
			}
		}
		/// <summary>
		/// gets or sets the size of the new image
		/// </summary>
		public Size ImageSize
		{
			get { return _size; }
			set
			{
				if (value == _size)
					return;
				_size = value;
				UpdateParameters(null);
			}
		}
		/// <summary>
		/// gets or sets the selected area
		/// </summary>
		public Rectangle SourceRect
		{
			get { return _source; }
			set
			{
				if (value == _source)
					return;
				_source = value;
				UpdateParameters(null);
			}
		}
		/// <summary>
		/// returns the rectangle for drawing the in image
		/// </summary>
		public Rectangle Inverse
		{
			get
			{
				if (_in == null)
					return Rectangle.Empty;
				return new Rectangle(
					-(_size.Width * _source.X) / _source.Width,
					-(_size.Height * _source.Y) / _source.Height,
					(_size.Width * _in.Width) / _source.Width,
					(_size.Height * _in.Height) / _source.Height);
			}
		}
		/// <summary>
		/// gets the interpolation mode
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
