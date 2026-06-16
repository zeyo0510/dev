using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ShellDll
{
  partial class ShellItem
  {
    internal int ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex = value; }
        }
  }
}