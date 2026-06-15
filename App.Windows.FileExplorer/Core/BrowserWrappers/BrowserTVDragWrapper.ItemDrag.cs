using System;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  internal partial class BrowserTVDragWrapper
  {
    void ItemDrag(object sender, ItemDragEventArgs e)
    {
      ReleaseCom();

      startButton = e.Button;
      ShellItem item = (ShellItem)((TreeNode)e.Item).Tag;
      dataObjectPtr = ShellHelper.GetIDataObject(new ShellItem[] { item });

      if (dataObjectPtr != IntPtr.Zero)
      {
        DragDropEffects effects;
        OnDragStart(new DragEnterEventArgs((item.ParentItem != null ? item.ParentItem : item), browser.FolderView));
        WinAPI.DoDragDrop(dataObjectPtr, this, DragDropEffects.Copy | DragDropEffects.Link | DragDropEffects.Move, out effects);
        OnDragEnd(new EventArgs());
      }
    }
  }
}