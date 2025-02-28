using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  public partial class NetworkStatus
  {
    private Dictionary<string, NiStatusRecord> _items = new Dictionary<string, NiStatusRecord>();
    /************************************************/
    public NetworkStatus()
    {
      NetworkInterface.GetAllNetworkInterfaces()
    . ToList()
    . ForEach((_) => {
        _items.Add(_.Id, new NiStatusRecord(_));
      });
    }
    /************************************************/
    public NetworkInterface this[string id]
    {
      get
      {
        return _items[id].Interface;
      }
      set
      {
        _items[id].Interface = value;
      }
    }
  }
}