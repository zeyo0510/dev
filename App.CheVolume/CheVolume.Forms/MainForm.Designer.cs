using a;
using CheVolume.Controls;
using System.ComponentModel;
/************************************************/
namespace CheVolume.Forms;
/************************************************/
partial class MainForm : Form
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
    this.components = new System.ComponentModel.Container();
    // System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheVolume.Forms.Main));
    this.topmostCheckBox = new CheVolume.Controls.CheCheckBox();
    this.advuserCheckBox = new CheVolume.Controls.CheCheckBox();
    this.settingButton = new System.Windows.Forms.Button();
    this.closeButton = new System.Windows.Forms.Button();
    this.pnlSessMgr = new a.AudioSessionManagerPanel();
    base.SuspendLayout();
    /************************************************/
    // topmostCheckBox
    this.topmostCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
    this.topmostCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
    this.topmostCheckBox.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.topmostCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
    this.topmostCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.topmostCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.topmostCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.topmostCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.topmostCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.topmostCheckBox.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold);
    this.topmostCheckBox.Image = CheVolume.Properties.Resources.Pinned;
    this.topmostCheckBox.Location = new System.Drawing.Point(763, 15);
    this.topmostCheckBox.Margin = new System.Windows.Forms.Padding(4);
    this.topmostCheckBox.Name = "topmostCheckBox";
    this.topmostCheckBox.Size = new System.Drawing.Size(45, 42);
    this.topmostCheckBox.TabIndex = 15;
    this.topmostCheckBox.UseVisualStyleBackColor = false;
    this.topmostCheckBox.CheckedChanged += new System.EventHandler(topmostCheckBox_CheckedChanged);
    // advuserCheckBox
    this.advuserCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
    this.advuserCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
    this.advuserCheckBox.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.advuserCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
    this.advuserCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.advuserCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.advuserCheckBox.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.advuserCheckBox.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.advuserCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.advuserCheckBox.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold);
    this.advuserCheckBox.Image = CheVolume.Properties.Resources.AdvancedUser;
    this.advuserCheckBox.Location = new System.Drawing.Point(816, 15);
    this.advuserCheckBox.Margin = new System.Windows.Forms.Padding(4);
    this.advuserCheckBox.Name = "advuserCheckBox";
    this.advuserCheckBox.Size = new System.Drawing.Size(45, 42);
    this.advuserCheckBox.TabIndex = 13;
    this.advuserCheckBox.UseVisualStyleBackColor = false;
    this.advuserCheckBox.CheckedChanged += new System.EventHandler(advuserCheckBox_CheckedChanged);
    // closeButton
    this.closeButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
    this.closeButton.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
    this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.closeButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.closeButton.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
    this.closeButton.Image = CheVolume.Properties.Resources.close;
    this.closeButton.Location = new System.Drawing.Point(976, 15);
    this.closeButton.Margin = new System.Windows.Forms.Padding(4);
    this.closeButton.Name = "closeButton";
    this.closeButton.Padding = new System.Windows.Forms.Padding(0, 0, 3, 2);
    this.closeButton.Size = new System.Drawing.Size(45, 42);
    this.closeButton.TabIndex = 16;
    this.closeButton.UseVisualStyleBackColor = false;
    this.closeButton.Click += new System.EventHandler(closeButton_Click);
    // settingButton
    this.settingButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
    this.settingButton.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.settingButton.Cursor = System.Windows.Forms.Cursors.Hand;
    this.settingButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
    this.settingButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.settingButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.settingButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
    this.settingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.settingButton.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
    this.settingButton.Image = CheVolume.Properties.Resources.options;
    this.settingButton.Location = new System.Drawing.Point(923, 15);
    this.settingButton.Margin = new System.Windows.Forms.Padding(4);
    this.settingButton.Name = "settingButton";
    this.settingButton.Padding = new System.Windows.Forms.Padding(0, 0, 3, 2);
    this.settingButton.Size = new System.Drawing.Size(45, 42);
    this.settingButton.TabIndex = 8;
    this.settingButton.UseVisualStyleBackColor = false;
    this.settingButton.Click += new System.EventHandler(settingButton_Click);
    // pnlSessMgr
    this.pnlSessMgr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
    this.pnlSessMgr.AutoScroll = true;
    this.pnlSessMgr.AutoSize = true;
    this.pnlSessMgr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
    this.pnlSessMgr.Location = new System.Drawing.Point(15, 142);
    this.pnlSessMgr.Margin = new System.Windows.Forms.Padding(0, 0, 27, 0);
    this.pnlSessMgr.MinimumSize = new System.Drawing.Size(747, 625);
    this.pnlSessMgr.Name = "pnlSessMgr";
    this.pnlSessMgr.PenColor = System.Drawing.Color.Empty;
    this.pnlSessMgr.Size = new System.Drawing.Size(747, 625);
    this.pnlSessMgr.TabIndex = 2;
    this.pnlSessMgr.Thickness = 0;
    this.pnlSessMgr.WrapContents = false;
    this.pnlSessMgr.SizeChanged += new System.EventHandler(pnlSessMgr_SizeChanged);
    this.pnlSessMgr.ControlAdded += new System.Windows.Forms.ControlEventHandler(pnlSessMgr_ControlAdded);
    // MainForm
    base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
    base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.BackColor = System.Drawing.Color.White;
    base.ClientSize = new System.Drawing.Size(1045, 756);
    base.Controls.Add(this.closeButton);
    base.Controls.Add(this.topmostCheckBox);
    base.Controls.Add(this.advuserCheckBox);
    base.Controls.Add(this.settingButton);
    base.Controls.Add(this.pnlSessMgr);
    this.DoubleBuffered = true;
    // base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
    base.Margin = new System.Windows.Forms.Padding(4);
    this.MaximumSize = new System.Drawing.Size(3994, 803);
    this.MinimumSize = new System.Drawing.Size(794, 803);
    base.Name = "MainForm";
    base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
    this.Text = "CheVolume";
    base.ResumeLayout(false);
    base.PerformLayout();
  }
  /************************************************/
  private CheCheckBox topmostCheckBox;
  private CheCheckBox advuserCheckBox;
  private Button settingButton;
  private Button closeButton;
  private AudioSessionManagerPanel pnlSessMgr;
}