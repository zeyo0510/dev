using System;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserLVDropWrapper
  {
    private ShellItem parentDropItem, parentDragItem;

    public ShellItem ParentDragItem
    {
        get { return parentDragItem; }
        set { parentDragItem = value; }
    }
  }
}