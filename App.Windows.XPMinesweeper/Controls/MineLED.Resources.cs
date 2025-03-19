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
    private static Dictionary<char, Bitmap> bmp = null;
    /************************************************/
    static MineLED()
    {
      if (MineLED.bmp == null)
      {
        MineLED.bmp = new Dictionary<char, Bitmap>();
        /************************************************/
        MineLED.bmp['-'] = MineLED.DrawBitmap("      G".Replace(" ", ""));
        MineLED.bmp[' '] = MineLED.DrawBitmap("       ".Replace(" ", ""));
        MineLED.bmp['9'] = MineLED.DrawBitmap("ABCD FG".Replace(" ", ""));
        MineLED.bmp['8'] = MineLED.DrawBitmap("ABCDEFG".Replace(" ", ""));
        MineLED.bmp['7'] = MineLED.DrawBitmap("ABC    ".Replace(" ", ""));
        MineLED.bmp['6'] = MineLED.DrawBitmap("A CDEFG".Replace(" ", ""));
        MineLED.bmp['5'] = MineLED.DrawBitmap("A CD FG".Replace(" ", ""));
        MineLED.bmp['4'] = MineLED.DrawBitmap(" BC  FG".Replace(" ", ""));
        MineLED.bmp['3'] = MineLED.DrawBitmap("ABCD  G".Replace(" ", ""));
        MineLED.bmp['2'] = MineLED.DrawBitmap("AB DE G".Replace(" ", ""));
        MineLED.bmp['1'] = MineLED.DrawBitmap(" BC    ".Replace(" ", ""));
        MineLED.bmp['0'] = MineLED.DrawBitmap("ABCDEF ".Replace(" ", ""));
      }
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