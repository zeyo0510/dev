using System;
using System.Windows.Forms;
using TestApp.Forms;

namespace TestApp {
  internal sealed class App {
    /// <summary>
    /// 應用程式進入點。
    /// </summary>
    [STAThread]
    private static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}
