using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class BrowserListView
  {
    [Browsable(true)]
    public ContextMenu ColumnHeaderContextMenu
    {
      get; set;
    }
  }
}