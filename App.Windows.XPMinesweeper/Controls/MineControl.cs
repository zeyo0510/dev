using System;
using System.Drawing;
using System.Windows.Forms;
using App.Windows.XPMinesweeper.Core;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public partial class MineControl : Control
  {
    private const int cellSize = 16;
    private Color gray = Color.Silver;
    private Color darkGray = Color.Gray;
    private Brush grayBrush, darkGrayBrush, brush1, brush2, brush3, brush4, brush5, brush6, brush7, brush8, doubtBrush, redBrush;
    private Pen lightPen,  darkGrayPen;
    private Font font;
    private Bitmap imgMarked, imgNotDiscovery, imgMarkedWrong;
    /************************************************/
    public MineControl()
    {
      this.InitializeComponent();
      /************************************************/
      SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer |
        ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.Selectable, false);
    }
    /************************************************/
    private Rectangle calcRect(Rectangle rect, int x, int y)
    {
      Rectangle result = new Rectangle(ClientRectangle.Left + x * cellSize, ClientRectangle.Top + y * cellSize, cellSize, cellSize);
      if (rect == ClientRectangle || Rectangle.Intersect(result, rect) != Rectangle.Empty)
        return result;
      else
        return Rectangle.Empty;
    }

    private MouseButtons mouseButton = MouseButtons.None;
    private Rectangle activeRect = Rectangle.Empty;
    private Rectangle prevRect = Rectangle.Empty;

    private static bool compareMouseButton(MouseButtons mb1, MouseButtons mb2)
    {
      return ((mb1 & mb2) == mb2);
    }

    private static Rectangle getSmallerRect(Rectangle rect)
    {
      return new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4);
    }

    const MouseButtons mbLeftnRight = MouseButtons.Left | MouseButtons.Right;

    private Rectangle expandRect(Rectangle rect)
    {
      if (rect.Width == cellSize)
      {
        rect = new Rectangle(rect.X - cellSize, rect.Y - cellSize, rect.Width + cellSize * 2, rect.Height + cellSize * 2);
        return Rectangle.Intersect(rect, ClientRectangle);
      }
      else
        return rect;
    }

    private bool twoKeyDoubleClick;

    private void setCellState(MouseButtons mb, Rectangle rect)
    {
      if (mb != MouseButtons.None && compareMouseButton(mouseButton | mb, mbLeftnRight))
      {
        twoKeyDoubleClick = true;
        mouseButton = mbLeftnRight;
        rect = expandRect(rect);
      }
      else
        mouseButton = mb;
      if (compareMouseButton(mouseButton, MouseButtons.Left) || compareMouseButton(mouseButton, mbLeftnRight))
      {
        if (rect !=Rectangle.Empty && rect != activeRect)
        {
          prevRect = activeRect;
          activeRect = rect;
          Invalidate(prevRect);
        }
        Invalidate(activeRect);
      }
      else
      {
        if (prevRect != Rectangle.Empty)
          Invalidate(prevRect);
        if (activeRect != Rectangle.Empty)
          Invalidate(activeRect);
        prevRect = Rectangle.Empty;
        activeRect = Rectangle.Empty;
      }
    }

    private void setCellState(MouseButtons mb)
    {
      setCellState(mb, Rectangle.Empty);
    }

    private Rectangle getRect(int x, int y)
    {
      x = x / cellSize;
      y = y / cellSize;
      /************************************************/
      Rectangle rect = calcRect(base.ClientRectangle, x, y);
      /************************************************/
      return rect;
    }

    private Mine getMine(int x, int y)
    {
      if (x < 0)
      {
        return null;
      }
      if (x > cellSize * mines.Width)
      {
        return null;
      }
      if (y < 0)
      {
        return null;
      }
      if (y > cellSize * mines.Height)
      {
        return null;
      }
      /************************************************/
      int i = y / cellSize * mines.Width + x / cellSize;
      if (i >= 0 && i < mines.mines.Length)
        return mines.mines[i];
      else
        return null;
    }

    private void mineStatusChange(object sender, MineStatusChangeEventArgs e)
    {
      if (e.Mine == null)
        Refresh();
      else
      {
        Rectangle rect = calcRect(ClientRectangle, e.Mine.Index % mines.Width, e.Mine.Index / mines.Width);
        Invalidate(rect);
        Update();
      }
    }

    public void AdjustSize()
    {
      if (Dock != DockStyle.Fill && mines != null)
      {
        Width = cellSize * mines.Width;
        Height = cellSize * mines.Height;
      }
    }

    public event EventHandler DigOrMark;

    private void AfterDigOrMark(object sender, EventArgs e)
    {
      if (DigOrMark != null)
        DigOrMark(this, e);
    }
  }
}
