using System;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public partial class MinePanel : Control
  {
    public MinePanel()
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