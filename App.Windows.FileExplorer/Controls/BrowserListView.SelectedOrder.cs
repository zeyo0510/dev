using System;
using System.Collections;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class BrowserListView
  {
    [Browsable(false)]
    public ArrayList SelectedOrder {
      get { return selectedOrder; }
    }
  }
}