using System;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public partial class MineButton : Button
  {
    public MineButton()
    {
      this.InitializeComponent();
      /************************************************/
      base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }
    /************************************************/
    private void guiTimer_Tick(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
  }
}