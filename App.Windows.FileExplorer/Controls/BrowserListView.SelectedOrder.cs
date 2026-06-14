using System;
using System.Collections;
using System.ComponentModel;
/************************************************/
namespace FileBrowser
{
  partial class BrowserListView
  {
    private ArrayList selectedOrder;
    /************************************************/
    [Browsable(false)]
    public ArrayList SelectedOrder
    {
      get
      {
        return selectedOrder;
      }
    }
  }
}