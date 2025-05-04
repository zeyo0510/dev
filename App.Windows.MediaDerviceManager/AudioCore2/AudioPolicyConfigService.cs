using System;
using AudioCore;
using AudioCore.Interfaces;

namespace AudioCore2
{
  internal class AudioPolicyConfigService
  {
    private const string DEVINTERFACE_AUDIO_RENDER = "#{e6327cad-dcec-4949-ae8a-991e976a79d2}";

    private const string DEVINTERFACE_AUDIO_CAPTURE = "#{2eef81be-33fa-4800-9670-1cd474972c3f}";

    private const string MMDEVAPI_TOKEN = "\\\\?\\SWD#MMDEVAPI#";

    private IAudioPolicyConfigFactory _sharedPolicyConfig;

    private EDataFlow _flow;

    public AudioPolicyConfigService(EDataFlow P_0)
    {
      _flow = P_0;
    }

    private void EnsurePolicyConfig()
    {
      if (_sharedPolicyConfig == null)
      {
        _sharedPolicyConfig = AudioPolicyConfigFactory.Create();
      }
    }

    private string GenerateDeviceId(string P_0)
    {
      return "\\\\?\\SWD#MMDEVAPI#" + P_0 + ((_flow == EDataFlow.eRender) ? "#{e6327cad-dcec-4949-ae8a-991e976a79d2}" : "#{2eef81be-33fa-4800-9670-1cd474972c3f}");
    }

    private string UnpackDeviceId(string P_0)
    {
      if (P_0.StartsWith("\\\\?\\SWD#MMDEVAPI#"))
      {
        P_0 = P_0.Remove(0, "\\\\?\\SWD#MMDEVAPI#".Length);
      }
      if (P_0.EndsWith("#{e6327cad-dcec-4949-ae8a-991e976a79d2}"))
      {
        P_0 = P_0.Remove(P_0.Length - "#{e6327cad-dcec-4949-ae8a-991e976a79d2}".Length);
      }
      if (P_0.EndsWith("#{2eef81be-33fa-4800-9670-1cd474972c3f}"))
      {
        P_0 = P_0.Remove(P_0.Length - "#{2eef81be-33fa-4800-9670-1cd474972c3f}".Length);
      }
      return P_0;
    }

    public void SetDefaultEndPoint(string P_0, int P_1)
    {
      try
      {
        EnsurePolicyConfig();
        IntPtr zero = IntPtr.Zero;
        if (!string.IsNullOrWhiteSpace(P_0))
        {
          string text = GenerateDeviceId(P_0);
          Combase._WindowsCreateString(text, (uint)text.Length, out zero);
        }
        _sharedPolicyConfig.SetPersistedDefaultAudioEndpoint((uint)P_1, _flow, ERole.eMultimedia, zero);
        _sharedPolicyConfig.SetPersistedDefaultAudioEndpoint((uint)P_1, _flow, ERole.eConsole, zero);
      }
      catch (Exception)
      {
      }
    }

    public string GetDefaultEndPoint(int P_0)
    {
      try
      {
        EnsurePolicyConfig();
        string text;
        _sharedPolicyConfig.GetPersistedDefaultAudioEndpoint((uint)P_0, _flow, ERole.eMultimedia, out text);
        return UnpackDeviceId(text);
      }
      catch (Exception)
      {
      }
      return null;
    }
  }
}