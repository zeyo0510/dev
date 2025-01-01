using System.Collections.Generic;
using Microsoft.Win32;

namespace MDC.RegViewer.Registry
{
	internal static class RegUtility
	{
		public static string GetRegValueName(string value)
		{
			if (!(value == string.Empty))
			{
				return value;
			}
			return "(Default)";
		}

		public static RegistryKey ParseRootKey(string path)
		{
			switch (path)
			{
			case "HKEY_CLASSES_ROOT":
				return Microsoft.Win32.Registry.ClassesRoot;
			case "HKEY_CURRENT_USER":
				return Microsoft.Win32.Registry.CurrentUser;
			case "HKEY_LOCAL_MACHINE":
				return Microsoft.Win32.Registry.LocalMachine;
			case "HKEY_USERS":
				return Microsoft.Win32.Registry.Users;
			default:
				return Microsoft.Win32.Registry.CurrentConfig;
			}
		}

		public static void SplitKey(string key, out string hive, out string branch)
		{
			int num = key.IndexOf('\\');
			hive = string.Empty;
			branch = string.Empty;
			if (num == -1)
			{
				hive = key;
				return;
			}
			hive = key.Substring(0, num);
			branch = key.Substring(num + 1);
		}

		public static bool DeleteKey(string key)
		{
			try
			{
				RegKey regKey = RegKey.Parse(key);
				RegKey regKey2 = RegKey.Parse(regKey.Parent, true);
				regKey2.Key.DeleteSubKeyTree(regKey.Name);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public static bool DeleteValue(string key, string value)
		{
			try
			{
				RegKey regKey = RegKey.Parse(key, true);
				regKey.Key.DeleteValue(value, false);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public static string GetNewKeyName(RegistryKey key)
		{
			List<RegKey> subKeys = RegExplorer.GetSubKeys(key);
			bool flag = false;
			int num = 0;
			string title = string.Empty;
			while (!flag)
			{
				num++;
				title = "New Key #" + num;
				flag = !subKeys.Exists((RegKey subKey) => subKey.Name == title);
			}
			return title;
		}

		public static string GetNewValueName(RegistryKey key)
		{
			List<RegValue> values = RegExplorer.GetValues(key);
			bool flag = false;
			int num = 0;
			string title = string.Empty;
			while (!flag)
			{
				num++;
				title = "New Value #" + num;
				flag = !values.Exists((RegValue val) => val.Name == title);
			}
			return title;
		}
	}
}
