using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using a;
using AudioCore;
using AudioCore.Interfaces;
using AudioCore2;
using CheVolume.Properties;
using EConTech.Windows.MACUI;
using Microsoft.Win32;
using WinForm = System.Windows.Forms;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class SessionVolumeControl
{
  private IContainer components;
  /************************************************/
  protected override void Dispose(bool disposing)
  {
    if (disposing && components != null)
    {
      this.components?.Dispose();
    }
    /************************************************/
    base.Dispose(disposing);
  }
  /************************************************/
  private void InitializeComponent()
  {
    this.components = new System.ComponentModel.Container();
    this.lblProcessName = new System.Windows.Forms.Label();
    this.lblVolume = new System.Windows.Forms.Label();
    this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
    this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
    this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
    this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
    this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
    this.lblPId = new System.Windows.Forms.Label();
    this.btnTransfert = new System.Windows.Forms.Button();
    this.IconBox = new System.Windows.Forms.PictureBox();
    this.BtnShowProcess = new CheVolume.Controls.CheCheckBox();
    this.btnLock = new CheVolume.Controls.CheCheckBox();
    this.macTrackBar1 = new EConTech.Windows.MACUI.MACTrackBar();
    this.btnMute = new CheVolume.Controls.CheCheckBox();
    this.lBarLeft = new CheVolume.Controls.LedBar();
    /************************************************/
    // lblProcessName
    this.lblProcessName.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.lblProcessName.ForeColor = System.Drawing.Color.DimGray;
    this.lblProcessName.Location = new System.Drawing.Point(2, 18);
    this.lblProcessName.Margin = new System.Windows.Forms.Padding(0);
    this.lblProcessName.Name = "lblProcessName";
    this.lblProcessName.Size = new System.Drawing.Size(96, 60);
    this.lblProcessName.TabIndex = 3;
    this.lblProcessName.Text = "Temp Text";
    this.lblProcessName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    // lblVolume
    this.lblVolume.BackColor = System.Drawing.Color.Transparent;
    this.lblVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
    this.lblVolume.Font = new System.Drawing.Font("Verdana", 7f, System.Drawing.FontStyle.Bold);
    this.lblVolume.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
    this.lblVolume.Location = new System.Drawing.Point(30, 371);
    this.lblVolume.Name = "lblVolume";
    this.lblVolume.Size = new System.Drawing.Size(40, 20);
    this.lblVolume.TabIndex = 11;
    this.lblVolume.Text = "100";
    this.lblVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    this.lblVolume.UseCompatibleTextRendering = true;
    this.lblVolume.UseMnemonic = false;
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
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    this.toolStripMenuItem2.Size = new System.Drawing.Size(203, 22);
    this.toolStripMenuItem2.Text = "toolStripMenuItem2";
    this.toolStripMenuItem3.Name = "toolStripMenuItem3";
    this.toolStripMenuItem3.Size = new System.Drawing.Size(203, 22);
    this.toolStripMenuItem3.Text = "toolStripMenuItem3";
    this.toolStripMenuItem4.Name = "toolStripMenuItem4";
    this.toolStripMenuItem4.Size = new System.Drawing.Size(203, 22);
    this.toolStripMenuItem4.Text = "toolStripMenuItem4";
    // lblPId
    this.lblPId.Font = new System.Drawing.Font("Verdana", 6.25f, System.Drawing.FontStyle.Bold);
    this.lblPId.ForeColor = System.Drawing.Color.DimGray;
    this.lblPId.Location = new System.Drawing.Point(2, 113);
    this.lblPId.Name = "lblPId";
    this.lblPId.Size = new System.Drawing.Size(96, 13);
    this.lblPId.TabIndex = 16;
    this.lblPId.Text = "label1";
    this.lblPId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
    this.lblPId.Visible = false;
    // btnTransfert
    this.btnTransfert.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnTransfert.BackgroundImage = CheVolume.Properties.Resources.CheV;
    this.btnTransfert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
    this.btnTransfert.Cursor = System.Windows.Forms.Cursors.Hand;
    this.btnTransfert.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.btnTransfert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnTransfert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnTransfert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.btnTransfert.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
    this.btnTransfert.Location = new System.Drawing.Point(33, 440);
    this.btnTransfert.Name = "btnTransfert";
    this.btnTransfert.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
    this.btnTransfert.Size = new System.Drawing.Size(34, 34);
    this.btnTransfert.TabIndex = 14;
    this.btnTransfert.TextAlign = System.Drawing.ContentAlignment.TopCenter;
    this.btnTransfert.UseVisualStyleBackColor = false;
    this.btnTransfert.Click += new System.EventHandler(btnTransfert_Click);
    this.btnTransfert.MouseEnter += new System.EventHandler(btnTransfert_MouseEnter);
    this.btnTransfert.MouseLeave += new System.EventHandler(btnTransfert_MouseLeave);
    this.btnTransfert.MouseHover += new System.EventHandler(btnTransfert_MouseHover);
    // IconBox
    this.IconBox.BackColor = System.Drawing.Color.Transparent;
    this.IconBox.Location = new System.Drawing.Point(33, 79);
    this.IconBox.Margin = new System.Windows.Forms.Padding(0);
    this.IconBox.Name = "IconBox";
    this.IconBox.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
    this.IconBox.Size = new System.Drawing.Size(34, 34);
    this.IconBox.TabIndex = 0;
    this.IconBox.TabStop = false;
    this.IconBox.Click += new System.EventHandler(IconBox_Click);
    // BtnShowProcess
    this.BtnShowProcess.Appearance = System.Windows.Forms.Appearance.Button;
    this.BtnShowProcess.AutoSize = true;
    this.BtnShowProcess.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.BtnShowProcess.Cursor = System.Windows.Forms.Cursors.Hand;
    this.BtnShowProcess.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.BtnShowProcess.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.BtnShowProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.BtnShowProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.BtnShowProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.BtnShowProcess.Image = CheVolume.Properties.Resources.showwindow;
    this.BtnShowProcess.Location = new System.Drawing.Point(53, 400);
    this.BtnShowProcess.Name = "BtnShowProcess";
    this.BtnShowProcess.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
    this.BtnShowProcess.Size = new System.Drawing.Size(34, 34);
    this.BtnShowProcess.TabIndex = 15;
    this.BtnShowProcess.UseVisualStyleBackColor = false;
    this.BtnShowProcess.Click += new System.EventHandler(BtnShowProcess_Click);
    this.BtnShowProcess.MouseEnter += new System.EventHandler(BtnShowProcess_MouseEnter);
    this.BtnShowProcess.MouseLeave += new System.EventHandler(BtnShowProcess_MouseLeave);
    this.BtnShowProcess.MouseHover += new System.EventHandler(BtnShowProcess_MouseHover);
    // btnLock
    this.btnLock.Appearance = System.Windows.Forms.Appearance.Button;
    this.btnLock.AutoSize = true;
    this.btnLock.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnLock.Cursor = System.Windows.Forms.Cursors.Hand;
    this.btnLock.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.btnLock.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnLock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnLock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.btnLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.btnLock.Image = CheVolume.Properties.Resources.lockoff;
    this.btnLock.Location = new System.Drawing.Point(73, 440);
    this.btnLock.Name = "btnLock";
    this.btnLock.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
    this.btnLock.Size = new System.Drawing.Size(34, 34);
    this.btnLock.TabIndex = 15;
    this.btnLock.UseVisualStyleBackColor = false;
    this.btnLock.Visible = false;
    this.btnLock.CheckedChanged += new System.EventHandler(btnLock_CheckedChanged);
    // macTrackBar1
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
    this.macTrackBar1.Name = "macTrackBar1";
    this.macTrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
    this.macTrackBar1.Size = new System.Drawing.Size(58, 247);
    this.macTrackBar1.TabIndex = 10;
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
    this.btnMute.Location = new System.Drawing.Point(13, 400);
    this.btnMute.Name = "btnMute";
    this.btnMute.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
    this.btnMute.Size = new System.Drawing.Size(34, 34);
    this.btnMute.TabIndex = 7;
    this.btnMute.UseVisualStyleBackColor = false;
    this.btnMute.CheckedChanged += new System.EventHandler(btnMute_CheckedChanged);
    this.btnMute.MouseEnter += new System.EventHandler(btnMute_MouseEnter);
    this.btnMute.MouseLeave += new System.EventHandler(btnMute_MouseLeave);
    // lBarLeft
    this.lBarLeft.BackColor = System.Drawing.SystemColors.ScrollBar;
    this.lBarLeft.IsMuted = false;
    this.lBarLeft.Location = new System.Drawing.Point(22, 132);
    this.lBarLeft.Name = "lBarLeft";
    this.lBarLeft.Size = new System.Drawing.Size(5, 225);
    this.lBarLeft.TabIndex = 1;
    // SessionVolumeControl
    base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
    base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
    this.BackColor = System.Drawing.Color.Transparent;
    base.Controls.Add(this.lblPId);
    base.Controls.Add(this.BtnShowProcess);
    base.Controls.Add(this.btnLock);
    base.Controls.Add(this.btnTransfert);
    base.Controls.Add(this.lblVolume);
    base.Controls.Add(this.macTrackBar1);
    base.Controls.Add(this.btnMute);
    base.Controls.Add(this.lblProcessName);
    base.Controls.Add(this.lBarLeft);
    base.Controls.Add(this.IconBox);
    base.Margin = new System.Windows.Forms.Padding(0);
    base.Name = "SessionVolumeControl";
    base.Size = new System.Drawing.Size(100, 485);
    base.Load += new System.EventHandler(_OnLoad);
  }
  /************************************************/
  public PictureBox IconBox;
  public LedBar lBarLeft;
  public Label lblProcessName;
  public CheCheckBox btnMute;
  public MACTrackBar macTrackBar1;
  private Label lblVolume;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem toolStripMenuItem1;
  private ToolStripMenuItem toolStripMenuItem2;
  private ToolStripMenuItem toolStripMenuItem3;
  private ToolStripMenuItem toolStripMenuItem4;
  public CheCheckBox btnLock;
  public Button btnTransfert;
  public CheCheckBox BtnShowProcess;
  private Label lblPId;
}