using System;
using System.Drawing;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  internal class BrowserComboItem
  {
    public BrowserComboItem(ShellItem shellItem, int indent)
    {
      this.ShellItem = shellItem;
      this.Indent = indent;
      this.Image = ShellImageList.GetIcon(shellItem.ImageIndex, true);
    }
    /************************************************/
    public ShellItem ShellItem
    {
      get; private set;
    }
    /************************************************/
    public int Indent
    {
      get; private set;
    }
    /************************************************/
    public Icon Image
    {
      get; private set;
    }
    /************************************************/
    public string Text
    {
      get
      {
        return this.ShellItem.Text;
      }
    }
    /************************************************/
    public override string ToString()
    {
      return this.ShellItem.Path;
    }
  }
}