// https://github.com/naudio/NAudio/blob/main/src/NAudio.Wasapi/CoreAudioApi/PropertyStore.cs
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
	public class PropertyStore
	{
		private readonly IPropertyStore _PropertyStore_;

		public int Count
		{
			get
			{
        Marshal.ThrowExceptionForHR(_PropertyStore_.GetCount(out int result));
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
            Marshal.ThrowExceptionForHR(_PropertyStore_.GetValue(ref propertyKey, out PropVariant propVariant));
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
      Marshal.ThrowExceptionForHR(_PropertyStore_.GetAt(P_0, out PropertyKey result));
      return result;
		}

		internal PropertyStore(IPropertyStore P_0)
		{
			_PropertyStore_ = P_0;
		}
	}
}