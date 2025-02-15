using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  partial class MainForm
  {
    public void ExportData()
    {
      SaveFileDialog dialog = new SaveFileDialog();
      {
        dialog.Filter = "Tab separated values|*.tsv|All files|*.*";
      }
      /************************************************/
      if (dialog.ShowDialog(this) != DialogResult.OK)
      {
        if (MessageBox.Show("User canceled!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
        {
          return;
        }
      }
      /************************************************/
      StringBuilder data = new StringBuilder();
      /************************************************/
      this.ListView1.Items
    . Cast<ListViewItem>()
    . ToList()
    . ForEach((_) => {
        string a = _.SubItems[0].Text;
        string b = _.SubItems[1].Text;
        /************************************************/
        data.AppendLine(string.Format("{0}\t{1}", a, b));
      });
      /************************************************/
      File.WriteAllText(dialog.FileName, data.ToString(), Encoding.UTF8);
      /************************************************/
      MessageBox.Show("Export successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
  }
}