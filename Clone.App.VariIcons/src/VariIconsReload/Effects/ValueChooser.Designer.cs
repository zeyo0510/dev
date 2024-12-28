namespace VariIconsReload.Effects
{
	partial class ValueChooser
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

		#region Vom Komponenten-Designer generierter Code

		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValueChooser));
			this.track = new System.Windows.Forms.TrackBar();
			this.upDown = new ControlsEx.ValueControls.ValueUpDown();
			this.reset = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.track)).BeginInit();
			this.SuspendLayout();
			// 
			// track
			// 
			this.track.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.track.Location = new System.Drawing.Point(8, 16);
			this.track.Maximum = 100;
			this.track.Name = "track";
			this.track.Size = new System.Drawing.Size(207, 45);
			this.track.TabIndex = 0;
			this.track.TickFrequency = 10;
			this.track.ValueChanged += new System.EventHandler(this.track_ValueChanged);
			// 
			// upDown
			// 
			this.upDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.upDown.Location = new System.Drawing.Point(223, 17);
			this.upDown.Name = "upDown";
			this.upDown.Size = new System.Drawing.Size(72, 25);
			this.upDown.TabIndex = 1;
			this.upDown.Text = "valueUpDown1";
			this.upDown.TrackerOrientation = System.Windows.Forms.Orientation.Vertical;
			this.upDown.ValueChanged += new ControlsEx.ValueControls.ValueChangedEH(this.upDown_ValueChanged);
			// 
			// reset
			// 
			this.reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.reset.Image = ((System.Drawing.Image)(resources.GetObject("reset.Image")));
			this.reset.Location = new System.Drawing.Point(303, 16);
			this.reset.Name = "reset";
			this.reset.Size = new System.Drawing.Size(24, 24);
			this.reset.TabIndex = 2;
			this.reset.UseVisualStyleBackColor = true;
			this.reset.Click += new System.EventHandler(this.reset_Click);
			// 
			// ValueChooser
			// 
			this.Controls.Add(this.reset);
			this.Controls.Add(this.upDown);
			this.Controls.Add(this.track);
			this.Name = "ValueChooser";
			this.Size = new System.Drawing.Size(330, 70);
			((System.ComponentModel.ISupportInitialize)(this.track)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TrackBar track;
		private ControlsEx.ValueControls.ValueUpDown upDown;
		private System.Windows.Forms.Button reset;
	}
}
