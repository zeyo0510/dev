using System;
using ShellDll;
using System.Windows.Forms;

namespace FileBrowser
{
  internal delegate void DragEnterEventHandler(object sender, DragEnterEventArgs e);

  internal class DragEnterEventArgs : EventArgs
  {
    public DragEnterEventArgs(ShellItem parent, Control dragStartControl)
    {
      this.parent = parent;
      this.dragStartControl = dragStartControl;
    }

    private ShellItem parent;
    public ShellItem Parent
    {
      get
      {
        return parent;
      }
    }

    private Control dragStartControl;
    public Control DragStartControl
    {
      get
      {
        return dragStartControl;
      }
    }
  }
}