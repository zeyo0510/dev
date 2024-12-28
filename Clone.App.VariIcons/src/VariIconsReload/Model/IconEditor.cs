using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ControlsEx.ListControls;
using DockingFrames;
using DrawingEx.IconEncoder;
using VariIconsReload.BrushModel;
using VariIconsReload.Components;
using VariIconsReload.Effects;
using VariIconsReload.Forms;
using VariIconsReload.Tools;
using VariIconsReload.Properties;
using VariIconsSDK.Model;
using VariIconsSDK.UI;

namespace VariIconsReload.Model
{
	public partial class IconEditor : EditorTab
	{
		#region types
		/// <summary>
		/// imagedisplayelement for iconimages
		/// </summary>
		private class IconImageDisplayElement : ImageDisplayItem
		{
			private IconImage _iconimage;
			public IconImageDisplayElement(IconImage iconimage)
				: base(iconimage.Bitmap,
					iconimage.Bitmap.Width.ToString() + "x" +
				iconimage.Bitmap.Height.ToString() + " " +
				iconimage.BitsPerPixel.ToString() + "bpp")
			{
				_iconimage = iconimage;
			}
			protected override void OnDraw(Graphics gr, Rectangle rct)
			{
				//frame with cut corners
				LayerView.DrawCutFrame(gr, Color.FromArgb(128, 0, 0, 0), 1f, rct);
				//checker pattern
				using (HatchBrush hbrs = new HatchBrush(HatchStyle.LargeCheckerBoard,
					Color.Silver, Color.White))
				{
					gr.FillRectangle(hbrs, rct);
				}
				//image
				gr.DrawImage(_iconimage.Bitmap, rct);
			}
			public IconImage IconImage
			{
				get { return _iconimage; }
			}
		}
		#endregion
		#region variables
		public static readonly FileFilter IconFilter = new FileFilter("Icon", "ico");
		//
		private GridLayer _gridlayer;
		private PointSelectLayer _pointlayer;
		private Selection _selection;
		//
		private BrushManager _brushes;
		private ToolManager _tools;
		private EffectManager _effects;
		//
		private BufferedStateMetaHistory<DrawingEx.IconEncoder.Icon, int> _history;
		#endregion
		#region ctor
		public IconEditor(DrawingEx.IconEncoder.Icon icon)
		{
			if (icon == null)
				throw new ArgumentNullException("icon");

			InitializeComponent();
			//
			_brushes = BrushManager.RegisterInstance(this);
			_tools = ToolManager.RegisterInstance(this);
			_effects = EffectManager.RegisterInstance(this);
			//
			_gridlayer = new GridLayer();
			_pointlayer = new PointSelectLayer();
			layerView1.Layers.Add(_pointlayer);
			_selection = new Selection();
			_selection.ActionCommitted += new EventHandler(Tool_ActionCommitted);
			layerView1.Layers.Add(_selection.SelectionSliceLayer);
			//
			_history = new BufferedStateMetaHistory<DrawingEx.IconEncoder.Icon, int>();
			_history.Changed += new EventHandler(_history_Changed);
			_history.State = icon;
			//
			Dirty = false;
		}

