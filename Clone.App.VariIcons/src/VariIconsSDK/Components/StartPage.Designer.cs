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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPage));
			this.lbltitle = new System.Windows.Forms.Label();
			this.line = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbnNew = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			this.lblRecent = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
			this.line.Size = new System.Drawing.Size(1, 219);
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
			this.label1.Location = new System.Drawing.Point(192, 144);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 56);
			this.label1.TabIndex = 3;
			// 
			// tbnNew
			// 
			this.tbnNew.Image = ((System.Drawing.Image)(resources.GetObject("tbnNew.Image")));
			this.tbnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tbnNew.Location = new System.Drawing.Point(232, 232);
			this.tbnNew.Name = "tbnNew";
			this.tbnNew.Size = new System.Drawing.Size(64, 64);
			this.tbnNew.TabIndex = 4;
			this.tbnNew.UseVisualStyleBackColor = true;
			this.tbnNew.Click += new System.EventHandler(this.tbnNew_Click);
			// 
			// btnOpen
			// 
			this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
			this.btnOpen.Location = new System.Drawing.Point(312, 232);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(64, 64);
			this.btnOpen.TabIndex = 4;
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
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
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(8, 168);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(168, 199);
			this.listBox1.TabIndex = 6;
			// 
			// StartPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.Caption = "StartPage";
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.lblRecent);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.tbnNew);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.line);
			this.Controls.Add(this.lbltitle);
			this.Controls.Add(this.pictureBox1);
			this.DoubleBuffered = true;
			this.Name = "StartPage";
			this.Size = new System.Drawing.Size(610, 377);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbltitle;
		private System.Windows.Forms.Label line;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button tbnNew;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Label lblRecent;
		private System.Windows.Forms.ListBox listBox1;

	}
}
