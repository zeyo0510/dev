using System;

namespace ShellDll
{
  partial class ShellItem
  {
    private static void SetAttributesFile(ShellItem item)
    {
        // file/folder attributes
        WinAPI.SFGAO attribs =
            WinAPI.SFGAO.LINK |
            WinAPI.SFGAO.SHARE | 
            WinAPI.SFGAO.FILESYSTEM |
            WinAPI.SFGAO.HIDDEN |
            WinAPI.SFGAO.CANRENAME |
            WinAPI.SFGAO.STREAM;
        item.ParentItem.ShellFolder.GetAttributesOf(
            1, new IntPtr[] { item.PIDLRel.Ptr }, ref attribs);

        item.isFolder = false;
        item.isLink = (attribs & WinAPI.SFGAO.LINK) != 0;
        item.isShared = (attribs & WinAPI.SFGAO.SHARE) != 0;
        item.isFileSystem = (attribs & WinAPI.SFGAO.FILESYSTEM) != 0;
        item.isHidden = (attribs & WinAPI.SFGAO.HIDDEN) != 0;
        item.hasSubfolder = false;
        item.isBrowsable = false;
        item.canRename = (attribs & WinAPI.SFGAO.CANRENAME) != 0;
        item.canRead = (attribs & WinAPI.SFGAO.STREAM) != 0;

        item.isDisk = false;
    }     
  }
}