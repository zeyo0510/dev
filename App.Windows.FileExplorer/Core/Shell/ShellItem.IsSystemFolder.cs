using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellItem
  {
    public bool IsSystemFolder { get { return type == browser.SystemFolderName; } }
  }
}