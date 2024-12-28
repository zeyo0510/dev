namespace VariIconsSDK.Pages
{
	partial class StartPage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPage));
			this.lbltitle = new System.Windows.Forms.Label();
			this.line = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblRecent = new System.Windows.Forms.Label();
			this.mainActions = new ControlsEx.ListControls.ActionList();
			this.lstRecent = new ControlsEx.ListControls.ActionList();
			this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuRemove = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.ctxMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbltitle
			// 
			this.lbltitle.AutoSize = true;
			this.lbltitle.BackColor = System.Drawing.Color.Transparent;
			this.lbltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbltitle.Location = new System.Drawing.Point(248, 168);
			this.lbltitle.Name = "lbltitle";
			this.lbltitle.Size = new System.Drawing.Size(295, 24);
			this.lbltitle.TabIndex = 0;
			this.lbltitle.Text = "Welcome to VariIcons Platform";
			// 
			// line
			// 
			this.line.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.line.Location = new System.Drawing.Point(184, 142);
			this.line.Name = "line";
			this.line.Size = new System.Drawing.Size(1, 261);
			this.line.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(610, 144);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.Location = new System.Drawing.Point(192, 152);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 56);
			this.label1.TabIndex = 3;
			// 
			// lblRecent
			// 
			this.lblRecent.AutoSize = true;
			this.lblRecent.Location = new System.Drawing.Point(8, 152);
			this.lblRecent.Name = "lblRecent";
			this.lblRecent.Size = new System.Drawing.Size(66, 13);
			this.lblRecent.TabIndex = 5;
			this.lblRecent.Text = "Recent Files";
			// 
			// mainActions
			// 
			this.mainActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.mainActions.BackColor = System.Drawing.Color.Transparent;
			this.mainActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mainActions.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.mainActions.ItemHeight = 84;
			this.mainActions.Location = new System.Drawing.Point(200, 232);
			this.mainActions.Name = "mainActions";
			this.mainActions.Size = new System.Drawing.Size(400, 176);
			this.mainActions.TabIndex = 8;
			this.mainActions.Text = "actionList1";
			this.mainActions.Scroll += new System.Windows.Forms.ScrollEventHandler(this.mainActions_Scroll);
			// 
			// lstRecent
			// 
			this.lstRecent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.lstRecent.BackColor = System.Drawing.Color.Transparent;
			this.lstRecent.ContextMenuStrip = this.ctxMenu;
			this.lstRecent.ImageAlignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.lstRecent.ItemHeight = 22;
			this.lstRecent.Location = new System.Drawing.Point(8, 176);
			this.lstRecent.Name = "lstRecent";
			this.lstRecent.Size = new System.Drawing.Size(168, 232);
			this.lstRecent.TabIndex = 9;
			this.lstRecent.Text = "actionList1";
			this.lstRecent.ItemClicked += new System.EventHandler<ControlsEx.ListControls.ItemEventArgs>(this.lstRecent_ItemClicked);
			this.lstRecent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstRecent_MouseDown);
			// 
			// ctxMenu
			// 
			this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemove});
			this.ctxMenu.Name = "contextMenuStrip1";
			this.ctxMenu.Size = new System.Drawing.Size(165, 48);
			this.ctxMenu.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.ctxMenu_Closing);
			// 
			// mnuRemove
			// 
			this.mnuRemove.Name = "mnuRemove";
			this.mnuRemove.Size = new System.Drawing.Size(164, 22);
			this.mnuRemove.Text = "Remove from list";
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// StartPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.Caption = "StartPage";
			this.Controls.Add(this.lstRecent);
			this.Controls.Add(this.mainActions);
			this.Controls.Add(this.lblRecent);
			this.Controls.Add(this.line);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lbltitle);
			this.Controls.Add(this.pictureBox1);
			this.DoubleBuffered = true;
			this.Name = "StartPage";
			this.Size = new System.Drawing.Size(610, 419);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ctxMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbltitle;
		private System.Windows.Forms.Label line;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblRecent;
		private ControlsEx.ListControls.ActionList mainActions;
		private ControlsEx.ListControls.ActionList lstRecent;
		private System.Windows.Forms.ContextMenuStrip ctxMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuRemove;

	}
}
