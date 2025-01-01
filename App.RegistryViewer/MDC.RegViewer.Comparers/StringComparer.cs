namespace MDC.RegViewer.Comparers
{
	internal class StringComparer : Comparer
	{
		private string patternLower;

		public StringComparer(string pattern, bool ignoreCase)
			: base(pattern, ignoreCase)
		{
			if (ignoreCase)
			{
				patternLower = pattern.ToLower();
			}
		}

		public override bool IsMatch(string value)
		{
			if (base.IgnoreCase)
			{
				return value.ToLower().Contains(patternLower);
			}
			return value.Contains(base.Pattern);
		}
	}
}
