using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePanel
  {
    private Timer tmrCount;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        darkGrayPen.Dispose();
        lightPen.Dispose();
        rbReset.Dispose();
        pnlLeft.Dispose();
        pnlRight.Dispose();
        ilLED.Dispose();
        tmrCount.Dispose();
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      Name = "minePanel";
      darkGrayPen = new Pen(darkGray, 1);
      lightPen = new Pen(Color.White, 1);
      rbReset = new ResetButton();
      rbReset.Name = "rbReset";
      rbReset.Text = "";
      rbReset.Parent = this;
      rbReset.Click += new EventHandler(OnReset);

      ilLED = new ImageList();
      ilLED.ImageSize = new Size(13, 23);
      string FileName = "0123456789-";
      for (int i = 0; i < FileName.Length; i++)
      {
        ilLED.Images.Add(getBitmap(FileName[i] + ".png", false));
      }

      pnlLeft = new LEDPanel();
      pnlLeft.Parent = this;
      pnlLeft.Name = "pnlLeft";
      pnlLeft.LEDImages = ilLED;
      pnlRight = new LEDPanel();
      pnlRight.Parent = this;
      pnlRight.Name = "pnlRight";
      pnlRight.LEDImages = ilLED;

      tmrCount = new Timer();
      tmrCount.Interval = 1000;
      tmrCount.Tick += new EventHandler(tmrCount_Tick);
      tmrCount.Stop();
    }
    /************************************************/
    private ResetButton rbReset;
    private LEDPanel pnlLeft, pnlRight;
  }
}