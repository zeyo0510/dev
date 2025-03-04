﻿using System;
using System.Globalization;
using System.Net;
using System.IO;
using System.Net.Cache;
using System.Xml;
using System.Reflection;
using Microsoft.Win32;
using System.ComponentModel;
using System.Threading;

namespace AutoUpdaterDotNET
{

    public enum RemindLaterFormat
    {
        Minutes,
        Hours,
        Days
    }

    /// <summary>
    /// Main class that lets you auto update applications by setting some static fields and executing its Start method.
    /// </summary>
    public static class AutoUpdater
    {
        internal static String DialogTitle;

        internal static String ChangeLogURL;

        internal static String DownloadURL;

        internal static String RegistryLocation;

        internal static String AppTitle;

        internal static Version CurrentVersion;

        internal static Version InstalledVersion;

        internal static Boolean ForceCheck;

        //internal static CultureInfo CurrentCulture;

        /// <summary>
        /// URL of the xml file that contains information about latest version of the application.
        /// </summary>
        /// 
        public static String AppCastURL;

        /// <summary>
        /// Opens the download url in default browser if true. Very usefull if you have portable application.
        /// </summary>
        public static bool OpenDownloadPage;

        /// <summary>
        /// Sets the current culture of the auto update notification window. Set this value if your application supports functionalty to change the languge of the application.
        /// </summary>
        public static CultureInfo CurrentCulture;

        /// <summary>
        /// If this is true users see dialog where they can set remind later interval otherwise it will take the interval from RemindLaterAt and RemindLaterTimeSpan fields.
        /// </summary>
        public static Boolean LetUserSelectRemindLater = true;

        /// <summary>
        /// Remind Later interval after user should be reminded of update.
        /// </summary>
        public static int RemindLaterAt = 2;

        /// <summary>
        /// Set if RemindLaterAt interval should be in Minutes, Hours or Days.
        /// </summary>
        public static RemindLaterFormat RemindLaterTimeSpan = RemindLaterFormat.Days;

        /// <summary>
        /// Start checking for new version of application and display dialog to the user if update is available.
        /// </summary>
        /// <param name="forceUpdate">If true, ignores remind later and checks for update right away</param>
        public static void Start(bool forceUpdate = false)
        {
            Start(AppCastURL, forceUpdate);
        }

        /// <summary>
        /// Start checking for new version of application and display dialog to the user if update is available.
        /// </summary>
        /// <param name="appCast">URL of the xml file that contains information about latest version of the application.</param>
        /// <param name="forceUpdate">If true, ignores remind later and checks for update right away</param>
        public static void Start(String appCast, bool forceUpdate = false)
        {
            AppCastURL = appCast;
            ForceCheck = forceUpdate;

            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            if (ci.Name == "zh-CHS")
                CurrentCulture = new CultureInfo(0x0804); // zh-CN Chinese (People's Republic of China)
            else if (ci.Name == "zh-CHT")
                CurrentCulture = new CultureInfo(0x0404); // zh-TW Chinese (Taiwan)
            else
                CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            
            var backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += BackgroundWorkerDoWork;

            backgroundWorker.RunWorkerAsync();
        }

        private static void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var mainAssembly = Assembly.GetEntryAssembly();
            var companyAttribute = (AssemblyCompanyAttribute) GetAttribute(mainAssembly, typeof (AssemblyCompanyAttribute));
            var titleAttribute = (AssemblyTitleAttribute) GetAttribute(mainAssembly, typeof (AssemblyTitleAttribute));
            AppTitle = titleAttribute != null ? titleAttribute.Title : mainAssembly.GetName().Name;
            var appCompany = companyAttribute != null ? companyAttribute.Company : "";

            RegistryLocation = !string.IsNullOrEmpty(appCompany) ? string.Format(@"Software\{0}\{1}\AutoUpdater", appCompany, AppTitle) : string.Format(@"Software\{0}\AutoUpdater", AppTitle);

