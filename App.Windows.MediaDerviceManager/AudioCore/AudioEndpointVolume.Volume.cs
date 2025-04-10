using System;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  partial class AudioEndpointVolume
  {
    public int Volume
    {
      get
      {
        int retValue = 0;
        /************************************************/
        retValue = (int)Math.Ceiling(this.MasterVolumeLevelScalar * 100f);
        /************************************************/
        return retValue;
      }
      set
      {
        this.MasterVolumeLevelScalar = (float)value / 100f;
      }
    }
  }
}