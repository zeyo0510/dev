using System;
using System.Linq;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.DotnetFrameworkViewer.Main
{
  partial class MainForm
  {
    public void SelectNoneItem()
    {
      this.listview1.Items
    . Cast<ListViewItem>()
    . ToList()
    . ForEach((_) => {
        _.Selected = false;
      });
    }
  }
}