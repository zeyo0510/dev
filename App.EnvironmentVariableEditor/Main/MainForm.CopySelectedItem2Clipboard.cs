using System;
using System.Linq;
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
      this.ListView1.SelectedItems
    . Cast<ListViewItem>()
    . ToList()
    . ForEach((_) => {
        string a = _.SubItems[0].Text;
        string b = _.SubItems[1].Text;
        /************************************************/
        data.AppendLine(string.Format("{0}\t{1}", a, b));
      });
      /************************************************/
      Clipboard.SetText(data.ToString());
    }
  }
}