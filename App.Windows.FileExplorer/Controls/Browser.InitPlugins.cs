using System;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    private void InitPlugins() {
      if (PluginWrapper == null)
        PluginWrapper = new BrowserPluginWrapper();

      while (fileView.Columns.Count > 1)
        fileView.Columns.RemoveAt(1);

      foreach (IColumnPlugin columnPlugin in PluginWrapper.ColumnPlugins) {
        foreach (string columnName in columnPlugin.ColumnNames) {
          MenuItem item = new MenuItem(columnName);
          item.Checked = true;
          item.Click += new EventHandler(ColumnContextItem_Click);
          columnContextMenu.MenuItems.Add(item);

          ColumnHeader header = new ColumnHeader();
          header.TextAlign = columnPlugin.GetAlignment(columnName);
          header.Text = columnName;
          header.Name = header.Text;
          header.Tag = columnPlugin;
          header.Width = 0;

          fileView.Columns.Add(header);
        }
      }

      fileView.ColumnHeaderContextMenu = columnContextMenu;
    }
  }
}