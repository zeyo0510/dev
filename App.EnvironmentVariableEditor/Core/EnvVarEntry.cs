using System;
/************************************************/
namespace App.EnvironmentVariableEditor.Core
{
  public partial class EnvVarEntry
  {
    public EnvVarEntry(string account, string variable, string value)
    {
      this.Account  = account;
      this.Variable = variable;
      this.Value    = value;
    }
  }
}