            RegistryKey updateKey = Registry.CurrentUser.OpenSubKey(RegistryLocation);

            if (updateKey != null && ForceCheck == false)
            {
                object remindLaterTime = updateKey.GetValue("remindlater");
 
                if (remindLaterTime != null)
                {
                    DateTime remindLater = Convert.ToDateTime(remindLaterTime.ToString(), CultureInfo.CreateSpecificCulture("en-US"));

                    int compareResult = DateTime.Compare(DateTime.Now, remindLater);

                    if (compareResult < 0)
                    {
                        var updateForm = new UpdateForm(true);
                        updateForm.SetTimer(remindLater);
                        return;
                    }
                }
            }

            var fileVersionAttribute = (AssemblyFileVersionAttribute)GetAttribute(mainAssembly, typeof(AssemblyFileVersionAttribute));
            InstalledVersion = new Version(fileVersionAttribute.Version);

            WebRequest webRequest = WebRequest.Create(AppCastURL);
            webRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            WebResponse webResponse;

            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (Exception)
            {
                return;
            }

            Stream appCastStream = webResponse.GetResponseStream();

            var receivedAppCastDocument = new XmlDocument();

            if (appCastStream != null) receivedAppCastDocument.Load(appCastStream);
            else return;

            XmlNodeList appCastItems = receivedAppCastDocument.SelectNodes("item");

            if (appCastItems != null)
                foreach (XmlNode item in appCastItems)
                {
                    XmlNode appCastVersion = item.SelectSingleNode("version");
                    if (appCastVersion != null)
                    {
                        String appVersion = appCastVersion.InnerText;
                        var version = new Version(appVersion);
                        if (version <= InstalledVersion)
                            continue;
                        CurrentVersion = version;
                    }
                    else
                        continue;

                    XmlNode appCastTitle = item.SelectSingleNode("title");

                    DialogTitle = appCastTitle != null ? appCastTitle.InnerText : "";

                    XmlNode appCastChangeLog = item.SelectSingleNode("changelog");

                    ChangeLogURL = appCastChangeLog != null ? appCastChangeLog.InnerText : "";

                    XmlNode appCastUrl = item.SelectSingleNode("url");

                    DownloadURL = appCastUrl != null ? appCastUrl.InnerText : "";
                }

            if (updateKey != null)
            {
                object skip = updateKey.GetValue("skip");
                object applicationVersion = updateKey.GetValue("version");
                if (skip != null && applicationVersion != null)
                {
                    string skipValue = skip.ToString();
                    var skipVersion = new Version(applicationVersion.ToString());
                    if (skipValue.Equals("1") && CurrentVersion <= skipVersion)
                        return;
                    if (CurrentVersion > skipVersion)
                    {
                        RegistryKey updateKeyWrite = Registry.CurrentUser.CreateSubKey(RegistryLocation);
                        if (updateKeyWrite != null)
                        {
                            updateKeyWrite.SetValue("version", CurrentVersion.ToString());
                            updateKeyWrite.SetValue("skip", 0);
                        }
                    }
                }
                updateKey.Close();
            }

            if (CurrentVersion != null && CurrentVersion > InstalledVersion)
            {
                var thread = new Thread(ShowUI);
                thread.CurrentCulture = thread.CurrentUICulture = CurrentCulture ?? System.Windows.Forms.Application.CurrentCulture;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else if (ForceCheck == true)
            {
                System.Windows.Forms.MessageBox.Show(Properties.Resources.updateLatest, Properties.Resources.updateTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private static void ShowUI()
        {
            var updateForm = new UpdateForm();
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            updateForm.ShowDialog();
        }

        private static Attribute GetAttribute (Assembly assembly,Type attributeType)
        {
            var attributes = assembly.GetCustomAttributes ( attributeType, false );
            if ( attributes.Length == 0 )
            {
                return null;
            }
            return (Attribute) attributes[0];
        }
    }
}
