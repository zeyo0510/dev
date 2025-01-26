using System;
/************************************************/
namespace App.EnvironmentVariablesEditor.Main
{
  partial class MainForm
  {
    public void ToggleStatusBar()
    {
      this.bottomStatusStrip.Visible = !this.bottomStatusStrip.Visible;
    }
  }
}