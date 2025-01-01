using System;
using System.Text.RegularExpressions;

namespace MDC.RegViewer.Comparers
{
	internal class RegexComparer : Comparer
	{
		private Regex regEx;

		public RegexComparer(string pattern, bool ignoreCase)
			: base(pattern, ignoreCase)
		{
			RegexOptions regexOptions = RegexOptions.Compiled;
			if (base.IgnoreCase)
			{
				regexOptions |= RegexOptions.IgnoreCase;
			}
			try
			{
				regEx = new Regex(base.Pattern, regexOptions);
			}
			catch (ArgumentException innerException)
			{
				throw new ArgumentException("Invalid regular expression", innerException);
			}
		}

		public override bool IsMatch(string value)
		{
			return regEx.IsMatch(value);
		}
	}
}
