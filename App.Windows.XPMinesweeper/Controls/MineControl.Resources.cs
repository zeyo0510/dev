using System;
using System.Drawing;
using System.IO;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineControl
  {
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
  }
}