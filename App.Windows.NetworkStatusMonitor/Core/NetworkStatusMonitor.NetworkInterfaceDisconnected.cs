using System;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatusMonitor
  {
    public event EventHandler<NetworkStatusMonitorEventArgs> NetworkInterfaceDisconnected = null;
  }
}