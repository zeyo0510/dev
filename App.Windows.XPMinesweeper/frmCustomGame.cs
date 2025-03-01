using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Minesweeper
{
	/// <summary>
	/// frmCustomGame ��ժҪ˵����
	/// </summary>
	public class frmCustomGame : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblHeight;
		private System.Windows.Forms.Label lblWidth;
		private System.Windows.Forms.Label lblMineCount;
		internal System.Windows.Forms.TextBox tbHeight;
		internal System.Windows.Forms.TextBox tbWidth;
		internal System.Windows.Forms.TextBox tbMineCount;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCustomGame()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
		}

		private static int strToInt(string text)
		{
			if (Regex.IsMatch(text, @"^-?\d+$"))
				return Int32.Parse(text);
			else
				return 0;
		}

		public static bool ShowSelf(IWin32Window parent, Point location, ref int width, ref int height, ref int mineCount)
		{
			bool result;
			frmCustomGame cg = new frmCustomGame();
			cg.tbHeight.Text = height.ToString();
			cg.tbWidth.Text = width.ToString();
			cg.tbMineCount.Text = mineCount.ToString();
			cg.Location = location;
			if (cg.ShowDialog(parent) == DialogResult.OK)
			{
				width = strToInt(cg.tbWidth.Text);
				height = strToInt(cg.tbHeight.Text);
				mineCount = strToInt(cg.tbMineCount.Text);
				result = true;
			}
			else
				result = false;
			cg.Dispose();
			cg = null;
			return result;
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.lblHeight = new System.Windows.Forms.Label();
			this.lblWidth = new System.Windows.Forms.Label();
			this.lblMineCount = new System.Windows.Forms.Label();
			this.tbHeight = new System.Windows.Forms.TextBox();
			this.tbWidth = new System.Windows.Forms.TextBox();
			this.tbMineCount = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblHeight
			// 
			this.lblHeight.AutoSize = true;
			this.lblHeight.Location = new System.Drawing.Point(12, 24);
			this.lblHeight.Name = "lblHeight";
			this.lblHeight.Size = new System.Drawing.Size(54, 17);
			this.lblHeight.TabIndex = 0;
			this.lblHeight.Text = "�߶�(&H):";
			// 
			// lblWidth
			// 
			this.lblWidth.AutoSize = true;
			this.lblWidth.Location = new System.Drawing.Point(12, 52);
			this.lblWidth.Name = "lblWidth";
			this.lblWidth.Size = new System.Drawing.Size(54, 17);
			this.lblWidth.TabIndex = 1;
			this.lblWidth.Text = "���(&W):";
			// 
			// lblMineCount
			// 
			this.lblMineCount.AutoSize = true;
			this.lblMineCount.Location = new System.Drawing.Point(12, 80);
			this.lblMineCount.Name = "lblMineCount";
			this.lblMineCount.Size = new System.Drawing.Size(54, 17);
			this.lblMineCount.TabIndex = 2;
			this.lblMineCount.Text = "����(&M):";
			// 
			// tbHeight
			// 
			this.tbHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbHeight.Location = new System.Drawing.Point(67, 20);
			this.tbHeight.MaxLength = 5;
			this.tbHeight.Name = "tbHeight";
			this.tbHeight.Size = new System.Drawing.Size(40, 21);
			this.tbHeight.TabIndex = 3;
			this.tbHeight.Text = "";
			// 
			// tbWidth
			// 
			this.tbWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbWidth.Location = new System.Drawing.Point(67, 48);
			this.tbWidth.MaxLength = 5;
			this.tbWidth.Name = "tbWidth";
			this.tbWidth.Size = new System.Drawing.Size(40, 21);
			this.tbWidth.TabIndex = 4;
			this.tbWidth.Text = "";
			// 
			// tbMineCount
			// 
			this.tbMineCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbMineCount.Location = new System.Drawing.Point(67, 76);
			this.tbMineCount.MaxLength = 5;
			this.tbMineCount.Name = "tbMineCount";
			this.tbMineCount.Size = new System.Drawing.Size(40, 21);
			this.tbMineCount.TabIndex = 5;
			this.tbMineCount.Text = "";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnOK.Location = new System.Drawing.Point(124, 32);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(48, 23);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "ȷ��";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnCancel.Location = new System.Drawing.Point(124, 66);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(48, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "ȡ��";
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
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmCustomGame";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "�Զ�������";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	}
}
