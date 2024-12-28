using VariIconsSDK.UI;
namespace VariIconsReload.Model
{
	partial class IconEditor
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			UserDispose();
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-MainTab geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconEditor));
			this.panel1 = new System.Windows.Forms.Panel();
			this.vDisplayList1 = new ControlsEx.ListControls.VDisplayList();
			this.toolBar2 = new DockingFrames.ToolBar();
			this.tbOpenImage = new DockingFrames.ToolBarButton();
			this.tbPasteImage = new DockingFrames.ToolBarButton();
			this.tbSaveImage = new DockingFrames.ToolBarButton();
			this.toolBarSpace2 = new DockingFrames.ToolBarSpace();
			this.tbResize = new DockingFrames.ToolBarButton();
			this.toolBar1 = new DockingFrames.ToolBar();
			this.tbAdd = new DockingFrames.ToolBarSplitButton();
			this.tbAdd16x16b32 = new DockingFrames.ToolBarButton();
			this.tbAdd32x32b32 = new DockingFrames.ToolBarButton();
			this.tbAdd48x48b32 = new DockingFrames.ToolBarButton();
			this.sep1 = new DockingFrames.ToolBarSeparator();
			this.tbOtherFormats = new DockingFrames.ToolBarDropDownButton();
			this.tb16x16b8 = new DockingFrames.ToolBarButton();
			this.tb32x32b8 = new DockingFrames.ToolBarButton();
			this.tb48x48b8 = new DockingFrames.ToolBarButton();
			this.toolBarSeparator1 = new DockingFrames.ToolBarSeparator();
			this.tb16x16b4 = new DockingFrames.ToolBarButton();
			this.tb32x32b4 = new DockingFrames.ToolBarButton();
			this.tb48x48b4 = new DockingFrames.ToolBarButton();
			this.toolBarSeparator2 = new DockingFrames.ToolBarSeparator();
			this.tb16x16b2 = new DockingFrames.ToolBarButton();
			this.tb32x32b2 = new DockingFrames.ToolBarButton();
			this.tb48x48b2 = new DockingFrames.ToolBarButton();
			this.toolBarSeparator3 = new DockingFrames.ToolBarSeparator();
			this.tb16x16b1 = new DockingFrames.ToolBarButton();
			this.tb32x32b1 = new DockingFrames.ToolBarButton();
			this.tb48x48b1 = new DockingFrames.ToolBarButton();
			this.tbCustomFormat = new DockingFrames.ToolBarButton();
			this.tbRemove = new DockingFrames.ToolBarButton();
			this.toolBarSpace1 = new DockingFrames.ToolBarSpace();
			this.tbUp = new DockingFrames.ToolBarButton();
			this.tbDown = new DockingFrames.ToolBarButton();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this._zoombar = new ControlsEx.ZoomBar();
			this.lblCursorPosition = new System.Windows.Forms.ToolStripLabel();
			this.lblSelectionSize = new System.Windows.Forms.ToolStripLabel();
			this.tbsep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tbsep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tbAutozoom = new System.Windows.Forms.ToolStripButton();
			this.tbGrid = new System.Windows.Forms.ToolStripButton();
			this.tbsep3 = new System.Windows.Forms.ToolStripSeparator();
			this.statusControls = new VariIconsSDK.UI.StripControlContainer(this.components);
			this.cbFore = new VariIconsReload.Components.ToolStripColorButton();
			this.cbBack = new VariIconsReload.Components.ToolStripColorButton();
			this.sep3 = new System.Windows.Forms.ToolStripSeparator();
			this.layerView1 = new VariIconsReload.Components.LayerView();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.vDisplayList1);
			this.panel1.Controls.Add(this.toolBar2);
			this.panel1.Controls.Add(this.toolBar1);
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			// 
			// vDisplayList1
			// 
			this.vDisplayList1.AllowDrop = true;
			resources.ApplyResources(this.vDisplayList1, "vDisplayList1");
			this.vDisplayList1.Name = "vDisplayList1";
			this.vDisplayList1.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
			this.vDisplayList1.SelectionChanged += new System.EventHandler(this.vDisplayList1_SelectionChanged);
			// 
			// toolBar2
			// 
			resources.ApplyResources(this.toolBar2, "toolBar2");
			this.toolBar2.ExtensionButtonVisible = false;
			this.toolBar2.GripVisible = false;
			this.toolBar2.Guid = "a942344f-97ce-4d7c-bba8-bbec9465cad9";
			this.toolBar2.Lines = 1;
			this.toolBar2.Name = "toolBar2";
			this.toolBar2.PreferredLocation = new System.Drawing.Point(0, 411);
			this.toolBar2.ToolBarControls.AddRange(new DockingFrames.ToolBarControl[] {
            this.tbOpenImage,
            this.tbPasteImage,
            this.tbSaveImage,
            this.toolBarSpace2,
            this.tbResize});
			// 
			// tbOpenImage
			// 
			// 
			// 
			// 
			this.tbOpenImage.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbOpenImage.Command.Image")));
			this.tbOpenImage.Command.Shortcut = ((System.Windows.Forms.Keys)(resources.GetObject("tbOpenImage.Command.Shortcut")));
			this.tbOpenImage.Command.Text = resources.GetString("tbOpenImage.Command.Text");
			this.tbOpenImage.Command.Click += new System.EventHandler(this.tbOpenImage_Click);
			// 
			// tbPasteImage
			// 
			// 
			// 
			// 
			this.tbPasteImage.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbPasteImage.Command.Image")));
			this.tbPasteImage.Command.Shortcut = ((System.Windows.Forms.Keys)(resources.GetObject("tbPasteImage.Command.Shortcut")));
			this.tbPasteImage.Command.Text = resources.GetString("tbPasteImage.Command.Text");
			this.tbPasteImage.Command.Click += new System.EventHandler(this.tbPasteImage_Command_Click);
			// 
			// tbSaveImage
			// 
			// 
			// 
			// 
			this.tbSaveImage.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbSaveImage.Command.Image")));
			this.tbSaveImage.Command.Shortcut = ((System.Windows.Forms.Keys)(resources.GetObject("tbSaveImage.Command.Shortcut")));
			this.tbSaveImage.Command.Text = resources.GetString("tbSaveImage.Command.Text");
			this.tbSaveImage.Command.Click += new System.EventHandler(this.tbSave_Click);
			// 
			// toolBarSpace2
			// 
			this.toolBarSpace2.Length = 69;
			// 
			// tbResize
			// 
			// 
			// 
			// 
			this.tbResize.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbResize.Command.Image")));
			this.tbResize.Command.Text = resources.GetString("tbResize.Command.Text");
			this.tbResize.Command.Click += new System.EventHandler(this.tbResize_Click);
			// 
			// toolBar1
			// 
			resources.ApplyResources(this.toolBar1, "toolBar1");
			this.toolBar1.ExtensionButtonVisible = false;
			this.toolBar1.GripVisible = false;
			this.toolBar1.Guid = "ac77b8b5-57d6-4920-9cdf-a1591facf82a";
			this.toolBar1.Lines = 1;
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.PreferredLocation = new System.Drawing.Point(0, 13);
			this.toolBar1.ToolBarControls.AddRange(new DockingFrames.ToolBarControl[] {
            this.tbAdd,
            this.tbRemove,
            this.toolBarSpace1,
            this.tbUp,
            this.tbDown});
			// 
			// tbAdd
			// 
			// 
			// 
			// 
			this.tbAdd.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbAdd.Command.Image")));
			this.tbAdd.Command.Text = resources.GetString("tbAdd.Command.Text");
			this.tbAdd.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			this.tbAdd.ToolBarControls.AddRange(new DockingFrames.ToolBarControl[] {
            this.tbAdd16x16b32,
            this.tbAdd32x32b32,
            this.tbAdd48x48b32,
            this.sep1,
            this.tbOtherFormats,
            this.tbCustomFormat});
			// 
			// tbAdd16x16b32
			// 
			// 
			// 
			// 
			this.tbAdd16x16b32.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbAdd16x16b32.Command.Image")));
			this.tbAdd16x16b32.Command.Text = resources.GetString("tbAdd16x16b32.Command.Text");
			this.tbAdd16x16b32.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tbAdd32x32b32
			// 
			this.tbAdd32x32b32.Appearance = DockingFrames.Appearance.Text;
			// 
			// 
			// 
			this.tbAdd32x32b32.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbAdd32x32b32.Command.Image")));
			this.tbAdd32x32b32.Command.Text = resources.GetString("tbAdd32x32b32.Command.Text");
			this.tbAdd32x32b32.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tbAdd48x48b32
			// 
			this.tbAdd48x48b32.Appearance = DockingFrames.Appearance.Text;
			// 
			// 
			// 
			this.tbAdd48x48b32.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbAdd48x48b32.Command.Image")));
			this.tbAdd48x48b32.Command.Text = resources.GetString("tbAdd48x48b32.Command.Text");
			this.tbAdd48x48b32.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tbOtherFormats
			// 
			// 
			// 
			// 
			this.tbOtherFormats.Command.Text = resources.GetString("tbOtherFormats.Command.Text");
			this.tbOtherFormats.ToolBarControls.AddRange(new DockingFrames.ToolBarControl[] {
            this.tb16x16b8,
            this.tb32x32b8,
            this.tb48x48b8,
            this.toolBarSeparator1,
            this.tb16x16b4,
            this.tb32x32b4,
            this.tb48x48b4,
            this.toolBarSeparator2,
            this.tb16x16b2,
            this.tb32x32b2,
            this.tb48x48b2,
            this.toolBarSeparator3,
            this.tb16x16b1,
            this.tb32x32b1,
            this.tb48x48b1});
			// 
			// tb16x16b8
			// 
			// 
			// 
			// 
			this.tb16x16b8.Command.Text = resources.GetString("tb16x16b8.Command.Text");
			this.tb16x16b8.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb32x32b8
			// 
			// 
			// 
			// 
			this.tb32x32b8.Command.Text = resources.GetString("tb32x32b8.Command.Text");
			this.tb32x32b8.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb48x48b8
			// 
			// 
			// 
			// 
			this.tb48x48b8.Command.Text = resources.GetString("tb48x48b8.Command.Text");
			this.tb48x48b8.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb16x16b4
			// 
			// 
			// 
			// 
			this.tb16x16b4.Command.Text = resources.GetString("tb16x16b4.Command.Text");
			this.tb16x16b4.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb32x32b4
			// 
			// 
			// 
			// 
			this.tb32x32b4.Command.Text = resources.GetString("tb32x32b4.Command.Text");
			this.tb32x32b4.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb48x48b4
			// 
			// 
			// 
			// 
			this.tb48x48b4.Command.Text = resources.GetString("tb48x48b4.Command.Text");
			this.tb48x48b4.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb16x16b2
			// 
			// 
			// 
			// 
			this.tb16x16b2.Command.Text = resources.GetString("tb16x16b2.Command.Text");
			this.tb16x16b2.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb32x32b2
			// 
			// 
			// 
			// 
			this.tb32x32b2.Command.Text = resources.GetString("tb32x32b2.Command.Text");
			this.tb32x32b2.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb48x48b2
			// 
			// 
			// 
			// 
			this.tb48x48b2.Command.Text = resources.GetString("tb48x48b2.Command.Text");
			this.tb48x48b2.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb16x16b1
			// 
			// 
			// 
			// 
			this.tb16x16b1.Command.Text = resources.GetString("tb16x16b1.Command.Text");
			this.tb16x16b1.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb32x32b1
			// 
			// 
			// 
			// 
			this.tb32x32b1.Command.Text = resources.GetString("tb32x32b1.Command.Text");
			this.tb32x32b1.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tb48x48b1
			// 
			// 
			// 
			// 
			this.tb48x48b1.Command.Text = resources.GetString("tb48x48b1.Command.Text");
			this.tb48x48b1.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tbCustomFormat
			// 
			this.tbCustomFormat.Appearance = DockingFrames.Appearance.Text;
			// 
			// 
			// 
			this.tbCustomFormat.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbCustomFormat.Command.Image")));
			this.tbCustomFormat.Command.Text = resources.GetString("tbCustomFormat.Command.Text");
			this.tbCustomFormat.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// tbRemove
			// 
			// 
			// 
			// 
			this.tbRemove.Command.Enabled = false;
			this.tbRemove.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbRemove.Command.Image")));
			this.tbRemove.Command.Text = resources.GetString("tbRemove.Command.Text");
			this.tbRemove.Command.Click += new System.EventHandler(this.tbRemove_Command_Click);
			// 
			// toolBarSpace1
			// 
			this.toolBarSpace1.Length = 56;
			// 
			// tbUp
			// 
			// 
			// 
			// 
			this.tbUp.Command.Enabled = false;
			this.tbUp.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbUp.Command.Image")));
			this.tbUp.Command.Text = resources.GetString("tbUp.Command.Text");
			this.tbUp.Command.Click += new System.EventHandler(this.tbUp_Command_Click);
			// 
			// tbDown
			// 
			// 
			// 
			// 
			this.tbDown.Command.Enabled = false;
			this.tbDown.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbDown.Command.Image")));
			this.tbDown.Command.Text = resources.GetString("tbDown.Command.Text");
			this.tbDown.Command.Click += new System.EventHandler(this.tbDown_Command_Click);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(161)))));
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(161)))));
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// _zoombar
			// 
			this._zoombar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this._zoombar.BackColor = System.Drawing.Color.Transparent;
			this._zoombar.Name = "_zoombar";
			this._zoombar.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
			resources.ApplyResources(this._zoombar, "_zoombar");
			this._zoombar.ZoomChanged += new System.EventHandler(this.ZoomChanged);
			// 
			// lblCursorPosition
			// 
			this.lblCursorPosition.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			resources.ApplyResources(this.lblCursorPosition, "lblCursorPosition");
			this.lblCursorPosition.Name = "lblCursorPosition";
			// 
			// lblSelectionSize
			// 
			this.lblSelectionSize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			resources.ApplyResources(this.lblSelectionSize, "lblSelectionSize");
			this.lblSelectionSize.Name = "lblSelectionSize";
			// 
			// tbsep1
			// 
			this.tbsep1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tbsep1.Name = "tbsep1";
			resources.ApplyResources(this.tbsep1, "tbsep1");
			// 
			// tbsep2
			// 
			this.tbsep2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tbsep2.Name = "tbsep2";
			resources.ApplyResources(this.tbsep2, "tbsep2");
			// 
			// tbAutozoom
			// 
			this.tbAutozoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tbAutozoom.Checked = true;
			this.tbAutozoom.CheckOnClick = true;
			this.tbAutozoom.CheckState = System.Windows.Forms.CheckState.Checked;
			resources.ApplyResources(this.tbAutozoom, "tbAutozoom");
			this.tbAutozoom.Name = "tbAutozoom";
			this.tbAutozoom.CheckedChanged += new System.EventHandler(this.tbAutozoom_CheckedChanged);
			// 
			// tbGrid
			// 
			this.tbGrid.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tbGrid.CheckOnClick = true;
			resources.ApplyResources(this.tbGrid, "tbGrid");
			this.tbGrid.Name = "tbGrid";
			this.tbGrid.CheckedChanged += new System.EventHandler(this.tbGrid_CheckedChanged);
			// 
			// tbsep3
			// 
			this.tbsep3.Name = "tbsep3";
			resources.ApplyResources(this.tbsep3, "tbsep3");
			// 
			// statusControls
			// 
			this.statusControls.Items.Add(this.cbFore);
			this.statusControls.Items.Add(this.cbBack);
			this.statusControls.Items.Add(this.sep3);
			this.statusControls.Items.Add(this._zoombar);
			this.statusControls.Items.Add(this.tbAutozoom);
			this.statusControls.Items.Add(this.tbGrid);
			this.statusControls.Items.Add(this.tbsep2);
			this.statusControls.Items.Add(this.lblSelectionSize);
			this.statusControls.Items.Add(this.lblCursorPosition);
			this.statusControls.Items.Add(this.tbsep1);
			// 
			// cbFore
			// 
			this.cbFore.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.cbFore.Color = System.Drawing.Color.LightGray;
			this.cbFore.Name = "cbFore";
			resources.ApplyResources(this.cbFore, "cbFore");
			this.cbFore.ColorChanged += new System.EventHandler(this.cbFore_ColorChanged);
			// 
			// cbBack
			// 
			this.cbBack.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.cbBack.Name = "cbBack";
			resources.ApplyResources(this.cbBack, "cbBack");
			this.cbBack.ColorChanged += new System.EventHandler(this.cbBack_ColorChanged);
			// 
			// sep3
			// 
			this.sep3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.sep3.Name = "sep3";
			resources.ApplyResources(this.sep3, "sep3");
			// 
			// layerView1
			// 
			this.layerView1.AutoScrollMinSize = new System.Drawing.Size(18, 18);
			this.layerView1.BackgroundColorA = System.Drawing.Color.LightGray;
			this.layerView1.BackgroundColorB = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.layerView1.DisplayMode = VariIconsReload.Components.DisplayMode.Center;
			resources.ApplyResources(this.layerView1, "layerView1");
			this.layerView1.Name = "layerView1";
			this.layerView1.ZoomChanged += new System.EventHandler(this.layerView1_ZoomChanged);
			this.layerView1.SizeChanged += new System.EventHandler(this.layerView1_SizeChanged);
			// 
			// IconEditor
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.layerView1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label2);
			this.Name = "IconEditor";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DockingFrames.ToolBarButton tbUp;
		private System.Windows.Forms.Panel panel1;
		private ControlsEx.ListControls.VDisplayList vDisplayList1;
		private DockingFrames.ToolBar toolBar1;
		private DockingFrames.ToolBarSplitButton tbAdd;
		private DockingFrames.ToolBarButton tbRemove;
		private DockingFrames.ToolBarSpace toolBarSpace1;
		private DockingFrames.ToolBarButton tbDown;
		private VariIconsReload.Components.LayerView layerView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolStripButton tbGrid;
		private System.Windows.Forms.ToolStripButton tbAutozoom;
		private ControlsEx.ZoomBar _zoombar;
		private DockingFrames.ToolBarButton tbAdd16x16b32;
		private DockingFrames.ToolBarButton tbAdd48x48b32;
		private DockingFrames.ToolBarButton tbAdd32x32b32;
		private DockingFrames.ToolBarSeparator sep1;
		private DockingFrames.ToolBarButton tbCustomFormat;
		private DockingFrames.ToolBarDropDownButton tbOtherFormats;
		private DockingFrames.ToolBarButton tb16x16b8;
		private DockingFrames.ToolBarButton tb32x32b8;
		private DockingFrames.ToolBarButton tb48x48b8;
		private DockingFrames.ToolBarSeparator toolBarSeparator1;
		private DockingFrames.ToolBarButton tb16x16b4;
		private DockingFrames.ToolBarButton tb32x32b4;
		private DockingFrames.ToolBarButton tb48x48b4;
		private DockingFrames.ToolBarSeparator toolBarSeparator2;
		private DockingFrames.ToolBarButton tb16x16b2;
		private DockingFrames.ToolBarButton tb32x32b2;
		private DockingFrames.ToolBarButton tb48x48b2;
		private DockingFrames.ToolBarSeparator toolBarSeparator3;
		private DockingFrames.ToolBarButton tb16x16b1;
		private DockingFrames.ToolBarButton tb32x32b1;
		private DockingFrames.ToolBarButton tb48x48b1;
		private System.Windows.Forms.ToolStripLabel lblCursorPosition;
		private System.Windows.Forms.ToolStripLabel lblSelectionSize;
		private System.Windows.Forms.ToolStripSeparator tbsep1;
		private System.Windows.Forms.ToolStripSeparator tbsep2;
		private DockingFrames.ToolBar toolBar2;
		private DockingFrames.ToolBarButton tbOpenImage;
		private DockingFrames.ToolBarButton tbSaveImage;
		private DockingFrames.ToolBarButton tbResize;
		private DockingFrames.ToolBarSpace toolBarSpace2;
		private DockingFrames.ToolBarButton tbPasteImage;
		private System.Windows.Forms.ToolStripSeparator tbsep3;
		private StripControlContainer statusControls;
		private Components.ToolStripColorButton cbFore;
		private Components.ToolStripColorButton cbBack;
		private System.Windows.Forms.ToolStripSeparator sep3;
	}
}
