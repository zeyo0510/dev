using System;
/************************************************/
namespace AudioCore
{
  public partial class MyAudioManager
  {
    public MyAudioManager()
    {
      Type type = Type.GetTypeFromCLSID(Guids.IID_MMDeviceEnumerator, true);
      /************************************************/
      this._MMDeviceEnumerator_ = (IMMDeviceEnumerator)Activator.CreateInstance(type);
    }
    /************************************************/
    ~MyAudioManager()
    {
      
    }
  }
}