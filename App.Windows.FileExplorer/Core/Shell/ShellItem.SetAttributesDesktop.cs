using System;
/************************************************/
namespace ShellDll
{
  partial class ShellItem
  {
    private static void SetAttributesDesktop(ShellItem item)
    {
      item.IsFolder = true;
      item.IsLink = false;
      item.IsShared = false;
      item.IsFileSystem = true;
      item.IsHidden = false;
      item.HasSubfolder = true;
      item.IsBrowsable = true;
      item.CanRename = false;
      item.CanRead = true;
    }
  }
}