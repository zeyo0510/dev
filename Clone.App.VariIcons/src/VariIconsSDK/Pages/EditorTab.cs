using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using VariIconsSDK.Model;
using VariIconsSDK.Pages;

namespace VariIconsSDK.UI
{
	public class EditorTab : MainTab
	{
		#region variables
		private string _source = null;
		private string _corecaption;
		private bool _dirty = false;
		#endregion
		#region controller
		protected override void AddGuiExtensions(MainForm frm)
		{
			frm.ToolBarStandart.ButtonUndo.Command.Click += new EventHandler(Undo_Click);
			frm.ToolBarStandart.ButtonRedo.Command.Click += new EventHandler(Redo_Click);
			//clipboard
			frm.ToolBarStandart.ButtonCut.Command.Click += new EventHandler(Cut_Click);
			frm.ToolBarStandart.ButtonCopy.Command.Click += new EventHandler(Copy_Click);
			frm.ToolBarStandart.ButtonPaste.Command.Click += new EventHandler(Paste_Click);
			frm.ToolBarStandart.ButtonDelete.Command.Click += new EventHandler(Delete_Click);
		}

		protected override void RemoveGuiExtensions()
		{
			if (MainForm == null)
				return;
			MainForm.ToolBarStandart.ButtonUndo.Command.Click -= new EventHandler(Undo_Click);
			MainForm.ToolBarStandart.ButtonRedo.Command.Click -= new EventHandler(Redo_Click);
			//clipboard
			MainForm.ToolBarStandart.ButtonCut.Command.Click -= new EventHandler(Cut_Click);
			MainForm.ToolBarStandart.ButtonCopy.Command.Click -= new EventHandler(Copy_Click);
			MainForm.ToolBarStandart.ButtonPaste.Command.Click -= new EventHandler(Paste_Click);
			MainForm.ToolBarStandart.ButtonDelete.Command.Click -= new EventHandler(Delete_Click);
		}
		protected void UpdateGuiExtensions()
		{
			if (MainForm == null)
				return;
			MainForm.ToolBarStandart.ButtonRedo.Enabled = RedoSteps > 0;
			MainForm.ToolBarStandart.ButtonUndo.Enabled = UndoSteps > 0;
		}
		//place an asterisk for unsaved documents
		private void UpdateCaption()
		{
			if (_corecaption == null)
				base.Caption = Dirty ? "*" : null;
			else
				base.Caption = Dirty ? _corecaption + "*" : _corecaption;
		}

		void Undo_Click(object sender, EventArgs e)
		{
			Undo();
		}
		void Redo_Click(object sender, EventArgs e)
		{
			Redo();
		}
		//clipboard
		void Cut_Click(object sender, EventArgs e)
		{
			Cut();
		}
		void Copy_Click(object sender, EventArgs e)
		{
			Copy();
		}
		void Paste_Click(object sender, EventArgs e)
		{
			Paste();
		}
		void Delete_Click(object sender, EventArgs e)
		{
			Delete();
		}
		#endregion
		#region virtual
		protected virtual void Save(string filename)
		{
		}
		protected virtual void Save(Stream str)
		{
		}
		protected virtual FileFilter[] Extensions
		{
			get { return null; }
		}

		protected virtual int UndoSteps
		{
			get { return 0; }
		}

		protected virtual void Undo()
		{
		}
		protected virtual int RedoSteps
		{
			get { return 0; }
		}

		protected virtual void Redo()
		{
		}
		protected virtual void Cut()
		{
		}
		protected virtual void Copy()
		{
		}
		protected virtual void Paste()
		{
		}
		protected virtual void Delete()
		{
		}
		#endregion
		#region public members
		/// <summary>
		/// cases this editor to save
		/// </summary>
		public void Save()
		{
			SaveFileDialog frm = null;
			try
			{
				if (Source != null)
				{
					Save(Source);
					Dirty = false;
					StartPage.AddRecentFile(Source);
				}
				else
				{
					frm = new SaveFileDialog();
					frm.Filter = FileFilter.BuildCombinedFilter(Extensions);
					frm.OverwritePrompt = true;
					if (frm.ShowDialog() == DialogResult.OK)
					{
						Save(frm.FileName);
						Source = frm.FileName;
						Dirty = false;
						StartPage.AddRecentFile(Source);
					}
				}
			}
			catch (Exception ex)
			{
				Source = null;
				MessageBox.Show(ex.StackTrace);
			}
			finally
			{
				if (frm != null)
					frm.Dispose();
			}
		}
		#endregion
		#region properties
		/// <summary>
		/// gets, if the editor should be saved before quitting
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Dirty
		{
			get { return _dirty; }
			protected set
			{
				if (value == _dirty)
					return;
				_dirty = value;
				UpdateCaption();
			}
		}
		/// <summary>
		/// gets or sets the caption of the editor
		/// NOTE: TabPage+Caption will return a different
		/// result due to dirty asterisk
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string Caption
		{
			get { return _corecaption; }
			set
			{
				if (value == _corecaption)
					return;
				_corecaption = value;
				UpdateCaption();
			}
		}
		/// <summary>
		/// gets or sets the source this editor was loaded from
		/// or last saved to
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Source
		{
			get { return _source; }
			set
			{
				_source = value;
				if (_source != null)
					this.Caption = Path.GetFileName(_source);
			}
		}
		#endregion
	}
}
