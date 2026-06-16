using System;
using System.Runtime.InteropServices;

namespace ShellDll
{
  partial class ShellItem
  {
    internal void Update(bool updateFiles, bool updateFolders)
    {
        if (Browser.UpdateCondition.ContinueUpdate && (updateFiles || updateFolders) && ShellFolder != null && !disposed)
        {
            lock (Browser)
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
                fileExists = new bool[SubFiles.Count];

                bool[] folderExists;
                folderExists = new bool[SubFolders.Count];

                int index;

                try
                {
                    if (Browser.UpdateCondition.ContinueUpdate && updateFiles)
                    {
                        ShellItemCollection add = new ShellItemCollection(this);
                        ShellItemCollection remove = new ShellItemCollection(this);

                        bool fileEnumCompleted = false;

                        if (this.Equals(Browser.DesktopItem) || ParentItem.Equals(Browser.DesktopItem))
                        {
                            if (ShellFolder.EnumObjects(
                                    IntPtr.Zero,
                                    fileFlag,
                                    out fileEnumPtr) == WinAPI.S_OK)
                            {
                                fileEnum = (IEnumIDList)Marshal.GetTypedObjectForIUnknown(fileEnumPtr, typeof(IEnumIDList));
                                WinAPI.SFGAO attribs = WinAPI.SFGAO.FOLDER;
                                while (Browser.UpdateCondition.ContinueUpdate &&
                                       fileEnum.Next(1, out pidlSubItem, out celtFetched) == WinAPI.S_OK && celtFetched == 1)
                                {
                                    ShellFolder.GetAttributesOf(1, new IntPtr[] { pidlSubItem }, ref attribs);

                                    if ((attribs & WinAPI.SFGAO.FOLDER) == 0)
                                    {
                                        if ((index = SubFiles.IndexOf(pidlSubItem)) == -1)
                                        {
                                            ShellItem newItem = new ShellItem(Browser, this, pidlSubItem);

                                            if (!SubFolders.Contains(newItem.Text))
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
                                while (Browser.UpdateCondition.ContinueUpdate &&
                                       fileEnum.Next(1, out pidlSubItem, out celtFetched) == WinAPI.S_OK && celtFetched == 1)
                                {
                                    if ((index = SubFiles.IndexOf(pidlSubItem)) == -1)
                                    {
                                        add.Add(new ShellItem(Browser, this, pidlSubItem));
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

                        for (int i = 0; fileEnumCompleted && Browser.UpdateCondition.ContinueUpdate && i < fileExists.Length; i++)
                        {
                            if (!fileExists[i] && SubFiles[i] != null)
                            {
                                remove.Add(SubFiles[i]);
                            }
                        }

                        if (fileEnumCompleted && Browser.UpdateCondition.ContinueUpdate)
                        {
                            int newIndex;
                            foreach (ShellItem oldItem in remove)
                            {
                                if ((newIndex = add.IndexOf(oldItem.Text)) > -1)
                                {
                                    ShellItem newItem = add[newIndex];
                                    add.Remove(newItem);

                                    oldItem.PIDLRel.Free();
                                    oldItem.PIDLRel = new PIDL(newItem.PIDLRel.Ptr, true);

                                    oldItem.shellFolder = newItem.shellFolder;
                                    oldItem.shellFolderPtr = newItem.shellFolderPtr;

                                    ((IDisposable)newItem).Dispose();

                                    Browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(oldItem, oldItem, ShellItemUpdateType.Updated));
                                }
                                else
                                {
                                    SubFiles.Remove(oldItem);
                                    Browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(oldItem, null, ShellItemUpdateType.Deleted));
                                    ((IDisposable)oldItem).Dispose();
                                }
                            }

                            foreach (ShellItem newItem in add)
                            {
                                SubFiles.Add(newItem);
                                Browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(null, newItem, ShellItemUpdateType.Created));
                            }

                            SubFiles.Capacity = SubFiles.Count;
                            SubFiles.Sort();

                            FilesExpanded = true;
                        }
                    }

                    if (Browser.UpdateCondition.ContinueUpdate && updateFolders)
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
                            while (Browser.UpdateCondition.ContinueUpdate &&
                                   folderEnum.Next(1, out pidlSubItem, out celtFetched) == WinAPI.S_OK && celtFetched == 1)
                            {
                                if ((index = SubFolders.IndexOf(pidlSubItem)) == -1)
                                {
                                    IntPtr shellFolderPtr;
                                    if (ShellFolder.BindToObject(
                                                pidlSubItem,
                                                IntPtr.Zero,
                                                ref WinAPI.IID_IShellFolder,
                                                out shellFolderPtr) == WinAPI.S_OK)
                                    {
                                        add.Add(new ShellItem(
                                            Browser,
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

                        for (int i = 0; folderEnumCompleted && Browser.UpdateCondition.ContinueUpdate && i < folderExists.Length; i++)
                        {
                            if (!folderExists[i] && SubFolders[i] != null)
                            {
                                remove.Add(SubFolders[i]);
                            }
                        }

                        if (folderEnumCompleted && Browser.UpdateCondition.ContinueUpdate)
                        {
                            int newIndex;
                            foreach (ShellItem oldItem in remove)
                            {
                                if ((newIndex = add.IndexOf(oldItem.Text)) > -1)
                                {
                                    ShellItem newItem = add[newIndex];
                                    add.Remove(newItem);

                                    oldItem.PIDLRel.Free();
                                    oldItem.PIDLRel = new PIDL(newItem.PIDLRel, true);

                                    Marshal.ReleaseComObject(oldItem.shellFolder);
                                    Marshal.Release(oldItem.shellFolderPtr);

                                    oldItem.shellFolder = newItem.shellFolder;
                                    oldItem.shellFolderPtr = newItem.shellFolderPtr;

                                    newItem.shellFolder = null;
                                    newItem.shellFolderPtr = IntPtr.Zero;
                                    ((IDisposable)newItem).Dispose();

                                    Browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(oldItem, oldItem, ShellItemUpdateType.Updated));
                                }
                                else
                                {
                                    SubFolders.Remove(oldItem);
                                    Browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(oldItem, null, ShellItemUpdateType.Deleted));
                                    ((IDisposable)oldItem).Dispose();
                                }
                            }

                            foreach (ShellItem newItem in add)
                            {
                                SubFolders.Add(newItem);

                                Browser.OnShellItemUpdate(this, new ShellItemUpdateEventArgs(null, newItem, ShellItemUpdateType.Created));
                            }

                            SubFolders.Capacity = SubFolders.Count;
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

                        if (!(Type == Browser.SystemFolderName && string.Compare(Text, "Control Panel", true) == 0))
                            Marshal.Release(fileEnumPtr);
                    }
                }
            }
        }
    }
  
    internal void Update(IntPtr newPidlFull, ShellItemUpdateType changeType)
    {
        Browser.UpdateCondition.ContinueUpdate = false;

        lock (Browser)
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
                    PIDLRel.Free();

                    shellFolderPtr = newShellFolderPtr;
                    shellFolder = (IShellFolder)Marshal.GetTypedObjectForIUnknown(shellFolderPtr, typeof(IShellFolder));
                    PIDLRel = new PIDL(newPidlRel, false);

                    foreach (ShellItem child in SubFolders)
                        UpdateShellFolders(child);
                }
                else
                {
                    PIDLRel.Free();
                    PIDLRel = new PIDL(newPidlRel, false);
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