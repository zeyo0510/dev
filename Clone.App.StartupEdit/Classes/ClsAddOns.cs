using System;
using System.Security.Principal;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Text;


namespace StartupEdit
{
	/// <summary>
	/// Holds the following methods
	/// 1) public void FirstLaunch ()
	/// 2) public string AnalyzeIt ( string MyString )
	/// 3) internal IntPtr AHandle(  string TheIconPath )
	/// 4) internal string ReportOS()
	/// </summary>
	internal class ClsAddOns
	{
		public ClsAddOns () { }

		#region First Initialization

		/// <summary>
		/// This Method responsible for creating necessary keys in the registry
		/// Avoiding Errors of keys not found ,we call this method in our first launch
		/// of the program only
		/// </summary>
		public void FirstLaunch ()
		{
				string QadKey = null;
				string GlobalString =  @"SOFTWARE";

				RegistryKey GlobalReg = Registry.LocalMachine.OpenSubKey ( GlobalString, true );
			
				QadKey = @"AlQademoUn\StartEdit\LM";
			
				GlobalReg.CreateSubKey ( QadKey + @"\Run" );
				GlobalReg.CreateSubKey ( QadKey + @"\RunOnce" );
				GlobalReg.CreateSubKey ( QadKey + @"\RunOnceEx" );
				GlobalReg.CreateSubKey ( QadKey + @"\RunServices" );
				GlobalReg.CreateSubKey ( QadKey + @"\RunServicesOnce" );

				QadKey = @"AlQademoUn\StartEdit\" + Environment.UserName;

				GlobalReg.CreateSubKey ( QadKey + @"\Run" );
				GlobalReg.CreateSubKey ( QadKey + @"\RunOnce" );
			
				GlobalReg.Close();
		}


		#endregion

		#region Analyze Method
		
		/// <summary>
		/// Method To Remove Leading Spaces Or Symboles Like /, " Or -
		/// Found In SomeActionsHere
		/// </summary>
		/// <param name="MyString">String To Analyze</param>
		/// <returns>Returns The String Without These Symboles</returns>
		public string AnalyzeIt ( string MyString )
		{	
			if ( MyString.StartsWith ( "RUNDLL32.EXE" )  || MyString.StartsWith ( "rundll32.exe" ) )
			{ 
				MyString = MyString.Remove (0, 12); 
			}

			MyString.Trim();

			int MyLenght = MyString.Length; 
			char[] cSlaHyp = {'-','/', '"',','}; 
			int iSlaHyp = 0;

			if ( MyString.StartsWith ( "\"" ) )
			{
				MyString = MyString.Substring ( 1 );
				MyString.Trim();
			}

			iSlaHyp = MyString.LastIndexOfAny ( cSlaHyp );

			if ( iSlaHyp > -1 )
			{
				MyString = MyString.Substring ( 0, iSlaHyp );
				MyString.Trim();

				MyString = AnalyzeIt ( MyString );
				MyString.Trim();
			}

			return MyString;
		}


		#endregion

		#region Return Icon Handle  Method

		/// <summary> 
		/// This Method takes the path to the file and then returns its icon handle HICON
		/// </summary>
		/// <param name="TheIconPath">Path To File</param>
		/// <returns>The Icon Handle</returns>
		
		internal IntPtr AHandle(  string TheIconPath )
		{
			IntPtr GlobalIntPtr = IntPtr.Zero;
			uint GlobalImageIndex = 0;
			return
				ClsAPICalls.GetExtensionIcon ( GlobalIntPtr, AnalyzeIt ( TheIconPath ), ref GlobalImageIndex );
		}


		#endregion

		#region Get Os and the user

		/// <summary>
		/// this method used to check if The User is an admin or not
		/// </summary>
		/// <returns>true if is administrator otherwise false </returns>
		internal string ReportOS()
		{
			OperatingSystem MyOs = Environment.OSVersion;
            PlatformID thePID = MyOs.Platform;
			WindowsPrincipal ItsPrince = new WindowsPrincipal ( WindowsIdentity.GetCurrent() );

            switch (thePID)
            {
                case PlatformID.Win32Windows: // this is win98, winme
                    return "A None WinXp System";

                case PlatformID.Win32NT:  // this is Win NT 4.0, 2000, XP, vista, 7
                    {
                        if (ItsPrince.IsInRole(WindowsBuiltInRole.Administrator))
                        {
                            return "WinXp With Admin";
                        }
                        else
                        {
                            return "WinXp Without Admin";

                        }
                    }
                default:
                    return "OUTSIDER";
            }
		}	

		#endregion
	}
}
