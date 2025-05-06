// https://learn.microsoft.com/en-us/windows/win32/api/mmeapi/ns-mmeapi-waveformat

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace AudioCore
{
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public class WaveFormat
  {
    protected WaveFormatEncoding waveFormatTag;

    protected short _Channels;

    protected int _SampleRate;

    protected int _AverageBytesPerSecond;

    protected short _BlockAlign;

    protected short _BitsPerSample;

    protected short _ExtraSize;

    public WaveFormatEncoding Encoding
    {
      get
      {
        return waveFormatTag;
      }
    }

    public int Channels
    {
      get
      {
        return _Channels;
      }
    }

    public int SampleRate
    {
      get
      {
        return _SampleRate;
      }
    }

    public int AverageBytesPerSecond
    {
      get
      {
        return _AverageBytesPerSecond;
      }
    }

    public virtual int BlockAlign
    {
      get
      {
        return _BlockAlign;
      }
    }

    public int BitsPerSample
    {
      get
      {
        return _BitsPerSample;
      }
    }

    public int ExtraSize
    {
      get
      {
        return _ExtraSize;
      }
    }

    public WaveFormat()
      : this(44100, 16, 2)
    {
    }

    public WaveFormat(int P_0, int P_1)
      : this(P_0, 16, P_1)
    {
    }

    public int ConvertLatencyToByteSize(int P_0)
    {
      int num = (int)((double)AverageBytesPerSecond / 1000.0 * (double)P_0);
      if (num % BlockAlign != 0)
      {
        num = num + BlockAlign - num % BlockAlign;
      }
      return num;
    }

    public static WaveFormat CreateCustomFormat(WaveFormatEncoding P_0, int P_1, int P_2, int P_3, int P_4, int P_5)
    {
      WaveFormat waveFormat = new WaveFormat();
      waveFormat.waveFormatTag = P_0;
      waveFormat._Channels = (short)P_2;
      waveFormat._SampleRate = P_1;
      waveFormat._AverageBytesPerSecond = P_3;
      waveFormat._BlockAlign = (short)P_4;
      waveFormat._BitsPerSample = (short)P_5;
      waveFormat._ExtraSize = 0;
      return waveFormat;
    }

    public static WaveFormat CreateALawFormat(int P_0, int P_1)
    {
      return CreateCustomFormat(WaveFormatEncoding.ALaw, P_0, P_1, P_0 * P_1, 1, 8);
    }

    public static WaveFormat CreateMuLawFormat(int P_0, int P_1)
    {
      return CreateCustomFormat(WaveFormatEncoding.MuLaw, P_0, P_1, P_0 * P_1, 1, 8);
    }

    public WaveFormat(int P_0, int P_1, int P_2)
    {
      if (P_2 < 1)
      {
        throw new ArgumentOutOfRangeException("Channels must be 1 or greater", "channels");
      }
      waveFormatTag = WaveFormatEncoding.Pcm;
      _Channels = (short)P_2;
      _SampleRate = P_0;
      _BitsPerSample = (short)P_1;
      _ExtraSize = 0;
      _BlockAlign = (short)(P_2 * (P_1 / 8));
      _AverageBytesPerSecond = _SampleRate * _BlockAlign;
    }

    public static WaveFormat CreateIeeeFloatWaveFormat(int P_0, int P_1)
    {
      WaveFormat waveFormat = new WaveFormat();
      waveFormat.waveFormatTag = WaveFormatEncoding.IeeeFloat;
      waveFormat._Channels = (short)P_1;
      waveFormat._BitsPerSample = 32;
      waveFormat._SampleRate = P_0;
      waveFormat._BlockAlign = (short)(4 * P_1);
      waveFormat._AverageBytesPerSecond = P_0 * waveFormat._BlockAlign;
      waveFormat._ExtraSize = 0;
      return waveFormat;
    }

    public static WaveFormat MarshalFromPtr(IntPtr P_0)
    {
      WaveFormat waveFormat = (WaveFormat)Marshal.PtrToStructure(P_0, typeof(WaveFormat));
      switch (waveFormat.Encoding)
      {
      case WaveFormatEncoding.Pcm:
        waveFormat._ExtraSize = 0;
        break;
      case WaveFormatEncoding.Extensible:
        waveFormat = (WaveFormatExtensible)Marshal.PtrToStructure(P_0, typeof(WaveFormatExtensible));
        break;
      case WaveFormatEncoding.Adpcm:
        waveFormat = (AdpcmWaveFormat)Marshal.PtrToStructure(P_0, typeof(AdpcmWaveFormat));
        break;
      default:
        if (waveFormat.ExtraSize > 0)
        {
          waveFormat = (WaveFormatExtraData)Marshal.PtrToStructure(P_0, typeof(WaveFormatExtraData));
        }
        break;
      }
      return waveFormat;
    }

    public static IntPtr MarshalToPtr(WaveFormat P_0)
    {
      IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(P_0));
      Marshal.StructureToPtr(P_0, intPtr, false);
      return intPtr;
    }

    public WaveFormat(BinaryReader P_0)
    {
      int num = P_0.ReadInt32();
      if (num < 16)
      {
        throw new ApplicationException("Invalid WaveFormat Structure");
      }
      waveFormatTag = (WaveFormatEncoding)P_0.ReadUInt16();
      _Channels = P_0.ReadInt16();
      _SampleRate = P_0.ReadInt32();
      _AverageBytesPerSecond = P_0.ReadInt32();
      _BlockAlign = P_0.ReadInt16();
      _BitsPerSample = P_0.ReadInt16();
      if (num > 16)
      {
        _ExtraSize = P_0.ReadInt16();
        if (_ExtraSize > num - 18)
        {
          Console.WriteLine("Format chunk mismatch");
          _ExtraSize = (short)(num - 18);
        }
      }
    }

    public override string ToString()
    {
      WaveFormatEncoding waveFormatEncoding = waveFormatTag;
      if (waveFormatEncoding == WaveFormatEncoding.Pcm || waveFormatEncoding == WaveFormatEncoding.Extensible)
      {
        return string.Format("{0} bit PCM: {1}kHz {2} channels", _BitsPerSample, _SampleRate / 1000, _Channels);
      }
      return waveFormatTag.ToString();
    }

    public override bool Equals(object P_0)
    {
      WaveFormat waveFormat = P_0 as WaveFormat;
      if (waveFormat != null)
      {
        if (waveFormatTag == waveFormat.waveFormatTag && _Channels == waveFormat._Channels && _SampleRate == waveFormat._SampleRate && _AverageBytesPerSecond == waveFormat._AverageBytesPerSecond && _BlockAlign == waveFormat._BlockAlign)
        {
          return _BitsPerSample == waveFormat._BitsPerSample;
        }
        return false;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return (int)waveFormatTag ^ (int)_Channels ^ _SampleRate ^ _AverageBytesPerSecond ^ _BlockAlign ^ _BitsPerSample;
    }

    public virtual void Serialize(BinaryWriter P_0)
    {
      P_0.Write(18 + _ExtraSize);
      P_0.Write((short)Encoding);
      P_0.Write((short)Channels);
      P_0.Write(SampleRate);
      P_0.Write(AverageBytesPerSecond);
      P_0.Write((short)BlockAlign);
      P_0.Write((short)BitsPerSample);
      P_0.Write(_ExtraSize);
    }
  }
}