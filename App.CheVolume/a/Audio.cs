using AudioCore;

namespace a
{
  internal static class Audio
  {
    internal static readonly List<string> list_a;

    internal static int num_a;

    internal static readonly MMDeviceEnumerator _MMDeviceEnumerator_;

    private static readonly Dictionary<int, string> dictionary_a;

    private static readonly PolicyConfigClient policyConfigClient_a;

    static Audio()
    {
      list_a = new List<string>();
      dictionary_a = new Dictionary<int, string>();
      policyConfigClient_a = new PolicyConfigClient();
      _MMDeviceEnumerator_ = new MMDeviceEnumerator();
    }

    internal static void Method1(DataFlow dataFlow)
    {
      list_a.Clear();
      dictionary_a.Clear();
      AudioDeviceCollection defaultAudioEndpoint = _MMDeviceEnumerator_.EnumAudioEndpoints(dataFlow, EDeviceState.Active);
      string id = _MMDeviceEnumerator_.EnumerateAudioEndPoints(dataFlow, ERole.eMultimedia).ID;
      int count = defaultAudioEndpoint.Count;
      for (int i = 0; i < count; i++)
      {
        AudioDevice mMDevice = defaultAudioEndpoint[i];
        string iD2 = mMDevice.ID;
        list_a.Add(mMDevice.FriendlyName);
        dictionary_a.Add(i, iD2);
        if (iD2 == id)
        {
          num_a = i;
        }
      }
    }

    internal static void SetDefault(int index)
    {
      SetDefault(dictionary_a[index]);
    }

    internal static void SetDefault(string s)
    {
      try
      {
        policyConfigClient_a.SetDefaultEndpoint(s, ERole.eMultimedia);
      }
      catch (Exception)
      {
        
      }
    }
  }
}