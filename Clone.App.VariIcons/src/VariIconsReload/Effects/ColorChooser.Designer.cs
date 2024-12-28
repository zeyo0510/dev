namespace VariIconsReload.Effects
{
	partial class ColorChooser
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorChooser));
			this.cbtn = new DrawingEx.ColorManagement.ColorButton();
			this.scrAlpha = new ControlsEx.ValueControls.HValueScrollBar();
			this.cDialog = new DrawingEx.ColorManagement.ColorDialogEx();
			this.btnReset = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cbtn
			// 
			this.cbtn.Alpha = ((byte)(255));
			this.cbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cbtn.Location = new System.Drawing.Point(96, 16);
			this.cbtn.Name = "cbtn";
			this.cbtn.Size = new System.Drawing.Size(128, 32);
			this.cbtn.TabIndex = 0;
			this.cbtn.UseVisualStyleBackColor = true;
			this.cbtn.ColorChanged += new System.EventHandler(this.cbtn_ColorChanged);
			this.cbtn.Click += new System.EventHandler(this.cbtn_Click);
			// 
			// scrAlpha
			// 
			this.scrAlpha.Location = new System.Drawing.Point(96, 48);
			this.scrAlpha.Maximum = 255;
			this.scrAlpha.Name = "scrAlpha";
			this.scrAlpha.Size = new System.Drawing.Size(128, 16);
			this.scrAlpha.TabIndex = 1;
			this.scrAlpha.Text = "hValueScrollBar1";
			this.scrAlpha.ValueChanged += new ControlsEx.ValueControls.ValueChangedEH(this.scrAlpha_ValueChanged);
			// 
			// cDialog
			// 
			this.cDialog.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
			this.btnReset.Location = new System.Drawing.Point(303, 16);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(24, 24);
			this.btnReset.TabIndex = 2;
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// ColorChooser
			// 
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.scrAlpha);
			this.Controls.Add(this.cbtn);
			this.Name = "ColorChooser";
			this.Size = new System.Drawing.Size(330, 74);
			this.ResumeLayout(false);

		}

		#endregion

		private DrawingEx.ColorManagement.ColorButton cbtn;
		private ControlsEx.ValueControls.HValueScrollBar scrAlpha;
		private DrawingEx.ColorManagement.ColorDialogEx cDialog;
		private System.Windows.Forms.Button btnReset;
	}
}