		private void UserDispose()
		{
			layerView1.Layers.Clear();
			_selection.Dispose();
			_history.Dispose();
			ToolManager.UnregisterInstance(this);
			BrushManager.UnregisterInstance(this);
			EffectManager.UnregisterInstance(this);
		}
		#endregion
		#region controller
		//load settings from app settings
		protected override void OnLoad(EventArgs e)
		{
			tbGrid.Checked = Settings.Default.ShowGrid;
			tbAutozoom.Checked = Settings.Default.AutoZoom;
			cbFore.Color = layerView1.BackgroundColorA =
				Settings.Default.IconEditor_CheckerA;
			cbBack.Color = layerView1.BackgroundColorB =
				Settings.Default.IconEditor_CheckerB;
			//apply settings
			configGrid();
			configAutoZoom();
		}
		#endregion
		#region editortab implementations
		//history buttons
		protected override int RedoSteps
		{
			get { return _history.RedoSteps; }
		}
		protected override void Redo()
		{
			_history.Redo();
		}
		protected override int UndoSteps
		{
			get { return _history.UndoSteps; }
		}
		protected override void Undo()
		{
			_history.Undo();
		}
		//saving
		protected override void Save(string filename)
		{
			_history.State.Save(filename);
		}
		protected override void Save(System.IO.Stream str)
		{
			_history.State.Save(str);
		}
		protected override FileFilter[] Extensions
		{
			get { return new FileFilter[] { IconFilter }; }
		}
		//clipboard
		protected override void Copy()
		{
			if (_selection.Content == null)
				return;
			if (_selection.Bounds != Rectangle.Empty)
			{
				if (_selection.Slice == null)
					//copy slice
					Clipboard.SetDataObject(_selection.Copy(_selection.Bounds), false);
				else
					//copy content below
					Clipboard.SetDataObject(_selection.Slice.Clone(), false);
			}
			else
			{
				//copy whole image
				Clipboard.SetDataObject(_selection.Copy(
					new Rectangle(Point.Empty, _selection.Content.Bitmap.Size)));
			}
		}
		protected override void Delete()
		{
			if (_selection.Content == null)
				return;
			if (_selection.Bounds != Rectangle.Empty)
			{
				if (_selection.Slice != null)
					//delete slice
					_selection.Reset();
				else
					//delete content below
					_selection.Delete(_selection.Bounds);
			}
			else
			{
				//delete whole image
				_selection.Delete(
					new Rectangle(Point.Empty, _selection.Content.Bitmap.Size));
			}
			Commit();
		}
		protected override void Paste()
		{
			Paste(false);
		}
		//paste image data, if possible. newimg denotes wheter
		//it should be pasted as new image in any case or not
		private void Paste(bool newimg)
		{
			try
			{
				Image img = Clipboard.GetDataObject().GetData(typeof(Bitmap)) as Image;
				if (img == null)
					return;
				_tools.CurrentInfo = _tools.SelectionTool;
				Bitmap ret = ImageEncoder.ImportImage(img, new Size(256, 256));
				if (_selection.Content == null || newimg)
					AddImage(ret);
				else
					_selection.Paste(ret);
				img.Dispose();
			}
			catch { return; }
		}
		protected override void Cut()
		{
			this.Copy();
			this.Delete();
		}
		#endregion
		#region history
		void Commit()
		{
			_history.Meta = vDisplayList1.SelectedIndex;
			_history.Commit();
		}
		void _history_Changed(object sender, EventArgs e)
		{
			UpdateGuiExtensions();
			Dirty = true;
			//just updated one image due to tool activity
			if (_history.State != null &&
				_history.State.Images.Count == vDisplayList1.Items.Count)
			{
				for (int i = 0; i < _history.State.Images.Count; i++)
					vDisplayList1.Items[i] =
						new IconImageDisplayElement(_history.State.Images[i]);
				//make layerviewer display updates
				if (_history.Meta == vDisplayList1.SelectedIndex)
					vDisplayList1_SelectionChanged(vDisplayList1, EventArgs.Empty);
				else
					vDisplayList1.SelectedIndex = _history.Meta;
			}
			else
			{
				//changed icon, reload
				vDisplayList1.Items.Clear();
				if (_history.State != null)
				{
					foreach (IconImage img in _history.State.Images)
						vDisplayList1.Items.Add(
							new IconImageDisplayElement(img));
					//if removed some elements the next image gets selected
					vDisplayList1.SelectedIndex = _history.Meta;
				}
				UpdateDefaultAddImage();
			}
		}
		//raised when list or list selection is changed
		private void vDisplayList1_SelectionChanged(object sender, EventArgs e)
		{
			IconImageDisplayElement elem =
				vDisplayList1.SelectedItem as IconImageDisplayElement;
			if (elem != null)
			{
				layerView1.Data = new ImageContent<Bitmap>(elem.IconImage.Bitmap);
				_tools.CurrentTool.Content = elem.IconImage;
				_effects.Content = elem.IconImage;
				_selection.Content = elem.IconImage;
				//
				tbRemove.Enabled = true;
				tbDown.Enabled = vDisplayList1.SelectedIndex <
					vDisplayList1.Items.Count - 1;
				tbUp.Enabled = vDisplayList1.SelectedIndex >
					0;
			}
			else
			{
				layerView1.Data = null;
				_tools.CurrentTool.Content = null;
				_effects.Content = null;
				_selection.Content = null;
				//
				tbRemove.Enabled = tbUp.Enabled =
					tbDown.Enabled = false;
			}
			configGrid();
		}
		#endregion
		#region gui extensions
		//add extending toolbars and frames to mainform
		protected override void AddGuiExtensions(MainForm frm)
		{
			base.AddGuiExtensions(frm);
			statusControls.Strip = frm.StatusStrip;
			//set default ret info
			frm.DockManager.SetDefaultDock(_tools.ToolBox, Point.Empty, frm.LeftDockingArea);
			frm.DockManager.SetDefaultDock(_tools.ConfigBar, new Point(0, 50), frm.TopDockingArea);
			frm.DockManager.SetDefaultDock(_effects.EffectsBar, new Point(0, 25), frm.TopDockingArea);
			frm.DockManager.SetDefaultDock(_brushes.Frame, Point.Empty, frm.RightDockingArea);
			//
			frm.DockManager.DockingFrames.AddRange(new DockingFrame[]{
			    _tools.ConfigBar,_tools.ToolBox,_effects.EffectsBar,_brushes.Frame
			});
			AttachManagers();
		}
		//remove all extending components again
		protected override void RemoveGuiExtensions()
		{
			DetachManagers();
			//
			base.RemoveGuiExtensions();
			statusControls.ReaquireAll();
			//
			MainForm.DockManager.DockingFrames.RemoveRange(new DockingFrame[]{
			    _tools.ToolBox,_tools.ConfigBar,_effects.EffectsBar,_brushes.Frame
			});
		}
		private void AttachManagers()
		{
			//connect to toolmanager
			_tools.ActionCommitted += new EventHandler(Tool_ActionCommitted);
			_tools.ToolStatusChanged += new EventHandler<ToolStatusEventArgs>(Tool_StatusChanged);
			_tools.ToolChanged += new EventHandler<ToolChangedEventArgs>(Tool_Changed);
			this.Tool_Changed(this, new ToolChangedEventArgs(null, _tools.CurrentTool, _tools.CurrentInfo));
			//
			_effects.ActionCommitted += new EventHandler(Tool_ActionCommitted);
			_effects.BeforeRender += new EventHandler(Effects_BeforeRender);
			_effects.LayerView = layerView1;
			//load content
			IconImageDisplayElement elem =
			vDisplayList1.SelectedItem as IconImageDisplayElement;
			if (elem != null)
			{
				_effects.Content = elem.IconImage;
				_tools.CurrentTool.Content = elem.IconImage;
			}
		}
		private void DetachManagers()
		{
			//disconnect from tool manager
			_tools.ActionCommitted -= new EventHandler(Tool_ActionCommitted);
			_tools.ToolStatusChanged -= new EventHandler<ToolStatusEventArgs>(Tool_StatusChanged);
			_tools.ToolChanged -= new EventHandler<ToolChangedEventArgs>(Tool_Changed);
			_tools.CurrentTool.Content = null;
			this.Tool_Changed(this, new ToolChangedEventArgs(_tools.CurrentTool, null, null));
			//
			_effects.Content = null;
			_effects.LayerView = null;
			_effects.ActionCommitted -= new EventHandler(Tool_ActionCommitted);
			_effects.BeforeRender -= new EventHandler(Effects_BeforeRender);
		}
		#endregion
		#region grid & zoom
		private bool _zooming = false;
		//enable grid if activated and zoomed in enough
		private void configGrid()
		{
			if (layerView1.RealZoom >= 4 && tbGrid.Checked)
			{
				if (!layerView1.Layers.Contains(_gridlayer))
					layerView1.Layers.Add(_gridlayer);
			}
			else if (layerView1.Layers.Contains(_gridlayer))
				layerView1.Layers.Remove(_gridlayer);
		}
		private void configAutoZoom()
		{
			_zoombar.Enabled = !tbAutozoom.Checked;
			layerView1.DisplayMode = tbAutozoom.Checked ?
				VariIconsReload.Components.DisplayMode.CenterStretch :
				VariIconsReload.Components.DisplayMode.Center;
			configGrid();
		}
		//zoom control and grid update
		void ZoomChanged(object sender, EventArgs e)
		{
			_zooming = true;
			layerView1.Zoom = _zoombar.Zoom;
			configGrid();
			_zooming = false;
		}

