using System;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MinePlayer
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
        tmrCount.Dispose();
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      tmrCount = new Timer();
      /************************************************/
      this.minePanel1      = new MinePanel();
      this.flagMineLED     = new MineLED();
      this.resetMineButton = new MineButton();
      this.durationMineLED = new MineLED();
      this.mineControl1    = new MineControl();
      /************************************************/
      // minePanel1
      {
        this.minePanel1.Name = "minePanel1";
        this.minePanel1.Dock = DockStyle.Fill;
        /************************************************/
        this.minePanel1.Panel1.Controls.Add(this.flagMineLED);
        this.minePanel1.Panel1.Controls.Add(this.resetMineButton);
        this.minePanel1.Panel1.Controls.Add(this.durationMineLED);
        /************************************************/
        this.minePanel1.Panel2.Controls.Add(this.mineControl1);
      }
      // flagMineLED
      {
        this.flagMineLED.Name      = "flagMineLED";
        this.flagMineLED.Anchor    = AnchorStyles.Left | AnchorStyles.Top;
        this.flagMineLED.Location  = new Point(6, 5);
      }
      // resetMineButton
      {
        this.resetMineButton.Name     = "resetMineButton";
        this.resetMineButton.Anchor   = AnchorStyles.Top;
        this.resetMineButton.Location = new Point(61, 5);
        /************************************************/
        this.resetMineButton.Click += new EventHandler(OnReset);
      }
      // durationMineLED
      {
        this.durationMineLED.Name      = "durationMineLED";
        this.durationMineLED.Anchor    = AnchorStyles.Top | AnchorStyles.Right;
        this.durationMineLED.Location  = new Point(99, 5);
      }
      // mineControl1
      {
        this.mineControl1.Name = "mineControl1";
        this.mineControl1.BackColor = Color.Silver;
        this.mineControl1.Mines = null;
        this.mineControl1.Size = new Size(272, 168);
      }
      // MinePanel
      {
        base.Name        = "MinePanel";
        base.ClientSize  = new Size(167, 210);
        base.MinimumSize = new Size(167, 210);
        /************************************************/
        base.Controls.Add(this.minePanel1);
      }
      // tmrCount
      {
        tmrCount.Interval = 1000;
        tmrCount.Tick += new EventHandler(tmrCount_Tick);
        tmrCount.Stop();
      }
    }
    /************************************************/
    private MinePanel  minePanel1      = null;
    private MineLED    flagMineLED     = null;
    private MineButton resetMineButton = null;
    private MineLED    durationMineLED = null;
    private MineControl mineControl1 = null;
  }
}