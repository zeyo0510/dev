using System;
using System.Threading;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatusMonitor
  {
    public void WaitForPoll() {
      // this shouldn't be called from the monitor thread
      // just return if it is.
      if (_monitorThread == Thread.CurrentThread)
        return;

      // if running and started...
      if (_run && _isStarted) {
        // wait until the lock is released..
        lock (_pulse)
          Monitor.Wait(_pulse);
      }
    }
  }
}