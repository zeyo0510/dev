// https://github.com/naudio/NAudio/blob/main/src/NAudio.Core/Wave/WaveFormats/WaveFormatExtraData.cs
/************************************************/
using System.Runtime.InteropServices;
/************************************************/
namespace AudioCore
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  internal class WaveFormatExtraData : WaveFormat
  {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    private byte[] extraData = new byte[100];
    /************************************************/
    private WaveFormatExtraData()
    {
      // do nothing...
    }
    /************************************************/
    public WaveFormatExtraData(BinaryReader reader) : base(reader)
    {
      if (this._ExtraSize > 0)
      {
        reader.Read(extraData, 0, this._ExtraSize);
      }
    }
    /************************************************/
    public override void Serialize(BinaryWriter writer)
    {
      base.Serialize(writer);
      /************************************************/
      if (this._ExtraSize > 0)
      {
        writer.Write(this.extraData, 0, this._ExtraSize);
      }
    }
  }
}