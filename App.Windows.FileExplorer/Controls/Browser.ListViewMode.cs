using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    [Browsable(false)]
    public View ListViewMode {
      get { return fileView.View; }
      set {
        if (currentViewPlugin != null && value != View.SmallIcon)
          ResetSpecialView();

        fileView.View = value;
      }
    }
  }
}