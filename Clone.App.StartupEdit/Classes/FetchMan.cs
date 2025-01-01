using System;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;


namespace StartupEdit
{
	/// <summary>
	/// this class responsible for reading all the entries whether in the registry or in the folders
	/// this class has the folloing methods
	/// 1)internal void ReadRegistry ( string hkey, string SubBranch, string Suffix, bool TrueIfDisabled )
	/// 2)internal void ReadStartUpFolders ( string Root )
	/// 3)internal void GetMSConfigNoneXPRegistry ( string hkey, string Key, string EntryType, string StatusMsg )
	/// 4)internal void GetMSConfigNoneXPFolders( string Path, string EntryType, string StatusMsg )
	/// 5)internal void GetMSConfigXPRegistry()
	/// 6)internal void GetMSConfigXPFolders()
	/// 7)internal void GetLocalMahine( bool TrueIfDisabled )
	/// 8)internal void GetCurrentUser( bool TrueIfDisabled )
	/// 9)internal void GetStartupFolder( bool GetDisabled )
	/// </summary>
	public class FetchMan
	{
		#region Some Instance Variables, Property And The Constructor

		/// <summary>
		/// Main Reg For Manipulating Registry
		/// </summary>
		private RegistryKey GlobalReg;

		/// <summary>
		/// Main Dir For Startup Directory
		/// </summary>
		private DirectoryInfo GlobalDir;

		/// <summary>
		/// The ListView Which Holds Th Main Target
		/// </summary>
		private ListView GlobalView; 

		/// <summary>
		/// This Instance Of SomeActionHere Class Has The (Analyze) Method
		/// Which Is Responsble For Analyzing Strings and AHandle Method to get the handle to an icon
		/// </summary>
		private ClsAddOns GlobalGet = new ClsAddOns();

		/// <summary>
		/// ImageList For Holding The Icons Extracted From The Entries Larg 32*32
		/// </summary>
		private ImageList GlobalImageLarg;

		/// <summary>
		/// ImageList For Holding The Icons Extracted From The Entries Larg 16*16
		/// </summary>
		private ImageList GlobalImageSmall;

		/// <summary>
		/// To Increment The Imagelist
		/// </summary>
		private static int GlobalImageCounter = 0;

		/// <summary>
		/// To Create The ListViewItem
		/// </summary>
		private ListViewItem GlobalEntry;

		/// <summary>
		/// To Manipulate The Entries
		/// </summary>
		private string GlobalString;
		
		/// <summary>
		/// Use this class to get enabled and disabled Entries from both
		/// windows registry and startup folder 
		/// </summary>
		public FetchMan()
		{
			// Initialize the imagelist that holds the larg icons
			GlobalImageLarg = new ImageList();

			// Some Properties
			GlobalImageLarg.TransparentColor = System.Drawing.Color.Transparent;
			GlobalImageLarg.ColorDepth = ColorDepth.Depth32Bit;
			GlobalImageLarg.ImageSize = new Size( 32, 32 );

			// Initialize the imagelist that holds the larg icons
			GlobalImageSmall = new ImageList();

			GlobalImageSmall.TransparentColor = System.Drawing.Color.Transparent;
			GlobalImageSmall.ColorDepth = ColorDepth.Depth32Bit;
			GlobalImageSmall.ImageSize = new Size( 16, 16 );
		}


		/// <summary>
		/// Gets Or Sets The ListView Object
		/// </summary>
		internal ListView TheGlobalView 
		{
			get { return GlobalView; }
			set 
			{ 
				GlobalView = value; 

				// Imagelists used by the listview assign it now
				GlobalView.LargeImageList = GlobalImageLarg; 
				GlobalView.SmallImageList = GlobalImageSmall;
			}
		}


		#endregion

		#region Get Items
	
		#region Windows Registry

