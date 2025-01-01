using System.IO;
using Microsoft.Win32;

namespace MDC.RegViewer.Export
{
	internal abstract class ExportProvider
	{
		protected TextWriter Writer { get; private set; }

		protected ExportProvider(TextWriter writer)
		{
			Writer = writer;
		}

		public abstract void BeginExport();

		public abstract void WriteKeyStart(string key);

		public abstract void WriteKeyEnd();

		public abstract void WriteValue(string name, RegistryValueKind kind, object data);

		public abstract void EndExport();

		public static ExportProvider Create(RegExportFormat format, StreamWriter writer)
		{
			ExportProvider exportProvider = null;
			switch (format)
			{
			case RegExportFormat.NativeRegFormat:
				return new NativeExportProvider(writer);
			case RegExportFormat.XmlFormat:
				return new XmlExportProvider(writer);
			default:
				return new TextExportProvider(writer);
			}
		}
	}
}
