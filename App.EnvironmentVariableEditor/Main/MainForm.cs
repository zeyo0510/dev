using System;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  internal partial class MainForm : Form
  {
    public MainForm()
    {
      this.InitializeComponent();
    }
    /************************************************/
    protected override void OnLoad(EventArgs e)
    {
      this.AdjuestClientSize();
      /************************************************/
      base.OnLoad(e);
    }
    /************************************************/
    private void guiTimer_Tick(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void importToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void exportToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.CloseApp();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void undoToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void redoToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void cutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void statusbarToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ToggleStatusBar();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void messsageToolStripStatusLabel_TextChanged(object sender, EventArgs e)
    {
      // do nothing...
    }
  }
}