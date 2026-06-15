using System;
using ShellDll;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  internal delegate void DragEnterEventHandler(object sender, DragEnterEventArgs e);
  /************************************************/
  internal class DragEnterEventArgs : EventArgs
  {
    public DragEnterEventArgs(ShellItem parent, Control dragStartControl)
    {
      this.Parent           = parent;
      this.DragStartControl = dragStartControl;
    }
    /************************************************/
    public ShellItem Parent
    {
      get; private set;
    }
    /************************************************/
    public Control DragStartControl
    {
      get; private set;
    }
  }
}