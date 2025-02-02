using System;
using System.Management;
/************************************************/
namespace App.EnvironmentVariableEditor.Core
{
  partial class EnvVar
  {
    public static bool Insert(string account, string variable, string value)
    {
      bool retValue = false;
      /************************************************/
      if (EnvVar.Query(account, variable).Length >= 1)
      {
        return retValue;
      }
      /************************************************/
      ManagementClass myClass = new ManagementClass("Win32_Environment");
      /************************************************/
      ManagementObject myObj = myClass.CreateInstance();
      /************************************************/
      myObj["UserName"     ] = account;
      myObj["Name"         ] = variable;
      myObj["VariableValue"] = value;
      /************************************************/
      myObj.Put();
      /************************************************/
      retValue = true;
      /************************************************/  
      return retValue;
    }
  }
}