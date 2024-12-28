using System;
using System.Collections.Generic;
using DockingFrames;
using VariIconsReload.Properties;
using System.Drawing;

namespace VariIconsReload.Tools
{
	/// <summary>
	/// base class for tool config options, which consist
	/// of toolbarcontrols and have a binding to a persistent
	/// storage. use them by adding them to the configoptions
	/// list of the tool.
	/// </summary>
	public abstract class ConfigOption : IDisposable
	{
		private List<ToolBarControl> _configControls;
		#region ctor
		public ConfigOption()
		{
			_configControls = new List<ToolBarControl>();
			_configControls.Add(new ToolBarSeparator());
		}
		public virtual void Dispose()
		{
			foreach (ToolBarControl ctl in _configControls)
				ctl.Dispose();
			_configControls.Clear();
		}
		#endregion
		public virtual void LoadConfig()
		{
		}
		/// <summary>
		/// gets the list of toolbar controls
		/// </summary>
		public List<ToolBarControl> ConfigControls
		{
			get { return _configControls; }
		}
		protected void RaiseChanged()
		{
			if (Changed != null)
				Changed(this, EventArgs.Empty);
		}
		public event EventHandler Changed;
	}
	/// <summary>
	/// abstract option for a value with up and down buttons
	/// </summary>
	public abstract class UpDownOption : ConfigOption
	{
		#region variables
		private ToolBarLabel _lblValue = null;
		private ToolBarButton _btnPlus = null, _btnMinus = null;
		private int _value, _max, _min, _step;
		private string _unit;
		#endregion
		/// <summary>
		/// constructor
		/// </summary>
		public UpDownOption(string name, string unit, int value, int max, int min, int step)
		{
			_max = max;
			_min = min;
			_step = step;
			_unit = unit == null ? "" : unit;
			_value = Math.Min(max, Math.Max(min, value));
			//
			ToolBarLabel lbl = new ToolBarLabel();
			lbl.Command.Text = name;
			lbl.Appearance = Appearance.Text;
			ConfigControls.Add(lbl);
			//
			_btnPlus = new ToolBarButton();
			_btnPlus.Command.Image = Resources.lst_plus;
			_btnPlus.Command.Click += new EventHandler(Command_Click);
			ConfigControls.Add(_btnPlus);
			//
			_lblValue = new ToolBarLabel();
			_lblValue.Appearance = Appearance.Text;
			_lblValue.Command.Text = _value.ToString() + _unit;
			ConfigControls.Add(_lblValue);
			//
			_btnMinus = new ToolBarButton();
			_btnMinus.Command.Image = Resources.lst_minus;
			_btnMinus.Command.Click += new EventHandler(Command_Click);
			ConfigControls.Add(_btnMinus);
		}
		#region controller
		//step up or down
		void Command_Click(object sender, EventArgs e)
		{
			if (sender == _btnPlus.Command)
				SetValue(_value + _step);
			else if (sender == _btnMinus.Command)
				SetValue(_value - _step);
		}
		//set and display the value
		protected void SetValue(int value)
		{
			value = Math.Min(_max, Math.Max(_min, value));
			if (value == _value)
				return;
			_value = value;
			_lblValue.Command.Text = value.ToString() + _unit;
			SaveConfig();
			RaiseChanged();
		}
		//save to persistent store
		protected abstract void SaveConfig();
		#endregion
		/// <summary>
		/// gets or sets the value between min and max
		/// </summary>
		public int Value
		{
			get { return _value; }
		}
	}
	/// <summary>
	/// config option for triggering a command
	/// </summary>
	public class ActionOption : ConfigOption
	{
		private ToolBarButton _btnAction;
		public ActionOption(string text, Image image)
		{
			ConfigControls.Add(_btnAction = new ToolBarButton());
			if (text != null && image != null)
				_btnAction.Appearance = Appearance.ImageAndText;
			else if (text != null)
				_btnAction.Appearance = Appearance.Text;
			_btnAction.Command.Text = text;
			_btnAction.Command.Image = image;
			_btnAction.Command.Click += new EventHandler(Command_Click);
		}

