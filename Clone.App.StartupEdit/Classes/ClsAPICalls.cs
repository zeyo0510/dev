using System;
using System.Runtime.InteropServices;

namespace StartupEdit
{
	internal class ClsAPICalls
	{
		/// <summary>
		/// Holds All API Calls And Enums
		/// </summary>
		public ClsAPICalls() {}

		#region Enum

		/// <summary>
		/// this enum used on conjuction with getobjectproberties
		/// </summary>
		public enum GetProperties
		{
			SHOP_PRINTERNAME = 0x00000001,  // lpObject points to a printer friendly name
			SHOP_FILEPATH = 0x00000002,  // lpObject points to a fully qualified path+file name
			SHOP_VOLUMEGUID = 0x00000004  // lpObject points to a Volume GUID
		};

		/// <summary>
		/// used for the show window like normal minimized etc
		/// </summary>
		public enum TheShowCmd
		{
			SW_HIDE = 0,
			SW_SHOWNORMAL = 1,
			SW_NORMAL =1,
			SW_SHOWMINIMIZED =2,
			SW_SHOWMAXIMIZED =3,
			SW_MAXIMIZE =3,
			SW_SHOWNOACTIVATE =4,
			SW_SHOW = 5,
			SW_MINIMIZE = 6,
			SW_SHOWMINNOACTIVE = 7,
			SW_SHOWNA =8,
			SW_RESTORE =9,
			SW_SHOWDEFAULT =10,
			SW_FORCEMINIMIZE =11,
			SW_MAX = 11
		};

		#endregion

		#region Methods

		/// <summary>
		/// Method For Extracting The Associacted Icon From Any File
		/// </summary>
		/// <param name="hInst">Handel To The Windows That calling this method</param>
		/// <param name="lpIconPath"> full path to the file</param>
		/// <param name="lpiIcon">the index of the icon</param>
		/// <returns></returns>

		[DllImport("shell32.dll", EntryPoint="ExtractAssociatedIcon")]
		public static extern IntPtr GetExtensionIcon
			(IntPtr hInst, string lpIconPath,ref uint lpiIcon);

		/// <summary>
		/// This function invokes the Properties context menu command on a Shell object.
		/// </summary>
		/// <param name="hwnd">[in] The HWND of the window that will be the parent of the dialog box.</param>
		/// <param name="dwType">enum to what to call</param>
		/// <param name="szObject">[in] A NULL-terminated Unicode string that contains the object name. 
		/// The contents of the string are determined by which of 
		/// the first three flags are set in dwType.</param>
		/// <param name="szPage">[in] A NULL-terminated Unicode string that contains the name of 
		/// the property sheet page to be initially opened. 
		/// Set this parameter to NULL to specify the default page.</param>
		/// <returns>Returns TRUE if the Properties command is successfully invoked, or FALSE otherwise.</returns>

		[DllImport("shell32.dll", EntryPoint="SHObjectProperties", CharSet = CharSet.Auto)]
		public static extern bool CallPropDialog
			( IntPtr hwnd, GetProperties dwType, string szObject, string szPage );

		/// <summary>
		/// Performs an operation on a specified file.
		/// </summary>
		/// <param name="hwnd">[in] Handle to a parent window. This window receives any message boxes that 
		/// an application produces, such as error reporting.</param>
		/// <param name="lpOperation">[in] Pointer to a null-terminated string, referred to in this case as 
		/// a verb, that specifies the action to be performed. The set of available verbs depends on 
		/// the particular file or folder. Generally, the actions available from an object's shortcut menu are
		///  available verbs. For more information about verbs and their availability, 
		///  see Object Verbs. See Extending Shortcut Menus for further discussion of shortcut menus.</param>
		/// <param name="lpFile">[in] Pointer to a null-terminated string that specifies the file or object 
		/// on which to execute the specified verb. To specify a Shell namespace object, 
		/// pass the fully qualified parse name. Note that not all verbs are supported on all objects. 
		/// For example, not all document types support the "print" verb.</param>
		/// <param name="lpParameters">[in] If the lpFile parameter specifies an executable file, 
		/// lpParameters is a pointer to a null-terminated string that specifies the parameters to be 
		/// passed to the application. The format of this string is determined by the verb that is to be 
		/// invoked. If lpFile specifies a document file, lpParameters should be NULL.</param>
		/// <param name="lpDirectory">[in] Pointer to a null-terminated string 
		/// that specifies the default directory.</param>
		/// <param name="nShowCmd">[in] Flags that specify how an application is to be displayed when 
		/// it is opened. If lpFile specifies a document file, the flag is simply passed to 
		/// the associated application. It is up to the application to decide how to handle it.</param>
		/// <returns></returns>
		[DllImport("shell32.dll", EntryPoint="ShellExecute", CharSet = CharSet.Auto)]
		public static extern IntPtr ASimpleExecuteIt
			(IntPtr hwnd, string lpOperation,string lpFile,string lpParameters,string lpDirectory,
			TheShowCmd nShowCmd);

		#endregion
	}
}
