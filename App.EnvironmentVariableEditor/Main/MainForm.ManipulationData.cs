using System;
using System.Linq;
using System.Windows.Forms;
using App.EnvironmentVariableEditor.Dialogs;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  partial class MainForm
  {
    public void ManipulationData(ManipulationMode mode)
    {
      string key   = "";
      string value = "";
      /************************************************/
      ListViewItem item = ListView1.SelectedItems
    . Cast<ListViewItem>()
    . FirstOrDefault(_ => {
        bool retValue = false;
        /************************************************/
        retValue = (mode == ManipulationMode.Insert) ? false : retValue;
        retValue = (mode == ManipulationMode.Update) ? true  : retValue;
        retValue = (mode == ManipulationMode.Delete) ? true  : retValue;
        /************************************************/
        return retValue;
      });
      /************************************************/
      if (item != null)
      {
        key   = item.SubItems[0].Text;
        value = item.SubItems[1].Text;
      }
      /************************************************/
      AdjuestDialog dialog = new AdjuestDialog(key, value);
      {
        dialog.ManipulationMode = mode;
      }
      /************************************************/
      if (dialog.ShowDialog(this) != DialogResult.OK)
      {
        return;
      }
      /************************************************/
      key   = dialog.Key;
      value = dialog.Value;
      /************************************************/
      System.Diagnostics.Debug.WriteLine(string.Format("key: {0}, value: {1}", key, value));
    }
  }
}