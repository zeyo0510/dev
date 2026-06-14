using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using ShellDll;

namespace FileBrowser
{
  internal class BrowserTVContextMenuWrapper : NativeWindow
  {
    private Browser br;
      
    private bool suspendContextMenu;

    private IContextMenu iContextMenu;
    private IContextMenu2 iContextMenu2;
    private IContextMenu3 iContextMenu3;

    private bool contextMenuVisible;

    private enum CMD_CUSTOM
    {
      ExpandCollapse = (int)WinAPI.CMD_LAST + 1
    }

    public BrowserTVContextMenuWrapper(Browser br)
    {
      this.br = br;

      br.FolderView.MouseUp += new MouseEventHandler(FolderView_MouseUp);
      br.FolderView.AfterLabelEdit += new NodeLabelEditEventHandler(FolderView_AfterLabelEdit);
      br.FolderView.BeforeLabelEdit += new NodeLabelEditEventHandler(FolderView_BeforeLabelEdit);
      br.FolderView.KeyDown += new KeyEventHandler(FolderView_KeyDown);

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

            if (ShellHelper.LoWord(m.WParam) == (int)CMD_CUSTOM.ExpandCollapse)
                info = "Expands or collapses the current selected item";
            else
            {
                info = ContextMenuHelper.GetCommandString(
                    iContextMenu,
                    ShellHelper.LoWord(m.WParam) - WinAPI.CMD_FIRST, 
                    false);
            }

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

        if (iContextMenu3 != null &&
            m.Msg == (int)WinAPI.WM.MENUCHAR)
        {
            if (iContextMenu3.HandleMenuMsg2(
                (uint)m.Msg, m.WParam, m.LParam, IntPtr.Zero) == WinAPI.S_OK)
                return;
        }

        base.WndProc(ref m);
    }


    void FolderView_KeyDown(object sender, KeyEventArgs e)
    {
      ContextMenuHelper.ProcessKeyCommands(br, sender, e);
    }

    void FolderView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
      ShellItem item = e.Node.Tag as ShellItem;

      if (!item.CanRename)
      {
        e.CancelEdit = true;
        System.Media.SystemSounds.Beep.Play();
      }
      if (item.IsDisk)
      {
        IntPtr editHandle = WinAPI.SendMessage(br.FolderView.Handle, WinAPI.WM.TVM_GETEDITCONTROL, 0, IntPtr.Zero);
        WinAPI.SendMessage(editHandle, WinAPI.WM.SETTEXT, 0, Marshal.StringToHGlobalAuto(item.Text.Substring(0, item.Text.LastIndexOf(' '))) );
      }
    }

    void FolderView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
      ShellItem item = e.Node.Tag as ShellItem;

      IntPtr newPidl = IntPtr.Zero;
      if (e.Label != null && !(item.IsDisk && item.Text.Substring(0, item.Text.LastIndexOf(' ')) == e.Label) && item.ParentItem.ShellFolder.SetNameOf(br.Handle, item.PIDLRel.Ptr, e.Label, WinAPI.SHGNO.NORMAL, out newPidl) == WinAPI.S_OK)
      {
        item.Update(newPidl, ShellItemUpdateType.Renamed);
      }
      else
      {
        e.CancelEdit = true;
      }

      br.FolderView.LabelEdit = false;
    }

    void FolderView_MouseUp(object sender, MouseEventArgs e)
    {
        if (suspendContextMenu || contextMenuVisible)
        {
            suspendContextMenu = false;
            return;
        }

        TreeViewHitTestInfo hitTest = br.FolderView.HitTest(e.Location);

        contextMenuVisible = true;
        if (e.Button == MouseButtons.Right &&
            (hitTest.Location == TreeViewHitTestLocations.Image ||
            hitTest.Location == TreeViewHitTestLocations.Label ||
            hitTest.Location == TreeViewHitTestLocations.StateImage))
        {
            ShellItem item = (ShellItem)hitTest.Node.Tag;

            IntPtr contextMenu = IntPtr.Zero,
                iContextMenuPtr = IntPtr.Zero,
                iContextMenuPtr2 = IntPtr.Zero,
                iContextMenuPtr3 = IntPtr.Zero;
            IShellFolder parentShellFolder =
                (item.ParentItem != null) ? item.ParentItem.ShellFolder : item.ShellFolder;

            try
            {
                if (ContextMenuHelper.GetIContextMenu(parentShellFolder, new IntPtr[] { item.PIDLRel.Ptr },
                        out iContextMenuPtr, out iContextMenu))
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

                    string topInvoke = hitTest.Node.IsExpanded ? "Collapse" : "Expand";
                    WinAPI.MFT extraFlag = (hitTest.Node.Nodes.Count > 0) ? 0 : WinAPI.MFT.GRAYED;
                    WinAPI.InsertMenu(contextMenu, 0,
                        WinAPI.MFT.BYPOSITION | extraFlag,
                        (int)CMD_CUSTOM.ExpandCollapse, topInvoke);
                    WinAPI.InsertMenu(contextMenu, 1,
                        WinAPI.MFT.BYPOSITION | WinAPI.MFT.SEPARATOR,
                        0, "-");

                    WinAPI.SetMenuDefaultItem(
                        contextMenu,
                        0,
                        true);

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

                    Point ptInvoke = br.FolderView.PointToScreen(e.Location);
                    uint selected = WinAPI.TrackPopupMenuEx(
                                        contextMenu,
                                        WinAPI.TPM.RETURNCMD,
                                        ptInvoke.X,
                                        ptInvoke.Y,
                                        this.Handle,
                                        IntPtr.Zero);

                    br.OnContextMenuMouseHover(new ContextMenuMouseHoverEventArgs(string.Empty));

                    if (selected == (int)CMD_CUSTOM.ExpandCollapse)
                    {
                        if (hitTest.Node.IsExpanded)
                            hitTest.Node.Collapse(true);
                        else
                            hitTest.Node.Expand();
                    }
                    else if (selected >= WinAPI.CMD_FIRST)
                    {
                        string command = ContextMenuHelper.GetCommandString(
                            iContextMenu, 
                            selected - WinAPI.CMD_FIRST, 
                            true);

                        if (command == "rename")
                        {
                            br.FolderView.LabelEdit = true;
                            hitTest.Node.BeginEdit();
                        }
                        else
                        {
                            ContextMenuHelper.InvokeCommand(
                                iContextMenu,
                                selected - WinAPI.CMD_FIRST,
                                (item.ParentItem != null) ?
                                ShellItem.GetRealPath(item.ParentItem) : ShellItem.GetRealPath(item),
                                ptInvoke);
                        }
                    }
                }
            } catch (Exception) {
            } finally {
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

                if (contextMenu != null) WinAPI.DestroyMenu(contextMenu);

                if (iContextMenuPtr  != IntPtr.Zero) Marshal.Release(iContextMenuPtr);
                if (iContextMenuPtr2 != IntPtr.Zero) Marshal.Release(iContextMenuPtr2);
                if (iContextMenuPtr3 != IntPtr.Zero) Marshal.Release(iContextMenuPtr3);
            }
        }
        contextMenuVisible = false;
    }
  }
}