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
    private void guiTimer_Tick(object sender, EventArgs e)
    {
      this.UpdateUI();
    }
  }
}