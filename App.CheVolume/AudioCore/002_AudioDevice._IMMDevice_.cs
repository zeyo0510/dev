using AudioCore.Interfaces;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    public IMMDevice MMDevice
    {
      get; private set;
    }
  }
}