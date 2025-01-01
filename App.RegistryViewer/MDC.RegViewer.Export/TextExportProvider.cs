using System.Collections.Generic;
using System.IO;
using MDC.RegViewer.Registry;
using Microsoft.Win32;

namespace MDC.RegViewer.Export
{
	internal class TextExportProvider : ExportProvider
	{
		private Stack<int> counters;

		private int counter;

		public TextExportProvider(TextWriter writer)
			: base(writer)
		{
			counters = new Stack<int>();
			counter = 1;
		}

		public override void BeginExport()
		{
		}

		public override void WriteKeyStart(string key)
		{
			base.Writer.WriteLine("Key Name:\t{0}", key);
			counters.Push(counter);
			counter = 1;
		}

		public override void WriteKeyEnd()
		{
			counter = counters.Pop();
			base.Writer.WriteLine();
		}

		public override void WriteValue(string name, RegistryValueKind kind, object data)
		{
			base.Writer.WriteLine("Value {0}", counter++);
			base.Writer.WriteLine("    Name:\t{0}", name);
			base.Writer.WriteLine("    Type:\t{0}", kind.ToDataType());
			base.Writer.WriteLine("    Data:\t{0}", RegValue.ToString(kind, data));
			base.Writer.WriteLine();
		}

		public override void EndExport()
		{
			base.Writer.WriteLine();
		}
	}
}
