using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellItem
  {
    internal ShellItemCollection SubFiles { get { return subFiles; } }
  }
}