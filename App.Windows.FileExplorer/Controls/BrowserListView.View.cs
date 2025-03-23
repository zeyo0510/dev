using System;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class BrowserListView
  {
    public new View View
    {
      get
      {
        return base.View;
      }
      set
      {
        base.View = value;

        if (value == View.Details) {
          foreach (ColumnHeader col in Columns)
            if (col.Width == 0)
              col.Width = 120;
        }
      }
    }
  }
}