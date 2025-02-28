using System;
using System.Windows.Forms;
using App.Windows.NetworkStatusMonitor.Main;
/************************************************/
namespace App.Windows.NetworkStatusMonitor
{
  internal sealed class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}