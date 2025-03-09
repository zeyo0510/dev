using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineLED
  {
    private static Dictionary<char, Bitmap> img = null;
    /************************************************/
    static MineLED()
    {
      MineLED.img = new Dictionary<char, Bitmap>();
      /************************************************/
      MineLED.img['-'] = MineLED.DrawBitmap("      G".Replace(" ", ""));
      MineLED.img[' '] = MineLED.DrawBitmap("       ".Replace(" ", ""));
      MineLED.img['9'] = MineLED.DrawBitmap("ABCD FG".Replace(" ", ""));
      MineLED.img['8'] = MineLED.DrawBitmap("ABCDEFG".Replace(" ", ""));
      MineLED.img['7'] = MineLED.DrawBitmap("ABC    ".Replace(" ", ""));
      MineLED.img['6'] = MineLED.DrawBitmap("A CDEFG".Replace(" ", ""));
      MineLED.img['5'] = MineLED.DrawBitmap("A CD FG".Replace(" ", ""));
      MineLED.img['4'] = MineLED.DrawBitmap(" BC  FG".Replace(" ", ""));
      MineLED.img['3'] = MineLED.DrawBitmap("ABCD  G".Replace(" ", ""));
      MineLED.img['2'] = MineLED.DrawBitmap("AB DE G".Replace(" ", ""));
      MineLED.img['1'] = MineLED.DrawBitmap(" BC    ".Replace(" ", ""));
      MineLED.img['0'] = MineLED.DrawBitmap("ABCDEF ".Replace(" ", ""));
    }
    /************************************************/
    private static Bitmap DrawBitmap(string s)
    {
      int w = 13;
      int h = 23;
      /************************************************/
      StringBuilder map = new StringBuilder();
      {
        map.Append("             ");
        map.Append("  AaAaAaAaA  ");
        map.Append(" f AaAaAaA b ");
        map.Append(" Ff AaAaA bB ");
        map.Append(" fFf     bBb ");
        map.Append(" FfF     BbB ");
        map.Append(" fFf     bBb ");
        map.Append(" FfF     BbB ");
        map.Append(" fFf     bBb ");
        map.Append(" Ff       bB ");
        map.Append(" f GgGgGgG b ");
        map.Append("  GgGgGgGgG  ");
        map.Append(" e GgGgGgG c ");
        map.Append(" Ee       cC ");
        map.Append(" eEe     cCc ");
        map.Append(" EeE     CcC ");
        map.Append(" eEe     cCc ");
        map.Append(" EeE     CcC ");
        map.Append(" eEe     cCc ");
        map.Append(" Ee DdDdD cC ");
        map.Append(" e DdDdDdD c ");
        map.Append("  DdDdDdDdD  ");
        map.Append("             ");
      }
      /************************************************/
      Bitmap retValue = new Bitmap(w, h);
      /************************************************/
      using (Graphics g = Graphics.FromImage(retValue))
      {
        using (Brush brush = new SolidBrush(Color.Black))
        {
          g.FillRectangle(brush, 0, 0, w, h);
        }
      }
      /************************************************/
      using (Graphics g = Graphics.FromImage(retValue))
      {
        using (Brush lightBrush = new SolidBrush(Color.Red))
        {
          using (Brush darkBrush = new SolidBrush(Color.Maroon))
          {
            for (int y = 0; y < h; y++)
            {
              for (int x = 0; x < w; x++)
              {
                char n = map.ToString()[(x * 01) + (y * 13)];
                /************************************************/
                if (char.IsLower(n))
                {
                  g.FillRectangle(darkBrush, x, y, 1, 1);
                }
                /************************************************/
                if (s.Contains(char.ToUpper(n)))
                {
                  g.FillRectangle(lightBrush, x, y, 1, 1);
                }
              }
            }
          }
        }
      }
      /************************************************/
      return retValue;
    }
  }
}