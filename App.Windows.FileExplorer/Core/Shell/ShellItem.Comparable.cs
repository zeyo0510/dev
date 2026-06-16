using System;
/************************************************/
namespace ShellDll
{
  partial class ShellItem : IComparable
  {
    public int CompareTo(object obj)
    {
      ShellItem other = (ShellItem)obj;
      /************************************************/
      if (this.SortFlag != other.SortFlag)
        return ((this.SortFlag > other.SortFlag) ? 1 : -1);
      else if (this.IsDisk)
        return string.Compare(this.Path, other.Path);
      else
        return string.Compare(this.Text, other.Text);
    }
  }
}