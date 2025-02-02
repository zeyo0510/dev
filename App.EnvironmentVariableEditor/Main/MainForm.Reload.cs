using System;
using System.Collections;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  partial class MainForm
  {
    public void Reload()
    {
      this.ListView1.Items.Clear();
      /************************************************/
      foreach (DictionaryEntry _ in Environment.GetEnvironmentVariables(this.CurrentEnvironmentVariable))
      {
        this.ListView1.Items.Add(new ListViewItem(
          new string[] { _.Key.ToString(), _.Value.ToString() }
        ));
        /************************************************/
        this.ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
      }
    }
  }
}