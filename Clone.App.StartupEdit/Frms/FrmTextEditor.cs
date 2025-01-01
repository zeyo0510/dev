using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace StartupEdit
{
	/// <summary>
	/// Summary description for FrmTextEditor.
	/// </summary>
	public class FrmTextEditor : System.Windows.Forms.Form
	{
		ClsAddOns MyAct = new ClsAddOns();
		
		#region Windows Delaration
		
		private System.Windows.Forms.TabControl TabTxtEdit;
		private System.Windows.Forms.TabPage PageSystemINI;
		private System.Windows.Forms.TabPage PageWinINI;
		private System.Windows.Forms.TabPage PageBootINI;
		private System.Windows.Forms.TabPage PageAutoEXEC;
		private System.Windows.Forms.TabPage PageConfigSys;
		private System.Windows.Forms.TabPage PageConfigNT;
		private System.Windows.Forms.TextBox TxtSystemINI;
		private System.Windows.Forms.TextBox TxtWinINI;
		private System.Windows.Forms.TextBox TxtBootINI;
		private System.Windows.Forms.TextBox TxtAutoExec;
		private System.Windows.Forms.TextBox TxtConfigSYS;
		private System.Windows.Forms.TextBox TxtConfigNT;
		private System.Windows.Forms.Panel ItarKedaOk;
		private System.Windows.Forms.Button CmdClose;
		private System.Windows.Forms.Button CmdOpen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button CmdEdit;
		private System.Windows.Forms.Label LblOr;
		private System.Windows.Forms.Label LblFileName;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmTextEditor()
		{
			InitializeComponent();
		}


		/// <summary>
		/// Clean up any resources being used.
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


		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.TabTxtEdit = new System.Windows.Forms.TabControl();
            this.PageSystemINI = new System.Windows.Forms.TabPage();
            this.TxtSystemINI = new System.Windows.Forms.TextBox();
            this.PageWinINI = new System.Windows.Forms.TabPage();
            this.TxtWinINI = new System.Windows.Forms.TextBox();
            this.PageBootINI = new System.Windows.Forms.TabPage();
            this.TxtBootINI = new System.Windows.Forms.TextBox();
            this.PageAutoEXEC = new System.Windows.Forms.TabPage();
            this.TxtAutoExec = new System.Windows.Forms.TextBox();
            this.PageConfigSys = new System.Windows.Forms.TabPage();
            this.TxtConfigSYS = new System.Windows.Forms.TextBox();
            this.PageConfigNT = new System.Windows.Forms.TabPage();
            this.TxtConfigNT = new System.Windows.Forms.TextBox();
            this.CmdClose = new System.Windows.Forms.Button();
            this.ItarKedaOk = new System.Windows.Forms.Panel();
            this.LblFileName = new System.Windows.Forms.Label();
            this.LblOr = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CmdOpen = new System.Windows.Forms.Button();
            this.CmdEdit = new System.Windows.Forms.Button();
            this.TabTxtEdit.SuspendLayout();
            this.PageSystemINI.SuspendLayout();
            this.PageWinINI.SuspendLayout();
            this.PageBootINI.SuspendLayout();
            this.PageAutoEXEC.SuspendLayout();
            this.PageConfigSys.SuspendLayout();
            this.PageConfigNT.SuspendLayout();
            this.ItarKedaOk.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabTxtEdit
            // 
            this.TabTxtEdit.Controls.Add(this.PageSystemINI);
            this.TabTxtEdit.Controls.Add(this.PageWinINI);
            this.TabTxtEdit.Controls.Add(this.PageBootINI);
            this.TabTxtEdit.Controls.Add(this.PageAutoEXEC);
            this.TabTxtEdit.Controls.Add(this.PageConfigSys);
            this.TabTxtEdit.Controls.Add(this.PageConfigNT);
            this.TabTxtEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabTxtEdit.Location = new System.Drawing.Point(0, 0);
            this.TabTxtEdit.Name = "TabTxtEdit";
            this.TabTxtEdit.SelectedIndex = 0;
            this.TabTxtEdit.Size = new System.Drawing.Size(672, 262);
            this.TabTxtEdit.TabIndex = 0;
            this.TabTxtEdit.SelectedIndexChanged += new System.EventHandler(this.TabTxtEdit_SelectedIndexChanged);
            // 
            // PageSystemINI
            // 
            this.PageSystemINI.Controls.Add(this.TxtSystemINI);
            this.PageSystemINI.Location = new System.Drawing.Point(4, 23);
            this.PageSystemINI.Name = "PageSystemINI";
            this.PageSystemINI.Size = new System.Drawing.Size(664, 235);
            this.PageSystemINI.TabIndex = 0;
            this.PageSystemINI.Text = "SYSTEM.INI";
            // 
            // TxtSystemINI
            // 
            this.TxtSystemINI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtSystemINI.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.TxtSystemINI.Location = new System.Drawing.Point(0, 0);
            this.TxtSystemINI.Multiline = true;
            this.TxtSystemINI.Name = "TxtSystemINI";
            this.TxtSystemINI.ReadOnly = true;
            this.TxtSystemINI.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtSystemINI.Size = new System.Drawing.Size(664, 235);
            this.TxtSystemINI.TabIndex = 2;
            this.TxtSystemINI.Tag = "SYSTEM.INI";
            // 
            // PageWinINI
            // 
            this.PageWinINI.Controls.Add(this.TxtWinINI);
            this.PageWinINI.Location = new System.Drawing.Point(4, 22);
            this.PageWinINI.Name = "PageWinINI";
            this.PageWinINI.Size = new System.Drawing.Size(664, 236);
            this.PageWinINI.TabIndex = 1;
            this.PageWinINI.Text = "WIN.INI";
            // 
            // TxtWinINI
            // 
            this.TxtWinINI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtWinINI.Location = new System.Drawing.Point(0, 0);
            this.TxtWinINI.Multiline = true;
            this.TxtWinINI.Name = "TxtWinINI";
            this.TxtWinINI.ReadOnly = true;
            this.TxtWinINI.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtWinINI.Size = new System.Drawing.Size(664, 236);
            this.TxtWinINI.TabIndex = 3;
            this.TxtWinINI.Tag = "WIN.INI";
            // 
            // PageBootINI
            // 
            this.PageBootINI.Controls.Add(this.TxtBootINI);
            this.PageBootINI.Location = new System.Drawing.Point(4, 22);
            this.PageBootINI.Name = "PageBootINI";
            this.PageBootINI.Size = new System.Drawing.Size(664, 236);
            this.PageBootINI.TabIndex = 2;
            this.PageBootINI.Text = "BOOT.INI";
            // 
            // TxtBootINI
            // 
            this.TxtBootINI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBootINI.Location = new System.Drawing.Point(0, 0);
            this.TxtBootINI.Multiline = true;
            this.TxtBootINI.Name = "TxtBootINI";
            this.TxtBootINI.ReadOnly = true;
            this.TxtBootINI.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtBootINI.Size = new System.Drawing.Size(664, 236);
            this.TxtBootINI.TabIndex = 3;
            this.TxtBootINI.Tag = "BOOT.INI";
            // 
            // PageAutoEXEC
            // 
            this.PageAutoEXEC.Controls.Add(this.TxtAutoExec);
            this.PageAutoEXEC.Location = new System.Drawing.Point(4, 22);
            this.PageAutoEXEC.Name = "PageAutoEXEC";
            this.PageAutoEXEC.Size = new System.Drawing.Size(664, 236);
            this.PageAutoEXEC.TabIndex = 3;
            this.PageAutoEXEC.Text = "AUTOEXEC.BAT";
            // 
            // TxtAutoExec
            // 
            this.TxtAutoExec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtAutoExec.Location = new System.Drawing.Point(0, 0);
            this.TxtAutoExec.Multiline = true;
            this.TxtAutoExec.Name = "TxtAutoExec";
            this.TxtAutoExec.ReadOnly = true;
            this.TxtAutoExec.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtAutoExec.Size = new System.Drawing.Size(664, 236);
            this.TxtAutoExec.TabIndex = 3;
            this.TxtAutoExec.Tag = "AUTOEXEC.BAT";
            // 
            // PageConfigSys
            // 
            this.PageConfigSys.Controls.Add(this.TxtConfigSYS);
            this.PageConfigSys.Location = new System.Drawing.Point(4, 22);
            this.PageConfigSys.Name = "PageConfigSys";
            this.PageConfigSys.Size = new System.Drawing.Size(664, 236);
            this.PageConfigSys.TabIndex = 4;
            this.PageConfigSys.Text = "CONFIG.SYS";
            // 
            // TxtConfigSYS
            // 
            this.TxtConfigSYS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtConfigSYS.Location = new System.Drawing.Point(0, 0);
            this.TxtConfigSYS.Multiline = true;
            this.TxtConfigSYS.Name = "TxtConfigSYS";
            this.TxtConfigSYS.ReadOnly = true;
            this.TxtConfigSYS.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtConfigSYS.Size = new System.Drawing.Size(664, 236);
            this.TxtConfigSYS.TabIndex = 3;
            this.TxtConfigSYS.Tag = "CONFIG.SYS";
            // 
            // PageConfigNT
            // 
            this.PageConfigNT.Controls.Add(this.TxtConfigNT);
            this.PageConfigNT.Location = new System.Drawing.Point(4, 22);
            this.PageConfigNT.Name = "PageConfigNT";
            this.PageConfigNT.Size = new System.Drawing.Size(664, 236);
            this.PageConfigNT.TabIndex = 5;
            this.PageConfigNT.Text = "CONFIG.NT";
            // 
            // TxtConfigNT
            // 
            this.TxtConfigNT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtConfigNT.Location = new System.Drawing.Point(0, 0);
            this.TxtConfigNT.Multiline = true;
            this.TxtConfigNT.Name = "TxtConfigNT";
            this.TxtConfigNT.ReadOnly = true;
            this.TxtConfigNT.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtConfigNT.Size = new System.Drawing.Size(664, 236);
            this.TxtConfigNT.TabIndex = 3;
            this.TxtConfigNT.Tag = "CONFIG.NT";
            // 
            // CmdClose
            // 
            this.CmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmdClose.Location = new System.Drawing.Point(560, 16);
            this.CmdClose.Name = "CmdClose";
            this.CmdClose.Size = new System.Drawing.Size(96, 40);
            this.CmdClose.TabIndex = 1;
            this.CmdClose.Text = "&Close";
            this.CmdClose.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // ItarKedaOk
            // 
            this.ItarKedaOk.Controls.Add(this.LblFileName);
            this.ItarKedaOk.Controls.Add(this.LblOr);
            this.ItarKedaOk.Controls.Add(this.label1);
            this.ItarKedaOk.Controls.Add(this.CmdClose);
            this.ItarKedaOk.Controls.Add(this.CmdOpen);
            this.ItarKedaOk.Controls.Add(this.CmdEdit);
            this.ItarKedaOk.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ItarKedaOk.Location = new System.Drawing.Point(0, 262);
            this.ItarKedaOk.Name = "ItarKedaOk";
            this.ItarKedaOk.Size = new System.Drawing.Size(672, 88);
            this.ItarKedaOk.TabIndex = 5;
            // 
            // LblFileName
            // 
            this.LblFileName.Location = new System.Drawing.Point(256, 48);
            this.LblFileName.Name = "LblFileName";
            this.LblFileName.Size = new System.Drawing.Size(232, 23);
            this.LblFileName.TabIndex = 4;
            this.LblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblOr
            // 
            this.LblOr.AutoSize = true;
            this.LblOr.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LblOr.Location = new System.Drawing.Point(360, 16);
            this.LblOr.Name = "LblOr";
            this.LblOr.Size = new System.Drawing.Size(20, 14);
            this.LblOr.TabIndex = 3;
            this.LblOr.Text = "Or";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 72);
            this.label1.TabIndex = 2;
            this.label1.Text = "Plesae Note this is not a file editor it is just a viewer if you want to edit any" +
                " of those click \"Edit\"  otherwise click \"Open\" to run it";
            // 
            // CmdOpen
            // 
            this.CmdOpen.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmdOpen.Location = new System.Drawing.Point(256, 8);
            this.CmdOpen.Name = "CmdOpen";
            this.CmdOpen.Size = new System.Drawing.Size(88, 32);
            this.CmdOpen.TabIndex = 1;
            this.CmdOpen.Text = "&Open";
            this.CmdOpen.Click += new System.EventHandler(this.CmdOpen_Click);
            // 
            // CmdEdit
            // 
            this.CmdEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmdEdit.Location = new System.Drawing.Point(392, 8);
            this.CmdEdit.Name = "CmdEdit";
            this.CmdEdit.Size = new System.Drawing.Size(88, 32);
            this.CmdEdit.TabIndex = 1;
            this.CmdEdit.Text = "&Edit";
            this.CmdEdit.Click += new System.EventHandler(this.CmdEdit_Click);
            // 
            // FrmTextEditor
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(672, 350);
            this.Controls.Add(this.TabTxtEdit);
            this.Controls.Add(this.ItarKedaOk);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "FrmTextEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmTextEditor";
            this.Load += new System.EventHandler(this.FrmTextEditor_Load);
            this.TabTxtEdit.ResumeLayout(false);
            this.PageSystemINI.ResumeLayout(false);
            this.PageSystemINI.PerformLayout();
            this.PageWinINI.ResumeLayout(false);
            this.PageWinINI.PerformLayout();
            this.PageBootINI.ResumeLayout(false);
            this.PageBootINI.PerformLayout();
            this.PageAutoEXEC.ResumeLayout(false);
            this.PageAutoEXEC.PerformLayout();
            this.PageConfigSys.ResumeLayout(false);
            this.PageConfigSys.PerformLayout();
            this.PageConfigNT.ResumeLayout(false);
            this.PageConfigNT.PerformLayout();
            this.ItarKedaOk.ResumeLayout(false);
            this.ItarKedaOk.PerformLayout();
            this.ResumeLayout(false);

		}


		#endregion

		#endregion

		#region Form Code

		private void FrmTextEditor_Load(object sender, System.EventArgs e)
		{
			string Windows = Environment.SystemDirectory.Substring ( 0,11);

			switch ( MyAct.ReportOS() )
			{
				case "WinXp With Admin":

					ReadTxt ( TxtBootINI, @"c:\boot.ini" );
					ReadTxt ( TxtConfigNT, Environment.SystemDirectory + @"\CONFIG.NT" );

					PageBootINI.Tag = @"c:\boot.ini";
					PageConfigNT.Tag = Environment.SystemDirectory + @"\CONFIG.NT";

					PageAutoEXEC.Dispose(); // Dispose we do no need it
					PageConfigSys.Dispose(); // Dispose we do no need it

					break;

				case "A None WinXp System": // NOt Xp
		
					ReadTxt ( TxtAutoExec, @"c:\autoexec.bat" );
					ReadTxt ( TxtConfigSYS, @"c:\config.sys" );

					PageAutoEXEC.Tag = @"c:\autoexec.bat";
					PageConfigSys.Tag = "c:\\config.sys";

					PageBootINI.Dispose(); // Dispose we do no need it
					PageConfigNT.Dispose() ; // Dispose we do no need it
					break;
			}

			
			ReadTxt ( TxtWinINI, Windows + "WIN.INI" );  // Common
			ReadTxt ( TxtSystemINI, Windows + "SYSTEM.INI" );		//Common
			
			PageSystemINI.Tag = Windows + "SYSTEM.INI";
			PageWinINI.Tag = Windows + "WIN.INI";

			TabTxtEdit.SelectedTab = PageSystemINI;


			
		}


		#endregion

		#region Buttons Code

		private void CmdOpen_Click(object sender, System.EventArgs e)
		{
			try
			{
				ProcessStartInfo Exec = new ProcessStartInfo();

				Exec.FileName = TabTxtEdit.SelectedTab.Tag.ToString();
				Exec.UseShellExecute = true;
				Process.Start ( Exec );
			}
			catch ( Win32Exception MyExp )
			{
				string Msg = "An error occured while trying to execute" + "\n \"" +
					TabTxtEdit.SelectedTab.Tag.ToString() + "\" \n" + "Windows Said : \"" + MyExp.Message + "\" \n" +
					"But !!!?\n" + "We can try to open it with notepad!! \n" +
					"Press \"Retry\" to Retry!!! or press \"Cancel\" to Cancel!!??";

				DialogResult MyRes = 
					MessageBox.Show ( Msg, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information );

				if ( MyRes == DialogResult.Retry )
				{
					CmdEdit_Click( sender, e );
				}
				return;
			}
		}


		private void CmdEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				ProcessStartInfo Edt = new ProcessStartInfo();

				Edt.FileName = "notepad.exe";
				Edt.Arguments = TabTxtEdit.SelectedTab.Tag.ToString();
				Edt.UseShellExecute = true;
			
				Process.Start ( Edt );
			}
			catch ( Win32Exception MyExp )
			{
				string Msg = "An error occured while trying to execute" + "\n \"" +
					TabTxtEdit.SelectedTab.Tag.ToString() + "\" \n" + "Windows Said : \"" + MyExp.Message + "\"" ;
			}
		}


		private void CmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		#endregion

		#region Addtional Methods

		void ReadTxt( TextBox TheBox, string ThePath )
		{
			try
			{
				using ( StreamReader TheReader = new StreamReader ( ThePath ) )
				{
					TheBox.Text = TheReader.ReadToEnd();
				}
			}
			catch ( IOException Exp )
			{
				Exp.Message.Trim();
				return;
			}

		}

		
		#endregion

		private void TabTxtEdit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Text = TabTxtEdit.SelectedTab.Text;

			LblFileName.Text = TabTxtEdit.SelectedTab.Text;

		}

	}
}
