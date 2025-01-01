using System;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace MDC.RegViewer.Export
{
	internal class NativeExportProvider : ExportProvider
	{
		public NativeExportProvider(TextWriter writer)
			: base(writer)
		{
		}

		public override void BeginExport()
		{
			base.Writer.WriteLine("Windows Registry Editor Version 5.00");
		}

		public override void WriteKeyStart(string key)
		{
			base.Writer.WriteLine();
			base.Writer.WriteLine(string.Format("[{0}]", key));
		}

		public override void WriteKeyEnd()
		{
		}

		public override void WriteValue(string name, RegistryValueKind kind, object data)
		{
			string arg;
			switch (kind)
			{
			case RegistryValueKind.Binary:
				arg = string.Format("hex:{0}", GetHexString((byte[])data));
				break;
			case RegistryValueKind.DWord:
				arg = string.Format("dword:{0:x8}", (uint)(int)data);
				break;
			case RegistryValueKind.QWord:
				arg = string.Format("qword:{0:x16}", (ulong)(long)data);
				break;
			case RegistryValueKind.ExpandString:
				arg = string.Format("hex(2):{0}", GetHexString((string)data));
				break;
			case RegistryValueKind.MultiString:
				arg = string.Format("hex(7):{0}", GetHexString((string[])data));
				break;
			case RegistryValueKind.String:
				arg = string.Format("\"{0}\"", (string)data);
				break;
			default:
				arg = string.Empty;
				break;
			}
			base.Writer.WriteLine("\"{0}\"={1}", name, arg);
		}

		private string GetHexString(string[] data)
		{
			if (data.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder output = new StringBuilder(data.Length * 10);
			Array.ForEach(data, delegate(string str)
			{
				output.Append(GetHexString(str)).Append(',');
			});
			output.Append("00,00");
			return output.ToString();
		}

		private string GetHexString(string data)
		{
			if (data.Length == 0)
			{
				return string.Empty;
			}
			int capacity = data.Length * 4 + data.Length * 2 + 5 + 1;
			StringBuilder output = new StringBuilder(capacity);
			Array.ForEach(data.ToCharArray(), delegate(char chr)
			{
				output.Append(string.Format("{0:x2},{1:x2},", (byte)chr, (byte)chr >> 8));
			});
			output.Append("00,00");
			return output.ToString(0, output.Length);
		}

		private string GetHexString(byte[] data)
		{
			if (data.Length == 0)
			{
				return string.Empty;
			}
			int capacity = data.Length * 2 + data.Length + 1;
			StringBuilder output = new StringBuilder(capacity);
			Array.ForEach(data, delegate(byte byt)
			{
				output.Append(string.Format("{0:x2},", byt));
			});
			return output.ToString(0, output.Length - 1);
		}

		public override void EndExport()
		{
			base.Writer.WriteLine();
		}
	}
}
