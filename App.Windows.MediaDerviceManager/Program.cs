using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using App.Windows.MediaDerviceManager.Main;

namespace App.Windows.MediaDerviceManager
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.ThreadException += App_ThreadException;
      Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
      AppDomain.CurrentDomain.UnhandledException += App_UnhandledException;
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }

    private static void CheckReqFIle(string P_0)
    {
      string text = Application.StartupPath + "\\" + P_0;
      Console.WriteLine(text);
      if (!File.Exists(text) && MessageBox.Show(null, "The file '" + P_0 + "' seems to be missing. \rThis file is needed for CheVolume to run.\r\r Please reinstall the application.", "Error : File missing", MessageBoxButtons.OK, MessageBoxIcon.Hand) == DialogResult.OK)
      {
        Environment.Exit(0);
      }
    }

    private static void App_UnhandledException(object P_0, UnhandledExceptionEventArgs P_1)
    {
      ReportErrorToOfficial(P_1.ExceptionObject as Exception);
      MessageBox.Show(((Exception)P_1.ExceptionObject).InnerException.ToString());
      Environment.Exit(0);
    }

    private static void App_ThreadException(object P_0, ThreadExceptionEventArgs P_1)
    {
      try
      {
        if (P_1 != null && P_1.Exception != null)
        {
          ReportErrorToOfficial(P_1.Exception);
        }
        MessageBox.Show(P_1.Exception.InnerException.ToString());
        MessageBox.Show("CheVolume has encountered a problem and needs to close.\rWe are sorry for the inconvenience.\r\rIf the problem persist, contact our Support Department.", "CheVolume", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      catch (Exception)
      {
      }
      Environment.Exit(0);
    }

    private static void ReportErrorToOfficial(Exception P_0)
    {
      string message = P_0.Message;
      string stackTrace = P_0.StackTrace;
      using (WebClient webClient = new WebClient())
      {
        NameValueCollection nameValueCollection = new NameValueCollection();
        nameValueCollection["message"] = Application.ProductVersion + " : " + message;
        nameValueCollection["stacktrace"] = stackTrace;
        try
        {
          byte[] bytes = webClient.UploadValues("http://www.chevolume.com/SendError.aspx", nameValueCollection);
          Encoding.Default.GetString(bytes);
        }
        catch (Exception)
        {
        }
      }
    }
  }
}