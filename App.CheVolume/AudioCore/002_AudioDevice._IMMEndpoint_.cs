using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore
{
  partial class AudioDevice
  {
    public IMMEndpoint IMMEndpoint
    {
      get
      {
        if (field == null)
        {
          field = (IMMEndpoint)this.MMDevice;
        }
        /************************************************/
        return field;
      }
    }
  }
}