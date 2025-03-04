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
      this.panel1 = new Control();
      this.panel2 = new Control();
      flagMineLED = new MineLED();
      resetMineButton = new MineButton();
      durationMineLED = new MineLED();
      /************************************************/
      // panel1
      {
        this.panel1.Name     = "panel1";
        this.panel1.Location = new Point(11, 11);
        this.panel1.Size     = new Size(146, 033);
      }
      // panel2
      {
        this.panel2.Name     = "panel2";
        this.panel2.Location = new Point(11, 54);
        this.panel2.Size     = new Size(146, 146);
      }
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
        base.Controls.Add(this.panel1);
        base.Controls.Add(this.panel2);
      }
      // tmrCount
      {
        tmrCount.Interval = 1000;
        tmrCount.Tick += new EventHandler(tmrCount_Tick);
        tmrCount.Stop();
      }
    }
    /************************************************/
    private Control panel1 = null;
    private Control panel2 = null;
    private MineLED flagMineLED = null;
    private MineButton resetMineButton = null;
    private MineLED durationMineLED = null;
  }
}