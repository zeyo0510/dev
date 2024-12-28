using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using DockingFrames;
using DrawingEx.IconEncoder;
using VariIconsReload.BrushModel;
using VariIconsReload.Components;

namespace VariIconsReload.Tools
{
	/// <summary>
	/// specifies a tool for the tool manager,
	/// which consists of:
	/// - one or more tool layers, reacting to user interaction on the layer view
	/// - zero or more config options, which can be chosen and combined freely
	/// - imagecontent, whcih is set by the editor when the tool is selected
	/// </summary>
	public abstract class Tool : IDisposable
	{
		#region variables
		private IconImage _content;
		private List<ConfigOption> _configOptions;
		private List<ToolLayer> _layers;
		#endregion
		#region ctor
		public Tool()
		{
			_configOptions = new List<ConfigOption>();
			_layers = new List<ToolLayer>();
		}
		public virtual void Dispose()
		{
			foreach (ToolLayer layer in _layers)
				layer.Dispose();
			_layers.Clear();
			foreach (ConfigOption opt in _configOptions)
				opt.Dispose();
			_configOptions.Clear();
		}
		#endregion
		#region helpers
		public static Point Floor(PointF value)
		{
			return new Point((int)Math.Floor(value.X), (int)Math.Floor(value.Y));
		}
		#endregion
		#region controller
		public virtual void LoadConfig()
		{
			foreach (ConfigOption opt in _configOptions)
				opt.LoadConfig();
		}
		protected virtual void OnContentChange(IconImage value)
		{
		}
		#endregion
		#region properties
		/// <summary>
		/// gets or sets the edited content
		/// </summary>
		public IconImage Content
		{
			get { return _content; }
			set
			{
				if (value == _content)
					return;
				OnContentChange(value);
				_content = value;
			}
		}
		/// <summary>
		/// gets the list of config controls of this
		/// </summary>
		public List<ConfigOption> ConfigOptions
		{
			get { return _configOptions; }
		}
		/// <summary>
		/// gets the list of layers
		/// </summary>
		public List<ToolLayer> Layers
		{
			get { return _layers; }
		}
		#endregion
		#region events
		/// <summary>
		/// raise this to save image changes
		/// </summary>
		protected void RaiseActionCommitted()
		{
			if (ActionCommitted != null)
				ActionCommitted(this, EventArgs.Empty);
		}
		public event EventHandler ActionCommitted;
		/// <summary>
		/// raise this to set a mark on rulers and statusbar
		/// </summary>
		protected void RaiseToolStatusChanged(RectangleF mark)
		{
			if (StatusChanged != null)
				StatusChanged(this, new ToolStatusEventArgs(mark));
		}
		protected void RaiseToolStatusChanged()
		{
			RaiseToolStatusChanged(LayerView.InvalidMark);
		}
		public event EventHandler<ToolStatusEventArgs> StatusChanged;
		#endregion
	}
	/// <summary>
	/// toolinfo for keeping image and description
	/// and to create a tool
	/// </summary>
	public abstract class ToolInfo
	{
		#region variables
		private String _description, _name;
		private Image _image;
		#endregion
		public ToolInfo(Image image, String name, String description)
		{
			this._image = image;
			this._name = name;
			this._description = description;
		}
		/// <summary>
		/// creates the tool
		/// </summary>
		public abstract Tool Create();
		/// <summary>
		/// gets or sets the status description
		/// </summary>
		public String Description
		{
			get { return _description; }
		}
		/// <summary>
		/// gets or sets the status description
		/// </summary>
		public String Name
		{
			get { return _name; }
		}
		public Image Image
		{
			get { return _image; }
		}
	}
	/// <summary>
	/// generic implementation of the tool info
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ToolInfo<T> : ToolInfo
		where T : Tool, new()
	{
		public ToolInfo(Image image, String name)
			: base(image, name, null) { }
		public ToolInfo(Image image, String name, String description)
			: base(image, name, description) { }
		public override Tool Create()
		{
			return new T();
		}
	}
	/// <summary>
	/// tool layer, adding a refresh support
	/// </summary>
	public abstract class ToolLayer : MouseKeyboardLayer, IDisposable
	{
		public virtual void Dispose() { }
	}
	/// <summary>
	/// used for updating statusbar and rulers
	/// </summary>
	public class ToolStatusEventArgs : EventArgs
	{
		private RectangleF _mark;
		public ToolStatusEventArgs(RectangleF mark)
		{
			_mark = mark;
		}
		public RectangleF Mark
		{
			get { return _mark; }
		}
	}
}
