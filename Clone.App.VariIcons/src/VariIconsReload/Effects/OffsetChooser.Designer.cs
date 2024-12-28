namespace VariIconsReload.Effects
{
	partial class OffsetChooser
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OffsetChooser));
			this.xUpDown = new ControlsEx.ValueControls.ValueUpDown();
			this.yUpDown = new ControlsEx.ValueControls.ValueUpDown();
			this.btnReset = new System.Windows.Forms.Button();
			this.xlabel = new System.Windows.Forms.Label();
			this.ylabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// xUpDown
			// 
			resources.ApplyResources(this.xUpDown, "xUpDown");
			this.xUpDown.Name = "xUpDown";
			this.xUpDown.TrackerOrientation = System.Windows.Forms.Orientation.Vertical;
			this.xUpDown.ValueChanged += new ControlsEx.ValueControls.ValueChangedEH(this.updown_ValueChanged);
			// 
			// yUpDown
			// 
			resources.ApplyResources(this.yUpDown, "yUpDown");
			this.yUpDown.Name = "yUpDown";
			this.yUpDown.TrackerOrientation = System.Windows.Forms.Orientation.Vertical;
			this.yUpDown.ValueChanged += new ControlsEx.ValueControls.ValueChangedEH(this.updown_ValueChanged);
			// 
			// btnReset
			// 
			resources.ApplyResources(this.btnReset, "btnReset");
			this.btnReset.Name = "btnReset";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// xlabel
			// 
			resources.ApplyResources(this.xlabel, "xlabel");
			this.xlabel.Name = "xlabel";
			// 
			// ylabel
			// 
			resources.ApplyResources(this.ylabel, "ylabel");
			this.ylabel.Name = "ylabel";
			// 
			// OffsetChooser
			// 
			this.Controls.Add(this.ylabel);
			this.Controls.Add(this.xlabel);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.yUpDown);
			this.Controls.Add(this.xUpDown);
			this.Name = "OffsetChooser";
			resources.ApplyResources(this, "$this");
			this.ResumeLayout(false);

		}

		#endregion

		private ControlsEx.ValueControls.ValueUpDown xUpDown;
		private ControlsEx.ValueControls.ValueUpDown yUpDown;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Label xlabel;
		private System.Windows.Forms.Label ylabel;
	}
}
