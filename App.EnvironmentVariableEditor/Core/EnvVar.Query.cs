using System;
using System.Collections.Generic;
using System.Management;
/************************************************/
namespace App.EnvironmentVariableEditor.Core
{
  partial class EnvVar
  {
    public static KeyValuePair<string, string>[] Query()
    {
      List<KeyValuePair<string, string>> retValue = new List<KeyValuePair<string, string>>();
      /************************************************/
      ManagementClass myClass = new ManagementClass("Win32_Environment");
      /************************************************/
      ManagementObjectCollection myObjectCollection = myClass.GetInstances();
      /************************************************/
      foreach (ManagementObject _ in myObjectCollection)
      {
        KeyValuePair<string, string> tmp = new KeyValuePair<string, string>(
          _["Name"].ToString(),
          _["VariableValue"].ToString()
         );
        /************************************************/
        retValue.Add(tmp);
      }
      /************************************************/
      return retValue.ToArray();
    }
    /************************************************/
    public static KeyValuePair<string, string>[] Query(string user)
    {
      List<KeyValuePair<string, string>> retValue = new List<KeyValuePair<string, string>>();
      /************************************************/
      ManagementClass myClass = new ManagementClass("Win32_Environment");
      /************************************************/
      ManagementObjectCollection myObjectCollection = myClass.GetInstances();
      /************************************************/
      foreach (ManagementObject _ in myObjectCollection)
      {
        if (_["UserName"].ToString() != user) continue;
        /************************************************/
        KeyValuePair<string, string> tmp = new KeyValuePair<string, string>(
          _["Name"].ToString(),
          _["VariableValue"].ToString()
         );
        /************************************************/
        retValue.Add(tmp);
      }
      /************************************************/
      return retValue.ToArray();
    }
    /************************************************/
    public static KeyValuePair<string, string>[] Query(string user, string variable)
    {
      List<KeyValuePair<string, string>> retValue = new List<KeyValuePair<string, string>>();
      /************************************************/
      ManagementClass myClass = new ManagementClass("Win32_Environment");
      /************************************************/
      ManagementObjectCollection myObjectCollection = myClass.GetInstances();
      /************************************************/
      foreach (ManagementObject _ in myObjectCollection)
      {
        if (_["UserName"].ToString() != user    ) continue;
        if (_["Name"    ].ToString() != variable) continue;
        /************************************************/
        KeyValuePair<string, string> tmp = new KeyValuePair<string, string>(
          _["Name"].ToString(),
          _["VariableValue"].ToString()
         );
        /************************************************/
        retValue.Add(tmp);
      }
      /************************************************/
      return retValue.ToArray();
    }
  }
}