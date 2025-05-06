using System.IO;
using System.Runtime.InteropServices;

namespace AudioCore
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  internal class WaveFormatExtraData : WaveFormat
  {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    private byte[] extraData = new byte[100];

    private WaveFormatExtraData()
    {
    }

    public WaveFormatExtraData(BinaryReader P_0) : base(P_0)
    {
      if (_ExtraSize > 0)
      {
        P_0.Read(extraData, 0, _ExtraSize);
      }
    }

    public override void Serialize(BinaryWriter P_0)
    {
      base.Serialize(P_0);
      if (_ExtraSize > 0)
      {
        P_0.Write(extraData, 0, _ExtraSize);
      }
    }
  }
}