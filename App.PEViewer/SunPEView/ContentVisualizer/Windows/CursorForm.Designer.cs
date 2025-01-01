namespace SunPEView.ContentVisualizer.Windows
{
    partial class CursorForm
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
            this.saveCursorButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveCursorButton
            // 
            this.saveCursorButton.Location = new System.Drawing.Point(12, 12);
            this.saveCursorButton.Name = "saveCursorButton";
            this.saveCursorButton.Size = new System.Drawing.Size(146, 23);
            this.saveCursorButton.TabIndex = 1;
            this.saveCursorButton.Text = "Save Cursor to File...";
            this.saveCursorButton.UseVisualStyleBackColor = true;
            this.saveCursorButton.Click += new System.EventHandler(this.saveCursorButton_Click);
            // 
            // CursorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.saveCursorButton);
            this.Name = "CursorForm";
            this.Text = "CursorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveCursorButton;
    }
}