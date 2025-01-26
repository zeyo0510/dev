using System;
using System.Windows.Forms;
/************************************************/
namespace App.EnvironmentVariablesEditor.Main
{
  public partial class MainForm : Form
  {
    public MainForm()
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