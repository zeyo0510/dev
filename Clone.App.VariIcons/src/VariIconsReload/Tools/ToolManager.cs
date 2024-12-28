using System;
using System.Collections.Generic;
using DockingFrames;
using DockingFrames.CommandManager;
using VariIconsReload.Components;
using VariIconsReload.BrushModel;
using VariIconsReload.Properties;
using VariIconsSDK.Model;

namespace VariIconsReload.Tools
{
	/// <summary>
	/// class for managing tools
	/// </summary>
	public class ToolManager : Manager<ToolManager>, IDisposable
	{
		private class SuspendableToolBar : ToolBar
		{
			private uint _suspend = 0;
			public override void Refresh()
			{
				if (_suspend == 0)
					base.Refresh();
			}
			public override void AdjustLayout()
			{
				if (_suspend == 0)
					base.AdjustLayout();
			}
			public override void Refresh(System.Drawing.Rectangle rct)
			{
				if (_suspend == 0)
					base.Refresh(rct);
			}
			public void SuspendUpdates()
			{
				_suspend++;
			}
			public void ResumeUpdates(bool performrefresh, bool performlayout)
			{
				_suspend--;
				if (performlayout) base.AdjustLayout();
				if (performrefresh) base.Refresh();
			}
		}
		#region variables
		private ToolBar _toolbox;
		private SuspendableToolBar _configbar;
		private BrushManager _brushes;
		//
		private Dictionary<ButtonCommand, ToolInfo> _toolbuttons;
		private ToolBarLabel _icon;
		private ToolBarBrushSwitcher _switcher;
		private Tool _currenttool;
		private ToolInfo _currentinfo;
		//special tools
		private ToolInfo _selectiontool;
		#endregion
		#region ctor
		public ToolManager()
		{
			_toolbuttons = new Dictionary<ButtonCommand, ToolInfo>();
			_icon = new ToolBarLabel();
			_icon.Appearance = Appearance.ImageAndText;
			_switcher = new ToolBarBrushSwitcher();
			_switcher.OutlineA = _switcher.OutlineB = false;
			_switcher.SwatchClicked += new EventHandler<ToolBarColorSwitcherBase.SwatchClickedEventArgs>(BrushSwitcher_SwatchClicked);
			//
			_brushes = BrushManager.RegisterInstance(this);
			_brushes.BrushChanged += new EventHandler(_brushes_BrushChanged);
			_switcher.BrushA = _brushes.BrushA;
			_switcher.BrushB = _brushes.BrushB;
			_switcher.SelectedArea = _brushes.SelectedA ?
				ToolBarColorSwitcherBase.HitArea.SwatchA :
				ToolBarColorSwitcherBase.HitArea.SwatchB;
			//
			_toolbox = new ToolBar();
			_toolbox.Guid = "F545C053-26B9-4C7A-905C-158276E31E3F";
			_toolbox.Lines = 2;
			_toolbox.Text = Resources.ToolManager_ToolBox_Text;
			AddToolInfos();
			_toolbox.ToolBarControls.Add(new ToolBarSeparator());
			_toolbox.ToolBarControls.Add(_switcher);
			//
			_configbar = new SuspendableToolBar();
			_configbar.Guid = "E910233D-751C-4D05-8B5A-CD158D458704";
			_configbar.Text = Resources.ToolManager_Configbar_Text;
			_configbar.ToolBarControls.Add(_icon);
			//select first tool
			foreach (ToolInfo info in _toolbuttons.Values)
			{
				SetTool(info);
				break;
			}
		}

