using System;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariablesEditor.Dialogs
{
  internal partial class AdjuestDailog : Form
  {
    public AdjuestDailog()
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
  }
}