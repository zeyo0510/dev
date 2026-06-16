using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ShellDll
{
  public sealed partial class ShellItem : IEnumerable
  {
    private IShellFolder shellFolder;
    private IntPtr shellFolderPtr;

    internal ShellItem(ShellBrowser browser, IntPtr pidl, IntPtr shellFolderPtr)
    {
        this.Browser = browser;

        this.shellFolderPtr = shellFolderPtr;
        this.shellFolder = (IShellFolder)Marshal.GetTypedObjectForIUnknown(shellFolderPtr, typeof(IShellFolder));
        SubFiles = new ShellItemCollection(this);
        SubFolders = new ShellItemCollection(this);

        PIDLRel = new PIDL(pidl, false);

        this.Text = "Desktop";
        this.Path = "Desktop";

        SetAttributesDesktop(this);

        WinAPI.SHFILEINFO info = new WinAPI.SHFILEINFO();
        WinAPI.SHGetFileInfo(PIDLRel.Ptr, 0, ref info, WinAPI.cbFileInfo, WinAPI.SHGFI.PIDL | WinAPI.SHGFI.TYPENAME | WinAPI.SHGFI.SYSICONINDEX);

        Type = info.szTypeName;

        ShellImageList.SetIconIndex(this, info.iIcon, false);
        ShellImageList.SetIconIndex(this, info.iIcon, true);

        SortFlag = 1;
    }

    internal ShellItem(ShellBrowser browser, ShellItem parentItem, IntPtr pidl, IntPtr shellFolderPtr)
    {
        this.Browser = browser;

        this.ParentItem = parentItem;
        this.shellFolderPtr = shellFolderPtr;
        this.shellFolder = (IShellFolder)Marshal.GetTypedObjectForIUnknown(shellFolderPtr, typeof(IShellFolder));
        SubFiles = new ShellItemCollection(this);
        SubFolders = new ShellItemCollection(this);

        PIDLRel = new PIDL(pidl, false);

        SetText(this);
        SetPath(this);
        SetAttributesFolder(this);
        SetInfo(this);

        SortFlag = MakeSortFlag(this);
    }

    internal ShellItem(ShellBrowser browser, ShellItem parentItem, IntPtr pidl)
    {
        this.Browser = browser;

        this.ParentItem = parentItem;

        PIDLRel = new PIDL(pidl, false);

        SetText(this);
        SetPath(this);
        SetAttributesFile(this);
        SetInfo(this);

        SortFlag = MakeSortFlag(this);
    }

    ~ShellItem()
    {
      ((IDisposable)this).Dispose();
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
        return new ShellItemEnumerator(this);
    }

    internal bool Contains(ShellItem value)
    {
        return (SubFolders.Contains(value) || SubFiles.Contains(value));
    }

    internal bool Contains(string name)
    {
        return (SubFolders.Contains(name) || SubFiles.Contains(name));
    }

    internal bool Contains(IntPtr pidl)
    {
        return (SubFolders.Contains(pidl) || SubFiles.Contains(pidl));
    }

    internal int IndexOf(ShellItem value)
    {
        int index;
        index = SubFolders.IndexOf(value);

        if (index > -1)
            return index;

        index = SubFiles.IndexOf(value);

        if (index > -1)
            return SubFolders.Count + index;

        return -1;
    }

    internal int IndexOf(string name)
    {
        int index;
        index = SubFolders.IndexOf(name);

        if (index > -1)
            return index;

        index = SubFiles.IndexOf(name);

        if (index > -1)
            return SubFolders.Count + index;

        return -1;
    }

    internal int IndexOf(IntPtr pidl)
    {
        int index;
        index = SubFolders.IndexOf(pidl);

        if (index > -1)
            return index;

        index = SubFiles.IndexOf(pidl);

        if (index > -1)
            return SubFolders.Count + index;

        return -1;
    }

    internal ShellItem this[int index]
    {
        get
        {
            if (index >= 0 && index < SubFolders.Count)
                return SubFolders[index];
            else if (index >= 0 && index - SubFolders.Count < SubFiles.Count)
                return SubFiles[index - SubFolders.Count];
            else
                throw new IndexOutOfRangeException();
        }
        set
        {
            if (index >= 0 && index < SubFolders.Count)
                SubFolders[index] = value;
            else if (index >= 0 && index - SubFolders.Count < SubFiles.Count)
                SubFiles[index - SubFolders.Count] = value;
            else
                throw new IndexOutOfRangeException();
        }
    }

    internal ShellItem this[string name]
    {
        get
        {
            ShellItem temp = SubFolders[name];

            if (temp != null)
                return temp;
            else
                return SubFiles[name];
        }
        set
        {
            ShellItem temp = SubFolders[name];

            if (temp != null)
                SubFolders[name] = value;
            else
                SubFiles[name] = value;
        }
    }

    internal ShellItem this[IntPtr pidl]
    {
        get
        {
            ShellItem temp = SubFolders[pidl];

            if (temp != null)
                return temp;
            else
                return SubFiles[pidl];
        }
        set
        {
            ShellItem temp = SubFolders[pidl];

            if (temp != null)
                SubFolders[pidl] = value;
            else
                SubFiles[pidl] = value;
        }
    }

    internal int Count
    {
        get { return SubFolders.Count + SubFiles.Count; }
    }
  }
}