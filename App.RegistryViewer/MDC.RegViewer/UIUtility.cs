using System.Windows.Forms;

namespace MDC.RegViewer
{
	internal static class UIUtility
	{
		public static void InformUser(IWin32Window owner, string message)
		{
			MessageBox.Show(owner, message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static void DisplayError(IWin32Window owner, string message)
		{
			DisplayError(owner, message, null);
		}

		public static void DisplayError(IWin32Window owner, string message, Control source)
		{
			MessageBox.Show(owner, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			if (source != null)
			{
				if (source is TextBox)
				{
					((TextBox)source).SelectAll();
				}
				source.Focus();
			}
		}

		public static void WarnUser(IWin32Window owner, string message)
		{
			MessageBox.Show(owner, message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public static bool ConfirmAction(IWin32Window owner, string message, string action, bool critical)
		{
			MessageBoxIcon icon = (critical ? MessageBoxIcon.Exclamation : MessageBoxIcon.Question);
			return MessageBox.Show(owner, message, "Confirm " + action, MessageBoxButtons.YesNo, icon, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
		}
	}
}
