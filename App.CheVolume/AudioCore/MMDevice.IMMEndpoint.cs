using JC.CS.Lib.CoreAudio;
/************************************************/
namespace AudioCore
{
  partial class MMDevice
  {
    public IMMEndpoint IMMEndpoint
    {
      get
      {
        if (field == null)
        {
          field = (IMMEndpoint)this._MMDevice_;
        }
        /************************************************/
        return field;
      }
    }
  }
}