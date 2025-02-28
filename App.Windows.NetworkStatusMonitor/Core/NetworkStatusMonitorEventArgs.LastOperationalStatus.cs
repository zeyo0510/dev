using System;
using System.Net.NetworkInformation;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatusMonitorEventArgs
  {
    public OperationalStatus LastOperationalStatus
    {
      get; set;
    }
  }
}