using System;
using System.ComponentModel;
using System.Windows.Forms;
using EConTech.Windows.MACUI;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class HDeviceVolumeControl
  {
    private IContainer components = null;
    /************************************************/
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.components != null)
        {
          this.components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    /************************************************/
    private void InitializeComponent()
    {
      this.defaultCheckBox = new CheckBox();
      this.iconPictureBox = new PictureBox();
      this.nameLabel = new Label();
      this.muteCheCheckBox = new CheCheckBox();
      this.volumeMACTrackBar = new MACTrackBar();
      /************************************************/
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).BeginInit();
      base.SuspendLayout();
      /************************************************/
      // defaultCheckBox
      this.defaultCheckBox.AutoSize = true;
      this.defaultCheckBox.Cursor = Cursors.Hand;
      this.defaultCheckBox.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
      this.defaultCheckBox.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
      this.defaultCheckBox.Location = new System.Drawing.Point(34, 23);
      this.defaultCheckBox.Margin = new Padding(4);
      this.defaultCheckBox.Name = "defaultCheckBox";
      this.defaultCheckBox.Size = new System.Drawing.Size(18, 17);
      this.defaultCheckBox.TabIndex = 13;
      this.defaultCheckBox.UseVisualStyleBackColor = true;
      this.defaultCheckBox.MouseClick += new MouseEventHandler(chkSetDefault_MouseClick);
      // iconPictureBox
      this.iconPictureBox.BackColor = System.Drawing.Color.Transparent;
      this.iconPictureBox.Location = new System.Drawing.Point(71, 12);
      this.iconPictureBox.Margin = new Padding(0);
      this.iconPictureBox.Name = "IconBox";
      this.iconPictureBox.Size = new System.Drawing.Size(43, 39);
      this.iconPictureBox.TabIndex = 0;
      this.iconPictureBox.TabStop = false;
      // nameLabel
      this.nameLabel.Font = new System.Drawing.Font("Verdana", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
      this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
      this.nameLabel.Location = new System.Drawing.Point(119, 4);
      this.nameLabel.Margin = new Padding(4, 0, 4, 0);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(157, 59);
      this.nameLabel.TabIndex = 3;
      this.nameLabel.Text = "Temp Text";
      this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.nameLabel.Click += new System.EventHandler(lblProcessName_Click);
      // muteCheCheckBox
      this.muteCheCheckBox.Appearance = Appearance.Button;
      this.muteCheCheckBox.AutoSize = true;
      this.muteCheCheckBox.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.muteCheCheckBox.Cursor = Cursors.Hand;
      this.muteCheCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
      this.muteCheCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.muteCheCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.muteCheCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.muteCheCheckBox.FlatStyle = FlatStyle.Flat;
      this.muteCheCheckBox.Image = CheVolume.Properties.Resources.mute;
      this.muteCheCheckBox.Location = new System.Drawing.Point(287, 12);
      this.muteCheCheckBox.Margin = new Padding(4);
      this.muteCheCheckBox.Name = "muteCheCheckBox";
      this.muteCheCheckBox.Padding = new Padding(0, 0, 3, 2);
      this.muteCheCheckBox.Size = new System.Drawing.Size(34, 34);
      this.muteCheCheckBox.TabIndex = 7;
      this.muteCheCheckBox.UseVisualStyleBackColor = false;
      this.muteCheCheckBox.CheckedChanged += new System.EventHandler(btnMute_CheckedChanged);
      this.muteCheCheckBox.MouseEnter += new System.EventHandler(btnMute_MouseEnter);
      this.muteCheCheckBox.MouseLeave += new System.EventHandler(btnMute_MouseLeave);
      // volumeMACTrackBar
      this.volumeMACTrackBar.BackColor = System.Drawing.Color.Transparent;
      this.volumeMACTrackBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
      this.volumeMACTrackBar.Cursor = Cursors.Hand;
      this.volumeMACTrackBar.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.volumeMACTrackBar.ForeColor = System.Drawing.Color.FromArgb(123, 125, 123);
      this.volumeMACTrackBar.IndentHeight = 6;
      this.volumeMACTrackBar.LargeChange = 1;
      this.volumeMACTrackBar.Location = new System.Drawing.Point(13, 54);
      this.volumeMACTrackBar.Margin = new Padding(4);
      this.volumeMACTrackBar.Maximum = 100;
      this.volumeMACTrackBar.Minimum = 0;
      this.volumeMACTrackBar.Name = "macTrackBar1";
      this.volumeMACTrackBar.Size = new System.Drawing.Size(329, 28);
      this.volumeMACTrackBar.TabIndex = 10;
      this.volumeMACTrackBar.TextTickStyle = TickStyle.None;
      this.volumeMACTrackBar.TickColor = System.Drawing.Color.FromArgb(148, 146, 148);
      this.volumeMACTrackBar.TickFrequency = 10;
      this.volumeMACTrackBar.TickHeight = 4;
      this.volumeMACTrackBar.TickStyle = TickStyle.None;
      this.volumeMACTrackBar.TrackerColor = System.Drawing.Color.FromArgb(255, 110, 0);
      this.volumeMACTrackBar.TrackerSize = new System.Drawing.Size(16, 16);
      this.volumeMACTrackBar.TrackLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
      this.volumeMACTrackBar.TrackLineHeight = 3;
      this.volumeMACTrackBar.Value = 0;
      this.volumeMACTrackBar.VolumeLineColor = System.Drawing.SystemColors.Control;
      this.volumeMACTrackBar.ValueChanged += Method1;
      // HDeviceVolumeControl
      base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
      base.AutoScaleMode = AutoScaleMode.Font;
      base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.BackColor = System.Drawing.Color.Transparent;
      base.Controls.Add(this.defaultCheckBox);
      base.Controls.Add(this.volumeMACTrackBar);
      base.Controls.Add(this.muteCheCheckBox);
      base.Controls.Add(this.nameLabel);
      base.Controls.Add(this.iconPictureBox);
      base.Margin = new Padding(0);
      base.Name = "HDeviceVolumeControl";
      base.Size = new System.Drawing.Size(364, 81);
      /************************************************/
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).EndInit();
      base.ResumeLayout(false);
      base.PerformLayout();
    }
    /************************************************/
    private CheckBox defaultCheckBox = null;
    public PictureBox iconPictureBox = null;
    public Label nameLabel = null;
    public CheCheckBox muteCheCheckBox = null;
    public MACTrackBar volumeMACTrackBar = null;
  }
}