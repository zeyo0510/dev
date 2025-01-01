using System.Diagnostics;
using Microsoft.Win32;

namespace MDC.Utility
{
	internal static class ShellUtility
	{
		private static string GetDefaultBrowser()
		{
			string name = "htmlfile\\shell\\open\\command";
			RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name);
			string text = registryKey.GetValue(string.Empty).ToString();
			return text.Split('"')[1];
		}

		public static void OpenWebPage(string url)
		{
			Process.Start(GetDefaultBrowser(), url);
		}
	}
}
