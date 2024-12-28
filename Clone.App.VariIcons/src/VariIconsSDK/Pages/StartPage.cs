using System;
using ControlsEx.ListControls;
using VariIconsSDK.UI;
using VariIconsSDK.Model;
using System.Collections.Generic;
using VariIconsSDK.Properties;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;

namespace VariIconsSDK.Pages
{
	public partial class StartPage : MainTab
	{
		#region variables
		private const int MAX_RECENT = 24;
		private static StartPage __instance;
		private static Set<Action> __customactions = new Set<Action>();
		#endregion
		#region ctor
		private StartPage()
		{
			InitializeComponent();
			//
			__customactions.Changed += new EventHandler(Actions_Changed);
			Core.Instance.Factories.Changed += new EventHandler(Actions_Changed);
			Actions_Changed(this, EventArgs.Empty);
			//
			UpdateRecents();
		}

		private void UserDispose()
		{
			Core.Instance.Factories.Changed -= new EventHandler(Actions_Changed);
			__customactions.Changed -= new EventHandler(Actions_Changed);
			__instance = null;
		}
		public static StartPage Instance
		{
			get
			{
				if (__instance == null)
					__instance = new StartPage();
				return __instance;
			}
		}
		#endregion
		#region controller
		#region main actions
		//update action list
		void Actions_Changed(object sender, EventArgs e)
		{
			List<Action> actions = new List<Action>();
			//new document types
			foreach (IEditorTabFactory factory in Core.Instance.Factories)
				if (factory.NewPossible)
					actions.Add(new Action(factory.NewImage, factory.NewText,
						factory, new EventHandler(newAction_Click)));
			//custom actions
			actions.AddRange(__customactions);
			//open document types
			foreach (IEditorTabFactory factory in Core.Instance.Factories)
				if (factory.OpenPossible)
					actions.Add(new Action(factory.OpenImage, factory.OpenText,
						factory, new EventHandler(openAction_Click)));
			//
			mainActions.Items.Clear();
			mainActions.Items.AddRange(actions.ToArray());
		}
		//new document
		private void newAction_Click(object sender, EventArgs e)
		{
			if (MainForm == null)
				return;
			Action act = sender as Action;
			if (act != null)
				MainForm.ToolBarStandart.New(
					act.Tag as IEditorTabFactory);
		}
		//open document with specified factory
		private void openAction_Click(object sender, EventArgs e)
		{
			if (MainForm == null)
				return;
			Action act = sender as Action;
			if (act != null)
				MainForm.ToolBarStandart.Open(
					act.Tag as IEditorTabFactory);
		}
		//propagate gradient background
		private void mainActions_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			mainActions.Refresh();
		}
		#endregion
		#region recent list
		//loads all recent files into the list
		void UpdateRecents()
		{
			lstRecent.SuspendUpdates();
			lstRecent.Items.Clear();
			foreach (string filename in Recents)
				lstRecent.Items.Add(new Action(null,
					Path.GetFileName(filename), filename, null));
			lstRecent.ResumeUpdates();
		}
		//open selected item
		private void lstRecent_ItemClicked(object sender, ItemEventArgs e)
		{
			if (e.Item == null)
				return;
			string filename = e.Item.Tag as string;
			if (filename == null || MainForm == null) return;
			MainForm.ToolBarStandart.Open(filename, null);
		}
		//remove from list and settings
		private void mnuRemove_Click(object sender, EventArgs e)
		{
			if (lstRecent.SelectedItem == null)
				return;
			string filename = lstRecent.SelectedItem.Tag as string;
			if (Recents.Contains(filename))
				Recents.Remove(filename);
			lstRecent.Items.Remove(lstRecent.SelectedItem);
			//
			lstRecent.SelectedIndex = -1;
		}
		//pre select for deletion
		private void lstRecent_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				lstRecent.SelectedIndex =
					lstRecent.GetIndexAtPosition(e.Location);
		}
		//restore selection
		private void ctxMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			if (e.CloseReason != ToolStripDropDownCloseReason.ItemClicked)
				lstRecent.SelectedIndex = -1;
		}
		#endregion
		#endregion
		/// <summary>
		/// gets the set of custom actions for the startpage
		/// </summary>
		public static Set<Action> CustomActions
		{
			get { return __customactions; }
		}
		/// <summary>
		/// gets the recents list
		/// </summary>
		private static StringCollection Recents
		{
			get
			{
				StringCollection recents = Settings.Default.RecentFiles;
				if (recents == null)
					Settings.Default.RecentFiles = recents =
						new StringCollection();
				return recents;
			}
		}
		/// <summary>
		/// add a recent file entry
		/// </summary>
		public static void AddRecentFile(string filename)
		{
			if (filename == null)
				throw new ArgumentNullException("filename");
			StringCollection recent = Recents;
			int index = recent.IndexOf(filename);
			//add in front, and clip too old items
			if (index == 0)
				return;
			else if (index > 0)
				recent.RemoveAt(index);
			else if (recent.Count > MAX_RECENT)
				recent.RemoveAt(recent.Count - 1);
			recent.Insert(0, filename);
			//update list
			if (__instance != null)
				__instance.UpdateRecents();
		}
		/// <summary>
		/// removes a recent file entry
		/// </summary>
		public static void RemoveRecentFile(string filename)
		{
			StringCollection recent = Recents;
			int index = Recents.IndexOf(filename);
			if (index != -1)
				recent.RemoveAt(index);
			//update list
			if (__instance != null)
				__instance.UpdateRecents();
		}
	}
}
