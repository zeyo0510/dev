using System;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  partial class MainForm
  {
    public void UpdateUI()
    {
      this.     fileToolStripMenuItem.Checked = false;
      this.     fileToolStripMenuItem.Enabled = true;
      this.   importToolStripMenuItem.Checked = false;
      this.   importToolStripMenuItem.Enabled = true;
      this.   exportToolStripMenuItem.Checked = false;
      this.   exportToolStripMenuItem.Enabled = this.ListView1.Items.Count > 0;
      this.     exitToolStripMenuItem.Checked = false;
      this.     exitToolStripMenuItem.Enabled = true;
      this.     editToolStripMenuItem.Checked = false;
      this.     editToolStripMenuItem.Enabled = true;
      this.     copyToolStripMenuItem.Checked = false;
      this.     copyToolStripMenuItem.Enabled = this.ListView1.SelectedItems.Count > 0;
      this.   insertToolStripMenuItem.Checked = false;
      this.   insertToolStripMenuItem.Enabled = true;
      this.   updateToolStripMenuItem.Checked = false;
      this.   updateToolStripMenuItem.Enabled = this.ListView1.SelectedItems.Count > 0;
      this.   deleteToolStripMenuItem.Checked = false;
      this.   deleteToolStripMenuItem.Enabled = this.ListView1.SelectedItems.Count > 0;
      this.selectallToolStripMenuItem.Checked = false;
      this.selectallToolStripMenuItem.Enabled = true;
      this.     viewToolStripMenuItem.Checked = false;
      this.     viewToolStripMenuItem.Enabled = true;
      this.     userToolStripMenuItem.Checked = this.CurrentEnvironmentVariable == EnvironmentVariableTarget.User;
      this.     userToolStripMenuItem.Enabled = true;
      this.  machineToolStripMenuItem.Checked = this.CurrentEnvironmentVariable == EnvironmentVariableTarget.Machine;
      this.  machineToolStripMenuItem.Enabled = true;
      this.  refreshToolStripMenuItem.Checked = false;
      this.  refreshToolStripMenuItem.Enabled = true;
      this.statusbarToolStripMenuItem.Checked = this.bottomStatusStrip.Visible;
      this.statusbarToolStripMenuItem.Enabled = true;
      /************************************************/
      this.messsageToolStripStatusLabel.Text = (this.ListView1.SelectedItems.Count == 0) ? "Ready"                                                           : this.messsageToolStripStatusLabel.Text;
      this.messsageToolStripStatusLabel.Text = (this.ListView1.SelectedItems.Count >= 1) ? string.Format("{0} selected", this.ListView1.SelectedItems.Count) : this.messsageToolStripStatusLabel.Text;
    }
  }
}