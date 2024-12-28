namespace VariIconsReload.Model
{
	partial class IconLibraryEditor
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
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IconLibraryEditor));
			this.tbLibrary = new DockingFrames.ToolBar();
			this.toolBarLabel1 = new DockingFrames.ToolBarLabel();
			this.tbOpenIcon = new DockingFrames.ToolBarButton();
			this.tbSaveIcon = new DockingFrames.ToolBarButton();
			this.toolBarSeparator1 = new DockingFrames.ToolBarSeparator();
			this.toolBarLabel2 = new DockingFrames.ToolBarLabel();
			this.tbCopyImage = new DockingFrames.ToolBarButton();
			this.tbSaveImage = new DockingFrames.ToolBarButton();
			this.listIcons = new ControlsEx.ListControls.VTableDisplayList();
			this.listImages = new ControlsEx.ListControls.HDisplayList();
			this.SuspendLayout();
			// 
			// tbLibrary
			// 
			this.tbLibrary.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tbLibrary.ExtensionButtonVisible = false;
			this.tbLibrary.GripVisible = false;
			this.tbLibrary.Guid = "af9ba6d6-71ee-464c-91ba-ca22cc8e54a7";
			this.tbLibrary.Lines = 1;
			this.tbLibrary.Location = new System.Drawing.Point(0, 198);
			this.tbLibrary.Name = "tbLibrary";
			this.tbLibrary.PreferredLocation = new System.Drawing.Point(0, 211);
			this.tbLibrary.TabIndex = 0;
			this.tbLibrary.Text = "toolBar1";
			this.tbLibrary.ToolBarControls.AddRange(new DockingFrames.ToolBarControl[] {
            this.toolBarLabel1,
            this.tbOpenIcon,
            this.tbSaveIcon,
            this.toolBarSeparator1,
            this.toolBarLabel2,
            this.tbCopyImage,
            this.tbSaveImage});
			// 
			// toolBarLabel1
			// 
			this.toolBarLabel1.Appearance = DockingFrames.Appearance.Text;
			// 
			// 
			// 
			this.toolBarLabel1.Command.Text = "Icon:";
			// 
			// tbOpenIcon
			// 
			// 
			// 
			// 
			this.tbOpenIcon.Command.Enabled = false;
			this.tbOpenIcon.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbOpenIcon.Command.Image")));
			this.tbOpenIcon.Command.Text = "Open Icon...";
			this.tbOpenIcon.Command.Click += new System.EventHandler(this.tbOpenIcon_Command_Click);
			// 
			// tbSaveIcon
			// 
			// 
			// 
			// 
			this.tbSaveIcon.Command.Enabled = false;
			this.tbSaveIcon.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbSaveIcon.Command.Image")));
			this.tbSaveIcon.Command.Text = "Save Icon...";
			this.tbSaveIcon.Command.Click += new System.EventHandler(this.tbSaveIcon_Command_Click);
			// 
			// toolBarLabel2
			// 
			this.toolBarLabel2.Appearance = DockingFrames.Appearance.Text;
			// 
			// 
			// 
			this.toolBarLabel2.Command.Text = "Image:";
			// 
			// tbCopyImage
			// 
			// 
			// 
			// 
			this.tbCopyImage.Command.Enabled = false;
			this.tbCopyImage.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbCopyImage.Command.Image")));
			this.tbCopyImage.Command.Text = "Copy Image";
			this.tbCopyImage.Command.Click += new System.EventHandler(this.tbCopyImage_Command_Click);
			// 
			// tbSaveImage
			// 
			// 
			// 
			// 
			this.tbSaveImage.Command.Enabled = false;
			this.tbSaveImage.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbSaveImage.Command.Image")));
			this.tbSaveImage.Command.Text = "Save Image...";
			this.tbSaveImage.Command.Click += new System.EventHandler(this.tbSaveImage_Command_Click);
			// 
			// listIcons
			// 
			this.listIcons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listIcons.FieldSize = new System.Drawing.Size(500, 128);
			this.listIcons.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.listIcons.Location = new System.Drawing.Point(0, 0);
			this.listIcons.Name = "listIcons";
			this.listIcons.Size = new System.Drawing.Size(524, 198);
			this.listIcons.TabIndex = 1;
			this.listIcons.Text = "vTableDisplayList1";
			this.listIcons.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
			this.listIcons.SelectionChanged += new System.EventHandler(this.listIcons_SelectionChanged);
			// 
			// listImages
			// 
			this.listImages.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.listImages.Location = new System.Drawing.Point(0, 224);
			this.listImages.Name = "listImages";
			this.listImages.Size = new System.Drawing.Size(524, 147);
			this.listImages.TabIndex = 2;
			this.listImages.Text = "hDisplayList1";
			this.listImages.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
			this.listImages.SelectionChanged += new System.EventHandler(this.listImages_SelectionChanged);
			// 
			// IconLibraryEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.listIcons);
			this.Controls.Add(this.tbLibrary);
			this.Controls.Add(this.listImages);
			this.Name = "IconLibraryEditor";
			this.Size = new System.Drawing.Size(524, 371);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DockingFrames.ToolBar tbLibrary;
		private DockingFrames.ToolBarButton tbOpenIcon;
		private ControlsEx.ListControls.VTableDisplayList listIcons;
		private ControlsEx.ListControls.HDisplayList listImages;
		private DockingFrames.ToolBarLabel toolBarLabel1;
		private DockingFrames.ToolBarButton tbSaveIcon;
		private DockingFrames.ToolBarSeparator toolBarSeparator1;
		private DockingFrames.ToolBarLabel toolBarLabel2;
		private DockingFrames.ToolBarButton tbCopyImage;
		private DockingFrames.ToolBarButton tbSaveImage;
	}
}
