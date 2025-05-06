using System.IO;
using System.Runtime.InteropServices;

namespace AudioCore
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public class AdpcmWaveFormat : WaveFormat
  {
    private short _SamplesPerBlock;

    private short _NumCoefficients;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    private short[] _Coefficients;

    public int SamplesPerBlock
    {
      get
      {
        return _SamplesPerBlock;
      }
    }

    public int NumCoefficients
    {
      get
      {
        return _NumCoefficients;
      }
    }

    public short[] Coefficients
    {
      get
      {
        return _Coefficients;
      }
    }

    private AdpcmWaveFormat()
      : this(8000, 1)
    {
    }

    public AdpcmWaveFormat(int P_0, int P_1)
      : base(P_0, 0, P_1)
    {
      waveFormatTag = WaveFormatEncoding.Adpcm;
      _ExtraSize = 32;
      switch (_SampleRate)
      {
      case 8000:
      case 11025:
        _BlockAlign = 256;
        break;
      case 22050:
        _BlockAlign = 512;
        break;
      default:
        _BlockAlign = 1024;
        break;
      }
      _BitsPerSample = 4;
      _SamplesPerBlock = (short)((_BlockAlign - 7 * P_1) * 8 / (_BitsPerSample * P_1) + 2);
      _AverageBytesPerSecond = base.SampleRate * _BlockAlign / _SamplesPerBlock;
      _NumCoefficients = 7;
      _Coefficients = new short[14]
      {
        256, 0, 512, -256, 0, 0, 192, 64, 240, 0,
        460, -208, 392, -232
      };
    }

    public override void Serialize(BinaryWriter P_0)
    {
      base.Serialize(P_0);
      P_0.Write(_SamplesPerBlock);
      P_0.Write(_NumCoefficients);
      short[] coefficients = _Coefficients;
      foreach (short value in coefficients)
      {
        P_0.Write(value);
      }
    }

    public override string ToString()
    {
      return string.Format("Microsoft ADPCM {0} Hz {1} channels {2} bits per sample {3} samples per block", base.SampleRate, _Channels, _BitsPerSample, _SamplesPerBlock);
    }
  }
}