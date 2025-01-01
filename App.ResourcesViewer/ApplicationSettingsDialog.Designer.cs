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
    partial class ApplicationSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationSettingsDialog));
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.btnClearLastFilenames = new System.Windows.Forms.Button();
            this.lblClearLastFilenames = new System.Windows.Forms.Label();
            this.chkAutoLoadLastFiles = new System.Windows.Forms.CheckBox();
            this.cmbBinLineLength = new System.Windows.Forms.ComboBox();
            this.lblBinLineLength = new System.Windows.Forms.Label();
            this.grpColors = new System.Windows.Forms.GroupBox();
            this.lblImageBorderColor = new System.Windows.Forms.Label();
            this.btnImageBorderColor = new plexdata.Controls.ColorButton();
            this.lblCursorBKColor = new System.Windows.Forms.Label();
            this.lblViewFGColor = new System.Windows.Forms.Label();
            this.lblViewBKColor = new System.Windows.Forms.Label();
            this.btnCursorBKColor = new plexdata.Controls.ColorButton();
            this.btnViewFGColor = new plexdata.Controls.ColorButton();
            this.btnViewBKColor = new plexdata.Controls.ColorButton();
            this.ttHelper = new System.Windows.Forms.ToolTip(this.components);
            this.btnDefaults = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblExportPath = new System.Windows.Forms.Label();
            this.btnExportPath = new System.Windows.Forms.Button();
            this.grpGeneral.SuspendLayout();
            this.grpColors.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this.btnExportPath);
            this.grpGeneral.Controls.Add(this.btnClearLastFilenames);
            this.grpGeneral.Controls.Add(this.lblExportPath);
            this.grpGeneral.Controls.Add(this.lblClearLastFilenames);
            this.grpGeneral.Controls.Add(this.chkAutoLoadLastFiles);
            this.grpGeneral.Controls.Add(this.cmbBinLineLength);
            this.grpGeneral.Controls.Add(this.lblBinLineLength);
            this.grpGeneral.Location = new System.Drawing.Point(12, 12);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(261, 127);
            this.grpGeneral.TabIndex = 0;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // btnClearLastFilenames
            // 
            this.btnClearLastFilenames.Image = global::Zeyo.ResourcesViewer.Properties.Resources.delete;
            this.btnClearLastFilenames.Location = new System.Drawing.Point(180, 75);
            this.btnClearLastFilenames.Name = "btnClearLastFilenames";
            this.btnClearLastFilenames.Size = new System.Drawing.Size(25, 23);
            this.btnClearLastFilenames.TabIndex = 5;
            this.ttHelper.SetToolTip(this.btnClearLastFilenames, resources.GetString("btnClearLastFilenames.ToolTip"));
            this.btnClearLastFilenames.UseVisualStyleBackColor = true;
            this.btnClearLastFilenames.Click += new System.EventHandler(this.ClearLastFilenamesButton_Click);
            // 
            // lblClearLastFilenames
            // 
            this.lblClearLastFilenames.AutoSize = true;
            this.lblClearLastFilenames.Location = new System.Drawing.Point(6, 80);
            this.lblClearLastFilenames.Name = "lblClearLastFilenames";
            this.lblClearLastFilenames.Size = new System.Drawing.Size(130, 13);
            this.lblClearLastFilenames.TabIndex = 4;
            this.lblClearLastFilenames.Text = "Clear list of last &used Files.";
            // 
            // chkAutoLoadLastFiles
            // 
            this.chkAutoLoadLastFiles.AutoSize = true;
            this.chkAutoLoadLastFiles.Location = new System.Drawing.Point(9, 104);
            this.chkAutoLoadLastFiles.Name = "chkAutoLoadLastFiles";
            this.chkAutoLoadLastFiles.Size = new System.Drawing.Size(157, 17);
            this.chkAutoLoadLastFiles.TabIndex = 6;
            this.chkAutoLoadLastFiles.Text = "Load last files &automatically.";
            this.ttHelper.SetToolTip(this.chkAutoLoadLastFiles, "Uncheck this option to avoid loading of last loaded files.");
            this.chkAutoLoadLastFiles.UseVisualStyleBackColor = true;
            // 
            // cmbBinLineLength
            // 
            this.cmbBinLineLength.FormattingEnabled = true;
            this.cmbBinLineLength.Items.AddRange(new object[] {
            "8",
            "16",
            "32"});
            this.cmbBinLineLength.Location = new System.Drawing.Point(180, 19);
            this.cmbBinLineLength.Name = "cmbBinLineLength";
            this.cmbBinLineLength.Size = new System.Drawing.Size(75, 21);
            this.cmbBinLineLength.TabIndex = 1;
            this.ttHelper.SetToolTip(this.cmbBinLineLength, "Provide a number of bytes to be used as line length of binary output screens.");
            // 
            // lblBinLineLength
            // 
            this.lblBinLineLength.AutoSize = true;
            this.lblBinLineLength.Location = new System.Drawing.Point(6, 22);
            this.lblBinLineLength.Name = "lblBinLineLength";
            this.lblBinLineLength.Size = new System.Drawing.Size(121, 13);
            this.lblBinLineLength.TabIndex = 0;
            this.lblBinLineLength.Text = "Binary Data &Line Length";
            // 
            // grpColors
            // 
            this.grpColors.Controls.Add(this.lblImageBorderColor);
            this.grpColors.Controls.Add(this.btnImageBorderColor);
            this.grpColors.Controls.Add(this.lblCursorBKColor);
            this.grpColors.Controls.Add(this.lblViewFGColor);
            this.grpColors.Controls.Add(this.lblViewBKColor);
            this.grpColors.Controls.Add(this.btnCursorBKColor);
            this.grpColors.Controls.Add(this.btnViewFGColor);
            this.grpColors.Controls.Add(this.btnViewBKColor);
            this.grpColors.Location = new System.Drawing.Point(12, 145);
            this.grpColors.Name = "grpColors";
            this.grpColors.Size = new System.Drawing.Size(261, 135);
            this.grpColors.TabIndex = 1;
            this.grpColors.TabStop = false;
            this.grpColors.Text = "Colors";
            // 
            // lblImageBorderColor
            // 
            this.lblImageBorderColor.AutoSize = true;
            this.lblImageBorderColor.Location = new System.Drawing.Point(6, 111);
            this.lblImageBorderColor.Name = "lblImageBorderColor";
            this.lblImageBorderColor.Size = new System.Drawing.Size(137, 13);
            this.lblImageBorderColor.TabIndex = 6;
            this.lblImageBorderColor.Text = "&General Image Border Color";
            // 
            // btnImageBorderColor
            // 
            this.btnImageBorderColor.Border = true;
            this.btnImageBorderColor.Color = System.Drawing.Color.White;
            this.btnImageBorderColor.Location = new System.Drawing.Point(180, 106);
            this.btnImageBorderColor.Name = "btnImageBorderColor";
            this.btnImageBorderColor.Padding = new System.Windows.Forms.Padding(5);
            this.btnImageBorderColor.Size = new System.Drawing.Size(75, 23);
            this.btnImageBorderColor.TabIndex = 7;
            this.btnImageBorderColor.Text = "";
            this.ttHelper.SetToolTip(this.btnImageBorderColor, "Choose a color to be used as border for displayed icons and cursors.");
            this.btnImageBorderColor.UseVisualStyleBackColor = true;
            this.btnImageBorderColor.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // lblCursorBKColor
            // 
            this.lblCursorBKColor.AutoSize = true;
            this.lblCursorBKColor.Location = new System.Drawing.Point(6, 82);
            this.lblCursorBKColor.Name = "lblCursorBKColor";
            this.lblCursorBKColor.Size = new System.Drawing.Size(157, 13);
            this.lblCursorBKColor.TabIndex = 4;
            this.lblCursorBKColor.Text = "&Cursor Image Background Color";
            // 
            // lblViewFGColor
            // 
            this.lblViewFGColor.AutoSize = true;
            this.lblViewFGColor.Location = new System.Drawing.Point(6, 53);
            this.lblViewFGColor.Name = "lblViewFGColor";
            this.lblViewFGColor.Size = new System.Drawing.Size(140, 13);
            this.lblViewFGColor.TabIndex = 2;
            this.lblViewFGColor.Text = "Data View &Foreground Color";
            // 
            // lblViewBKColor
            // 
            this.lblViewBKColor.AutoSize = true;
            this.lblViewBKColor.Location = new System.Drawing.Point(6, 24);
            this.lblViewBKColor.Name = "lblViewBKColor";
            this.lblViewBKColor.Size = new System.Drawing.Size(144, 13);
            this.lblViewBKColor.TabIndex = 0;
            this.lblViewBKColor.Text = "Data View &Background Color";
            // 
            // btnCursorBKColor
            // 
            this.btnCursorBKColor.Border = true;
            this.btnCursorBKColor.Color = System.Drawing.Color.White;
            this.btnCursorBKColor.Location = new System.Drawing.Point(180, 77);
            this.btnCursorBKColor.Name = "btnCursorBKColor";
            this.btnCursorBKColor.Padding = new System.Windows.Forms.Padding(5);
            this.btnCursorBKColor.Size = new System.Drawing.Size(75, 23);
            this.btnCursorBKColor.TabIndex = 5;
            this.btnCursorBKColor.Text = "";
            this.ttHelper.SetToolTip(this.btnCursorBKColor, "Choose a color to be used as background for statically displayed cursors.");
            this.btnCursorBKColor.UseVisualStyleBackColor = true;
            this.btnCursorBKColor.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // btnViewFGColor
            // 
            this.btnViewFGColor.Border = true;
            this.btnViewFGColor.Color = System.Drawing.Color.White;
            this.btnViewFGColor.Location = new System.Drawing.Point(180, 48);
            this.btnViewFGColor.Name = "btnViewFGColor";
            this.btnViewFGColor.Padding = new System.Windows.Forms.Padding(5);
            this.btnViewFGColor.Size = new System.Drawing.Size(75, 23);
            this.btnViewFGColor.TabIndex = 3;
            this.btnViewFGColor.Text = "";
            this.ttHelper.SetToolTip(this.btnViewFGColor, "Choose a color to be used as foreground (e.g. text color) for each data output sc" +
                    "reen.");
            this.btnViewFGColor.UseVisualStyleBackColor = true;
            this.btnViewFGColor.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // btnViewBKColor
            // 
            this.btnViewBKColor.Border = true;
            this.btnViewBKColor.Color = System.Drawing.Color.White;
            this.btnViewBKColor.Location = new System.Drawing.Point(180, 19);
            this.btnViewBKColor.Name = "btnViewBKColor";
            this.btnViewBKColor.Padding = new System.Windows.Forms.Padding(5);
            this.btnViewBKColor.Size = new System.Drawing.Size(75, 23);
            this.btnViewBKColor.TabIndex = 1;
            this.btnViewBKColor.Text = "";
            this.ttHelper.SetToolTip(this.btnViewBKColor, "Choose a color to be used as background for each data output screen.");
            this.btnViewBKColor.UseVisualStyleBackColor = true;
            this.btnViewBKColor.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // ttHelper
            // 
            this.ttHelper.AutoPopDelay = 10000;
            this.ttHelper.InitialDelay = 500;
            this.ttHelper.ReshowDelay = 100;
            // 
            // btnDefaults
            // 
            this.btnDefaults.Image = ((System.Drawing.Image)(resources.GetObject("btnDefaults.Image")));
            this.btnDefaults.Location = new System.Drawing.Point(12, 286);
            this.btnDefaults.Name = "btnDefaults";
            this.btnDefaults.Size = new System.Drawing.Size(75, 23);
            this.btnDefaults.TabIndex = 2;
            this.btnDefaults.Text = "&Defaults";
            this.btnDefaults.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttHelper.SetToolTip(this.btnDefaults, "Restore default values for each option and reset currently used settings.");
            this.btnDefaults.UseVisualStyleBackColor = true;
            this.btnDefaults.Click += new System.EventHandler(this.DefaultsButton_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(198, 286);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.Location = new System.Drawing.Point(117, 286);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&OK";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // lblExportPath
            // 
            this.lblExportPath.AutoSize = true;
            this.lblExportPath.Location = new System.Drawing.Point(6, 51);
            this.lblExportPath.Name = "lblExportPath";
            this.lblExportPath.Size = new System.Drawing.Size(109, 13);
            this.lblExportPath.TabIndex = 2;
            this.lblExportPath.Text = "Clear last e&xport path.";
            // 
            // btnExportPath
            // 
            this.btnExportPath.Image = global::Zeyo.ResourcesViewer.Properties.Resources.delete;
            this.btnExportPath.Location = new System.Drawing.Point(180, 46);
            this.btnExportPath.Name = "btnExportPath";
            this.btnExportPath.Size = new System.Drawing.Size(25, 23);
            this.btnExportPath.TabIndex = 3;
            this.ttHelper.SetToolTip(this.btnExportPath, "Click this button to clear last known file export path.");
            this.btnExportPath.UseVisualStyleBackColor = true;
            this.btnExportPath.Click += new System.EventHandler(this.ClearExportPathButton_Click);
            // 
            // ApplicationSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(285, 321);
            this.Controls.Add(this.grpColors);
            this.Controls.Add(this.grpGeneral);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnDefaults);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationSettingsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.ApplicationSettingsDialog_Load);
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpColors.ResumeLayout(false);
            this.grpColors.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.GroupBox grpColors;
        private System.Windows.Forms.Label lblCursorBKColor;
        private System.Windows.Forms.Label lblViewBKColor;
        private plexdata.Controls.ColorButton btnCursorBKColor;
        private plexdata.Controls.ColorButton btnViewBKColor;
        private plexdata.Controls.ColorButton btnViewFGColor;
        private System.Windows.Forms.Button btnDefaults;
        private System.Windows.Forms.ComboBox cmbBinLineLength;
        private System.Windows.Forms.Label lblViewFGColor;
        private System.Windows.Forms.Label lblBinLineLength;
        private System.Windows.Forms.ToolTip ttHelper;
        private System.Windows.Forms.Label lblImageBorderColor;
        private plexdata.Controls.ColorButton btnImageBorderColor;
        private System.Windows.Forms.CheckBox chkAutoLoadLastFiles;
        private System.Windows.Forms.Button btnClearLastFilenames;
        private System.Windows.Forms.Label lblClearLastFilenames;
        private System.Windows.Forms.Button btnExportPath;
        private System.Windows.Forms.Label lblExportPath;
    }
}