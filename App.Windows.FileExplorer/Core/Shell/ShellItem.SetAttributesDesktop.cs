using System;

namespace ShellDll
{
  partial class ShellItem
  {
    private static void SetAttributesDesktop(ShellItem item)
    {
        item.isFolder = true;
        item.isLink = false;
        item.isShared = false;
        item.isFileSystem = true;
        item.isHidden = false;
        item.hasSubfolder = true;
        item.isBrowsable = true;
        item.canRename = false;
        item.canRead = true;
    }
  }
}