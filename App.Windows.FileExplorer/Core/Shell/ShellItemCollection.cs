using System;
using System.Collections;
/************************************************/
namespace ShellDll
{
  internal class ShellItemCollection : IEnumerable
  {
    private ArrayList items;
    /************************************************/
    public ShellItemCollection(ShellItem shellItem)
    {
        this.ShellItem = shellItem;
        items = new ArrayList();
    }
    /************************************************/
    public ShellItem ShellItem
    {
      get; private set;
    }
    /************************************************/
    public int Count
    {
      get
      {
        return items.Count;
      }
    }
    /************************************************/
    public void Sort()
    {
        items.Sort();
    }
    /************************************************/
    internal int Capacity
    {
      get
      {
        return items.Capacity;
      }
      set
      {
        items.Capacity = value;
      }
    }
    /************************************************/
    internal int Add(ShellItem value)
    {
      return items.Add(value);
    }
    /************************************************/
    internal void Clear()
    {
      items.Clear();
    }
    /************************************************/
    public bool Contains(ShellItem value)
    {
      return items.Contains(value);
    }
    /************************************************/
    public bool Contains(string name)
    {
      foreach (ShellItem item in this)
      {
        if (string.Compare(item.Text, name, true) == 0)
        {
          return true;
        }
      }
      /************************************************/
      return false;
    }
    /************************************************/
    public bool Contains(IntPtr pidl)
    {
      foreach (ShellItem item in this)
      {
        if (item.PIDLRel.Equals(pidl))
        {
          return true;
        }
      }
      /************************************************/
      return false;
    }
    /************************************************/
    public int IndexOf(ShellItem value)
    {
      return items.IndexOf(value);
    }
    /************************************************/
    public int IndexOf(string name)
    {
      for (int i = 0; i < items.Count; i++)
      {
        if (string.Compare(this[i].Text, name, true) == 0)
        {
          return i;
        }
      }
      /************************************************/
      return -1;
    }
    /************************************************/
    public int IndexOf(IntPtr pidl)
    {
      for (int i = 0; i < items.Count; i++)
      {
        if (this[i].PIDLRel.Equals(pidl))
        {
          return i;
        }
      }
      /************************************************/
      return -1;
    }
    /************************************************/
    internal void Insert(int index, ShellItem value)
    {
      items.Insert(index, value);
    }
    /************************************************/
    public bool IsFixedSize
    {
      get
      {
        return items.IsFixedSize;
      }
    }
    /************************************************/
    public bool IsReadOnly
    {
      get
      {
        return items.IsReadOnly;
      }
    }
    /************************************************/
    internal void Remove(ShellItem value)
    {
      items.Remove(value);
    }
    /************************************************/
    internal void Remove(string name)
    {
      int index;
      /************************************************/
      if ((index = IndexOf(name)) > -1)
      {
        RemoveAt(index);
      }
    }
    /************************************************/
    internal void RemoveAt(int index)
    {
      items.RemoveAt(index);
    }
    /************************************************/
    public ShellItem this[int index]
    {
      get
      {
        try
        {
          return (ShellItem)items[index];
        }
        catch (ArgumentOutOfRangeException)
        {
          return null;
        }
      }
      set
      {
        items[index] = value;
      }
    }
    /************************************************/
    public ShellItem this[string name]
    {
      get
      {
        int index;
        if ((index = IndexOf(name)) > -1)
        {
          return (ShellItem)items[index];
        }
        else
        {
          return null;
        }
      }
      set
      {
        int index;
        if ((index = IndexOf(name)) > -1)
        {
          items[index] = value;
        }
      }
    }
    /************************************************/
    public ShellItem this[IntPtr pidl]
    {
      get
      {
        int index;
        if ((index = IndexOf(pidl)) > -1)
        {
          return (ShellItem)items[index];
        }
        else
        {
          return null;
        }
      }
      set
      {
        int index;
        if ((index = IndexOf(pidl)) > -1)
        {
          items[index] = value;
        }
      }
    }
    /************************************************/
    public IEnumerator GetEnumerator()
    {
      return items.GetEnumerator();
    }
  }
}