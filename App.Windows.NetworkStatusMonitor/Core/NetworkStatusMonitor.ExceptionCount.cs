using System;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatusMonitor
  {
    private int _exceptionCount = 0;
    /************************************************/
    public int ExceptionCount
    {
      get
      {
        return _exceptionCount;
      }
    }
  }
}