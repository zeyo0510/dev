using System;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  partial class MainForm
  {
    public void ToggleStatusBar()
    {
      this.bottomStatusStrip.Visible = !this.bottomStatusStrip.Visible;
    }
  }
}