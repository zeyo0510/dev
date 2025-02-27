using System;
/************************************************/
namespace App.Windows.DotnetFrameworkViewer.Main
{
  partial class MainForm
  {
    public void UpdateUI()
    {
      this.           fileToolStripMenuItem.Checked = (false)                                            ;
      this.           exitToolStripMenuItem.Checked = (false)                                            ;
      this.           editToolStripMenuItem.Checked = (false)                                            ;
      this.           copyToolStripMenuItem.Checked = (false)                                            ;
      this.      selectallToolStripMenuItem.Checked = (false)                                            ;
      this.     selectnoneToolStripMenuItem.Checked = (false)                                            ;
      this.invertselectionToolStripMenuItem.Checked = (false)                                            ;
      this.           viewToolStripMenuItem.Checked = (false)                                            ;
      this.        refreshToolStripMenuItem.Checked = (false)                                            ;
      this.      statusbarToolStripMenuItem.Checked = (false) || (this.bottomStatusStrip.Visible == true);
      /************************************************/
      this.           fileToolStripMenuItem.Enabled = (true)                                            ;
      this.           exitToolStripMenuItem.Enabled = (true)                                            ;
      this.           editToolStripMenuItem.Enabled = (true)                                            ;
      this.           copyToolStripMenuItem.Enabled = (true) && (this.listview1.SelectedItems.Count > 0);
      this.      selectallToolStripMenuItem.Enabled = (true)                                            ;
      this.     selectnoneToolStripMenuItem.Enabled = (true)                                            ;
      this.invertselectionToolStripMenuItem.Enabled = (true)                                            ;
      this.           viewToolStripMenuItem.Enabled = (true)                                            ;
      this.        refreshToolStripMenuItem.Enabled = (true)                                            ;
      this.      statusbarToolStripMenuItem.Enabled = (true)                                            ;
      /************************************************/
      this.messageToolStripStatusLabel.Text = (this.listview1.SelectedItems.Count == 0) ? string.Format("{0}"         , "Ready"                           ) : this.messageToolStripStatusLabel.Text;
      this.messageToolStripStatusLabel.Text = (this.listview1.SelectedItems.Count >= 1) ? string.Format("{0} selected", this.listview1.SelectedItems.Count) : this.messageToolStripStatusLabel.Text;
    }
  }
}