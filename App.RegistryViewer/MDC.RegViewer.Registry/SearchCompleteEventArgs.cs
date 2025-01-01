using System;
using System.Collections.Generic;

namespace MDC.RegViewer.Registry
{
	internal class SearchCompleteEventArgs : EventArgs
	{
		public List<RegSearchMatch> Matches { get; private set; }

		public SearchCompleteEventArgs(List<RegSearchMatch> matches)
		{
			Matches = matches;
		}
	}
}
