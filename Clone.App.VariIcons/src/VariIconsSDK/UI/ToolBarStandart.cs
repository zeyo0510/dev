using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DockingFrames;
using VariIconsSDK.Model;
using VariIconsSDK.Pages;
using VariIconsSDK.Properties;
using System.Collections.Generic;

namespace VariIconsSDK.UI
{
	public class ToolBarStandart : DockingFrames.ToolBar
	{
		#region generated
		private DockingFrames.ToolBarButton tbOpen;
		private DockingFrames.ToolBarButton tbSave;
		private DockingFrames.ToolBarSeparator toolBarSeparator1;
		private DockingFrames.ToolBarButton tbCut;
		private DockingFrames.ToolBarButton tbCopy;
		private DockingFrames.ToolBarButton tbPaste;
		private DockingFrames.ToolBarSeparator toolBarSeparator2;
		private DockingFrames.ToolBarButton tbUndo;
		private DockingFrames.ToolBarButton tbRedo;
		private DockingFrames.ToolBarButton tbDelete;
		private DockingFrames.ToolBarButton tbSaveAll;
		private ToolBarSeparator toolBarSeparator3;
		private DockingFrames.ToolBarButton tbOptions;
		private DockingFrames.ToolBarButton tbHelp;
		private DockingFrames.ToolBarButton tbUpdate;
		private DockingFrames.ToolBarButton tbStartPage;
		private DockingFrames.ToolBarButton tbNew;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolBarStandart));
			this.tbNew = new DockingFrames.ToolBarButton();
			this.tbOpen = new DockingFrames.ToolBarButton();
			this.tbSave = new DockingFrames.ToolBarButton();
			this.toolBarSeparator1 = new DockingFrames.ToolBarSeparator();
			this.tbCut = new DockingFrames.ToolBarButton();
			this.tbCopy = new DockingFrames.ToolBarButton();
			this.tbPaste = new DockingFrames.ToolBarButton();
			this.toolBarSeparator2 = new DockingFrames.ToolBarSeparator();
			this.tbUndo = new DockingFrames.ToolBarButton();
			this.tbRedo = new DockingFrames.ToolBarButton();
			this.tbDelete = new DockingFrames.ToolBarButton();
			this.tbSaveAll = new DockingFrames.ToolBarButton();
			this.toolBarSeparator3 = new DockingFrames.ToolBarSeparator();
			this.tbOptions = new DockingFrames.ToolBarButton();
			this.tbHelp = new DockingFrames.ToolBarButton();
			this.tbUpdate = new DockingFrames.ToolBarButton();
			this.tbStartPage = new DockingFrames.ToolBarButton();
			this.SuspendLayout();
			// 
			// tbNew
			// 
			// 
			// 
			// 
			this.tbNew.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbNew.Command.Image")));
			this.tbNew.Command.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.tbNew.Command.Text = null;
			this.tbNew.Command.Click += new System.EventHandler(this.New_Click);
			// 
			// tbOpen
			// 
			// 
			// 
			// 
			this.tbOpen.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbOpen.Command.Image")));
			this.tbOpen.Command.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.tbOpen.Command.Text = "Open";
			this.tbOpen.Command.Click += new System.EventHandler(this.Open_Click);
			// 
			// tbSave
			// 
			// 
			// 
			// 
			this.tbSave.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbSave.Command.Image")));
			this.tbSave.Command.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.tbSave.Command.Text = "Save";
			this.tbSave.Command.Click += new System.EventHandler(this.Save_Click);
			// 
			// tbCut
			// 
			// 
			// 
			// 
			this.tbCut.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbCut.Command.Image")));
			this.tbCut.Command.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.tbCut.Command.Text = "Cut";
			// 
			// tbCopy
			// 
			// 
			// 
			// 
			this.tbCopy.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbCopy.Command.Image")));
			this.tbCopy.Command.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tbCopy.Command.Text = "Copy";
			// 
			// tbPaste
			// 
			// 
			// 
			// 
			this.tbPaste.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbPaste.Command.Image")));
			this.tbPaste.Command.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.tbPaste.Command.Text = "Paste";
			// 
			// tbUndo
			// 
			// 
			// 
			// 
			this.tbUndo.Command.Enabled = false;
			this.tbUndo.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbUndo.Command.Image")));
			this.tbUndo.Command.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.tbUndo.Command.Text = "Undo";
			// 
			// tbRedo
			// 
			// 
			// 
			// 
			this.tbRedo.Command.Enabled = false;
			this.tbRedo.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbRedo.Command.Image")));
			this.tbRedo.Command.Shortcut = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.tbRedo.Command.Text = "Redo";
			// 
			// tbDelete
			// 
			// 
			// 
			// 
			this.tbDelete.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbDelete.Command.Image")));
			this.tbDelete.Command.Text = "Delete";
			// 
			// tbSaveAll
			// 
			// 
			// 
			// 
			this.tbSaveAll.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbSaveAll.Command.Image")));
			this.tbSaveAll.Command.Text = "Save All";
			this.tbSaveAll.Command.Click += new System.EventHandler(this.SaveAll_Click);
			// 
			// tbOptions
			// 
			// 
			// 
			// 
			this.tbOptions.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbOptions.Command.Image")));
			this.tbOptions.Command.Text = "Options...";
			// 
			// tbHelp
			// 
			// 
			// 
			// 
			this.tbHelp.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbHelp.Command.Image")));
			this.tbHelp.Command.Text = "Help";
			// 
			// tbUpdate
			// 
			// 
			// 
			// 
			this.tbUpdate.Command.Enabled = false;
			this.tbUpdate.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbUpdate.Command.Image")));
			this.tbUpdate.Command.Text = "Update";
			// 
			// tbStartPage
			// 
			// 
			// 
			// 
			this.tbStartPage.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbStartPage.Command.Image")));
			this.tbStartPage.Command.Text = null;
			this.tbStartPage.Command.Click += new System.EventHandler(this.tbStartPage_Command_Click);
			// 
			// ToolBarStandart
			// 
			this.Guid = "FE63B6FF-1260-4F10-963B-5118FE5A2177";
			this.ToolBarControls.AddRange(new DockingFrames.ToolBarControl[] {
            this.tbNew,
            this.tbOpen,
            this.tbSave,
            this.tbSaveAll,
            this.toolBarSeparator1,
            this.tbCut,
            this.tbCopy,
            this.tbPaste,
            this.tbDelete,
            this.toolBarSeparator2,
            this.tbUndo,
            this.tbRedo,
            this.toolBarSeparator3,
            this.tbOptions,
            this.tbHelp,
            this.tbUpdate,
            this.tbStartPage});
			this.ResumeLayout(false);

		}
		#endregion
		//ctor
		public ToolBarStandart()
		{
			this.InitializeComponent();
		}

		//clean up handlers
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		private void New_Click(object sender, EventArgs e)
		{
			New();
		}

		private void Open_Click(object sender, EventArgs e)
		{
			Open();
		}

		private void Save_Click(object sender, EventArgs e)
		{
			Save();
		}
		private void SaveAll_Click(object sender, EventArgs e)
		{
			SaveAll();
		}

		private void tbStartPage_Command_Click(object sender, EventArgs e)
		{
			if (MainForm.Instance.Pages.Contains(StartPage.Instance))
			{
				MainForm.Instance.SelectedPage = StartPage.Instance;
				return;
			}
			MainForm.Instance.Pages.Add(StartPage.Instance);
			MainForm.Instance.SelectedPage = StartPage.Instance;
		}
		#region public members
		//build file filters
		private string CreateFilter(IEditorTabFactory factory)
		{
			if (factory != null)
				return FileFilter.BuildCombinedFilter(factory.Extensions);
			//collect all
			List<FileFilter> allfilters = new List<FileFilter>();
			foreach (IEditorTabFactory fac in Core.Instance.Factories)
				allfilters.AddRange(fac.Extensions);
			return FileFilter.BuildCombinedFilter(allfilters);
		}

		public void New()
		{
			New(null);
		}
		public void New(IEditorTabFactory current)
		{
			if (current == null)
				foreach (IEditorTabFactory factory in Core.Instance.Factories)
					if (factory.NewPossible)
					{
						current = factory;
						break;
					}
			if (current == null)
				return;
			try
			{
				EditorTab editor = current.New();
				if (editor == null)
					return;
				//get suggested name
				Type type = editor.GetType();
				int num = 1;
				foreach (ControlsEx.DockingFrames.TabPage d in MainForm.Instance.Pages)
					if (d.GetType() == type) num++;
				editor.Caption = current.DocumentName + " " + num.ToString();
				//
				MainForm.Instance.Pages.Add(editor);
				MainForm.Instance.SelectedPage = editor;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
		public void Open()
		{
			Open(null);
		}
		public void Open(IEditorTabFactory current)
		{
			using (OpenFileDialog frm = new OpenFileDialog())
			{
				frm.Filter = CreateFilter(current);
				frm.Multiselect = false;
				if (frm.ShowDialog() != DialogResult.OK)
					return;
				Open(frm.FileName, current);
			}
		}
		public void Open(string filename, IEditorTabFactory current)
		{
			//check if document is already opened
			foreach (ControlsEx.DockingFrames.TabPage page
				in MainForm.Instance.Pages)
			{
				EditorTab editor = page as EditorTab;
				if (editor != null && editor.Source == filename)
				{
					MainForm.Instance.SelectedPage = editor;
					return;
				}
			}
			//find opening factory
			if (current == null)
				foreach (IEditorTabFactory factory in Core.Instance.Factories)
				{
					if (factory.CanOpen(filename))
					{
						current = factory;
						break;
					}
				}
			//try opening with factory
			if (current != null)
			{
				try
				{
					EditorTab editor = current.Open(filename);
					//
					editor.Caption = Path.GetFileName(filename);
					editor.Source = filename;
					MainForm.Instance.Pages.Add(editor);
					MainForm.Instance.SelectedPage = editor;
					//
					StartPage.AddRecentFile(filename);
				}
				catch (Exception ex)
				{
					StartPage.RemoveRecentFile(filename);
					MessageBox.Show(ex.ToString());
				}
				return;
			}
			//found no input module
			MessageBox.Show("No Importer/Factory found");
		}
		/// <summary>
		/// save current editor
		/// </summary>
		public void Save()
		{
			EditorTab editor = MainForm.Instance.SelectedPage as EditorTab;
			if (editor != null)
				editor.Save();
		}
		/// <summary>
		/// save all open editor pages
		/// </summary>
		public void SaveAll()
		{
			foreach (ControlsEx.DockingFrames.TabPage page
				in MainForm.Instance.Pages)
			{
				EditorTab editor = page as EditorTab;
				if (editor != null)
					editor.Save();
			}
		}
		#endregion
		#region properties
		public DockingFrames.ToolBarButton ButtonUndo
		{
			get { return tbUndo; }
		}
		public DockingFrames.ToolBarButton ButtonRedo
		{
			get { return tbRedo; }
		}
		public DockingFrames.ToolBarButton ButtonCut
		{
			get { return tbCut; }
		}
		public DockingFrames.ToolBarButton ButtonCopy
		{
			get { return tbCopy; }
		}
		public DockingFrames.ToolBarButton ButtonPaste
		{
			get { return tbPaste; }
		}
		public DockingFrames.ToolBarButton ButtonDelete
		{
			get { return tbDelete; }
		}
		#endregion
	}
}
