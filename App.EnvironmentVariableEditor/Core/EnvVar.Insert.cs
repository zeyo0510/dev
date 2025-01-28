using System;
using System.Diagnostics;
using System.Management;
/************************************************/
namespace App.EnvironmentVariableEditor.Core
{
  partial class EnvVar
  {
    public static bool Insert(string user, string variable, string value)
    {
      bool retValue = false;
      /************************************************/
      try
      {
        ManagementClass environmentClass = new ManagementClass("Win32_Environment");
        /************************************************/
        ManagementObject newEnvVar = environmentClass.CreateInstance();
        /************************************************/
        if (newEnvVar != null)
        {
          newEnvVar["UserName"]      = user;
          newEnvVar["Name"]          = variable;
          newEnvVar["VariableValue"] = value;
          /************************************************/
          newEnvVar.Put();
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