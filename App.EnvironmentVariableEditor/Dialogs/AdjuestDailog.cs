using System;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariableEditor.Dialogs
{
  internal partial class AdjuestDailog : Form
  {
    private AdjuestDailog()
    {
      this.InitializeComponent();
    }
    /************************************************/
    public AdjuestDailog(string key, string value) : this()
    {
      this.Key   = key;
      this.Value = value;
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
    private void keyLabel_Click(object sender, EventArgs e)
    {
      this.FocusTextBox("key");
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void keyTextBox_TextChanged(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void valueLabel_Click(object sender, EventArgs e)
    {
      this.FocusTextBox("value");
      /************************************************/
      this.UpdateUI();
    }
    /************************************************/
    private void valueTextBox_TextChanged(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void upButton_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void downButton_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void okButton_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void cancelButton_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
  }
}