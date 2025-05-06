using System;
using System.Collections.Generic;
/************************************************/
namespace AudioCore
{
  partial class MyAudioManager
  {
    public AudioDevice[] AudioDeviceRenderItems
    {
      get
      {
        List<AudioDevice> retValue = new List<AudioDevice>();
        /************************************************/
        AudioDeviceCollection collection = this.EnumAudioEndpoints(EDataFlow.eRender, EDeviceState.Active);
        /************************************************/
        for (int i = 0; i < collection.Count; i++)
        {
          retValue.Add(collection[i]);
        }
        /************************************************/
        return retValue.ToArray();
      }
    }
  }
}