		public void Dispose()
		{
			if (_toolbox != null)
			{
				//take out config controls
				SetTool(null);
				//
				_brushes.BrushChanged -= new EventHandler(_brushes_BrushChanged);
				BrushManager.UnregisterInstance(this);
				_brushes = null;
				//take out switcher
				_switcher.Dispose();
				_switcher = null;
				_icon.Dispose();
				_icon = null;
				_toolbuttons.Clear();
				_toolbuttons = null;
				//
				_configbar.Dispose();
				_configbar = null;
				_toolbox.Dispose();
				_toolbox = null;
			}
		}
		#endregion
		#region controller
		//add all tool infos
		private void AddToolInfos()
		{
			AddToolInfo(
				new ToolInfo<NavigatorTool>(Resources.tl_pointer, Resources.NavigatorTool_Name),
				_selectiontool = new ToolInfo<SelectionTool>(Resources.tl_select, Resources.SelectionTool_Name),
				new ToolInfo<PencilTool>(Resources.tl_pen, Resources.PencilTool_Name),
				new ToolInfo<EraserTool>(Resources.tl_eraser, Resources.EraserTool_Name));
			_toolbox.ToolBarControls.Add(new ToolBarSeparator());
			AddToolInfo(
				new ToolInfo<DrawSplineTool>(Resources.tl_curve, Resources.DrawSplineTool_Name, Resources.PointListTool_Description),
				new ToolInfo<DrawLineTool>(Resources.tl_line, Resources.DrawLineTool_Name, Resources.ShapeTool_Description),
				new ToolInfo<FillRectTool>(Resources.tl_filledrectangle, Resources.FillRectTool_Name, Resources.ShapeTool_Description),
				new ToolInfo<DrawRectTool>(Resources.tl_rectangle, Resources.DrawRectTool_Name, Resources.ShapeTool_Description),
				new ToolInfo<FillEllipseTool>(Resources.tl_filledellipse, Resources.FillEllipseTool_Name, Resources.ShapeTool_Description),
				new ToolInfo<DrawEllipseTool>(Resources.tl_ellipse, Resources.DrawEllipseTool_Name, Resources.ShapeTool_Description),
				new ToolInfo<FillPolygonTool>(Resources.tl_filledpolygon, Resources.FillPolygonTool_Name, Resources.PointListTool_Description),
				new ToolInfo<DrawPolygonTool>(Resources.tl_polygon, Resources.DrawPolygonTool_Name, Resources.PointListTool_Description),
				new ToolInfo<FillRoundedRectTool>(Resources.tl_froundrect, Resources.FillRoundedRectTool_Name, Resources.ShapeTool_Description),
				new ToolInfo<DrawRoundedRectTool>(Resources.tl_roundrect, Resources.DrawRoundedRectTool_Name, Resources.ShapeTool_Description),
				new ToolInfo<FillPieTool>(Resources.tl_filledpie, Resources.FillPieTool_Name, Resources.ShapeTool_Description),
				new ToolInfo<DrawPieTool>(Resources.tl_pie, Resources.DrawPieTool_Name, Resources.ShapeTool_Description));
			_toolbox.ToolBarControls.Add(new ToolBarSeparator());
			AddToolInfo(
				new ToolInfo<TextTool>(Resources.tl_text, Resources.TextTool_Name),
				new ToolInfo<AirBrushTool>(Resources.tl_airbrush, Resources.AirBrushTool_Name),
				new ToolInfo<PickTool>(Resources.tl_picker, Resources.PickTool_Name),
				new ToolInfo<FloodTool>(Resources.tl_flood, Resources.FloodTool_Name),
				new ToolInfo<BrushTool>(Resources.tl_brush, Resources.BrushTool_Name),
				new ToolInfo<PointSelectTool>(Resources.tl_remember, Resources.PointSelectTool_Name));
		}
		//adds a tool to toolbox
		private void AddToolInfo(params ToolInfo[] values)
		{
			foreach (ToolInfo value in values)
			{
				ToolBarButton btn = new ToolBarButton();
				btn.Command.Image = value.Image;
				btn.Command.Text = value.Name;
				btn.Command.Click += new EventHandler(Command_Click);
				//
				_toolbuttons.Add(btn.Command, value);
				_toolbox.ToolBarControls.Add(btn);
			}
		}
		//handle tool click
		void Command_Click(object sender, EventArgs e)
		{
			ButtonCommand cmd = sender as ButtonCommand;
			if (cmd == null)
				return;
			SetTool(_toolbuttons[cmd]);
		}
		//change current tool
		void SetTool(ToolInfo newinfo)
		{
			//load tool and update configbar
			if (newinfo == _currentinfo)
				return;
			Tool previoustool = _currenttool,
				newtool = null;
			if (newinfo != null)
				newtool = newinfo.Create();
			//
			_configbar.SuspendUpdates();
			if (_currenttool != null)
			{
				foreach (ConfigOption opt in _currenttool.ConfigOptions)
				{
					foreach (ToolBarControl ctl in opt.ConfigControls)
						if (_configbar.ToolBarControls.Contains(ctl))
							_configbar.ToolBarControls.Remove(ctl);
				}
				_currenttool.ActionCommitted -= new EventHandler(Tool_ActionCommitted);
				_icon.Command.Image = null;
				_icon.Command.Text = null;
			}
			_currenttool = newtool;
			_currentinfo = newinfo;
			if (newtool != null)
			{
				newtool.LoadConfig();
				foreach (ConfigOption opt in newtool.ConfigOptions)
					_configbar.ToolBarControls.AddRange(opt.ConfigControls.ToArray());
				newtool.ActionCommitted += new EventHandler(Tool_ActionCommitted);
				newtool.StatusChanged += new EventHandler<ToolStatusEventArgs>(Tool_StatusChanged);
				_icon.Command.Image = newinfo.Image;
				_icon.Command.Text = newinfo.Name;
			}
			if (ToolChanged != null)
				ToolChanged(this, new ToolChangedEventArgs(previoustool, _currenttool, _currentinfo));
			//highlight toolbox
			foreach (KeyValuePair<ButtonCommand, ToolInfo> entry in _toolbuttons)
				entry.Key.Checked = (_currentinfo == entry.Value);
			//update tool options
			_configbar.ResumeUpdates(true, true);
			//
			if (previoustool != null)
				previoustool.Dispose();
		}
		//cascade event
		void Tool_StatusChanged(object sender, ToolStatusEventArgs e)
		{
			if (ToolStatusChanged != null)
				ToolStatusChanged(this, e);
		}
		//cascade event
		void Tool_ActionCommitted(object sender, EventArgs e)
		{
			if (ActionCommitted != null)
				ActionCommitted(this, e);
		}
		//select the brush to edit of flip
		void BrushSwitcher_SwatchClicked(object sender, ToolBarColorSwitcherBase.SwatchClickedEventArgs e)
		{
			if (e.HitArea == ToolBarColorSwitcherBase.HitArea.SwatchA)
				_brushes.SelectBrush(true);
			else if (e.HitArea == ToolBarColorSwitcherBase.HitArea.SwatchB)
				_brushes.SelectBrush(false);
			else if (e.HitArea == ToolBarColorSwitcherBase.HitArea.Swap)
				_brushes.SwapBrushes();
			_switcher.SelectedArea = _brushes.SelectedA ?
				ToolBarColorSwitcherBase.HitArea.SwatchA :
				ToolBarColorSwitcherBase.HitArea.SwatchB;
		}
		//apply changed brushes to
		void _brushes_BrushChanged(object sender, EventArgs e)
		{
			_switcher.BrushA = _brushes.BrushA;
			_switcher.BrushB = _brushes.BrushB;
		}
		#endregion
		#region properties
		/// <summary>
		/// gets the toolbox
		/// </summary>
		public ToolBar ToolBox
		{
			get { return _toolbox; }
		}
		/// <summary>
		/// gets the tool config bar
		/// </summary>
		public ToolBar ConfigBar
		{
			get { return _configbar; }
		}
		/// <summary>
		/// gets the current selected tool
		/// </summary>
		public Tool CurrentTool
		{
			get { return _currenttool; }
		}
		public ToolInfo CurrentInfo
		{
			get { return _currentinfo; }
			set
			{
				if (value != _currentinfo &&
					_toolbuttons.ContainsValue(value))
					SetTool(value);
			}
		}
		public ToolInfo SelectionTool
		{
			get { return _selectiontool; }
		}
		#endregion
		#region events
		public event EventHandler ActionCommitted;
		public event EventHandler<ToolChangedEventArgs> ToolChanged;
		public event EventHandler<ToolStatusEventArgs> ToolStatusChanged;
		#endregion
	}
	/// <summary>
	/// event args for changing the tool layer
	/// </summary>
	public class ToolChangedEventArgs : EventArgs
	{
		private Tool _previous, _current;
		private ToolInfo _currentinfo;
		public ToolChangedEventArgs(Tool previous, Tool current, ToolInfo currentinfo)
		{
			_previous = previous; _current = current; _currentinfo = currentinfo;
		}
		public Tool Previous
		{
			get { return _previous; }
		}
		public Tool Current
		{
			get { return _current; }
		}
		public ToolInfo CurrentInfo
		{
			get { return _currentinfo; }
		}
	}
}
