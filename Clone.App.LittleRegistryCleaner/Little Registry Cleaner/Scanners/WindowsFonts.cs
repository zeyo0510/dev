﻿/*
    Little Registry Cleaner
    Copyright (C) 2008 Little Apps (http://www.little-apps.org/)

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
using Microsoft.Win32;

namespace Little_Registry_Cleaner.Scanners
{
    public class WindowsFonts : ScannerBase
    {
        public override string ScannerName
        {
            get { return Strings.WindowsFonts; }
        }

        [DllImport("shell32.dll")]
        public static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder strPath, int nFolder, bool fCreate);

        const int CSIDL_FONTS = 0x0014;    // windows\fonts 

        /// <summary>
        /// Finds invalid font references
        /// </summary>
        public static void Scan()
        {
            StringBuilder strPath = new StringBuilder(260);

            try
            {
                using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\Fonts"))
                {
                    if (regKey == null)
                        return;

                    Main.Logger.WriteLine("Scanning for invalid fonts");

                    if (!SHGetSpecialFolderPath(IntPtr.Zero, strPath, CSIDL_FONTS, false))
                        return;

                    foreach (string strFontName in regKey.GetValueNames())
                    {
                        string strValue = regKey.GetValue(strFontName) as string;

                        // Skip if value is empty
                        if (string.IsNullOrEmpty(strValue))
                            continue;

                        // Check value by itself
                        if (Utils.FileExists(strValue))
                            continue;

                        // Check for font in fonts folder
                        string strFontPath = String.Format("{0}\\{1}", strPath.ToString(), strValue);

                        ScanDlg.CurrentScannedObject = strFontPath;

                        if (!File.Exists(strFontPath))
                            ScanDlg.StoreInvalidKey(Strings.InvalidFile, regKey.ToString(), strFontName);
                    }

                }
            }
            catch (System.Security.SecurityException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
