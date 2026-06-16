using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellItem
  {
    internal IShellFolder ShellFolder
    {
        get
        {
            if (updateShellFolder)
            {
                Marshal.ReleaseComObject(shellFolder);
                Marshal.Release(shellFolderPtr);

                if (ParentItem.ShellFolder.BindToObject(
                            pidlRel.Ptr,
                            IntPtr.Zero,
                            ref WinAPI.IID_IShellFolder,
                            out shellFolderPtr) == WinAPI.S_OK)
                {
                    shellFolder = (IShellFolder)Marshal.GetTypedObjectForIUnknown(shellFolderPtr, typeof(IShellFolder));
                }

                updateShellFolder = false;
            }

            return shellFolder;
        }
    }
  }
}