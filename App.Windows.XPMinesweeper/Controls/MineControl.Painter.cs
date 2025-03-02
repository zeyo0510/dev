using System;
using System.Drawing;
using System.Windows.Forms;
using App.Windows.XPMinesweeper.Core;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineControl
  {
    protected override void OnPaint(PaintEventArgs e)
    {
      if (mines == null)
      {
        base.OnPaint(e);
        return;
      }

      if (mines.GameState == GameState.Processing || mines.GameState == GameState.NotStarted)
        Enabled = true;

      for (int i = 0; i < mines.Width; i++)
      {
        for (int j = 0; j < mines.Height; j++)
        {
          MineCell_Paint(this, new MineControlPaintEventArgs(e.Graphics, calcRect(e.ClipRectangle, i, j), i, j));
        }
      }
    }
    /************************************************/
    private Brush getBrush(int i)
    {
      switch (i)
      {
        case 1:  return brush1;
        case 2:  return brush2;
        case 3:  return brush3;
        case 4:  return brush4;
        case 5:  return brush5;
        case 6:  return brush6;
        case 7:  return brush7;
        case 8:  return brush8;
        default: return null;
      }
    }
    /************************************************/
    private void MineCell_Paint(object sender, MineControlPaintEventArgs e)
    {
      Rectangle rect = new Rectangle(e.ClipRectangle.Location, new Size(e.ClipRectangle.Size.Width - 1, e.ClipRectangle.Size.Height - 1));
      Graphics g = e.Graphics;

      if (rect == Rectangle.Empty || !g.IsVisible(rect))
        return;

      Mine mine = mines.mines[e.Y * mines.Width + e.X];
      switch(mine.MineStatus)
      {
        case MineStatus.HasMine:
        case MineStatus.NoMine:
          if (mine.MineStatus == MineStatus.HasMine && (mines.GameState != GameState.Processing && mines.GameState != GameState.NotStarted))
          {
            DrawFrame(g, rect);
            g.DrawImage(imgNotDiscovery, rect.Left + 1,  rect.Top + 1);
          }
          else
          {
            int offset = 0;
            if ((compareMouseButton(mouseButton, MouseButtons.Left) &&  activeRect == e.ClipRectangle) ||
              (compareMouseButton(mouseButton, mbLeftnRight) &&  Rectangle.Intersect(activeRect, getSmallerRect(e.ClipRectangle)) != Rectangle.Empty))
            {
              DrawFrame(g, rect);
              offset = 1;
            }
            else
              DrawButton(g, rect);
            if (mine.Doubt)
              g.DrawString("?", font, doubtBrush, rect.Left + 2 + offset, rect.Top + offset);
          }
          break;
        case MineStatus.MarkedRight:
          this.DrawButton(g, rect);  
          g.DrawImage(imgMarked, rect.Left + 2,  rect.Top + 2);
          break;
        case MineStatus.MarkedWrong:
          if (mines.GameState == GameState.Processing || mines.GameState == GameState.NotStarted)
          {
            DrawButton(g, rect);  
            g.DrawImage(imgMarked, rect.Left + 2,  rect.Top + 2);
          }
          else
          {
            DrawFrame(g, rect);
            g.DrawImage(imgMarkedWrong, rect.Left + 1,  rect.Top + 1);
          }
          break;
        case MineStatus.Exploded:
          g.FillRectangle(redBrush, e.ClipRectangle);
          DrawFrame(g, rect);
          g.DrawImage(imgNotDiscovery, rect.Left + 1,  rect.Top + 1);
          break;
        case MineStatus.Clear:
          DrawFrame(g, rect);
          int count = mine.MineCount;
          if  (count > 0)
            g.DrawString(count.ToString(), font, getBrush(count), rect.Left + 2, rect.Top);
          break;
      }
    }
    /************************************************/
    private void DrawFrame(Graphics g, Rectangle rect)
    {
      g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Left, rect.Bottom);
      g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Right, rect.Top);
    }
    /************************************************/
    private void DrawButton(Graphics g, Rectangle rect)
    {
      #region Top Border
      g.DrawLine(lightPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
      g.DrawLine(lightPen, rect.Left, rect.Top + 1, rect.Right - 2, rect.Top + 1);
      #endregion

      #region Bottom Border
      g.DrawLine(darkGrayPen, rect.Left + 1, rect.Bottom, rect.Right, rect.Bottom);
      g.DrawLine(darkGrayPen, rect.Left + 2, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
      #endregion

      #region Left Border
      g.DrawLine(lightPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
      g.DrawLine(lightPen, rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom - 2);
      #endregion

      #region Right Border
      g.DrawLine(darkGrayPen, rect.Right, rect.Top + 1, rect.Right, rect.Bottom);
      g.DrawLine(darkGrayPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom);
      #endregion
    }
  }
}