using System;
using System.Collections.Generic;
using System.Text;
using VariIconsSDK.Model;
using VariIconsSDK.UI;
using System.IO;
using VariIconsReload.Components;
using DrawingEx.IconEncoder;
using VariIconsReload.Properties;


namespace VariIconsReload.Model
{
	public class IconEditorFactory : IEditorTabFactory
	{
		#region variables
		//buffers for opening files
		private string _lastfile;
		private Icon _lasticon;
		#endregion
		public EditorTab New()
		{
			Icon icon = new Icon();
			icon.Images.Add(new IconImage32bpp(new System.Drawing.Bitmap(16, 16)));
			icon.Images.Add(new IconImage32bpp(new System.Drawing.Bitmap(32, 32)));
			icon.Images.Add(new IconImage32bpp(new System.Drawing.Bitmap(48, 48)));
			return new Model.IconEditor(icon);
		}

		public bool CanOpen(string filename)
		{
			_lastfile = filename;
			try
			{
				//try to open icon
				_lasticon = new Icon(filename);
				return true;
			}
			catch
			{
				//failed, cannot import
				_lasticon = null;
				_lastfile = null;
				return false;
			}
		}

		public EditorTab Open(string filename)
		{
			Model.IconEditor editor;
			if (filename == _lastfile && _lasticon != null)
				//buffered icon
				editor = new Model.IconEditor(_lasticon);
			else
			{
				//tried to open other icon
				editor = new IconEditor(new Icon(filename));
				if (_lasticon != null)
					_lasticon.Dispose();
			}
			//clean up buffers
			_lasticon = null;
			_lastfile = null;
			return editor;
		}
		public FileFilter[] Extensions
		{
			get { return new FileFilter[] { Model.IconEditor.IconFilter }; }
		}
		public string DocumentName
		{
			get { return Resources.IconEditorFactory_DocumentName; }
		}
		//new action
		public bool NewPossible { get { return true; } }
		public System.Drawing.Image NewImage
		{
			get { return Resources.newicon; }
		}
		public string NewText
		{
			get { return Resources.IconEditorFactory_NewText; }
		}
		//open action
		public bool OpenPossible { get { return true; } }
		public System.Drawing.Image OpenImage
		{
			get { return Resources.openicon; }
		}
		public string OpenText
		{
			get { return Resources.IconEditorFactory_OpenText; }
		}
	}
}
