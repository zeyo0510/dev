using System;
using System.Linq;
using System.Windows.Forms;
using App.Windows.DotnetFrameworkViewer.Core;
/************************************************/
namespace App.Windows.DotnetFrameworkViewer.Main
{
  partial class MainForm
  {
    public void BuildItem()
    {
      this.listview1.Items.Clear();
      /************************************************/
      DotnetFramework.Items
    . ToList()
    . ForEach((_) => {
        string a = _.Name;
        string b = _.Version;
        string c = _.ServicePack;
        string d = _.Install;
        /************************************************/
        ListViewItem item = new ListViewItem(new string[] { a, b, c, d });
        {
          item.Tag = _;
        }
        /************************************************/
        this.listview1.Items.Add(item);
      });
      /************************************************/
      this.listview1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
      /************************************************/
      this.listview1.Sort();
    }
  }
}