using ControlsEx.DockingFrames;
using VariIconsSDK.UI;

namespace VariIconsSDK.Pages
{
	public partial class StartPage : MainTab
	{
		private static StartPage __instance;
		private StartPage()
		{
			InitializeComponent();
		}
		private void UserDispose()
		{
			__instance = null;
		}
		public static StartPage Instance
		{
			get
			{
				if (__instance == null)
					__instance = new StartPage();
				return __instance;
			}
		}

		private void tbnNew_Click(object sender, System.EventArgs e)
		{
			if (MainForm == null)
				return;
			MainForm.ToolBarStandart.New();
		}

		private void btnOpen_Click(object sender, System.EventArgs e)
		{
			if (MainForm == null)
				return;
			MainForm.ToolBarStandart.Open();
		}

	}
}
