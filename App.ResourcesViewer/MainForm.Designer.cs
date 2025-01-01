/*
 * Copyright (C)  2011  Axel Kesseler
 * 
 * This software is free and you can use it for any purpose. Furthermore, 
 * you are free to copy, to modify and/or to redistribute this software.
 * 
 * In addition, this software is distributed in the hope that it will be 
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 */

namespace plexdata.ResourcesViewer
{
    partial class MainForm
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
            this.panPlaceholder = new System.Windows.Forms.Label();
            this.tbMain = new System.Windows.Forms.ToolStrip();
            this.tbbQuit = new System.Windows.Forms.ToolStripButton();
            this.tbbOpen = new System.Windows.Forms.ToolStripButton();
            this.tbbClose = new System.Windows.Forms.ToolStripButton();
            this.tbbExport = new System.Windows.Forms.ToolStripButton();
            this.tbbSettings = new System.Windows.Forms.ToolStripButton();
            this.tbbAbout = new System.Windows.Forms.ToolStripButton();
            this.sbMain = new System.Windows.Forms.StatusStrip();
            this.sblFilename = new System.Windows.Forms.ToolStripStatusLabel();
            this.splLayouter = new System.Windows.Forms.SplitContainer();
            this.trvResources = new plexdata.ResourcesViewer.ResourceTreeView();
            this.tbMain.SuspendLayout();
            this.sbMain.SuspendLayout();
            this.splLayouter.Panel1.SuspendLayout();
            this.splLayouter.Panel2.SuspendLayout();
            this.splLayouter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panPlaceholder
            // 
            this.panPlaceholder.BackColor = System.Drawing.Color.White;
            this.panPlaceholder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPlaceholder.Location = new System.Drawing.Point(0, 0);
            this.panPlaceholder.Name = "panPlaceholder";
            this.panPlaceholder.Size = new System.Drawing.Size(605, 395);
            this.panPlaceholder.TabIndex = 0;
            this.panPlaceholder.Text = "Nothing";
            this.panPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbMain
            // 
            this.tbMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.tbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbQuit,
            this.tbbOpen,
            this.tbbClose,
            this.tbbExport,
            this.tbbSettings,
            this.tbbAbout});
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.Size = new System.Drawing.Size(834, 38);
            this.tbMain.TabIndex = 0;
            this.tbMain.Text = "Toolbar";
            // 
            // tbbQuit
            // 
            this.tbbQuit.Image = global::Zeyo.ResourcesViewer.Properties.Resources.quit;
            this.tbbQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbQuit.Name = "tbbQuit";
            this.tbbQuit.Size = new System.Drawing.Size(34, 35);
            this.tbbQuit.Text = "&Quit";
            this.tbbQuit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbQuit.ToolTipText = "Leave this nice application.";
            this.tbbQuit.Click += new System.EventHandler(this.ToolbarQuit_Click);
            // 
            // tbbOpen
            // 
            this.tbbOpen.Image = global::Zeyo.ResourcesViewer.Properties.Resources.open;
            this.tbbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbOpen.Name = "tbbOpen";
            this.tbbOpen.Size = new System.Drawing.Size(40, 35);
            this.tbbOpen.Text = "&Open";
            this.tbbOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbOpen.ToolTipText = "Open another binary resource.";
            this.tbbOpen.Click += new System.EventHandler(this.ToolbarOpen_Click);
            // 
            // tbbClose
            // 
            this.tbbClose.Image = global::Zeyo.ResourcesViewer.Properties.Resources.close;
            this.tbbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbClose.Name = "tbbClose";
            this.tbbClose.Size = new System.Drawing.Size(40, 35);
            this.tbbClose.Text = "&Close";
            this.tbbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbClose.ToolTipText = "Close current binary resource.";
            this.tbbClose.Click += new System.EventHandler(this.ToolbarClose_Click);
            // 
            // tbbExport
            // 
            this.tbbExport.Image = global::Zeyo.ResourcesViewer.Properties.Resources.export;
            this.tbbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbExport.Name = "tbbExport";
            this.tbbExport.Size = new System.Drawing.Size(44, 35);
            this.tbbExport.Text = "E&xport";
            this.tbbExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbExport.ToolTipText = "Export current selection.";
            this.tbbExport.Click += new System.EventHandler(this.ToolbarExport_Click);
            // 
            // tbbSettings
            // 
            this.tbbSettings.Image = global::Zeyo.ResourcesViewer.Properties.Resources.options;
            this.tbbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSettings.Name = "tbbSettings";
            this.tbbSettings.Size = new System.Drawing.Size(53, 35);
            this.tbbSettings.Text = "&Settings";
            this.tbbSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbSettings.ToolTipText = "Open Settings dialog box.";
            this.tbbSettings.Click += new System.EventHandler(this.ToolbarSettings_Click);
            // 
            // tbbAbout
            // 
            this.tbbAbout.Image = global::Zeyo.ResourcesViewer.Properties.Resources.about;
            this.tbbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbAbout.Name = "tbbAbout";
            this.tbbAbout.Size = new System.Drawing.Size(44, 35);
            this.tbbAbout.Text = "&About";
            this.tbbAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbbAbout.ToolTipText = "About this application.";
            this.tbbAbout.Click += new System.EventHandler(this.ToolbarAbout_Click);
            // 
            // sbMain
            // 
            this.sbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sblFilename});
            this.sbMain.Location = new System.Drawing.Point(0, 440);
            this.sbMain.Name = "sbMain";
            this.sbMain.Size = new System.Drawing.Size(834, 22);
            this.sbMain.TabIndex = 2;
            this.sbMain.Text = "Status";
            // 
            // sblFilename
            // 
            this.sblFilename.Name = "sblFilename";
            this.sblFilename.Size = new System.Drawing.Size(22, 17);
            this.sblFilename.Text = "???";
            // 
            // splLayouter
            // 
            this.splLayouter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splLayouter.Location = new System.Drawing.Point(13, 42);
            this.splLayouter.Name = "splLayouter";
            // 
            // splLayouter.Panel1
            // 
            this.splLayouter.Panel1.Controls.Add(this.trvResources);
            // 
            // splLayouter.Panel2
            // 
            this.splLayouter.Panel2.Controls.Add(this.panPlaceholder);
            this.splLayouter.Size = new System.Drawing.Size(809, 395);
            this.splLayouter.SplitterDistance = 200;
            this.splLayouter.TabIndex = 1;
            this.splLayouter.TabStop = false;
            // 
            // trvResources
            // 
            this.trvResources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvResources.HideSelection = false;
            this.trvResources.ImageIndex = 0;
            this.trvResources.Location = new System.Drawing.Point(0, 0);
            this.trvResources.Name = "trvResources";
            this.trvResources.SelectedImageIndex = 0;
            this.trvResources.Size = new System.Drawing.Size(200, 395);
            this.trvResources.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(834, 462);
            this.Controls.Add(this.splLayouter);
            this.Controls.Add(this.sbMain);
            this.Controls.Add(this.tbMain);
            this.MinimumSize = new System.Drawing.Size(850, 500);
            this.Name = "MainForm";
            this.Text = "Resources Viewer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.tbMain.ResumeLayout(false);
            this.tbMain.PerformLayout();
            this.sbMain.ResumeLayout(false);
            this.sbMain.PerformLayout();
            this.splLayouter.Panel1.ResumeLayout(false);
            this.splLayouter.Panel2.ResumeLayout(false);
            this.splLayouter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label panPlaceholder;
        private System.Windows.Forms.ToolStrip tbMain;
        private System.Windows.Forms.StatusStrip sbMain;
        private System.Windows.Forms.ToolStripStatusLabel sblFilename;
        private System.Windows.Forms.ToolStripButton tbbOpen;
        private System.Windows.Forms.ToolStripButton tbbClose;
        private System.Windows.Forms.ToolStripButton tbbAbout;
        private System.Windows.Forms.ToolStripButton tbbExport;
        private System.Windows.Forms.ToolStripButton tbbSettings;
        private System.Windows.Forms.ToolStripButton tbbQuit;
        private System.Windows.Forms.SplitContainer splLayouter;
        private plexdata.ResourcesViewer.ResourceTreeView trvResources;
    }
}

