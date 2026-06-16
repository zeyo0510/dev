using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellItem : IComparable
  {
    public int CompareTo(object obj)
    {
        ShellItem other = (ShellItem)obj;

        if (SortFlag != other.SortFlag)
            return ((SortFlag > other.SortFlag) ? 1 : -1);
        else if (IsDisk)
            return string.Compare(Path, other.Path);
        else
            return string.Compare(Text, other.Text);
    }
  }
}