		private void layerView1_ZoomChanged(object sender, EventArgs e)
		{
			if (!_zooming)
				_zoombar.Zoom = layerView1.Zoom;
		}
		//toogle autozoom
		private void tbAutozoom_CheckedChanged(object sender, EventArgs e)
		{
			configAutoZoom();
			Settings.Default.AutoZoom = tbAutozoom.Checked;
		}
		//toogle grid
		private void tbGrid_CheckedChanged(object sender, EventArgs e)
		{
			configGrid();
			Settings.Default.ShowGrid = tbGrid.Checked;
		}
		//check grid on resize
		private void layerView1_SizeChanged(object sender, EventArgs e)
		{
			if (tbAutozoom.Checked && tbGrid.Checked)
				configGrid();
		}
		//checkerboard colors
		private void cbBack_ColorChanged(object sender, EventArgs e)
		{
			layerView1.BackgroundColorB =
				Settings.Default.IconEditor_CheckerB = cbBack.Color;
		}

		private void cbFore_ColorChanged(object sender, EventArgs e)
		{
			layerView1.BackgroundColorA =
				Settings.Default.IconEditor_CheckerA = cbFore.Color;
		}
		#endregion
		#region tools & effects
		//reset selection
		private void Effects_BeforeRender(object sender, EventArgs e)
		{
			_selection.Draw();
		}
		private void Tool_Changed(object sender, ToolChangedEventArgs e)
		{
			if (e.Previous != null)
			{
				e.Previous.Content = null;
				//layers
				layerView1.SuspendRefresh();
				foreach (Layer layer in e.Previous.Layers)
					layerView1.Layers.Remove(layer);
				layerView1.ResumeRefresh();
				//
				if (MainForm != null && e.Current != null)
					MainForm.ResetStatusText();
			}
			if (e.Current != null)
			{
				//load into tool
				IconImageDisplayElement elem =
				vDisplayList1.SelectedItem as IconImageDisplayElement;
				if (elem != null)
					e.Current.Content = elem.IconImage;
				//load into view
				layerView1.SuspendRefresh();
				foreach (Layer layer in e.Current.Layers)
					layerView1.Layers.Add(layer);
				layerView1.ResumeRefresh();
				//
				if (MainForm != null && e.CurrentInfo != null)
					MainForm.StatusText = e.CurrentInfo.Description;
				//
				if (e.Previous is SelectionTool)
					_selection.Draw();
			}
		}

