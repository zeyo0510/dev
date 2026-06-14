using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class BrowserListView
  {
    private ContextMenu columnHeaderContextMenu;
    /************************************************/
    [Browsable(true)]
    public ContextMenu ColumnHeaderContextMenu
    {
      get
      {
        return columnHeaderContextMenu;
      }
      set
      {
        columnHeaderContextMenu = value;
      }
    }
  }
}