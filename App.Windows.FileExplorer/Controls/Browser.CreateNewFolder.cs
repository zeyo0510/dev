using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    public bool CreateNewFolder()
    {
      if (selectedItem.IsFileSystem)
      {
        IntPtr newMenuPtr;
        IContextMenu newMenu;

        if (ContextMenuHelper.GetNewContextMenu(selectedItem, out newMenuPtr, out newMenu)) {
          lock (ShellBrowser) {
            NewItemCreated = true;
          }

          ContextMenuHelper.InvokeCommand(newMenu, "NewFolder", ShellItem.GetRealPath(selectedItem), new Point(0, 0));

          Marshal.ReleaseComObject(newMenu);
          Marshal.Release(newMenuPtr);

          return true;
        }
        else
          return false;
      }
      else
        return false;
    }
  }
}