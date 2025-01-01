using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;


namespace StartupEdit
{
	/// <summary>
	/// This Class Is Used By The Application to add, edit, delete, enable and disable entries
	/// </summary>
	internal class ClsEntriesManipulation
	{
		#region Some Variables and the constructor

		/// <summary>
		/// Instance of the class that holds the analyze method
		/// </summary>
		ClsAddOns Hallel = new ClsAddOns();
		/// <summary>
		/// this static string holds the old entry name when frmeditEntry Loads
		/// </summary>
		public static string OldEntryName;
		/// <summary>
		/// this static string holds the old entry data when frmeditEntry Loads
		/// </summary>
		public static string OldEntryData;
		/// <summary>
		/// this static string holds the old entry Root when frmeditEntry Loads
		/// </summary>
		public static int OldEntryHkey;
		/// <summary>
		/// this static string holds the old entry Type when frmeditEntry Loads
		/// </summary>
		public static int OldWhichRun;

		/// <summary>
		/// one Global RegistryKey Used Throughout the entire class
		/// </summary>
		private RegistryKey GlobalReg;
		/// <summary>
		///  One Global Dir Used to Operate with Entries found in the startup folders
		/// </summary>
		private DirectoryInfo GlobalDir;
		/// <summary>
		/// One Golbal File Name To Use Throughout the entire class
		/// </summary>
		private FileInfo GlobalFile;

		/// <summary>
		/// Initialize This Class To Deal With Entries Like 
		/// Edit, Remove and Add Entry
		/// </summary>
		public ClsEntriesManipulation()
		{}

		#endregion

		#region Add Entry

		/// <summary>
		/// To Add new Entry
		/// </summary>
		/// <param name="Suffix"> This is very important this tells the method which key to use
		/// run or runonce or runoncex</param>
		/// <param name="LocalCurrent"> 0 -> means local machine || 1 -> menas current user</param>
		/// <param name="EntryName">Value to add</param>
		/// <param name="DataName">data to hold in value</param>
		public void AddEntry( string Suffix, int LocalCurrent, string EntryName, string DataName )
		{
			string Short = @"SOFTWARE\Microsoft\Windows\CurrentVersion";
			string KeyName = Short + @"\" + Suffix;

			switch ( LocalCurrent )
			{
				case 0: // Local Machine
				{
					try
					{
						GlobalReg = Registry.LocalMachine.OpenSubKey ( KeyName, true );
						GlobalReg.SetValue ( EntryName, DataName );
						GlobalReg.Close();
					}
					catch ( NullReferenceException MyEx )
					{
						GlobalReg = Registry.LocalMachine.OpenSubKey( Short ,true );
						GlobalReg.CreateSubKey ( Suffix );
						GlobalReg.Close();

						MyEx.Message.Trim();
				
						AddEntry( Suffix, LocalCurrent, EntryName, DataName );
					}
					break;
				}

				case 1: // Current User
				{
					try
					{
						GlobalReg = Registry.CurrentUser.OpenSubKey ( KeyName, true );
						GlobalReg.SetValue ( EntryName, DataName );
						GlobalReg.Close();
					}
					catch ( NullReferenceException MyEx )
					{
						GlobalReg = Registry.CurrentUser.OpenSubKey ( Short ,true );
						GlobalReg.CreateSubKey ( Suffix );
						GlobalReg.Close();

						MyEx.Message.Trim();

						AddEntry( Suffix, LocalCurrent, EntryName, DataName );
					}
					break;
				}
			}
		}


		#endregion 

		#region To Check If Entry Is Edited

		/// <summary>
		/// To Check If Entry Is Edited This Method Works in conjuction with
		/// </summary>
		/// <param name="CurrentEntryName">The Last entry name that was found </param>
		/// <param name="CurrentEntryData">the last entry data was found</param>
		/// <param name="CurrentEntryHkey">so alike</param>
		/// <param name="CurrentWhichRun">so alike </param>
		public  void CheckIfEdit 
			( string CurrentEntryName, string CurrentEntryData, int CurrentEntryHkey, int CurrentWhichRun )
		{
			/// let's compare the current with the one stored in the static variables 
			if ( OldEntryName != CurrentEntryName || OldEntryData != CurrentEntryData || 
				OldEntryHkey != CurrentEntryHkey || OldWhichRun != CurrentWhichRun )
			{
				///well let's edit
				EditEntry (	CurrentEntryName,	CurrentEntryData, CurrentEntryHkey, CurrentWhichRun,	
								OldEntryName ,		OldEntryHkey, OldWhichRun );
			}
		}

