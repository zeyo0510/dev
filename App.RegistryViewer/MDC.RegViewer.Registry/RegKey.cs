using System;
using System.Linq;
using Microsoft.Win32;

namespace MDC.RegViewer.Registry
{
	internal class RegKey : IComparable<RegKey>
	{
		public string Name { get; private set; }

		public RegistryKey Key { get; private set; }

		public string Parent { get; private set; }

		public RegKey(string name, RegistryKey key)
		{
			Name = name;
			Key = key;
			int num = key.Name.Length - name.Length - 1;
			if (num > 0)
			{
				Parent = key.Name.Substring(0, num);
			}
		}

		public RegKey(RegistryKey key)
			: this(key.Name.Contains('\\') ? key.Name.Substring(key.Name.LastIndexOf('\\')) : key.Name, key)
		{
		}

		public static RegKey Parse(string keyPath)
		{
			return Parse(keyPath, false);
		}

		public static RegKey Parse(string keyPath, bool writable)
		{
			string[] array = keyPath.Split(new char[1] { '\\' }, 2);
			RegistryKey registryKey = RegUtility.ParseRootKey(array[0]);
			if (array.Length == 1)
			{
				return new RegKey(registryKey);
			}
			string name = array[1];
			string name2 = keyPath.Substring(keyPath.LastIndexOf('\\') + 1);
			try
			{
				RegistryKey registryKey2 = registryKey.OpenSubKey(name, writable);
				if (registryKey2 == null)
				{
					return null;
				}
				return new RegKey(name2, registryKey2);
			}
			catch
			{
				return null;
			}
		}

		public int CompareTo(RegKey other)
		{
			return Name.CompareTo(other.Name);
		}
	}
}
