using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CheVolume.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings A = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return A;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		public StringCollection FriendlyNames
		{
			get
			{
				return (StringCollection)this["FriendlyNames"];
			}
			set
			{
				this["FriendlyNames"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string ListOfProcessesLocked
		{
			get
			{
				return (string)this["ListOfProcessesLocked"];
			}
			set
			{
				this["ListOfProcessesLocked"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool IsAdvancedUser
		{
			get
			{
				return (bool)this["IsAdvancedUser"];
			}
			set
			{
				this["IsAdvancedUser"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool IsNotifyAdvancedUser
		{
			get
			{
				return (bool)this["IsNotifyAdvancedUser"];
			}
			set
			{
				this["IsNotifyAdvancedUser"] = value;
			}
		}
	}
}
