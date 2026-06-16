using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellItem
  {
    internal PIDL PIDLFull
    {
        get
        {
            PIDL pidlFull = new PIDL(pidlRel.Ptr, true);
            ShellItem current = ParentItem;
            while (current != null)
            {
                pidlFull.Insert(current.PIDLRel.Ptr);
                current = current.ParentItem;
            }
            return pidlFull;
        }
    }

  }
}