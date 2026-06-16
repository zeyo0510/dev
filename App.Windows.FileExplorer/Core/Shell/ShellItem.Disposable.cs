using System;
using System.Runtime.InteropServices;

namespace ShellDll
{
  partial class ShellItem : IDisposable
  {
    private bool disposed = false;
    
    void IDisposable.Dispose()
    {
        if (!disposed)
        {
            DisposeShellItem();
            GC.SuppressFinalize(this);
        }
    }
    
    private void DisposeShellItem()
    {
        disposed = true;

        if (ShellFolder != null)
        {
            Marshal.ReleaseComObject(ShellFolder);
            shellFolder = null;
        }

        if (shellFolderPtr != IntPtr.Zero)
        {
            try
            {
                Marshal.Release(shellFolderPtr);
            }
            catch (Exception) { }
            finally
            {
                shellFolderPtr = IntPtr.Zero;
            }
        }

        PIDLRel.Free();
    }

  }
}