using a;
using AudioCore;
using AudioCore.Interfaces;
using AudioCore2;
using CheVolume.Properties;
using EConTech.Windows.MACUI;
using JC.CS.Lib.Controls;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinForm = System.Windows.Forms;
using ComModel = System.ComponentModel;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  private ComModel::IContainer components;
  /************************************************/
  private WinForm::Timer timer1;
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
    this.volumeLabel         = new();
    this.contextMenuStrip1   = new();
    this.toolStripMenuItem1  = new();
    this.toolStripMenuItem2  = new();
    this.toolStripMenuItem3  = new();
    this.toolStripMenuItem4  = new();
    this.pidLabel            = new();
    this.transfertButton     = new();
    this.sessionPictureBox   = new();
    this.showprocCheCheckBox = new();
    this.macTrackBar1        = new();
    this.muteCheCheckBox     = new();
    this.leftLedBar          = new();
    /************************************************/
    // nameLabel
    this.nameLabel.Name = "nameLabel";
    this.nameLabel.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.nameLabel.ForeColor = System.Drawing.Color.DimGray;
    this.nameLabel.Location = new System.Drawing.Point(2, 18);
    this.nameLabel.Margin = new System.Windows.Forms.Padding(0);
    this.nameLabel.Size = new System.Drawing.Size(96, 60);
    this.nameLabel.Text = "Temp Text";
    this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    // sessionPictureBox
    this.sessionPictureBox.Name = "sessionPictureBox";
    this.sessionPictureBox.BackColor = System.Drawing.Color.Transparent;
    this.sessionPictureBox.Location = new System.Drawing.Point(33, 79);
    this.sessionPictureBox.Margin = new System.Windows.Forms.Padding(0);
    this.sessionPictureBox.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
    this.sessionPictureBox.Size = new System.Drawing.Size(34, 34);
    this.sessionPictureBox.TabStop = false;
    this.sessionPictureBox.Click += new System.EventHandler(sessionPictureBox_Click);
    // pidLabel
    this.pidLabel.Name = "pidLabel";
    this.pidLabel.Font = new System.Drawing.Font("Verdana", 6.25f, System.Drawing.FontStyle.Bold);
    this.pidLabel.ForeColor = System.Drawing.Color.DimGray;
    this.pidLabel.Location = new System.Drawing.Point(2, 113);
    this.pidLabel.Size = new System.Drawing.Size(96, 13);
    this.pidLabel.Text = "label1";
    this.pidLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    this.pidLabel.Visible = false;
    // leftLedBar
    this.leftLedBar.Name = "leftLedBar";
    this.leftLedBar.BackColor = System.Drawing.SystemColors.ScrollBar;
    this.leftLedBar.Enabled = true;
    this.leftLedBar.Location = new System.Drawing.Point(22, 132);
    this.leftLedBar.Size = new System.Drawing.Size(5, 225);
    // macTrackBar1
    this.macTrackBar1.Name = "macTrackBar1";
    this.macTrackBar1.BackColor = System.Drawing.Color.Transparent;
    this.macTrackBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
    this.macTrackBar1.Cursor = System.Windows.Forms.Cursors.Hand;
    this.macTrackBar1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.macTrackBar1.ForeColor = System.Drawing.Color.FromArgb(123, 125, 123);
    this.macTrackBar1.IndentHeight = 6;
    this.macTrackBar1.LargeChange = 1;
    this.macTrackBar1.Location = new System.Drawing.Point(30, 121);
    this.macTrackBar1.Maximum = 100;
    this.macTrackBar1.Minimum = 0;
    this.macTrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
    this.macTrackBar1.Size = new System.Drawing.Size(58, 247);
    this.macTrackBar1.TickColor = System.Drawing.Color.FromArgb(148, 146, 148);
    this.macTrackBar1.TickFrequency = 10;
    this.macTrackBar1.TickHeight = 4;
    this.macTrackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
    this.macTrackBar1.TrackerColor = System.Drawing.Color.FromArgb(255, 128, 0);
    this.macTrackBar1.TrackerSize = new System.Drawing.Size(16, 16);
    this.macTrackBar1.TrackLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
    this.macTrackBar1.TrackLineHeight = 3;
    this.macTrackBar1.Value = 0;
    this.macTrackBar1.ValueChanged += macTrackBar1_ValueChanged;
    // volumeLabel
    this.volumeLabel.Name = "volumeLabel";
    this.volumeLabel.BackColor = System.Drawing.Color.Transparent;
    this.volumeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
    this.volumeLabel.Font = new System.Drawing.Font("Verdana", 7f, System.Drawing.FontStyle.Bold);
    this.volumeLabel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
    this.volumeLabel.Location = new System.Drawing.Point(30, 371);
    this.volumeLabel.Size = new System.Drawing.Size(40, 20);
    this.volumeLabel.Text = "100";
    this.volumeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    this.volumeLabel.UseCompatibleTextRendering = true;
    this.volumeLabel.UseMnemonic = false;
    // muteCheCheckBox
    this.muteCheCheckBox.Name = "muteCheCheckBox";
    this.muteCheCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
    this.muteCheCheckBox.AutoSize = true;
    this.muteCheCheckBox.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.muteCheCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
    this.muteCheCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.muteCheCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.muteCheCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.muteCheCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.muteCheCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.muteCheCheckBox.Image = CheVolume.Properties.Resources.mute;
    this.muteCheCheckBox.Location = new System.Drawing.Point(13, 400);
    this.muteCheCheckBox.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
    this.muteCheCheckBox.Size = new System.Drawing.Size(34, 34);
    this.muteCheCheckBox.UseVisualStyleBackColor = false;
    this.muteCheCheckBox.CheckedChanged += new System.EventHandler(muteCheCheckBox_CheckedChanged);
    this.muteCheCheckBox.MouseEnter += new System.EventHandler(muteCheCheckBox_MouseEnter);
    this.muteCheCheckBox.MouseLeave += new System.EventHandler(muteCheCheckBox_MouseLeave);
    // showprocCheCheckBox
    this.showprocCheCheckBox.Name = "showprocCheCheckBox";
    this.showprocCheCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
    this.showprocCheCheckBox.AutoSize = true;
    this.showprocCheCheckBox.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.showprocCheCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
    this.showprocCheCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.showprocCheCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.showprocCheCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.showprocCheCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.showprocCheCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.showprocCheCheckBox.Image = CheVolume.Properties.Resources.showwindow;
    this.showprocCheCheckBox.Location = new System.Drawing.Point(53, 400);
    this.showprocCheCheckBox.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
    this.showprocCheCheckBox.Size = new System.Drawing.Size(34, 34);
    this.showprocCheCheckBox.UseVisualStyleBackColor = false;
    this.showprocCheCheckBox.Click += new System.EventHandler(showprocCheCheckBox_Click);
    this.showprocCheCheckBox.MouseEnter += new System.EventHandler(showprocCheCheckBox_MouseEnter);
    this.showprocCheCheckBox.MouseLeave += new System.EventHandler(showprocCheCheckBox_MouseLeave);
    this.showprocCheCheckBox.MouseHover += new System.EventHandler(showprocCheCheckBox_MouseHover);
    // transfertButton
    this.transfertButton.Name = "transfertButton";
    this.transfertButton.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.transfertButton.BackgroundImage = CheVolume.Properties.Resources.CheV;
    this.transfertButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
    this.transfertButton.Cursor = System.Windows.Forms.Cursors.Hand;
    this.transfertButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.transfertButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.transfertButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.transfertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.transfertButton.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.transfertButton.Location = new System.Drawing.Point(33, 440);
    this.transfertButton.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
    this.transfertButton.Size = new System.Drawing.Size(34, 34);
    this.transfertButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
    this.transfertButton.UseVisualStyleBackColor = false;
    this.transfertButton.Click += new System.EventHandler(transfertButton_Click);
    this.transfertButton.MouseEnter += new System.EventHandler(transfertButton_MouseEnter);
    this.transfertButton.MouseLeave += new System.EventHandler(transfertButton_MouseLeave);
    this.transfertButton.MouseHover += new System.EventHandler(transfertButton_MouseHover);
    // contextMenuStrip1
    this.contextMenuStrip1.Font = new System.Drawing.Font("Verdana", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
    this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
    this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3, this.toolStripMenuItem4 });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
    this.contextMenuStrip1.Size = new System.Drawing.Size(204, 114);
    this.contextMenuStrip1.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(contextMenuStrip1_Closed);
    // toolStripMenuItem1
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    this.toolStripMenuItem1.Size = new System.Drawing.Size(203, 22);
    this.toolStripMenuItem1.Text = "toolStripMenuItem1";
    // toolStripMenuItem2
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    this.toolStripMenuItem2.Size = new System.Drawing.Size(203, 22);
    this.toolStripMenuItem2.Text = "toolStripMenuItem2";
    // toolStripMenuItem3
    this.toolStripMenuItem3.Name = "toolStripMenuItem3";
    this.toolStripMenuItem3.Size = new System.Drawing.Size(203, 22);
    this.toolStripMenuItem3.Text = "toolStripMenuItem3";
    // toolStripMenuItem4
    this.toolStripMenuItem4.Name = "toolStripMenuItem4";
    this.toolStripMenuItem4.Size = new System.Drawing.Size(203, 22);
    this.toolStripMenuItem4.Text = "toolStripMenuItem4";
    // SessionVolumeControl
    base.Name = "SessionVolumeControl";
    base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
    base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
    this.BackColor = System.Drawing.Color.Transparent;
    base.Margin = new System.Windows.Forms.Padding(0);
    base.Size = new System.Drawing.Size(100, 485);
    base.Load += new System.EventHandler(_OnLoad);
    base.Controls.Add(this.pidLabel);
    base.Controls.Add(this.showprocCheCheckBox);
    base.Controls.Add(this.transfertButton);
    base.Controls.Add(this.volumeLabel);
    base.Controls.Add(this.macTrackBar1);
    base.Controls.Add(this.muteCheCheckBox);
    base.Controls.Add(this.nameLabel);
    base.Controls.Add(this.leftLedBar);
    base.Controls.Add(this.sessionPictureBox);
  }
  /************************************************/
  public Label nameLabel;
  public PictureBox sessionPictureBox;
  private Label pidLabel;
  public VLevelMeter leftLedBar;
  public MACTrackBar macTrackBar1;
  private Label volumeLabel;
  public CheckBox muteCheCheckBox;
  public CheckBox showprocCheCheckBox;
  public Button transfertButton;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem toolStripMenuItem1;
  private ToolStripMenuItem toolStripMenuItem2;
  private ToolStripMenuItem toolStripMenuItem3;
  private ToolStripMenuItem toolStripMenuItem4;
}