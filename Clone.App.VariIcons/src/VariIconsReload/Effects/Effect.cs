using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using DrawingEx.IconEncoder;

namespace VariIconsReload.Effects
{
	public abstract class Effect
	{
		#region variables
		private String _description, _name;
		private Image _image;
		#endregion
		#region controller
		public abstract void Render(IconImage target, EventHandler<RenderProgressArgs> callback);
		#endregion
		#region properties
		/// <summary>
		/// gets or sets the status description
		/// </summary>
		public String Description
		{
			get { return _description; }
			set { _description = value; }
		}
		/// <summary>
		/// gets or sets the status description
		/// </summary>
		public String Name
		{
			get { return _name; }
			set { _name = value; }
		}
		public Image Image
		{
			get { return _image; }
			set { _image = value; }
		}
		#endregion
	}
	/// <summary>
	/// event args for a rendering progress
	/// </summary>
	public class RenderProgressArgs : EventArgs
	{
		public static readonly RenderProgressArgs Commit = new RenderProgressArgs(true);
		public static readonly RenderProgressArgs Refresh = new RenderProgressArgs(false);
		//
		private bool _commitflag;
		public RenderProgressArgs(bool commit)
		{
			_commitflag = commit;
		}
		public bool CommitFlag
		{
			get { return _commitflag; }
		}
	}
}
