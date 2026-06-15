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
  internal partial class BrowserLVContextMenuWrapper : NativeWindow
  {
    private Browser browser;

    private BrowserPluginWrapper pluginWrapper;
    private StreamStorageProvider provider;

    private BackgroundWorker viewPluginWorker;
    private Timer viewPluginTimer;
    private delegate void ViewPluginFileChange(IFileInfoProvider provider, ShellItem item);
    private delegate void ViewPluginFolderChange(IDirInfoProvider provider, ShellItem item);

    private delegate void ToolTipDelegate(ListViewItem listItem);
    private ToolTipDelegate toolTipDelegate;

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

    public BrowserLVContextMenuWrapper(Browser borwser, BrowserPluginWrapper pluginWrapper)
    {
      this.browser = borwser;
      this.pluginWrapper = pluginWrapper;

      provider = new StreamStorageProvider(FileAccess.ReadWrite);
      viewPluginWorker = new BackgroundWorker();
      viewPluginWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
      viewPluginTimer = new System.Windows.Forms.Timer();
      viewPluginTimer.Tick += new EventHandler(viewPluginTimer_Tick);
      viewPluginTimer.Interval = 300;

      browser.FileView.ShowItemToolTips = true;
      toolTipDelegate = new ToolTipDelegate(SetToolTip);

      browser.SelectedFolderChanged += new SelectedFolderChangedEventHandler(br_SelectedFolderChanged);

      browser.FileView.LabelEdit = true;
      browser.FileView.MouseUp += new MouseEventHandler(FileView_MouseUp);
      browser.FileView.ItemActivate += new EventHandler(FileView_ItemActivate);
      browser.FileView.AfterLabelEdit += new LabelEditEventHandler(FileView_AfterLabelEdit);
      browser.FileView.BeforeLabelEdit += new LabelEditEventHandler(FileView_BeforeLabelEdit);
      browser.FileView.KeyDown += new KeyEventHandler(FileView_KeyDown);
      browser.FileView.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(FileView_ItemSelectionChanged);
      browser.FileView.ItemMouseHover += new ListViewItemMouseHoverEventHandler(FileView_ItemMouseHover);

      this.CreateHandle(new CreateParams());
    }

    void FileView_KeyDown(object sender, KeyEventArgs e)
    {
        ContextMenuHelper.ProcessKeyCommands(browser, sender, e);
    }

    void FileView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
    {
        ShellItem item = browser.FileView.Items[e.Item].Tag as ShellItem;

        if (!item.CanRename)
        {
            e.CancelEdit = true;
            System.Media.SystemSounds.Beep.Play();
            return;
        }
        else if (item.IsDisk)
        {
            IntPtr editHandle = WinAPI.SendMessage(browser.FileView.Handle, WinAPI.WM.LVM_GETEDITCONTROL, 0, IntPtr.Zero);
            WinAPI.SendMessage(editHandle, WinAPI.WM.SETTEXT, 0,
                Marshal.StringToHGlobalAuto(item.Text.Substring(0, item.Text.LastIndexOf(' '))));
        }
    }

    void FileView_AfterLabelEdit(object sender, LabelEditEventArgs e)
    {
        ShellItem item = browser.FileView.Items[e.Item].Tag as ShellItem;

        if (e.Label != null && browser.FileView.SelectedOrder.Count == 1)
        {
            IntPtr newPidl = IntPtr.Zero;
            if (!(item.IsDisk && item.Text.Substring(0, item.Text.LastIndexOf(' ')) == e.Label) &&
                item.ParentItem.ShellFolder.SetNameOf(
                    browser.Handle,
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
            string label = (!string.IsNullOrEmpty(e.Label) ? e.Label : browser.FileView.Items[e.Item].Text);

            string startString = Path.GetFileNameWithoutExtension(label);
            string endString = Path.GetExtension(label);

            int countLength = browser.FileView.SelectedOrder.Count.ToString().Length;

            IntPtr newPidl = IntPtr.Zero;
            ShellItem shellItem;
            ListViewItem listItem;

            StringBuilder renameString = new StringBuilder(startString.Length + endString.Length + countLength);
            renameString.Append(startString);
            renameString.Append('0', countLength);
            renameString.Append(endString);

            string counter;
            for (int i = 1; i <= browser.FileView.SelectedOrder.Count; i++)
            {
                listItem = browser.FileView.SelectedOrder[i - 1] as ListViewItem;
                shellItem = listItem.Tag as ShellItem;

                if (shellItem.IsDisk)
                    continue;

                counter = i.ToString();
                renameString.Insert(startString.Length, "0", countLength - counter.Length);
                renameString.Insert(startString.Length + countLength - counter.Length, counter);
                renameString.Remove(startString.Length + countLength, countLength);

                if (shellItem.ParentItem.ShellFolder.SetNameOf(
                        (i == 1 ? browser.Handle : IntPtr.Zero),
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
        TreeNode parent = browser.FolderView.SelectedNode;

        if (parent != null && browser.FileView.SelectedOrder.Count > 0)
        {
            ListViewItem listViewItem = (ListViewItem)browser.FileView.SelectedOrder[0];
            ShellItem shellItem = (ShellItem)listViewItem.Tag;
            int startIndex = 0;

            if ((Control.ModifierKeys & Keys.Alt) == 0)
            {
                if (shellItem.IsFolder)
                {
                    if (!parent.IsExpanded)
                        parent.Expand();

                    browser.FolderView.SelectedNode = parent.Nodes[listViewItem.Text];
                    startIndex = 1;
                }

                if (browser.FileView.SelectedOrder.Count - startIndex > 0)
                {
                    IntPtr[] pidls = new IntPtr[browser.FileView.SelectedOrder.Count - startIndex];
                    for (int i = startIndex; i < pidls.Length; i++)
                    {
                        pidls[i - startIndex] = ((ShellItem)((ListViewItem)browser.FileView.SelectedOrder[i]).Tag).PIDLRel.Ptr;
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
                IntPtr[] pidls = new IntPtr[browser.FileView.SelectedOrder.Count - startIndex];
                for (int i = startIndex; i < pidls.Length; i++)
                {
                    pidls[i - startIndex] = ((ShellItem)((ListViewItem)browser.FileView.SelectedOrder[i]).Tag).PIDLRel.Ptr;
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

        ListViewHitTestInfo hitTest = browser.FileView.HitTest(e.Location);
        Point ptInvoke = browser.FileView.PointToScreen(e.Location);

        contextMenuVisible = true;
        if (e.Button == MouseButtons.Right &&
            hitTest.Item != null && 
            (browser.FileView.View != View.Details || (hitTest.SubItem != null && hitTest.Item.Name == hitTest.SubItem.Name)) &&
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
        if (browser.CurrentViewPlugin != null) browser.CurrentViewPlugin.Reset();
    }

    void FileView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
        if (e.IsSelected && browser.SelectionChange && browser.CurrentViewPlugin != null)
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
                    FolderSelected selectedDelegate = new FolderSelected(browser.CurrentViewPlugin.FolderSelected);
                    asyncResult = browser.CurrentViewPlugin.ViewControl.BeginInvoke(selectedDelegate, provider, provider.ProviderItem);
                }
                else
                {
                    FileSelected selectedDelegate = new FileSelected(browser.CurrentViewPlugin.FileSelected);
                    asyncResult = browser.CurrentViewPlugin.ViewControl.BeginInvoke(selectedDelegate, provider, provider.ProviderItem);
                }

                asyncResult.AsyncWaitHandle.WaitOne();
            }
            else
                browser.CurrentViewPlugin.Reset();
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
        browser.BeginInvoke(toolTipDelegate, e.Item);
    }
  }
}