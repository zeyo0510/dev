using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Little_Disk_Cleaner
{
  static class Program
  {
    [STAThread]
    static void Main()
    {
      bool bMutexCreated = false;
      Mutex mutexMain = new Mutex(true, "Little Disk Cleaner", out bMutexCreated);

      // If mutex isnt available, show message and exit...
      if (!bMutexCreated)
      {
        MessageBox.Show("Another program seems to be already running...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
        return;
      }

      // Check if admin, otherwise exit
      if (!Permissions.IsUserAdministrator)
      {
        MessageBox.Show("You must be an administrator to use this program", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
        return;
      }

      // Enable needed privileges
      Permissions.SetPrivileges(true);

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());

      // Disable needed privileges
      Permissions.SetPrivileges(false);

      // Release Mutex
      mutexMain.ReleaseMutex();

      // Save Settings
      Properties.Settings.Default.Save();
    }
  }
}