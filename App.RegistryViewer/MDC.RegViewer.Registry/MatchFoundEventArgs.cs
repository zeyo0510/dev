using System;

namespace MDC.RegViewer.Registry
{
	internal class MatchFoundEventArgs : EventArgs
	{
		public RegSearchMatch Match { get; private set; }

		public MatchFoundEventArgs(RegSearchMatch match)
		{
			Match = match;
		}
	}
}
