using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatus : IEnumerable<NetworkInterface>
  {
    public IEnumerator<NetworkInterface> GetEnumerator()
    {
      foreach (var pair in _items)
      {
        yield return pair.Value.Interface;
      }
    }
    /************************************************/
    IEnumerator IEnumerable.GetEnumerator()
    {
      foreach (var pair in _items)
      {
        yield return pair.Value;
      }
    }
  }
}