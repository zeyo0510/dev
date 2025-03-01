using System;
using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public class MineControlPaintEventArgs: EventArgs
  {
    private int m_X, m_Y;
    private Rectangle clipRectangle;
    private Graphics g;
    /************************************************/
    public Graphics Graphics
    {
      get
      {
        return g;
      }
    }
    /************************************************/
    public Rectangle ClipRectangle
    {
      get
      {
        return clipRectangle;
      }
    }
    /************************************************/
    public int X
    {
      get
      {
        return m_X;
      }
    }
    /************************************************/
    public int Y
    {
      get
      {
        return m_Y;
      }
    }
    /************************************************/
    public MineControlPaintEventArgs(Graphics graphics, Rectangle clipRect, int x, int y)
    {
      m_X = x;
      m_Y = y;
      clipRectangle = clipRect;
      g = graphics;
    }
  }
}
