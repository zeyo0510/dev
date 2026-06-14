using System;
using ShellDll;
using System.Windows.Forms;

namespace FileBrowser
{
  internal delegate void DropEventHandler(object sender, DropEventArgs e);

  internal class DropEventArgs : EventArgs
  {
    private WinAPI.MK mouseButtons;
    private Control dragStartControl;

    public DropEventArgs(WinAPI.MK mouseButtons, Control dragStartControl)
    {
      this.mouseButtons = mouseButtons;
      this.dragStartControl = dragStartControl;
    }

    public WinAPI.MK MouseButtons
    {
      get
      {
        return mouseButtons;
      }
    }
    
    public Control DragStartControl
    {
      get
      {
        return dragStartControl;
      }
    }
  }
}