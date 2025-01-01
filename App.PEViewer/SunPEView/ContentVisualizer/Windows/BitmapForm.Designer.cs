namespace SunPEView.ContentVisualizer.Windows
{
    partial class BitmapForm
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
            this.saveIconButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveIconButton
            // 
            this.saveIconButton.Location = new System.Drawing.Point(12, 12);
            this.saveIconButton.Name = "saveIconButton";
            this.saveIconButton.Size = new System.Drawing.Size(146, 23);
            this.saveIconButton.TabIndex = 1;
            this.saveIconButton.Text = "Save Bitmap to File...";
            this.saveIconButton.UseVisualStyleBackColor = true;
            this.saveIconButton.Click += new System.EventHandler(this.saveIconButton_Click);
            // 
            // BitmapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.saveIconButton);
            this.Name = "BitmapForm";
            this.Text = "BitmapForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveIconButton;
    }
}