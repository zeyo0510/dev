using DrawingEx.IconEncoder;
using VariIconsReload.Properties;
using VariIconsSDK.Model;
using VariIconsSDK.UI;

namespace VariIconsReload.Model
{
	/// <summary>
	/// factory for an icon library editor
	/// </summary>
	public class IconLibraryEditorFactory : IEditorTabFactory
	{
		#region types
		//asynchronous loader for icon library
		private class IconLibraryLoader : Worker<string, IconCollection>
		{
			protected override IconCollection Run(string argument)
			{
				return IconCollection.FromDLL(argument);
			}
		}
		#endregion
		#region variables
		/// <summary>
		/// file filter for icon library
		/// </summary>
		private static readonly FileFilter[] extensions = new FileFilter[]{
			new FileFilter(Resources.IconLibraryEditorFactory_DocumentName,
				new string[]{"dll","exe"})
		};
		#endregion
		#region factory members
		public EditorTab New()
		{
			return null;
		}

		public bool CanOpen(string filename)
		{
			return true;
		}

		//load icon
		public EditorTab Open(string filename)
		{
			//loading may take a long time, so use an async loader
			IconLibraryLoader loader = new IconLibraryLoader();
			using (WorkerForm<string, IconCollection> frm =
				new WorkerForm<string, IconCollection>(loader, filename))
			{
				frm.Cancellable = false;
				frm.Text = OpenText;
				frm.ShowDialog();
			}
			if (loader.Result == null)
				return null;
			return new IconLibraryEditor(loader.Result);
		}

		public FileFilter[] Extensions
		{
			get { return extensions; }
		}

		public string DocumentName
		{
			get { return Resources.IconLibraryEditorFactory_DocumentName; }
		}
		#endregion
		#region factory gui members
		public bool NewPossible
		{
			get { return false; }
		}

		public System.Drawing.Image NewImage
		{
			get { return null; }
		}

		public string NewText
		{
			get { return null; }
		}

		public bool OpenPossible
		{
			get { return true; }
		}

		public System.Drawing.Image OpenImage
		{
			get { return Resources.library; }
		}

		public string OpenText
		{
			get { return Resources.IconLibraryFactory_OpenText; }
		}
		#endregion
	}
}
