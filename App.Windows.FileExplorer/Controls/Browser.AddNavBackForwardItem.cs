using System;
using System.Drawing;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void AddNavBackForwardItem(ToolStripSplitButton button, ShellItem selectedItem) {
      Bitmap image = ShellImageList.GetIcon(selectedItem.ImageIndex, true).ToBitmap();
      ToolStripMenuItem backItem = new ToolStripMenuItem(selectedItem.Text, image);
      backItem.Name = backItem.Text;
      backItem.Tag = selectedItem;
      backItem.ImageScaling = ToolStripItemImageScaling.None;
  
      if (button.DropDownItems.Count == maxBackForward)
        button.DropDownItems.RemoveAt(maxBackForward - 1);
  
      button.DropDownItems.Insert(0, backItem);
      button.Enabled = true;
    }
  }
}