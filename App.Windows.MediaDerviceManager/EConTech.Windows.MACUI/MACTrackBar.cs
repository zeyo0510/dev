using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using AudioCore;

namespace EConTech.Windows.MACUI
{
	[Description("MACTrackBar represents an advanced track bar that is very better than the standard trackbar.")]
	[ToolboxBitmap(typeof(MACTrackBar), "Editors.MACTrackBar.MACTrackBar.bmp")]
	[Designer(typeof(MACTrackBarDesigner))]
	[DefaultProperty("Maximum")]
	[DefaultEvent("ValueChanged")]
	public class MACTrackBar : Control
	{
		private int _value;

		private int _minimum;

		private int _maximum = 10;

		private int _largeChange = 2;

		private int _smallChange = 1;

		private Orientation _orientation;

		private MACBorderStyle _borderStyle;

		private Color _borderColor = SystemColors.ActiveBorder;

		private Size _trackerSize = new Size(10, 20);

		private int _indentWidth = 6;

		private int _indentHeight = 6;

		private int _tickHeight = 2;

		private int _tickFrequency = 1;

		private Color _tickColor = Color.Black;

		private TickStyle _tickStyle = TickStyle.BottomRight;

		private TickStyle _textTickStyle = TickStyle.BottomRight;

		private int _trackLineHeight = 3;

		private Color _trackLineColor = SystemColors.Control;

		private Color Color1 = SystemColors.Control;

		private Color _trackerColor = SystemColors.Control;

		public RectangleF _trackerRect = RectangleF.Empty;

		private bool _autoSize = true;

		private bool leftButtonDown;

		private float mouseStartPos = -1f;

		private float _actualVolume;

		public event ValueChangedHandler ValueChanged;

		[CompilerGenerated]
		private EventHandler Scroll;

		public bool bool1;

		public AudioEndpointVolume audioEndpointVolume1;

		public float ActualVolume
		{
			get
			{
				return _actualVolume;
			}
			private set
			{
				_actualVolume = value;
			}
		}

