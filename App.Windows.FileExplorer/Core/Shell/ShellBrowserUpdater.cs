using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ShellDll
{
    internal class ShellBrowserUpdater : NativeWindow
    {
        #region Fields

        private ShellBrowser br;
        private uint notifyId = 0;

        #endregion

        public ShellBrowserUpdater(ShellBrowser br)
        {
            this.br = br;
            CreateHandle(new CreateParams());

            WinAPI.SHChangeNotifyEntry entry = new WinAPI.SHChangeNotifyEntry();
            entry.pIdl = br.DesktopItem.PIDLRel.Ptr;
            entry.Recursively = true;

            notifyId = WinAPI.SHChangeNotifyRegister(
                this.Handle,
                WinAPI.SHCNRF.InterruptLevel | WinAPI.SHCNRF.ShellLevel,
                WinAPI.SHCNE.ALLEVENTS | WinAPI.SHCNE.INTERRUPT,
                WinAPI.WM.SH_NOTIFY,
                1,
                new WinAPI.SHChangeNotifyEntry[] { entry });
        }

        ~ShellBrowserUpdater()
        {
            if (notifyId > 0)
            {
                WinAPI.SHChangeNotifyDeregister(notifyId);
                GC.SuppressFinalize(this);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WinAPI.WM.SH_NOTIFY)
            {
                WinAPI.SHNOTIFYSTRUCT shNotify =
                    (WinAPI.SHNOTIFYSTRUCT)Marshal.PtrToStructure(m.WParam, typeof(WinAPI.SHNOTIFYSTRUCT));

                //Console.Out.WriteLine("Event: {0}", (ShellAPI.SHCNE)m.LParam);
                //if (shNotify.dwItem1 != IntPtr.Zero)
                    //PIDL.Write(shNotify.dwItem1);
                //if (shNotify.dwItem2 != IntPtr.Zero)
                    //PIDL.Write(shNotify.dwItem2);

                switch ((WinAPI.SHCNE)m.LParam)
                {
                    #region File Changes

                    case WinAPI.SHCNE.CREATE:
                        #region Create Item
                        {
                            if (!PIDL.IsEmpty(shNotify.dwItem1))
                            {
                                IntPtr parent, child, relative;
                                PIDL.SplitPidl(shNotify.dwItem1, out parent, out child);

                                PIDL parentPIDL = new PIDL(parent, false);
                                ShellItem parentItem = br.GetShellItem(parentPIDL);
                                if (parentItem != null && parentItem.FilesExpanded && !parentItem.SubFiles.Contains(child))
                                {
                                    WinAPI.SHGetRealIDL(
                                        parentItem.ShellFolder,
                                        child,
                                        out relative);
                                    parentItem.AddItem(new ShellItem(br, parentItem, relative));
                                }
                                    
                                Marshal.FreeCoTaskMem(child);
                                parentPIDL.Free();
                            }
                        }
                        #endregion
                        break;

                    case WinAPI.SHCNE.RENAMEITEM:
                        #region Rename Item
                        {
                            if (!PIDL.IsEmpty(shNotify.dwItem1) && !PIDL.IsEmpty(shNotify.dwItem2))
                            {
                                ShellItem item = br.GetShellItem(new PIDL(shNotify.dwItem1, true));
                                if (item != null)
                                    item.Update(shNotify.dwItem2, ShellItemUpdateType.Renamed);
                            }
                        }
                        #endregion
                        break;

                    case WinAPI.SHCNE.DELETE:
                        #region Delete Item
                        {
                            if (!PIDL.IsEmpty(shNotify.dwItem1))
                            {
                                IntPtr parent, child;
                                PIDL.SplitPidl(shNotify.dwItem1, out parent, out child);

                                PIDL parentPIDL = new PIDL(parent, false);
                                ShellItem parentItem = br.GetShellItem(parentPIDL);
                                if (parentItem != null && parentItem.SubFiles.Contains(child))
                                    parentItem.RemoveItem(parentItem.SubFiles[child]);

                                Marshal.FreeCoTaskMem(child);
                                parentPIDL.Free();
                            }
                        }
                        #endregion
                        break;

                    case WinAPI.SHCNE.UPDATEITEM:
                        #region Update Item
                        {
                            if (!PIDL.IsEmpty(shNotify.dwItem1))
                            {
                                ShellItem item = br.GetShellItem(new PIDL(shNotify.dwItem1, true));
                                if (item != null)
                                {
                                    Console.Out.WriteLine("Item: {0}", item);
                                    item.Update(IntPtr.Zero, ShellItemUpdateType.Updated);
                                    item.Update(IntPtr.Zero, ShellItemUpdateType.IconChange);
                                }
                            }
                        }
                        #endregion
                        break;

                    #endregion

                    #region Folder Changes

                    case WinAPI.SHCNE.MKDIR:                        
                    case WinAPI.SHCNE.DRIVEADD:
                    case WinAPI.SHCNE.DRIVEADDGUI:                        
                        #region Make Directory
                        {
                            if (!PIDL.IsEmpty(shNotify.dwItem1))
                            {
                                IntPtr parent, child, relative;
                                PIDL.SplitPidl(shNotify.dwItem1, out parent, out child);

                                PIDL parentPIDL = new PIDL(parent, false);
                                ShellItem parentItem = br.GetShellItem(parentPIDL);
                                if (parentItem != null && parentItem.FoldersExpanded && !parentItem.SubFolders.Contains(child))
                                {
                                    WinAPI.SHGetRealIDL(
                                        parentItem.ShellFolder,
                                        child,
                                        out relative);

                                    IntPtr shellFolderPtr;
                                    if (parentItem.ShellFolder.BindToObject(
                                                relative,
                                                IntPtr.Zero,
                                                ref WinAPI.IID_IShellFolder,
                                                out shellFolderPtr) == WinAPI.S_OK)
                                    {
                                        parentItem.AddItem(new ShellItem(br, parentItem, relative, shellFolderPtr));
                                    }
                                    else
                                        Marshal.FreeCoTaskMem(relative);
                                }

                                Marshal.FreeCoTaskMem(child);
                                parentPIDL.Free();
                            }
                        }
                        #endregion
                        break;

                    case WinAPI.SHCNE.RENAMEFOLDER:
                        #region Rename Directory
                        {
                            if (!PIDL.IsEmpty(shNotify.dwItem1) && !PIDL.IsEmpty(shNotify.dwItem2))
                            {
                                ShellItem item = br.GetShellItem(new PIDL(shNotify.dwItem1, false));
                                if (item != null)
                                {
                                    //Console.Out.WriteLine("Update: {0}", item);
                                    item.Update(shNotify.dwItem2, ShellItemUpdateType.Renamed);
                                }
                            }
                        }
                        #endregion
                        break;

                    case WinAPI.SHCNE.RMDIR:
                    case WinAPI.SHCNE.DRIVEREMOVED:
                        #region Remove Directory
                        {
                            if (!PIDL.IsEmpty(shNotify.dwItem1))
                            {
                                IntPtr parent, child;
                                PIDL.SplitPidl(shNotify.dwItem1, out parent, out child);

                                PIDL parentPIDL = new PIDL(parent, false);
                                ShellItem parentItem = br.GetShellItem(parentPIDL);
                                if (parentItem != null && parentItem.SubFolders.Contains(child))
                                    parentItem.RemoveItem(parentItem.SubFolders[child]);

                                Marshal.FreeCoTaskMem(child);
                                parentPIDL.Free();
                            }
                        }
                        #endregion
                        break;

                    case WinAPI.SHCNE.UPDATEDIR:
                    case WinAPI.SHCNE.ATTRIBUTES:
                        #region Update Directory
                        {
                            if (!PIDL.IsEmpty(shNotify.dwItem1))
                            {
                                ShellItem item = br.GetShellItem(new PIDL(shNotify.dwItem1, true));
                                if (item != null)
                                {
                                    item.Update(IntPtr.Zero, ShellItemUpdateType.Updated);
                                    item.Update(IntPtr.Zero, ShellItemUpdateType.IconChange);
                                }
                            }
                        }
                        #endregion
                        break;

                    case WinAPI.SHCNE.MEDIAINSERTED:
                    case WinAPI.SHCNE.MEDIAREMOVED:
                        #region Media Change
                        {
                            if (!PIDL.IsEmpty(shNotify.dwItem1))
                            {
                                ShellItem item = br.GetShellItem(new PIDL(shNotify.dwItem1, true));
                                if (item != null)
                                    item.Update(IntPtr.Zero, ShellItemUpdateType.MediaChange);
                            }
                        }
                        #endregion
                        break;

                    #endregion

                    #region Other Changes

                    case WinAPI.SHCNE.ASSOCCHANGED:
                        #region Update Images
                        {
                            
                        }
                        #endregion
                        break;

                    case WinAPI.SHCNE.NETSHARE:
                    case WinAPI.SHCNE.NETUNSHARE:
                        break;

                    case WinAPI.SHCNE.UPDATEIMAGE:
                        UpdateRecycleBin();
                        break;

                    #endregion
                }
            }

            base.WndProc(ref m);
        }

        private void UpdateRecycleBin()
        {
            ShellItem recycleBin = br.DesktopItem["Recycle Bin"];
            if (recycleBin != null)
                recycleBin.Update(IntPtr.Zero, ShellItemUpdateType.IconChange);
        }
    }
}
