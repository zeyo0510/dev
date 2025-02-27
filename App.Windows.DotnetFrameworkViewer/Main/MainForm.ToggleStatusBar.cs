using System;
/************************************************/
namespace App.Windows.DotnetFrameworkViewer.Main
{
  partial class MainForm
  {
    public void ToggleStatusBar()
    {
      this.bottomStatusStrip.Visible = !this.bottomStatusStrip.Visible;
    }
  }
}