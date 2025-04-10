using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  partial class VSessionVolumeControl
  {
    private IContainer components = null;
    /************************************************/
    private Timer guiTimer = null;
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
      this.nameLabel          = new Label();
      this.iconPictureBox     = new PictureBox();
      this.pidLabel           = new Label();
      this.leftVLedBar        = new VLedBar();
      this.volumeVTrackBar    = new VTrackBar();
      this.volumeLabel        = new Label();
      this.muteCheckBox       = new CheckBox();
      this.contextMenuStrip1  = new ContextMenuStrip(this.components);
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
      this.nameLabel.Font = new Font("Verdana", 10f, FontStyle.Bold, GraphicsUnit.World, 0);
      this.nameLabel.ForeColor = Color.DimGray;
      this.nameLabel.Location = new Point(2, 18);
      this.nameLabel.Margin = new Padding(0);
      this.nameLabel.Size = new Size(96, 60);
      this.nameLabel.TabIndex = 3;
      this.nameLabel.Text = "Temp Text";
      this.nameLabel.TextAlign = ContentAlignment.MiddleCenter;
      // iconPictureBox
      this.iconPictureBox.Name = "iconPictureBox";
      this.iconPictureBox.BackColor = Color.Transparent;
      this.iconPictureBox.Location = new Point(33, 79);
      this.iconPictureBox.Margin = new Padding(0);
      this.iconPictureBox.Padding = new Padding(1, 1, 0, 0);
      this.iconPictureBox.Size = new Size(34, 34);
      this.iconPictureBox.TabIndex = 0;
      this.iconPictureBox.TabStop = false;
      this.iconPictureBox.MouseClick += iconPictureBox_MouseClick;
      // pidLabel
      this.pidLabel.Name = "pidLabel";
      this.pidLabel.Font = new Font("Verdana", 6.25f, FontStyle.Bold);
      this.pidLabel.ForeColor = Color.DimGray;
      this.pidLabel.Location = new Point(2, 113);
      this.pidLabel.Size = new Size(96, 13);
      this.pidLabel.TabIndex = 16;
      this.pidLabel.Text = "label1";
      this.pidLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.pidLabel.Visible = true;
      // leftVLedBar
      this.leftVLedBar.Name = "leftVLedBar";
      this.leftVLedBar.Location = new Point(22, 132);
      // volumeVTrackBar
      this.volumeVTrackBar.Name = "volumeVTrackBar";
      this.volumeVTrackBar.Location = new Point(30, 132);
      this.volumeVTrackBar.Size = new Size(030, 225);
      this.volumeVTrackBar.Direction = VDirection.BottomTop;
      this.volumeVTrackBar.ValueChanged += this.volumeVTrackBar_ValueChanged;
      // volumeLabel
      this.volumeLabel.BackColor = Color.Transparent;
      this.volumeLabel.BorderStyle = BorderStyle.FixedSingle;
      this.volumeLabel.Font = new Font("Verdana", 7f, FontStyle.Bold);
      this.volumeLabel.ForeColor = Color.FromArgb(64, 64, 64);
      this.volumeLabel.Location = new Point(30, 371);
      this.volumeLabel.Name = "lblVolume";
      this.volumeLabel.Size = new Size(40, 20);
      this.volumeLabel.TabIndex = 11;
      this.volumeLabel.Text = "100";
      this.volumeLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.volumeLabel.UseCompatibleTextRendering = true;
      this.volumeLabel.UseMnemonic = false;
      // muteCheckBox
      this.muteCheckBox.Name     = "muteCheckBox";
      this.muteCheckBox.AutoSize = true;
      this.muteCheckBox.Location = new Point(13, 400);
      this.muteCheckBox.Text     = "Mute";
      this.muteCheckBox.Click += muteCheckBox_Click;
      // contextMenuStrip1
      this.contextMenuStrip1.Font = new Font("Verdana", 11f, FontStyle.Bold, GraphicsUnit.World);
      this.contextMenuStrip1.ImageScalingSize = new Size(32, 32);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[4] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3, this.toolStripMenuItem4 });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.RenderMode = ToolStripRenderMode.Professional;
      // toolStripMenuItem1
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new Size(203, 22);
      this.toolStripMenuItem1.Text = "toolStripMenuItem1";
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new Size(203, 22);
      this.toolStripMenuItem2.Text = "toolStripMenuItem2";
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new Size(203, 22);
      this.toolStripMenuItem3.Text = "toolStripMenuItem3";
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new Size(203, 22);
      this.toolStripMenuItem4.Text = "toolStripMenuItem4";
      // VSessionVolumeControl
      base.AutoScaleDimensions = new SizeF(6f, 13f);
      base.AutoScaleMode = AutoScaleMode.Font;
      base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.BackColor = Color.Transparent;
      this.Font                = new Font("Verdana", 10f, FontStyle.Bold, GraphicsUnit.World, 0);
      base.Margin = new Padding(0);
      base.Name = "VSessionVolumeControl";
      base.Size = new Size(100, 485);
      base.Controls.Add(this.pidLabel);
      base.Controls.Add(this.volumeLabel);
      base.Controls.Add(this.volumeVTrackBar);
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
    private Label             nameLabel         = null;
    private PictureBox        iconPictureBox    = null;
    private Label             pidLabel          = null;
    private VLedBar           leftVLedBar       = null;
    private VTrackBar         volumeVTrackBar   = null;
    private Label             volumeLabel       = null;
    private CheckBox          muteCheckBox      = null;
    private ContextMenuStrip  contextMenuStrip1;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem toolStripMenuItem4;
  }
}