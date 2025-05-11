using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
/************************************************/
namespace Little_Disk_Cleaner
{
  partial class MainForm
  {
    public void ViewFileProperties()
    {
      ListViewItem item = this.listview1.SelectedItems
    . Cast<ListViewItem>()
    . First();
      /************************************************/
      FileInfo fi = item.Tag as FileInfo;
      Utils.ShowFileProperties(fi.FullName);
    }
  }
}