namespace VariIconsReload.BrushModel
{
	partial class ColorSelectionPage
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
			UserDispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorSelectionPage));
			this.scrAlpha = new ControlsEx.ValueControls.HValueScrollBar();
			this.cPlane = new DrawingEx.ColorManagement.ColorModels.Selection.ColorSelectionPlane();
			this.cFader = new DrawingEx.ColorManagement.ColorModels.Selection.ColorSelectionFader();
			this.cbtn = new DrawingEx.ColorManagement.ColorButton();
			this.colorDialogEx1 = new DrawingEx.ColorManagement.ColorDialogEx();
			this.palStd = new VariIconsReload.BrushModel.Swatches();
			this.palHistory = new VariIconsReload.BrushModel.Swatches();
			this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuLock = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// scrAlpha
			// 
			resources.ApplyResources(this.scrAlpha, "scrAlpha");
			this.scrAlpha.Maximum = 255;
			this.scrAlpha.Name = "scrAlpha";
			this.scrAlpha.Value = 255;
			this.scrAlpha.ValueChanged += new ControlsEx.ValueControls.ValueChangedEH(this.scrAlpha_ValueChanged);
			// 
			// cPlane
			// 
			resources.ApplyResources(this.cPlane, "cPlane");
			this.cPlane.Name = "cPlane";
			// 
			// cFader
			// 
			resources.ApplyResources(this.cFader, "cFader");
			this.cFader.Name = "cFader";
			// 
			// cbtn
			// 
			this.cbtn.Alpha = ((byte)(255));
			this.cbtn.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			resources.ApplyResources(this.cbtn, "cbtn");
			this.cbtn.Name = "cbtn";
			this.cbtn.UseVisualStyleBackColor = true;
			this.cbtn.ColorChanged += new System.EventHandler(this.cbtnA_ColorChanged);
			this.cbtn.Click += new System.EventHandler(this.cbtn_Click);
			// 
			// colorDialogEx1
			// 
			this.colorDialogEx1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			// 
			// palStd
			// 
			this.palStd.Colors.Add(System.Drawing.Color.Black);
			this.palStd.Colors.Add(System.Drawing.Color.Navy);
			this.palStd.Colors.Add(System.Drawing.Color.Green);
			this.palStd.Colors.Add(System.Drawing.Color.Teal);
			this.palStd.Colors.Add(System.Drawing.Color.Maroon);
			this.palStd.Colors.Add(System.Drawing.Color.Purple);
			this.palStd.Colors.Add(System.Drawing.Color.Olive);
			this.palStd.Colors.Add(System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224))))));
			this.palStd.Colors.Add(System.Drawing.Color.Gray);
			this.palStd.Colors.Add(System.Drawing.Color.Blue);
			this.palStd.Colors.Add(System.Drawing.Color.Lime);
			this.palStd.Colors.Add(System.Drawing.Color.Cyan);
			this.palStd.Colors.Add(System.Drawing.Color.Red);
			this.palStd.Colors.Add(System.Drawing.Color.Fuchsia);
			this.palStd.Colors.Add(System.Drawing.Color.Yellow);
			this.palStd.Colors.Add(System.Drawing.Color.White);
			resources.ApplyResources(this.palStd, "palStd");
			this.palStd.Name = "palStd";
			this.palStd.SelectedColorChanged += new System.EventHandler(this.palStd_SelectedColorChanged);
			// 
			// palHistory
			// 
			this.palHistory.ContextMenuStrip = this.ctxMenu;
			resources.ApplyResources(this.palHistory, "palHistory");
			this.palHistory.Name = "palHistory";
			this.palHistory.SelectedColorChanged += new System.EventHandler(this.palHistory_SelectedColorChanged);
			// 
			// ctxMenu
			// 
			this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLock,
            this.mnuClear});
			this.ctxMenu.Name = "ctxMenu";
			resources.ApplyResources(this.ctxMenu, "ctxMenu");
			// 
			// mnuLock
			// 
			this.mnuLock.CheckOnClick = true;
			resources.ApplyResources(this.mnuLock, "mnuLock");
			this.mnuLock.Name = "mnuLock";
			// 
			// mnuClear
			// 
			resources.ApplyResources(this.mnuClear, "mnuClear");
			this.mnuClear.Name = "mnuClear";
			this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
			// 
			// ColorSelectionPage
			// 
			this.Caption = "Color Picker";
			this.Controls.Add(this.cPlane);
			this.Controls.Add(this.cFader);
			this.Controls.Add(this.palStd);
			this.Controls.Add(this.palHistory);
			this.Controls.Add(this.cbtn);
			this.Controls.Add(this.scrAlpha);
			this.Name = "ColorSelectionPage";
			resources.ApplyResources(this, "$this");
			this.ctxMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DrawingEx.ColorManagement.ColorModels.Selection.ColorSelectionPlane cPlane;
		private DrawingEx.ColorManagement.ColorModels.Selection.ColorSelectionFader cFader;
		private DrawingEx.ColorManagement.ColorButton cbtn;
		private ControlsEx.ValueControls.HValueScrollBar scrAlpha;
		private DrawingEx.ColorManagement.ColorDialogEx colorDialogEx1;
		private VariIconsReload.BrushModel.Swatches palHistory;
		private VariIconsReload.BrushModel.Swatches palStd;
		private System.Windows.Forms.ContextMenuStrip ctxMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuLock;
		private System.Windows.Forms.ToolStripMenuItem mnuClear;

	}
}
