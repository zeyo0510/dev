using System;
using System.Windows.Forms;
using ShellDll;

namespace FileBrowser
{
  partial class BrowserTVDropWrapper
  {
    public int DragDrop(IntPtr pDataObj, WinAPI.MK grfKeyState, WinAPI.POINT pt, ref DragDropEffects pdwEffect)
    {
        OnDrop(new DropEventArgs(mouseButtons, br.FolderView));

        if (!((mouseButtons & WinAPI.MK.RBUTTON) != 0 ||
              grfKeyState == WinAPI.MK.CONTROL || 
              grfKeyState == WinAPI.MK.ALT || 
              grfKeyState == (WinAPI.MK.CONTROL | WinAPI.MK.SHIFT)) && ShellItem.Equals(parentDragItem, parentDropItem))
        {
            ResetDrop();                
            ReleaseCom();
            pdwEffect = DragDropEffects.None;

            if (dropHelper != null)
                dropHelper.DragLeave();

            return WinAPI.S_OK;
        }

        ResetDrop();
        if (dropTarget != null)
        {
            dropTarget.DragDrop(pDataObj, grfKeyState, pt, ref pdwEffect);

            ReleaseCom();
            dropDataObject = IntPtr.Zero;
        }

        if (dropHelper != null)
            dropHelper.Drop(pDataObj, ref pt, pdwEffect);

        return WinAPI.S_OK;
    }
  }
}