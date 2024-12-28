using System;
using ControlsEx.DockingFrames;

namespace VariIconsSDK.UI
{
	public partial class MainTab : TabPage
	{
		private MainForm _frm;
		public MainTab()
		{
			InitializeComponent();
		}
		protected override void OnSelected()
		{
			TabControl owner;
			if (this.Visible &&
				(owner = this.Parent as TabControl) != null &&
				(_frm = owner.Parent as MainForm) != null)
				AddGuiExtensions(_frm);
		}
		protected override void OnDeselected()
		{
			if (_frm != null)
			{
				RemoveGuiExtensions();
				_frm = null;
			}
		}
		/// <summary>
		/// override this to add extensions to the given gui
		/// </summary>
		protected virtual void AddGuiExtensions(MainForm frm)
		{
		}
		/// <summary>
		/// override this to remove installes gui extensions
		/// </summary>
		protected virtual void RemoveGuiExtensions()
		{
		}
		/// <summary>
		/// gets the mainform this editor is loaded in.
		/// can be NULL
		/// </summary>
		protected MainForm MainForm
		{
			get { return _frm; }
		}
	}
}
