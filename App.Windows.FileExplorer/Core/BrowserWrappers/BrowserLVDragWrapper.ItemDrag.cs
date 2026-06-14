using System;
using ShellDll;
using System.Windows.Forms;

namespace FileBrowser
{
  partial class BrowserLVDragWrapper
  {
    void ItemDrag(object sender, ItemDragEventArgs e)
    {
      ReleaseCom();

      startButton = e.Button;

      ShellItem[] items = new ShellItem[br.FileView.SelectedItems.Count];
      for (int i = 0; i < br.FileView.SelectedItems.Count; i++)
      {
          items[i] = (ShellItem)br.FileView.SelectedItems[i].Tag;
      }

      dataObjectPtr = ShellHelper.GetIDataObject(items);

      if (dataObjectPtr != IntPtr.Zero)
      {
          DragDropEffects effects;
          OnDragStart(new DragEnterEventArgs((items[0].ParentItem != null ? items[0].ParentItem : items[0]), br.FileView));
          WinAPI.DoDragDrop(dataObjectPtr, this, DragDropEffects.Copy | DragDropEffects.Link | DragDropEffects.Move, out effects);
          OnDragEnd(new EventArgs());
      }
    }
  }
}