using System;
using System.Text;
using Microsoft.Win32;

namespace MDC.RegViewer.Registry
{
	internal class RegValue : IComparable<RegValue>
	{
		private string name;

		public string Name
		{
			get
			{
				if (IsDefault)
				{
					return "(Default)";
				}
				return name;
			}
			set
			{
				name = value;
			}
		}

		public RegistryValueKind Kind { get; set; }

		public object Data { get; set; }

		public bool IsDefault
		{
			get
			{
				return name == string.Empty;
			}
		}

		public RegistryKey ParentKey { get; private set; }

		public RegValue(string name, RegistryValueKind kind, object data)
		{
			this.name = name;
			Kind = kind;
			Data = data;
		}

		public RegValue(RegistryKey parentKey, string valueName)
			: this(valueName, parentKey.GetValueKind(valueName), parentKey.GetValue(valueName))
		{
			ParentKey = parentKey;
		}

		public override string ToString()
		{
			return ToString(Kind, Data);
		}

		public static string ToString(object valueData)
		{
			if (valueData is byte[])
			{
				return Encoding.ASCII.GetString((byte[])valueData);
			}
			return valueData.ToString();
		}

		public static string ToString(RegistryValueKind valueKind, object valueData)
		{
			switch (valueKind)
			{
			case RegistryValueKind.Binary:
				return Encoding.ASCII.GetString((byte[])valueData);
			case RegistryValueKind.MultiString:
				return string.Join(" ", (string[])valueData);
			case RegistryValueKind.DWord:
				return ((uint)(int)valueData).ToString();
			case RegistryValueKind.QWord:
				return ((ulong)(long)valueData).ToString();
			case RegistryValueKind.String:
			case RegistryValueKind.ExpandString:
				return valueData.ToString();
			default:
				return string.Empty;
			}
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		public int CompareTo(RegValue other)
		{
			return Name.CompareTo(other.Name);
		}
	}
}
