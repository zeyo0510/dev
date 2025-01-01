using System;
using System.Collections.Generic;

namespace MDC.Collections
{
	internal class ItemEventArgs<TKey, TValue> : EventArgs
	{
		public KeyValuePair<TKey, TValue> Item { get; set; }

		public ItemEventArgs(TKey key, TValue value)
		{
			Item = new KeyValuePair<TKey, TValue>(key, value);
		}
	}
}
