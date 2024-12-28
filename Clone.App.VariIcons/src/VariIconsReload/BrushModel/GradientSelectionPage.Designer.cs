namespace VariIconsReload.BrushModel
{
	partial class GradientSelectionPage
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
		/// Der Inhalt der Methode darf nicht mit dem Code-MainTab geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GradientSelectionPage));
			this.scrAlpha = new ControlsEx.ValueControls.HValueScrollBar();
			this.grpPreview = new System.Windows.Forms.GroupBox();
			this.brushTransform1 = new VariIconsReload.BrushModel.BrushTransform();
			this.toolBar = new DockingFrames.ToolBar();
			this.drpGradientType = new DockingFrames.ToolBarDropDownButton();
			this.tbGradientLinear = new DockingFrames.ToolBarButton();
			this.tbGradientRadial = new DockingFrames.ToolBarButton();
			this.tbGradientAngled = new DockingFrames.ToolBarButton();
			this.toolBarSpace1 = new DockingFrames.ToolBarSpace();
			this.tbForm = new DockingFrames.ToolBarButton();
			this.tbReverse = new DockingFrames.ToolBarButton();
			this.btnRemoveStop = new System.Windows.Forms.Button();
			this.bottomBar = new DockingFrames.ToolBar();
			this.tbAdd = new DockingFrames.ToolBarButton();
			this.toolBarSpace2 = new DockingFrames.ToolBarSpace();
			this.drpMoreOptions = new DockingFrames.ToolBarDropDownButton();
			this.tbClearPresets = new DockingFrames.ToolBarButton();
			this.tbOpenPresets = new DockingFrames.ToolBarButton();
			this.tbSavePresets = new DockingFrames.ToolBarButton();
			this.toolBarSeparator1 = new DockingFrames.ToolBarSeparator();
			this.tbCopyCode = new DockingFrames.ToolBarButton();
			this.sDialog = new System.Windows.Forms.SaveFileDialog();
			this.oDialog = new System.Windows.Forms.OpenFileDialog();
			this.grdList = new DrawingEx.ColorManagement.Gradients.GradientListBox();
			this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.deletePresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.grdEdit = new DrawingEx.ColorManagement.Gradients.GradientEdit();
			this.cDialog = new DrawingEx.ColorManagement.ColorDialogEx();
			this.btnColor = new DrawingEx.ColorManagement.ColorButton();
			this.grpPreview.SuspendLayout();
			this.ctxMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// scrAlpha
			// 
			resources.ApplyResources(this.scrAlpha, "scrAlpha");
			this.scrAlpha.Maximum = 255;
			this.scrAlpha.Name = "scrAlpha";
			this.scrAlpha.ValueChanged += new ControlsEx.ValueControls.ValueChangedEH(scrAlpha_ValueChanged);
			// 
			// grpPreview
			// 
			resources.ApplyResources(this.grpPreview, "grpPreview");
			this.grpPreview.Controls.Add(this.brushTransform1);
			this.grpPreview.Name = "grpPreview";
			this.grpPreview.TabStop = false;
			// 
			// brushTransform1
			// 
			resources.ApplyResources(this.brushTransform1, "brushTransform1");
			this.brushTransform1.Name = "brushTransform1";
			// 
			// toolBar
			// 
			resources.ApplyResources(this.toolBar, "toolBar");
			this.toolBar.ExtensionButtonVisible = false;
			this.toolBar.GripVisible = false;
			this.toolBar.Guid = "d634a690-c6cc-4b26-bd85-604149aa7add";
			this.toolBar.Lines = 1;
			this.toolBar.Name = "toolBar";
			this.toolBar.PreferredLocation = new System.Drawing.Point(0, 13);
			this.toolBar.ToolBarControls.AddRange(new DockingFrames.ToolBarControl[] {
            this.drpGradientType,
            this.toolBarSpace1,
            this.tbForm,
            this.tbReverse});
			// 
			// drpGradientType
			// 
			// 
			// 
			// 
			this.drpGradientType.Command.Image = ((System.Drawing.Image)(resources.GetObject("drpGradientType.Command.Image")));
			this.drpGradientType.Command.Text = resources.GetString("drpGradientType.Command.Text");
			this.drpGradientType.ToolBarControls.AddRange(new DockingFrames.ToolBarControl[] {
            this.tbGradientLinear,
            this.tbGradientRadial,
            this.tbGradientAngled});
			// 
			// tbGradientLinear
			// 
			// 
			// 
			// 
			this.tbGradientLinear.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbGradientLinear.Command.Image")));
			this.tbGradientLinear.Command.Text = resources.GetString("tbGradientLinear.Command.Text");
			this.tbGradientLinear.Command.Click += new System.EventHandler(this.tbGradient_Click);
			// 
			// tbGradientRadial
			// 
			// 
			// 
			// 
			this.tbGradientRadial.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbGradientRadial.Command.Image")));
			this.tbGradientRadial.Command.Text = resources.GetString("tbGradientRadial.Command.Text");
			this.tbGradientRadial.Command.Click += new System.EventHandler(this.tbGradient_Click);
			// 
			// tbGradientAngled
			// 
			// 
			// 
			// 
			this.tbGradientAngled.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbGradientAngled.Command.Image")));
			this.tbGradientAngled.Command.Text = resources.GetString("tbGradientAngled.Command.Text");
			this.tbGradientAngled.Command.Click += new System.EventHandler(this.tbGradient_Click);
			// 
			// toolBarSpace1
			// 
			this.toolBarSpace1.Length = 77;
			// 
			// tbForm
			// 
			// 
			// 
			// 
			this.tbForm.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbForm.Command.Image")));
			this.tbForm.Command.Text = resources.GetString("tbForm.Command.Text");
			this.tbForm.Command.Click += new System.EventHandler(this.tbForm_Command_Click);
			// 
			// tbReverse
			// 
			// 
			// 
			// 
			this.tbReverse.Command.Enabled = false;
			this.tbReverse.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbReverse.Command.Image")));
			this.tbReverse.Command.Text = resources.GetString("tbReverse.Command.Text");
			this.tbReverse.Command.Click += new System.EventHandler(this.tbReverse_Command_Click);
			// 
			// btnRemoveStop
			// 
			resources.ApplyResources(this.btnRemoveStop, "btnRemoveStop");
			this.btnRemoveStop.Name = "btnRemoveStop";
			this.btnRemoveStop.UseVisualStyleBackColor = true;
			this.btnRemoveStop.Click += new System.EventHandler(this.btnRemoveStop_Click);
			// 
			// bottomBar
			// 
			resources.ApplyResources(this.bottomBar, "bottomBar");
			this.bottomBar.ExtensionButtonVisible = false;
			this.bottomBar.GripVisible = false;
			this.bottomBar.Guid = "cbca9e43-ce93-48ec-8d68-9623c86f46bf";
			this.bottomBar.Lines = 1;
			this.bottomBar.Name = "bottomBar";
			this.bottomBar.PreferredLocation = new System.Drawing.Point(0, 389);
			this.bottomBar.ToolBarControls.AddRange(new DockingFrames.ToolBarControl[] {
            this.tbAdd,
            this.toolBarSpace2,
            this.drpMoreOptions});
			// 
			// tbAdd
			// 
			// 
			// 
			// 
			this.tbAdd.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbAdd.Command.Image")));
			this.tbAdd.Command.Text = resources.GetString("tbAdd.Command.Text");
			this.tbAdd.Command.Click += new System.EventHandler(this.tbAdd_Command_Click);
			// 
			// toolBarSpace2
			// 
			this.toolBarSpace2.Length = 99;
			// 
			// drpMoreOptions
			// 
			// 
			// 
			// 
			this.drpMoreOptions.Command.Image = ((System.Drawing.Image)(resources.GetObject("drpMoreOptions.Command.Image")));
			this.drpMoreOptions.Command.Text = resources.GetString("drpMoreOptions.Command.Text");
			this.drpMoreOptions.ToolBarControls.AddRange(new DockingFrames.ToolBarControl[] {
            this.tbClearPresets,
            this.tbOpenPresets,
            this.tbSavePresets,
            this.toolBarSeparator1,
            this.tbCopyCode});
			// 
			// tbClearPresets
			// 
			// 
			// 
			// 
			this.tbClearPresets.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbClearPresets.Command.Image")));
			this.tbClearPresets.Command.Text = resources.GetString("tbClearPresets.Command.Text");
			this.tbClearPresets.Command.Click += new System.EventHandler(this.tbClearPresets_Command_Click);
			// 
			// tbOpenPresets
			// 
			// 
			// 
			// 
			this.tbOpenPresets.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbOpenPresets.Command.Image")));
			this.tbOpenPresets.Command.Text = resources.GetString("tbOpenPresets.Command.Text");
			this.tbOpenPresets.Command.Click += new System.EventHandler(this.tbOpenPresets_Command_Click);
			// 
			// tbSavePresets
			// 
			// 
			// 
			// 
			this.tbSavePresets.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbSavePresets.Command.Image")));
			this.tbSavePresets.Command.Text = resources.GetString("tbSavePresets.Command.Text");
			this.tbSavePresets.Command.Click += new System.EventHandler(this.tbSavePresets_Command_Click);
			// 
			// tbCopyCode
			// 
			// 
			// 
			// 
			this.tbCopyCode.Command.Image = ((System.Drawing.Image)(resources.GetObject("tbCopyCode.Command.Image")));
			this.tbCopyCode.Command.Text = resources.GetString("tbCopyCode.Command.Text");
			this.tbCopyCode.Command.Click += new System.EventHandler(this.tbCopyCode_Command_Click);
			// 
			// sDialog
			// 
			this.sDialog.DefaultExt = "grdx";
			resources.ApplyResources(this.sDialog, "sDialog");
			// 
			// oDialog
			// 
			resources.ApplyResources(this.oDialog, "oDialog");
			// 
			// grdList
			// 
			resources.ApplyResources(this.grdList, "grdList");
			this.grdList.ContextMenuStrip = this.ctxMenu;
			this.grdList.Name = "grdList";
			this.grdList.TabStop = false;
			this.grdList.GradientClicked += new System.EventHandler<DrawingEx.ColorManagement.Gradients.GradientEventArgs>(this.grdList_GradientClicked);
			this.grdList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdList_MouseDown);
			// 
			// ctxMenu
			// 
			this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletePresetToolStripMenuItem});
			this.ctxMenu.Name = "ctxMenu";
			this.ctxMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			resources.ApplyResources(this.ctxMenu, "ctxMenu");
			this.ctxMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.ctxMenu_Closed);
			// 
			// deletePresetToolStripMenuItem
			// 
			this.deletePresetToolStripMenuItem.Name = "deletePresetToolStripMenuItem";
			resources.ApplyResources(this.deletePresetToolStripMenuItem, "deletePresetToolStripMenuItem");
			this.deletePresetToolStripMenuItem.Click += new System.EventHandler(this.deletePresetToolStripMenuItem_Click);
			// 
			// grdEdit
			// 
			resources.ApplyResources(this.grdEdit, "grdEdit");
			this.grdEdit.BackColor = System.Drawing.Color.Transparent;
			this.grdEdit.Name = "grdEdit";
			this.grdEdit.SelectionChanged += new System.EventHandler(this.grdEdit_SelectionChanged);
			// 
			// cDialog
			// 
			this.cDialog.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			// 
			// btnColor
			// 
			this.btnColor.Alpha = ((byte)(255));
			resources.ApplyResources(this.btnColor, "btnColor");
			this.btnColor.DisplayHex = true;
			this.btnColor.Name = "btnColor";
			this.btnColor.UseVisualStyleBackColor = true;
			this.btnColor.ColorChanged += new System.EventHandler(this.btnColor_ColorChanged);
			this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
			// 
			// GradientSelectionPage
			// 
			this.Caption = "Gradient Picker";
			this.Controls.Add(this.btnColor);
			this.Controls.Add(this.btnRemoveStop);
			this.Controls.Add(this.grdEdit);
			this.Controls.Add(this.grdList);
			this.Controls.Add(this.grpPreview);
			this.Controls.Add(this.bottomBar);
			this.Controls.Add(this.toolBar);
			this.Controls.Add(this.scrAlpha);
			resources.ApplyResources(this, "$this");
			this.Name = "GradientSelectionPage";
			this.grpPreview.ResumeLayout(false);
			this.ctxMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ControlsEx.ValueControls.HValueScrollBar scrAlpha;
		private System.Windows.Forms.GroupBox grpPreview;
		private DockingFrames.ToolBar toolBar;
		private DockingFrames.ToolBarSpace toolBarSpace1;
		private DockingFrames.ToolBarButton tbReverse;
		private DrawingEx.ColorManagement.Gradients.GradientEdit grdEdit;
		private System.Windows.Forms.Button btnRemoveStop;
		private DockingFrames.ToolBarDropDownButton drpMoreOptions;
		private DockingFrames.ToolBarButton tbOpenPresets;
		private DockingFrames.ToolBarButton tbSavePresets;
		private DockingFrames.ToolBarSeparator toolBarSeparator1;
		private DockingFrames.ToolBarButton tbCopyCode;
		private DockingFrames.ToolBar bottomBar;
		private DockingFrames.ToolBarButton tbAdd;
		private DockingFrames.ToolBarSpace toolBarSpace2;
		private DockingFrames.ToolBarDropDownButton drpGradientType;
		private DockingFrames.ToolBarButton tbGradientLinear;
		private DockingFrames.ToolBarButton tbGradientRadial;
		private DockingFrames.ToolBarButton tbGradientAngled;
		private DrawingEx.ColorManagement.Gradients.GradientListBox grdList;
		private System.Windows.Forms.SaveFileDialog sDialog;
		private System.Windows.Forms.OpenFileDialog oDialog;
		private DrawingEx.ColorManagement.ColorDialogEx cDialog;
		private DrawingEx.ColorManagement.ColorButton btnColor;
		private BrushModel.BrushTransform brushTransform1;
		private System.Windows.Forms.ContextMenuStrip ctxMenu;
		private System.Windows.Forms.ToolStripMenuItem deletePresetToolStripMenuItem;
		private DockingFrames.ToolBarButton tbForm;
		private DockingFrames.ToolBarButton tbClearPresets;
	}
}
