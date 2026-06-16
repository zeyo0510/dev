using System;
using System.Runtime.InteropServices;

namespace ShellDll
{
  partial class ShellItem
  {
    internal bool Expand(bool expandFiles, bool expandFolders, IntPtr winHandle)
    {
        if (((expandFiles && !filesExpanded) || !expandFiles) &&
            ((expandFolders && !foldersExpanded) || !expandFolders) &&
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
                    if (this.Equals(browser.DesktopItem) || parentItem.Equals(browser.DesktopItem))
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
                                    ShellItem newItem = new ShellItem(browser, this, pidlSubItem);

                                    if (!subFolders.Contains(newItem.Text))
                                        subFiles.Add(newItem);
                                }
                                else
                                    Marshal.FreeCoTaskMem(pidlSubItem);
                            }

                            subFiles.Sort();
                            filesExpanded = true;
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
                                ShellItem newItem = new ShellItem(browser, this, pidlSubItem);
                                subFiles.Add(newItem);
                            }

                            subFiles.Sort();
                            filesExpanded = true;
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
                                    browser,
                                    this,
                                    pidlSubItem,
                                    shellFolderPtr);
                                subFolders.Add(newItem);
                            }
                        }

                        subFolders.Sort();
                        foldersExpanded = true;
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

        return ((expandFiles == filesExpanded || !expandFiles) && (expandFolders == foldersExpanded || !expandFolders));
    }
  
  }
}