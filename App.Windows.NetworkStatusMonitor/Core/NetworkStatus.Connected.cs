using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatus
  {
    public IEnumerable<NetworkInterface> Connected(NetworkStatus lastStatus)
    {
      foreach (var pair in _items)
      {
        if (lastStatus._items.ContainsKey(pair.Key)) {
          if (lastStatus._items[pair.Key].OperationalStatus != OperationalStatus.Up)
          {
            if (pair.Value.OperationalStatus == OperationalStatus.Up)
            {
              yield return pair.Value.Interface;
            }
          }
        } else {
          if (pair.Value.OperationalStatus == OperationalStatus.Up)
          {
            yield return pair.Value.Interface;
          }
        }
      }
    }
  }
}