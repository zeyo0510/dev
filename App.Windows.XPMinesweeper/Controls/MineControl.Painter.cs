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

      for (int x = 0; x < this.mines.Width; x++)
      {
        for (int y = 0; y < this.mines.Height; y++)
        {
          Rectangle rect = this.calcRect(e.ClipRectangle, x, y);
          /************************************************/
          this.DrawMine(e.Graphics, rect, x, y);
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
    private void DrawMine(Graphics g, Rectangle rect, int x, int y)
    {
      rect = new Rectangle(rect.Location, new Size(rect.Size.Width - 1, rect.Size.Height - 1));
      /************************************************/
      if (rect == Rectangle.Empty || !g.IsVisible(rect))
        return;
      /************************************************/
      Mine mine = this.mines.mines[y * mines.Width + x];
      /************************************************/
      switch (mine.MineStatus)
      {
        case MineStatus.HasMine:
          if (mine.MineStatus == MineStatus.HasMine && (mines.GameState != GameState.Processing && mines.GameState != GameState.NotStarted))
          {
            DrawFrame(g, rect);
            g.DrawImage(imgNotDiscovery, rect.Left + 1,  rect.Top + 1);
          }
          else
          {
            int offset = 0;
            if ((compareMouseButton(mouseButton, MouseButtons.Left) &&  activeRect == rect) ||
              (compareMouseButton(mouseButton, mbLeftnRight) &&  Rectangle.Intersect(activeRect, getSmallerRect(rect)) != Rectangle.Empty))
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
        case MineStatus.NoMine:
          int offset2 = 0;
          if ((compareMouseButton(mouseButton, MouseButtons.Left) &&  activeRect == rect) ||
            (compareMouseButton(mouseButton, mbLeftnRight) &&  Rectangle.Intersect(activeRect, getSmallerRect(rect)) != Rectangle.Empty))
          {
            DrawFrame(g, rect);
            offset2 = 1;
          }
          else
            DrawButton(g, rect);
          if (mine.Doubt)
            g.DrawString("?", font, doubtBrush, rect.Left + 2 + offset2, rect.Top + offset2);
          break;
        case MineStatus.MarkedRight: // 標記旗幟，且有炸彈
          this.DrawButton(g, rect);  
          g.DrawImage(imgMarked, rect.Left + 2,  rect.Top + 2);
          break;
        case MineStatus.MarkedWrong: // 標記旗幟，未有炸彈
          // 判斷遊戲是否進行中。若進行中，則不應公開
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
        case MineStatus.Exploded: // 踩到地雷
          g.FillRectangle(redBrush, rect);
          DrawFrame(g, rect);
          g.DrawImage(imgNotDiscovery, rect.Left + 1,  rect.Top + 1);
          break;
        case MineStatus.Clear: // 地雷被清除
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
      g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Left, rect.Bottom); // Left
      g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Right, rect.Top); // Top
    }
    /************************************************/
    private void DrawButton(Graphics g, Rectangle rect)
    {
      g.DrawLine(lightPen, rect.Left, rect.Top + 0, rect.Right - 1, rect.Top + 0); // Top
      g.DrawLine(lightPen, rect.Left, rect.Top + 1, rect.Right - 2, rect.Top + 1); // Top

      g.DrawLine(darkGrayPen, rect.Left + 1, rect.Bottom - 0, rect.Right, rect.Bottom - 0); // Bottom
      g.DrawLine(darkGrayPen, rect.Left + 2, rect.Bottom - 1, rect.Right, rect.Bottom - 1); // Bottom

      g.DrawLine(lightPen, rect.Left + 0, rect.Top, rect.Left + 0, rect.Bottom - 1); // Left
      g.DrawLine(lightPen, rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom - 2); // Left

      g.DrawLine(darkGrayPen, rect.Right - 0, rect.Top + 1, rect.Right - 0, rect.Bottom); // Right
      g.DrawLine(darkGrayPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom); // Right
    }
  }
}