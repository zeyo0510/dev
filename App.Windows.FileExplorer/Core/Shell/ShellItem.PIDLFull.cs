using System;

namespace ShellDll
{
  partial class ShellItem
  {
    internal PIDL PIDLFull
    {
        get
        {
            PIDL pidlFull = new PIDL(PIDLRel.Ptr, true);
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