using System;
using System.Drawing;
using System.IO;
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
      SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer |
        ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.Selectable, false);
      BackColor = Color.Silver;
      Width = cellSize * 9;
      Height = cellSize * 9;
    }
    /************************************************/
    private Bitmap getBitmap(string fileName)
    {
      Image img = Image.FromStream(GetResource(fileName));
      Bitmap bmp = new Bitmap(img);
      img.Dispose();
      img = null;
      bmp.MakeTransparent(bmp.GetPixel(1, 1));
      return bmp;
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

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter (e);

      setCellState(MouseButtons.None);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave (e);

      setCellState(MouseButtons.None);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);

      setCellState(mouseButton, getRect(e.X, e.Y));
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown (e);

      Mine m = getMine(e.X, e.Y);
      if (m == null)
        return;

      twoKeyDoubleClick = false;
      setCellState(e.Button, getRect(e.X, e.Y));
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp (e);

      Mine m = getMine(e.X, e.Y);
      if (m == null || !Enabled)
        return;

      if (mouseButton != MouseButtons.None && (mines.GameState == GameState.Processing || mines.GameState == GameState.NotStarted))
      {
        if (compareMouseButton(mouseButton, mbLeftnRight))
        {
          m.QuickDig();
          AfterDigOrMark(this, EventArgs.Empty);
        }
        else if (compareMouseButton(e.Button, MouseButtons.Left) && !twoKeyDoubleClick)
        {
          m.Dig(true);
          AfterDigOrMark(this, EventArgs.Empty);
        }
        else if (compareMouseButton(e.Button, MouseButtons.Right) && !twoKeyDoubleClick)
        {
          m.Mark();
          AfterDigOrMark(this, EventArgs.Empty);
        }

        if (mines.GameState != GameState.Processing && mines.GameState != GameState.NotStarted)
        {
          Enabled = false;
          Refresh();
        }
      }

      if (compareMouseButton(mouseButton, mbLeftnRight))
        mouseButton = e.Button == MouseButtons.Left ? MouseButtons.Right : MouseButtons.Left;
      else
        mouseButton = MouseButtons.None;
      setCellState(mouseButton, getRect(e.X, e.Y));
    }

    private Rectangle getRect(int x, int y)
    {
      Rectangle rect = calcRect(ClientRectangle, x / cellSize, y / cellSize);
      return rect;
    }

    private Mine getMine(int x, int y)
    {
      if (x > cellSize * mines.Width || y > cellSize * mines.Height || x < 0 || y < 0)
        return null;
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

    /// <summary>
    /// 植訧埭DLL笢腕剒猁腔訧埭
    /// </summary>
    public Stream GetResource(string fileName)
    {
      if (fileName == null || fileName.Length == 0)
        return null;

      Stream stream = null;
      Type resourceType = this.GetType();
      string resourceName = "App.Windows.XPMinesweeper.Resources." + fileName.Replace("\\", ".");
      System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(resourceType);
      if (assembly == null)
        throw new MineException("拸楊蚾婥訧埭恅璃: " + resourceType.Namespace + ".dll");
      stream = System.Reflection.Assembly.GetAssembly(resourceType).GetManifestResourceStream(resourceName);
      if (stream == null)
        throw new MineException("拸楊腕訧埭: " + fileName);
      return stream;
    }

    public event EventHandler DigOrMark;

    private void AfterDigOrMark(object sender, EventArgs e)
    {
      if (DigOrMark != null)
        DigOrMark(this, e);
    }
  }
}
