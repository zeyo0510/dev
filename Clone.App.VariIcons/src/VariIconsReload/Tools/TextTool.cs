using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using DockingFrames;
using VariIconsReload.BrushModel;
using VariIconsReload.Properties;
using VariIconsReload.Components;

namespace VariIconsReload.Tools
{
	/// <summary>
	/// stamp-and-print text tool with preview
	/// </summary>
	public class TextTool : Tool
	{
		#region types
		/// <summary>
		/// configoptions for this tool
		/// </summary>
		private class TextFormatOption : ConfigOption
		{
			#region variables
			private ToolBarAlignmentChooser _align;
			private ToolBarFontComboBox _fonts;
			private ToolBarComboBox _sizes;
			private ToolBarTextBox _text;
			//
			private Font _cache;
			private float _sz = 11f;
			#endregion
			public TextFormatOption()
			{
				ToolBarLabel lbl = new ToolBarLabel();
				lbl.Command.Text = Resources.TextFormatOption_Name;
				lbl.Appearance = DockingFrames.Appearance.Text;
				ConfigControls.Add(lbl);
				//
				_align = new ToolBarAlignmentChooser();
				_align.ValueChanged += new EventHandler(_align_ValueChanged);
				ConfigControls.Add(_align);
				//
				ToolBarSpace space = new ToolBarSpace();
				space.Length = 8;
				ConfigControls.Add(space);
				//
				_fonts = new ToolBarFontComboBox();
				_fonts.Size = new Size(100, 22);
				_fonts.SelectedIndexChanged += new EventHandler(_fonts_SelectedIndexChanged);
				ConfigControls.Add(_fonts);
				//
				space = new ToolBarSpace();
				space.Length = 8;
				ConfigControls.Add(space);
				//
				_sizes = new ToolBarComboBox();
				_sizes.Items.AddRange(new object[] { 8, 9, 11, 12, 13, 14 });
				_sizes.Size = new Size(50, 22);
				_sizes.SelectedIndexChanged += new EventHandler(_sizes_Changed);
				_sizes.KeyDown += new KeyEventHandler(_sizes_KeyDown);
				ConfigControls.Add(_sizes);
				//
				ConfigControls.Add(new ToolBarSeparator());
				//
				lbl = new ToolBarLabel();
				lbl.Command.Text = Resources.TextFormatOption_Text;
				lbl.Appearance = DockingFrames.Appearance.Text;
				ConfigControls.Add(lbl);
				//
				_text = new ToolBarTextBox();
				_text.Size = new Size(100, 22);
				_text.TextChanged += new EventHandler(_text_TextChanged);
				ConfigControls.Add(_text);
			}

			public override void Dispose()
			{
				ClearCache();
				base.Dispose();
			}
			#region controller
			void _align_ValueChanged(object sender, EventArgs e)
			{
				Settings.Default.TextFormatOption_Alignment =
					_align.Value;
			}
			#region font sizing
			void _sizes_Changed(object sender, EventArgs e)
			{
				ValidateSize();
			}
			void _sizes_KeyDown(object sender, KeyEventArgs e)
			{
				if (e.KeyCode == Keys.Return)
				{
					e.Handled = true;
					ValidateSize();
				}
			}
			//validates and applies size from text or selection
			void ValidateSize()
			{
				float w = _sz;
				if (_sizes.SelectedIndex != -1 &&
				   _sizes.Items[_sizes.SelectedIndex] is int)
					w = (int)_sizes.Items[_sizes.SelectedIndex];
				else if (float.TryParse(_sizes.Text, out w) && !float.IsNaN(w))
					w = Math.Max(0.5f, Math.Min(1000f, w));
				else
					w = 11f;
				//
				if (_sz == w)
					return;
				_sz = w;
				_sizes.Text = Settings.Default.TextFormatOptin_Size = _sz.ToString();
				ClearCache();
			}
			#endregion

