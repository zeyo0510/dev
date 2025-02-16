using System;
/************************************************/
namespace App.EnvironmentVariableEditor.Main
{
  partial class MainForm
  {
    private EnvironmentVariableTarget currentEnvironmentVariable = EnvironmentVariableTarget.User;
    /************************************************/
    public EnvironmentVariableTarget CurrentEnvironmentVariable
    {
      get
      {
        EnvironmentVariableTarget retValue = this.currentEnvironmentVariable;
        /************************************************/
        return retValue;
      }
      set
      {
        this.currentEnvironmentVariable = value;
        /************************************************/
        this.ReloadEnvVar();
      }
    }
  }
}