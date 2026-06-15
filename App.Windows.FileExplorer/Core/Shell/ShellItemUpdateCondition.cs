using System;
/************************************************/
namespace ShellDll
{
  internal class ShellItemUpdateCondition
  {
    public ShellItemUpdateCondition()
    {
      this.ContinueUpdate = true;
    }
    /************************************************/
    public bool ContinueUpdate
    {
      get; set;
    }
  }
}