		[Description("Gets or sets the height and width of the control.")]
		[Category("Layout")]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				if (_orientation == Orientation.Horizontal)
				{
					base.Width = value.Width;
					base.Height = FitSize.Height;
				}
				else
				{
					base.Width = FitSize.Width;
					base.Height = value.Height;
				}
			}
		}

		[Category("Behavior")]
		[Description("Gets or sets the height of track line.")]
		[DefaultValue(4)]
		public new bool AutoSize
		{
			get
			{
				return _autoSize;
			}
			set
			{
				if (_autoSize != value)
				{
					_autoSize = value;
					if (_autoSize)
					{
						Size = FitSize;
					}
				}
			}
		}

		[Category("Behavior")]
		[Description("Gets or sets a value to be added to or subtracted from the Value property when the slider is moved a large distance.")]
		[DefaultValue(2)]
		public int LargeChange
		{
			get
			{
				return _largeChange;
			}
			set
			{
				_largeChange = value;
				if (_largeChange < 1)
				{
					_largeChange = 1;
				}
			}
		}

		[Category("Behavior")]
		[Description("Gets or sets a value to be added to or subtracted from the Value property when the slider is moved a small distance.")]
		[DefaultValue(1)]
		public int SmallChange
		{
			get
			{
				return _smallChange;
			}
			set
			{
				_smallChange = value;
				if (_smallChange < 1)
				{
					_smallChange = 1;
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the height of track line.")]
		[DefaultValue(4)]
		public int TrackLineHeight
		{
			get
			{
				return _trackLineHeight;
			}
			set
			{
				if (_trackLineHeight != value)
				{
					_trackLineHeight = value;
					if (_trackLineHeight < 1)
					{
						_trackLineHeight = 1;
					}
					if (_trackLineHeight > _trackerSize.Height)
					{
						_trackLineHeight = _trackerSize.Height;
					}
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the tick's color of the control.")]
		public Color TickColor
		{
			get
			{
				return _tickColor;
			}
			set
			{
				if (_tickColor != value)
				{
					_tickColor = value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets a value that specifies the delta between ticks drawn on the control.")]
		[DefaultValue(1)]
		public int TickFrequency
		{
			get
			{
				return _tickFrequency;
			}
			set
			{
				if (_tickFrequency != value)
				{
					_tickFrequency = value;
					if (_tickFrequency < 1)
					{
						_tickFrequency = 1;
					}
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the height of tick.")]
		[DefaultValue(6)]
		public int TickHeight
		{
			get
			{
				return _tickHeight;
			}
			set
			{
				if (_tickHeight != value)
				{
					_tickHeight = value;
					if (_tickHeight < 1)
					{
						_tickHeight = 1;
					}
					if (_autoSize)
					{
						Size = FitSize;
					}
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the height of indent.")]
		[DefaultValue(2)]
		public int IndentHeight
		{
			get
			{
				return _indentHeight;
			}
			set
			{
				if (_indentHeight != value)
				{
					_indentHeight = value;
					if (_indentHeight < 0)
					{
						_indentHeight = 0;
					}
					if (_autoSize)
					{
						Size = FitSize;
					}
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the width of indent.")]
		[DefaultValue(6)]
		public int IndentWidth
		{
			get
			{
				return _indentWidth;
			}
			set
			{
				if (_indentWidth != value)
				{
					_indentWidth = value;
					if (_indentWidth < 0)
					{
						_indentWidth = 0;
					}
					if (_autoSize)
					{
						Size = FitSize;
					}
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the tracker's size.")]
		public Size TrackerSize
		{
			get
			{
				return _trackerSize;
			}
			set
			{
				if (_trackerSize != value)
				{
					_trackerSize = value;
					if (_autoSize)
					{
						Size = FitSize;
					}
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the text tick style.")]
		[DefaultValue(TickStyle.BottomRight)]
		public TickStyle TextTickStyle
		{
			get
			{
				return _textTickStyle;
			}
			set
			{
				if (_textTickStyle != value)
				{
					_textTickStyle = value;
					if (_autoSize)
					{
						Size = FitSize;
					}
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the tick style.")]
		[DefaultValue(TickStyle.BottomRight)]
		public TickStyle TickStyle
		{
			get
			{
				return _tickStyle;
			}
			set
			{
				if (_tickStyle != value)
				{
					_tickStyle = value;
					if (_autoSize)
					{
						Size = FitSize;
					}
					Invalidate();
				}
			}
		}

		[Description("Gets or set tracker's color.")]
		[Category("Appearance")]
		public Color TrackerColor
		{
			get
			{
				return _trackerColor;
			}
			set
			{
				if (_trackerColor != value)
				{
					_trackerColor = value;
					Invalidate();
				}
			}
		}

		[Description("The current value for the MACTrackBar, in the range specified by the Minimum and Maximum properties.")]
		[Category("Behavior")]
		public int Value
		{
			get
			{
				return _value;
			}
			set
			{
				if (_value != value)
				{
					if (value < _minimum)
					{
						_value = _minimum;
					}
					else if (value > _maximum)
					{
						_value = _maximum;
					}
					else
					{
						_value = value;
					}
					OnValueChanged(_value);
					Invalidate();
				}
			}
		}

		[Description("The lower bound of the range this MACTrackBar is working with.")]
		[Category("Behavior")]
		public int Minimum
		{
			get
			{
				return _minimum;
			}
			set
			{
				_minimum = value;
				if (_minimum > _maximum)
				{
					_maximum = _minimum;
				}
				if (_minimum > _value)
				{
					_value = _minimum;
				}
				Invalidate();
			}
		}

		[Description("The uppper bound of the range this MACTrackBar is working with.")]
		[Category("Behavior")]
		public int Maximum
		{
			get
			{
				return _maximum;
			}
			set
			{
				_maximum = value;
				if (_maximum < _value)
				{
					_value = _maximum;
				}
				if (_maximum < _minimum)
				{
					_minimum = _maximum;
				}
				Invalidate();
			}
		}

		[Description("Gets or sets a value indicating the horizontal or vertical orientation of the track bar.")]
		[Category("Behavior")]
		[DefaultValue(Orientation.Horizontal)]
		public Orientation Orientation
		{
			get
			{
				return _orientation;
			}
			set
			{
				if (value == _orientation)
				{
					return;
				}
				_orientation = value;
				if (_orientation == Orientation.Horizontal)
				{
					if (base.Width < base.Height)
					{
						int num = base.Width;
						base.Width = base.Height;
						base.Height = num;
					}
				}
				else if (base.Width > base.Height)
				{
					int num2 = base.Width;
					base.Width = base.Height;
					base.Height = num2;
				}
				Invalidate();
			}
		}

		[Description("Gets or sets the border type of the trackbar control.")]
		[Category("Appearance")]
		[DefaultValue(typeof(MACBorderStyle), "None")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public MACBorderStyle BorderStyle
		{
			get
			{
				return _borderStyle;
			}
			set
			{
				if (_borderStyle != value)
				{
					_borderStyle = value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the border color of the control.")]
		public Color BorderColor
		{
			get
			{
				return _borderColor;
			}
			set
			{
				if (value != _borderColor)
				{
					_borderColor = value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the color of the track line.")]
		public Color TrackLineColor
		{
			get
			{
				return _trackLineColor;
			}
			set
			{
				if (value != _trackLineColor)
				{
					_trackLineColor = value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets the color of the Volume line.")]
		public Color VolumeLineColor
		{
			get
			{
				return Color1;
			}
			set
			{
				if (value != Color1)
				{
					Color1 = value;
					Invalidate();
				}
			}
		}

		[Description("Gets the Size of area need for drawing.")]
		[Browsable(false)]
		private Size FitSize
		{
			get
			{
				Graphics graphics = CreateGraphics();
				Rectangle.Inflate(base.ClientRectangle, -_indentWidth, -_indentHeight);
				float num = 0f;
				Size result;
				if (_orientation == Orientation.Horizontal)
				{
					num = _indentHeight;
					float num2 = graphics.MeasureString(_maximum.ToString(), Font).Height;
					if (_textTickStyle == TickStyle.TopLeft || _textTickStyle == TickStyle.Both)
					{
						num += num2;
					}
					if (_tickStyle == TickStyle.TopLeft || _tickStyle == TickStyle.Both)
					{
						num += (float)(_tickHeight + 1);
					}
					num += (float)_trackerSize.Height;
					if (_tickStyle == TickStyle.BottomRight || _tickStyle == TickStyle.Both)
					{
						num += 1f;
						num += (float)_tickHeight;
					}
					if (_textTickStyle == TickStyle.BottomRight || _textTickStyle == TickStyle.Both)
					{
						num += num2;
					}
					num += (float)_indentHeight;
					result = new Size(base.ClientRectangle.Width, (int)num);
				}
				else
				{
					num = _indentWidth;
					float num2 = graphics.MeasureString(_maximum.ToString(), Font).Width;
					if (_textTickStyle == TickStyle.TopLeft || _textTickStyle == TickStyle.Both)
					{
						num += num2;
					}
					if (_tickStyle == TickStyle.TopLeft || _tickStyle == TickStyle.Both)
					{
						num += (float)(_tickHeight + 1);
					}
					num += (float)_trackerSize.Height;
					if (_tickStyle == TickStyle.BottomRight || _tickStyle == TickStyle.Both)
					{
						num += 1f;
						num += (float)_tickHeight;
					}
					if (_textTickStyle == TickStyle.BottomRight || _textTickStyle == TickStyle.Both)
					{
						num += num2;
					}
					num += (float)_indentWidth;
					result = new Size((int)num, base.ClientRectangle.Height);
				}
				graphics.Dispose();
				return result;
			}
		}

		[Description("Gets the rectangle containing the tracker.")]
		private RectangleF TrackerRect
		{
			get
			{
				Graphics graphics = CreateGraphics();
				Rectangle rectangle = Rectangle.Inflate(base.ClientRectangle, -_indentWidth, -_indentHeight);
				float num = 0f;
				RectangleF result;
				if (_orientation == Orientation.Horizontal)
				{
					num = _indentHeight;
					float num2 = graphics.MeasureString(_maximum.ToString(), Font).Height;
					if (_textTickStyle == TickStyle.TopLeft || _textTickStyle == TickStyle.Both)
					{
						num += num2;
					}
					if (_tickStyle == TickStyle.TopLeft || _tickStyle == TickStyle.Both)
					{
						num += (float)(_tickHeight + 1);
					}
					float num3 = ((_maximum != _minimum) ? ((float)((rectangle.Width - _trackerSize.Width) * (_value - _minimum) / (_maximum - _minimum) + rectangle.Left)) : ((float)rectangle.Left));
					result = new RectangleF(num3, num, _trackerSize.Width, _trackerSize.Height);
					result.Inflate(0f, -1f);
				}
				else
				{
					num = _indentWidth;
					float num2 = graphics.MeasureString(_maximum.ToString(), Font).Width;
					if (_textTickStyle == TickStyle.TopLeft || _textTickStyle == TickStyle.Both)
					{
						num += num2;
					}
					if (_tickStyle == TickStyle.TopLeft || _tickStyle == TickStyle.Both)
					{
						num += (float)(_tickHeight + 1);
					}
					float num4 = ((_maximum != _minimum) ? ((float)((rectangle.Height - _trackerSize.Width) * (_value - _minimum) / (_maximum - _minimum))) : ((float)rectangle.Top));
					result = new RectangleF(num, (float)rectangle.Bottom - num4 - (float)_trackerSize.Width, _trackerSize.Height, _trackerSize.Width);
					result.Inflate(-1f, 0f);
				}
				graphics.Dispose();
				return result;
			}
		}

		public MACTrackBar()
		{
			base.MouseDown += _OnMouseDown;
			base.MouseUp += _OnMouseUp;
			base.MouseLeave += _OnMouseLeave;
			base.MouseMove += _OnMouseMove;
			SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			ForeColor = Color.FromArgb(123, 125, 123);
			BackColor = Color.Transparent;
			_tickColor = Color.FromArgb(148, 146, 148);
			_tickHeight = 4;
			_trackerColor = Color.FromArgb(24, 130, 198);
			_trackerSize = new Size(46, 20);
			_indentWidth = 6;
			_indentHeight = 6;
			_trackLineColor = Color.FromArgb(90, 93, 90);
			_trackLineHeight = 3;
			_borderStyle = MACBorderStyle.None;
			_borderColor = SystemColors.Window;
			_autoSize = true;
			base.Height = FitSize.Height;
		}

		public virtual void OnValueChanged(int P_0)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, P_0);
			}
		}

		public virtual void OnScroll()
		{
			try
			{
				if (Scroll != null)
				{
					Scroll(this, new EventArgs());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("OnScroll Exception: " + ex.Message);
			}
		}

		public void Increment(int P_0)
		{
			if (_value < _maximum)
			{
				_value += P_0;
				if (_value > _maximum)
				{
					_value = _maximum;
				}
			}
			else
			{
				_value = _maximum;
			}
			OnValueChanged(_value);
			Invalidate();
		}

		public void Decrement(int P_0)
		{
			if (_value > _minimum)
			{
				_value -= P_0;
				if (_value < _minimum)
				{
					_value = _minimum;
				}
			}
			else
			{
				_value = _minimum;
			}
			OnValueChanged(_value);
			Invalidate();
		}

		public void SetRange(int P_0, int P_1)
		{
			_minimum = P_0;
			if (_minimum > _value)
			{
				_value = _minimum;
			}
			_maximum = P_1;
			if (_maximum < _value)
			{
				_value = _maximum;
			}
			if (_maximum < _minimum)
			{
				_minimum = _maximum;
			}
			Invalidate();
		}

		public void ResetAppearance()
		{
			Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			ForeColor = Color.FromArgb(123, 125, 123);
			BackColor = Color.Transparent;
			_tickColor = Color.FromArgb(148, 146, 148);
			_tickHeight = 4;
			_trackerColor = Color.FromArgb(24, 130, 198);
			_trackerSize = new Size(16, 16);
			_indentWidth = 6;
			_indentHeight = 6;
			_trackLineColor = Color.FromArgb(90, 93, 90);
			_trackLineHeight = 3;
			_borderStyle = MACBorderStyle.None;
			_borderColor = SystemColors.ActiveBorder;
			if (_autoSize)
			{
				Size = FitSize;
			}
			Invalidate();
		}

		public void SetActualVolume(float P_0)
		{
			ActualVolume = P_0;
		}

		protected override void OnCreateControl()
		{
		}

		protected override void OnLostFocus(EventArgs P_0)
		{
			Invalidate();
			base.OnLostFocus(P_0);
		}

		protected override void OnGotFocus(EventArgs P_0)
		{
			Invalidate();
			base.OnGotFocus(P_0);
		}

		protected override void OnClick(EventArgs P_0)
		{
			Focus();
			Invalidate();
			base.OnClick(P_0);
		}

		protected override bool ProcessCmdKey(ref Message P_0, Keys P_1)
		{
			bool result = true;
			if (P_0.Msg == 256 || P_0.Msg == 260)
			{
				switch (P_1)
				{
				case Keys.Left:
				case Keys.Down:
					Decrement(_smallChange);
					break;
				case Keys.Up:
				case Keys.Right:
					Increment(_smallChange);
					break;
				case Keys.Prior:
					Increment(_largeChange);
					break;
				case Keys.Next:
					Decrement(_largeChange);
					break;
				case Keys.Home:
					Value = _maximum;
					break;
				case Keys.End:
					Value = _minimum;
					break;
				default:
					result = base.ProcessCmdKey(ref P_0, P_1);
					break;
				}
			}
			return result;
		}

		protected override void Dispose(bool P_0)
		{
			base.Dispose(P_0);
		}

		protected override void OnPaint(PaintEventArgs P_0)
		{
			Rectangle rectangle = Rectangle.Inflate(base.ClientRectangle, -_indentWidth, -_indentHeight);
			float num = 0f;
			Brush brush = new SolidBrush(BackColor);
			RectangleF rect = base.ClientRectangle;
			P_0.Graphics.FillRectangle(brush, rect);
			brush.Dispose();
			if (_orientation == Orientation.Horizontal)
			{
				num = _indentHeight;
				float num2 = P_0.Graphics.MeasureString(_maximum.ToString(), Font).Height;
				RectangleF rectangleF;
				if (_textTickStyle == TickStyle.TopLeft || _textTickStyle == TickStyle.Both)
				{
					rectangleF = new RectangleF(rectangle.Left, num, rectangle.Width, num2);
					rectangleF.Inflate(-_trackerSize.Width / 2, 0f);
					num += num2;
					DrawTickTextLine(P_0.Graphics, rectangleF, _tickFrequency, _minimum, _maximum, ForeColor, Font, _orientation);
				}
				if (_tickStyle == TickStyle.TopLeft || _tickStyle == TickStyle.Both)
				{
					rectangleF = new RectangleF(rectangle.Left, num, rectangle.Width, _tickHeight);
					rectangleF.Inflate(-_trackerSize.Width / 2, 0f);
					num += (float)(_tickHeight + 1);
					DrawTickLine(P_0.Graphics, rectangleF, _tickFrequency, _minimum, _maximum, _tickColor, _orientation);
				}
				float num3 = ((_maximum != _minimum) ? ((float)((rectangle.Width - _trackerSize.Width) * (_value - _minimum) / (_maximum - _minimum) + rectangle.Left)) : ((float)rectangle.Left));
				_trackerRect = new RectangleF(num3, num, _trackerSize.Width, _trackerSize.Height);
				rectangleF = new RectangleF(rectangle.Left, num + (float)(_trackerSize.Height / 2) - (float)(_trackLineHeight / 2), rectangle.Width, _trackLineHeight);
				DrawTrackLine(P_0.Graphics, rectangleF);
				RectangleF rectangleF2 = new RectangleF(rectangle.Left, num + (float)(_trackerSize.Height / 2) - (float)(_trackLineHeight / 2), (float)rectangle.Width * ActualVolume, _trackLineHeight);
				DrawTrackLine_2(P_0.Graphics, rectangleF2);
				num += (float)_trackerSize.Height;
				if (_tickStyle == TickStyle.BottomRight || _tickStyle == TickStyle.Both)
				{
					num += 1f;
					rectangleF = new RectangleF(rectangle.Left, num, rectangle.Width, _tickHeight);
					rectangleF.Inflate(-_trackerSize.Width / 2, 0f);
					num += (float)_tickHeight;
					DrawTickLine(P_0.Graphics, rectangleF, _tickFrequency, _minimum, _maximum, _tickColor, _orientation);
				}
				if (_textTickStyle == TickStyle.BottomRight || _textTickStyle == TickStyle.Both)
				{
					rectangleF = new RectangleF(rectangle.Left, num, rectangle.Width, num2);
					rectangleF.Inflate(-_trackerSize.Width / 2, 0f);
					num += num2;
					DrawTickTextLine(P_0.Graphics, rectangleF, _tickFrequency, _minimum, _maximum, ForeColor, Font, _orientation);
				}
			}
			else
			{
				num = _indentWidth;
				float num2 = P_0.Graphics.MeasureString(_maximum.ToString(), Font).Width;
				RectangleF rectangleF;
				if (_textTickStyle == TickStyle.TopLeft || _textTickStyle == TickStyle.Both)
				{
					rectangleF = new RectangleF(num, rectangle.Top, num2, rectangle.Height);
					rectangleF.Inflate(0f, -_trackerSize.Width / 2);
					num += num2;
					DrawTickTextLine(P_0.Graphics, rectangleF, _tickFrequency, _minimum, _maximum, ForeColor, Font, _orientation);
				}
				if (_tickStyle == TickStyle.TopLeft || _tickStyle == TickStyle.Both)
				{
					rectangleF = new RectangleF(num, rectangle.Top, _tickHeight, rectangle.Height);
					rectangleF.Inflate(0f, -_trackerSize.Width / 2);
					num += (float)(_tickHeight + 1);
					DrawTickLine(P_0.Graphics, rectangleF, _tickFrequency, _minimum, _maximum, _tickColor, _orientation);
				}
				float num4 = ((_maximum != _minimum) ? ((float)((rectangle.Height - _trackerSize.Width) * (_value - _minimum) / (_maximum - _minimum))) : ((float)rectangle.Top));
				_trackerRect = new RectangleF(num, (float)rectangle.Bottom - num4 - (float)_trackerSize.Width, _trackerSize.Height, _trackerSize.Width);
				rect = _trackerRect;
				rectangleF = new RectangleF(num + (float)(_trackerSize.Height / 2) - (float)(_trackLineHeight / 2), rectangle.Top, _trackLineHeight, rectangle.Height);
				DrawTrackLine(P_0.Graphics, rectangleF);
				num += (float)_trackerSize.Height;
				if (_tickStyle == TickStyle.BottomRight || _tickStyle == TickStyle.Both)
				{
					num += 1f;
					rectangleF = new RectangleF(num, rectangle.Top, _tickHeight, rectangle.Height);
					rectangleF.Inflate(0f, -_trackerSize.Width / 2);
					num += (float)_tickHeight;
					DrawTickLine(P_0.Graphics, rectangleF, _tickFrequency, _minimum, _maximum, _tickColor, _orientation);
				}
				if (_textTickStyle == TickStyle.BottomRight || _textTickStyle == TickStyle.Both)
				{
					rectangleF = new RectangleF(num, rectangle.Top, num2, rectangle.Height);
					rectangleF.Inflate(0f, -_trackerSize.Width / 2);
					num += num2;
					DrawTickTextLine(P_0.Graphics, rectangleF, _tickFrequency, _minimum, _maximum, ForeColor, Font, _orientation);
				}
			}
			if (_maximum == _minimum)
			{
				DrawBorder(P_0.Graphics);
				return;
			}
			DrawTracker(P_0.Graphics, _trackerRect);
			DrawBorder(P_0.Graphics);
		}

		private void DrawTrackLine(Graphics P_0, RectangleF P_1)
		{
			DrawMACStyleHelper.DrawAquaPillSingleLayer(P_0, P_1, _trackLineColor, _orientation);
		}

		private void DrawTrackLine_2(Graphics P_0, RectangleF P_1)
		{
			DrawMACStyleHelper.DrawAquaPillSingleLayer(P_0, P_1, Color1, _orientation);
		}

		private void DrawTracker(Graphics P_0, RectangleF P_1)
		{
			DrawMACStyleHelper.DrawAquaPill(P_0, P_1, _trackerColor, _orientation);
		}

		private void DrawTickTextLine(Graphics P_0, RectangleF P_1, int P_2, int P_3, int P_4, Color P_5, Font P_6, Orientation P_7)
		{
			if (P_4 == P_3)
			{
				return;
			}
			int num = (P_4 - P_3) / P_2;
			if ((P_4 - P_3) % P_2 == 0)
			{
				num--;
			}
			StringFormat stringFormat = new StringFormat();
			stringFormat.FormatFlags = StringFormatFlags.NoWrap;
			stringFormat.LineAlignment = StringAlignment.Center;
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.Trimming = StringTrimming.EllipsisCharacter;
			stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
			Brush brush = new SolidBrush(P_5);
			if (_orientation == Orientation.Horizontal)
			{
				float num2 = P_1.Width * (float)P_2 / (float)(P_4 - P_3);
				string s;
				for (int i = 0; i <= num; i++)
				{
					s = Convert.ToString(_minimum + P_2 * i, 10);
					P_0.DrawString(s, P_6, brush, P_1.Left + num2 * (float)i, P_1.Top + P_1.Height / 2f, stringFormat);
				}
				s = Convert.ToString(_maximum, 10);
				P_0.DrawString(s, P_6, brush, P_1.Right, P_1.Top + P_1.Height / 2f, stringFormat);
			}
			else
			{
				float num2 = P_1.Height * (float)P_2 / (float)(P_4 - P_3);
				string s;
				for (int j = 0; j <= num; j++)
				{
					s = Convert.ToString(_minimum + P_2 * j, 10);
					P_0.DrawString(s, P_6, brush, P_1.Left + P_1.Width / 2f, P_1.Bottom - num2 * (float)j, stringFormat);
				}
				s = Convert.ToString(_maximum, 10);
				P_0.DrawString(s, P_6, brush, P_1.Left + P_1.Width / 2f, P_1.Top, stringFormat);
			}
		}

		private void DrawTickLine(Graphics P_0, RectangleF P_1, int P_2, int P_3, int P_4, Color P_5, Orientation P_6)
		{
			if (P_4 == P_3)
			{
				return;
			}
			Pen pen = new Pen(P_5, 1f);
			int num = (P_4 - P_3) / P_2;
			if ((P_4 - P_3) % P_2 == 0)
			{
				num--;
			}
			if (_orientation == Orientation.Horizontal)
			{
				float num2 = P_1.Width * (float)P_2 / (float)(P_4 - P_3);
				for (int i = 0; i <= num; i++)
				{
					P_0.DrawLine(pen, P_1.Left + num2 * (float)i, P_1.Top, P_1.Left + num2 * (float)i, P_1.Bottom);
				}
				P_0.DrawLine(pen, P_1.Right, P_1.Top, P_1.Right, P_1.Bottom);
			}
			else
			{
				float num2 = P_1.Height * (float)P_2 / (float)(P_4 - P_3);
				for (int j = 0; j <= num; j++)
				{
					P_0.DrawLine(pen, P_1.Left, P_1.Bottom - num2 * (float)j, P_1.Right, P_1.Bottom - num2 * (float)j);
				}
				P_0.DrawLine(pen, P_1.Left, P_1.Top, P_1.Right, P_1.Top);
			}
		}

		private void DrawBorder(Graphics P_0)
		{
			switch (_borderStyle)
			{
			case MACBorderStyle.Dashed:
				ControlPaint.DrawBorder(P_0, base.ClientRectangle, _borderColor, ButtonBorderStyle.Dashed);
				break;
			case MACBorderStyle.Dotted:
				ControlPaint.DrawBorder(P_0, base.ClientRectangle, _borderColor, ButtonBorderStyle.Dotted);
				break;
			case MACBorderStyle.Inset:
				ControlPaint.DrawBorder(P_0, base.ClientRectangle, _borderColor, ButtonBorderStyle.Inset);
				break;
			case MACBorderStyle.Outset:
				ControlPaint.DrawBorder(P_0, base.ClientRectangle, _borderColor, ButtonBorderStyle.Outset);
				break;
			case MACBorderStyle.Solid:
				ControlPaint.DrawBorder(P_0, base.ClientRectangle, _borderColor, ButtonBorderStyle.Solid);
				break;
			case MACBorderStyle.Adjust:
				ControlPaint.DrawBorder3D(P_0, base.ClientRectangle, Border3DStyle.Adjust);
				break;
			case MACBorderStyle.Bump:
				ControlPaint.DrawBorder3D(P_0, base.ClientRectangle, Border3DStyle.Bump);
				break;
			case MACBorderStyle.Etched:
				ControlPaint.DrawBorder3D(P_0, base.ClientRectangle, Border3DStyle.Etched);
				break;
			case MACBorderStyle.Flat:
				ControlPaint.DrawBorder3D(P_0, base.ClientRectangle, Border3DStyle.Flat);
				break;
			case MACBorderStyle.Raised:
				ControlPaint.DrawBorder3D(P_0, base.ClientRectangle, Border3DStyle.Raised);
				break;
			case MACBorderStyle.RaisedInner:
				ControlPaint.DrawBorder3D(P_0, base.ClientRectangle, Border3DStyle.RaisedInner);
				break;
			case MACBorderStyle.RaisedOuter:
				ControlPaint.DrawBorder3D(P_0, base.ClientRectangle, Border3DStyle.RaisedOuter);
				break;
			case MACBorderStyle.Sunken:
				ControlPaint.DrawBorder3D(P_0, base.ClientRectangle, Border3DStyle.Sunken);
				break;
			case MACBorderStyle.SunkenInner:
				ControlPaint.DrawBorder3D(P_0, base.ClientRectangle, Border3DStyle.SunkenInner);
				break;
			case MACBorderStyle.SunkenOuter:
				ControlPaint.DrawBorder3D(P_0, base.ClientRectangle, Border3DStyle.SunkenOuter);
				break;
			case MACBorderStyle.None:
				break;
			}
		}

		private void _OnMouseDown(object P_0, MouseEventArgs P_1)
		{
			bool1 = true;
			int num = 50;
			PointF pt = new PointF(P_1.X, P_1.Y);
			if (_trackerRect.Contains(pt))
			{
				if (!leftButtonDown)
				{
					leftButtonDown = true;
					base.Capture = true;
					switch (_orientation)
					{
					case Orientation.Horizontal:
						mouseStartPos = pt.X - _trackerRect.X;
						break;
					case Orientation.Vertical:
						mouseStartPos = pt.Y - _trackerRect.Y;
						break;
					}
				}
				return;
			}
			switch (_orientation)
			{
			case Orientation.Horizontal:
				num = ((!(pt.X + (float)(_trackerSize.Width / 2) >= (float)(base.Width - _indentWidth))) ? ((!(pt.X - (float)(_trackerSize.Width / 2) <= (float)_indentWidth)) ? ((int)((double)((pt.X - (float)_indentWidth - (float)(_trackerSize.Width / 2)) * (float)(_maximum - _minimum) / (float)(base.Width - 2 * _indentWidth - _trackerSize.Width)) + 0.5)) : 0) : (_maximum - _minimum));
				break;
			case Orientation.Vertical:
				num = ((!(pt.Y + (float)(_trackerSize.Width / 2) >= (float)(base.Height - _indentHeight))) ? ((!(pt.Y - (float)(_trackerSize.Width / 2) <= (float)_indentHeight)) ? ((int)((double)(((float)base.Height - pt.Y - (float)_indentHeight - (float)(_trackerSize.Width / 2)) * (float)(_maximum - _minimum) / (float)(base.Height - 2 * _indentHeight - _trackerSize.Width)) + 0.5)) : (_maximum - _minimum)) : 0);
				break;
			}
			int value = _value;
			_value = _minimum + num;
			Invalidate();
			if (value != _value)
			{
				OnScroll();
				OnValueChanged(_value);
			}
		}

		private void Method1(object P_0, MouseEventArgs P_1)
		{
			bool1 = false;
		}

		private void _OnMouseUp(object P_0, MouseEventArgs P_1)
		{
			bool1 = false;
			leftButtonDown = false;
			base.Capture = false;
			if (Value > 100)
			{
				Value = 100;
			}
			else if (Value < 0)
			{
				Value = 0;
			}
			if (audioEndpointVolume1 != null)
			{
				audioEndpointVolume1.Volume = Value;
			}
		}

		private void _OnMouseLeave(object P_0, EventArgs P_1)
		{
			bool1 = false;
		}

		private void _OnMouseMove(object P_0, MouseEventArgs P_1)
		{
			bool1 = true;
			if (Value > 100)
			{
				Value = 100;
			}
			else if (Value < 0)
			{
				Value = 0;
			}
			if (audioEndpointVolume1 != null)
			{
				audioEndpointVolume1.Volume = Value;
			}
			int num = 0;
			PointF pointF = new PointF(P_1.X, P_1.Y);
			if (!leftButtonDown)
			{
				return;
			}
			try
			{
				switch (_orientation)
				{
				case Orientation.Horizontal:
					num = ((!(pointF.X + (float)_trackerSize.Width - mouseStartPos >= (float)(base.Width - _indentWidth))) ? ((!(pointF.X - mouseStartPos <= (float)_indentWidth)) ? ((int)((double)((pointF.X - mouseStartPos - (float)_indentWidth) * (float)(_maximum - _minimum) / (float)(base.Width - 2 * _indentWidth - _trackerSize.Width)) + 0.5)) : 0) : (_maximum - _minimum));
					break;
				case Orientation.Vertical:
					num = ((!(pointF.Y + (float)(_trackerSize.Width / 2) >= (float)(base.Height - _indentHeight))) ? ((!(pointF.Y + (float)(_trackerSize.Width / 2) <= (float)_indentHeight)) ? ((int)((double)(((float)base.Height - pointF.Y + (float)(_trackerSize.Width / 2) - mouseStartPos - (float)_indentHeight) * (float)(_maximum - _minimum) / (float)(base.Height - 2 * _indentHeight)) + 0.5)) : (_maximum - _minimum)) : 0);
					break;
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				int value = _value;
				_value = _minimum + num;
				Invalidate();
				if (value != _value)
				{
					OnScroll();
					OnValueChanged(_value);
				}
			}
		}
	}
}
