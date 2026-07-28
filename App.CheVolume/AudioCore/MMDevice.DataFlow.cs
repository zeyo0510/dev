namespace AudioCore
{
  partial class MMDevice
  {
    public DataFlow DataFlow
    {
      get
      {
        this._MMEndpoint_.GetDataFlow(out DataFlow retValue);
        /************************************************/
        return retValue;
      }
    }
  }
}