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
using Microsoft.Win32;

namespace Little_Registry_Cleaner.Scanners
{
    public class ApplicationPaths : ScannerBase
    {
        public override string ScannerName
        {
            get { return Strings.ApplicationPaths; }
        }

        /// <summary>
        /// Verifies programs in App Paths
        /// </summary>
        public static void Scan()
        {
            try
            {
                Main.Logger.WriteLine("Checking for invalid installer folders");
                ScanInstallFolders();

                Main.Logger.WriteLine("Checking for invalid application paths");
                ScanAppPaths();
            }
            catch (System.Security.SecurityException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private static void ScanInstallFolders()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\Folders");

            if (regKey == null)
                return;

            foreach (string strFolder in regKey.GetValueNames())
            {
                ScanDlg.CurrentScannedObject = strFolder;

                if (!Utils.DirExists(strFolder))
                    ScanDlg.StoreInvalidKey(Strings.InvalidFile, regKey.Name, strFolder);
            }
        }

        private static void ScanAppPaths()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths");

            if (regKey == null)
                return;

            foreach (string strSubKey in regKey.GetSubKeyNames())
            {
                RegistryKey regKey2 = regKey.OpenSubKey(strSubKey);

                if (regKey2 != null)
                {
                    ScanDlg.CurrentScannedObject = regKey2.ToString();

                    if (Convert.ToInt32(regKey2.GetValue("BlockOnTSNonInstallMode")) == 1)
                        continue;

                    string strAppPath = regKey2.GetValue("") as string;
                    string strAppDir = regKey2.GetValue("Path") as string;

                    if (string.IsNullOrEmpty(strAppPath))
                    {
                        ScanDlg.StoreInvalidKey(Strings.InvalidRegKey, regKey2.ToString());
                        continue;
                    }

                    if (!string.IsNullOrEmpty(strAppDir))
                    {
                        if (Utils.SearchPath(strAppPath, strAppDir))
                            continue;
                        else if (Utils.SearchPath(strSubKey, strAppDir))
                            continue;
                    }
                    else
                    {
                        if (Utils.FileExists(strAppPath))
                            continue;
                    }

                    ScanDlg.StoreInvalidKey(Strings.InvalidFile, regKey2.Name);
                }
            }

            regKey.Close();
        }
    }
}
