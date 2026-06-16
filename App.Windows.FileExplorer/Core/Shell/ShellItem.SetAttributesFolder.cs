using System;

namespace ShellDll
{
  partial class ShellItem
  {
    private static void SetAttributesFolder(ShellItem item)
    {
        // file/folder attributes
        WinAPI.SFGAO attribs = 
            WinAPI.SFGAO.SHARE |
            WinAPI.SFGAO.FILESYSTEM |
            WinAPI.SFGAO.HIDDEN |
            WinAPI.SFGAO.HASSUBFOLDER |
            WinAPI.SFGAO.BROWSABLE |
            WinAPI.SFGAO.CANRENAME | 
            WinAPI.SFGAO.STORAGE;
        item.ParentItem.ShellFolder.GetAttributesOf(
            1, new IntPtr[] { item.PIDLRel.Ptr }, ref attribs);
        
        item.IsFolder = true;
        item.IsLink = false;
        item.IsShared = (attribs & WinAPI.SFGAO.SHARE) != 0;
        item.IsFileSystem = (attribs & WinAPI.SFGAO.FILESYSTEM) != 0;
        item.IsHidden = (attribs & WinAPI.SFGAO.HIDDEN) != 0;
        item.HasSubfolder = (attribs & WinAPI.SFGAO.HASSUBFOLDER) != 0;
        item.IsBrowsable = (attribs & WinAPI.SFGAO.BROWSABLE) != 0;
        item.CanRename = (attribs & WinAPI.SFGAO.CANRENAME) != 0;
        item.CanRead = (attribs & WinAPI.SFGAO.STORAGE) != 0;

        item.IsDisk = (item.Path.Length == 3 && item.Path.EndsWith(":\\"));
    }
  }
}