using System;
using System.Net.NetworkInformation;
using System.Threading;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Core
{
  partial class NetworkStatusMonitor
  {
    private void MonitorTask()
    {
      while (_run)
      {
        try {
          // has the last status been taken?
          if (_last == null) {
            // snapshot the current status.
            _last = new NetworkStatus();

            // sleep for the duration of the poll interval.
            Thread.Sleep(_waitInterval);

            // run to the next iteration.
            continue;
          } else {
            NetworkStatus current = new NetworkStatus();

            // test for changes and raise events where neccessary.
            if (NetworkInterfaceConnected != null)
            {
              // evaluate all the network interfaces that have connected since the
              // last snapshot
              foreach (var ni in current.Connected(_last)) {
                // test if the network interface was in the last snapshot:
                OperationalStatus lastStatus = OperationalStatus.NotPresent;
                if (_last.Contains(ni.Id))
                  lastStatus = _last[ni.Id].OperationalStatus;

                // raise the interface connected event:
                NetworkInterfaceConnected(this, new NetworkStatusMonitorEventArgs() {
                  EventType = NetworkStatusMonitorEventType.Connected,
                  Interface = ni,
                  LastOperationalStatus = lastStatus
                });

              }
            }

            // test for interface dis-connections
            if (NetworkInterfaceDisconnected != null)
            {
              // enumerate the network interfaces that were Up but are not now.
              foreach (var ni in current.Disconnected(_last)) {
                // raise the interface dis-connected event:
                NetworkInterfaceDisconnected(this, new NetworkStatusMonitorEventArgs() {
                  // set the event-type, interface and last status.
                  EventType = NetworkStatusMonitorEventType.Disconnected,
                  Interface = ni,
                  LastOperationalStatus = OperationalStatus.Up
                });
              }
            }

            // test for interface changes.
            if (NetworkInterfaceChanged != null) {
              // enumerate the interfaces that have changed status in any way since
              // the last snapshot.
              foreach (var ni in current.Changed(_last)) {
                // find the last status of the interface:
                OperationalStatus lastStatus = OperationalStatus.NotPresent;
                if (_last.Contains(ni.Id))
                  lastStatus = _last[ni.Id].OperationalStatus;

                // raise the interface changed event:
                NetworkInterfaceChanged(this, new NetworkStatusMonitorEventArgs() {
                  // set the event-type interface and last status.
                  EventType = NetworkStatusMonitorEventType.Changed,
                  Interface = ni,
                  LastOperationalStatus = lastStatus
                });
              }
            }

            // set last to the current.
            _last = current;

            // wait...
            if (_run)
              Thread.Sleep(_waitInterval);
          }

          // pulse any threads waiting in WaitForPoll.
          lock (_pulse)
            Monitor.PulseAll(_pulse);
        } catch (Exception exception) {
          Console.WriteLine(exception.ToString());

          Interlocked.Increment(ref _exceptionCount);
        }
      }
    }
  }
}