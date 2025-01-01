using System;
using System.Collections.Generic;

namespace MDC.Collections
{
	internal class EventDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		public new TValue this[TKey key]
		{
			get
			{
				return base[key];
			}
			set
			{
				bool flag = ContainsKey(key);
				base[key] = value;
				if (!flag && this.ItemAdded != null)
				{
					this.ItemAdded(this, new ItemEventArgs<TKey, TValue>(key, value));
				}
			}
		}

		public event EventHandler<ItemEventArgs<TKey, TValue>> ItemAdded;

		public event EventHandler<ItemEventArgs<TKey, TValue>> ItemRemoved;

		public new void Add(TKey key, TValue value)
		{
			base.Add(key, value);
			if (this.ItemAdded != null)
			{
				this.ItemAdded(this, new ItemEventArgs<TKey, TValue>(key, value));
			}
		}

		public new bool Remove(TKey key)
		{
			TValue value = base[key];
			bool flag = base.Remove(key);
			if (flag && this.ItemRemoved != null)
			{
				this.ItemRemoved(this, new ItemEventArgs<TKey, TValue>(key, value));
			}
			return flag;
		}
	}
}
