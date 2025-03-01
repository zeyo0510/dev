using System;
using System.Windows.Forms;
using App.Windows.XPMinesweeper.Main;
/************************************************/
namespace App.Windows.XPMinesweeper
{
  internal sealed class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}