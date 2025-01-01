using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MDC.RegViewer.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
	[CompilerGenerated]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return defaultInstance;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool Maximized
		{
			get
			{
				return (bool)this["Maximized"];
			}
			set
			{
				this["Maximized"] = value;
			}
		}

		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public bool LookAtValues
		{
			get
			{
				return (bool)this["LookAtValues"];
			}
			set
			{
				this["LookAtValues"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("False")]
		public bool MatchCase
		{
			get
			{
				return (bool)this["MatchCase"];
			}
			set
			{
				this["MatchCase"] = value;
			}
		}

		[DefaultSettingValue("False")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool UseRegEx
		{
			get
			{
				return (bool)this["UseRegEx"];
			}
			set
			{
				this["UseRegEx"] = value;
			}
		}

		[DefaultSettingValue("120")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public int ValWidth1
		{
			get
			{
				return (int)this["ValWidth1"];
			}
			set
			{
				this["ValWidth1"] = value;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("120")]
		public int ValWidth2
		{
			get
			{
				return (int)this["ValWidth2"];
			}
			set
			{
				this["ValWidth2"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("114")]
		[UserScopedSetting]
		public int ValWidth3
		{
			get
			{
				return (int)this["ValWidth3"];
			}
			set
			{
				this["ValWidth3"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("270")]
		[DebuggerNonUserCode]
		public int ResWidth1
		{
			get
			{
				return (int)this["ResWidth1"];
			}
			set
			{
				this["ResWidth1"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("240")]
		[UserScopedSetting]
		public int ResWidth2
		{
			get
			{
				return (int)this["ResWidth2"];
			}
			set
			{
				this["ResWidth2"] = value;
			}
		}

		[DefaultSettingValue("46")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public int ResWidth3
		{
			get
			{
				return (int)this["ResWidth3"];
			}
			set
			{
				this["ResWidth3"] = value;
			}
		}

		[DefaultSettingValue("617, 433")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public Size Size
		{
			get
			{
				return (Size)this["Size"];
			}
			set
			{
				this["Size"] = value;
			}
		}

		[DefaultSettingValue("-1, -1")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public Point Location
		{
			get
			{
				return (Point)this["Location"];
			}
			set
			{
				this["Location"] = value;
			}
		}

		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		[UserScopedSetting]
		public string LastKey
		{
			get
			{
				return (string)this["LastKey"];
			}
			set
			{
				this["LastKey"] = value;
			}
		}

		[UserScopedSetting]
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		public bool LookAtKeys
		{
			get
			{
				return (bool)this["LookAtKeys"];
			}
			set
			{
				this["LookAtKeys"] = value;
			}
		}

		[DefaultSettingValue("True")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool LookAtData
		{
			get
			{
				return (bool)this["LookAtData"];
			}
			set
			{
				this["LookAtData"] = value;
			}
		}

		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("All Hives")]
		public string SearchHive
		{
			get
			{
				return (string)this["SearchHive"];
			}
			set
			{
				this["SearchHive"] = value;
			}
		}

		private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
		{
		}

		private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
		{
		}
	}
}
