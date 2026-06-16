using System;
using System.Runtime.InteropServices;

namespace ShellDll
{
  partial class ShellItem
  {
    internal void Update(bool updateFiles, bool updateFolders)
    {
        if (browser.UpdateCondition.ContinueUpdate && (updateFiles || updateFolders) && ShellFolder != null && !disposed)
        {
            lock (browser)
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

                bool[] fileExists;
                fileExists = new bool[subFiles.Count];

                bool[] folderExists;
                folderExists = new bool[subFolders.Count];

                int index;

                try
                {
                    if (browser.UpdateCondition.ContinueUpdate && updateFiles)
                    {
                        ShellItemCollection add = new ShellItemCollection(this);
                        ShellItemCollection remove = new ShellItemCollection(this);

                        bool fileEnumCompleted = false;

                        if (this.Equals(browser.DesktopItem) || parentItem.Equals(browser.DesktopItem))
                        {
                            if (ShellFolder.EnumObjects(
                                    IntPtr.Zero,
                                    fileFlag,
                                    out fileEnumPtr) == WinAPI.S_OK)
                            {
                                fileEnum = (IEnumIDList)Marshal.GetTypedObjectForIUnknown(fileEnumPtr, typeof(IEnumIDList));
                                WinAPI.SFGAO attribs = WinAPI.SFGAO.FOLDER;
                                while (browser.UpdateCondition.ContinueUpdate &&
                                       fileEnum.Next(1, out pidlSubItem, out celtFetched) == WinAPI.S_OK && celtFetched == 1)
                                {
                                    ShellFolder.GetAttributesOf(1, new IntPtr[] { pidlSubItem }, ref attribs);

                                    if ((attribs & WinAPI.SFGAO.FOLDER) == 0)
                                    {
                                        if ((index = subFiles.IndexOf(pidlSubItem)) == -1)
                                        {
                                            ShellItem newItem = new ShellItem(browser, this, pidlSubItem);

                                            if (!subFolders.Contains(newItem.Text))
                                            {
                                                add.Add(newItem);
                                            }
                                        }
                                        else if (index < fileExists.Length)
                                        {
                                            fileExists[index] = true;
                                            Marshal.FreeCoTaskMem(pidlSubItem);
                                        }
                                    }
                                    else
                                        Marshal.FreeCoTaskMem(pidlSubItem);
                                }

                                fileEnumCompleted = true;
                            }
                                
                        }
                        else
                        {
                            if (ShellFolder.EnumObjects(
                                    IntPtr.Zero,
                                    fileFlag,
                                    out fileEnumPtr) == WinAPI.S_OK)
                            {
                                fileEnum = (IEnumIDList)Marshal.GetTypedObjectForIUnknown(fileEnumPtr, typeof(IEnumIDList));
                                while (browser.UpdateCondition.ContinueUpdate &&
                                       fileEnum.Next(1, out pidlSubItem, out celtFetched) == WinAPI.S_OK && celtFetched == 1)
                                {
                                    if ((index = subFiles.IndexOf(pidlSubItem)) == -1)
                                    {
                                        add.Add(new ShellItem(browser, this, pidlSubItem));
                                    }
                                    else if (index < fileExists.Length)
                                    {
                                        fileExists[index] = true;
                                        Marshal.FreeCoTaskMem(pidlSubItem);
                                    }
                                }

                                fileEnumCompleted = true;
                            }
                        }

                        for (int i = 0; fileEnumCompleted && browser.UpdateCondition.ContinueUpdate && i < fileExists.Length; i++)
                        {
                            if (!fileExists[i] && subFiles[i] != null)
                            {
                                remove.Add(subFiles[i]);
                            }
                        }

                        if (fileEnumCompleted && browser.UpdateCondition.ContinueUpdate)
                        {
                            int newIndex;
                            foreach (ShellItem oldItem in remove)
                            {
                                if ((newIndex = add.IndexOf(oldItem.Text)) > -1)
                                {
                                    ShellItem newItem = add[newIndex];
                                    add.Remove(newItem);

                                    oldItem.pidlRel.Free();
                                    oldItem.pidlRel = new PIDL(newItem.pidlRel.Ptr, true);

                                    oldItem.shellFolder = newItem.shellFolder;
                                    oldItem.shellFolderPtr = newItem.shellFolderPtr;

                                    ((IDisposable)newItem).Dispose();

                                    browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(oldItem, oldItem, ShellItemUpdateType.Updated));
                                }
                                else
                                {
                                    subFiles.Remove(oldItem);
                                    browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(oldItem, null, ShellItemUpdateType.Deleted));
                                    ((IDisposable)oldItem).Dispose();
                                }
                            }

                            foreach (ShellItem newItem in add)
                            {
                                subFiles.Add(newItem);
                                browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(null, newItem, ShellItemUpdateType.Created));
                            }

                            subFiles.Capacity = subFiles.Count;
                            subFiles.Sort();

                            filesExpanded = true;
                        }
                    }

