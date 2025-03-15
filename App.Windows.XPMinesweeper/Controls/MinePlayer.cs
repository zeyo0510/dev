using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public partial class MinePlayer : UserControl
  {
    public MinePlayer()
    {
      this.InitializeComponent();

      SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer |
        ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.Selectable, false);
      BackColor = Color.Silver;
      Width = 320;
      Height = 240;
      ChangeFace(1);
    }

    private void tmrCount_Tick(object sender, EventArgs e)
    {
      durationMineLED.Value = durationMineLED.Value + 1;
    }

    public Size GetWindowClientSize(Size mineControlSize)
    {
      return new Size(mineControlSize.Width + 9 + 3 + 3 + 6, mineControlSize.Height + 9 + 2 + 36 + 2 + 6 + 3 + 4);
    }

    internal void ChangeFace(int faceID)
    {
      if (resetMineButton.Image != null)
      {
        resetMineButton.Image.Dispose();
        resetMineButton.Image = null;
      }
      if (faceID == 4)
      {
        resetMineButton.Image = MineButton.Bitmap1;
      }
      if (faceID == 3)
      {
        resetMineButton.Image = MineButton.Bitmap2;
      }
      if (faceID == 2)
      {
        resetMineButton.Image = MineButton.Bitmap3;
      }
      if (faceID == 1)
      {
        resetMineButton.Image = MineButton.Bitmap4;
      }
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