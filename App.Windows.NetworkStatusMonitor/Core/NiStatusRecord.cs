using System;
using System.Net.NetworkInformation;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  public partial class NiStatusRecord
  {
    public NiStatusRecord(NetworkInterface ni)
    {
      this.Interface         = ni;
      this.OperationalStatus = ni.OperationalStatus;
    }
  }
}