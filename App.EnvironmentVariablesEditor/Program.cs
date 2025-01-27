using System;
using System.Windows.Forms;
using App.EnvironmentVariableEditor.Main;
/************************************************/
namespace App.EnvironmentVariableEditor
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