		#endregion

		#region Edit Entry()

		/// <summary>
		/// to edit an entry  we can change the data or move it from hkey branch to other hkey branch
		/// </summary>
		/// <param name="NewEntryName"> new name </param>
		/// <param name="NewEntryData">new data</param>
		/// <param name="NewEntryHkey"> 0 -> Means LocalMachine || 1 -> Means CurrentUser</param>
		/// <param name="NewWhichRun"> 0 -> Means Run || 1 -> Means RunOnce</param>
		/// <param name="OldEntryName">Old Entry Name</param>
		/// <param name="OldEntryHkey"></param>
		public void EditEntry
			(	string	NewEntryName,	string	NewEntryData,	int	NewEntryHkey,	int NewWhichRun,
			string	OldEntryName,									int	OldEntryHkey,  int OldWhichRun )
		{
			string Short = @"SOFTWARE\Microsoft\Windows\CurrentVersion";

			string NewSuffix = TellMeTheSuffix ( NewWhichRun );
			string NewGlobal = Short + @"\" + NewSuffix;

			string OldSuffix = TellMeTheSuffix ( OldWhichRun );
			string OldGlobal = Short + @"\"  + OldSuffix;

			try
			{
				RegistryKey NewEntryLM = Registry.LocalMachine.OpenSubKey ( NewGlobal, true);
				RegistryKey NewEntryCU = Registry.CurrentUser.OpenSubKey ( NewGlobal, true);

				//--------------------------------------------------------------------------------------

				RegistryKey OldEntryLM = Registry.LocalMachine.OpenSubKey ( OldGlobal, true);
				RegistryKey OldEntryCU = Registry.CurrentUser.OpenSubKey ( OldGlobal, true);

				//=================================================================================
				// 0 Local, 1 Current 
			
				string Complex = OldEntryHkey.ToString() + NewEntryHkey.ToString();

				switch ( Complex )
				{ 
					case "01": // from : hkey_local_machine to hkey_current_user
						OldEntryLM.DeleteValue ( OldEntryName, false );
						NewEntryCU.SetValue ( NewEntryName, NewEntryData );
						break;

					case "10": // from : hkey_current_user to hkey_local_machine
						OldEntryCU.DeleteValue ( OldEntryName, false );
						NewEntryLM.SetValue ( NewEntryName, NewEntryData );
						break;

					case "00": // from :  hkey_local_machine to hkey_local_machine
						NewEntryLM.SetValue ( NewEntryName, NewEntryData );
						OldEntryLM.DeleteValue ( OldEntryName,  false );
						break;

					case "11": // from : hkey_current_user to hkey_current_user
						NewEntryCU.SetValue ( NewEntryName, NewEntryData );
						OldEntryCU.DeleteValue ( OldEntryName, false );
						break;
				}

				NewEntryLM.Close();
				NewEntryCU.Close();

				OldEntryLM.Close();
				OldEntryCU.Close();
			}
			catch ( Exception My )
			{
				RegistryKey NotFoundLM = Registry.LocalMachine.OpenSubKey ( Short, true);
				NotFoundLM.CreateSubKey( NewSuffix );
				NotFoundLM.Close();

				RegistryKey NotFoundCU = Registry.CurrentUser.OpenSubKey ( Short, true);
				NotFoundCU.CreateSubKey( NewSuffix );
				NotFoundCU.Close();

				//MessageBox.Show ( My.Message );
				My.Message.Trim();

				EditEntry
					( NewEntryName,	NewEntryData,NewEntryHkey, NewWhichRun,OldEntryName,OldEntryHkey,  OldWhichRun );
			}
		}


		#endregion

		#region Remove The Entry

