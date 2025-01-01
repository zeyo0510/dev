namespace SunPEView.ContentVisualizer.Windows
{
    partial class GenericByteArrayWindow
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
            this.components = new System.ComponentModel.Container();
            this.hexEditBox1 = new HexEditBox.HexEditBox();
            this.SuspendLayout();
            // 
            // hexEditBox1
            // 
            this.hexEditBox1.ByteProvider = null;
            this.hexEditBox1.ContextMenuActive = true;
            this.hexEditBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexEditBox1.Font = new System.Drawing.Font("Courier New", 9F);
            this.hexEditBox1.Location = new System.Drawing.Point(0, 0);
            this.hexEditBox1.Name = "hexEditBox1";
            this.hexEditBox1.ReadOnlyContent = true;
            this.hexEditBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.hexEditBox1.Size = new System.Drawing.Size(650, 185);
            this.hexEditBox1.TabIndex = 0;
            this.hexEditBox1.Text = "";
            // 
            // GenericByteArrayWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 185);
            this.Controls.Add(this.hexEditBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenericByteArrayWindow";
            this.Text = "GenericByteArrayWindow";
            this.ResizeBegin += new System.EventHandler(this.GenericByteArrayWindow_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.GenericByteArrayWindow_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.GenericByteArrayWindow_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private HexEditBox.HexEditBox hexEditBox1;
    }
}