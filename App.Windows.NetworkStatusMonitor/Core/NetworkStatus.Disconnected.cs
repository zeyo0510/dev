using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatus
  {
    public IEnumerable<NetworkInterface> Disconnected(NetworkStatus lastStatus)
    {
      foreach (var pair in _items)
      {
        if (lastStatus._items.ContainsKey(pair.Key))
        {
          if (lastStatus._items[pair.Key].OperationalStatus == OperationalStatus.Up)
          {
            if (pair.Value.OperationalStatus != OperationalStatus.Up)
            {
              yield return pair.Value.Interface;
            }
          }
        }
      }
      /************************************************/
      foreach (var pair in lastStatus._items)
      {
        if (!_items.ContainsKey(pair.Key))
        {
          if (pair.Value.OperationalStatus == OperationalStatus.Up)
          {
            yield return pair.Value.Interface;
          }
        }
      }
    }
  }
}