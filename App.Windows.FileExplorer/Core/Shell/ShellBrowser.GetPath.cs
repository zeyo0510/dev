using System;
using System.Collections;

namespace ShellDll
{
  partial class ShellBrowser
  {
    internal ShellItem[] GetPath(ShellItem item)
    {
        ArrayList pathList = new ArrayList();
        
        ShellItem currentItem = item;
        while (currentItem.ParentItem != null)
        {
            pathList.Add(currentItem);
            currentItem = currentItem.ParentItem;
        }
        pathList.Add(currentItem);
        pathList.Reverse();

        return (ShellItem[])pathList.ToArray(typeof(ShellItem));
    }
  }
}