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

    internal PropertyStore(IPropertyStore stroe)
    {
      _PropertyStore_ = stroe;
    }

    public PropertyStoreProperty this[PropertyKey key]
    {
      get
      {
        for (int i = 0; i < Count; i++)
        {
          PropertyKey propertyKey = Get(i);
          if (propertyKey.fmtid == key.fmtid && propertyKey.pid == key.pid)
          {
            Marshal.ThrowExceptionForHR(_PropertyStore_.GetValue(ref propertyKey, out PropVariant propVariant));
            /************************************************/
            return new PropertyStoreProperty(propVariant);
          }
        }
        return null;
      }
    }

    public int Count
    {
      get
      {
        Marshal.ThrowExceptionForHR(_PropertyStore_.GetCount(out int retValue));
        /************************************************/
        return retValue;
      }
    }

    public bool Contains(PropertyKey key)
    {
      for (int i = 0; i < Count; i++)
      {
        PropertyKey propertyKey = Get(i);
        /************************************************/
        if (propertyKey.fmtid == key.fmtid && propertyKey.pid == key.pid)
        {
          return true;
        }
      }
      /************************************************/
      return false;
    }

    public PropertyKey Get(int index)
    {
      Marshal.ThrowExceptionForHR(_PropertyStore_.GetAt(index, out PropertyKey retValue));
      /************************************************/
      return retValue;
    }
  }
}