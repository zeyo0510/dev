using System;
using AudioCore.Interfaces;

namespace AudioCore2
{
  public class AudioPolicyConfigFactory
  {
    public static IAudioPolicyConfigFactory Create()
    {
      Guid gUID = typeof(IAudioPolicyConfigFactory).GUID;
      object obj;
      Combase._RoGetActivationFactory("Windows.Media.Internal.AudioPolicyConfig", ref gUID, out obj);
      return (IAudioPolicyConfigFactory)obj;
    }
  }
}