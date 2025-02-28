using System;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatus
  {
    public bool Contains(string id)
    {
      return _items.ContainsKey(id);
    }
  }
}