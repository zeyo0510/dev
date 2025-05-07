using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
	public class PropertyStore
	{
		private readonly IPropertyStore _PropertyStore_;

		public int Count
		{
			get
			{
				int result;
				Marshal.ThrowExceptionForHR(_PropertyStore_.GetCount(out result));
				return result;
			}
		}

		public PropertyStoreProperty this[PropertyKey P_0]
		{
			get
			{
				for (int i = 0; i < Count; i++)
				{
					PropertyKey propertyKey = Get(i);
					if (propertyKey.fmtid == P_0.fmtid && propertyKey.pid == P_0.pid)
					{
						PropVariant propVariant;
						Marshal.ThrowExceptionForHR(_PropertyStore_.GetValue(ref propertyKey, out propVariant));
						return new PropertyStoreProperty(propVariant);
					}
				}
				return null;
			}
		}

		public bool Contains(PropertyKey P_0)
		{
			for (int i = 0; i < Count; i++)
			{
				PropertyKey propertyKey = Get(i);
				if (propertyKey.fmtid == P_0.fmtid && propertyKey.pid == P_0.pid)
				{
					return true;
				}
			}
			return false;
		}

		public PropertyKey Get(int P_0)
		{
			PropertyKey result;
			Marshal.ThrowExceptionForHR(_PropertyStore_.GetAt(P_0, out result));
			return result;
		}

		internal PropertyStore(IPropertyStore P_0)
		{
			_PropertyStore_ = P_0;
		}
	}
}
