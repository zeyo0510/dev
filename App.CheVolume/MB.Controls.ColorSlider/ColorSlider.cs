using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace MB.Controls.ColorSlider
{
	[ToolboxBitmap(typeof(TrackBar))]
	[DefaultEvent("Scroll")]
	[DefaultProperty("BarInnerColor")]
	public class ColorSlider : Control
	{
		public enum ColorSchemas
		{
			PerlBlueGreen,
			PerlRedCoral,
			PerlGold,
			PerlRoyalColors
		}

		[CompilerGenerated]
		private EventHandler ValueChanged;

		[CompilerGenerated]
		private ScrollEventHandler Scroll;

		private Rectangle thumbRect;

		private Rectangle barRect;

		private Rectangle barHalfRect;

		private Rectangle thumbHalfRect;

		private Rectangle elapsedRect;

		private int _thumbSize = 15;

		private GraphicsPath _thumbCustomShape;

		private Size _thumbRoundRectSize = new Size(8, 8);

		private Size _borderRoundRectSize = new Size(8, 8);

		private Orientation _barOrientation;

		private int _trackerValue = 50;

		private int _minimum;

		private int _maximum = 100;

		private uint _smallChange = 1u;

		private uint _largeChange = 5u;

		private bool _drawFocusRectangle = true;

		private bool _drawSemitransparentThumb = true;

		private bool _mouseEffects = true;

		private int _mouseWheelBarPartitions = 10;

		private Color _thumbOuterColor = Color.White;

		private Color _thumbInnerColor = Color.Gainsboro;

		private Color _thumbPenColor = Color.Silver;

		private Color _barOuterColor = Color.SkyBlue;

		private Color _barInnerColor = Color.DarkSlateBlue;

		private Color _barPenColor = Color.Gainsboro;

		private Color _elapsedOuterColor = Color.DarkGreen;

		private Color _elapsedInnerColor = Color.Chartreuse;

		private Color[,] aColorSchema = new Color[4, 8]
		{
			{
				Color.White,
				Color.Gainsboro,
				Color.Silver,
				Color.SkyBlue,
				Color.DarkSlateBlue,
				Color.Gainsboro,
				Color.DarkGreen,
				Color.Chartreuse
			},
			{
				Color.White,
				Color.Gainsboro,
				Color.Silver,
				Color.Red,
				Color.DarkRed,
				Color.Gainsboro,
				Color.Coral,
				Color.LightCoral
			},
			{
				Color.White,
				Color.Gainsboro,
				Color.Silver,
				Color.GreenYellow,
				Color.Yellow,
				Color.Gold,
				Color.Orange,
				Color.OrangeRed
			},
			{
				Color.White,
				Color.Gainsboro,
				Color.Silver,
				Color.Red,
				Color.Crimson,
				Color.Gainsboro,
				Color.DarkViolet,
				Color.Violet
			}
		};

		private ColorSchemas colorSchema;

		private bool mouseInRegion;

		private bool mouseInThumbRegion;

		private IContainer components;

		[Browsable(false)]
		public Rectangle ThumbRect
		{
			get
			{
				return thumbRect;
			}
		}

		[Description("Set Slider thumb size")]
		[Category("ColorSlider")]
		[DefaultValue(15)]
		public int ThumbSize
		{
			get
			{
				return _thumbSize;
			}
			set
			{
				if ((value > 0) & (value < ((_barOrientation == Orientation.Horizontal) ? base.ClientRectangle.Width : base.ClientRectangle.Height)))
				{
					_thumbSize = value;
					Invalidate();
					return;
				}
				throw new ArgumentOutOfRangeException("TrackSize has to be greather than zero and lower than half of Slider width");
			}
		}

		[Description("Set Slider's thumb's custom shape")]
		[Category("ColorSlider")]
		[Browsable(false)]
		[DefaultValue(typeof(GraphicsPath), "null")]
		public GraphicsPath ThumbCustomShape
		{
			get
			{
				return _thumbCustomShape;
			}
			set
			{
				_thumbCustomShape = value;
				_thumbSize = (int)((_barOrientation == Orientation.Horizontal) ? value.GetBounds().Width : value.GetBounds().Height) + 1;
				Invalidate();
			}
		}

		[Description("Set Slider's thumb round rect size")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(Size), "8; 8")]
		public Size ThumbRoundRectSize
		{
			get
			{
				return _thumbRoundRectSize;
			}
			set
			{
				int num = value.Height;
				int num2 = value.Width;
				if (num <= 0)
				{
					num = 1;
				}
				if (num2 <= 0)
				{
					num2 = 1;
				}
				_thumbRoundRectSize = new Size(num2, num);
				Invalidate();
			}
		}

		[Description("Set Slider's border round rect size")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(Size), "8; 8")]
		public Size BorderRoundRectSize
		{
			get
			{
				return _borderRoundRectSize;
			}
			set
			{
				int num = value.Height;
				int num2 = value.Width;
				if (num <= 0)
				{
					num = 1;
				}
				if (num2 <= 0)
				{
					num2 = 1;
				}
				_borderRoundRectSize = new Size(num2, num);
				Invalidate();
			}
		}

		[Description("Set Slider orientation")]
		[Category("ColorSlider")]
		[DefaultValue(Orientation.Horizontal)]
		public Orientation Orientation
		{
			get
			{
				return _barOrientation;
			}
			set
			{
				if (_barOrientation != value)
				{
					_barOrientation = value;
					int num = base.Width;
					base.Width = base.Height;
					base.Height = num;
					if (_thumbCustomShape != null)
					{
						_thumbSize = (int)((_barOrientation == Orientation.Horizontal) ? _thumbCustomShape.GetBounds().Width : _thumbCustomShape.GetBounds().Height) + 1;
					}
					Invalidate();
				}
			}
		}

		[Description("Set Slider value")]
		[Category("ColorSlider")]
		[DefaultValue(50)]
		public int Value
		{
			get
			{
				return _trackerValue;
			}
			set
			{
				if ((value >= _minimum) & (value <= _maximum))
				{
					_trackerValue = value;
					if (ValueChanged != null)
					{
						ValueChanged(this, new EventArgs());
					}
					Invalidate();
					return;
				}
				throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
			}
		}

		[Description("Set Slider minimal point")]
		[Category("ColorSlider")]
		[DefaultValue(0)]
		public int Minimum
		{
			get
			{
				return _minimum;
			}
			set
			{
				if (value < _maximum)
				{
					_minimum = value;
					if (_trackerValue < _minimum)
					{
						_trackerValue = _minimum;
						if (ValueChanged != null)
						{
							ValueChanged(this, new EventArgs());
						}
					}
					Invalidate();
					return;
				}
				throw new ArgumentOutOfRangeException("Minimal value is greather than maximal one");
			}
		}

		[Description("Set Slider maximal point")]
		[Category("ColorSlider")]
		[DefaultValue(100)]
		public int Maximum
		{
			get
			{
				return _maximum;
			}
			set
			{
				if (value > _minimum)
				{
					_maximum = value;
					if (_trackerValue > _maximum)
					{
						_trackerValue = _maximum;
						if (ValueChanged != null)
						{
							ValueChanged(this, new EventArgs());
						}
					}
					Invalidate();
					return;
				}
				throw new ArgumentOutOfRangeException("Maximal value is lower than minimal one");
			}
		}

		[Description("Set trackbar's small change")]
		[Category("ColorSlider")]
		[DefaultValue(1)]
		public uint SmallChange
		{
			get
			{
				return _smallChange;
			}
			set
			{
				_smallChange = value;
			}
		}

		[Description("Set trackbar's large change")]
		[Category("ColorSlider")]
		[DefaultValue(5)]
		public uint LargeChange
		{
			get
			{
				return _largeChange;
			}
			set
			{
				_largeChange = value;
			}
		}

		[Description("Set whether to draw focus rectangle")]
		[Category("ColorSlider")]
		[DefaultValue(true)]
		public bool DrawFocusRectangle
		{
			get
			{
				return _drawFocusRectangle;
			}
			set
			{
				_drawFocusRectangle = value;
				Invalidate();
			}
		}

		[Description("Set whether to draw semitransparent thumb")]
		[Category("ColorSlider")]
		[DefaultValue(true)]
		public bool DrawSemitransparentThumb
		{
			get
			{
				return _drawSemitransparentThumb;
			}
			set
			{
				_drawSemitransparentThumb = value;
				Invalidate();
			}
		}

		[Description("Set whether mouse entry and exit actions have impact on how control look")]
		[Category("ColorSlider")]
		[DefaultValue(true)]
		public bool MouseEffects
		{
			get
			{
				return _mouseEffects;
			}
			set
			{
				_mouseEffects = value;
				Invalidate();
			}
		}

		[Description("Set to how many parts is bar divided when using mouse wheel")]
		[Category("ColorSlider")]
		[DefaultValue(10)]
		public int MouseWheelBarPartitions
		{
			get
			{
				return _mouseWheelBarPartitions;
			}
			set
			{
				if (value > 0)
				{
					_mouseWheelBarPartitions = value;
					return;
				}
				throw new ArgumentOutOfRangeException("MouseWheelBarPartitions has to be greather than zero");
			}
		}

		[Description("Set Slider thumb outer color")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(Color), "White")]
		public Color ThumbOuterColor
		{
			get
			{
				return _thumbOuterColor;
			}
			set
			{
				_thumbOuterColor = value;
				Invalidate();
			}
		}

		[Description("Set Slider thumb inner color")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(Color), "Gainsboro")]
		public Color ThumbInnerColor
		{
			get
			{
				return _thumbInnerColor;
			}
			set
			{
				_thumbInnerColor = value;
				Invalidate();
			}
		}

		[Description("Set Slider thumb pen color")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(Color), "Silver")]
		public Color ThumbPenColor
		{
			get
			{
				return _thumbPenColor;
			}
			set
			{
				_thumbPenColor = value;
				Invalidate();
			}
		}

		[Description("Set Slider bar outer color")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(Color), "SkyBlue")]
		public Color BarOuterColor
		{
			get
			{
				return _barOuterColor;
			}
			set
			{
				_barOuterColor = value;
				Invalidate();
			}
		}

		[Description("Set Slider bar inner color")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(Color), "DarkSlateBlue")]
		public Color BarInnerColor
		{
			get
			{
				return _barInnerColor;
			}
			set
			{
				_barInnerColor = value;
				Invalidate();
			}
		}

		[Description("Set Slider bar pen color")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(Color), "Gainsboro")]
		public Color BarPenColor
		{
			get
			{
				return _barPenColor;
			}
			set
			{
				_barPenColor = value;
				Invalidate();
			}
		}

		[Description("Set Slider's elapsed part outer color")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(Color), "DarkGreen")]
		public Color ElapsedOuterColor
		{
			get
			{
				return _elapsedOuterColor;
			}
			set
			{
				_elapsedOuterColor = value;
				Invalidate();
			}
		}

		[Description("Set Slider's elapsed part inner color")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(Color), "Chartreuse")]
		public Color ElapsedInnerColor
		{
			get
			{
				return _elapsedInnerColor;
			}
			set
			{
				_elapsedInnerColor = value;
				Invalidate();
			}
		}

		[Description("Set Slider color schema. Has no effect when slider colors are changed manually after schema was applied.")]
		[Category("ColorSlider")]
		[DefaultValue(typeof(ColorSchemas), "PerlBlueGreen")]
		public ColorSchemas ColorSchema
		{
			get
			{
				return colorSchema;
			}
			set
			{
				colorSchema = value;
				byte b = (byte)value;
				_thumbOuterColor = aColorSchema[b, 0];
				_thumbInnerColor = aColorSchema[b, 1];
				_thumbPenColor = aColorSchema[b, 2];
				_barOuterColor = aColorSchema[b, 3];
				_barInnerColor = aColorSchema[b, 4];
				_barPenColor = aColorSchema[b, 5];
				_elapsedOuterColor = aColorSchema[b, 6];
				_elapsedInnerColor = aColorSchema[b, 7];
				Invalidate();
			}
		}

		[SpecialName]
		[CompilerGenerated]
		public void ValueChangedAdd(EventHandler P_0)
		{
			EventHandler eventHandler = ValueChanged;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value = (EventHandler)Delegate.Combine(eventHandler2, P_0);
				eventHandler = Interlocked.CompareExchange(ref ValueChanged, value, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}

		[SpecialName]
		[CompilerGenerated]
		public void ValueChangedRemove(EventHandler P_0)
		{
			EventHandler eventHandler = ValueChanged;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler value = (EventHandler)Delegate.Remove(eventHandler2, P_0);
				eventHandler = Interlocked.CompareExchange(ref ValueChanged, value, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}

		public ColorSlider(int P_0, int P_1, int P_2)
		{
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.UserMouse | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			BackColor = Color.Transparent;
			Minimum = P_0;
			Maximum = P_1;
			Value = P_2;
		}

		public ColorSlider()
			: this(0, 100, 50)
		{
		}

		protected override void OnPaint(PaintEventArgs P_0)
		{
			if (!base.Enabled)
			{
				Color[] array = DesaturateColors(_thumbOuterColor, _thumbInnerColor, _thumbPenColor, _barOuterColor, _barInnerColor, _barPenColor, _elapsedOuterColor, _elapsedInnerColor);
				DrawColorSlider(P_0, array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]);
			}
			else if (_mouseEffects && mouseInRegion)
			{
				Color[] array2 = LightenColors(_thumbOuterColor, _thumbInnerColor, _thumbPenColor, _barOuterColor, _barInnerColor, _barPenColor, _elapsedOuterColor, _elapsedInnerColor);
				DrawColorSlider(P_0, array2[0], array2[1], array2[2], array2[3], array2[4], array2[5], array2[6], array2[7]);
			}
			else
			{
				DrawColorSlider(P_0, _thumbOuterColor, _thumbInnerColor, _thumbPenColor, _barOuterColor, _barInnerColor, _barPenColor, _elapsedOuterColor, _elapsedInnerColor);
			}
		}

		private void DrawColorSlider(PaintEventArgs P_0, Color P_1, Color P_2, Color P_3, Color P_4, Color P_5, Color P_6, Color P_7, Color P_8)
		{
			try
			{
				if (_barOrientation == Orientation.Horizontal)
				{
					int num = (_trackerValue - _minimum) * (base.ClientRectangle.Width - _thumbSize) / (_maximum - _minimum);
					thumbRect = new Rectangle(num, 1, _thumbSize - 1, base.ClientRectangle.Height - 3);
				}
				else
				{
					int num2 = (_trackerValue - _minimum) * (base.ClientRectangle.Height - _thumbSize) / (_maximum - _minimum);
					thumbRect = new Rectangle(1, num2, base.ClientRectangle.Width - 3, _thumbSize - 1);
				}
				barRect = base.ClientRectangle;
				thumbHalfRect = thumbRect;
				LinearGradientMode linearGradientMode;
				if (_barOrientation == Orientation.Horizontal)
				{
					barRect.Inflate(-1, -barRect.Height / 3);
					barHalfRect = barRect;
					barHalfRect.Height /= 2;
					linearGradientMode = LinearGradientMode.Vertical;
					thumbHalfRect.Height /= 2;
					elapsedRect = barRect;
					elapsedRect.Width = thumbRect.Left + _thumbSize / 2;
				}
				else
				{
					barRect.Inflate(-barRect.Width / 3, -1);
					barHalfRect = barRect;
					barHalfRect.Width /= 2;
					linearGradientMode = LinearGradientMode.Horizontal;
					thumbHalfRect.Width /= 2;
					elapsedRect = barRect;
					elapsedRect.Height = thumbRect.Top + _thumbSize / 2;
				}
				GraphicsPath graphicsPath;
				if (_thumbCustomShape == null)
				{
					graphicsPath = CreateRoundRectPath(thumbRect, _thumbRoundRectSize);
				}
				else
				{
					graphicsPath = _thumbCustomShape;
					Matrix matrix = new Matrix();
					matrix.Translate((float)thumbRect.Left - graphicsPath.GetBounds().Left, (float)thumbRect.Top - graphicsPath.GetBounds().Top);
					graphicsPath.Transform(matrix);
				}
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(barHalfRect, P_4, P_5, linearGradientMode))
				{
					linearGradientBrush.WrapMode = WrapMode.TileFlipXY;
					P_0.Graphics.FillRectangle(linearGradientBrush, barRect);
					using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(barHalfRect, P_7, P_8, linearGradientMode))
					{
						linearGradientBrush2.WrapMode = WrapMode.TileFlipXY;
						if (base.Capture && _drawSemitransparentThumb)
						{
							Region region = new Region(elapsedRect);
							region.Exclude(graphicsPath);
							P_0.Graphics.FillRegion(linearGradientBrush2, region);
						}
						else
						{
							P_0.Graphics.FillRectangle(linearGradientBrush2, elapsedRect);
						}
					}
					using (Pen pen = new Pen(P_6, 0.5f))
					{
						P_0.Graphics.DrawRectangle(pen, barRect);
					}
				}
				Color color = P_1;
				Color color2 = P_2;
				if (base.Capture && _drawSemitransparentThumb)
				{
					color = Color.FromArgb(175, P_1);
					color2 = Color.FromArgb(175, P_2);
				}
				using (LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(thumbHalfRect, color, color2, linearGradientMode))
				{
					linearGradientBrush3.WrapMode = WrapMode.TileFlipXY;
					P_0.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
					P_0.Graphics.FillPath(linearGradientBrush3, graphicsPath);
					Color color3 = P_3;
					if (_mouseEffects && (base.Capture || mouseInThumbRegion))
					{
						color3 = ControlPaint.Dark(color3);
					}
					using (Pen pen2 = new Pen(color3))
					{
						P_0.Graphics.DrawPath(pen2, graphicsPath);
					}
				}
				if (!(Focused & _drawFocusRectangle))
				{
					return;
				}
				using (Pen pen3 = new Pen(Color.FromArgb(200, P_6)))
				{
					pen3.DashStyle = DashStyle.Dot;
					Rectangle clientRectangle = base.ClientRectangle;
					clientRectangle.Width -= 2;
					clientRectangle.Height--;
					clientRectangle.X++;
					using (GraphicsPath path = CreateRoundRectPath(clientRectangle, _borderRoundRectSize))
					{
						P_0.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
						P_0.Graphics.DrawPath(pen3, path);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("DrawBackGround Error in " + base.Name + ":" + ex.Message);
			}
		}

		protected override void OnEnabledChanged(EventArgs P_0)
		{
			base.OnEnabledChanged(P_0);
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs P_0)
		{
			base.OnMouseEnter(P_0);
			mouseInRegion = true;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs P_0)
		{
			base.OnMouseLeave(P_0);
			mouseInRegion = false;
			mouseInThumbRegion = false;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs P_0)
		{
			base.OnMouseDown(P_0);
			if (P_0.Button == MouseButtons.Left)
			{
				base.Capture = true;
				if (Scroll != null)
				{
					Scroll(this, new ScrollEventArgs(ScrollEventType.ThumbTrack, _trackerValue));
				}
				if (ValueChanged != null)
				{
					ValueChanged(this, new EventArgs());
				}
				OnMouseMove(P_0);
			}
		}

		protected override void OnMouseMove(MouseEventArgs P_0)
		{
			base.OnMouseMove(P_0);
			mouseInThumbRegion = IsPointInRect(P_0.Location, thumbRect);
			if (base.Capture & (P_0.Button == MouseButtons.Left))
			{
				ScrollEventType type = ScrollEventType.ThumbPosition;
				Point location = P_0.Location;
				int num = ((_barOrientation == Orientation.Horizontal) ? location.X : location.Y);
				int num2 = _thumbSize >> 1;
				num -= num2;
				float num3 = (float)(_maximum - _minimum) / (float)(((_barOrientation == Orientation.Horizontal) ? base.ClientSize.Width : base.ClientSize.Height) - 2 * num2);
				_trackerValue = (int)((float)num * num3 + (float)_minimum);
				if (_trackerValue <= _minimum)
				{
					_trackerValue = _minimum;
					type = ScrollEventType.First;
				}
				else if (_trackerValue >= _maximum)
				{
					_trackerValue = _maximum;
					type = ScrollEventType.Last;
				}
				if (Scroll != null)
				{
					Scroll(this, new ScrollEventArgs(type, _trackerValue));
				}
				if (ValueChanged != null)
				{
					ValueChanged(this, new EventArgs());
				}
			}
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs P_0)
		{
			base.OnMouseUp(P_0);
			base.Capture = false;
			mouseInThumbRegion = IsPointInRect(P_0.Location, thumbRect);
			if (Scroll != null)
			{
				Scroll(this, new ScrollEventArgs(ScrollEventType.EndScroll, _trackerValue));
			}
			if (ValueChanged != null)
			{
				ValueChanged(this, new EventArgs());
			}
			Invalidate();
		}

		protected override void OnMouseWheel(MouseEventArgs P_0)
		{
			base.OnMouseWheel(P_0);
			int num = P_0.Delta / 120 * (_maximum - _minimum) / _mouseWheelBarPartitions;
			SetProperValue(Value + num);
		}

		protected override void OnGotFocus(EventArgs P_0)
		{
			base.OnGotFocus(P_0);
			Invalidate();
		}

		protected override void OnLostFocus(EventArgs P_0)
		{
			base.OnLostFocus(P_0);
			Invalidate();
		}

		protected override void OnKeyUp(KeyEventArgs P_0)
		{
			base.OnKeyUp(P_0);
			switch (P_0.KeyCode)
			{
			case Keys.Left:
			case Keys.Down:
				SetProperValue(Value - (int)_smallChange);
				if (Scroll != null)
				{
					Scroll(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, Value));
				}
				break;
			case Keys.Up:
			case Keys.Right:
				SetProperValue(Value + (int)_smallChange);
				if (Scroll != null)
				{
					Scroll(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, Value));
				}
				break;
			case Keys.Home:
				Value = _minimum;
				break;
			case Keys.End:
				Value = _maximum;
				break;
			case Keys.Next:
				SetProperValue(Value - (int)_largeChange);
				if (Scroll != null)
				{
					Scroll(this, new ScrollEventArgs(ScrollEventType.LargeDecrement, Value));
				}
				break;
			case Keys.Prior:
				SetProperValue(Value + (int)_largeChange);
				if (Scroll != null)
				{
					Scroll(this, new ScrollEventArgs(ScrollEventType.LargeIncrement, Value));
				}
				break;
			}
			if (Scroll != null && Value == _minimum)
			{
				Scroll(this, new ScrollEventArgs(ScrollEventType.First, Value));
			}
			if (Scroll != null && Value == _maximum)
			{
				Scroll(this, new ScrollEventArgs(ScrollEventType.Last, Value));
			}
			Point point = PointToClient(Cursor.Position);
			OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, point.X, point.Y, 0));
		}

		protected override bool ProcessDialogKey(Keys P_0)
		{
			if ((P_0 == Keys.Tab) | (Control.ModifierKeys == Keys.Shift))
			{
				return base.ProcessDialogKey(P_0);
			}
			OnKeyDown(new KeyEventArgs(P_0));
			return true;
		}

		public static GraphicsPath CreateRoundRectPath(Rectangle P_0, Size P_1)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddLine(P_0.Left + P_1.Width / 2, P_0.Top, P_0.Right - P_1.Width / 2, P_0.Top);
			graphicsPath.AddArc(P_0.Right - P_1.Width, P_0.Top, P_1.Width, P_1.Height, 270f, 90f);
			graphicsPath.AddLine(P_0.Right, P_0.Top + P_1.Height / 2, P_0.Right, P_0.Bottom - P_1.Width / 2);
			graphicsPath.AddArc(P_0.Right - P_1.Width, P_0.Bottom - P_1.Height, P_1.Width, P_1.Height, 0f, 90f);
			graphicsPath.AddLine(P_0.Right - P_1.Width / 2, P_0.Bottom, P_0.Left + P_1.Width / 2, P_0.Bottom);
			graphicsPath.AddArc(P_0.Left, P_0.Bottom - P_1.Height, P_1.Width, P_1.Height, 90f, 90f);
			graphicsPath.AddLine(P_0.Left, P_0.Bottom - P_1.Height / 2, P_0.Left, P_0.Top + P_1.Height / 2);
			graphicsPath.AddArc(P_0.Left, P_0.Top, P_1.Width, P_1.Height, 180f, 90f);
			return graphicsPath;
		}

		public static Color[] DesaturateColors(params Color[] colorsToDesaturate)
		{
			Color[] array = new Color[colorsToDesaturate.Length];
			for (int i = 0; i < colorsToDesaturate.Length; i++)
			{
				int num = (int)((double)(int)colorsToDesaturate[i].R * 0.3 + (double)(int)colorsToDesaturate[i].G * 0.6 + (double)(int)colorsToDesaturate[i].B * 0.1);
				array[i] = Color.FromArgb(-65793 * (255 - num) - 1);
			}
			return array;
		}

		public static Color[] LightenColors(params Color[] colorsToLighten)
		{
			Color[] array = new Color[colorsToLighten.Length];
			for (int i = 0; i < colorsToLighten.Length; i++)
			{
				array[i] = ControlPaint.Light(colorsToLighten[i]);
			}
			return array;
		}

		private void SetProperValue(int P_0)
		{
			if (P_0 < _minimum)
			{
				Value = _minimum;
			}
			else if (P_0 > _maximum)
			{
				Value = _maximum;
			}
			else
			{
				Value = P_0;
			}
		}

		private static bool IsPointInRect(Point P_0, Rectangle P_1)
		{
			if ((P_0.X > P_1.Left) & (P_0.X < P_1.Right) & (P_0.Y > P_1.Top) & (P_0.Y < P_1.Bottom))
			{
				return true;
			}
			return false;
		}

		protected override void Dispose(bool P_0)
		{
			if (P_0 && components != null)
			{
				components.Dispose();
			}
			base.Dispose(P_0);
		}

		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.Size = new System.Drawing.Size(200, 30);
			base.ResumeLayout(false);
		}
	}
}
