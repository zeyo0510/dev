namespace VariIconsSDK.UI
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			UserDispose();
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this._dockTop = new DockingFrames.DockingArea();
			this.toolBarStandart1 = new VariIconsSDK.UI.ToolBarStandart();
			this._dockMan = new DockingFrames.DockManager(this.components);
			this._dockBottom = new DockingFrames.DockingArea();
			this._dockLeft = new DockingFrames.DockingArea();
			this._dockRight = new DockingFrames.DockingArea();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statuslbl = new System.Windows.Forms.ToolStripStatusLabel();
			this._tabs = new VariIconsSDK.UI.MainTabControl();
			this._dockTop.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// _dockTop
			// 
			this._dockTop.Controls.Add(this.toolBarStandart1);
			resources.ApplyResources(this._dockTop, "_dockTop");
			this._dockTop.DockManager = this._dockMan;
			this._dockTop.Guid = "top";
			this._dockTop.Name = "_dockTop";
			// 
			// toolBarStandart1
			// 
			this.toolBarStandart1.DockManager = this._dockMan;
			this.toolBarStandart1.Guid = "FE63B6FF-1260-4F10-963B-5118FE5A2177";
			this.toolBarStandart1.Lines = 1;
			resources.ApplyResources(this.toolBarStandart1, "toolBarStandart1");
			this.toolBarStandart1.Name = "toolBarStandart1";
			this.toolBarStandart1.PreferredLocation = new System.Drawing.Point(3, 0);
			// 
			// _dockMan
			// 
			this._dockMan.HideFloats = false;
			this._dockMan.OwnerForm = this;
			// 
			// _dockBottom
			// 
			resources.ApplyResources(this._dockBottom, "_dockBottom");
			this._dockBottom.DockManager = this._dockMan;
			this._dockBottom.Guid = "bottom";
			this._dockBottom.Name = "_dockBottom";
			// 
			// _dockLeft
			// 
			resources.ApplyResources(this._dockLeft, "_dockLeft");
			this._dockLeft.DockManager = this._dockMan;
			this._dockLeft.Guid = "left";
			this._dockLeft.Name = "_dockLeft";
			// 
			// _dockRight
			// 
			resources.ApplyResources(this._dockRight, "_dockRight");
			this._dockRight.DockManager = this._dockMan;
			this._dockRight.Guid = "right";
			this._dockRight.Name = "_dockRight";
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslbl});
			this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.Name = "statusStrip";
			// 
			// statuslbl
			// 
			this.statuslbl.Name = "statuslbl";
			resources.ApplyResources(this.statuslbl, "statuslbl");
			// 
			// _tabs
			// 
			resources.ApplyResources(this._tabs, "_tabs");
			this._tabs.Name = "_tabs";
			this._tabs.SelectedPage = null;
			this._tabs.SelectedPageChanged += new ControlsEx.DockingFrames.TabPageEventHandler(this._tabs_SelectedPageChanged);
			this._tabs.BeforeSelectedPageChanged += new System.EventHandler(this._tabs_BeforeSelectedPageChanged);
			this._tabs.PageClosing += new System.EventHandler<ControlsEx.DockingFrames.PageCloseEventArgs>(this._tabs_PageClosing);
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._tabs);
			this.Controls.Add(this._dockRight);
			this.Controls.Add(this._dockLeft);
			this.Controls.Add(this._dockBottom);
			this.Controls.Add(this._dockTop);
			this.Controls.Add(this.statusStrip);
			this.Name = "MainForm";
			this._dockTop.ResumeLayout(false);
			this._dockTop.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DockingFrames.DockingArea _dockTop;
		private DockingFrames.DockingArea _dockRight;
		private DockingFrames.DockingArea _dockLeft;
		private DockingFrames.DockingArea _dockBottom;
		private System.Windows.Forms.StatusStrip statusStrip;
		private VariIconsSDK.UI.ToolBarStandart toolBarStandart1;
		private MainTabControl _tabs;
		private DockingFrames.DockManager _dockMan;
		private System.Windows.Forms.ToolStripStatusLabel statuslbl;
	}
}