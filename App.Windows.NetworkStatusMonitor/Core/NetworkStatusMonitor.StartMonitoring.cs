using System;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatusMonitor
  {
    public void StartMonitoring()
    {
      if (!this.IsStarted)
      {
        _run = true;
        _monitorThread.Start();
        this.IsStarted = true;
      }
    }
  }
}