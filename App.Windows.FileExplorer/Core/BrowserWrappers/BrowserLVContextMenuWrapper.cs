using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  internal class BrowserLVContextMenuWrapper : NativeWindow
  {
    private Browser br;

    private BrowserPluginWrapper pluginWrapper;
    private StreamStorageProvider provider;

    private BackgroundWorker viewPluginWorker;
    private Timer viewPluginTimer;
    private delegate void ViewPluginFileChange(IFileInfoProvider provider, ShellItem item);
    private delegate void ViewPluginFolderChange(IDirInfoProvider provider, ShellItem item);

    private delegate void ToolTipDelegate(ListViewItem listItem);
    private ToolTipDelegate toolTipDelegate;

    private bool suspendContextMenu;

    private IContextMenu iContextMenu, newContextMenu;
    private IContextMenu2 iContextMenu2, newContextMenu2;
    private IContextMenu3 iContextMenu3, newContextMenu3;

    private IntPtr newSubmenuPtr;

    private bool contextMenuVisible;

    private enum CMD_CUSTOM
    {
      Tiles = (int)WinAPI.CMD_LAST + 1,
      Icons,
      List,
      Details,
      Properties,
      Paste,
      Paste_ShortCut,
      SpecialView
    }


    public BrowserLVContextMenuWrapper(Browser br, BrowserPluginWrapper pluginWrapper)
    {
      this.br = br;
      this.pluginWrapper = pluginWrapper;

      provider = new StreamStorageProvider(FileAccess.ReadWrite);
      viewPluginWorker = new BackgroundWorker();
      viewPluginWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
      viewPluginTimer = new System.Windows.Forms.Timer();
      viewPluginTimer.Tick += new EventHandler(viewPluginTimer_Tick);
      viewPluginTimer.Interval = 300;

      br.FileView.ShowItemToolTips = true;
      toolTipDelegate = new ToolTipDelegate(SetToolTip);

      br.SelectedFolderChanged += new SelectedFolderChangedEventHandler(br_SelectedFolderChanged);

      br.FileView.LabelEdit = true;
      br.FileView.MouseUp += new MouseEventHandler(FileView_MouseUp);
      br.FileView.ItemActivate += new EventHandler(FileView_ItemActivate);
      br.FileView.AfterLabelEdit += new LabelEditEventHandler(FileView_AfterLabelEdit);
      br.FileView.BeforeLabelEdit += new LabelEditEventHandler(FileView_BeforeLabelEdit);
      br.FileView.KeyDown += new KeyEventHandler(FileView_KeyDown);
      br.FileView.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(FileView_ItemSelectionChanged);
      br.FileView.ItemMouseHover += new ListViewItemMouseHoverEventHandler(FileView_ItemMouseHover);

      this.CreateHandle(new CreateParams());
    }

    public bool SuspendContextMenu
    {
      get { return suspendContextMenu; }
      set { suspendContextMenu = value; }
    }

    protected override void WndProc(ref Message m)
    {
        if (iContextMenu != null &&
            m.Msg == (int)WinAPI.WM.MENUSELECT &&
            ((int)ShellHelper.HiWord(m.WParam) & (int)WinAPI.MFT.SEPARATOR) == 0 &&
            ((int)ShellHelper.HiWord(m.WParam) & (int)WinAPI.MFT.POPUP) == 0)
        {
            string info = string.Empty;
            info = ContextMenuHelper.GetCommandString(
                iContextMenu,
                ShellHelper.LoWord(m.WParam) - WinAPI.CMD_FIRST, 
                false);
            br.OnContextMenuMouseHover(new ContextMenuMouseHoverEventArgs(info.ToString()));
        }

        if (iContextMenu2 != null &&
            (m.Msg == (int)WinAPI.WM.INITMENUPOPUP ||
             m.Msg == (int)WinAPI.WM.MEASUREITEM ||
             m.Msg == (int)WinAPI.WM.DRAWITEM))
        {
            if (iContextMenu2.HandleMenuMsg(
                (uint)m.Msg, m.WParam, m.LParam) == WinAPI.S_OK)
                return;
        }

        if (newContextMenu2 != null &&
            ((m.Msg == (int)WinAPI.WM.INITMENUPOPUP && m.WParam == newSubmenuPtr) ||
             m.Msg == (int)WinAPI.WM.MEASUREITEM ||
             m.Msg == (int)WinAPI.WM.DRAWITEM))
        {
            if (newContextMenu2.HandleMenuMsg(
                (uint)m.Msg, m.WParam, m.LParam) == WinAPI.S_OK)
                return;
        }

        if (iContextMenu3 != null &&
            m.Msg == (int)WinAPI.WM.MENUCHAR)
        {
            if (iContextMenu3.HandleMenuMsg2(
                (uint)m.Msg, m.WParam, m.LParam, IntPtr.Zero) == WinAPI.S_OK)
                return;
        }

        if (newContextMenu3 != null &&
            m.Msg == (int)WinAPI.WM.MENUCHAR)
        {
            if (newContextMenu3.HandleMenuMsg2(
                (uint)m.Msg, m.WParam, m.LParam, IntPtr.Zero) == WinAPI.S_OK)
                return;
        }


        base.WndProc(ref m);
    }

    void FileView_KeyDown(object sender, KeyEventArgs e)
    {
        ContextMenuHelper.ProcessKeyCommands(br, sender, e);
    }

    void FileView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
    {
        ShellItem item = br.FileView.Items[e.Item].Tag as ShellItem;

        if (!item.CanRename)
        {
            e.CancelEdit = true;
            System.Media.SystemSounds.Beep.Play();
            return;
        }
        else if (item.IsDisk)
        {
            IntPtr editHandle = WinAPI.SendMessage(br.FileView.Handle, WinAPI.WM.LVM_GETEDITCONTROL, 0, IntPtr.Zero);
            WinAPI.SendMessage(editHandle, WinAPI.WM.SETTEXT, 0,
                Marshal.StringToHGlobalAuto(item.Text.Substring(0, item.Text.LastIndexOf(' '))));
        }
    }

    void FileView_AfterLabelEdit(object sender, LabelEditEventArgs e)
    {
        ShellItem item = br.FileView.Items[e.Item].Tag as ShellItem;

        if (e.Label != null && br.FileView.SelectedOrder.Count == 1)
        {
            IntPtr newPidl = IntPtr.Zero;
            if (!(item.IsDisk && item.Text.Substring(0, item.Text.LastIndexOf(' ')) == e.Label) &&
                item.ParentItem.ShellFolder.SetNameOf(
                    br.Handle,
                    item.PIDLRel.Ptr,
                    e.Label,
                    WinAPI.SHGNO.NORMAL,
                    out newPidl) == WinAPI.S_OK)
            {
                item.Update(newPidl, ShellItemUpdateType.Renamed);
            }
            else
                e.CancelEdit = true;
        }
        else if (e.Label != null)
        {
            string label = (!string.IsNullOrEmpty(e.Label) ? e.Label : br.FileView.Items[e.Item].Text);

            string startString = Path.GetFileNameWithoutExtension(label);
            string endString = Path.GetExtension(label);

            int countLength = br.FileView.SelectedOrder.Count.ToString().Length;

            IntPtr newPidl = IntPtr.Zero;
            ShellItem shellItem;
            ListViewItem listItem;

            StringBuilder renameString = new StringBuilder(startString.Length + endString.Length + countLength);
            renameString.Append(startString);
            renameString.Append('0', countLength);
            renameString.Append(endString);

            string counter;
            for (int i = 1; i <= br.FileView.SelectedOrder.Count; i++)
            {
                listItem = br.FileView.SelectedOrder[i - 1] as ListViewItem;
                shellItem = listItem.Tag as ShellItem;

                if (shellItem.IsDisk)
                    continue;

                counter = i.ToString();
                renameString.Insert(startString.Length, "0", countLength - counter.Length);
                renameString.Insert(startString.Length + countLength - counter.Length, counter);
                renameString.Remove(startString.Length + countLength, countLength);

                if (shellItem.ParentItem.ShellFolder.SetNameOf(
                        (i == 1 ? br.Handle : IntPtr.Zero),
                        shellItem.PIDLRel.Ptr,
                        renameString.ToString(),
                        WinAPI.SHGNO.NORMAL,
                        out newPidl) == WinAPI.S_OK)
                {
                    shellItem.Update(newPidl, ShellItemUpdateType.Renamed);
                }
                else if (i == 1)
                    break;
            }

            e.CancelEdit = true;
        }
    }

    void FileView_ItemActivate(object sender, EventArgs e)
    {
        TreeNode parent = br.FolderView.SelectedNode;

        if (parent != null && br.FileView.SelectedOrder.Count > 0)
        {
            ListViewItem listViewItem = (ListViewItem)br.FileView.SelectedOrder[0];
            ShellItem shellItem = (ShellItem)listViewItem.Tag;
            int startIndex = 0;

            if ((Control.ModifierKeys & Keys.Alt) == 0)
            {
                if (shellItem.IsFolder)
                {
                    if (!parent.IsExpanded)
                        parent.Expand();

                    br.FolderView.SelectedNode = parent.Nodes[listViewItem.Text];
                    startIndex = 1;
                }

                if (br.FileView.SelectedOrder.Count - startIndex > 0)
                {
                    IntPtr[] pidls = new IntPtr[br.FileView.SelectedOrder.Count - startIndex];
                    for (int i = startIndex; i < pidls.Length; i++)
                    {
                        pidls[i - startIndex] = ((ShellItem)((ListViewItem)br.FileView.SelectedOrder[i]).Tag).PIDLRel.Ptr;
                    }

                    IntPtr icontextMenuPtr = IntPtr.Zero, context2Ptr = IntPtr.Zero, context3Ptr = IntPtr.Zero;
                    ContextMenu contextMenu = new ContextMenu();
                    ShellItem parentShellItem = (ShellItem)parent.Tag;
                    IShellFolder parentShellFolder = parentShellItem.ShellFolder;


                    try
                    {
                        if (ContextMenuHelper.GetIContextMenu(parentShellFolder, pidls, out icontextMenuPtr, out iContextMenu))
                        {
                            iContextMenu.QueryContextMenu(
                                contextMenu.Handle,
                                0,
                                WinAPI.CMD_FIRST,
                                WinAPI.CMD_LAST,
                                WinAPI.CMF.DEFAULTONLY);

                            int defaultCommand = WinAPI.GetMenuDefaultItem(contextMenu.Handle, false, 0);
                            if (defaultCommand >= WinAPI.CMD_FIRST)
                            {
                                ContextMenuHelper.InvokeCommand(
                                    iContextMenu,
                                    (uint)defaultCommand - WinAPI.CMD_FIRST,
                                    ShellItem.GetRealPath(parentShellItem),
                                    Control.MousePosition);
                            }
                        }
                    }
                    catch (Exception) { }
                    finally
                    {
                        if (iContextMenu != null)
                        {
                            Marshal.ReleaseComObject(iContextMenu);
                            iContextMenu = null;
                        }

                        if (contextMenu.Handle != null)
                            Marshal.FreeCoTaskMem(contextMenu.Handle);

                        Marshal.Release(icontextMenuPtr);
                    }
                }
            }
            else
            {
                IntPtr[] pidls = new IntPtr[br.FileView.SelectedOrder.Count - startIndex];
                for (int i = startIndex; i < pidls.Length; i++)
                {
                    pidls[i - startIndex] = ((ShellItem)((ListViewItem)br.FileView.SelectedOrder[i]).Tag).PIDLRel.Ptr;
                }

                ContextMenuHelper.InvokeCommand(shellItem.ParentItem, pidls, "properties", Control.MousePosition);
            }
        }
    }

    void FileView_MouseUp(object sender, MouseEventArgs e)
    {
        if (suspendContextMenu || contextMenuVisible)
        {
            suspendContextMenu = false;
            return;
        }

        ListViewHitTestInfo hitTest = br.FileView.HitTest(e.Location);
        Point ptInvoke = br.FileView.PointToScreen(e.Location);

        contextMenuVisible = true;
        if (e.Button == MouseButtons.Right &&
            hitTest.Item != null && 
            (br.FileView.View != View.Details || (hitTest.SubItem != null && hitTest.Item.Name == hitTest.SubItem.Name)) &&
            (hitTest.Location == ListViewHitTestLocations.Image ||
            hitTest.Location == ListViewHitTestLocations.Label ||
            hitTest.Location == ListViewHitTestLocations.StateImage))
        {
            CreateNormalMenu(ptInvoke, hitTest, e);
        }
        else if (e.Button == MouseButtons.Right)
        {
            CreateFolderMenu(ptInvoke, e);
        }
        contextMenuVisible = false;
    }

    private void CreateNormalMenu(Point ptInvoke, ListViewHitTestInfo hitTest, MouseEventArgs e)
    {
        hitTest.Item.Selected = true;

        ShellItem item = (ShellItem)hitTest.Item.Tag;

        int offset = br.FileView.SelectedOrder.Contains(hitTest.Item) ? 0 : 1;
        IntPtr[] pidls = new IntPtr[br.FileView.SelectedOrder.Count + offset];

        if (offset == 1)
            pidls[0] = item.PIDLRel.Ptr;

        for (int i = offset; i < pidls.Length; i++)
        {
            pidls[i] = ((ShellItem)((ListViewItem)br.FileView.SelectedOrder[i - offset]).Tag).PIDLRel.Ptr;
        }

        
        IntPtr contextMenu = IntPtr.Zero,
            iContextMenuPtr = IntPtr.Zero,
            iContextMenuPtr2 = IntPtr.Zero,
            iContextMenuPtr3 = IntPtr.Zero;
        IShellFolder parentShellFolder =
            (item.ParentItem != null) ? item.ParentItem.ShellFolder : item.ShellFolder;

        try
        {
            if (ContextMenuHelper.GetIContextMenu(parentShellFolder, pidls, out iContextMenuPtr, out iContextMenu))
            {
                contextMenu = WinAPI.CreatePopupMenu();
                iContextMenu.QueryContextMenu(
                    contextMenu,
                    0,
                    WinAPI.CMD_FIRST,
                    WinAPI.CMD_LAST,
                    WinAPI.CMF.EXPLORE |
                    WinAPI.CMF.CANRENAME |
                    ((Control.ModifierKeys & Keys.Shift) != 0 ? WinAPI.CMF.EXTENDEDVERBS : 0));

                Marshal.QueryInterface(iContextMenuPtr, ref WinAPI.IID_IContextMenu2, out iContextMenuPtr2);
                Marshal.QueryInterface(iContextMenuPtr, ref WinAPI.IID_IContextMenu3, out iContextMenuPtr3);

                try
                {
                    iContextMenu2 =
                        (IContextMenu2)Marshal.GetTypedObjectForIUnknown(iContextMenuPtr2, typeof(IContextMenu2));

                    iContextMenu3 =
                        (IContextMenu3)Marshal.GetTypedObjectForIUnknown(iContextMenuPtr3, typeof(IContextMenu3));
                }
                catch (Exception) { }

                uint selected = WinAPI.TrackPopupMenuEx(
                                    contextMenu,
                                    WinAPI.TPM.RETURNCMD,
                                    ptInvoke.X,
                                    ptInvoke.Y,
                                    this.Handle,
                                    IntPtr.Zero);

                br.OnContextMenuMouseHover(new ContextMenuMouseHoverEventArgs(string.Empty));

                if (selected >= WinAPI.CMD_FIRST)
                {
                    string command = ContextMenuHelper.GetCommandString(iContextMenu, selected - WinAPI.CMD_FIRST, true);

                    if (command == "Explore" && br.FolderView.SelectedNode != null)
                    {
                        if (!br.FolderView.SelectedNode.IsExpanded)
                            br.FolderView.SelectedNode.Expand();

                        br.FolderView.SelectedNode = br.FolderView.SelectedNode.Nodes[hitTest.Item.Text];
                    }
                    else if (command == "rename")
                    {
                        hitTest.Item.BeginEdit();
                    }
                    else
                    {
                        ContextMenuHelper.InvokeCommand(
                            iContextMenu,
                            selected - WinAPI.CMD_FIRST,
                            ShellItem.GetRealPath(br.SelectedItem),
                            ptInvoke);
                    }
                }
            }
        }
        catch (Exception) { }
        finally
        {
            if (iContextMenu != null)
            {
                Marshal.ReleaseComObject(iContextMenu);
                iContextMenu = null;
            }

            if (iContextMenu2 != null)
            {
                Marshal.ReleaseComObject(iContextMenu2);
                iContextMenu2 = null;
            }

            if (iContextMenu3 != null)
            {
                Marshal.ReleaseComObject(iContextMenu3);
                iContextMenu3 = null;
            }

            if (contextMenu != null)
                WinAPI.DestroyMenu(contextMenu);

            if (iContextMenuPtr != IntPtr.Zero)
                Marshal.Release(iContextMenuPtr);

            if (iContextMenuPtr2 != IntPtr.Zero)
                Marshal.Release(iContextMenuPtr2);

            if (iContextMenuPtr3 != IntPtr.Zero)
                Marshal.Release(iContextMenuPtr3);
        }
    }

    private void CreateFolderMenu(Point ptInvoke, MouseEventArgs e)
    {
        ShellItem currentItem = br.SelectedItem;

        foreach (ListViewItem item in br.FileView.SelectedItems)
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
            WinAPI.AppendMenu(viewSubMenu, 
                (br.CurrentViewPlugin == null && br.FileView.View == View.Tile ? rCheck : 0), (uint)CMD_CUSTOM.Tiles, "Tiles");
            WinAPI.AppendMenu(viewSubMenu,
                (br.CurrentViewPlugin == null && br.FileView.View == View.LargeIcon ? rCheck : 0), (uint)CMD_CUSTOM.Icons, "Icons");
            WinAPI.AppendMenu(viewSubMenu,
                (br.CurrentViewPlugin == null && br.FileView.View == View.List ? rCheck : 0), (uint)CMD_CUSTOM.List, "List");
            WinAPI.AppendMenu(viewSubMenu,
                (br.CurrentViewPlugin == null && br.FileView.View == View.Details ? rCheck : 0), (uint)CMD_CUSTOM.Details, "Details");

            for (int i = 0; i < pluginWrapper.ViewPlugins.Count; i++)
            {
                WinAPI.AppendMenu(viewSubMenu,
                    (IViewPlugin.Equals(br.CurrentViewPlugin, pluginWrapper.ViewPlugins[i]) ? rCheck : 0), 
                        (uint)CMD_CUSTOM.SpecialView + (uint)i, ((IViewPlugin)pluginWrapper.ViewPlugins[i]).ViewName);
            }


            DragDropEffects effects = ShellHelper.CanDropClipboard(currentItem);
            bool canPaste = (effects & DragDropEffects.Copy) != 0 || (effects & DragDropEffects.Move) != 0;
            bool canPasteShortCut = (effects & DragDropEffects.Link) != 0;

            WinAPI.AppendMenu(contextMenu, WinAPI.MFT.SEPARATOR, 0, string.Empty);
            WinAPI.AppendMenu(contextMenu, canPaste ? 0 : WinAPI.MFT.GRAYED, (int)CMD_CUSTOM.Paste, "Paste");
            WinAPI.AppendMenu(contextMenu, canPasteShortCut ? 0 : WinAPI.MFT.GRAYED, (int)CMD_CUSTOM.Paste_ShortCut, "Paste Shortcut");


            if (br.SelectedItem.IsFileSystem &&
                ContextMenuHelper.GetNewContextMenu(br.SelectedItem, out newContextMenuPtr, out newContextMenu))
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


            if (!br.SelectedItem.Equals(br.ShellBrowser.DesktopItem))
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
                        br.FileView.View = View.Tile;
                        br.ResetSpecialView();
                        provider.ReleaseStorage();
                        provider.ReleaseStream();
                        break;
                    case CMD_CUSTOM.Icons:
                        br.FileView.View = View.LargeIcon;
                        br.ResetSpecialView();
                        provider.ReleaseStorage();
                        provider.ReleaseStream();
                        break;
                    case CMD_CUSTOM.List:
                        br.FileView.View = View.List;
                        br.ResetSpecialView();
                        provider.ReleaseStorage();
                        provider.ReleaseStream();
                        break;
                    case CMD_CUSTOM.Details:
                        br.FileView.SuspendHeaderContextMenu = true;
                        br.FileView.View = View.Details;
                        br.ResetSpecialView();
                        provider.ReleaseStorage();
                        provider.ReleaseStream();
                        break;

                    case CMD_CUSTOM.Properties:
                        ContextMenuHelper.InvokeCommand(
                            br.SelectedItem.ParentItem,
                            new IntPtr[] { br.SelectedItem.PIDLRel.Ptr },
                            "properties",
                            ptInvoke);
                        break;

                    case CMD_CUSTOM.Paste:
                        ContextMenuHelper.InvokeCommand(
                            br.SelectedItem.ParentItem,
                            new IntPtr[] { br.SelectedItem.PIDLRel.Ptr },
                            "paste",
                            ptInvoke);
                        break;

                    case CMD_CUSTOM.Paste_ShortCut:
                        ContextMenuHelper.InvokeCommand(
                            br.SelectedItem.ParentItem,
                            new IntPtr[] { br.SelectedItem.PIDLRel.Ptr },
                            "pastelink",
                            ptInvoke);
                        break;


                    default:
                        if ((uint)selected <= WinAPI.CMD_LAST)
                        {
                            lock (br.ShellBrowser)
                            {
                                br.NewItemCreated = true;
                            }

                            ContextMenuHelper.InvokeCommand(
                                newContextMenu,
                                (uint)selected - WinAPI.CMD_FIRST,
                                ShellItem.GetRealPath(br.SelectedItem),
                                ptInvoke);
                        }
                        else
                        {
                            int index = (int)selected - (int)CMD_CUSTOM.SpecialView;

                            if (br.FileView.Alignment != ListViewAlignment.Left)
                                br.FileView.Alignment = ListViewAlignment.Left;
                            
                            br.FileView.View = View.LargeIcon;
                            br.CurrentViewPlugin = pluginWrapper.ViewPlugins[index] as IViewPlugin;
                            br.CurrentViewPlugin.ViewControl.Dock = DockStyle.Fill;
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


    private delegate void FileSelected(IFileInfoProvider provider, ShellItem item);
    private delegate void FolderSelected(IDirInfoProvider provider, ShellItem item);

    void viewPluginTimer_Tick(object sender, EventArgs e)
    {
        if (!viewPluginWorker.IsBusy)
        {
            viewPluginWorker.RunWorkerAsync();
            viewPluginTimer.Stop();
        }
    }

    void br_SelectedFolderChanged(object sender, SelectedFolderChangedEventArgs e)
    {
        if (br.CurrentViewPlugin != null)
            br.CurrentViewPlugin.Reset();
    }

    void FileView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
        if (e.IsSelected && br.SelectionChange && br.CurrentViewPlugin != null)
        {
            viewPluginTimer.Stop();
            provider.ProviderItem = e.Item.Tag as ShellItem;
            viewPluginTimer.Start();
        }
    }

    void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        provider.ReleaseStorage();
        provider.ReleaseStream();

        try
        {
            if (provider.ProviderItem.CanRead)
            {
                IAsyncResult asyncResult = null;

                if (provider.ProviderItem.IsFolder)
                {
                    FolderSelected selectedDelegate = new FolderSelected(br.CurrentViewPlugin.FolderSelected);
                    asyncResult = br.CurrentViewPlugin.ViewControl.BeginInvoke(selectedDelegate, provider, provider.ProviderItem);
                }
                else
                {
                    FileSelected selectedDelegate = new FileSelected(br.CurrentViewPlugin.FileSelected);
                    asyncResult = br.CurrentViewPlugin.ViewControl.BeginInvoke(selectedDelegate, provider, provider.ProviderItem);
                }

                asyncResult.AsyncWaitHandle.WaitOne();
            }
            else
                br.CurrentViewPlugin.Reset();
        }
        catch (Exception) { }
    }

    void SetToolTip(ListViewItem listItem)
    {
        IntPtr queryInfoPtr;
        IQueryInfo queryInfo;

        if (ShellHelper.GetIQueryInfo((ShellItem)listItem.Tag, out queryInfoPtr, out queryInfo))
        {
            string info;
            queryInfo.GetInfoTip(WinAPI.QITIPF.DEFAULT, out info);


            Marshal.ReleaseComObject(queryInfo);
            Marshal.Release(queryInfoPtr);

            listItem.ToolTipText = info;
        }
    }

    void FileView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
    {
        br.BeginInvoke(toolTipDelegate, e.Item);
    }
  }
}