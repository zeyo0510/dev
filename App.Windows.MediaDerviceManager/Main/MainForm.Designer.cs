using System;
using System.ComponentModel;
using System.Windows.Forms;
using a;
using CheVolume.Controls;
/************************************************/
namespace App.Windows.MediaDerviceManager.Main
{
  partial class MainForm
  {
    private IContainer components;
    /************************************************/
    private AudioSessionManagerPanel pnlSessMgr;
    /************************************************/
    protected override void Dispose(bool P_0)
    {
      if (P_0 && components != null)
      {
        components.Dispose();
      }
      base.Dispose(P_0);
    }
    /************************************************/
    private void InitializeComponent()
    {
      this.components = new Container();
      /************************************************/
      this.systemButton = new Button();
      this.cheCheckBox2 = new CheCheckBox();
      this.cheCheckBox1 = new CheCheckBox();
      this.pnlSessMgr = new a.AudioSessionManagerPanel();
      this.toolTip1 = new ToolTip(this.components);
      this.exitButton = new Button();
      /************************************************/
      base.SuspendLayout();
      /************************************************/
      this.systemButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.systemButton.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.systemButton.Cursor = Cursors.Hand;
      this.systemButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
      this.systemButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.systemButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.systemButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.systemButton.FlatStyle = FlatStyle.Flat;
      this.systemButton.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
      this.systemButton.Image = CheVolume.Properties.Resources.options;
      this.systemButton.Location = new System.Drawing.Point(923, 15);
      this.systemButton.Margin = new Padding(4);
      this.systemButton.Name = "systemButton";
      this.systemButton.Padding = new Padding(0, 0, 3, 2);
      this.systemButton.Size = new System.Drawing.Size(45, 42);
      this.systemButton.TabIndex = 8;
      this.systemButton.UseVisualStyleBackColor = false;
      this.systemButton.Click += new System.EventHandler(systemButton_Click);
      this.cheCheckBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cheCheckBox2.Appearance = Appearance.Button;
      this.cheCheckBox2.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.cheCheckBox2.Cursor = Cursors.Hand;
      this.cheCheckBox2.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
      this.cheCheckBox2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.cheCheckBox2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.cheCheckBox2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.cheCheckBox2.FlatStyle = FlatStyle.Flat;
      this.cheCheckBox2.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold);
      this.cheCheckBox2.Image = CheVolume.Properties.Resources.Pinned;
      this.cheCheckBox2.Location = new System.Drawing.Point(763, 15);
      this.cheCheckBox2.Margin = new Padding(4);
      this.cheCheckBox2.Name = "cheCheckBox2";
      this.cheCheckBox2.Size = new System.Drawing.Size(45, 42);
      this.cheCheckBox2.TabIndex = 15;
      this.cheCheckBox2.UseVisualStyleBackColor = false;
      this.cheCheckBox2.CheckedChanged += new System.EventHandler(cheCheckBox2_CheckedChanged);
      this.cheCheckBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cheCheckBox1.Appearance = Appearance.Button;
      this.cheCheckBox1.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.cheCheckBox1.Cursor = Cursors.Hand;
      this.cheCheckBox1.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
      this.cheCheckBox1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.cheCheckBox1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.cheCheckBox1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.cheCheckBox1.FlatStyle = FlatStyle.Flat;
      this.cheCheckBox1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold);
      this.cheCheckBox1.Image = CheVolume.Properties.Resources.AdvancedUser;
      this.cheCheckBox1.Location = new System.Drawing.Point(816, 15);
      this.cheCheckBox1.Margin = new Padding(4);
      this.cheCheckBox1.Name = "cheCheckBox1";
      this.cheCheckBox1.Size = new System.Drawing.Size(45, 42);
      this.cheCheckBox1.TabIndex = 13;
      this.cheCheckBox1.UseVisualStyleBackColor = false;
      this.cheCheckBox1.CheckedChanged += new System.EventHandler(cheCheckBox1_CheckedChanged);
      this.pnlSessMgr.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.pnlSessMgr.AutoScroll = true;
      this.pnlSessMgr.AutoSize = true;
      this.pnlSessMgr.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.pnlSessMgr.Location = new System.Drawing.Point(15, 142);
      this.pnlSessMgr.Margin = new Padding(0, 0, 27, 0);
      this.pnlSessMgr.MinimumSize = new System.Drawing.Size(747, 625);
      this.pnlSessMgr.Name = "pnlSessMgr";
      this.pnlSessMgr.PenColor = System.Drawing.Color.Empty;
      this.pnlSessMgr.Size = new System.Drawing.Size(747, 625);
      this.pnlSessMgr.TabIndex = 2;
      this.pnlSessMgr.Thickness = 0;
      this.pnlSessMgr.WrapContents = false;
      this.exitButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.exitButton.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.exitButton.Cursor = Cursors.Hand;
      this.exitButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
      this.exitButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
      this.exitButton.FlatStyle = FlatStyle.Flat;
      this.exitButton.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
      this.exitButton.Image = CheVolume.Properties.Resources.close;
      this.exitButton.Location = new System.Drawing.Point(976, 15);
      this.exitButton.Margin = new Padding(4);
      this.exitButton.Name = "exitButton";
      this.exitButton.Padding = new Padding(0, 0, 3, 2);
      this.exitButton.Size = new System.Drawing.Size(45, 42);
      this.exitButton.TabIndex = 16;
      this.exitButton.UseVisualStyleBackColor = false;
      this.exitButton.Click += new System.EventHandler(exitButton_Click);
      base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
      base.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      base.ClientSize = new System.Drawing.Size(1045, 756);
      base.Controls.Add(this.exitButton);
      base.Controls.Add(this.cheCheckBox2);
      base.Controls.Add(this.cheCheckBox1);
      base.Controls.Add(this.systemButton);
      base.Controls.Add(this.pnlSessMgr);
      this.DoubleBuffered = true;
      base.Margin = new Padding(4);
      this.MaximumSize = new System.Drawing.Size(3994, 803);
      this.MinimumSize = new System.Drawing.Size(794, 803);
      base.Name = "Main";
      base.SizeGripStyle = SizeGripStyle.Show;
      this.Text = "CheVolume";
      base.FormClosing += new FormClosingEventHandler(_OnFormClosing);
      /************************************************/
      base.ResumeLayout(false);
      base.PerformLayout();
    }
    /************************************************/
    private Button systemButton;
    private CheCheckBox cheCheckBox1;
    private CheCheckBox cheCheckBox2;
    private ToolTip toolTip1;
    private Button exitButton;
  }
}