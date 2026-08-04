namespace AudioCore
{
  partial class MMDevice
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