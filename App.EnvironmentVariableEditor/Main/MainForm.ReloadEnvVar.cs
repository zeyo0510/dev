using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  partial class MainForm
  {
    public void ReloadEnvVar()
    {
      this.ListView1.Items.Clear();
      /************************************************/
      Environment.GetEnvironmentVariables(this.CurrentEnvironmentVariable)
    . Cast<DictionaryEntry>()
    . ToList()
    . ForEach((_) => {
        string key   = _.Key.ToString();
        string value = _.Value.ToString();
        /************************************************/
        this.ListView1.Items.Add(new ListViewItem(
          new string[] { key, value }
        ));
      });
      /************************************************/
      this.ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    }
  }
}