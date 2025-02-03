using System;
using System.Text;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  partial class MainForm
  {
    public void CopySelectedItem2Clipboard()
    {
      StringBuilder data = new StringBuilder();
      /************************************************/
      foreach (ListViewItem _ in this.ListView1.SelectedItems)
      {
        string a = _.SubItems[0].Text;
        string b = _.SubItems[1].Text;
        /************************************************/
        data.AppendLine(string.Format("{0}\t{1}", a, b));
      }
      /************************************************/
      Clipboard.SetText(data.ToString());
    }
  }
}