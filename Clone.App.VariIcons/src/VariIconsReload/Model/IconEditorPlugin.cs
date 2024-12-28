using VariIconsReload.Model;
using VariIconsReload.Properties;
using VariIconsSDK;
using VariIconsSDK.Model;
using ControlsEx.ListControls;
using VariIconsSDK.Pages;
using DrawingEx.IconEncoder;
using System.Windows.Forms;
using System;
using System.Drawing;
using VariIconsSDK.UI;
using System.IO;

namespace VariIconsReload
{
	public class IconEditorPlugin : IPlugin
	{

		private IconEditorFactory _factory;
		private IconLibraryEditorFactory _libraryfct;
		private Action actBatch;

		public IconEditorPlugin()
		{
			_factory = new IconEditorFactory();
			_libraryfct = new IconLibraryEditorFactory();
			//
			actBatch = new Action();
			actBatch.Image = Resources.batch;
			actBatch.Text = Resources.IconEditorPlugin_CreateFromImages;
			actBatch.Clicked += new System.EventHandler(actBatch_Clicked);
		}
		//open images
		void actBatch_Clicked(object sender, System.EventArgs e)
		{
			using (OpenFileDialog frm = new OpenFileDialog())
			{
				//use multiple files
				frm.Filter = FileFilter.BuildCombinedFilter(
					ImageEncoder.GetImportExtensions());
				frm.FilterIndex = 9;
				frm.Multiselect = true;
				if (frm.ShowDialog() == DialogResult.OK &&
					frm.FileNames.Length>0)
				{
					//combine to icon
					DrawingEx.IconEncoder.Icon icn =
						new DrawingEx.IconEncoder.Icon();
					foreach (string filename in frm.FileNames)
					{
						try
						{
							Bitmap bmp = ImageEncoder.ImportImage(filename,
								new Size(256, 256));
							if (bmp == null)
								break;
							icn.Images.Add(new IconImage32bpp(bmp));
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.ToString());
							break;
						}
					}
					//open as tab
					if (icn.Images.Count > 0)
					{
						Model.IconEditor editor = new Model.IconEditor(icn);
						editor.Caption = "MultiIcon";
						MainForm.Instance.Pages.Add(editor);
						MainForm.Instance.SelectedPage = editor;
					}
					else icn.Dispose();
				}
			}
		}
		#region IPlugin Member

		void IPlugin.Load()
		{
			Core.Instance.Factories.AddRange(_factory, _libraryfct);
			StartPage.CustomActions.Add(actBatch);
		}

		void IPlugin.Unload()
		{
			Core.Instance.Factories.RemoveRange(_factory, _libraryfct);
			StartPage.CustomActions.Remove(actBatch);
		}

		void IPlugin.SaveSettings()
		{
			Settings.Default.Save();
		}

		#endregion
	}
}
