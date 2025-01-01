using System.IO;
using System.Xml;
using MDC.RegViewer.Registry;
using Microsoft.Win32;

namespace MDC.RegViewer.Export
{
	internal class XmlExportProvider : ExportProvider
	{
		private XmlTextWriter xmlWriter;

		private bool firstKey;

		public XmlExportProvider(TextWriter writer)
			: base(writer)
		{
			xmlWriter = new XmlTextWriter(writer);
			xmlWriter.Formatting = Formatting.Indented;
			firstKey = true;
		}

		public override void BeginExport()
		{
			xmlWriter.WriteStartDocument();
		}

		public override void WriteKeyStart(string key)
		{
			if (firstKey)
			{
				xmlWriter.WriteStartElement("registry");
				xmlWriter.WriteAttributeString("branch", key);
				firstKey = false;
			}
			else
			{
				xmlWriter.WriteStartElement("key");
				xmlWriter.WriteAttributeString("name", key.Substring(key.LastIndexOf('\\') + 1));
			}
		}

		public override void WriteKeyEnd()
		{
			xmlWriter.WriteEndElement();
		}

		public override void WriteValue(string name, RegistryValueKind kind, object data)
		{
			xmlWriter.WriteStartElement("value");
			xmlWriter.WriteAttributeString("name", name);
			xmlWriter.WriteAttributeString("type", kind.ToDataType());
			xmlWriter.WriteAttributeString("data", RegValue.ToString(kind, data));
			xmlWriter.WriteEndElement();
		}

		public override void EndExport()
		{
			xmlWriter.WriteEndDocument();
		}
	}
}
