using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
/************************************************/
namespace Little_Disk_Cleaner
{
  partial class MainForm
  {
    public void OpenFile()
    {
      ListViewItem item = this.listview1.SelectedItems.Cast<ListViewItem>().First();
      /************************************************/
      FileInfo fi = item.Tag as FileInfo;
      ProcessStartInfo psi = new ProcessStartInfo(fi.FullName);
      {
        psi.ErrorDialog = true;
        psi.ErrorDialogParentHandle = this.Handle;
      }
      /************************************************/
      Process.Start(psi);
    }
  }
}