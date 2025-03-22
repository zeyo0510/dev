using System;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ShellDll
{
    public static class ShellImageList
    {
        private static IntPtr smallImageListHandle, largeImageListHandle;
        private static Hashtable imageTable;

        private const int TVSIL_NORMAL = 0;
        private const int TVSIL_SMALL = 1;

        static ShellImageList()
        {
            imageTable = new Hashtable();

            WinAPI.SHGFI flag = WinAPI.SHGFI.USEFILEATTRIBUTES | WinAPI.SHGFI.SYSICONINDEX | WinAPI.SHGFI.SMALLICON;
            WinAPI.SHFILEINFO shfiSmall = new WinAPI.SHFILEINFO();
            smallImageListHandle = WinAPI.SHGetFileInfo(".txt", WinAPI.FILE_ATTRIBUTE.NORMAL, ref shfiSmall, WinAPI.cbFileInfo, flag);

            flag = WinAPI.SHGFI.USEFILEATTRIBUTES | WinAPI.SHGFI.SYSICONINDEX | WinAPI.SHGFI.LARGEICON;
            WinAPI.SHFILEINFO shfiLarge = new WinAPI.SHFILEINFO();
            largeImageListHandle = WinAPI.SHGetFileInfo(".txt", WinAPI.FILE_ATTRIBUTE.NORMAL, ref shfiLarge, WinAPI.cbFileInfo, flag);
        }

        internal static void SetIconIndex(ShellItem item, int index, bool SelectedIcon)
        {
            bool HasOverlay = false; //true if it's an overlay
            int rVal = 0; //The returned Index

            WinAPI.SHGFI dwflag = WinAPI.SHGFI.SYSICONINDEX | WinAPI.SHGFI.PIDL | WinAPI.SHGFI.ICON;
            WinAPI.FILE_ATTRIBUTE dwAttr = 0;
            //build Key into HashTable for this Item
            int Key = index * 256;
            if (item.IsLink)
            {
                Key = Key | 1;
                dwflag = dwflag | WinAPI.SHGFI.LINKOVERLAY;
                HasOverlay = true;
            }
            if (item.IsShared)
            {
                Key = Key | 2;
                dwflag = dwflag | WinAPI.SHGFI.ADDOVERLAYS;
                HasOverlay = true;
            }
            if (SelectedIcon)
            {
                Key = Key | 4;
                dwflag = dwflag | WinAPI.SHGFI.OPENICON;
                HasOverlay = true; //not really an overlay, but handled the same
            }
            
            if (imageTable.ContainsKey(Key))
            {
                rVal = (int)imageTable[Key];
            }
            else if (!HasOverlay && !item.IsHidden) //for non-overlay icons, we already have
            {                
                rVal = (int)System.Math.Floor((double)Key / 256); // the right index -- put in table
                imageTable[Key] = rVal;
            }
            else //don't have iconindex for an overlay, get it.
            {
                if (item.IsFileSystem & !item.IsDisk & !item.IsFolder)
                {
                    dwflag = dwflag | WinAPI.SHGFI.USEFILEATTRIBUTES;
                    dwAttr = dwAttr | WinAPI.FILE_ATTRIBUTE.NORMAL;
                }

                PIDL pidlFull = item.PIDLFull;

                WinAPI.SHFILEINFO shfiSmall = new WinAPI.SHFILEINFO();
                WinAPI.SHGetFileInfo(pidlFull.Ptr, dwAttr, ref shfiSmall, WinAPI.cbFileInfo, dwflag | WinAPI.SHGFI.SMALLICON);

                WinAPI.SHFILEINFO shfiLarge = new WinAPI.SHFILEINFO();
                WinAPI.SHGetFileInfo(pidlFull.Ptr, dwAttr, ref shfiLarge, WinAPI.cbFileInfo, dwflag | WinAPI.SHGFI.LARGEICON);

                Marshal.FreeCoTaskMem(pidlFull.Ptr);

                lock (imageTable)
                {
                    rVal = WinAPI.ImageList_ReplaceIcon(smallImageListHandle, -1, shfiSmall.hIcon);
                    WinAPI.ImageList_ReplaceIcon(largeImageListHandle, -1, shfiLarge.hIcon);
                }

                WinAPI.DestroyIcon(shfiSmall.hIcon);
                WinAPI.DestroyIcon(shfiLarge.hIcon);
                imageTable[Key] = rVal;
            }

            if (SelectedIcon)
                item.SelectedImageIndex = rVal;
            else
                item.ImageIndex = rVal;
        }

        public static Icon GetIcon(int index, bool small)
        {
            IntPtr iconPtr;

            if (small)
                iconPtr = WinAPI.ImageList_GetIcon(smallImageListHandle, index, WinAPI.ILD.NORMAL);
            else
                iconPtr = WinAPI.ImageList_GetIcon(largeImageListHandle, index, WinAPI.ILD.NORMAL);

            if (iconPtr != IntPtr.Zero)
            {
                Icon icon = Icon.FromHandle(iconPtr);
                Icon retVal = (Icon)icon.Clone();
                WinAPI.DestroyIcon(iconPtr);
                return retVal;
            }
            else
                return null;
        }

        internal static IntPtr SmallImageList { get { return smallImageListHandle; } }
        internal static IntPtr LargeImageList { get { return largeImageListHandle; } }

        #region Set Small Handle

        internal static void SetSmallImageList(TreeView treeView)
        {
            WinAPI.SendMessage(treeView.Handle, WinAPI.WM.TVM_SETIMAGELIST, TVSIL_NORMAL, smallImageListHandle);
        }

        internal static void SetSmallImageList(ListView listView)
        {
            WinAPI.SendMessage(listView.Handle, WinAPI.WM.LVM_SETIMAGELIST, TVSIL_SMALL, smallImageListHandle);
        }

        #endregion

        #region Set Large Handle

        internal static void SetLargeImageList(ListView listView)
        {
            WinAPI.SendMessage(listView.Handle, WinAPI.WM.LVM_SETIMAGELIST, TVSIL_NORMAL, largeImageListHandle);
        }

        #endregion
    }
}
