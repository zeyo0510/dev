using System;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPNotepad.Main
{
  partial class MainForm
  {
    public void ShowHelpTopics()
    {
      Help.ShowHelp(this, "notepad.chm", HelpNavigator.Topic);
    }
  }
}