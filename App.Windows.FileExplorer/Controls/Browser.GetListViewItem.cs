using System;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private ListViewItem GetListViewItem(string[] subItems, ShellItem shellItem) {
      ListViewItem listItem = new ListViewItem(shellItem.Text, shellItem.ImageIndex);
      listItem.Name = listItem.Text;
      listItem.Tag = shellItem;

      provider.ProviderItem = shellItem;

      if (shellItem.CanRead) {
        if (shellItem.IsFolder) {
          #region Folder Info

          for (int i = 1; i < fileView.Columns.Count; i++) {
            IColumnPlugin plugin = fileView.Columns[i].Tag as IColumnPlugin;

            try {
              subItems[i - 1] = plugin.GetFolderInfo(provider, fileView.Columns[i].Text, shellItem);
             } catch (Exception) {
              subItems[i - 1] = string.Empty;
            }
          }

          provider.ReleaseStorage();

          #endregion
        } else {
          #region File Info

          for (int i = 1; i < fileView.Columns.Count; i++) {
            IColumnPlugin plugin = fileView.Columns[i].Tag as IColumnPlugin;

            try {
                subItems[i - 1] = plugin.GetFileInfo(provider, fileView.Columns[i].Text, shellItem);
            } catch (Exception) {
              subItems[i - 1] = string.Empty;
            }
          }

          provider.ReleaseStream();

          #endregion
        }
      } else {
        for (int i = 1; i < fileView.Columns.Count; i++) {
          subItems[i - 1] = string.Empty;
        }
      }

      provider.ProviderItem = null;
      listItem.SubItems.AddRange(subItems);
      return listItem;
    }

  }
}