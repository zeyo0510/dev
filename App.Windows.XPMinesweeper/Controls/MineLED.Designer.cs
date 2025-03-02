using System;
using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineLED
  {
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      base.Name        = "MineLED";
      base.MinimumSize = new Size(039, 023);
      base.MaximumSize = new Size(039, 023);
      base.ClientSize  = new Size(039, 023);
    }
  }
}