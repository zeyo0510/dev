using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public partial class MinePanel : Panel
  {
    private Color darkGray = Color.Gray;

    private Pen lightPen,  darkGrayPen;

    private ImageList ilLED;

    public MinePanel()
    {
      InitializeComponent();

      SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer |
        ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.Selectable, false);
      BackColor = Color.Silver;
      Width = 320;
      Height = 240;
      ChangeFace(1);
      ArrangeChildChildren();
    }

    private void tmrCount_Tick(object sender, EventArgs e)
    {
      pnlRight.Value = pnlRight.Value + 1;
    }

    private Bitmap getBitmap(string fileName, bool transparent)
    {
      Image img =  Image.FromStream(GetResource(fileName));
      Bitmap bmp = new Bitmap(img);
      img.Dispose();
      img = null;
      if (transparent)
        bmp.MakeTransparent(bmp.GetPixel(1, 1));
      return bmp;
    }

    public Size GetWindowClientSize(Size mineControlSize)
    {
      return new Size(mineControlSize.Width + 9 + 3 + 3 + 6, mineControlSize.Height + 9 + 2 + 36 + 2 + 6 + 3 + 4);
    }

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

    internal void ChangeFace(int faceID)
    {
      if (rbReset.Image != null)
      {
        rbReset.Image.Dispose();
        rbReset.Image = null;
      }
      rbReset.Image = getBitmap("Face" + faceID.ToString() + ".png", true);
    }

    public void ArrangeChildChildren()
    {
      rbReset.Left = 3 + (ClientSize.Width - 3 - rbReset.Width) / 2;
      rbReset.Top = 3 + (6 + 6 + 36 - rbReset.Height) / 2;

      pnlLeft.Left = 3 + 6 + 7;
      pnlLeft.Top = rbReset.Top;
      pnlRight.Left = ClientSize.Width - 6 - 7 - pnlRight.Width;
      pnlRight.Top = rbReset.Top;
    }

    public event EventHandler Reset;

    private void OnReset(object sender, EventArgs e)
    {
      if (Reset != null)
        Reset(this, e);
    }

    public void StartTimer()
    {
      tmrCount.Start();
    }

    public void StopTimer()
    {
      tmrCount.Stop();
    }
  }
}