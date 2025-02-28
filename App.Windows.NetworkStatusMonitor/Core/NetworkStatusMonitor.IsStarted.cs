using System;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatusMonitor
  {
    private bool _isStarted = false;
    /************************************************/
    public bool IsStarted
    {
      get
      {
        bool retValue = this._isStarted;
        /************************************************/
        return retValue;
      }
      private set
      {
        this._isStarted = value;
      }
    }
  }
}