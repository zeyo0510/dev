using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ControlsEx.ListControls;
using DrawingEx.IconEncoder;
using VariIconsSDK.Model;

namespace VariIconsReload.Model
{
	public partial class IconLibraryEditor : VariIconsSDK.UI.EditorTab
	{
		#region types
		/// <summary>
		/// listviewitem for displaying the multipla images of an icon
		/// </summary>
		private class MultiIconDisplayItem : DisplayItem
		{
			#region variables
			private DrawingEx.IconEncoder.Icon _icon;
			private Size _size;
			#endregion
			public MultiIconDisplayItem(DrawingEx.IconEncoder.Icon icon, string text, object tag)
				: base(text, tag)
			{
				_icon = icon;
				UpdateSize();
			}
			public override void Dispose()
			{
				if (_icon != null)
					_icon.Dispose();
				base.Dispose();
			}
			#region controller
			private void UpdateSize()
			{
				_size = Size.Empty;
				if (_icon == null)
					return;
				foreach (IconImage img in _icon.Images)
				{
					_size.Height = Math.Max(_size.Height, img.Bitmap.Height);
					_size.Width += 1 + img.Bitmap.Width;
				}
			}
			protected override void OnDraw(Graphics gr, Rectangle rct)
			{
				if (_icon == null || _size.Height < 1 || _size.Width < 1)
					return;
				float x = rct.X,
					scale = Math.Min(1f, Math.Min(
					rct.Width / _size.Width, rct.Height / _size.Height));
				foreach (IconImage img in _icon.Images)
				{
					float w = scale * (float)img.Bitmap.Width;
					gr.DrawImage(img.Bitmap, x, rct.Y,
						w, scale * img.Bitmap.Height);
					x += w + 1f;
				}
			}
			protected override void OnDrawUnscaled(Graphics gr, int x, int y)
			{
				if (_icon == null)
					return;
				foreach (IconImage img in _icon.Images)
				{
					gr.DrawImageUnscaled(img.Bitmap, x, y);
					x += 1 + img.Bitmap.Width;
				}
			}
			public override Size Size
			{
				get { return _size; }
			}
			#endregion
			public DrawingEx.IconEncoder.Icon Icon
			{
				get { return _icon; }
			}
		}
		#endregion
		public IconLibraryEditor(IconCollection coll)
		{
			if (coll == null)
				throw new ArgumentNullException("coll");
			InitializeComponent();
			//
			listIcons.SuspendUpdates();
			int index = 0;
			foreach (DrawingEx.IconEncoder.Icon icon in coll)
				listIcons.Items.Add(new MultiIconDisplayItem(icon, (++index).ToString(), null));
			listIcons.ResumeUpdates();
		}
		#region controller
		//open image list
		private void listIcons_SelectionChanged(object sender, EventArgs e)
		{
			MultiIconDisplayItem item = listIcons.SelectedItem as MultiIconDisplayItem;
			listImages.SuspendUpdates();
			listImages.Items.Clear();
			if (item != null)
			{
				foreach (IconImage img in item.Icon.Images)
					listImages.Items.Add(new ImageDisplayItem(img.Bitmap,
						img.Bitmap.Width.ToString() + "x" +
						img.Bitmap.Height.ToString() + " " +
						img.BitsPerPixel.ToString() + "bpp"));
			}
			listImages.ResumeUpdates();
			//enable controls
			tbSaveIcon.Enabled = tbOpenIcon.Enabled = item != null;
		}
		//open icon in new tab
		private void tbOpenIcon_Command_Click(object sender, EventArgs e)
		{
			MultiIconDisplayItem item = listIcons.SelectedItem as MultiIconDisplayItem;
			if (item == null)
				return;
			IconEditor editor = new IconEditor(item.Icon);
			editor.Caption = this.Caption + ">>" + item.Text;
			MainForm.Pages.Add(editor);
			MainForm.SelectedPage = editor;
		}
		//export icon to new file
		private void tbSaveIcon_Command_Click(object sender, EventArgs e)
		{
			MultiIconDisplayItem item = listIcons.SelectedItem as MultiIconDisplayItem;
			if (item == null)
				return;
			using (SaveFileDialog sd = new SaveFileDialog())
			{
				sd.Filter = IconEditor.IconFilter.BuildFilter();
				if (sd.ShowDialog() != DialogResult.OK)
					return;
				try
				{
					item.Icon.Save(sd.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}
		//update controls
		private void listImages_SelectionChanged(object sender, EventArgs e)
		{
			ImageDisplayItem item = listImages.SelectedItem as ImageDisplayItem;
			tbCopyImage.Enabled = tbSaveImage.Enabled =
				item != null;
		}
		//copy current image to clipboard
		private void tbCopyImage_Command_Click(object sender, EventArgs e)
		{
			ImageDisplayItem item = listImages.SelectedItem as ImageDisplayItem;
			if (item == null)
				return;
			Clipboard.SetDataObject(item.Image.Clone(), false);
		}
		//save current image
		private void tbSaveImage_Command_Click(object sender, EventArgs e)
		{
			ImageDisplayItem item = listImages.SelectedItem as ImageDisplayItem;
			if (item == null)
				return;
			Bitmap bmp = item.Image as Bitmap;
			if (bmp == null)
				return;
			//
			using (SaveFileDialog frm = new SaveFileDialog())
			{
				frm.Filter = FileFilter.BuildCombinedFilter(
					ImageEncoder.GetExportExtensions());
				frm.FilterIndex = 5;
				if (frm.ShowDialog() != DialogResult.OK)
					return;
				try
				{
					ImageEncoder.ExportImage(bmp, frm.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}
		#endregion

	}
}
