using System;
/************************************************/
namespace FileBrowser
{
  partial class BrowserListView
  {
    public void ClearSelections()
    {
      selectedOrder.Clear();
      selectedOrder.Capacity = 0;
    }
  }
}