using Microsoft.Win32;

namespace MDC.RegViewer.Registry
{
	internal class RegSearchArgs
	{
		private RegSearchLookAt lookAt;

		private bool lookAtKeys;

		private bool lookAtValues;

		private bool lookAtData;

		private bool lookAtValuesOrData;

		public bool LookAtKeys
		{
			get
			{
				return lookAtKeys;
			}
		}

		public bool LookAtValues
		{
			get
			{
				return lookAtValues;
			}
		}

		public bool LookAtData
		{
			get
			{
				return lookAtData;
			}
		}

		public bool LookAtValuesOrData
		{
			get
			{
				return lookAtValuesOrData;
			}
		}

		public bool MatchCase { get; set; }

		public RegSearchLookAt LookAt
		{
			get
			{
				return lookAt;
			}
			set
			{
				lookAt = value;
				lookAtKeys = (lookAt & RegSearchLookAt.Keys) == RegSearchLookAt.Keys;
				lookAtValues = (lookAt & RegSearchLookAt.Values) == RegSearchLookAt.Values;
				lookAtData = (lookAt & RegSearchLookAt.Data) == RegSearchLookAt.Data;
				lookAtValuesOrData = lookAtValues || lookAtData;
			}
		}

		public RegistryKey[] RootKeys { get; set; }

		public string Pattern { get; set; }

		public bool UseRegEx { get; set; }

		public RegSearchArgs(RegistryKey[] regKeys, string pattern, bool matchCase, RegSearchLookAt lookAt, bool useRegEx)
		{
			RootKeys = regKeys;
			Pattern = pattern;
			MatchCase = matchCase;
			LookAt = lookAt;
			UseRegEx = useRegEx;
		}
	}
}
