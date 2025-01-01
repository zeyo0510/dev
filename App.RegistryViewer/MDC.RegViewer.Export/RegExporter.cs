using MDC.RegViewer.Registry;

namespace MDC.RegViewer.Export
{
	internal class RegExporter
	{
		public static void Export(RegKey key, ExportProvider provider)
		{
			provider.BeginExport();
			ExportKey(key, provider);
			provider.EndExport();
		}

		private static void ExportKey(RegKey key, ExportProvider provider)
		{
			provider.WriteKeyStart(key.Key.Name);
			ExportValues(key, provider);
			foreach (RegKey subKey in RegExplorer.GetSubKeys(key.Key))
			{
				ExportKey(subKey, provider);
			}
			provider.WriteKeyEnd();
		}

		private static void ExportValues(RegKey key, ExportProvider provider)
		{
			foreach (RegValue value in RegExplorer.GetValues(key.Key))
			{
				provider.WriteValue(value.Name, value.Kind, value.Data);
			}
		}
	}
}
