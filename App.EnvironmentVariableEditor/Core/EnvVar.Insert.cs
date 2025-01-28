using System;
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
      if (EnvVar.Query(user, variable).Length >= 1)
      {
        return retValue;
      }
      /************************************************/
      ManagementClass myClass = new ManagementClass("Win32_Environment");
      /************************************************/
      ManagementObject myObj = myClass.CreateInstance();
      /************************************************/
      myObj["UserName"     ] = user;
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