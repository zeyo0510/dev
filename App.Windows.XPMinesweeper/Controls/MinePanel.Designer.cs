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
        resetMineButton.Dispose();
        flagMineLED.Dispose();
        durationMineLED.Dispose();
        ilLED.Dispose();
        tmrCount.Dispose();
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      darkGrayPen = new Pen(darkGray, 1);
      ilLED = new ImageList();
      ilLED.ImageSize = new Size(13, 23);
      string FileName = "0123456789-";
      for (int i = 0; i < FileName.Length; i++)
      {
        ilLED.Images.Add(getBitmap(FileName[i] + ".png", false));
      }
      /************************************************/
      tmrCount = new Timer();
      /************************************************/
      flagMineLED = new MineLED();
      resetMineButton = new MineButton();
      durationMineLED = new MineLED();
      /************************************************/
      // flagMineLED
      {
        flagMineLED.Name = "flagMineLED";
        flagMineLED.Parent = this;
        flagMineLED.LEDImages = ilLED;
      }
      // resetMineButton
      {
        resetMineButton.Name = "resetMineButton";
        resetMineButton.Text = "";
        resetMineButton.Parent = this;
        resetMineButton.Click += new EventHandler(OnReset);
      }
      // durationMineLED
      {
        durationMineLED.Name = "durationMineLED";
        durationMineLED.Parent = this;
        durationMineLED.LEDImages = ilLED;
      }
      // MinePanel
      {
        base.Name = "MinePanel";
      }
      // tmrCount
      {
        tmrCount.Interval = 1000;
        tmrCount.Tick += new EventHandler(tmrCount_Tick);
        tmrCount.Stop();
      }
    }
    /************************************************/
    private MineLED flagMineLED = null;
    private MineButton resetMineButton = null;
    private MineLED durationMineLED = null;
  }
}