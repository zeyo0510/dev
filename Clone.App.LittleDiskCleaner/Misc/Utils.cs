/*
    Little Disk Cleaner
    Copyright (C) 2008-2010 Little Apps  (http://www.littleapps.co.cc/)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Little_Disk_Cleaner
{
    public static class Utils
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
        public struct SHELLEXECUTEINFO
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpVerb;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpFile;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpParameters;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }

        private const int SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
        const int FO_DELETE = 3;
        const int FOF_ALLOWUNDO = 0x40;
        const int FOF_NOCONFIRMATION = 0x10;    //Don't prompt the user.; 

        public static void ShowFileProperties(string Filename)
        {
            SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = Filename;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            ShellExecuteEx(ref info);
        }

        public static void SendFileToRecycleBin(string filePath)
        {
            SHFILEOPSTRUCT shf = new SHFILEOPSTRUCT();
            shf.wFunc = FO_DELETE;
            shf.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
            shf.pFrom = filePath;
            SHFileOperation(ref shf); 
        }

        /// <summary>
        /// Compare multiple wildcards to string
        /// </summary>
        /// <param name="WildString">String to compare</param>
        /// <param name="Mask">Wildcard masks seperated by a semicolon (;)</param>
        /// <returns>True if match found</returns>
        public static bool CompareWildcards(string WildString, string Mask, bool IgnoreCase = true)
        {
            int i = 0;

            if (String.IsNullOrEmpty(Mask))
                return false;
            if (Mask == "*")
                return true;

            while (i != Mask.Length)
            {
                if (CompareWildcard(WildString, Mask.Substring(i), IgnoreCase))
                    return true;

                while (i != Mask.Length && Mask[i] != ';')
                    i += 1;

                if (i != Mask.Length && Mask[i] == ';')
                {
                    i += 1;

                    while (i != Mask.Length && Mask[i] == ' ')
                        i += 1;
                }
            }

            return false;
        }

        /// <summary>
        /// Compares wildcard to string
        /// </summary>
        /// <param name="WildString">String to compare</param>
        /// <param name="Mask">Wildcard mask (ex: *.jpg)</param>
        /// <returns>True if match found</returns>
        public static bool CompareWildcard(string WildString, string Mask, bool IgnoreCase = true)
        {
            int i = 0, k = 0;

            while (k != WildString.Length)
            {
                switch (Mask[i])
                {
                    case '*':

                        if ((i + 1) == Mask.Length)
                            return true;

                        while (k != WildString.Length)
                        {
                            if (CompareWildcard(WildString.Substring(k + 1), Mask.Substring(i + 1), IgnoreCase))
                                return true;

                            k += 1;
                        }

                        return false;

                    case '?':

                        break;

                    default:

                        if (IgnoreCase == false && WildString[k] != Mask[i])
                            return false;

                        if (IgnoreCase && Char.ToLower(WildString[k]) != Char.ToLower(Mask[i]))
                            return false;

                        break;
                }

                i += 1;
                k += 1;
            }

            if (k == WildString.Length)
            {
                if (i == Mask.Length || Mask[i] == ';' || Mask[i] == '*')
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Converts the size in bytes to a formatted string
        /// </summary>
        /// <param name="Length">Size in bytes</param>
        /// <returns>Formatted String</returns>
        public static string ConvertSizeToString(long Length)
        {
            if (Length <= 0)
                return "0 B";

            float nSize;
            string strSizeFmt, strUnit = "";

            if (Length < 1000)             // 1KB
            {
                nSize = Length;
                strUnit = " B";
            }
            else if (Length < 1000000)     // 1MB
            {
                nSize = Length / (float)0x400;
                strUnit = " KB";
            }
            else if (Length < 1000000000)   // 1GB
            {
                nSize = Length / (float)0x100000;
                strUnit = " MB";
            }
            else
            {
                nSize = Length / (float)0x40000000;
                strUnit = " GB";
            }

            if (nSize == (int)nSize)
                strSizeFmt = nSize.ToString("0");
            else if (nSize < 10)
                strSizeFmt = nSize.ToString("0.00");
            else if (nSize < 100)
                strSizeFmt = nSize.ToString("0.0");
            else
                strSizeFmt = nSize.ToString("0");

            return strSizeFmt + strUnit;
        }

        /// <summary>
        /// Calculates size of directory
        /// </summary>
        /// <param name="directory">DirectoryInfo class</param>
        /// <param name="includeSubdirectories">Includes sub directories (default true)</param>
        /// <returns>Size of directory in bytes</returns>
        public static long CalculateDirectorySize(DirectoryInfo directory, bool includeSubdirectories = true)
        {
            long totalSize = 0;

            // Examine all contained files.
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                totalSize += file.Length;
            }

            // Examine all contained directories.
            if (includeSubdirectories)
            {
                DirectoryInfo[] dirs = directory.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    totalSize += CalculateDirectorySize(dir, true);
                }
            }

            return totalSize;
        }

        /// <summary>
        /// Checks for default program then launches URI
        /// </summary>
        /// <param name="WebAddress">The address to launch</param>
        public static void LaunchURI(string WebAddress)
        {
            System.Windows.Forms.Help.ShowHelp(System.Windows.Forms.Form.ActiveForm, string.Copy(WebAddress));
        }
    }
}
