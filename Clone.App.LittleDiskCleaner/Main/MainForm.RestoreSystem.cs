using System;
using System.Diagnostics;
using System.Windows.Forms;
/************************************************/
namespace Little_Disk_Cleaner
{
  partial class MainForm
  {
    public void RestoreSystem()
    {
      try
      {
        Process.Start("rstrui.exe");
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, string.Format("An error occured trying to launch System Restore ({0})", ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}