using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.DotnetFrameworkViewer.Main
{
  partial class MainForm
  {
    public void CopyItem()
    {
      StringBuilder buffer = new StringBuilder();
      /************************************************/
      this.listview1.SelectedItems
    . Cast<ListViewItem>()
    . ToList()
    . ForEach((_) => {
        string a = _.SubItems[0].Text;
        string b = _.SubItems[1].Text;
        string c = _.SubItems[2].Text;
        string d = _.SubItems[3].Text;
        /************************************************/
        buffer.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}", a, b, c, d));
      });
      /************************************************/
      Clipboard.SetText(buffer.ToString());
    }
  }
}