		/// <summary>
		/// To Read From windows Registry everything Enabled and Disabled
		/// </summary>
		/// <param name="hkey">Root Key LM, CU. CR</param>
		/// <param name="SubBranch">With disabled entries"LM" Or Environment.UserName</param>
		/// <param name="Suffix"> Run, RunOnce</param>
		/// <param name="TrueIfDisabled">if you are calling a disabled Item</param>
		internal void ReadRegistry ( string hkey, string SubBranch, string Suffix, bool TrueIfDisabled )
		{
			string TheKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\";
			string Status = "Enabled";

			if ( TrueIfDisabled )
			{
				TheKey = @"Software\AlQademoUn\StartEdit\" + SubBranch + @"\" ;
				Status = "Disabled";
			}

			try
			{
				switch ( hkey )
				{
					case "HKEY_LOCAL_MACHINE":
						GlobalReg = Registry.LocalMachine.OpenSubKey ( TheKey + Suffix, true );
						break;

					case "HKEY_CURRENT_USER":
						GlobalReg = Registry.CurrentUser.OpenSubKey ( TheKey + Suffix, true );
						break;

					case "HKEY_CLASSES_ROOT":
						GlobalReg = Registry.ClassesRoot.OpenSubKey ( TheKey + Suffix, true );
						break;
				}

				if ( GlobalReg.ValueCount != 0 )
				{
					foreach ( string SingleIt in GlobalReg.GetValueNames() )
					{
						GlobalString = GlobalReg.GetValue ( SingleIt, "Error" ).ToString();

						GlobalEntry = new ListViewItem();
						
						GlobalEntry.ImageIndex = GlobalImageCounter;
						GlobalImageLarg.Images.Add( Icon.FromHandle ( GlobalGet.AHandle ( GlobalString ) ) );
						GlobalImageSmall.Images.Add ( GlobalImageLarg.Images[GlobalImageCounter] );
						

						GlobalEntry.Text = SingleIt;							//Name
						GlobalEntry.SubItems.Add( GlobalString );				// Data
						GlobalEntry.SubItems.Add( hkey );						// Hive
						GlobalEntry.SubItems.Add( Suffix );						// Type
						GlobalEntry.SubItems.Add( Status );					// Status
						GlobalEntry.SubItems.Add(  /* GlobalReg.Name */ TheKey + Suffix  );	// Full Key Path
				
						GlobalView.Items.Add ( GlobalEntry );

						GlobalImageCounter ++;
					}

					GlobalReg.Close();
				}
			}
			catch ( InvalidOperationException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( hkey + "\\" + TheKey + Suffix + "\n" + MyEx.Message.Trim() );
			}
			catch ( System.Security.SecurityException MyExp )
			{
				MessageBox.Show 
					( "An error returned while trying to access \n" + hkey + "\\" + TheKey + Suffix + "\n" +
					"Error is " + MyExp.Message + "\n" + 
					"Startup Editor requires an administrartor privileges to run properly" ,"Access is denied");
				return;
			}
			catch ( ArgumentNullException MyEx )
			{
				//MyEx.Message.Trim();
				MessageBox.Show ( hkey + "\\" + TheKey + Suffix + "\n" + MyEx.Message.Trim() );
			}
			catch ( NullReferenceException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( hkey + "\\" + TheKey + Suffix + "\n" + MyEx.Message.Trim() );
			}
			catch ( Exception MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( hkey + "\\" + TheKey + Suffix + "\n" + MyEx.Message.Trim() );
			}
		}


		#endregion

		#region Startup Folders

