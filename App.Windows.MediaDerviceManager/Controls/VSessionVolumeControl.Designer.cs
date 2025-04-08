using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EConTech.Windows.MACUI;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VSessionVolumeControl
  {
    private IContainer components;
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
      this.components = new Container();
      /************************************************/
      this.nameLabel = new Label();
      this.iconPictureBox = new PictureBox();
      this.pidLabel = new Label();
      this.leftVLedBar = new VLedBar();
      this.volumeMACTrackBar = new MACTrackBar();
      this.volumeLabel = new Label();
      this.muteCheckBox = new CheckBox();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.toolStripMenuItem1 = new ToolStripMenuItem();
      this.toolStripMenuItem2 = new ToolStripMenuItem();
      this.toolStripMenuItem3 = new ToolStripMenuItem();
      this.toolStripMenuItem4 = new ToolStripMenuItem();
      /************************************************/
      this.contextMenuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).BeginInit();
      base.SuspendLayout();
      /************************************************/
      // nameLabel
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.nameLabel.ForeColor = System.Drawing.Color.DimGray;
      this.nameLabel.Location = new System.Drawing.Point(2, 18);
      this.nameLabel.Margin = new Padding(0);
      this.nameLabel.Size = new System.Drawing.Size(96, 60);
      this.nameLabel.TabIndex = 3;
      this.nameLabel.Text = "Temp Text";
      this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // iconPictureBox
      this.iconPictureBox.Name = "iconPictureBox";
      this.iconPictureBox.BackColor = System.Drawing.Color.Transparent;
      this.iconPictureBox.Location = new System.Drawing.Point(33, 79);
      this.iconPictureBox.Margin = new Padding(0);
      this.iconPictureBox.Padding = new Padding(1, 1, 0, 0);
      this.iconPictureBox.Size = new System.Drawing.Size(34, 34);
      this.iconPictureBox.TabIndex = 0;
      this.iconPictureBox.TabStop = false;
      this.iconPictureBox.MouseClick += iconPictureBox_MouseClick;
      // pidLabel
      this.pidLabel.Name = "pidLabel";
      this.pidLabel.Font = new System.Drawing.Font("Verdana", 6.25f, System.Drawing.FontStyle.Bold);
      this.pidLabel.ForeColor = System.Drawing.Color.DimGray;
      this.pidLabel.Location = new System.Drawing.Point(2, 113);
      this.pidLabel.Size = new System.Drawing.Size(96, 13);
      this.pidLabel.TabIndex = 16;
      this.pidLabel.Text = "label1";
      this.pidLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.pidLabel.Visible = true;
      // leftVLedBar
      this.leftVLedBar.Name = "leftVLedBar";
      this.leftVLedBar.Location = new Point(22, 132);
      // volumeMACTrackBar
      this.volumeMACTrackBar.BackColor = System.Drawing.Color.Transparent;
      this.volumeMACTrackBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
      this.volumeMACTrackBar.Cursor = Cursors.Hand;
      this.volumeMACTrackBar.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.volumeMACTrackBar.ForeColor = System.Drawing.Color.FromArgb(123, 125, 123);
      this.volumeMACTrackBar.IndentHeight = 6;
      this.volumeMACTrackBar.LargeChange = 1;
      this.volumeMACTrackBar.Location = new System.Drawing.Point(30, 121);
      this.volumeMACTrackBar.Maximum = 100;
      this.volumeMACTrackBar.Minimum = 0;
      this.volumeMACTrackBar.Name = "volumeMACTrackBar";
      this.volumeMACTrackBar.Orientation = Orientation.Vertical;
      this.volumeMACTrackBar.Size = new System.Drawing.Size(58, 247);
      this.volumeMACTrackBar.TabIndex = 10;
      this.volumeMACTrackBar.TickColor = System.Drawing.Color.FromArgb(148, 146, 148);
      this.volumeMACTrackBar.TickFrequency = 10;
      this.volumeMACTrackBar.TickHeight = 4;
      this.volumeMACTrackBar.TickStyle = TickStyle.Both;
      this.volumeMACTrackBar.TrackerColor = System.Drawing.Color.FromArgb(255, 128, 0);
      this.volumeMACTrackBar.TrackerSize = new System.Drawing.Size(16, 16);
      this.volumeMACTrackBar.TrackLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
      this.volumeMACTrackBar.TrackLineHeight = 3;
      this.volumeMACTrackBar.Value = 0;
      this.volumeMACTrackBar.ValueChanged += macTrackBar1_ValueChanged;
      // volumeLabel
      this.volumeLabel.BackColor = System.Drawing.Color.Transparent;
      this.volumeLabel.BorderStyle = BorderStyle.FixedSingle;
      this.volumeLabel.Font = new System.Drawing.Font("Verdana", 7f, System.Drawing.FontStyle.Bold);
      this.volumeLabel.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
      this.volumeLabel.Location = new System.Drawing.Point(30, 371);
      this.volumeLabel.Name = "lblVolume";
      this.volumeLabel.Size = new System.Drawing.Size(40, 20);
      this.volumeLabel.TabIndex = 11;
      this.volumeLabel.Text = "100";
      this.volumeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.volumeLabel.UseCompatibleTextRendering = true;
      this.volumeLabel.UseMnemonic = false;
      // muteCheckBox
      this.muteCheckBox.Name     = "muteCheckBox";
      this.muteCheckBox.AutoSize = true;
      this.muteCheckBox.Location = new Point(13, 400);
      this.muteCheckBox.Text     = "Mute";
      this.muteCheckBox.CheckedChanged += this.muteCheckBox_CheckedChanged;
      // contextMenuStrip1
      this.contextMenuStrip1.Font = new System.Drawing.Font("Verdana", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
      this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[4] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3, this.toolStripMenuItem4 });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.RenderMode = ToolStripRenderMode.Professional;
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
      // VSessionVolumeControl
      base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      base.AutoScaleMode = AutoScaleMode.Font;
      base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Font                = new Font("Verdana", 10f, FontStyle.Bold, GraphicsUnit.World, 0);
      base.Margin = new Padding(0);
      base.Name = "VSessionVolumeControl";
      base.Size = new System.Drawing.Size(100, 485);
      base.Controls.Add(this.pidLabel);
      base.Controls.Add(this.volumeLabel);
      base.Controls.Add(this.volumeMACTrackBar);
      base.Controls.Add(this.muteCheckBox);
      base.Controls.Add(this.nameLabel);
      base.Controls.Add(this.leftVLedBar);
      base.Controls.Add(this.iconPictureBox);
      base.Load += new System.EventHandler(_OnLoad);
      /************************************************/
      this.contextMenuStrip1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).EndInit();
      base.ResumeLayout(false);
      base.PerformLayout();
    }
    /************************************************/
    private Label nameLabel = null;
    private PictureBox iconPictureBox = null;
    private Label pidLabel = null;
    private VLedBar leftVLedBar = null;
    public MACTrackBar volumeMACTrackBar = null;
    private Label volumeLabel = null;
    public CheckBox muteCheckBox = null;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem toolStripMenuItem4;
  }
}