namespace Little_Disk_Cleaner
{
    partial class Analyze
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Analyze));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelProblems = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textBoxCurrentFile = new System.Windows.Forms.TextBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.timerUpdateFile = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.labelProblems);
            this.groupBox1.Controls.Add(this.buttonStop);
            this.groupBox1.Controls.Add(this.textBoxCurrentFile);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 111);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 32);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(301, 24);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 4;
            // 
            // labelProblems
            // 
            this.labelProblems.AutoSize = true;
            this.labelProblems.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelProblems.Location = new System.Drawing.Point(6, 16);
            this.labelProblems.Name = "labelProblems";
            this.labelProblems.Size = new System.Drawing.Size(67, 13);
            this.labelProblems.TabIndex = 3;
            this.labelProblems.Text = "Files Found: ";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(316, 33);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(57, 23);
            this.buttonStop.TabIndex = 0;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // textBoxCurrentFile
            // 
            this.textBoxCurrentFile.Location = new System.Drawing.Point(6, 62);
            this.textBoxCurrentFile.Multiline = true;
            this.textBoxCurrentFile.Name = "textBoxCurrentFile";
            this.textBoxCurrentFile.ReadOnly = true;
            this.textBoxCurrentFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxCurrentFile.Size = new System.Drawing.Size(367, 40);
            this.textBoxCurrentFile.TabIndex = 1;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "ActiveXComObjects");
            this.imageList.Images.SetKeyName(1, "ApplicationInfo");
            this.imageList.Images.SetKeyName(2, "SystemDrivers");
            this.imageList.Images.SetKeyName(3, "WindowsFonts");
            this.imageList.Images.SetKeyName(4, "WindowsHelpFiles");
            this.imageList.Images.SetKeyName(5, "RecentDocs");
            this.imageList.Images.SetKeyName(6, "ApplicationPaths");
            this.imageList.Images.SetKeyName(7, "SharedDLLs");
            this.imageList.Images.SetKeyName(8, "ApplicationSettings");
            this.imageList.Images.SetKeyName(9, "WindowsSounds");
            this.imageList.Images.SetKeyName(10, "StartupFiles");
            // 
            // timerUpdateFile
            // 
            this.timerUpdateFile.Tick += new System.EventHandler(this.timerUpdateFile_Tick);
            // 
            // Analyze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 119);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Analyze";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Little Disk Cleaner - Analyzing...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Analyze_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelProblems;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textBoxCurrentFile;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Timer timerUpdateFile;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}