using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VariIconsSDK.UI;
using VariIconsSDK;
using System.Globalization;

namespace VariIcons
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using (Splash spl = new Splash())
			{
				try
				{
					CultureInfo cult=(CultureInfo)Application.CurrentCulture.Clone();
					cult.NumberFormat.NaNSymbol = "-";
					Application.CurrentCulture = cult;
				}
				catch  { }
				spl.Show();
				spl.Refresh();
				MainForm frm = MainForm.Instance;
				PluginFinder.Instance.LoadPlugin(new VariIconsReload.IconEditorPlugin());
			}
			Application.Run(MainForm.Instance);
		}
	}
}
