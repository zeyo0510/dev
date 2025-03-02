using System;
using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineButton
  {
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        // do nothing...
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      base.Name        = "MineButton";
      base.ClientSize  = new Size(24, 24);
      base.MaximumSize = new Size(24, 24);
      base.MinimumSize = new Size(24, 24);
    }
  }
}