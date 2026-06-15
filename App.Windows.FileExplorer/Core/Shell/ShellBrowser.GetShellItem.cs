using System;
using System.Collections;

namespace ShellDll
{
  partial class ShellBrowser
  {
    internal ShellItem GetShellItem(PIDL pidlFull)
    {
        ShellItem current = DesktopItem;
        if (pidlFull.Ptr == IntPtr.Zero)
            return current;

        foreach (IntPtr pidlRel in pidlFull)
        {
            int index;
            if ((index = current.IndexOf(pidlRel)) > -1)
            {
                current = current[index];
            }
            else
            {
                current = null;
                break;
            }
        }

        return current;
    }
  }
}