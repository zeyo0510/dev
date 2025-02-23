using System;
using System.Windows.Forms;
using App.EnvironmentVariableEditor.Dialogs;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  internal partial class MainForm : Form
  {
    public MainForm()
    {
      this.InitializeComponent();
      /************************************************/
      this.ReloadEnvVar();
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
    private void ListView1_DoubleClick(object sender, EventArgs e)
    {
      // TODO: do anything...
      
      ListViewItem item = ListView1.SelectedItems[0];
      
      AdjuestDailog dialog = new AdjuestDailog(item.SubItems[0].Text, item.SubItems[1].Text);
      {
        dialog.ManipulationMode = ManipulationMode.Update;
      }
      /************************************************/
      if (dialog.ShowDialog(this) != DialogResult.OK)
      {
        return;
      }
    }
    /************************************************/
    private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void importToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ImportData();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void exportToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ExportData();
      /************************************************/
      this.UpdateUI();
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
    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.CopySelectedItem2Clipboard();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void insertToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void updateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void selectallToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SelectAllItem();
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
    /************************************************/
    private void userToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.CurrentEnvironmentVariable = EnvironmentVariableTarget.User;
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void machineToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.CurrentEnvironmentVariable = EnvironmentVariableTarget.Machine;
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ReloadEnvVar();
      /************************************************/
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