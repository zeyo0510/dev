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
//      this.Key = this.keyTextBox.Text;
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
//      this.Value = this.valueTextBox.Text;
    }
    /************************************************/
    private void okButton_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
      string data = string.Format("Key: {0}, Value: {1}", this.Key, this.Value);
      System.Diagnostics.Debug.WriteLine(data);
    }
    /************************************************/
    private void cancelButton_Click(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
  }
}