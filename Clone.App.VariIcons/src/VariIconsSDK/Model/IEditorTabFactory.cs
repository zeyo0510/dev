using System;
using System.IO;
using ControlsEx.DockingFrames;
using VariIconsSDK.UI;
using System.Drawing;

namespace VariIconsSDK.Model
{
	/// <summary>
	/// factory for creating and opening documents
	/// </summary>
	public interface IEditorTabFactory
	{
		/// <summary>
		/// create a new empty editor
		/// </summary>
		EditorTab New();
		/// <summary>
		/// specified if a editor can be opened by this factory
		/// </summary>
		bool CanOpen(string filename);
		EditorTab Open(string filename);
		FileFilter[] Extensions { get; }
		//gui things
		string DocumentName { get; }
		//new action
		bool NewPossible { get; }
		Image NewImage { get; }
		string NewText { get; }
		//open action
		bool OpenPossible { get; }
		Image OpenImage { get; }
		string OpenText { get; }
	}
}
