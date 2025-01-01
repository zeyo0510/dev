using System;
using System.Windows.Forms;

namespace MDC.RegViewer
{
	internal class BusyCursor : IDisposable
	{
		private Form source;

		public BusyCursor(Form source)
		{
			this.source = source;
			source.Cursor = Cursors.WaitCursor;
		}

		public void Dispose()
		{
			source.Cursor = Cursors.Default;
		}
	}
}
