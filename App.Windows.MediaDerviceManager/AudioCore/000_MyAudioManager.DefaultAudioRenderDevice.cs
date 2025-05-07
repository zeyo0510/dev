using System;
/************************************************/
namespace AudioCore
{
  partial class MyAudioManager
  {
    public AudioDevice DefaultAudioRenderDevice
    {
      get
      {
        AudioDevice retValue = this.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
        /************************************************/
        return retValue;
      }
    }
  }
}