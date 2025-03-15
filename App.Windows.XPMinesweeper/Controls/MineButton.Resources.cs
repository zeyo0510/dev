using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineButton
  {
    private static Dictionary<char, Bitmap> bmp = null;
    /************************************************/
    static MineButton()
    {
      if (MineButton.bmp == null)
      {
        MineButton.bmp = new Dictionary<char, Bitmap>();
        /************************************************/
        MineButton.bmp['1'] = MineButton.DrawBitmap("Face1");
        MineButton.bmp['2'] = MineButton.DrawBitmap("Face2");
        MineButton.bmp['3'] = MineButton.DrawBitmap("Face3");
        MineButton.bmp['4'] = MineButton.DrawBitmap("Face4");
      }
    }
    /************************************************/
    private static Bitmap DrawBitmap(string s)
    {
      int w = 17;
      int h = 17;
      /************************************************/
      StringBuilder map = new StringBuilder();
      /************************************************/
      if (s == "Face1")
      {
        map.Append("      BBBBB      ");
        map.Append("    BBYYYYYBB    ");
        map.Append("   BYYYYYYYYYB   ");
        map.Append("  BYYYYYYYYYYYB  ");
        map.Append(" BYYYYYYYYYYYYYB ");
        map.Append(" BYYBBBBBBBBBYYB ");
        map.Append("BYYBBBBBYBBBBBYYB");
        map.Append("BYBYBBBBYBBBBYBYB");
        map.Append("BBYYOBBYYYBBOYYBB");
        map.Append("BYYYYYYYYYYYYYYYB");
        map.Append("BYYYYYYYYYYYYYYYB");
        map.Append(" BYYYBYYYYYBYYYB ");
        map.Append(" BYYYYBBBBBYYYYB ");
        map.Append("  BYYYYYYYYYYYB  ");
        map.Append("   BYYYYYYYYYB   ");
        map.Append("    BBYYYYYBB    ");
        map.Append("      BBBBB      ");
      }
      if (s == "Face2")
      {
        map.Append("      BBBBB      ");
        map.Append("    BBYYYYYBB    ");
        map.Append("   BYYYYYYYYYB   ");
        map.Append("  BYYYYYYYYYYYB  ");
        map.Append(" BYYBYBYYYBYBYYB ");
        map.Append(" BYYYBYYYYYBYYYB ");
        map.Append("BYYYBYBYYYBYBYYYB");
        map.Append("BYYYYYYYYYYYYYYYB");
        map.Append("BYYYYYYYYYYYYYYYB");
        map.Append("BYYYYYYYYYYYYYYYB");
        map.Append("BYYYYYBBBBBYYYYYB");
        map.Append(" BYYYBYYYYYBYYYB ");
        map.Append(" BYYBYYYYYYYBYYB ");
        map.Append("  BYYYYYYYYYYYB  ");
        map.Append("   BYYYYYYYYYB   ");
        map.Append("    BBYYYYYBB    ");
        map.Append("      BBBBB      ");
      }
      if (s == "Face3")
      {
        map.Append("      BBBBB      ");
        map.Append("    BBYYYYYBB    ");
        map.Append("   BYYYYYYYYYB   ");
        map.Append("  BYYYYYYYYYYYB  ");
        map.Append(" BYYOBOYYYOBOYYB ");
        map.Append(" BYYBBBYYYBBBYYB ");
        map.Append("BYYYOBOYYYOBOYYYB");
        map.Append("BYYYYYYYYYYYYYYYB");
        map.Append("BYYYYYYYYYYYYYYYB");
        map.Append("BYYYYYYBBBYYYYYYB");
        map.Append("BYYYYYOBYBOYYYYYB");
        map.Append(" BYYYYBYYYBYYYYB ");
        map.Append(" BYYYYOBYBOYYYYB ");
        map.Append("  BYYYYBBBYYYYB  ");
        map.Append("   BYYYYYYYYYB   ");
        map.Append("    BBYYYYYBB    ");
        map.Append("      BBBBB      ");
      }
      if (s == "Face4")
      {
        map.Append("      BBBBB      ");
        map.Append("    BBYYYYYBB    ");
        map.Append("   BYYYYYYYYYB   ");
        map.Append("  BYYYYYYYYYYYB  ");
        map.Append(" BYYYYYYYYYYYYYB ");
        map.Append(" BYYYBBYYYBBYYYB ");
        map.Append("BYYYYBBYYYBBYYYYB");
        map.Append("BYYYYYYYYYYYYYYYB");
        map.Append("BYYYYYYYYYYYYYYYB");
        map.Append("BYYYYYYYYYYYYYYYB");
        map.Append("BYYYBYYYYYYYBYYYB");
        map.Append(" BYYYBYYYYYBYYYB ");
        map.Append(" BYYYYBBBBBYYYYB ");
        map.Append("  BYYYYYYYYYYYB  ");
        map.Append("   BYYYYYYYYYB   ");
        map.Append("    BBYYYYYBB    ");
        map.Append("      BBBBB      ");
        }
      /************************************************/
      Bitmap retValue = new Bitmap(w, h);
      /************************************************/
      using (Graphics g = Graphics.FromImage(retValue))
      {
        using (Brush bBrush = new SolidBrush(Color.Black))
        {
          using (Brush yBrush = new SolidBrush(Color.Yellow))
          {
            using (Brush oBrush = new SolidBrush(Color.Olive))
            {
              for (int y = 0; y < h; y++)
              {
                for (int x = 0; x < w; x++)
                {
                  char n = map.ToString()[(x * 01) + (y * 17)];
                  /************************************************/
                  if (n == 'B') g.FillRectangle(bBrush, x, y, 1, 1);
                  if (n == 'Y') g.FillRectangle(yBrush, x, y, 1, 1);
                  if (n == 'O') g.FillRectangle(oBrush, x, y, 1, 1);
                }
              }
            }
          }
        }
      }
      /************************************************/
      return retValue;
    }
    /************************************************/
    public static Bitmap Bitmap1
    {
      get
      {
        Bitmap retValue = MineButton.bmp['1'];
        /************************************************/
        retValue = (Bitmap)retValue.Clone();
        /************************************************/
        return retValue;
      }
    }
    /************************************************/
    public static Bitmap Bitmap2
    {
      get
      {
        Bitmap retValue = MineButton.bmp['2'];
        /************************************************/
        retValue = (Bitmap)retValue.Clone();
        /************************************************/
        return retValue;
      }
    }
    /************************************************/
    public static Bitmap Bitmap3
    {
      get
      {
        Bitmap retValue = MineButton.bmp['3'];
        /************************************************/
        retValue = (Bitmap)retValue.Clone();
        /************************************************/
        return retValue;
      }
    }
    /************************************************/
    public static Bitmap Bitmap4
    {
      get
      {
        Bitmap retValue = MineButton.bmp['4'];
        /************************************************/
        retValue = (Bitmap)retValue.Clone();
        /************************************************/
        return retValue;
      }
    }
  }
}