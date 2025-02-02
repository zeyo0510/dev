using System;
/************************************************/
namespace App.EnvironmentVariableEditor.Core
{
  public partial class EnvVarAccount
  {
    public EnvVarAccount(string name, EnvVarAccountType type)
    {
      this.Name = name;
      this.Type = type;
    }
  }
}