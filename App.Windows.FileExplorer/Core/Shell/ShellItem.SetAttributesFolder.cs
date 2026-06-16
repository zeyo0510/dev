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
        
        item.isFolder = true;
        item.isLink = false;
        item.isShared = (attribs & WinAPI.SFGAO.SHARE) != 0;
        item.isFileSystem = (attribs & WinAPI.SFGAO.FILESYSTEM) != 0;
        item.isHidden = (attribs & WinAPI.SFGAO.HIDDEN) != 0;
        item.hasSubfolder = (attribs & WinAPI.SFGAO.HASSUBFOLDER) != 0;
        item.isBrowsable = (attribs & WinAPI.SFGAO.BROWSABLE) != 0;
        item.canRename = (attribs & WinAPI.SFGAO.CANRENAME) != 0;
        item.canRead = (attribs & WinAPI.SFGAO.STORAGE) != 0;

        item.isDisk = (item.path.Length == 3 && item.path.EndsWith(":\\"));
    }
  }
}