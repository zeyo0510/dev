using System;
using System.Collections.Generic;
using System.Management;
/************************************************/
namespace App.EnvironmentVariableEditor.Core
{
  partial class EnvVar
  {
    public static EnvVarAccount[] Accounts
    {
      get
      {
        List<EnvVarAccount> retValue = new List<EnvVarAccount>();
        /************************************************/
        ManagementClass myClass = new ManagementClass("Win32_UserAccount");
        /************************************************/
        ManagementObjectCollection myObjectCollection = myClass.GetInstances();
        /************************************************/
        foreach (ManagementObject _ in myObjectCollection)
        {
          retValue.Add(new EnvVarAccount(_["Caption"].ToString(), EnvVarAccountType.User));
        }
        /************************************************/
        ManagementClass myClass2 = new ManagementClass("Win32_SystemAccount");
        /************************************************/
        ManagementObjectCollection myObjectCollection2 = myClass2.GetInstances();
        /************************************************/
        foreach (ManagementObject _ in myObjectCollection2)
        {
          retValue.Add(new EnvVarAccount(_["Caption"].ToString(), EnvVarAccountType.System));
        }
        /************************************************/
        return retValue.ToArray();
      }
    }
  }
}