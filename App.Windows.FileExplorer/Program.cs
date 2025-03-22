using System;
using System.Windows.Forms;
using App.Windows.FileExplorer.Main;
/************************************************/
namespace App.Windows.FileExplorer
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