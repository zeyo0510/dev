using System;
using System.Linq;
using System.Windows.Forms;
/************************************************/
namespace Little_Disk_Cleaner
{
  partial class MainForm
  {
    public void InvertSelectionItem()
    {
      this.listview1.Items
    . Cast<ListViewItem>()
    . ToList()
    . ForEach((_) => {
        _.Selected = !_.Selected;
      });
    }
  }
}