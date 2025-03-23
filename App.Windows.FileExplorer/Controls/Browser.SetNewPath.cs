using System;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private bool SetNewPath(ShellItem oldItem, ShellItem newItem) {
      Cursor.Current = Cursors.WaitCursor;

      if (oldItem != newItem && newItem.Expand(true, false, Handle)) {
        ShellBrowser.UpdateCondition.ContinueUpdate = false;

        fileView.BeginUpdate();
        fileView.Items.Clear();
        fileView.ClearSelections();

        if (oldItem != null) {
          bool used = false;
          foreach (Browser br in ShellBrowser.Browsers) {
            if (!this.Equals(br) && oldItem.Equals(br.SelectedItem)) {
              used = true;
              break;
            }
          }

          if (!used)
            oldItem.Clear(true, false);
        }

        selectedItem = newItem;

        ListViewItem[] newListItemsArray = new ListViewItem[newItem.Count];
        string[] subItems = new string[fileView.Columns.Count - 1];
        for (int i = 0; i < newListItemsArray.Length; i++) {
            newListItemsArray[i] = GetListViewItem(subItems, newItem[i]);
        }
        fileView.SetSorting(true);
        fileView.Items.AddRange(newListItemsArray);
        fileView.SetSorting(false);

        fileView.EndUpdate();

        Cursor.Current = Cursors.Default;
        return true;
      } else {
        Cursor.Current = Cursors.Default;
        return (oldItem == newItem);
      }
    }
  }
}