		void Command_Click(object sender, EventArgs e)
		{
			if (Action != null)
				Action(this, e);
		}
		public event EventHandler Action;
	}
	/// <summary>
	/// config option for a width control
	/// </summary>
	public class WidthOption : UpDownOption
	{
		public WidthOption()
			: base(Resources.DrawTool_Width, "px", 1, 10, 1, 1) { }
		public override void LoadConfig()
		{
			SetValue(Settings.Default.DrawTool_Width);
		}
		protected override void SaveConfig()
		{
			Settings.Default.DrawTool_Width = Value;
		}
	}
	/// <summary>
	/// strength option for a pen, i.e. the hardnes
	/// of the box
	/// </summary>
	public class StrengthOption : UpDownOption
	{
		public StrengthOption()
			: base(Resources.BrushTool_Strength, "%", 50, 100, 0, 10) { }
		public override void LoadConfig()
		{
			SetValue(Settings.Default.BrushTool_Strength);
		}
		protected override void SaveConfig()
		{
			Settings.Default.BrushTool_Strength = Value;
		}
	}
	/// <summary>
	/// box shape enumaration
	/// </summary>
	public enum TipShape
	{
		Square = 0, Circle = 1
	}
	/// <summary>
	/// tipshape option for a pen
	/// </summary>
	public class TipShapeOption : ConfigOption
	{
		#region variables
		private TipShape _tipshape = TipShape.Square;
		private DockingFrames.ToolBarButton _btnSquare, _btnCircle;
		#endregion
		public TipShapeOption()
		{
			ToolBarLabel lbl = new ToolBarLabel();
			lbl.Appearance = DockingFrames.Appearance.Text;
			lbl.Command.Text = Resources.PenTool_TipShape;
			ConfigControls.Add(lbl);
			//
			_btnSquare = new DockingFrames.ToolBarButton();
			_btnSquare.Command.Image = Resources.tip_square;
			_btnSquare.Command.Checked = true;
			_btnSquare.Command.Click += new EventHandler(TipShape_Click);
			ConfigControls.Add(_btnSquare);
			//
			_btnCircle = new DockingFrames.ToolBarButton();
			_btnCircle.Command.Image = Resources.tip_circle;
			_btnCircle.Command.Click += new EventHandler(TipShape_Click);
			ConfigControls.Add(_btnCircle);
		}
		#region controller
		//select box shape
		void TipShape_Click(object sender, EventArgs e)
		{
			if (sender == _btnCircle.Command)
				Value = TipShape.Circle;
			else if (sender == _btnSquare.Command)
				Value = TipShape.Square;
		}
		//load box shape
		public override void LoadConfig()
		{
			base.LoadConfig();
			if (!Enum.IsDefined(typeof(TipShape), Settings.Default.PenTool_TipShape))
				return;
			Value = (TipShape)Settings.Default.PenTool_TipShape;
		}
		#endregion
		public TipShape Value
		{
			get { return _tipshape; }
			set
			{
				if (value == _tipshape)
					return;
				_tipshape = value;
				_btnCircle.Command.Checked = (value == TipShape.Circle);
				_btnSquare.Command.Checked = (value == TipShape.Square);
				Settings.Default.PenTool_TipShape = (int)_tipshape;
				RaiseChanged();
			}
		}
	}
	/// <summary>
	/// config option for anti alias or font hinting
	/// </summary>
	public class AntiAliasOption : ConfigOption
	{
		private ToolBarButton _btnAntiAlias;
		public AntiAliasOption()
		{
			_btnAntiAlias = new ToolBarButton();
			_btnAntiAlias.Command.Image = Resources.antialias;
			_btnAntiAlias.Command.Click += new EventHandler(Command_Click);
			ConfigControls.Add(_btnAntiAlias);
		}
		void Command_Click(object sender, EventArgs e)
		{
			_btnAntiAlias.Command.Checked = !
				_btnAntiAlias.Command.Checked;
			Settings.Default.ConfigOption_AntiAlias = _btnAntiAlias.Command.Checked;
			RaiseChanged();
		}
		public override void LoadConfig()
		{
			_btnAntiAlias.Command.Checked = Settings.Default.ConfigOption_AntiAlias;
		}
		public bool Value
		{
			get { return _btnAntiAlias.Command.Checked; }
			set
			{
				_btnAntiAlias.Command.Checked =
					Settings.Default.ConfigOption_AntiAlias = value;
			}
		}
	}
	/// <summary>
	/// used for the corners of a rounded rectangle tool
	/// </summary>
	public class DiameterOption : UpDownOption
	{
		public DiameterOption()
			: base(Resources.ToolOption_Diameter, "px", 3, 10, 1, 1) { }
		public override void LoadConfig()
		{
			SetValue(Settings.Default.ToolOption_Diameter);
		}
		protected override void SaveConfig()
		{
			Settings.Default.ToolOption_Diameter = Value;
		}
	}
	/// <summary>
	/// used for pie tools
	/// </summary>
	public class StartAngleOption : UpDownOption
	{
		public StartAngleOption()
			: base(Resources.StartAngleOption_Name, "°", 0, 350, 0, 10) { }
		public override void LoadConfig()
		{
			SetValue(Settings.Default.StartAngleOption_Value);
		}
		protected override void SaveConfig()
		{
			Settings.Default.StartAngleOption_Value = Value;
		}
	}
	/// <summary>
	/// used for pie tools
	/// </summary>
	public class SwepAngleOption : UpDownOption
	{
		public SwepAngleOption()
			: base(Resources.SwepAngleOption_Name, "°", 0, 350, 0, 10) { }
		public override void LoadConfig()
		{
			SetValue(Settings.Default.SwepAngleOption_Value);
		}
		protected override void SaveConfig()
		{
			Settings.Default.SwepAngleOption_Value = Value;
		}
	}
	/// <summary>
	/// used for spline tool
	/// </summary>
	public class TensionOption : UpDownOption
	{
		public TensionOption()
			: base(Resources.TensionOption_Name, "%", 70, 100, 0, 10) { }
		public override void LoadConfig()
		{
			SetValue(Settings.Default.TensionOption_Value);
		}
		protected override void SaveConfig()
		{
			Settings.Default.TensionOption_Value = Value;
		}
	}
	/// <summary>
	/// used for flood fill
	/// </summary>
	public class ToleranceOption : UpDownOption
	{
		public ToleranceOption()
			: base(Resources.ToleranceOption_Name, "", 0, 255, 0, 5) { }
		public override void LoadConfig()
		{
			SetValue(Settings.Default.ToleranceOption_Value);
		}
		protected override void SaveConfig()
		{
			Settings.Default.ToleranceOption_Value = Value;
		}
	}
}
