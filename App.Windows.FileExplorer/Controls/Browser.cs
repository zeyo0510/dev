using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  #region Delegates

  // This delegate is used for the ContextMenuHoverEvent
  public delegate void ContextMenuMouseHoverEventHandler(object sender, ContextMenuMouseHoverEventArgs e);

  // This delegate is used for the SelectedFolderChangedEvent
  public delegate void SelectedFolderChangedEventHandler(object sender, SelectedFolderChangedEventArgs e);

  #endregion
  
  public partial class Browser : UserControl
  {
    #region Fields

    // The shellbrowser used by this browser to get the shellitems of all files and folders
    private ShellBrowser shellBrowser;

    // These wrappers are used for accepting drops on the browser
    private BrowserTVDropWrapper tvDropWrapper;
    private BrowserLVDropWrapper lvDropWrapper;

    // These wrappers are used to allow dragging from the browser
    private BrowserTVDragWrapper tvDragWrapper;
    private BrowserLVDragWrapper lvDragWrapper;

    // These wrappers are used to create the standard context menu's, like in Windows Explorer
    private BrowserTVContextMenuWrapper tvContextWrapper;
    private BrowserLVContextMenuWrapper lvContextWrapper;

    private BrowserPluginWrapper pluginWrapper;

    // This field is used to store the control from where dragging has started
    private Control dragStartControl;

    // When this bool is true, selecting a node will change the current directory, 
    // otherwise the current directory won't change
    private bool selectionChange = true, newItemCreated;
    
    // These fields are used to determine the directory to start the browser in
    private SpecialFolders startupDir = SpecialFolders.MyComputer;
    private string otherStartupDir = string.Empty;

    // Selected node and item are the TreeNode and ShellItem of the current directory
    private TreeNode selectedNode;
    private ShellItem selectedItem;

    // These TreeNodes are used very often, so need own fields. They are for the Root (Desktop) and My Computer.
    private TreeNode desktopNode, myCompNode;

    private Thread updateThread;
    private UpdateInvoker updateInvoker;
    private bool updating;
    private ContextMenu columnContextMenu;
    private ArrayList invisibleColumns;
    private StreamStorageProvider provider;

    private int maxBackForward = 10;
    private bool suspendNavBackAdd, handleCreated;

    private delegate void UpdateInvoker(object sender, ShellItemUpdateEventArgs e);

    #region Events

    public event ContextMenuMouseHoverEventHandler ContextMenuMouseHover;
    
    public event SelectedFolderChangedEventHandler SelectedFolderChanged;

    #endregion

    #endregion
    
    public Browser()
    {
      this.InitializeComponent();
      InitBrowser();
      InitFolderView();
      InitFileView();
    }

    #region Events

    void Browser_HandleCreated(object sender, EventArgs e) {
      InitPlugins();
      InitBaseItems();
      InitContextMenu();
      InitDragDrop();
      InitStartUp();
      InitUpdate();

      GC.Collect();

      handleCreated = true;
    }

    void Browser_HandleDestroyed(object sender, EventArgs e) {
      if (handleCreated) {
        handleCreated = false;

        folderView.Nodes.Clear();
        fileView.Items.Clear();

        updating = false;

        ShellBrowser.UpdateCondition.ContinueUpdate = false;

        updateThread.IsBackground = false;
        updateThread.Join(5000);

        GC.Collect();
      }
    }

    private void navigationBar_Resize(object sender, EventArgs e)
    {
      int newSize = navigationBar.Width - navAddressLabel.Bounds.Right - 15;
  
      if (newSize > 0)
        navAddressBox.Size = new Size(newSize, navAddressBox.Height);
  
      navAddressBox.SelectionLength = 0;
    }
    
    private void navBackButton_ButtonClick(object sender, EventArgs e)
    {
      this.Back();
    }
    
    private void navForwardButton_ButtonClick(object sender, EventArgs e)
    {
      this.Forward();
    }
    
    private void navBackForwardButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      ToolStripSplitButton button = sender as ToolStripSplitButton;

      if (sender.Equals(navBackButton)) {
        AddNavBackForwardItem(navForwardButton, selectedItem);
      } else {
        AddNavBackForwardItem(navBackButton, selectedItem);
      }

      int index = button.DropDownItems.IndexOf(e.ClickedItem);

      ToolStripItem item;
      while (index > -1) {
        item = button.DropDownItems[0];
        button.DropDownItems.Remove(item);
        index--;

        if (index > -1 && sender.Equals(navBackButton)) {
          navForwardButton.Enabled = true;
          navForwardButton.DropDownItems.Insert(0, item);

          if (navForwardButton.DropDownItems.Count > maxBackForward)
              navForwardButton.DropDownItems.RemoveAt(maxBackForward);
        } else if (index > -1) {
          navBackButton.Enabled = true;
          navBackButton.DropDownItems.Insert(0, item);

          if (navBackButton.DropDownItems.Count > maxBackForward)
            navBackButton.DropDownItems.RemoveAt(maxBackForward);
        }
      }

      if (button.DropDownItems.Count == 0)
        button.Enabled = false;

      suspendNavBackAdd = true;
      SelectPath((ShellItem)e.ClickedItem.Tag, false);
    }
    
    private void navUpButton_Click(object sender, EventArgs e)
    {
      this.Up();
    }
    
    private void navFoldersButton_CheckedChanged(object sender, EventArgs e)
    {
      this.browseSplitter.Panel1Collapsed = !this.browseSplitter.Panel1Collapsed;
    }
    
    private void navAddressBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (navAddressBox.SelectedIndex > -1) {
        ShellItem item = ((BrowserComboItem)navAddressBox.Items[navAddressBox.SelectedIndex]).ShellItem;
  
        if (!selectedItem.Equals(item)) {
          TreeNode oldNode = selectedNode;
          int currentIndex = navAddressBox.Items.IndexOf(navAddressBox.CurrentItem);
          BrowserComboItem currentItem = navAddressBox.CurrentItem;
          int maxIndent = folderView.IsParentNode(myCompNode, selectedNode) ? 3 : 2;
  
          navAddressBox.CurrentItem = (BrowserComboItem)navAddressBox.Items[navAddressBox.SelectedIndex];
          TreeNode newNode = SelectPath(item, false);
  
          if (folderView.IsParentNode(newNode, oldNode)) {
            while (currentItem.Text != newNode.Text && currentItem.Indent >= maxIndent) {
              navAddressBox.Items.Remove(currentItem);
              currentIndex--;
              currentItem = (BrowserComboItem)navAddressBox.Items[currentIndex];
            }
          } else {
            while (currentItem.Indent >= maxIndent) {
              navAddressBox.Items.Remove(currentItem);
              currentIndex--;
              currentItem = (BrowserComboItem)navAddressBox.Items[currentIndex];
            }
          }
        }
      }
    }
    
    private void navAddressBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter) {
        if (SelectPath(this.navAddressBox.Text, false) == null)
          e.Handled = true;
      }
    }
    
    private void splitter_MouseDown(object sender, MouseEventArgs e)
    {
      /* This disables the normal move behavior */
      ((SplitContainer)sender).IsSplitterFixed = true;
    }

    private void splitter_MouseMove(object sender, MouseEventArgs e) {
      /* Check to make sure the splitter won't be updated by the normal move behavior also */
      if (((SplitContainer)sender).IsSplitterFixed) {
        /* Make sure that the button used to move the splitter is the left mouse button */
        if (e.Button.Equals(MouseButtons.Left)) {
          /* Checks to see if the splitter is aligned Vertically */
          if (((SplitContainer)sender).Orientation.Equals(Orientation.Vertical)) {
            /* Only move the splitter if the mouse is within the appropriate bounds */
            if (e.X > 0 && e.X < ((SplitContainer)sender).Width) {
                /* Move the splitter */
                ((SplitContainer)sender).SplitterDistance = e.X;
            }
          }
          /* If it isn't aligned vertically then it must be horizontal */
          else
          {
            /* Only move the splitter if the mouse is within  the appropriate bounds */
            if (e.Y > 0 && e.Y < ((SplitContainer)sender).Height) {
              /* Move the splitter */
              ((SplitContainer)sender).SplitterDistance = e.Y;
            }
          }

          ((SplitContainer)sender).Refresh();
        }
        /* If a button other than left is pressed or no button  at all */
        else
        {
          /* This allows the splitter to be moved normally again */
          ((SplitContainer)sender).IsSplitterFixed = false;
        }
      }
    }

    private void splitter_MouseUp(object sender, MouseEventArgs e) {
      /* This allows the splitter to be moved normally again */
      ((SplitContainer)sender).IsSplitterFixed = false;
    }
    
    #region FolderView Events

    void folderView_BeforeSelect(object sender, TreeViewCancelEventArgs e) {
      if (selectionChange) {
        ShellItem nodeItem = (ShellItem)e.Node.Tag;

        if (e.Action == TreeViewAction.ByMouse && !e.Node.IsExpanded)
          e.Node.Expand();
        else
          ExtendTreeNode(e.Node, false);
      }
    }
    
    void folderView_AfterSelect(object sender, TreeViewEventArgs e) {
      if (selectionChange) {
        ShellItem oldItem = selectedItem;
        ShellItem newItem = e.Node.Tag as ShellItem;

        if (SetNewPath(oldItem, newItem)) {
          if (!ShellItem.Equals(oldItem, newItem)) {
            if (suspendNavBackAdd)
              suspendNavBackAdd = false;
            else
              AddNavBackForwardItem(navBackButton, oldItem);

            OnSelectedFolderChanged(new SelectedFolderChangedEventArgs(folderView.SelectedNode));
          }
        } else
          SelectPath(oldItem, false);
      }
    }

    void folderView_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
      ShellItem nodeItem = (ShellItem)e.Node.Tag;

      Cursor.Current = Cursors.WaitCursor;
      e.Cancel = !ExtendTreeNode(e.Node, false);
      Cursor.Current = Cursors.Default;

      if (e.Cancel)
        e.Node.Nodes.Clear();
    }

    #endregion
    
    #endregion

    #region Generated Events

    private void OnSelectedFolderChanged(SelectedFolderChangedEventArgs e) {
      ChangeNavBarItem(e);
      selectedNode = e.Node;

      if (SelectedFolderChanged != null)
        SelectedFolderChanged(this, e);
    }

    internal void OnContextMenuMouseHover(ContextMenuMouseHoverEventArgs e) {
      if (ContextMenuMouseHover != null)
        ContextMenuMouseHover(this, e);
    }

    #endregion

    #region Update File/Folder Changes

    void shellBrowser_ShellItemUpdate(object sender, ShellItemUpdateEventArgs e) {
      if (updating) {
        if (this.InvokeRequired)
          Invoke(updateInvoker, sender, e);
        else
          ShellItemUpdateInvoke(sender, e);
      }
    }

    private void ShellItemUpdateInvoke(object sender, ShellItemUpdateEventArgs e) {
      switch (e.UpdateType) {
        case ShellItemUpdateType.Created:
            #region Created
            {
                if (Object.Equals(sender, selectedItem))
                {
                    string[] subItems = new string[fileView.Columns.Count - 1];
                    ListViewItem newListItem = GetListViewItem(subItems, e.NewItem);
                    fileView.Items.Add(newListItem);

                    if (newItemCreated)
                    {
                        newItemCreated = false;

                        foreach (ListViewItem item in fileView.Items)
                            item.Selected = false;

                        newListItem.Selected = true;
                        newListItem.BeginEdit();
                    }
                }

                if (e.NewItem.IsFolder)
                {
                    ShellItem parent = sender as ShellItem;
                    TreeNode parentNode;

                    if (folderView.GetTreeNode(parent, out parentNode))
                    {
                        TreeNode newNode = new TreeNode(
                            e.NewItem.Text,
                            e.NewItem.ImageIndex,
                            e.NewItem.SelectedImageIndex);
                        newNode.Tag = e.NewItem;

                        if (e.NewItem.HasSubfolder)
                            newNode.Nodes.Add(string.Empty);

                        newNode.Name = newNode.Text;

                        parentNode.Nodes.Add(newNode);
                    }
                }
            }
          #endregion
          break;

        case ShellItemUpdateType.Deleted:
            #region Deleted
            {
                ListViewItem listItem;
                if (Object.Equals(sender, selectedItem) && fileView.GetListItem(e.OldItem, out listItem))
                {
                    fileView.Items.Remove(listItem);
                }

                if (e.OldItem.IsFolder)
                {
                    ShellItem parent = sender as ShellItem;
                    TreeNode parentNode;

                    if (folderView.GetTreeNode(parent, out parentNode))
                    {
                        parentNode.Nodes.RemoveByKey(e.OldItem.Text);
                    }
                }
            }
          #endregion
          break;

        case ShellItemUpdateType.Renamed:
            #region Renamed
            {
                ListViewItem listItem;
                if (Object.Equals(sender, selectedItem) && fileView.GetListItem(e.OldItem, out listItem))
                {
                    listItem.Text = e.NewItem.Text;
                    listItem.Name = listItem.Text;
                }

                if (e.NewItem.IsFolder)
                {
                    TreeNode node;

                    if (folderView.GetTreeNode(e.NewItem, out node))
                    {
                        node.Text = e.NewItem.Text;
                        node.Name = node.Text;
                        
                        TreeNode parent = node.Parent;
                        parent.Nodes.Remove(node);
                        parent.Nodes.Add(node);
                    }
                }
            }
          #endregion 
          break;

        case ShellItemUpdateType.IconChange:
            #region IconChange
            {
                ListViewItem listItem;
                if (Object.Equals(sender, selectedItem) && 
                    fileView.GetListItem(e.OldItem, out listItem) && 
                    listItem.ImageIndex != e.NewItem.ImageIndex)
                {
                    listItem.ImageIndex = e.NewItem.ImageIndex;
                }

                if (e.NewItem.IsFolder)
                {
                    TreeNode node;

                    if (folderView.GetTreeNode(e.NewItem, out node) && node.ImageIndex != e.NewItem.ImageIndex)
                    {
                        node.ImageIndex = e.NewItem.ImageIndex;
                    }
                }
            }
          #endregion
          break;

        case ShellItemUpdateType.MediaChange:
            #region MediaChange
            {
                ListViewItem listItem;
                if (Object.Equals(sender, selectedItem) && fileView.GetListItem(e.OldItem, out listItem))
                {
                    listItem.ImageIndex = e.NewItem.ImageIndex;
                }

                TreeNode node;
                if (folderView.GetTreeNode(e.NewItem, out node))
                {
                    node.Collapse(true);

                    if (node.Equals(selectedNode) || folderView.IsParentNode(node, selectedNode))
                        folderView.SelectedNode = node.Parent;

                    node.Nodes.Clear();
                    node.ImageIndex = e.NewItem.ImageIndex;

                    if (e.NewItem.HasSubfolder)
                        node.Nodes.Add(string.Empty);
                }
            }
          #endregion
          break;
      }
    }

    #endregion

    #region ListView Items/Columns

    void ColumnContextItem_Click(object sender, EventArgs e) {
      MenuItem item = sender as MenuItem;
      item.Checked = !item.Checked;

      fileView.BeginUpdate();
      Cursor.Current = Cursors.WaitCursor;

      if (item.Checked) {
        #region Add Column

        foreach (ColumnHeader header in invisibleColumns) {
          if (header.Text == item.Text) {
            invisibleColumns.Remove(header);
            fileView.Columns.Add(header);

            IColumnPlugin plugin = header.Tag as IColumnPlugin;
            foreach (ListViewItem listItem in fileView.Items) {
              provider.ProviderItem = listItem.Tag as ShellItem;

              if (provider.ProviderItem.CanRead) {
                try {
                  if (provider.ProviderItem.IsFolder) {
                    #region Folder Info

                    listItem.SubItems.Add(plugin.GetFolderInfo(provider, header.Text, provider.ProviderItem));

                    provider.ReleaseStorage();

                    #endregion
                  } else {
                    #region File Info

                    listItem.SubItems.Add(plugin.GetFileInfo(provider, header.Text, provider.ProviderItem));

                    provider.ReleaseStream();

                    #endregion
                  }
                } catch (Exception) {
                  listItem.SubItems.Add(string.Empty);
                }
              } else
                listItem.SubItems.Add(string.Empty);
            }

            provider.ProviderItem = null;

            break;
          }
        }

        #endregion
      } else {
        #region Remove Column

        ColumnHeader header = fileView.Columns[item.Text];

        int index = header.Index;
        fileView.Columns.Remove(header);

        foreach (ListViewItem listItem in fileView.Items) {
          listItem.SubItems.RemoveAt(index);
        }

        invisibleColumns.Add(header);

        #endregion
      }

      Cursor.Current = Cursors.Default;
      fileView.EndUpdate();
    }

    #endregion

    #region Drag/Drop

    void DragWrapper_DragStart(object sender, DragEnterEventArgs e)
    {
        dragStartControl = e.DragStartControl;

        tvDropWrapper.ParentDragItem = e.Parent;
        lvDropWrapper.ParentDragItem = e.Parent;
    }

    void DragWrapper_DragEnd(object sender, EventArgs e)
    {
        tvDropWrapper.ParentDragItem = null;
        lvDropWrapper.ParentDragItem = null;
    }

    void DropWrapper_Drop(object sender, DropEventArgs e)
    {
      if (Control.Equals(dragStartControl, e.DragStartControl) && (e.MouseButtons & WinAPI.MK.RBUTTON) != 0)
      {
          if (Control.Equals(dragStartControl, folderView)) tvContextWrapper.SuspendContextMenu = true;
          else if (Control.Equals(dragStartControl, fileView)) lvContextWrapper.SuspendContextMenu = true;
      }

        dragStartControl = null;
    }

    #endregion
  }
}