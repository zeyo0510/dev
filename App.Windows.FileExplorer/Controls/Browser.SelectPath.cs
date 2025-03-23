using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ShellDll;
/************************************************/
namespace FileBrowser
{
  partial class Browser
  {
    public TreeNode SelectPath(string path, bool expandNode)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        if (PathExists(path))
        {
            string converted = ConvertPath(path);
            string[] pathParts = converted.Split('\\');

            TreeNode currentNode = null;

            #region Get Start Node
            // Change .Expand() to function which extends the node without expanding it

            if (string.Compare(pathParts[0], "desktop", true) == 0)
                currentNode = desktopNode;
            else if (desktopNode.Nodes.ContainsKey(pathParts[0]))
            {
                currentNode = desktopNode.Nodes[pathParts[0]];
                ExtendTreeNode(currentNode, false);
            }
            else
            {
                if (string.Compare(pathParts[0], myCompNode.Text, true) == 0)
                    currentNode = myCompNode;
                else
                {
                    if (pathParts[0][pathParts[0].Length - 1] == ':')
                        pathParts[0] += "\\";

                    foreach (TreeNode node in myCompNode.Nodes)
                    {
                        if (string.Compare(
                                pathParts[0],
                                ((ShellItem)node.Tag).Path, true) == 0)
                        {
                            currentNode = node;
                            ExtendTreeNode(currentNode, false);
                            break;
                        }
                    }
                }
            }

            #endregion

            if (currentNode == null)
            {
                folderView.EndUpdate();
                return null;
            }

            #region Iterate

            for (int i = 1; i < pathParts.Length; i++)
            {
                if (pathParts[i][pathParts[i].Length - 1] == ':')
                    pathParts[i] += "\\";

                bool found = false;
                foreach (TreeNode child in currentNode.Nodes)
                {
                    if (string.Compare(pathParts[i], child.Text, true) == 0)
                    {
                        currentNode = child;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    folderView.EndUpdate();
                    return null;
                }

                ExtendTreeNode(currentNode, false);
            }

            #endregion

            if (expandNode)
                currentNode.Expand();

            folderView.SelectedNode = currentNode;

            return currentNode;
        }
        else
            return null;
    }
    /************************************************/
    public TreeNode SelectPath(SpecialFolders specialFolder, bool expandNode)
    {
        StringBuilder path = new StringBuilder(256);
        IntPtr pidl = IntPtr.Zero;

        if (specialFolder == SpecialFolders.Desktop)
            return SelectPath("Desktop", expandNode);
        else if (WinAPI.SHGetFolderPath(
                IntPtr.Zero, (WinAPI.CSIDL)specialFolder,
                IntPtr.Zero, WinAPI.SHGFP.TYPE_CURRENT, path) == WinAPI.S_OK)
        {
            path.Replace(ShellBrowser.MyDocumentsPath, ShellBrowser.MyDocumentsName);
            return SelectPath(path.ToString(), expandNode);
        }
        else
        {
            #region Get Pidl

            if (specialFolder == SpecialFolders.MyDocuments)
            {
                uint pchEaten = 0;
                WinAPI.SFGAO pdwAttributes = 0;
                ShellBrowser.DesktopItem.ShellFolder.ParseDisplayName(
                    IntPtr.Zero,
                    IntPtr.Zero,
                    "::{450d8fba-ad25-11d0-98a8-0800361b1103}",
                    ref pchEaten,
                    out pidl,
                    ref pdwAttributes);
            }
            else
            {
                WinAPI.SHGetSpecialFolderLocation(
                    IntPtr.Zero,
                    (WinAPI.CSIDL)specialFolder,
                    out pidl);
            }

            #endregion

            #region Make Path

            if (pidl != IntPtr.Zero)
            {
                IntPtr strr = Marshal.AllocCoTaskMem(WinAPI.MAX_PATH * 2 + 4);
                Marshal.WriteInt32(strr, 0, 0);
                StringBuilder buf = new StringBuilder(WinAPI.MAX_PATH);

                if (ShellBrowser.DesktopItem.ShellFolder.GetDisplayNameOf(
                                pidl,
                                WinAPI.SHGNO.FORADDRESSBAR | WinAPI.SHGNO.FORPARSING,
                                strr) == WinAPI.S_OK)
                {
                    WinAPI.StrRetToBuf(strr, pidl, buf, WinAPI.MAX_PATH);
                }

                Marshal.FreeCoTaskMem(pidl);
                Marshal.FreeCoTaskMem(strr);

                if (!string.IsNullOrEmpty(buf.ToString()))
                    return SelectPath(buf.ToString(), expandNode);
                else
                    return null;
            }
            else
                return null;

            #endregion
        }
    }
    /************************************************/
    public TreeNode SelectPath(ShellItem item, bool expandNode)
    {
        if (item == null)
            return null;

        ShellItem[] path = ShellBrowser.GetPath(item);

        if (path != null)
        {
            TreeNode currentNode = desktopNode;
            for (int i = 1; i < path.Length; i++)
            {
                ExtendTreeNode(currentNode, false);
                foreach (TreeNode subNode in currentNode.Nodes)
                {
                    if (path[i].Equals(subNode.Tag))
                    {
                        currentNode = subNode;
                        break;
                    }
                }
            }

            if (expandNode)
                currentNode.Expand();

            folderView.SelectedNode = currentNode;

            return currentNode;
        }
        else
            return null;
    }

  }
}