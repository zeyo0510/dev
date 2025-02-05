using System;
using System.Windows.Forms;

namespace App.Windows.XPNotepad
{
  internal sealed class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new global::notepad.Form1());
    }
  }
}