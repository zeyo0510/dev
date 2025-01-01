namespace MDC.RegViewer.Registry
{
	internal class RegSearchMatch
	{
		public string Key { get; private set; }

		public string Value { get; private set; }

		public string Data { get; private set; }

		public RegSearchMatch(string key, string value, string data)
		{
			Key = key;
			Value = value;
			Data = data;
		}

		public override string ToString()
		{
			return string.Format("({0}:{1}:{2})", Key, Value, Data);
		}
	}
}
