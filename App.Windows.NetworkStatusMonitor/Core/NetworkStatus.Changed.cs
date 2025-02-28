using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatus
  {
    public IEnumerable<NetworkInterface> Changed(NetworkStatus lastStatus)
    {
      foreach (var pair in _items)
      {
        if (lastStatus._items.ContainsKey(pair.Key))
        {
          if (lastStatus._items[pair.Key].OperationalStatus != pair.Value.OperationalStatus)
          {
            yield return pair.Value.Interface;
          }
        }
        else
        {
          yield return pair.Value.Interface;
        }
      }
      /************************************************/
      foreach (var pair in lastStatus._items)
      {
        if (!_items.ContainsKey(pair.Key))
        {
          yield return pair.Value.Interface;
        }
      }
    }
  }
}
