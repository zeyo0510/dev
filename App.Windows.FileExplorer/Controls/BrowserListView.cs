using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  internal partial class BrowserListView : ListView
  {
    private int columnHeight = 0;
    /************************************************/
    private BrowserListSorter sorter;
    /************************************************/
    public BrowserListView()
    {
      OwnerDraw = true;

      HandleCreated += new EventHandler(BrowserListView_HandleCreated);
      selectedOrder = new ArrayList();
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);

      DrawItem += new DrawListViewItemEventHandler(BrowserListView_DrawItem);
      DrawSubItem += new DrawListViewSubItemEventHandler(BrowserListView_DrawSubItem);
      DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(BrowserListView_DrawColumnHeader);

      this.Alignment = ListViewAlignment.Left;
      sorter = new BrowserListSorter();
    }
    /************************************************/
    void BrowserListView_DrawItem(object sender, DrawListViewItemEventArgs e)
    {
      e.DrawDefault = true;
    }
    /************************************************/
    void BrowserListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
    {
      e.DrawDefault = true;
    }
    /************************************************/
    void BrowserListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
    {
      e.DrawDefault = true;
      columnHeight = e.Bounds.Height;
    }
    /************************************************/
    protected override void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
    {
      if (e.IsSelected)
        selectedOrder.Insert(0, e.Item);
      else
        selectedOrder.Remove(e.Item);
      base.OnItemSelectionChanged(e);
    }
    /************************************************/
    protected override void WndProc(ref Message m) {
      if (this.View == View.Details && columnHeaderContextMenu != null && (int)m.Msg == (int)WinAPI.WM.CONTEXTMENU) {
        if (suspendHeaderContextMenu)
          suspendHeaderContextMenu = false;
        else {
          int x = (int)ShellHelper.LoWord(m.LParam);
          int y = (int)ShellHelper.HiWord(m.LParam);
          Point clientPoint = PointToClient(new Point(x, y));
          
          if (clientPoint.Y <= columnHeight)
            columnHeaderContextMenu.Show(this, clientPoint);
        }

        return;
      }

      base.WndProc(ref m);
    }
    /************************************************/
    void BrowserListView_HandleCreated(object sender, EventArgs e)
    {
      ShellImageList.SetSmallImageList(this);
      ShellImageList.SetLargeImageList(this);
    }
  }
}