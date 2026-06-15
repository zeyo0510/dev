using System;
using ShellDll;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  internal delegate void DropEventHandler(object sender, DropEventArgs e);
  /************************************************/
  internal class DropEventArgs : EventArgs
  {
    public DropEventArgs(WinAPI.MK mouseButtons, Control dragStartControl)
    {
      this.MouseButtons     = mouseButtons;
      this.DragStartControl = dragStartControl;
    }
    /************************************************/
    public WinAPI.MK MouseButtons
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