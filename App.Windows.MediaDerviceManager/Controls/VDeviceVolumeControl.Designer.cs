using System;
using System.ComponentModel;
using System.Windows.Forms;
using EConTech.Windows.MACUI;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VDeviceVolumeControl
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
      this.nameLabel = new Label();
      this.muteCheCheckBox = new App.Windows.MediaDerviceManager.Controls.CheCheckBox();
      this.iconPictureBox = new PictureBox();
      this.defaultCheckBox = new CheckBox();
      this.rightVLedBar = new App.Windows.MediaDerviceManager.Controls.VLedBar();
      this.volumeMACTrackBar = new EConTech.Windows.MACUI.MACTrackBar();
      this.leftVLedBar = new App.Windows.MediaDerviceManager.Controls.VLedBar();
      this.volumeLabel = new Label();
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).BeginInit();
      base.SuspendLayout();
      this.nameLabel.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
      this.nameLabel.Location = new System.Drawing.Point(3, 10);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(122, 48);
      this.nameLabel.TabIndex = 3;
      this.nameLabel.Text = "Temp Text";
      this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.nameLabel.Click += new System.EventHandler(lblProcessName_Click);
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
      this.muteCheCheckBox.Location = new System.Drawing.Point(46, 400);
      this.muteCheCheckBox.Name = "btnMute";
      this.muteCheCheckBox.Padding = new Padding(0, 0, 2, 2);
      this.muteCheCheckBox.Size = new System.Drawing.Size(34, 34);
      this.muteCheCheckBox.TabIndex = 7;
      this.muteCheCheckBox.UseVisualStyleBackColor = false;
      this.muteCheCheckBox.CheckedChanged += new System.EventHandler(btnMute_CheckedChanged);
      this.muteCheCheckBox.MouseEnter += new System.EventHandler(btnMute_MouseEnter);
      this.muteCheCheckBox.MouseLeave += new System.EventHandler(btnMute_MouseLeave);
      this.iconPictureBox.BackColor = System.Drawing.Color.Transparent;
      this.iconPictureBox.Location = new System.Drawing.Point(47, 58);
      this.iconPictureBox.Margin = new Padding(0);
      this.iconPictureBox.Name = "iconPictureBox";
      this.iconPictureBox.Size = new System.Drawing.Size(32, 32);
      this.iconPictureBox.TabIndex = 0;
      this.iconPictureBox.TabStop = false;
      this.defaultCheckBox.AutoSize = true;
      this.defaultCheckBox.Cursor = Cursors.Hand;
      this.defaultCheckBox.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
      this.defaultCheckBox.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
      this.defaultCheckBox.Location = new System.Drawing.Point(13, 98);
      this.defaultCheckBox.Name = "defaultCheckBox";
      this.defaultCheckBox.Size = new System.Drawing.Size(105, 16);
      this.defaultCheckBox.TabIndex = 13;
      this.defaultCheckBox.Text = "Set as Default";
      this.defaultCheckBox.UseVisualStyleBackColor = true;
      this.defaultCheckBox.MouseClick += new MouseEventHandler(chkSetDefault_MouseClick);
      this.rightVLedBar.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.rightVLedBar.IsMuted = false;
      this.rightVLedBar.Location = new System.Drawing.Point(108, 132);
      this.rightVLedBar.Name = "rightVLedBar";
      this.rightVLedBar.Size = new System.Drawing.Size(5, 225);
      this.rightVLedBar.TabIndex = 12;
      this.volumeMACTrackBar.BackColor = System.Drawing.Color.Transparent;
      this.volumeMACTrackBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
      this.volumeMACTrackBar.Cursor = Cursors.Hand;
      this.volumeMACTrackBar.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.volumeMACTrackBar.ForeColor = System.Drawing.Color.FromArgb(123, 125, 123);
      this.volumeMACTrackBar.IndentHeight = 6;
      this.volumeMACTrackBar.LargeChange = 1;
      this.volumeMACTrackBar.Location = new System.Drawing.Point(23, 121);
      this.volumeMACTrackBar.Maximum = 100;
      this.volumeMACTrackBar.Minimum = 0;
      this.volumeMACTrackBar.Name = "macTrackBar1";
      this.volumeMACTrackBar.Orientation = Orientation.Vertical;
      this.volumeMACTrackBar.Size = new System.Drawing.Size(79, 247);
      this.volumeMACTrackBar.TabIndex = 10;
      this.volumeMACTrackBar.TextTickStyle = TickStyle.Both;
      this.volumeMACTrackBar.TickColor = System.Drawing.Color.FromArgb(148, 146, 148);
      this.volumeMACTrackBar.TickFrequency = 10;
      this.volumeMACTrackBar.TickHeight = 4;
      this.volumeMACTrackBar.TickStyle = TickStyle.Both;
      this.volumeMACTrackBar.TrackerColor = System.Drawing.Color.FromArgb(255, 110, 0);
      this.volumeMACTrackBar.TrackerSize = new System.Drawing.Size(16, 16);
      this.volumeMACTrackBar.TrackLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
      this.volumeMACTrackBar.VolumeLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
      this.volumeMACTrackBar.TrackLineHeight = 3;
      this.volumeMACTrackBar.Value = 0;
      this.volumeMACTrackBar.ValueChanged += macTrackBar1_OnValueChanged;
      this.leftVLedBar.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.leftVLedBar.IsMuted = false;
      this.leftVLedBar.Location = new System.Drawing.Point(13, 132);
      this.leftVLedBar.Name = "leftVLedBar";
      this.leftVLedBar.Size = new System.Drawing.Size(5, 225);
      this.leftVLedBar.TabIndex = 1;
      this.volumeLabel.Name = "volumeLabel";
      this.volumeLabel.BackColor = System.Drawing.Color.Transparent;
      this.volumeLabel.BorderStyle = BorderStyle.FixedSingle;
      this.volumeLabel.Font = new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.volumeLabel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
      this.volumeLabel.Location = new System.Drawing.Point(44, 370);
      this.volumeLabel.Size = new System.Drawing.Size(38, 19);
      this.volumeLabel.TabIndex = 11;
      this.volumeLabel.Text = "100";
      this.volumeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.volumeLabel.UseCompatibleTextRendering = true;
      this.volumeLabel.UseMnemonic = false;
      base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      base.AutoScaleMode = AutoScaleMode.Font;
      base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.BackColor = System.Drawing.Color.Transparent;
      base.Controls.Add(this.defaultCheckBox);
      base.Controls.Add(this.rightVLedBar);
      base.Controls.Add(this.volumeLabel);
      base.Controls.Add(this.volumeMACTrackBar);
      base.Controls.Add(this.muteCheCheckBox);
      base.Controls.Add(this.nameLabel);
      base.Controls.Add(this.leftVLedBar);
      base.Controls.Add(this.iconPictureBox);
      base.Margin = new Padding(0);
      base.Name = "VDeviceVolumeControl";
      base.Size = new System.Drawing.Size(128, 485);
      base.Paint += new PaintEventHandler(_OnPaint);
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).EndInit();
      base.ResumeLayout(false);
      base.PerformLayout();
    }
    /************************************************/
    public Label nameLabel = null;
    public PictureBox iconPictureBox = null;
    private CheckBox defaultCheckBox = null;
    public VLedBar leftVLedBar = null;
    public MACTrackBar volumeMACTrackBar = null;
    public VLedBar rightVLedBar = null;
    private Label volumeLabel = null;
    public CheCheckBox muteCheCheckBox = null;
  }
}