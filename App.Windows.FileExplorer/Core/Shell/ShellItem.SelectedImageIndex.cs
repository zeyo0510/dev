using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellItem
  {
    internal int SelectedImageIndex
    {
        get { return selectedImageIndex; }
        set { selectedImageIndex = value; }
    }

  }
}