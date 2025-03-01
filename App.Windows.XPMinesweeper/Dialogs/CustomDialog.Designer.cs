using System;
using System.ComponentModel;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Dialogs
{
  partial class frmCustomGame
  {
    private Container components = null;
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
      this.lblHeight = new Label();
      this.lblWidth = new Label();
      this.lblMineCount = new Label();
      this.tbHeight = new TextBox();
      this.tbWidth = new TextBox();
      this.tbMineCount = new TextBox();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.SuspendLayout();
      // 
      // lblHeight
      // 
      this.lblHeight.AutoSize = true;
      this.lblHeight.Location = new System.Drawing.Point(12, 24);
      this.lblHeight.Name = "lblHeight";
      this.lblHeight.Size = new System.Drawing.Size(54, 17);
      this.lblHeight.TabIndex = 0;
      this.lblHeight.Text = "詢僅(&H):";
      // 
      // lblWidth
      // 
      this.lblWidth.AutoSize = true;
      this.lblWidth.Location = new System.Drawing.Point(12, 52);
      this.lblWidth.Name = "lblWidth";
      this.lblWidth.Size = new System.Drawing.Size(54, 17);
      this.lblWidth.TabIndex = 1;
      this.lblWidth.Text = "遵僅(&W):";
      // 
      // lblMineCount
      // 
      this.lblMineCount.AutoSize = true;
      this.lblMineCount.Location = new System.Drawing.Point(12, 80);
      this.lblMineCount.Name = "lblMineCount";
      this.lblMineCount.Size = new System.Drawing.Size(54, 17);
      this.lblMineCount.TabIndex = 2;
      this.lblMineCount.Text = "濘杅(&M):";
      // 
      // tbHeight
      // 
      this.tbHeight.BorderStyle = BorderStyle.FixedSingle;
      this.tbHeight.Location = new System.Drawing.Point(67, 20);
      this.tbHeight.MaxLength = 5;
      this.tbHeight.Name = "tbHeight";
      this.tbHeight.Size = new System.Drawing.Size(40, 21);
      this.tbHeight.TabIndex = 3;
      this.tbHeight.Text = "";
      // 
      // tbWidth
      // 
      this.tbWidth.BorderStyle = BorderStyle.FixedSingle;
      this.tbWidth.Location = new System.Drawing.Point(67, 48);
      this.tbWidth.MaxLength = 5;
      this.tbWidth.Name = "tbWidth";
      this.tbWidth.Size = new System.Drawing.Size(40, 21);
      this.tbWidth.TabIndex = 4;
      this.tbWidth.Text = "";
      // 
      // tbMineCount
      // 
      this.tbMineCount.BorderStyle = BorderStyle.FixedSingle;
      this.tbMineCount.Location = new System.Drawing.Point(67, 76);
      this.tbMineCount.MaxLength = 5;
      this.tbMineCount.Name = "tbMineCount";
      this.tbMineCount.Size = new System.Drawing.Size(40, 21);
      this.tbMineCount.TabIndex = 5;
      this.tbMineCount.Text = "";
      // 
      // btnOK
      // 
      this.btnOK.FlatStyle = FlatStyle.Popup;
      this.btnOK.Location = new System.Drawing.Point(124, 32);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(48, 23);
      this.btnOK.TabIndex = 6;
      this.btnOK.Text = "隅";
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.FlatStyle = FlatStyle.Popup;
      this.btnCancel.Location = new System.Drawing.Point(124, 66);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(48, 23);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "秏";
      // 
      // frmCustomGame
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(192, 121);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.tbMineCount);
      this.Controls.Add(this.tbWidth);
      this.Controls.Add(this.tbHeight);
      this.Controls.Add(this.lblMineCount);
      this.Controls.Add(this.lblWidth);
      this.Controls.Add(this.lblHeight);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.HelpButton = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmCustomGame";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Show;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "赻隅砱濘";
      this.ResumeLayout(false);
    }
    /************************************************/
    private Label lblHeight;
    private Label lblWidth;
    private Label lblMineCount;
    internal TextBox tbHeight;
    internal TextBox tbWidth;
    internal TextBox tbMineCount;
    private Button btnOK;
    private Button btnCancel;
  }
}