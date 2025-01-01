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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Little_Registry_Cleaner.Scanners;
using Microsoft.Win32;
using Little_Registry_Cleaner.Xml;
using System.Security.Permissions;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Little_Registry_Cleaner
{
    public partial class ScanDlg : Form
    {
        public delegate void ScanDelegate();

        Thread threadMain, threadScan;

        private static string _currentObject = string.Empty;
        private static int _objectsScanned = 0;

        public static string CurrentScannedObject
        {
            get 
            { 
                return _currentObject;
            }
            set 
            {
                ScanDlg._currentObject = Utils.PrefixRegPath(value);
                ScanDlg._objectsScanned++;
            }
        }

        public static string CurrentSection
        {
            get;
            set;
        }

        private static ScannerBase currentScanner;

        public static BadRegKeyArray arrBadRegistryKeys = new BadRegKeyArray();

        public ScanDlg(int nSectionCount)
        {
            InitializeComponent();

            // Set the section count so it can be accessed later
            ScanDlg.arrBadRegistryKeys.Clear(nSectionCount);
        }

        
        private void ScanDlg_Shown(object sender, EventArgs e)
        {
            // Setup progress bar
            this.progressBar.Position = 0;
            this.progressBar.PositionMin = 0;
            this.progressBar.PositionMax = ScanDlg.arrBadRegistryKeys.SectionCount;

            SetProgressValue(0, ScanDlg.arrBadRegistryKeys.SectionCount);

            // Append 0 to problem label
            this.labelProblems.Text = string.Format("{0} 0", this.labelProblems.Text);

            // Starts scanning registry on seperate thread
            this.threadMain = new Thread(new ThreadStart(StartScanning));
            this.threadMain.Start();
        }

        /// <summary>
        /// Begins scanning for errors in the registry
        /// </summary>
        private void StartScanning()
        {
            // Get start time of scan
            DateTime dateTimeStart = DateTime.Now;

            // Begin Critical Region
            Thread.BeginCriticalRegion();

            // Begin scanning
            try
            {
                Main.Logger.WriteLine("Started scan at " + DateTime.Now.ToString());
                Main.Logger.WriteLine();

                if (Main.bScanStartup)
                    this.StartScanner(new StartupFiles());

                if (Main.bScanSharedDLL)
                    this.StartScanner(new SharedDLLs());

                if (Main.bScanFonts)
                    this.StartScanner(new WindowsFonts());

                if (Main.bScanAppInfo)
                    this.StartScanner(new ApplicationInfo());

                if (Main.bScanAppPaths)
                    this.StartScanner(new ApplicationPaths());

                if (Main.bScanActivex)
                    this.StartScanner(new ActivexComObjects());

                if (Main.bScanDrivers)
                    this.StartScanner(new SystemDrivers());

                if (Main.bScanHelpFiles)
                    this.StartScanner(new WindowsHelpFiles());

                if (Main.bScanSounds)
                    this.StartScanner(new WindowsSounds());

                if (Main.bScanAppSettings)
                    this.StartScanner(new ApplicationSettings());

                if (Main.bScanHistoryList)
                    this.StartScanner(new RecentDocs());

                this.DialogResult = DialogResult.OK;
            }
            catch (ThreadAbortException)
            {
                // Scanning was aborted
                Main.Logger.Write("User aborted scan... ");
                if (this.threadScan.IsAlive)
                    this.threadScan.Abort();

                this.DialogResult = DialogResult.Abort;
                Main.Logger.WriteLine("Exiting.\r\n");
            }
            finally
            {
                // Clear taskbar progress bar
                if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor > 0)
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);

                // Compute time between start and end of scan
                TimeSpan ts = DateTime.Now.Subtract(dateTimeStart);

                Main.Logger.WriteLine(string.Format("Total time elapsed: {0} seconds", ts.TotalSeconds));
                Main.Logger.WriteLine(string.Format("Total problems found: {0}", ScanDlg.arrBadRegistryKeys.Count));
                Main.Logger.WriteLine(string.Format("Total objects scanned: {0}", ScanDlg.arrBadRegistryKeys.ItemsScanned));
                Main.Logger.WriteLine();
                Main.Logger.WriteLine("Finished scan at " + DateTime.Now.ToString());
            }

            // End Critical Region
            Thread.EndCriticalRegion();

            // Dialog will be closed automatically

            return;
        }

        /// <summary>
        /// Starts a scanner
        /// </summary>
        public void StartScanner(ScannerBase scannerName)
        {
            currentScanner = scannerName;

            System.Reflection.MethodInfo mi = scannerName.GetType().GetMethod("Scan", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            ScanDelegate objScan = (ScanDelegate)Delegate.CreateDelegate(typeof(ScanDelegate), mi);
            
            Main.Logger.WriteLine("Starting scanning: " + scannerName.ScannerName);

            // Update section name
            scannerName.RootNode.SectionName = scannerName.ScannerName;
            scannerName.RootNode.Img = this.imageList.Images[scannerName.GetType().Name];
            ScanDlg.CurrentSection = scannerName.ScannerName;

            // Start scanning
            this.threadScan = new Thread(new ThreadStart(objScan));
            this.threadScan.Start();
            this.threadScan.Join();

            // Wait 250ms
            Thread.Sleep(250);

            if (scannerName.RootNode.Nodes.Count > 0)
                ScanDlg.arrBadRegistryKeys.Add(scannerName.RootNode);

            Main.Logger.WriteLine("Finished scanning: " + scannerName.ScannerName);
            Main.Logger.WriteLine();

            this.progressBar.Position++;
            SetProgressValue(this.progressBar.Position, ScanDlg.arrBadRegistryKeys.SectionCount);
        }

        private void SetProgressValue(int currentValue, int maxValue)
        {
            // Make sure OS is Windows 7 or greater
            if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor > 0)
            {
                TaskbarManager.Instance.SetProgressValue(currentValue, maxValue);
            }
        }

        /// <summary>
        /// <para>Stores an invalid registry key to array list</para>
        /// <para>Use IsOnIgnoreList to check for ignored registry keys and paths</para>
        /// </summary>
        /// <param name="Problem">Reason its invalid</param>
        /// <param name="Path">The path to registry key (including registry hive)</param>
        /// <returns>True if it was added</returns>
        public static bool StoreInvalidKey(string Problem, string Path)
        {
            return StoreInvalidKey(Problem, Path, "");
        }

        /// <summary>
        /// <para>Stores an invalid registry key to array list</para>
        /// <para>Use IsOnIgnoreList to check for ignored registry keys and paths</para>
        /// </summary>
        /// <param name="problem">Reason its invalid</param>
        /// <param name="regPath">The path to registry key (including registry hive)</param>
        /// <param name="valueName">Value name (leave blank if theres none)</param>
        /// <returns>True if it was added. Otherwise, false.</returns>
        public static bool StoreInvalidKey(string problem, string regPath, string valueName)
        {
            string baseKey, subKey;

            // Check for null parameters
            if (string.IsNullOrEmpty(problem) || string.IsNullOrEmpty(regPath))
                return false;

            // Make sure registry key exists
            if (string.IsNullOrEmpty(valueName))
            {
                if (!Utils.RegKeyExists(regPath))
                    return false;
            }
            else
            {
                if (!Utils.ValueNameExists(regPath, valueName))
                    return false;
            }

            // Parse registry key to base and subkey
            if (!Utils.ParseRegKeyPath(regPath, out baseKey, out subKey))
                return false;

            // Check for ignored registry path
            if (IsOnIgnoreList(regPath))
                return false;

            // If value name is specified, see if it exists
            if (!string.IsNullOrEmpty(valueName))
                if (!Utils.ValueNameExists(baseKey, subKey, valueName))
                    return false;

            // Make sure we have the correct permissions for the registry key
            if (!Utils.CanDeleteKey(Utils.RegOpenKey(baseKey, subKey)))
                return false;

            ScanDlg.currentScanner.RootNode.Nodes.Add(new BadRegistryKey(problem, baseKey, subKey, valueName));

            ScanDlg.arrBadRegistryKeys.Problems++;

            if (!string.IsNullOrEmpty(valueName))
                Main.Logger.WriteLine(string.Format("Bad Registry Value Found! Problem: \"{0}\" Path: \"{1}\" Value Name: \"{2}\"", problem, regPath, valueName));
            else
                Main.Logger.WriteLine(string.Format("Bad Registry Key Found! Problem: \"{0}\" Path: \"{1}\"", problem, regPath));

            return true;
        }

        /// <summary>
        /// Checks for the path in ignore list
        /// </summary>
        /// <returns>true if it is on the ignore list, otherwise false</returns>
        public static bool IsOnIgnoreList(string Path)
        {
            if (!string.IsNullOrEmpty(Path))
            {
                foreach (ExcludeList.ExcludeItem i in Properties.Settings.Default.arrayExcludeList)
                    if (string.Compare(i.ToString(), Path) == 0)
                        return true;
            }

            return false;
        }

        private void ScanDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (MessageBox.Show(this, Properties.Resources.scanDlgExit, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        e.Cancel = true;
                    else
                        this.threadMain.Abort();
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(typeof(ScanDlg));

            this.progressBar.Text = Properties.Resources.scanDlgProgressBarScanning + ScanDlg.CurrentSection;
            this.labelProblems.Text = string.Format("{0} {1}", rm.GetString("labelProblems.Text"), ScanDlg.arrBadRegistryKeys.Problems);
            this.textBoxSubKey.Text = ScanDlg.CurrentScannedObject;
        }
    }
}
