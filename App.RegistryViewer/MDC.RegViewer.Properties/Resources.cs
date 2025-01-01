using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MDC.RegViewer.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("MDC.RegViewer.Properties.Resources", typeof(Resources).Assembly);
					resourceMan = resourceManager;
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static string Confirm_DeleteEntries
		{
			get
			{
				return ResourceManager.GetString("Confirm_DeleteEntries", resourceCulture);
			}
		}

		internal static string Confirm_DeleteKey
		{
			get
			{
				return ResourceManager.GetString("Confirm_DeleteKey", resourceCulture);
			}
		}

		internal static string Confirm_DeleteValue
		{
			get
			{
				return ResourceManager.GetString("Confirm_DeleteValue", resourceCulture);
			}
		}

		internal static string Error_AlreadyFavorite
		{
			get
			{
				return ResourceManager.GetString("Error_AlreadyFavorite", resourceCulture);
			}
		}

		internal static string Error_CreateKeyFail
		{
			get
			{
				return ResourceManager.GetString("Error_CreateKeyFail", resourceCulture);
			}
		}

		internal static string Error_CreateValueFail
		{
			get
			{
				return ResourceManager.GetString("Error_CreateValueFail", resourceCulture);
			}
		}

		internal static string Error_DeleteEntriesFail
		{
			get
			{
				return ResourceManager.GetString("Error_DeleteEntriesFail", resourceCulture);
			}
		}

		internal static string Error_DeleteKeyFail
		{
			get
			{
				return ResourceManager.GetString("Error_DeleteKeyFail", resourceCulture);
			}
		}

		internal static string Error_DeleteValueFail
		{
			get
			{
				return ResourceManager.GetString("Error_DeleteValueFail", resourceCulture);
			}
		}

		internal static string Error_ExportFail
		{
			get
			{
				return ResourceManager.GetString("Error_ExportFail", resourceCulture);
			}
		}

		internal static string Error_FileOpenFail
		{
			get
			{
				return ResourceManager.GetString("Error_FileOpenFail", resourceCulture);
			}
		}

		internal static string Error_InvalidKey
		{
			get
			{
				return ResourceManager.GetString("Error_InvalidKey", resourceCulture);
			}
		}

		internal static string Info_ExportSuccess
		{
			get
			{
				return ResourceManager.GetString("Info_ExportSuccess", resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}
