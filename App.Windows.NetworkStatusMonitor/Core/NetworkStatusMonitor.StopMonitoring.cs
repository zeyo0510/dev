using System;
using System.Threading;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatusMonitor
  {
    public void StopMonitoring()
    {
      if (this.IsStarted)
      {
        _run = false;
        _monitorThread.Join();
        _monitorThread = new Thread(MonitorTask);
        this.IsStarted = false;
      }
    }
  }
}