                    if (browser.UpdateCondition.ContinueUpdate && updateFolders)
                    {
                        ShellItemCollection add = new ShellItemCollection(this);
                        ShellItemCollection remove = new ShellItemCollection(this);

                        bool folderEnumCompleted = false;

                        if (ShellFolder.EnumObjects(
                                    IntPtr.Zero,
                                    folderFlag,
                                    out folderEnumPtr) == WinAPI.S_OK)
                        {
                            folderEnum = (IEnumIDList)Marshal.GetTypedObjectForIUnknown(folderEnumPtr, typeof(IEnumIDList));
                            while (browser.UpdateCondition.ContinueUpdate &&
                                   folderEnum.Next(1, out pidlSubItem, out celtFetched) == WinAPI.S_OK && celtFetched == 1)
                            {
                                if ((index = subFolders.IndexOf(pidlSubItem)) == -1)
                                {
                                    IntPtr shellFolderPtr;
                                    if (ShellFolder.BindToObject(
                                                pidlSubItem,
                                                IntPtr.Zero,
                                                ref WinAPI.IID_IShellFolder,
                                                out shellFolderPtr) == WinAPI.S_OK)
                                    {
                                        add.Add(new ShellItem(
                                            browser,
                                            this,
                                            pidlSubItem,
                                            shellFolderPtr));
                                    }
                                }
                                else if (index < folderExists.Length)
                                {
                                    folderExists[index] = true;
                                    Marshal.FreeCoTaskMem(pidlSubItem);
                                }
                            }

                            folderEnumCompleted = true;
                        }

                        for (int i = 0; folderEnumCompleted && browser.UpdateCondition.ContinueUpdate && i < folderExists.Length; i++)
                        {
                            if (!folderExists[i] && subFolders[i] != null)
                            {
                                remove.Add(subFolders[i]);
                            }
                        }

                        if (folderEnumCompleted && browser.UpdateCondition.ContinueUpdate)
                        {
                            int newIndex;
                            foreach (ShellItem oldItem in remove)
                            {
                                if ((newIndex = add.IndexOf(oldItem.Text)) > -1)
                                {
                                    ShellItem newItem = add[newIndex];
                                    add.Remove(newItem);

                                    oldItem.pidlRel.Free();
                                    oldItem.pidlRel = new PIDL(newItem.pidlRel, true);

                                    Marshal.ReleaseComObject(oldItem.shellFolder);
                                    Marshal.Release(oldItem.shellFolderPtr);

                                    oldItem.shellFolder = newItem.shellFolder;
                                    oldItem.shellFolderPtr = newItem.shellFolderPtr;

                                    newItem.shellFolder = null;
                                    newItem.shellFolderPtr = IntPtr.Zero;
                                    ((IDisposable)newItem).Dispose();

                                    browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(oldItem, oldItem, ShellItemUpdateType.Updated));
                                }
                                else
                                {
                                    subFolders.Remove(oldItem);
                                    browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(oldItem, null, ShellItemUpdateType.Deleted));
                                    ((IDisposable)oldItem).Dispose();
                                }
                            }

                            foreach (ShellItem newItem in add)
                            {
                                subFolders.Add(newItem);

                                browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(null, newItem, ShellItemUpdateType.Created));
                            }

                            subFolders.Capacity = subFolders.Count;
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

                        if (!(type == browser.SystemFolderName && string.Compare(text, "Control Panel", true) == 0))
                            Marshal.Release(fileEnumPtr);
                    }
                }
            }
        }
    }
  
    internal void Update(IntPtr newPidlFull, ShellItemUpdateType changeType)
    {
        browser.UpdateCondition.ContinueUpdate = false;

        lock (browser)
        {
            if (newPidlFull != IntPtr.Zero)
            {
                IntPtr tempPidl = PIDL.ILClone(PIDL.ILFindLastID(newPidlFull)), newPidlRel, newShellFolderPtr;
                WinAPI.SHGetRealIDL(ParentItem.ShellFolder, tempPidl, out newPidlRel);

                if (IsFolder && ParentItem.ShellFolder.BindToObject(
                                    newPidlRel,
                                    IntPtr.Zero,
                                    ref WinAPI.IID_IShellFolder,
                                    out newShellFolderPtr) == WinAPI.S_OK)
                {
                    Marshal.ReleaseComObject(shellFolder);
                    Marshal.Release(shellFolderPtr);
                    pidlRel.Free();

                    shellFolderPtr = newShellFolderPtr;
                    shellFolder = (IShellFolder)Marshal.GetTypedObjectForIUnknown(shellFolderPtr, typeof(IShellFolder));
                    pidlRel = new PIDL(newPidlRel, false);

                    foreach (ShellItem child in SubFolders)
                        UpdateShellFolders(child);
                }
                else
                {
                    pidlRel.Free();
                    pidlRel = new PIDL(newPidlRel, false);
                }

                Marshal.FreeCoTaskMem(tempPidl);
                Marshal.FreeCoTaskMem(newPidlFull);
            }

            switch (changeType)
            {
                case ShellItemUpdateType.Renamed:
                    SetText(this);
                    SetPath(this);
                    break;

                case ShellItemUpdateType.Updated:
                    if (IsFolder)
                        SetAttributesFolder(this);
                    else
                        SetAttributesFile(this);
                    break;

                case ShellItemUpdateType.MediaChange:
                    SetInfo(this);
                    Clear(true, true);
                    break;

                case ShellItemUpdateType.IconChange:
                    SetInfo(this);
                    break;
            }
        }

        Browser.OnShellItemUpdate(ParentItem, new ShellItemUpdateEventArgs(this, this, changeType));
    }
  }
}