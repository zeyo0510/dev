namespace AudioCore
{
  partial class AudioDevice
  {
    public DataFlow DataFlow
    {
      get
      {
        this.IMMEndpoint.GetDataFlow(out int retValue);
        /************************************************/
        return (DataFlow)retValue;
      }
    }
  }
}