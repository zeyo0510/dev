using System;
using System.Collections.Generic;
using System.Management;
/************************************************/
namespace App.EnvironmentVariableEditor.Core
{
  partial class EnvVar
  {
    public static EnvVarEntry[] Query()
    {
      List<EnvVarEntry> retValue = new List<EnvVarEntry>();
      /************************************************/
      ManagementClass myClass = new ManagementClass("Win32_Environment");
      /************************************************/
      ManagementObjectCollection myObjectCollection = myClass.GetInstances();
      /************************************************/
      foreach (ManagementObject _ in myObjectCollection)
      {
        retValue.Add(new EnvVarEntry(
          _["UserName"     ].ToString(),
          _["Name"         ].ToString(),
          _["VariableValue"].ToString()
        ));
      }
      /************************************************/
      return retValue.ToArray();
    }
    /************************************************/
    public static EnvVarEntry[] Query(string account)
    {
      List<EnvVarEntry> retValue = new List<EnvVarEntry>();
      /************************************************/
      foreach (EnvVarEntry _ in EnvVar.Query())
      {
        if (_.Account != account) continue;
        /************************************************/
        retValue.Add(_);
      }
      /************************************************/
      return retValue.ToArray();
    }
    /************************************************/
    public static EnvVarEntry[] Query(string account, string variable)
    {
      List<EnvVarEntry> retValue = new List<EnvVarEntry>();
      /************************************************/
      foreach (EnvVarEntry _ in EnvVar.Query(account))
      {
        if (_.Variable != variable) continue;
        /************************************************/
        retValue.Add(_);
      }
      /************************************************/
      return retValue.ToArray();
    }
  }
}