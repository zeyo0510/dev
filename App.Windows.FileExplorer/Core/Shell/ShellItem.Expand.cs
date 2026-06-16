using System;
using System.Runtime.InteropServices;

namespace ShellDll
{
  partial class ShellItem
  {
    internal bool Expand(bool expandFiles, bool expandFolders, IntPtr winHandle)
    {
        if (((expandFiles && !FilesExpanded) || !expandFiles) &&
            ((expandFolders && !FoldersExpanded) || !expandFolders) &&
            (expandFiles || expandFolders) && ShellFolder != null && !disposed)
        {
            IntPtr fileEnumPtr = IntPtr.Zero, folderEnumPtr = IntPtr.Zero;
            IEnumIDList fileEnum = null, folderEnum = null;
            IntPtr pidlSubItem;
            int celtFetched;

            WinAPI.SHCONTF fileFlag =
                    WinAPI.SHCONTF.NONFOLDERS |
                    WinAPI.SHCONTF.INCLUDEHIDDEN;

            WinAPI.SHCONTF folderFlag =
                    WinAPI.SHCONTF.FOLDERS |
                    WinAPI.SHCONTF.INCLUDEHIDDEN;

            try
            {
                if (expandFiles)
                {
                    if (this.Equals(Browser.DesktopItem) || ParentItem.Equals(Browser.DesktopItem))
                    {
                        if (ShellFolder.EnumObjects(
                                winHandle,
                                fileFlag,
                                out fileEnumPtr) == WinAPI.S_OK)
                        {
                            fileEnum = (IEnumIDList)Marshal.GetTypedObjectForIUnknown(fileEnumPtr, typeof(IEnumIDList));
                            WinAPI.SFGAO attribs = WinAPI.SFGAO.FOLDER;
                            while (fileEnum.Next(1, out pidlSubItem, out celtFetched) == WinAPI.S_OK && celtFetched == 1)
                            {
                                ShellFolder.GetAttributesOf(1, new IntPtr[] { pidlSubItem }, ref attribs);

                                if ((attribs & WinAPI.SFGAO.FOLDER) == 0)
                                {
                                    ShellItem newItem = new ShellItem(Browser, this, pidlSubItem);

                                    if (!SubFolders.Contains(newItem.Text))
                                        SubFiles.Add(newItem);
                                }
                                else
                                    Marshal.FreeCoTaskMem(pidlSubItem);
                            }

                            SubFiles.Sort();
                            FilesExpanded = true;
                        }
                    }
                    else
                    {
                        if (ShellFolder.EnumObjects(
                                winHandle,
                                fileFlag,
                                out fileEnumPtr) == WinAPI.S_OK)
                        {
                            fileEnum = (IEnumIDList)Marshal.GetTypedObjectForIUnknown(fileEnumPtr, typeof(IEnumIDList));
                            while (fileEnum.Next(1, out pidlSubItem, out celtFetched) == WinAPI.S_OK && celtFetched == 1)
                            {
                                ShellItem newItem = new ShellItem(Browser, this, pidlSubItem);
                                SubFiles.Add(newItem);
                            }

                            SubFiles.Sort();
                            FilesExpanded = true;
                        }
                    }
                }

                if (expandFolders)
                {
                    if (ShellFolder.EnumObjects(
                                winHandle,
                                folderFlag,
                                out folderEnumPtr) == WinAPI.S_OK)
                    {
                        folderEnum = (IEnumIDList)Marshal.GetTypedObjectForIUnknown(folderEnumPtr, typeof(IEnumIDList));
                        while (folderEnum.Next(1, out pidlSubItem, out celtFetched) == WinAPI.S_OK && celtFetched == 1)
                        {
                            IntPtr shellFolderPtr;
                            if (ShellFolder.BindToObject(
                                        pidlSubItem,
                                        IntPtr.Zero,
                                        ref WinAPI.IID_IShellFolder,
                                        out shellFolderPtr) == WinAPI.S_OK)
                            {
                                ShellItem newItem = new ShellItem(
                                    Browser,
                                    this,
                                    pidlSubItem,
                                    shellFolderPtr);
                                SubFolders.Add(newItem);
                            }
                        }

                        SubFolders.Sort();
                        FoldersExpanded = true;
                    }
                }

            }
            catch (Exception) { }
            finally
            {
                if (folderEnum != null)
                {
                    Marshal.ReleaseComObject(folderEnum);
                    Marshal.Release(folderEnumPtr);
                }

                if (fileEnum != null)
                {
                    Marshal.ReleaseComObject(fileEnum);
                    Marshal.Release(fileEnumPtr);
                }
            }
        }

        return ((expandFiles == FilesExpanded || !expandFiles) && (expandFolders == FoldersExpanded || !expandFolders));
    }
  
  }
}