using System;
using System.Diagnostics;
using System.Management;
/************************************************/
namespace App.EnvironmentVariableEditor.Core
{
  partial class EnvVar
  {
    public static bool Delete(string user, string variable)
    {
      bool retValue = false;
      /************************************************/
      try
      {
        string statement = string.Format("SELECT * FROM Win32_Environment WHERE UserName = '{0}' AND Name = '{1}'", user, variable);
        /************************************************/
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(statement);
        /************************************************/
        foreach (ManagementObject _ in searcher.Get())
        {
          _.Delete();
          /************************************************/
          retValue = true;
        }
      }
      catch (ManagementException ex)
      {
        Debug.WriteLine(string.Format("WMI Error: {0}", ex.Message));
      }
      catch (Exception ex)
      {
        Debug.WriteLine(string.Format("An error occurred: {0}", ex.Message));
      }
      /************************************************/
      return retValue;
    }
  }
}