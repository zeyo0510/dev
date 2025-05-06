using System;
/************************************************/
namespace AudioCore
{
  public struct PropertyKey
  {
    public Guid fmtid;

    public int pid;

    public PropertyKey(Guid P_0, int P_1)
    {
      fmtid = P_0;
      pid   = P_1;
    }
  }
}