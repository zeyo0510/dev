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
      string old_key   = "";
      string old_value = "";
      string new_key   = "";
      string new_value = "";
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
        old_key   = item.SubItems[0].Text;
        old_value = item.SubItems[1].Text;
      }
      /************************************************/
      AdjuestDialog dialog = new AdjuestDialog(old_key, old_value);
      {
        dialog.ManipulationMode = mode;
      }
      /************************************************/
      if (dialog.ShowDialog(this) != DialogResult.OK)
      {
        return;
      }
      /************************************************/
      new_key   = dialog.Key;
      new_value = dialog.Value;
      /************************************************/
      if (mode == ManipulationMode.Insert)
      {
        Environment.SetEnvironmentVariable(new_key, new_value, this.CurrentEnvironmentVariable);
      }
      if (mode == ManipulationMode.Update)
      {
        Environment.SetEnvironmentVariable(old_key, ""       , this.CurrentEnvironmentVariable);
        Environment.SetEnvironmentVariable(new_key, new_value, this.CurrentEnvironmentVariable);
      }
      if (mode == ManipulationMode.Delete)
      {
        Environment.SetEnvironmentVariable(old_key, "", this.CurrentEnvironmentVariable);
      }
      /************************************************/
      this.ReloadEnvVar();
    }
  }
}