		private void Tool_StatusChanged(object sender, ToolStatusEventArgs e)
		{
			lblCursorPosition.Text = e.Mark.X.ToString() +
				"; " + e.Mark.Y.ToString();
			lblSelectionSize.Text = e.Mark.Width.ToString() +
				"; " + e.Mark.Height.ToString();
			//
			layerView1.RulerMark = e.Mark;
		}
		//tool or effect committed
		private void Tool_ActionCommitted(object sender, EventArgs e)
		{
			//quantize image
			IconImageDisplayElement elem =
				vDisplayList1.SelectedItem as IconImageDisplayElement;
			if (elem != null)
			{
				IconImageIndexed img = elem.IconImage as IconImageIndexed;
				if (img != null)
					img.Quantize(true, DitheringType.FloydSteinberg);
			}
			//
			_history.PreviousMeta = vDisplayList1.SelectedIndex;
			Commit();
		}
		#endregion
		#region image list
		//add predefined images
		private void tbAdd_Command_Click(object sender, EventArgs e)
		{
			if (sender == tb16x16b1.Command) AddImage(16, 16, 1);
			else if (sender == tb32x32b1.Command) AddImage(32, 32, 1);
			else if (sender == tb48x48b1.Command) AddImage(48, 48, 1);
			//
			else if (sender == tb16x16b2.Command) AddImage(16, 16, 2);
			else if (sender == tb32x32b2.Command) AddImage(32, 32, 2);
			else if (sender == tb48x48b2.Command) AddImage(48, 48, 2);
			//
			else if (sender == tb16x16b4.Command) AddImage(16, 16, 4);
			else if (sender == tb32x32b4.Command) AddImage(32, 32, 4);
			else if (sender == tb48x48b4.Command) AddImage(48, 48, 4);
			//
			else if (sender == tb16x16b8.Command) AddImage(16, 16, 8);
			else if (sender == tb32x32b8.Command) AddImage(32, 32, 8);
			else if (sender == tb48x48b8.Command) AddImage(48, 48, 8);
			//
			else if (sender == tbAdd16x16b32.Command ||
				(sender == tbAdd.Command && tbAdd16x16b32.Appearance == DockingFrames.Appearance.ImageAndText))
				AddImage(16, 16, 32);
			else if (sender == tbAdd32x32b32.Command ||
				(sender == tbAdd.Command && tbAdd32x32b32.Appearance == DockingFrames.Appearance.ImageAndText))
				AddImage(32, 32, 32);
			else if (sender == tbAdd48x48b32.Command ||
				(sender == tbAdd.Command && tbAdd48x48b32.Appearance == DockingFrames.Appearance.ImageAndText))
				AddImage(48, 48, 32);
			//
			else if (sender == tbCustomFormat.Command ||
				(sender == tbAdd.Command && tbCustomFormat.Appearance == DockingFrames.Appearance.ImageAndText))
			{
				using (CustomFormat frm = new CustomFormat())
				{
					if (frm.ShowDialog() == DialogResult.OK)
						AddImage(frm.ImageWidth, frm.ImageHeight, frm.BitsPerPixel);
				}
			}
		}
		//updates the plus icon in the add image menu
		private void UpdateDefaultAddImage()
		{
			// update plus icon
			tbAdd16x16b32.Appearance = tbAdd32x32b32.Appearance = tbAdd48x48b32.Appearance =
				tbCustomFormat.Appearance = DockingFrames.Appearance.Text;
			if (!ContainsImage(16, 16, 32))
				tbAdd16x16b32.Appearance = DockingFrames.Appearance.ImageAndText;
			else if (!ContainsImage(32, 32, 32))
				tbAdd32x32b32.Appearance = DockingFrames.Appearance.ImageAndText;
			else if (!ContainsImage(48, 48, 32))
				tbAdd48x48b32.Appearance = DockingFrames.Appearance.ImageAndText;
			else
				tbCustomFormat.Appearance = DockingFrames.Appearance.ImageAndText;
		}
		//evaluates if a image with the given format is in the editor
		private bool ContainsImage(int w, int h, int bpp)
		{
			if (_history.State == null)
				return false;
			foreach (IconImage img in _history.State.Images)
				if (img.BitsPerPixel == bpp &&
					img.Bitmap.Width == w &&
					img.Bitmap.Height == h)
					return true;
			return false;
		}
		//adds a new image with the given format
		private void AddImage(int w, int h, short bpp)
		{
			if (_history.State == null)
				return;
			Bitmap bmp = new Bitmap(w, h,
				PixelFormat.Format32bppArgb);
			IconImage img = null;
			switch (bpp)
			{
				case 32: img = new IconImage32bpp(bmp); break;
				case 24: img = new IconImage24bpp(bmp); break;
				case 8:
				case 4:
				case 2:
				case 1: img = new IconImageIndexed(bmp, bpp); break;
				default: bmp.Dispose(); return;//bug
			}
			//
			vDisplayList1.Items.Add(new IconImageDisplayElement(img));//to make fast gui update
			_history.State.Images.Add(img);
			Commit();
			//select last image
			vDisplayList1.SelectedIndex =
				vDisplayList1.Items.Count - 1;
		}
		private void AddImage(Bitmap bmp)
		{
			IconImage img = new IconImage32bpp(bmp);
			vDisplayList1.Items.Add(new IconImageDisplayElement(img));//to make fast gui update
			_history.State.Images.Add(img);
			Commit();
			//select last image
			vDisplayList1.SelectedIndex =
				vDisplayList1.Items.Count - 1;
		}
		private void ResizeImage(int index)
		{
			//out of range, no image or size identical
			if (_history.State == null ||
				index < 0 || index >= _history.State.Images.Count)
				return;
			using (DocumentResize frm = new DocumentResize())
			{
				IconImage original = _history.State.Images[index];
				frm.OldDocumentSize = original.Bitmap.Size;
				//
				if (frm.ShowDialog() != DialogResult.OK ||
					original.Bitmap.Size == frm.DocumentSize)
					return;
				//create new image with same resolution
				Bitmap bmp = new Bitmap(frm.DocumentSize.Width,
					frm.DocumentSize.Height, PixelFormat.Format32bppArgb);
				IconImage img = null;
				switch (original.BitsPerPixel)
				{
					case 32: img = new IconImage32bpp(bmp); break;
					case 24: img = new IconImage24bpp(bmp); break;
					case 8:
					case 4:
					case 2:
					case 1: img = new IconImageIndexed(bmp, original.BitsPerPixel); break;
					default: bmp.Dispose(); return;//bug
				}
				//copy contents
				using (Graphics gr = Graphics.FromImage(bmp))
				{
					gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
					if (frm.DocumentScale)
					{
						gr.InterpolationMode = frm.InterpolationMode;
						gr.DrawImage(original.Bitmap, 0, 0,
							bmp.Width, bmp.Height);
					}
					else
					{
						Point pt = Point.Empty;
						//vertical
						if ((frm.DocumentAnchor & AnchorStyles.Top) != 0)
							pt.Y = 0;
						else if ((frm.DocumentAnchor & AnchorStyles.Bottom) != 0)
							pt.Y = bmp.Height - original.Bitmap.Height;
						else
							pt.Y = (bmp.Height - original.Bitmap.Height) / 2;
						//horizontal
						if ((frm.DocumentAnchor & AnchorStyles.Left) != 0)
							pt.X = 0;
						else if ((frm.DocumentAnchor & AnchorStyles.Right) != 0)
							pt.X = bmp.Width - original.Bitmap.Width;
						else
							pt.X = (bmp.Width - original.Bitmap.Width) / 2;
						//
						gr.DrawImageUnscaled(original.Bitmap, pt);
					}
				}
				//swap images
				vDisplayList1.Items[index] = new IconImageDisplayElement(img);//to make fast gui update
				_history.State.Images[index] = img;
				Commit();
				//dispose original
				original.Dispose();
			}
		}

