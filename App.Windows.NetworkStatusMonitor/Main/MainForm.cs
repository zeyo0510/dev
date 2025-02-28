using System;
using System.Windows.Forms;
using App.Windows.NetworkStatusMonitor.Core;
/************************************************/
namespace App.Windows.NetworkStatusMonitor.Main
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      this.InitializeComponent();
      /************************************************/
      global::App.Windows.NetworkStatusMonitor.Core.NetworkStatusMonitor monitor = new global::App.Windows.NetworkStatusMonitor.Core.NetworkStatusMonitor(300);
      {
        monitor.NetworkInterfaceConnected += monitor_NetworkInterfaceConnected;
        monitor.NetworkInterfaceDisconnected += monitor_NetworkInterfaceDisconnected;
      }
      /************************************************/
      monitor.StartMonitoring();
    }
    /************************************************/
    private static void monitor_NetworkInterfaceDisconnected(object sender, NetworkStatusMonitorEventArgs e) {
      MessageBox.Show("Network Interface: " + e.Interface.Description + " Disconnected!");
    }
    /************************************************/
    private static void monitor_NetworkInterfaceConnected(object sender, NetworkStatusMonitorEventArgs e) {
      MessageBox.Show("Network Interface: " + e.Interface.Description + " Connected!");
    }
  }
}
