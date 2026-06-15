using System;
using System.Collections;
/************************************************/
namespace ShellDll
{
  public class ShellItemEnumerator : IEnumerator
  {
    private ShellItem parent;
    /************************************************/
    private int index;
    /************************************************/
    public ShellItemEnumerator(ShellItem parent)
    {
      this.parent = parent;
      index = -1;
    }
    /************************************************/
    public object Current
    {
      get
      {
        return parent[index];
      }
    }
    /************************************************/
    public bool MoveNext()
    {
      index++;
      /************************************************/
      return (index < parent.Count);
    }
    /************************************************/
    public void Reset()
    {
      index = -1;
    }
  }
}