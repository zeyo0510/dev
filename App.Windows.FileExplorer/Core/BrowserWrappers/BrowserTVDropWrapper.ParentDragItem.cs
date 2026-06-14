using System;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserTVDropWrapper
  {
    public ShellItem ParentDragItem
    {
      get { return parentDragItem; }
      set { parentDragItem = value; }
    }
  }
}