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

        item.IsFolder = false;
        item.IsLink = (attribs & WinAPI.SFGAO.LINK) != 0;
        item.IsShared = (attribs & WinAPI.SFGAO.SHARE) != 0;
        item.IsFileSystem = (attribs & WinAPI.SFGAO.FILESYSTEM) != 0;
        item.IsHidden = (attribs & WinAPI.SFGAO.HIDDEN) != 0;
        item.HasSubfolder = false;
        item.IsBrowsable = false;
        item.CanRename = (attribs & WinAPI.SFGAO.CANRENAME) != 0;
        item.CanRead = (attribs & WinAPI.SFGAO.STREAM) != 0;

        item.IsDisk = false;
    }     
  }
}