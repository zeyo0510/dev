using System;
using System.Runtime.InteropServices;

namespace ShellDll
{
  partial class ShellItem
  {
    internal IShellFolder ShellFolder
    {
        get
        {
            if (UpdateShellFolder)
            {
                Marshal.ReleaseComObject(shellFolder);
                Marshal.Release(shellFolderPtr);

                if (ParentItem.ShellFolder.BindToObject(
                            PIDLRel.Ptr,
                            IntPtr.Zero,
                            ref WinAPI.IID_IShellFolder,
                            out shellFolderPtr) == WinAPI.S_OK)
                {
                    shellFolder = (IShellFolder)Marshal.GetTypedObjectForIUnknown(shellFolderPtr, typeof(IShellFolder));
                }

                UpdateShellFolder = false;
            }

            return shellFolder;
        }
    }
  }
}