using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace FileBrowser
{
  internal partial class BrowserComboBox : ToolStripComboBox
  {
    private int indentWidth = 10;
    /************************************************/
    private ComboEditWindow editWindow;
    /************************************************/
    public BrowserComboBox()
    {
      ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
      ComboBox.DrawItem += new DrawItemEventHandler(ComboBox_DrawItem);
      ComboBox.HandleCreated += new EventHandler(ComboBox_HandleCreated);
      ComboBox.MouseDown += new MouseEventHandler(ComboBox_MouseDown);
      ComboBox.MouseMove += new MouseEventHandler(ComboBox_MouseMove);
      ComboBox.MouseClick += new MouseEventHandler(ComboBox_MouseClick);
      ComboBox.DropDown += new EventHandler(ComboBox_DropDown);
    }
    /************************************************/
    protected override void OnSelectedIndexChanged(EventArgs e)
    {
      base.OnSelectedIndexChanged(e);

      if (ComboBox.SelectedIndex > -1)
        this.CurrentItem = ComboBox.SelectedItem as BrowserComboItem;
    }
    /************************************************/
    private void ComboBox_HandleCreated(object sender, EventArgs e)
    {
      editWindow = new ComboEditWindow(this);
    }
    /************************************************/
    private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
    {
      if (e.Index == -1)
        return;
      else {
        BrowserComboItem item = (BrowserComboItem)Items[e.Index];

        e.DrawBackground();
        e.DrawFocusRectangle();

        int indentOffset = indentWidth * item.Indent;

        int imageYOffset = (e.Bounds.Height - item.Image.Height) / 2;
        Point imagePoint = new Point(
            e.Bounds.Left + indentOffset + 2, 
            e.Bounds.Top + imageYOffset);

        Size textSize = e.Graphics.MeasureString(item.Text, Font).ToSize();
        int textYOffset = (e.Bounds.Height - textSize.Height) / 2;
        Point textPoint = new Point(
            e.Bounds.Left + item.Image.Width + indentOffset + 2, 
            e.Bounds.Top + textYOffset);

        e.Graphics.DrawIcon(item.Image, imagePoint.X, imagePoint.Y);
        e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), textPoint);
      }
    }
    /************************************************/
    private void ComboBox_MouseMove(object sender, MouseEventArgs e)
    {
      if (editWindow.ImageRect.Contains(e.Location)) {
        Cursor.Current = Cursors.Hand;
      } else if (Cursor.Current == Cursors.Hand) {
        Cursor.Current = Cursors.Default;
      }
    }
    /************************************************/
    private void ComboBox_MouseDown(object sender, MouseEventArgs e)
    {
      if (editWindow.ImageRect.Contains(e.Location))
        Cursor.Current = Cursors.Hand;
    }  
    /************************************************/
    private void ComboBox_MouseClick(object sender, MouseEventArgs e)
    {
      if (editWindow.ImageRect.Contains(e.Location))
      {
        SelectAll();
      }
    }
    
    /************************************************/
    private void ComboBox_DropDown(object sender, EventArgs e)
    {
      int width = 0;
      Graphics gfx = ComboBox.CreateGraphics();
      foreach (BrowserComboItem item in Items) {
          int itemWidth =
              gfx.MeasureString(item.Text, Font).ToSize().Width +
              item.Image.Width +
              indentWidth * item.Indent +
              (Items.Count > MaxDropDownItems ? SystemInformation.VerticalScrollBarWidth : 0);

          if (itemWidth > width)
              width = itemWidth;
      }

      if (width > this.Width)
        ComboBox.DropDownWidth = width;
      else
        ComboBox.DropDownWidth = this.Width;
    }
  }
}