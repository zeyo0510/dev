using System;
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
      if (EnvVar.Query(user).Length <= 0)
      {
        return retValue;
      }
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
        _.Delete();
        /************************************************/
        retValue = true;
      }
      /************************************************/
      return retValue;
    }
  }
}