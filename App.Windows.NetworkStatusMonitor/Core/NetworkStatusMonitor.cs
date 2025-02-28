using System;
using System.Threading;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  public partial class NetworkStatusMonitor
  {
    #region Fields

    /// <summary>
    /// thread monitoring network status and raising events.
    /// </summary>
    private Thread _monitorThread = null;

    /// <summary>
    /// run flag.
    /// </summary>
    private bool _run = true;

    /// <summary>
    /// last recorded network interface status.
    /// </summary>
    private NetworkStatus _last = null;

    /// <summary>
    /// pulse to signal WaitForPoll.
    /// </summary>
    private object _pulse = new object();

    #endregion

    public NetworkStatusMonitor(int pollInterval)
    {
      _monitorThread = new Thread(MonitorTask);
      /************************************************/
      try {
        _monitorThread.Name = "NetMon";
      } catch {
        // do nothing...
      }
      /************************************************/
      _waitInterval = pollInterval;
    }
  }
}