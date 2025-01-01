using System.Collections.Generic;
using Microsoft.Win32;

namespace MDC.RegViewer.Registry
{
	internal static class RegExplorer
	{
		public const string RegistryFavoritePath = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Applets\\Regedit\\Favorites";

		public static List<RegKey> GetSubKeys(RegistryKey key)
		{
			int subKeyCount = key.SubKeyCount;
			if (subKeyCount == 0)
			{
				return new List<RegKey>();
			}
			List<RegKey> list = new List<RegKey>(subKeyCount);
			string[] subKeyNames = key.GetSubKeyNames();
			for (int i = 0; i < subKeyNames.Length; i++)
			{
				try
				{
					string name = subKeyNames[i];
					RegKey item = new RegKey(name, key.OpenSubKey(name));
					list.Add(item);
				}
				catch
				{
				}
			}
			return list;
		}

		public static List<RegValue> GetValues(RegistryKey key)
		{
			int valueCount = key.ValueCount;
			if (valueCount == 0)
			{
				return new List<RegValue>();
			}
			List<RegValue> list = new List<RegValue>(valueCount);
			string[] valueNames = key.GetValueNames();
			for (int i = 0; i < valueNames.Length; i++)
			{
				list.Add(new RegValue(key, valueNames[i]));
			}
			return list;
		}
	}
}
