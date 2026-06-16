using System;
/************************************************/
namespace ShellDll
{
  partial class ShellItem
  {
    public static void UpdateShellFolders(ShellItem item)
    {
      item.UpdateShellFolder = true;
      /************************************************/
      foreach (ShellItem child in item.SubFolders)
      {
        ShellItem.UpdateShellFolders(child);
      }
    }
  }
}