using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ControlsEx.DockingFrames;
using DockingFrames;
using VariIconsSDK.Pages;
using VariIconsSDK.Properties;
using System.Configuration;
using System.Collections.Generic;

namespace VariIconsSDK.UI
{
	/// <summary>
	/// Main Form Singleton of application
	/// </summary>
	public partial class MainForm : Form
	{
		#region variables
		private static MainForm __instance;
		#endregion
		private MainForm()
		{
			InitializeComponent();
			//
			LoadSettings();
			Settings.Default.SettingsSaving +=
				new SettingsSavingEventHandler(SettingsSaving);
		}
		private void UserDispose()
		{
			StartPage.Instance.Dispose();
			__instance = null;
		}
		/// <summary>
		/// singleton-pattern implementation
		/// </summary>
		public static MainForm Instance
		{
			get
			{
				if (__instance == null)
					__instance = new MainForm();
				return __instance;
			}
		}
		//add startpage
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			_tabs.Pages.Add(StartPage.Instance);
		}
		//collect all captions of unsavededitors
		private List<string> CollectUnsavedEditors()
		{
			List<string> unsaved = new List<string>();
			foreach (ControlsEx.DockingFrames.TabPage page in _tabs.Pages)
			{
				EditorTab editor = page as EditorTab;
				if (editor != null && editor.Dirty)
					unsaved.Add(editor.Caption);
			}
			return unsaved;
		}
		//save editors and settings
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			List<string> unsaved;
			while ((unsaved = CollectUnsavedEditors()).Count > 0)
			{
				//there are unsaved changes, ask user
				using (SaveChangesForm frm = new SaveChangesForm())
				{
					frm.Items.AddRange(unsaved.ToArray());
					DialogResult res = frm.ShowDialog();
					if (res == DialogResult.Yes)
						toolBarStandart1.SaveAll();
					else if (res == DialogResult.No)
						break;
					else
					{
						e.Cancel = true;
						return;
					}
				}
			}
			//
			Settings.Default.Save();
			base.OnFormClosing(e);
		}
		//single page closing
		private void _tabs_PageClosing(object sender, PageCloseEventArgs e)
		{
			EditorTab editor = _tabs.SelectedPage as EditorTab;
			while (editor != null && editor.Dirty)
			{
				//there are unsaved changes, ask user
				using (SaveChangesForm frm = new SaveChangesForm())
				{
					frm.Items.Add(editor.Caption);
					DialogResult res = frm.ShowDialog();
					if (res == DialogResult.Yes)
						editor.Save();
					else if (res == DialogResult.No)
						break;
					else
					{
						e.Cancel = true;
						return;
					}
				}
			}
		}
		//load gui settings
		void LoadSettings()
		{
			//load dockinfos
			using (StringReader rdr = new StringReader(Settings.Default.Docking))
			{
				_dockMan.LoadDockInfos(XmlReader.Create(rdr));
			}
			//restore window
			if (Settings.Default.MainForm_WindowState == FormWindowState.Normal)
			{
				this.Location = Settings.Default.MainForm_Location;
				this.Size = Settings.Default.MainForm_Size;
			}
			else
				this.WindowState = Settings.Default.MainForm_WindowState;
		}
		//save gui settings
		void SettingsSaving(object sender, CancelEventArgs e)
		{
			//change settings for window position
			if (this.WindowState != FormWindowState.Minimized)
				Settings.Default.MainForm_WindowState = this.WindowState;
			if (this.WindowState == FormWindowState.Normal)
			{
				Settings.Default.MainForm_Location = this.Location;
				Settings.Default.MainForm_Size = this.Size;
			}
			//save dock infos
			StringBuilder docking = new StringBuilder();
			XmlWriter wrt = XmlWriter.Create(docking);
			_dockMan.SaveDockInfos(wrt);
			wrt.Close();
			Settings.Default.Docking = docking.ToString();
			//
			PluginFinder.Instance.SavePluginSettings();
		}
		//suppress updates on editor switching
		void _tabs_BeforeSelectedPageChanged(object sender, System.EventArgs e)
		{
			_dockMan.SuspendUpdates();
		}

		void _tabs_SelectedPageChanged(ControlsEx.DockingFrames.TabControl sender, ControlsEx.DockingFrames.TabPageEventArgs e)
		{
			_dockMan.ResumeUpdates();
		}
		#region properties
		/// <summary>
		/// gets the collection of editors
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TabPageCollection Pages
		{
			get { return _tabs.Pages; }
		}
		public ControlsEx.DockingFrames.TabPage SelectedPage
		{
			get { return _tabs.SelectedPage; }
			set { _tabs.SelectedPage = value; }
		}
		public DockManager DockManager
		{
			get { return _dockMan; }
		}
		public string StatusText
		{
			get { return statuslbl.Text; }
			set { statuslbl.Text = value; }
		}
		public void ResetStatusText()
		{
			System.ComponentModel.ComponentResourceManager resources =
				new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			resources.ApplyResources(this.statuslbl, "statuslbl");
		}
		public StatusStrip StatusStrip
		{
			get { return statusStrip; }
		}
		public ToolBarStandart ToolBarStandart
		{
			get { return toolBarStandart1; }
		}
		//dockingareas for toolbars
		public DockingArea LeftDockingArea
		{
			get { return _dockLeft; }
		}
		public DockingArea TopDockingArea
		{
			get { return _dockTop; }
		}
		public DockingArea RightDockingArea
		{
			get { return _dockRight; }
		}
		public DockingArea BottomDockingArea
		{
			get { return _dockBottom; }
		}
		#endregion

	}
}
