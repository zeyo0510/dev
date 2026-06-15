using System;
using System.Collections;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void InitFileView()
    {
      this.fileView.Columns.Add("Name", "Name", 0, HorizontalAlignment.Left, -1);
      this.columnContextMenu = new ContextMenu();
      this.invisibleColumns = new ArrayList();
    }
  }
}