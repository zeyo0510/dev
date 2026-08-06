using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    private IMMEndpoint IMMEndpoint
    {
      get
      {
        if (field == null)
        {
          field = (IMMEndpoint)this.IMMDevice;
        }
        /************************************************/
        return field;
      }
    }
  }
}