using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  public class PropertyStore
  {
    private readonly IPropertyStore _PropertyStore_;
    /************************************************/
    internal PropertyStore(IPropertyStore obj)
    {
      this._PropertyStore_ = obj;
    }
    /************************************************/
    public PropertyStoreProperty this[PropertyKey P_0]
    {
      get
      {
        for (int i = 0; i < this.Count; i++)
        {
          PropertyKey propertyKey = this.Get(i);
          /************************************************/
          if (propertyKey.fmtid == P_0.fmtid && propertyKey.pid == P_0.pid)
          {
            PropVariant propVariant;
            /************************************************/
            Marshal.ThrowExceptionForHR(_PropertyStore_.GetValue(ref propertyKey, out propVariant));
            /************************************************/
            return new PropertyStoreProperty(propVariant);
          }
        }
        /************************************************/
        return null;
      }
    }
    /************************************************/
    public int Count
    {
      get
      {
        int retValue = 0;
        /************************************************/
        Marshal.ThrowExceptionForHR(this._PropertyStore_.GetCount(out retValue));
        /************************************************/
        return retValue;
      }
    }
    /************************************************/
    public bool Contains(PropertyKey P_0)
    {
      for (int i = 0; i < this.Count; i++)
      {
        PropertyKey propertyKey = this.Get(i);
        /************************************************/
        if (propertyKey.fmtid == P_0.fmtid && propertyKey.pid == P_0.pid)
        {
          return true;
        }
      }
      /************************************************/
      return false;
    }
    /************************************************/
    public PropertyKey Get(int index)
    {
      PropertyKey retValue;
      /************************************************/
      Marshal.ThrowExceptionForHR(_PropertyStore_.GetAt(index, out retValue));
      /************************************************/
      return retValue;
    }
  }
}