using System;
using System.ComponentModel;
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
      this.leftLedBar = new LedBar();
      this.volumeMACTrackBar = new MACTrackBar();
      this.volumeLabel = new Label();
      this.muteCheCheckBox = new CheCheckBox();
      this.BtnShowProcess = new CheCheckBox();
      this.btnTransfert = new Button();
      this.btnLock = new CheCheckBox();
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
      this.nameLabel.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.nameLabel.ForeColor = System.Drawing.Color.DimGray;
      this.nameLabel.Location = new System.Drawing.Point(2, 18);
      this.nameLabel.Margin = new Padding(0);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(96, 60);
      this.nameLabel.TabIndex = 3;
      this.nameLabel.Text = "Temp Text";
      this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // iconPictureBox
      this.iconPictureBox.BackColor = System.Drawing.Color.Transparent;
      this.iconPictureBox.Location = new System.Drawing.Point(33, 79);
      this.iconPictureBox.Margin = new Padding(0);
      this.iconPictureBox.Name = "iconPictureBox";
      this.iconPictureBox.Padding = new Padding(1, 1, 0, 0);
      this.iconPictureBox.Size = new System.Drawing.Size(34, 34);
      this.iconPictureBox.TabIndex = 0;
      this.iconPictureBox.TabStop = false;
      this.iconPictureBox.Click += new System.EventHandler(IconBox_Click);
      // pidLabel
      this.pidLabel.Font = new System.Drawing.Font("Verdana", 6.25f, System.Drawing.FontStyle.Bold);
      this.pidLabel.ForeColor = System.Drawing.Color.DimGray;
      this.pidLabel.Location = new System.Drawing.Point(2, 113);
      this.pidLabel.Name = "pidLabel";
      this.pidLabel.Size = new System.Drawing.Size(96, 13);
      this.pidLabel.TabIndex = 16;
      this.pidLabel.Text = "label1";
      this.pidLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.pidLabel.Visible = false;
      // leftLedBar
      this.leftLedBar.BackColor = System.Drawing.SystemColors.ScrollBar;
      this.leftLedBar.Color = true;
      this.leftLedBar.Location = new System.Drawing.Point(22, 132);
      this.leftLedBar.Name = "leftLedBar";
      this.leftLedBar.Size = new System.Drawing.Size(5, 225);
      this.leftLedBar.TabIndex = 1;
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
      this.muteCheCheckBox.Location = new System.Drawing.Point(13, 400);
      this.muteCheCheckBox.Name = "muteCheCheckBox";
      this.muteCheCheckBox.Padding = new Padding(0, 0, 2, 2);
      this.muteCheCheckBox.Size = new System.Drawing.Size(34, 34);
      this.muteCheCheckBox.TabIndex = 7;
      this.muteCheCheckBox.UseVisualStyleBackColor = false;
      this.muteCheCheckBox.CheckedChanged += new System.EventHandler(btnMute_CheckedChanged);
      this.muteCheCheckBox.MouseEnter += new System.EventHandler(btnMute_MouseEnter);
      this.muteCheCheckBox.MouseLeave += new System.EventHandler(btnMute_MouseLeave);
      // BtnShowProcess
      this.BtnShowProcess.Appearance = Appearance.Button;
      this.BtnShowProcess.AutoSize = true;
      this.BtnShowProcess.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.BtnShowProcess.Cursor = Cursors.Hand;
      this.BtnShowProcess.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
      this.BtnShowProcess.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.BtnShowProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.BtnShowProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.BtnShowProcess.FlatStyle = FlatStyle.Flat;
      this.BtnShowProcess.Image = CheVolume.Properties.Resources.showwindow;
      this.BtnShowProcess.Location = new System.Drawing.Point(53, 400);
      this.BtnShowProcess.Name = "BtnShowProcess";
      this.BtnShowProcess.Padding = new Padding(0, 0, 2, 2);
      this.BtnShowProcess.Size = new System.Drawing.Size(34, 34);
      this.BtnShowProcess.TabIndex = 15;
      this.BtnShowProcess.UseVisualStyleBackColor = false;
      this.BtnShowProcess.Click += new System.EventHandler(BtnShowProcess_Click);
      this.BtnShowProcess.MouseEnter += new System.EventHandler(BtnShowProcess_MouseEnter);
      this.BtnShowProcess.MouseLeave += new System.EventHandler(BtnShowProcess_MouseLeave);
      this.BtnShowProcess.MouseHover += new System.EventHandler(BtnShowProcess_MouseHover);
      // btnTransfert
      this.btnTransfert.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.btnTransfert.BackgroundImage = CheVolume.Properties.Resources.CheV;
      this.btnTransfert.BackgroundImageLayout = ImageLayout.Center;
      this.btnTransfert.Cursor = Cursors.Hand;
      this.btnTransfert.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
      this.btnTransfert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.btnTransfert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.btnTransfert.FlatStyle = FlatStyle.Flat;
      this.btnTransfert.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
      this.btnTransfert.Location = new System.Drawing.Point(33, 440);
      this.btnTransfert.Name = "btnTransfert";
      this.btnTransfert.Padding = new Padding(0, 0, 2, 2);
      this.btnTransfert.Size = new System.Drawing.Size(34, 34);
      this.btnTransfert.TabIndex = 14;
      this.btnTransfert.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnTransfert.UseVisualStyleBackColor = false;
      this.btnTransfert.Click += new System.EventHandler(btnTransfert_Click);
      this.btnTransfert.MouseEnter += new System.EventHandler(btnTransfert_MouseEnter);
      // btnLock
      this.btnLock.Appearance = Appearance.Button;
      this.btnLock.AutoSize = true;
      this.btnLock.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.btnLock.Cursor = Cursors.Hand;
      this.btnLock.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
      this.btnLock.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.btnLock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.btnLock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.btnLock.FlatStyle = FlatStyle.Flat;
      this.btnLock.Image = CheVolume.Properties.Resources.lockoff;
      this.btnLock.Location = new System.Drawing.Point(73, 440);
      this.btnLock.Name = "btnLock";
      this.btnLock.Padding = new Padding(0, 0, 2, 2);
      this.btnLock.Size = new System.Drawing.Size(34, 34);
      this.btnLock.TabIndex = 15;
      this.btnLock.UseVisualStyleBackColor = false;
      this.btnLock.Visible = false;
      this.btnLock.CheckedChanged += new System.EventHandler(btnLock_CheckedChanged);
      // contextMenuStrip1
      this.contextMenuStrip1.Font = new System.Drawing.Font("Verdana", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
      this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[4] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3, this.toolStripMenuItem4 });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.RenderMode = ToolStripRenderMode.Professional;
      this.contextMenuStrip1.Size = new System.Drawing.Size(204, 114);
      this.contextMenuStrip1.Closed += new ToolStripDropDownClosedEventHandler(contextMenuStrip1_Closed);
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
      base.Controls.Add(this.pidLabel);
      base.Controls.Add(this.BtnShowProcess);
      base.Controls.Add(this.btnLock);
      base.Controls.Add(this.btnTransfert);
      base.Controls.Add(this.volumeLabel);
      base.Controls.Add(this.volumeMACTrackBar);
      base.Controls.Add(this.muteCheCheckBox);
      base.Controls.Add(this.nameLabel);
      base.Controls.Add(this.leftLedBar);
      base.Controls.Add(this.iconPictureBox);
      base.Margin = new Padding(0);
      base.Name = "VSessionVolumeControl";
      base.Size = new System.Drawing.Size(100, 485);
      base.Load += new System.EventHandler(_OnLoad);
      /************************************************/
      this.contextMenuStrip1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)this.iconPictureBox).EndInit();
      base.ResumeLayout(false);
      base.PerformLayout();
    }
    /************************************************/
    public Label nameLabel = null;
    public PictureBox iconPictureBox = null;
    private Label pidLabel = null;
    public LedBar leftLedBar = null;
    public MACTrackBar volumeMACTrackBar = null;
    private Label volumeLabel = null;
    public CheCheckBox muteCheCheckBox = null;
    public CheCheckBox BtnShowProcess;
    public Button btnTransfert;
    public CheCheckBox btnLock;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem toolStripMenuItem2;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem toolStripMenuItem4;
  }
}