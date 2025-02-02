using System;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  partial class MainForm
  {
    // TODO: do anything...
    public void UpdateUI()
    {
      this.   userToolStripMenuItem.Checked = this.CurrentEnvironmentVariable == EnvironmentVariableTarget.User;
      this.machineToolStripMenuItem.Checked = this.CurrentEnvironmentVariable == EnvironmentVariableTarget.Machine;
      /************************************************/
      this.statusbarToolStripMenuItem.Checked = this.bottomStatusStrip.Visible;
    }
  }
}