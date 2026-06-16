using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellItem
  {
    internal bool UpdateShellFolder
    {
        get { return updateShellFolder; }
        set { updateShellFolder = value; }
    }
  }
}