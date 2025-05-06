using System;
using System.IO;
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public class WaveFormatExtensible : WaveFormat
  {
    private WaveFormatExtensible()
    {
      // do nothing...
    }
    /************************************************/
    public WaveFormatExtensible(int P_0, int P_1, int P_2) : base(P_0, P_1, P_2)
    {
      waveFormatTag = WaveFormatEncoding.Extensible;
      _ExtraSize = 22;
      wValidBitsPerSample = (short)P_1;
      for (int i = 0; i < P_2; i++)
      {
        dwChannelMask |= 1 << i;
      }
      if (P_1 == 32)
      {
        subFormat = AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT;
      }
      else
      {
        subFormat = AudioMediaSubtypes.MEDIASUBTYPE_PCM;
      }
    }
    /************************************************/
    private short wValidBitsPerSample;
    /************************************************/
    private int dwChannelMask;
    /************************************************/
    private Guid subFormat;
    /************************************************/
    public override void Serialize(BinaryWriter P_0)
    {
      base.Serialize(P_0);
      P_0.Write(wValidBitsPerSample);
      P_0.Write(dwChannelMask);
      byte[] array = subFormat.ToByteArray();
      P_0.Write(array, 0, array.Length);
    }
    /************************************************/
    public override string ToString()
    {
      return string.Format("{0} wBitsPerSample:{1} dwChannelMask:{2} subFormat:{3} extraSize:{4}", base.ToString(), wValidBitsPerSample, dwChannelMask, subFormat, _ExtraSize);
    }
  }
}