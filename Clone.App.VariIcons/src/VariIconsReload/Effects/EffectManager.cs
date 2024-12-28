using System;
using System.Collections.Generic;
using DockingFrames;
using DockingFrames.CommandManager;
using DrawingEx.IconEncoder;
using VariIconsReload.Properties;
using VariIconsSDK.Model;
using VariIconsReload.Components;

namespace VariIconsReload.Effects
{
	/// <summary>
	/// effect manager for selecting and executing effects
	/// </summary>
	public class EffectManager : Manager<EffectManager>, IDisposable
	{
		#region variables
		private ToolBar _effectsbar;
		private Dictionary<ButtonCommand, Effect> _effectbuttons;
		//
		private IconImage _content;
		private LayerView _view;
		#endregion
		#region ctor
		public EffectManager()
		{
			_effectsbar = new ToolBar();
			_effectsbar.Guid = "3C3B8007-FEE8-46A6-A0E7-24AC405CAF33";
			_effectsbar.Text = Resources.EffectManager_EffectBar_Text;
			//
			_effectbuttons = new Dictionary<ButtonCommand, Effect>();
			//add single effects
			AddEffect(new MoveLeftEffect(), new MoveRightEffect(),
				new MoveUpEffect(), new MoveDownEffect());
			_effectsbar.ToolBarControls.Add(new ToolBarSeparator());
			AddEffect(new RotateCCWEffect(), new RotateCWEffect(),
				new FlipHEffect(), new FlipVEffect(),
				new RotateFreeEffect());
			_effectsbar.ToolBarControls.Add(new ToolBarSeparator());
			AddEffect(new BrightnessEffect(), new DesaturateEffect(),
				new ColorEffect());//, new ShadowEffect());
		}
		public void Dispose()
		{
			if (_effectsbar != null)
			{
				_effectsbar.Dispose();
				_effectsbar = null;
			}
		}
		#endregion
		#region controller
		//adds an effect to the bar
		private void AddEffect(params Effect[] values)
		{
			foreach (Effect value in values)
			{
				ToolBarButton btn = new ToolBarButton();
				btn.Command.Image = value.Image;
				btn.Command.Text = value.Name;
				btn.Command.Click += new EventHandler(Command_Click);
				//
				_effectbuttons.Add(btn.Command, value);
				_effectsbar.ToolBarControls.Add(btn);
			}
		}
		//effect selected
		void Command_Click(object sender, EventArgs e)
		{
			if (_content == null)
				return;
			ButtonCommand cmd = sender as ButtonCommand;
			if (cmd == null)
				return;
			if (BeforeRender != null)
				BeforeRender(this, EventArgs.Empty);
			_effectbuttons[cmd].Render(_content,
				new EventHandler<RenderProgressArgs>(Effect_Progress));
		}
		//handle effect progress
		void Effect_Progress(object sender, RenderProgressArgs e)
		{
			if (e.CommitFlag)
			{
				if (ActionCommitted != null)
					ActionCommitted(this, EventArgs.Empty);
			}
			else
			{
				if (_view != null)
					_view.Refresh();
			}
		}
		#endregion
		/// <summary>
		/// gets the effects bar
		/// </summary>
		public ToolBar EffectsBar
		{
			get { return _effectsbar; }
		}
		/// <summary>
		/// gets or sets the content to be worked on
		/// </summary>
		public IconImage Content
		{
			get { return _content; }
			set { _content = value; }
		}
		/// <summary>
		/// gets or sets the layer view
		/// </summary>
		public LayerView LayerView
		{
			get { return _view; }
			set { _view = value; }
		}
		public event EventHandler ActionCommitted;
		public event EventHandler BeforeRender;
	}
}
