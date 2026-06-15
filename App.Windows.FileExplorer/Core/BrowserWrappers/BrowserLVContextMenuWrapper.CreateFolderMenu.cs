using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserLVContextMenuWrapper
  {
    private void CreateFolderMenu(Point ptInvoke, MouseEventArgs e)
    {
        ShellItem currentItem = browser.SelectedItem;

        foreach (ListViewItem item in browser.FileView.SelectedItems)
            item.Selected = false;

        IntPtr contextMenu = IntPtr.Zero, viewSubMenu = IntPtr.Zero;
        IntPtr newContextMenuPtr = IntPtr.Zero, newContextMenuPtr2 = IntPtr.Zero, newContextMenuPtr3 = IntPtr.Zero;
        newSubmenuPtr = IntPtr.Zero;

        try
        {
            contextMenu = WinAPI.CreatePopupMenu();
            viewSubMenu = WinAPI.CreatePopupMenu();

            WinAPI.MENUITEMINFO itemInfo = new WinAPI.MENUITEMINFO("View");
            itemInfo.cbSize = WinAPI.cbMenuItemInfo;
            itemInfo.fMask = WinAPI.MIIM.SUBMENU | WinAPI.MIIM.STRING;
            itemInfo.hSubMenu = viewSubMenu;
            WinAPI.InsertMenuItem(contextMenu, 0, true, ref itemInfo);

            WinAPI.MFT rCheck = WinAPI.MFT.RADIOCHECK | WinAPI.MFT.CHECKED;
            WinAPI.AppendMenu(viewSubMenu, (browser.CurrentViewPlugin == null && browser.FileView.View == View.Tile ? rCheck : 0), (uint)CMD_CUSTOM.Tiles, "Tiles");
            WinAPI.AppendMenu(viewSubMenu, (browser.CurrentViewPlugin == null && browser.FileView.View == View.LargeIcon ? rCheck : 0), (uint)CMD_CUSTOM.Icons, "Icons");
            WinAPI.AppendMenu(viewSubMenu, (browser.CurrentViewPlugin == null && browser.FileView.View == View.List ? rCheck : 0), (uint)CMD_CUSTOM.List, "List");
            WinAPI.AppendMenu(viewSubMenu, (browser.CurrentViewPlugin == null && browser.FileView.View == View.Details ? rCheck : 0), (uint)CMD_CUSTOM.Details, "Details");

            for (int i = 0; i < pluginWrapper.ViewPlugins.Count; i++)
            {
                WinAPI.AppendMenu(viewSubMenu,
                    (IViewPlugin.Equals(browser.CurrentViewPlugin, pluginWrapper.ViewPlugins[i]) ? rCheck : 0), 
                        (uint)CMD_CUSTOM.SpecialView + (uint)i, ((IViewPlugin)pluginWrapper.ViewPlugins[i]).ViewName);
            }


            DragDropEffects effects = ShellHelper.CanDropClipboard(currentItem);
            bool canPaste = (effects & DragDropEffects.Copy) != 0 || (effects & DragDropEffects.Move) != 0;
            bool canPasteShortCut = (effects & DragDropEffects.Link) != 0;

            WinAPI.AppendMenu(contextMenu, WinAPI.MFT.SEPARATOR, 0, string.Empty);
            WinAPI.AppendMenu(contextMenu, canPaste ? 0 : WinAPI.MFT.GRAYED, (int)CMD_CUSTOM.Paste, "Paste");
            WinAPI.AppendMenu(contextMenu, canPasteShortCut ? 0 : WinAPI.MFT.GRAYED, (int)CMD_CUSTOM.Paste_ShortCut, "Paste Shortcut");


            if (browser.SelectedItem.IsFileSystem &&
                ContextMenuHelper.GetNewContextMenu(browser.SelectedItem, out newContextMenuPtr, out newContextMenu))
            {
                WinAPI.AppendMenu(contextMenu, WinAPI.MFT.SEPARATOR, 0, string.Empty);
                newContextMenu.QueryContextMenu(
                    contextMenu,
                    5,
                    WinAPI.CMD_FIRST,
                    WinAPI.CMD_LAST,
                    WinAPI.CMF.NORMAL);

                newSubmenuPtr = WinAPI.GetSubMenu(contextMenu, 5);

                Marshal.QueryInterface(newContextMenuPtr, ref WinAPI.IID_IContextMenu2, out newContextMenuPtr2);
                Marshal.QueryInterface(newContextMenuPtr, ref WinAPI.IID_IContextMenu3, out newContextMenuPtr3);

                try
                {
                    newContextMenu2 =
                        (IContextMenu2)Marshal.GetTypedObjectForIUnknown(newContextMenuPtr2, typeof(IContextMenu2));

                    newContextMenu3 =
                        (IContextMenu3)Marshal.GetTypedObjectForIUnknown(newContextMenuPtr3, typeof(IContextMenu3));
                }
                catch (Exception) { }
            }


            if (!browser.SelectedItem.Equals(browser.ShellBrowser.DesktopItem))
            {
                WinAPI.AppendMenu(contextMenu, WinAPI.MFT.SEPARATOR, 0, string.Empty);
                WinAPI.AppendMenu(contextMenu, 0, (int)CMD_CUSTOM.Properties, "Properties");
            }

            CMD_CUSTOM selected = (CMD_CUSTOM)WinAPI.TrackPopupMenuEx(
                                contextMenu,
                                WinAPI.TPM.RETURNCMD,
                                ptInvoke.X,
                                ptInvoke.Y,
                                this.Handle,
                                IntPtr.Zero);

            if ((int)selected >= WinAPI.CMD_FIRST)
            {
                switch (selected)
                {
                    case CMD_CUSTOM.Tiles:
                        browser.FileView.View = View.Tile;
                        browser.ResetSpecialView();
                        provider.ReleaseStorage();
                        provider.ReleaseStream();
                        break;
                    case CMD_CUSTOM.Icons:
                        browser.FileView.View = View.LargeIcon;
                        browser.ResetSpecialView();
                        provider.ReleaseStorage();
                        provider.ReleaseStream();
                        break;
                    case CMD_CUSTOM.List:
                        browser.FileView.View = View.List;
                        browser.ResetSpecialView();
                        provider.ReleaseStorage();
                        provider.ReleaseStream();
                        break;
                    case CMD_CUSTOM.Details:
                        browser.FileView.SuspendHeaderContextMenu = true;
                        browser.FileView.View = View.Details;
                        browser.ResetSpecialView();
                        provider.ReleaseStorage();
                        provider.ReleaseStream();
                        break;

                    case CMD_CUSTOM.Properties:
                        ContextMenuHelper.InvokeCommand(
                            browser.SelectedItem.ParentItem,
                            new IntPtr[] { browser.SelectedItem.PIDLRel.Ptr },
                            "properties",
                            ptInvoke);
                        break;

                    case CMD_CUSTOM.Paste:
                        ContextMenuHelper.InvokeCommand(
                            browser.SelectedItem.ParentItem,
                            new IntPtr[] { browser.SelectedItem.PIDLRel.Ptr },
                            "paste",
                            ptInvoke);
                        break;

                    case CMD_CUSTOM.Paste_ShortCut:
                        ContextMenuHelper.InvokeCommand(
                            browser.SelectedItem.ParentItem,
                            new IntPtr[] { browser.SelectedItem.PIDLRel.Ptr },
                            "pastelink",
                            ptInvoke);
                        break;


                    default:
                        if ((uint)selected <= WinAPI.CMD_LAST)
                        {
                            lock (browser.ShellBrowser)
                            {
                                browser.NewItemCreated = true;
                            }

                            ContextMenuHelper.InvokeCommand(
                                newContextMenu,
                                (uint)selected - WinAPI.CMD_FIRST,
                                ShellItem.GetRealPath(browser.SelectedItem),
                                ptInvoke);
                        }
                        else
                        {
                            int index = (int)selected - (int)CMD_CUSTOM.SpecialView;

                            if (browser.FileView.Alignment != ListViewAlignment.Left)
                                browser.FileView.Alignment = ListViewAlignment.Left;
                            
                            browser.FileView.View = View.LargeIcon;
                            browser.CurrentViewPlugin = pluginWrapper.ViewPlugins[index] as IViewPlugin;
                            browser.CurrentViewPlugin.ViewControl.Dock = DockStyle.Fill;
                        }
                        break;
                }
            }
        }
        catch (Exception) { }
        finally
        {
            if (newContextMenu != null)
            {
                Marshal.ReleaseComObject(newContextMenu);
                newContextMenu = null;
            }

            if (newContextMenu2 != null)
            {
                Marshal.ReleaseComObject(newContextMenu2);
                newContextMenu2 = null;
            }

            if (newContextMenu3 != null)
            {
                Marshal.ReleaseComObject(newContextMenu3);
                newContextMenu3 = null;
            }

            if (contextMenu != null)
                WinAPI.DestroyMenu(contextMenu);

            if (viewSubMenu != null)
                WinAPI.DestroyMenu(viewSubMenu);

            if (newContextMenuPtr != IntPtr.Zero)
                Marshal.Release(newContextMenuPtr);

            if (newContextMenuPtr2 != IntPtr.Zero)
                Marshal.Release(newContextMenuPtr2);

            if (newContextMenuPtr3 != IntPtr.Zero)
                Marshal.Release(newContextMenuPtr3);
            
            newSubmenuPtr = IntPtr.Zero;
        }
    }


  }
}