			void _fonts_SelectedIndexChanged(object sender, EventArgs e)
			{
				Font fnt = _fonts.SelectedFont;
				if (fnt != null)
				{
					_fonts.PreferredFontName = fnt.Name;
					Settings.Default.TextFormatOption_PreferredFont =
						_fonts.PreferredFontName;
				}
				ClearCache();
			}
			void _text_TextChanged(object sender, EventArgs e)
			{
				Settings.Default.TextFormatOption_Text =
					_text.Text;
			}
			//load parameters
			public override void LoadConfig()
			{
				_align.Value = Settings.Default.TextFormatOption_Alignment;
				_fonts.PreferredFontName = Settings.Default.TextFormatOption_PreferredFont;
				_sizes.Text = Settings.Default.TextFormatOptin_Size;
				ValidateSize();
				_text.Text = Settings.Default.TextFormatOption_Text;
			}
			private void ClearCache()
			{
				if (_cache == null)
					return;
				_cache.Dispose();
				_cache = null;
			}
			#endregion
			public ContentAlignment Align
			{
				get { return _align.Value; }
			}
			public string Text
			{
				get { return _text.Text; }
			}
			/// <summary>
			/// creates the font in the selected size
			/// </summary>
			public Font Font
			{
				get
				{
					if (_cache == null)
					{
						Font fnt = _fonts.SelectedFont;
						if (fnt == null)
							return _cache = new Font("Arial", _sz);
						_cache = new Font(fnt.FontFamily, _sz);
					}
					return _cache;
				}
			}
		}
		/// <summary>
		/// config option for grid snapping
		/// </summary>
		private class SnapGridOption : ConfigOption
		{
			private DockingFrames.ToolBarButton _btnSnap;
			public SnapGridOption()
			{
				_btnSnap = new DockingFrames.ToolBarButton();
				_btnSnap.Command.Image = Resources.snap_grid;
				_btnSnap.Command.Click += new EventHandler(Command_Click);
				ConfigControls.Add(_btnSnap);
			}
			void Command_Click(object sender, EventArgs e)
			{
				_btnSnap.Command.Checked = !
					_btnSnap.Command.Checked;
				Settings.Default.SnapGridOption_Value =
					_btnSnap.Command.Checked;
				RaiseChanged();
			}
			public override void LoadConfig()
			{
				_btnSnap.Command.Checked = Settings.Default.SnapGridOption_Value;
			}
			public bool Value
			{
				get { return _btnSnap.Command.Checked; }
			}
		}
		/// <summary>
		/// construction and preview layer
		/// </summary>
		private class TextLayer : ToolLayer
		{
			#region variables
			private PointF _loc = PointF.Empty;
			private bool _inside = false;
			//
			private TextTool _tool;
			#endregion
			public TextLayer(TextTool tool)
			{
				if (tool == null)
					throw new ArgumentNullException("tool");
				_tool = tool;
				this.Order = 100;
			}
			#region controller
			//fewer updates for more performance
			private PointF Quantize(PointF loc)
			{
				if (_tool._snap.Value)
					return Floor(loc);
				return loc;
			}
			//incalidate construction lines
			private void InvalidateAnnotations()
			{
				if (Owner == null)
					return;
				Point pt = Point.Round(Owner.ContentToClient(_loc));
				Owner.Invalidate(new Rectangle(0, pt.Y - 3, Owner.ClientSize.Width, 6));
				Owner.Invalidate(new Rectangle(pt.X - 3, 0, 6, Owner.ClientSize.Height));
			}
			//draw construction lines
			protected internal override void OnPaint(PaintEventArgs e)
			{
				if (!_inside || Owner == null)
					return;
				//cross
				RectangleF area = Owner.ClientToContent(Owner.ClientRectangle);
				float w = Owner.RealZoom.Unscale(1f);
				using (Pen pn = new Pen(Color.FromArgb(200, 255, 255, 255), w * 3f))
				{
					e.Graphics.DrawLine(pn, area.X, _loc.Y, area.Right, _loc.Y);
					e.Graphics.DrawLine(pn, _loc.X, area.Y, _loc.X, area.Bottom);
					//
					pn.Color = Color.Black;
					pn.Width = w;
					e.Graphics.DrawLine(pn, area.X, _loc.Y, area.Right, _loc.Y);
					e.Graphics.DrawLine(pn, _loc.X, area.Y, _loc.X, area.Bottom);
				}
			}
			//print
			protected override void OnMouseDown(object sender, MouseEventArgs e)
			{
				_tool.Print(e.Button);
			}
			//update preview
			protected override void OnMouseMove(object sender, MouseEventArgs e)
			{
				PointF newloc = Quantize(Owner.ClientToContent(e.Location));
				if (newloc == _loc)
					return;
				InvalidateAnnotations();
				_loc = newloc;
				InvalidateAnnotations();
				_tool.Update(_loc);
				Owner.Update();
			}
			//hide cursor
			protected override void OnMouseLeave(object sender, EventArgs e)
			{
				_inside = false;
				InvalidateAnnotations();
				_tool.Reset();
				Owner.Update();
			}
			//prepare to show cursor
			protected override void OnMouseEnter(object sender, EventArgs e)
			{
				_inside = true;
				_loc = new Point(short.MaxValue, short.MaxValue);
			}
			//on tool change
			protected override void OnParentChange(LayerView value)
			{
				base.OnParentChange(value);
				//case cross invalidation
				_inside = value != null;
				_loc = new Point(short.MaxValue, short.MaxValue);
			}
			#endregion
		}
		/// <summary>
		/// alpha blended buffer layer
		/// </summary>
		private class TextPreviewLayer : BufferLayer
		{
			private ColorMatrix _cols;
			public TextPreviewLayer()
			{
				_cols = CreateMatrix(0.5f);
			}
			private ColorMatrix CreateMatrix(float alpha)
			{
				return new ColorMatrix(new float[][]{ 
					new float[] {1,  0,  0,  0, 0},
					new float[] {0,  1,  0,  0, 0},
					new float[] {0,  0,  1,  0, 0},
					new float[] {0,  0,  0,  alpha, 0},
					new float[] {0,  0,  0,  0, 1}});
			}
			protected internal override void OnPaint(PaintEventArgs e)
			{
				//draw buffer
				if (Image != null)
				{
					e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
					using (ImageAttributes att = new ImageAttributes())
					{
						att.SetColorMatrix(_cols);
						//
						e.Graphics.DrawImage(Image,
							new Rectangle(Point.Empty, Image.Size),
							0, 0, Image.Width, Image.Height,
							GraphicsUnit.Pixel, att);

					}
				}
			}
		}
		#endregion
		#region variables
		private TextLayer _control;
		private TextPreviewLayer _preview;
		//
		private TextFormatOption _format;
		private AntiAliasOption _antialias;
		private SnapGridOption _snap;
		private BrushManager _brushes;
		//caching
		private RectangleF _box;
		private string _text;
		private Font _fnt;
		#endregion
		#region ctor
		public TextTool()
		{
			this.ConfigOptions.Add(_antialias = new AntiAliasOption());
			this.ConfigOptions.Add(_snap = new SnapGridOption());
			this.ConfigOptions.Add(_format = new TextFormatOption());
			_brushes = BrushManager.RegisterInstance(this);
			//
			this.Layers.Add(_control = new TextLayer(this));
			this.Layers.Add(_preview = new TextPreviewLayer());
		}
		public override void Dispose()
		{
			BrushManager.UnregisterInstance(this);
			_brushes = null;
			base.Dispose();
		}
		#endregion
		#region controller
		//init buffer
		protected override void OnContentChange(DrawingEx.IconEncoder.IconImage value)
		{
			base.OnContentChange(value);
			_preview.Initialize(value);
		}
		//reset preview and status
		private void Reset()
		{
			if (_preview.Initialized)
			{
				_preview.Clear();
				_preview.InvalidateContent(_box);
			}
			RaiseToolStatusChanged();
		}
		//gets a location from aconstruction point and a size
		private PointF GetLocation(PointF loc, SizeF size, ContentAlignment align)
		{
			switch (align)
			{
				case ContentAlignment.TopLeft:
					return loc;
				case ContentAlignment.TopCenter:
					return PointF.Add(loc, new SizeF(-size.Width / 2f, 0));
				case ContentAlignment.TopRight:
					return PointF.Add(loc, new SizeF(-size.Width, 0));
				//
				case ContentAlignment.MiddleLeft:
					return PointF.Add(loc, new SizeF(0, -size.Height / 2f));
				case ContentAlignment.MiddleCenter:
					return PointF.Add(loc, new SizeF(-size.Width / 2f, -size.Height / 2f));
				case ContentAlignment.MiddleRight:
					return PointF.Add(loc, new SizeF(-size.Width, -size.Height / 2f));
				//
				case ContentAlignment.BottomLeft:
					return PointF.Add(loc, new SizeF(0, -size.Height));
				case ContentAlignment.BottomCenter:
					return PointF.Add(loc, new SizeF(-size.Width / 2f, -size.Height));
				default:
					return PointF.Add(loc, new SizeF(-size.Width, -size.Height));
			}
		}
		//hints for graphics
		private TextRenderingHint GetHint()
		{
			if (_antialias.Value)
			{
				if (_snap.Value)
					return TextRenderingHint.AntiAliasGridFit;
				else
					return TextRenderingHint.AntiAlias;
			}
			if (_snap.Value)
				return TextRenderingHint.SingleBitPerPixelGridFit;
			return TextRenderingHint.SingleBitPerPixel;
		}
		//update preview
		private void Update(PointF pt)
		{
			if (!_box.Size.IsEmpty)
				_preview.InvalidateContent(RectangleF.Inflate(_box, 1, 1));
			//measure string
			if ((_format.Text != _text || _format.Font != _fnt) &&
				_preview.Owner != null)
			{
				_text = _format.Text;
				_fnt = _format.Font;
				if (String.IsNullOrEmpty(_text))
					_box.Size = SizeF.Empty;
				else
				{
					using (Graphics gr = _preview.Owner.CreateGraphics())
						_box.Size = gr.MeasureString(_text, _format.Font);
				}
			}
			//draw string
			if (_preview.Initialized)
			{
				_box.Location = GetLocation(pt, _box.Size, _format.Align);
				_preview.Clear();
				if (!String.IsNullOrEmpty(_text))
				{
					using (PaintEventArgs e = _preview.CreateArgs())
					{
						e.Graphics.TextRenderingHint = GetHint();
						e.Graphics.DrawString(_text, _format.Font, Brushes.Black, _box.Location);
					}
				}
			}
			//
			if (!_box.Size.IsEmpty)
				_preview.InvalidateContent(RectangleF.Inflate(_box, 1, 1));
			RaiseToolStatusChanged(Rectangle.Round(_box));
		}
		//print to content and commit
		private void Print(MouseButtons btn)
		{
			if (!String.IsNullOrEmpty(_text) && Content != null)
			{
				Rectangle bounds = Rectangle.Inflate(Rectangle.Round(_box), 1, 1);
				//
				Brush brs;
				if (btn == MouseButtons.Left)
					brs = _brushes.BrushA.GetOutBrush(bounds);
				else brs = _brushes.BrushB.GetOutBrush(bounds);
				//
				using (Graphics gr = Graphics.FromImage(Content.Bitmap))
				{
					gr.TextRenderingHint = GetHint();
					gr.DrawString(_text, _format.Font, brs, _box.Location);
				}
				//
				if (_preview.Initialized)
					_preview.Clear();
				RaiseActionCommitted();
			}
		}
		#endregion
	}
}
