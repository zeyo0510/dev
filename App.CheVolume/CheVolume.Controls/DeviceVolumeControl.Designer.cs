using EConTech.Windows.MACUI;
using System.ComponentModel;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class DeviceVolumeControl
{
  private IContainer components;
  /************************************************/
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.components?.Dispose();
    }
    /************************************************/
    base.Dispose(disposing);
  }
  /************************************************/
  private void InitializeComponent()
  {
    this.lblProcessName = new();
    this.btnMute        = new();
    this.IconBox        = new();
    this.chkSetDefault  = new();
    this.lBarRight      = new();
    this.macTrackBar1   = new();
    this.lBarLeft       = new();
    this.lblVolume      = new();
    ((System.ComponentModel.ISupportInitialize)this.IconBox).BeginInit();
    base.SuspendLayout();
    // lblProcessName
    this.lblProcessName.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.lblProcessName.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
    this.lblProcessName.Location = new System.Drawing.Point(3, 10);
    this.lblProcessName.Name = "lblProcessName";
    this.lblProcessName.Size = new System.Drawing.Size(122, 48);
    this.lblProcessName.TabIndex = 3;
    this.lblProcessName.Text = "Temp Text";
    this.lblProcessName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    this.lblProcessName.Click += new System.EventHandler(lblProcessName_Click);
    // btnMute
    this.btnMute.Appearance = System.Windows.Forms.Appearance.Button;
    this.btnMute.AutoSize = true;
    this.btnMute.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnMute.Cursor = System.Windows.Forms.Cursors.Hand;
    this.btnMute.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.btnMute.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnMute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnMute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnMute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.btnMute.Image = CheVolume.Properties.Resources.mute;
    this.btnMute.Location = new System.Drawing.Point(46, 400);
    this.btnMute.Name = "btnMute";
    this.btnMute.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
    this.btnMute.Size = new System.Drawing.Size(34, 34);
    this.btnMute.TabIndex = 7;
    this.btnMute.UseVisualStyleBackColor = false;
    this.btnMute.CheckedChanged += new System.EventHandler(btnMute_CheckedChanged);
    this.btnMute.MouseEnter += new System.EventHandler(btnMute_MouseEnter);
    this.btnMute.MouseLeave += new System.EventHandler(btnMute_MouseLeave);
    this.btnMute.MouseUp += new System.Windows.Forms.MouseEventHandler(btnMute_MouseUp);
    // IconBox
    this.IconBox.BackColor = System.Drawing.Color.Transparent;
    this.IconBox.Location = new System.Drawing.Point(47, 58);
    this.IconBox.Margin = new System.Windows.Forms.Padding(0);
    this.IconBox.Name = "IconBox";
    this.IconBox.Size = new System.Drawing.Size(32, 32);
    this.IconBox.TabIndex = 0;
    this.IconBox.TabStop = false;
    // chkSetDefault
    this.chkSetDefault.AutoSize = true;
    this.chkSetDefault.Cursor = System.Windows.Forms.Cursors.Hand;
    this.chkSetDefault.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
    this.chkSetDefault.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
    this.chkSetDefault.Location = new System.Drawing.Point(13, 98);
    this.chkSetDefault.Name = "chkSetDefault";
    this.chkSetDefault.Size = new System.Drawing.Size(105, 16);
    this.chkSetDefault.TabIndex = 13;
    this.chkSetDefault.Text = "Set as Default";
    this.chkSetDefault.UseVisualStyleBackColor = true;
    this.chkSetDefault.CheckedChanged += new System.EventHandler(chkSetDefault_CheckedChanged);
    this.chkSetDefault.MouseClick += new System.Windows.Forms.MouseEventHandler(chkSetDefault_MouseClick);
    // lBarRight
    this.lBarRight.BackColor = System.Drawing.SystemColors.ScrollBar;
    this.lBarRight.IsMuted = false;
    this.lBarRight.Location = new System.Drawing.Point(108, 132);
    this.lBarRight.Name = "lBarRight";
    this.lBarRight.Size = new System.Drawing.Size(5, 225);
    this.lBarRight.TabIndex = 12;
    // macTrackBar1
    this.macTrackBar1.BackColor = System.Drawing.Color.Transparent;
    this.macTrackBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
    this.macTrackBar1.Cursor = System.Windows.Forms.Cursors.Hand;
    this.macTrackBar1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.macTrackBar1.ForeColor = System.Drawing.Color.FromArgb(123, 125, 123);
    this.macTrackBar1.IndentHeight = 6;
    this.macTrackBar1.LargeChange = 1;
    this.macTrackBar1.Location = new System.Drawing.Point(23, 121);
    this.macTrackBar1.Maximum = 100;
    this.macTrackBar1.Minimum = 0;
    this.macTrackBar1.Name = "macTrackBar1";
    this.macTrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
    this.macTrackBar1.Size = new System.Drawing.Size(79, 247);
    this.macTrackBar1.TabIndex = 10;
    this.macTrackBar1.TextTickStyle = System.Windows.Forms.TickStyle.Both;
    this.macTrackBar1.TickColor = System.Drawing.Color.FromArgb(148, 146, 148);
    this.macTrackBar1.TickFrequency = 10;
    this.macTrackBar1.TickHeight = 4;
    this.macTrackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
    this.macTrackBar1.TrackerColor = System.Drawing.Color.FromArgb(255, 110, 0);
    this.macTrackBar1.TrackerSize = new System.Drawing.Size(16, 16);
    this.macTrackBar1.TrackLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
    this.macTrackBar1.VolumeLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
    this.macTrackBar1.TrackLineHeight = 3;
    this.macTrackBar1.Value = 0;
    this.macTrackBar1.ValueChanged += macTrackBar1_OnValueChanged;
    // lBarLeft
    this.lBarLeft.BackColor = System.Drawing.SystemColors.ScrollBar;
    this.lBarLeft.IsMuted = false;
    this.lBarLeft.Location = new System.Drawing.Point(13, 132);
    this.lBarLeft.Name = "lBarLeft";
    this.lBarLeft.Size = new System.Drawing.Size(5, 225);
    this.lBarLeft.TabIndex = 1;
    this.lBarLeft.Load += new System.EventHandler(lBarLeft_Load);
    // lblVolume
    this.lblVolume.BackColor = System.Drawing.Color.Transparent;
    this.lblVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
    this.lblVolume.Font = new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.lblVolume.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
    this.lblVolume.Location = new System.Drawing.Point(44, 370);
    this.lblVolume.Name = "lblVolume";
    this.lblVolume.Size = new System.Drawing.Size(38, 19);
    this.lblVolume.TabIndex = 11;
    this.lblVolume.Text = "100";
    this.lblVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    this.lblVolume.UseCompatibleTextRendering = true;
    this.lblVolume.UseMnemonic = false;
    // DeviceVolumeControl
    base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
    base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
    this.BackColor = System.Drawing.Color.Transparent;
    base.Controls.Add(this.chkSetDefault);
    base.Controls.Add(this.lBarRight);
    base.Controls.Add(this.lblVolume);
    base.Controls.Add(this.macTrackBar1);
    base.Controls.Add(this.btnMute);
    base.Controls.Add(this.lblProcessName);
    base.Controls.Add(this.lBarLeft);
    base.Controls.Add(this.IconBox);
    base.Margin = new System.Windows.Forms.Padding(0);
    base.Name = "DeviceVolumeControl";
    base.Size = new System.Drawing.Size(128, 485);
    ((System.ComponentModel.ISupportInitialize)this.IconBox).EndInit();
    base.ResumeLayout(false);
    base.PerformLayout();
  }
  /************************************************/
  public PictureBox IconBox;
  public LedBar lBarLeft;
  public Label lblProcessName;
  public CheCheckBox btnMute;
  public MACTrackBar macTrackBar1;
  public LedBar lBarRight;
  private CheckBox chkSetDefault;
  private Label lblVolume;
}