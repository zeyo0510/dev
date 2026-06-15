using System;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    internal void ResetSpecialView()
    {
      if (fileView.Alignment != ListViewAlignment.Top)
      {
        fileView.Alignment = ListViewAlignment.Top;
      }
      CurrentViewPlugin = null;
    }
  }
}