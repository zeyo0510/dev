using System;
using System.Collections.Generic;
using AudioCore;

namespace a
{
  internal static class AudioManager
  {
    internal static readonly MyAudioManager _MMDeviceEnumerator_ = null;
    
    
    internal static readonly List<string> list_a;

    internal static int num_a;

    private static readonly Dictionary<int, string> dictionary_a;

    private static readonly PolicyConfigClient policyConfigClient_a;

    static AudioManager()
    {
      list_a = new List<string>();
      dictionary_a = new Dictionary<int, string>();
      policyConfigClient_a = new PolicyConfigClient();
      _MMDeviceEnumerator_ = new MyAudioManager();
    }

//    internal static void Method1(EDataFlow P_0)
//    {
//      list_a.Clear();
//      dictionary_a.Clear();
//      AudioDeviceCollection defaultAudioEndpoint = _MMDeviceEnumerator_.EnumAudioEndpoints(P_0, EDeviceState.Active);
//      string iD = _MMDeviceEnumerator_.GetDefaultAudioEndpoint(P_0, ERole.eMultimedia).ID;
//      int count = defaultAudioEndpoint.Count;
//      for (int i = 0; i < count; i++)
//      {
//        AudioDevice mMDevice = defaultAudioEndpoint[i];
//        string iD2 = mMDevice.ID;
//        list_a.Add(mMDevice.FriendlyName);
//        dictionary_a.Add(i, iD2);
//        if (iD2 == iD)
//        {
//          num_a = i;
//        }
//      }
//    }

    internal static void SetDefaultEndpoint(int P_0)
    {
      AudioManager.SetDefaultEndpoint(dictionary_a[P_0]);
    }

    internal static void SetDefaultEndpoint(string deviceID)
    {
      try
      {
        policyConfigClient_a.SetDefaultEndpoint(deviceID, ERole.eMultimedia);
      }
      catch (Exception)
      {
      }
    }
  }
}