		private void tbRemove_Command_Click(object sender, EventArgs e)
		{
			int index = vDisplayList1.SelectedIndex;
			if (_history.State != null && index >= 0)
			{
				_history.PreviousMeta = index;
				IconImage original = _history.State.Images[
					vDisplayList1.SelectedIndex];
				//
				vDisplayList1.Items.RemoveAt(index);//to make fast gui update
				_history.State.Images.RemoveAt(index);
				//commit changes
				Commit();
				//dispose old
				original.Dispose();
			}
		}

		private void tbDown_Command_Click(object sender, EventArgs e)
		{
			int index = vDisplayList1.SelectedIndex;
			if (_history.State != null &&
				index >= 0 && index < _history.State.Images.Count - 1)
			{
				layerView1.SuspendRefresh();
				_history.PreviousMeta = index;
				IconImage tmp = _history.State.Images[index];
				_history.State.Images[index] = _history.State.Images[index + 1];
				_history.State.Images[index + 1] = tmp;
				//commit changes
				Commit();
				vDisplayList1.SelectedIndex = index + 1;
				layerView1.ResumeRefresh();
			}
		}

		private void tbUp_Command_Click(object sender, EventArgs e)
		{
			int index = vDisplayList1.SelectedIndex;
			if (_history.State != null && index >= 1)
			{
				layerView1.SuspendRefresh();
				_history.PreviousMeta = index;
				IconImage tmp = _history.State.Images[index];
				_history.State.Images[index] = _history.State.Images[index - 1];
				_history.State.Images[index - 1] = tmp;
				//commit changes
				Commit();
				vDisplayList1.SelectedIndex = index - 1;
				layerView1.ResumeRefresh();
			}
		}
		//resize current image
		private void tbResize_Click(object sender, EventArgs e)
		{
			ResizeImage(vDisplayList1.SelectedIndex);
		}
		//import image to list
		private void tbOpenImage_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog frm = new OpenFileDialog())
			{
				frm.Filter = FileFilter.BuildCombinedFilter(
					ImageEncoder.GetImportExtensions());
				frm.FilterIndex = 9;
				if (frm.ShowDialog() == DialogResult.OK)
				{
					try
					{
						Bitmap bmp = ImageEncoder.ImportImage(frm.FileName,
							new System.Drawing.Size(256, 256));
						if (bmp == null)
							return;
						AddImage(bmp);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
				}
			}
		}

		private void tbPasteImage_Command_Click(object sender, EventArgs e)
		{
			Paste(true);
		}
		//save current selected image
		private void tbSave_Click(object sender, EventArgs e)
		{
			IconImageDisplayElement elem =
				vDisplayList1.SelectedItem as IconImageDisplayElement;
			if (elem == null)
				return;
			using (SaveFileDialog frm = new SaveFileDialog())
			{
				frm.Filter = FileFilter.BuildCombinedFilter(
					ImageEncoder.GetExportExtensions());
				frm.FilterIndex = 5;//usually png
				if (frm.ShowDialog() == DialogResult.OK)
				{
					try
					{
						ImageEncoder.ExportImage(elem.IconImage.Bitmap, frm.FileName);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
				}
			}
		}
		#endregion
		#region properties
		/// <summary>
		/// gets or sets the currently displayed icon
		/// </summary>
		public DrawingEx.IconEncoder.Icon Icon
		{
			get { return _history.State; }
		}
		#endregion
	}
}
