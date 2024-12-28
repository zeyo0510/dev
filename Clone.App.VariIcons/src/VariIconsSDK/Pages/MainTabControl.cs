using System.Drawing;
using System.Windows.Forms;
using ControlsEx.DockingFrames;
using DockingFrames;

namespace VariIconsSDK.UI
{
	/// <summary>
	/// modified editor control to blend into the design
	/// </summary>
	public class MainTabControl : ControlsEx.DockingFrames.TabControl
	{
		private class MainHeaderModule : VS2005HeaderModule
		{
			private MainTabControl _owner;
			public MainHeaderModule(MainTabControl owner):base(owner)
			{
				_owner = owner;
			}
			protected override void OnPaint(PaintEventArgs pevent)
			{
				//paint using renderer
				Form frm = _owner.FindForm();
				if (frm == null) return;
				DockManager.DefaultRenderer.DrawDockingAreaBackground(
					pevent,
					_owner.RectangleToScreen(new Rectangle(
						0,0,
						_owner.Width,HeaderHeight)),
					frm.RectangleToScreen(frm.ClientRectangle));
				base.OnPaint(pevent);
			}
		}
		protected override HeaderModule CreateHeaderModule()
		{
			return new MainHeaderModule(this);
		}
	}
}
