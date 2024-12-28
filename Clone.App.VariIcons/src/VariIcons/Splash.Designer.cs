namespace VariIcons
{
	partial class Splash
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
			this.SuspendLayout();
			// 
			// Splash
			// 
			this.ApplicationName = "VariIcons";
			this.ClientSize = new System.Drawing.Size(350, 220);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.InitText = "Initializing";
			this.Name = "Splash";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.SubTitle = "created by VariIcons Team";
			this.SubVersion = "REvolve";
			this.ResumeLayout(false);

		}

		#endregion
	}
}
