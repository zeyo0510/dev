using a;
using AudioCore;
using AudioCore.Interfaces;
using AudioCore2;
using CheVolume.Properties;
using EConTech.Windows.MACUI;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
/************************************************/
using JC.CS.Lib.Controls;
/************************************************/
using WinForm = System.Windows.Forms;
using ComModel = System.ComponentModel;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  private ComModel::IContainer components = null;
  /************************************************/
  private WinForm::Timer guiTimer = null;
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
    this.components = new ComModel::Container();
    /************************************************/
    this.nameLabel           = new();
    this.sessionPictureBox   = new();
    this.pidLabel            = new();
    this.leftLedBar          = new();
    this.macTrackBar1        = new();
    this.volumeLabel         = new();
    this.muteCheckBox     = new();
    this.showprocCheCheckBox = new();
    this.transfertButton     = new();
    this.contextMenuStrip1   = new();
    /************************************************/
    // nameLabel
    {
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Font = new("Verdana", 10f, FontStyle.Bold, GraphicsUnit.World, 0);
      this.nameLabel.ForeColor = Color.DimGray;
      this.nameLabel.Location = new(2, 18);
      this.nameLabel.Margin = new(0);
      this.nameLabel.Size = new(96, 60);
      this.nameLabel.Text = "Temp Text";
      this.nameLabel.TextAlign = ContentAlignment.MiddleCenter;
    }
    // sessionPictureBox
    {
      this.sessionPictureBox.Name = "sessionPictureBox";
      this.sessionPictureBox.BackColor = Color.Transparent;
      this.sessionPictureBox.Location = new(33, 79);
      this.sessionPictureBox.Margin = new(0);
      this.sessionPictureBox.Padding = new(1, 1, 0, 0);
      this.sessionPictureBox.Size = new(34, 34);
      this.sessionPictureBox.TabStop = false;
    }
    // pidLabel
    {
      this.pidLabel.Name = "pidLabel";
      this.pidLabel.Font = new("Verdana", 6.25f, FontStyle.Bold);
      this.pidLabel.ForeColor = Color.DimGray;
      this.pidLabel.Location = new(2, 113);
      this.pidLabel.Size = new(96, 13);
      this.pidLabel.Text = "label1";
      this.pidLabel.TextAlign = ContentAlignment.MiddleCenter;
    }
    // leftLedBar
    {
      this.leftLedBar.Name = "leftLedBar";
      this.leftLedBar.BackColor = SystemColors.ScrollBar;
      this.leftLedBar.Enabled = true;
      this.leftLedBar.Location = new(22, 132);
      this.leftLedBar.Size = new(5, 225);
    }
    // macTrackBar1
    {
      this.macTrackBar1.Name = "macTrackBar1";
      this.macTrackBar1.BackColor = Color.Transparent;
      this.macTrackBar1.BorderColor = SystemColors.ActiveBorder;
      this.macTrackBar1.Cursor = Cursors.Hand;
      this.macTrackBar1.Font = new("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.World, 0);
      this.macTrackBar1.ForeColor = Color.FromArgb(123, 125, 123);
      this.macTrackBar1.IndentHeight = 6;
      this.macTrackBar1.LargeChange = 1;
      this.macTrackBar1.Location = new(30, 121);
      this.macTrackBar1.Maximum = 100;
      this.macTrackBar1.Minimum = 0;
      this.macTrackBar1.Orientation = Orientation.Vertical;
      this.macTrackBar1.Size = new(58, 247);
      this.macTrackBar1.TickColor = Color.FromArgb(148, 146, 148);
      this.macTrackBar1.TickFrequency = 10;
      this.macTrackBar1.TickHeight = 4;
      this.macTrackBar1.TickStyle = TickStyle.Both;
      this.macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
      this.macTrackBar1.TrackerSize = new(16, 16);
      this.macTrackBar1.TrackLineColor = Color.FromArgb(90, 93, 90);
      this.macTrackBar1.TrackLineHeight = 3;
      this.macTrackBar1.Value = 0;
      /************************************************/
      this.macTrackBar1.ValueChanged += macTrackBar1_ValueChanged;
    }
    // volumeLabel
    {
      this.volumeLabel.Name = "volumeLabel";
      this.volumeLabel.BackColor = Color.Transparent;
      this.volumeLabel.BorderStyle = BorderStyle.FixedSingle;
      this.volumeLabel.Font = new("Verdana", 7f, FontStyle.Bold);
      this.volumeLabel.ForeColor = Color.FromArgb(64, 64, 64);
      this.volumeLabel.Location = new(30, 371);
      this.volumeLabel.Size = new(40, 20);
      this.volumeLabel.Text = "100";
      this.volumeLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.volumeLabel.UseCompatibleTextRendering = true;
      this.volumeLabel.UseMnemonic = false;
    }
    // muteCheckBox
    {
      this.muteCheckBox.Name = "muteCheckBox";
      this.muteCheckBox.AutoSize = true;
      this.muteCheckBox.Location = new(13, 400);
      this.muteCheckBox.Text = "Mute";
      /************************************************/
      this.muteCheckBox.CheckedChanged += this.muteCheckBox_CheckedChanged;
    }
    // showprocCheCheckBox
    {
      this.showprocCheCheckBox.Name = "showprocCheCheckBox";
      this.showprocCheCheckBox.Appearance = Appearance.Button;
      this.showprocCheCheckBox.AutoSize = true;
      this.showprocCheCheckBox.BackColor = Color.FromArgb(225, 225, 225);
      this.showprocCheCheckBox.Cursor = Cursors.Hand;
      this.showprocCheCheckBox.FlatAppearance.BorderColor = Color.DarkGray;
      this.showprocCheCheckBox.FlatAppearance.CheckedBackColor = Color.FromArgb(225, 225, 225);
      this.showprocCheCheckBox.FlatAppearance.MouseDownBackColor = Color.FromArgb(225, 225, 225);
      this.showprocCheCheckBox.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 225, 225);
      this.showprocCheCheckBox.FlatStyle = FlatStyle.Flat;
      this.showprocCheCheckBox.Image = CheVolume.Properties.Resources.showwindow;
      this.showprocCheCheckBox.Location = new(53, 400);
      this.showprocCheCheckBox.Padding = new(0, 0, 2, 2);
      this.showprocCheCheckBox.Size = new(34, 34);
      this.showprocCheCheckBox.UseVisualStyleBackColor = false;
      /************************************************/
      this.showprocCheCheckBox.Click += showprocCheCheckBox_Click;
    }
    // transfertButton
    {
      this.transfertButton.Name = "transfertButton";
      this.transfertButton.BackColor = Color.FromArgb(225, 225, 225);
      this.transfertButton.BackgroundImage = CheVolume.Properties.Resources.CheV;
      this.transfertButton.BackgroundImageLayout = ImageLayout.Center;
      this.transfertButton.Cursor = Cursors.Hand;
      this.transfertButton.FlatAppearance.BorderColor = Color.DarkGray;
      this.transfertButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(225, 225, 225);
      this.transfertButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 225, 225);
      this.transfertButton.FlatStyle = FlatStyle.Flat;
      this.transfertButton.Font = new("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.World, 0);
      this.transfertButton.Location = new(33, 440);
      this.transfertButton.Padding = new(0, 0, 2, 2);
      this.transfertButton.Size = new(34, 34);
      this.transfertButton.TextAlign = ContentAlignment.TopCenter;
      this.transfertButton.UseVisualStyleBackColor = false;
      /************************************************/
      this.transfertButton.Click += transfertButton_Click;
      this.transfertButton.MouseEnter += transfertButton_MouseEnter;
    }
    // contextMenuStrip1
    {
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Font = new("Verdana", 11f, FontStyle.Bold, GraphicsUnit.World);
      this.contextMenuStrip1.ImageScalingSize = new(32, 32);
      this.contextMenuStrip1.RenderMode = ToolStripRenderMode.Professional;
    }
    // SessionVolumeControl
    {
      base.Name = "SessionVolumeControl";
      base.AutoScaleDimensions = new(6f, 13f);
      base.AutoScaleMode = AutoScaleMode.Font;
      base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.BackColor = Color.Transparent;
      base.Margin = new(0);
      base.Size = new(100, 485);
      base.Controls.Add(this.pidLabel);
      base.Controls.Add(this.showprocCheCheckBox);
      base.Controls.Add(this.transfertButton);
      base.Controls.Add(this.volumeLabel);
      base.Controls.Add(this.macTrackBar1);
      base.Controls.Add(this.muteCheckBox);
      base.Controls.Add(this.nameLabel);
      base.Controls.Add(this.leftLedBar);
      base.Controls.Add(this.sessionPictureBox);
      /************************************************/
      base.Load += _OnLoad;
    }
  }
  /************************************************/
  public Label nameLabel;
  public PictureBox sessionPictureBox;
  private Label pidLabel;
  public VLevelMeter leftLedBar;
  public MACTrackBar macTrackBar1;
  public VTrackBar volumeVTrackBar;
  private Label volumeLabel;
  public CheckBox muteCheckBox;
  public CheckBox showprocCheCheckBox;
  public Button transfertButton;
  private ContextMenuStrip contextMenuStrip1;
}