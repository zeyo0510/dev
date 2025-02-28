using System;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatusMonitor
  {
    private int _waitInterval = 100;
    /************************************************/
    public int PollInterval
    {
      get { return _waitInterval; }
      set { _waitInterval = value; }
    }
  }
}