		/// <summary>
		/// To read all the entries found in the startup folders
		/// </summary>
		/// <param name="Root">Put one of the following(Replacing Enabled with Disabled if needed) 
		/// All Users Enabled, User Name Enabled</param>
		internal void ReadStartUpFolders ( string Root )
		{
			string Status = "Enabled";
			try
			{
				switch ( Root )
				{
					case "All Users Enabled":
						Root = "All Users";
						GlobalReg = Registry.LocalMachine.OpenSubKey 
							( @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders" , true );
						GlobalDir = new DirectoryInfo ( GlobalReg.GetValue( "Common Startup", "C:\\" ).ToString() );

						GlobalReg.Close();
						break;

					case "User Name Enabled":
						Root = Environment.UserName;
						GlobalDir = new DirectoryInfo ( Environment.GetFolderPath ( Environment.SpecialFolder.Startup ));
						break;

					case "All Users Disabled":
						Root = "All Users";
						Status = "Disabled";
						GlobalDir = new DirectoryInfo ( Application.StartupPath + @"\BackUps\All Users" );
						break;

					case "User Name Disabled":
						Root = Environment.UserName ;
						Status = "Disabled";
						GlobalDir = new DirectoryInfo ( Application.StartupPath + @"\BackUps\" + Environment.UserName );

						break;
				}
				if ( GlobalDir.Exists )
				{
					foreach (  FileInfo SingleIt in GlobalDir.GetFiles ( "*.*" ) )
					{
						GlobalString = SingleIt.FullName;

						GlobalEntry = new ListViewItem();

						GlobalEntry.ImageIndex = GlobalImageCounter;
						GlobalImageLarg.Images.Add( Icon.FromHandle ( GlobalGet.AHandle ( GlobalString ) ) );
						GlobalImageSmall.Images.Add ( GlobalImageLarg.Images[GlobalImageCounter] );

						GlobalEntry.Text = SingleIt.Name;
						GlobalEntry.SubItems.Add ( GlobalString );
						GlobalEntry.SubItems.Add( "Startup Folder" );
						GlobalEntry.SubItems.Add( Root );
						GlobalEntry.SubItems.Add( Status );
						GlobalEntry.SubItems.Add( GlobalString ); // Full Key Path
				
						GlobalView.Items.Add ( GlobalEntry );

						GlobalImageCounter ++;
					}
				}
			}
			catch ( InvalidOperationException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( MyEx.Message +"\n" + MyEx.Source );
			}
			catch ( System.Security.SecurityException MyExp )
			{
				MessageBox.Show 
					( "An error returned while trying to access \n"  + "Error is " + MyExp.Message + "\n" + 
					"Startup Editor requires an administrartor privileges to run properly" ,"Access is denied");
				return;
			}
			catch ( ArgumentNullException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( MyEx.Message +"\n" + MyEx.Source );
			}
			catch ( NullReferenceException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( MyEx.Message +"\n" + MyEx.Source );
			}
			catch ( Exception MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( MyEx.Message +"\n" + MyEx.Source  );
			}
		}


		#endregion

		#endregion

		#region Microsoft Configuration Utility MSConfig.exe

		#region A none WinXP Systems

		internal void GetMSConfigNoneXPRegistry ( string hkey, string Key, string EntryType, string StatusMsg )
		{
			try
			{
				switch ( hkey )
				{
					case "HKEY_LOCAL_MACHINE":
						GlobalReg = Registry.LocalMachine.OpenSubKey ( Key, true );
						break;
					case "HKEY_CURRENT_USER":
						GlobalReg = Registry.CurrentUser.OpenSubKey ( Key, true );
						break;
					case "HKEY_CLASSES_ROOT":
						GlobalReg = Registry.ClassesRoot.OpenSubKey ( Key, true );
						break;
				}

				if ( GlobalReg.ValueCount != 0 )
				{

					foreach ( string SingleCustom in GlobalReg.GetValueNames() )
					{
						GlobalString = GlobalReg.GetValue ( SingleCustom, "Error" ).ToString();

						GlobalEntry = new ListViewItem();

						GlobalEntry.ImageIndex = GlobalImageCounter;

						GlobalImageLarg.Images.Add( Icon.FromHandle ( GlobalGet.AHandle ( GlobalString ) ) );

						GlobalImageSmall.Images.Add ( GlobalImageLarg.Images[GlobalImageCounter] );

						GlobalEntry.Text = SingleCustom;				//name
						GlobalEntry.SubItems.Add( GlobalString );	//data
						GlobalEntry.SubItems.Add( hkey );			//root
						GlobalEntry.SubItems.Add( EntryType );		// TYPE
						GlobalEntry.SubItems.Add( StatusMsg );		// Status
						GlobalEntry.SubItems.Add( Key );				// Full Key Path
				
						GlobalView.Items.Add ( GlobalEntry );

						GlobalImageCounter ++;
					}

					GlobalReg.Close();
				}
			}
			catch ( InvalidOperationException MyEx )
			{
				//MyEx.Message.Trim();
				MessageBox.Show ( hkey + "\\" + Key + "\n" + MyEx.Message.Trim() );
			}
			catch ( System.Security.SecurityException MyExp )
			{
				MessageBox.Show 
					( "An error returned while trying to access \n" + hkey + "\\" + "\n" +
					"Error is " + MyExp.Message + "\n" + 
					"Startup Editor requires an administrartor privileges to run properly" ,"Access is denied");
				return;
			}
			catch ( ArgumentNullException MyEx )
			{
				//MyEx.Message.Trim();
				MessageBox.Show ( hkey + "\\" + Key + "\n" + MyEx.Message.Trim() );
			}
			catch ( NullReferenceException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( hkey + "\\" + Key + "\n" + MyEx.Message.Trim() );
			}
			catch ( Exception MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( hkey + "\\" + Key + "\n" + MyEx.Message.Trim() );
			}
		}


		internal void GetMSConfigNoneXPFolders( string Path, string EntryType, string StatusMsg )
		{
			try
			{
				GlobalReg = Registry.LocalMachine.OpenSubKey ( @"SOFTWARE\Microsoft\Windows\CurrentVersion" , true );

				switch ( Path )
				{
					case "All Users":
						// This is usually "C:\WINDOWS\ALL USERS\START MENU\PROGRAMS\
						Path = GlobalReg.GetValue( "SystemRoot", @"C:\Windows" ).ToString() + 
							@"\All Users\Start Menu\Programs\Disabled Startup Items"; // All Users NONE XP
						GlobalReg.Close();
						break;

					case "Current User":
						// This is usually "C:\WINDOWS\START MENU\PROGRAMS\
						Path = Environment.GetFolderPath ( Environment.SpecialFolder.Programs ) + 
							@"\Disabled Startup Items"; // this is a Current User in a NONE XP SYSTEMS
						break;
				}

				GlobalDir = new DirectoryInfo ( Path );

				if ( GlobalDir.Exists )
				{

					foreach (  FileInfo SingleFile in GlobalDir.GetFiles ( "*.*" ) )
					{
						GlobalString = SingleFile.FullName;

						GlobalEntry = new ListViewItem();

						GlobalEntry.ImageIndex = GlobalImageCounter;
						GlobalImageLarg.Images.Add( Icon.FromHandle ( GlobalGet.AHandle ( GlobalString ) ) );
						GlobalImageSmall.Images.Add ( GlobalImageLarg.Images[GlobalImageCounter] );

						GlobalEntry.Text = SingleFile.Name;
						GlobalEntry.SubItems.Add ( GlobalString );
						GlobalEntry.SubItems.Add( "Startup Folder" );
						GlobalEntry.SubItems.Add( EntryType );
						GlobalEntry.SubItems.Add( StatusMsg );
						GlobalEntry.SubItems.Add( GlobalString ); // Full Key Path

						GlobalView.Items.Add ( GlobalEntry );

						GlobalImageCounter ++;
					}
				}
			}
			catch ( InvalidOperationException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( MyEx.Message +"\n" + MyEx.Source );
			}
			catch ( System.Security.SecurityException MyExp )
			{
				MessageBox.Show 
					( "An error returned while trying to access \n"  + "Error is " + MyExp.Message + "\n" + 
					"Startup Editor requires an administrartor privileges to run properly" ,"Access is denied");
				return;
			}
			catch ( ArgumentNullException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( MyEx.Message +"\n" + MyEx.Source );
			}
			catch ( NullReferenceException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( MyEx.Message +"\n" + MyEx.Source );
			}
			catch ( Exception MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( MyEx.Message +"\n" + MyEx.Source  );
			}
		}


		#endregion

		#region XP System

		internal void GetMSConfigXPRegistry()
		{
			string ThePath = @"SOFTWARE\Microsoft\Shared Tools\MSConfig\startupreg";

			try
			{
				GlobalReg = Registry.LocalMachine.OpenSubKey ( ThePath ,true );

				if ( GlobalReg.SubKeyCount != 0 )
				{
					foreach ( string SingleKey in GlobalReg.GetSubKeyNames() )
					{
						GlobalReg = Registry.LocalMachine.OpenSubKey ( ThePath + @"\" + SingleKey, true );

						//----------------------------------------------------------------------------
						string Entry = GlobalReg.GetValue ( "item", "item" ).ToString();
						string command = GlobalReg.GetValue ( "Command", "Command" ).ToString();
						string key = GlobalReg.GetValue ( "key", "key" ).ToString();
						string hkey = GlobalReg.GetValue ( "hkey", "hkey" ).ToString();
						if ( hkey == "HKLM" ) { hkey = "HKEY_LOCAL_MACHINE"; }
						if ( hkey == "HKCU" ) { hkey = "HKEY_CURRENT_USER"; }
						//----------------------------------------------------------------------------

						GlobalString = GlobalGet.AnalyzeIt ( command );

						GlobalEntry = new ListViewItem();
					
						GlobalEntry.ImageIndex = GlobalImageCounter;
						GlobalImageLarg.Images.Add( Icon.FromHandle ( GlobalGet.AHandle ( GlobalString ) ) );
						GlobalImageSmall.Images.Add ( GlobalImageLarg.Images[GlobalImageCounter] );
					
						GlobalEntry.Text = Entry;
						GlobalEntry.SubItems.Add( command );
						GlobalEntry.SubItems.Add( hkey );
						GlobalEntry.SubItems.Add( "Run" );
						GlobalEntry.SubItems.Add( "Disabled By MSConfig.exe" );
						GlobalEntry.SubItems.Add( ThePath + @"\" + SingleKey );
									
						GlobalView.Items.Add ( GlobalEntry );
					
						GlobalImageCounter ++;
					}
				}
			}
			catch ( System.Security.SecurityException MyExp )
			{
				MessageBox.Show ( "Sorry Man it looks like \n" + MyExp.Message + "\n" + MyExp.GrantedSet );
				Application.ExitThread();
				Application.Exit();
			}
			catch ( ArgumentNullException MyEx )
			{
				MyEx.Message.Trim();
			}
			catch ( NullReferenceException MyEx )
			{
				MyEx.Message.Trim();
			}
			catch ( Exception MyEx )
			{
				MyEx.Message.Trim();
			}

		}


		internal void GetMSConfigXPFolders()
		{
			string ThePath = @"SOFTWARE\Microsoft\Shared Tools\MSConfig\startupfolder";

			try
			{
				GlobalReg = Registry.LocalMachine.OpenSubKey ( ThePath ,true );

				if ( GlobalReg.SubKeyCount != 0 )
				{
					foreach ( string SingleKey in GlobalReg.GetSubKeyNames() )
					{
						GlobalReg = Registry.LocalMachine.OpenSubKey ( ThePath + @"\" + SingleKey, true );

						//----------------------------------------------------------------------------
						string Entry = GlobalReg.GetValue ( "item", "item" ).ToString();
						string command = GlobalReg.GetValue ( "Command", "Command" ).ToString();
						string path = GlobalReg.GetValue ( "path", "path" ).ToString();
						string backup = GlobalReg.GetValue ( "backup", "backup" ).ToString();
					
						string location = GlobalReg.GetValue ( "location", "location" ).ToString();
						if ( location == "Common Startup" ) { location = "All Users"; }
						if ( location == "Startup" ) { location = Environment.UserName; }
						//----------------------------------------------------------------------------

						GlobalString = GlobalGet.AnalyzeIt ( path );

						GlobalEntry = new ListViewItem();
					
						GlobalEntry.ImageIndex = GlobalImageCounter;
						GlobalImageLarg.Images.Add( Icon.FromHandle ( GlobalGet.AHandle ( GlobalString ) ) );
						GlobalImageSmall.Images.Add ( GlobalImageLarg.Images[GlobalImageCounter] );
					
						GlobalEntry.Text = Entry;
						GlobalEntry.SubItems.Add( path );
						GlobalEntry.SubItems.Add( "Startup Folder" );
						GlobalEntry.SubItems.Add( location );
						GlobalEntry.SubItems.Add( "Disabled By MSConfig.exe" );
						GlobalEntry.SubItems.Add( backup );
									
						GlobalView.Items.Add ( GlobalEntry );
					
						GlobalImageCounter ++;
					}
				}
			}

			catch ( System.Security.SecurityException MyExp )
			{
				MessageBox.Show 
					( "An error returned while trying to access MSConfig.exe Entries\n" + "\n" +
					" Error is " + MyExp.Message + "\n" + 
					"Startup Editor requires an administrartor privileges to run properly" ,"Access is denied");
				return;
			}
			catch ( ArgumentNullException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( ThePath + "\n" + MyEx.Message.Trim() );
			}
			catch ( NullReferenceException MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( ThePath + "\n" + MyEx.Message.Trim() );
			}
			catch ( Exception MyEx )
			{
				MyEx.Message.Trim();
				//MessageBox.Show ( ThePath + "\n" + MyEx.Message.Trim() );
			}

		}


		#endregion

		#endregion MsConfig

		#region Faciliate The Code Design

		/// <summary>
		/// Get Group of Entries
		/// </summary>
		/// <param name="Suffix">Put one of (Once, OnceEx, Services, ServicesOnce </param>
		/// <param name="Disabled">True If Disabled</param>
		internal void GetLocalMahine( bool TrueIfDisabled )
		{
			ReadRegistry ( "HKEY_LOCAL_MACHINE", "LM", "Run", TrueIfDisabled );
			ReadRegistry ( "HKEY_LOCAL_MACHINE", "LM", "RunOnce", TrueIfDisabled );
			ReadRegistry ( "HKEY_LOCAL_MACHINE", "LM", "RunOnceEx", TrueIfDisabled );
			ReadRegistry ( "HKEY_LOCAL_MACHINE", "LM", "RunServices", TrueIfDisabled );
			ReadRegistry ( "HKEY_LOCAL_MACHINE", "LM", "RunServicesOnce", TrueIfDisabled );

		}


		internal void GetCurrentUser( bool TrueIfDisabled )
		{
			string hkey = "HKEY_CURRENT_USER";

			if ( TrueIfDisabled ) { hkey = "HKEY_LOCAL_MACHINE"; }

			ReadRegistry ( hkey, Environment.UserName,"Run", TrueIfDisabled );
			ReadRegistry ( hkey, Environment.UserName,"RunOnce", TrueIfDisabled );
		}


		internal void GetStartupFolder( bool GetDisabled )
		{
			string VersionS = " Enabled";

			if ( GetDisabled ) { VersionS = " Disabled"; }

				ReadStartUpFolders ( "User Name" + VersionS );
				ReadStartUpFolders ( "All Users" + VersionS );
		}

        internal void GetMSConfigFolder( string caseIs)
        {
            switch (caseIs)
            {
                case "A None WinXp System":
                    {
                        GetMSConfigNoneXPFolders("Current User", Environment.UserName, "Disabled By MSConfig.exe");
                        GetMSConfigNoneXPFolders("All Users", "All Users", "Disabled By MSConfig.exe");
                        break;
                    }

                case "WinXp With Admin":
                    {
                        GetMSConfigXPFolders();
                        break;
                    }
            }
        }

        internal void GetMSConfigReg(string caseIs)
        {
            string Key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\";

            switch (caseIs)
            {
                case "A None WinXp System":
                    {
                        // msconfig.exe in a none winxp system only monitors two keys in the HKEY_LOCAL_MACHINE
                        // Run and RunServices, while in the HKEY_CURRENT_USER the program monitors only the Run 
                        // Key and it adds the "-" suffix
                        GetMSConfigNoneXPRegistry("HKEY_LOCAL_MACHINE", Key + "Run-", "Run", "Disabled By MSConfig.exe");
                        GetMSConfigNoneXPRegistry("HKEY_LOCAL_MACHINE", Key + "RunServices-", "Run", "Disabled By MSConfig.exe");

                        GetMSConfigNoneXPRegistry("HKEY_CURRENT_USER", Key + "Run-", "Run", "Disabled By MSConfig.exe");
                        break;
                    }

                case "WinXp With Admin":
                    {
                        GetMSConfigXPRegistry();
                        break;
                    }
            }
        }

		#endregion

	}
}