		/// <summary>
		/// To Remove any entry no matter where its location just give me the following;
		/// </summary>
		/// <param name="EntryName">Name of the entry</param>
		/// <param name="EntryData">Data of the entry</param>
		/// <param name="EnryRoot">send me the the hkey the root</param>
		/// <param name="EntryStatus">Status of the entry</param>
		/// <param name="EntryKey">the fullkey path</param>
		/// <returns>true if ii removes otherwise false</returns>
		public bool RemoveTheEntry 
			( string EntryName,	string EntryData, string EntryHkey, string EntryStatus, string EntryKey  )
		{
			string TheMessage = "Are You Sure You Want To Permanently Remove " + "\n" + "\n"+
				"Name: " + EntryName + "\n" + 
				"Data: " + EntryData + "\n" +
				"Entry Root: " + EntryHkey + "\n" + 
				"Status: " + EntryStatus + "\n" + "\n" + 
				"Note: This Step Can Not Be Undone ! " + "\n" + "\n" +"Continue Deleting ! ?" + "\n" ;

			if ( MessageBox.Show ( TheMessage ,"Confirm Delete", MessageBoxButtons.OKCancel, 
				MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2 ) == DialogResult.OK )

			{
				switch ( EntryHkey )
				{
					case "Startup Folder" :

						GlobalFile = new FileInfo ( EntryData );

                        if (!GlobalFile.Exists) { return false; }

						GlobalFile.Attributes = FileAttributes.Normal; // in case if it is readonly
						GlobalFile.Delete();

						return true;		
				
					case "HKEY_LOCAL_MACHINE" :
						GlobalReg = Registry.LocalMachine.OpenSubKey ( EntryKey, true );

						GlobalReg.DeleteValue( EntryName, false );
						GlobalReg.Close();
						return true;

					case "HKEY_CURRENT_USER" :
						GlobalReg = Registry.CurrentUser.OpenSubKey ( EntryKey, true );

						GlobalReg.DeleteValue( EntryName, false );
						GlobalReg.Close();
						return true;
				}

				return false;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region EnableEntry

		public bool EnableEntry
			( string EntryName, string EntryData, string EntryHkey, string WhichRun, string EntryKey )
		{
		string PathTo = "BackUps\\" + Environment.UserName ;

			string PureEntryData = Hallel.AnalyzeIt ( EntryData );

			string OnePath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\";

			switch ( EntryHkey )
			{
				case "HKEY_LOCAL_MACHINE":
				{
					GlobalReg = Registry.LocalMachine.OpenSubKey ( OnePath + WhichRun, true );
					GlobalReg.SetValue ( EntryName, EntryData );
					GlobalReg.Close();

					// in msconfig xp edtion we need to delete
					// the whole key which is "EntryName"
					GlobalReg = Registry.LocalMachine.OpenSubKey ( EntryKey, true );
					GlobalReg.DeleteValue ( EntryName, false );
					GlobalReg.Close();

					return true;
				}
				case "HKEY_CURRENT_USER" :
				{
					GlobalReg = Registry.CurrentUser.OpenSubKey ( OnePath + WhichRun , true );
					GlobalReg.SetValue ( EntryName, EntryData );
					GlobalReg.Close();

					GlobalReg = Registry.CurrentUser.OpenSubKey ( EntryKey, true );
					GlobalReg.DeleteValue ( EntryName, false );
					GlobalReg.Close();

					return true;
				}
				case "Startup Folder" :
				{
					if ( WhichRun == Environment.UserName )
					{
						GlobalFile = new FileInfo( PureEntryData );
			
						GlobalFile.Attributes = FileAttributes.Normal;
						GlobalFile.MoveTo 
							( Environment.GetFolderPath ( Environment.SpecialFolder.Startup ) + "\\" + EntryName );

						return true;
					}
					else if ( WhichRun == "All Users" )
					{
						GlobalReg = Registry.LocalMachine.OpenSubKey 
							( @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders" , true );

						GlobalFile = new FileInfo( PureEntryData );

						GlobalFile.Attributes = FileAttributes.Normal;
						GlobalFile.MoveTo 
							( GlobalReg.GetValue( "Common Startup", @"C:\" ).ToString() + @"\" + EntryName );
						GlobalReg.Close();

						return true;
					}
					else //not All Users Or Current User
					{
						return false;
					}
				}

				default:// not HKEY_LOCAL_MACHINE or HKEY_CURRENT_USER or  "Startup Folder"
				{
					return false;
				}
			}
		}


		#endregion

		#region DisableEntry

		/// <summary>
		/// to disable an Entry so we move it to the disabled key or folder
		/// </summary>
		/// <param name="EntryName">Entry To Disable "Move"</param>
		/// <param name="EntryData">"Used In Confirmation</param>
		/// <param name="EntryHkey">Local Machine Or Current User</param>
		/// <param name="WhichRun">Run Or Run Once</param>
		/// <param name="PureEntryData">To Get its Real Path</param>
		/// <returns>Returns True If Everything went ok otherwise false</returns>
		public bool DisableEntry
			( string EntryName, string EntryData, string EntryHkey, string WhichRun, string EntryKey )
		{
			try
			{
				string PathTo = @"BackUps\" + Environment.UserName ;
				string PathToAll = @"BackUps\All Users";
				string PureEntryData = Hallel.AnalyzeIt ( EntryData );

				#region Permit The User And Take Action

				string TheMessage = "Are You Sure You Want To Disable " + "\n" + "\n"+
					"Name: " + EntryName + "\n" + "Data: " + EntryData + "\n" +
					"Entry Root: " + EntryHkey + "\n" + "\n" + 
					"Note: To Enable This Entry Again Right Click It And Hit Enable ! " + "\n" + "\n" 
					+"Disable Entry Now ! ?" + "\n" ;

				#region  Choose To Disable

				if ( 
					MessageBox.Show
					( 
					TheMessage ,"Confirm Disable", MessageBoxButtons.OKCancel,	MessageBoxIcon.Warning, 
					MessageBoxDefaultButton.Button2 
					) 
					== DialogResult.OK 
					)
				{
					switch ( EntryHkey )
					{
						case "HKEY_LOCAL_MACHINE" :
						{
							GlobalReg = Registry.LocalMachine.OpenSubKey 
								( @"SOFTWARE\AlQademoUn\StartEdit\LM\" + WhichRun, true );

							GlobalReg.SetValue ( EntryName, EntryData );
							GlobalReg.Close();

							GlobalReg = Registry.LocalMachine.OpenSubKey
								( @"SOFTWARE\Microsoft\Windows\CurrentVersion\" + WhichRun, true );

							GlobalReg.DeleteValue ( EntryName, false );
							GlobalReg.Close();

							return true;
						}
						case "HKEY_CURRENT_USER" :
						{
							GlobalReg = Registry.LocalMachine.OpenSubKey 
								(	@"SOFTWARE\AlQademoUn\StartEdit\" + Environment.UserName + @"\" + WhichRun, true );

							GlobalReg.SetValue ( EntryName, EntryData );
							GlobalReg.Close();

							GlobalReg = Registry.CurrentUser.OpenSubKey
								( @"SOFTWARE\Microsoft\Windows\CurrentVersion\" + WhichRun, true );

							GlobalReg.DeleteValue ( EntryName, false );
							GlobalReg.Close();

							return true;
						}
						case "Startup Folder" :
						{
							if ( WhichRun == Environment.UserName )
							{
								GlobalDir = new DirectoryInfo( Application.StartupPath );

								GlobalDir.CreateSubdirectory ( PathTo );

								GlobalFile	 = new FileInfo( PureEntryData );

								GlobalFile.Attributes = FileAttributes.Normal;
								GlobalFile.MoveTo ( Application.StartupPath + @"\" + PathTo + @"\" + EntryName );

								return true;
							}
							else if ( WhichRun == "All Users" )
							{
								GlobalDir = new DirectoryInfo( Application.StartupPath );

								GlobalDir.CreateSubdirectory ( PathToAll );

								GlobalFile = new FileInfo( PureEntryData );

								GlobalFile.Attributes = FileAttributes.Normal;
								GlobalFile.MoveTo ( Application.StartupPath + @"\" + PathToAll + @"\" + EntryName );

								return true;
							}
							else // It Is Not Current User & All Users
							{
								return false;
							}
						}
						default: // It Is Not HKEY_LOCAL_MACHINE  HKEY_CURRENT_USER "Startup Folder"
						{
							return false;
						}
					}
				}
					#endregion Choose To Disable

					#region Choose Not To Disable

				else
				{
					return false;
				}

				#endregion Choose Not To Disable

				#endregion Permit The User And Take Action
			}
			catch ( NullReferenceException MyEx )
			{
				MessageBox.Show ( MyEx.Message +"\n" + MyEx.Source );
				return false;
			}
		}


		#endregion

		public string TellMeTheSuffix ( int TheNumber )
		{
			string Suffix = "Run";

			switch ( TheNumber )
			{
				case 0: //Run
					Suffix = "Run";
					break;

				case 1: // RunOnce
					Suffix = "RunOnce";
					break;

				case 2: // RunOnceEx
					Suffix = "RunOnceEx";
					break;

				case 3: // RunServices
					Suffix = "RunServices";
					break;

				case 4: // RunServicesOnce
					Suffix = "RunServicesOnce";
					break;
			}

			return Suffix;
		}

	}
}
