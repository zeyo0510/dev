using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;


namespace StartupEdit
{
	public class FrmOptions : System.Windows.Forms.Form
	{

		#region Variables

		/// <summary>
		/// string holding the registrykey will be initialized in the constructor with the value
		/// "SOFTWARE\AlQademoUn\StartEdit\" + Environment.UserName
		/// </summary>
		string TheKey;

		/// <summary>
		/// one global registrykey instance
		/// </summary>
		RegistryKey GlobalReg;

		#endregion

		#region Own Windows Specific

		private System.Windows.Forms.CheckBox ChkSaveWinSize;
		private System.Windows.Forms.CheckBox ChkSaveWinPos;
		private System.Windows.Forms.CheckBox ChkSaveColWidth;
		private System.Windows.Forms.CheckBox ChkOnTop;
		internal System.Windows.Forms.TabPage PageGeneral;
		private System.Windows.Forms.CheckBox ChkBottom;
		private System.Windows.Forms.CheckBox ChkRememberView;
        internal System.Windows.Forms.TabPage PageAbout;
        internal System.Windows.Forms.TabControl TabOptions;
		private System.Windows.Forms.CheckBox ChkEditDefault;
        private System.Windows.Forms.CheckBox ChkExpandTree;
        private PictureBox IconBox;
        private Button CmdBrowse;
        private Button CmdSave;
        private TextBox TxtTaskManager;
        private Label LblTaskManager;
        private TextBox textBox1;

		private System.ComponentModel.Container components = null;

		#endregion

		#region Constructor

		public FrmOptions()
		{
			InitializeComponent();

			TheKey = @"SOFTWARE\AlQademoUn\StartEdit\" + Environment.UserName ;

			ChkOnTop.Checked = CheckValue ( "OnTop" );
			ChkSaveColWidth.Checked = CheckValue ( "SaveColWidth" );
			ChkSaveWinSize.Checked = CheckValue ( "SaveFrmSize" );
			ChkSaveWinPos.Checked = CheckValue ( "SaveFrmLocation" );
			ChkBottom.Checked = CheckValue ( "BarBottom" );
			ChkRememberView.Checked = CheckValue ( "RememberView" );
			ChkEditDefault.Checked = CheckValue ( "EditDefault" );
			ChkExpandTree.Checked = CheckValue ( "ExpandOnStart" );

			GetTaskManagerState ( false , 2 );
		}


		#endregion

		#region Windows Form Designer generated code

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


		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOptions));
            this.TabOptions = new System.Windows.Forms.TabControl();
            this.PageGeneral = new System.Windows.Forms.TabPage();
            this.IconBox = new System.Windows.Forms.PictureBox();
            this.CmdBrowse = new System.Windows.Forms.Button();
            this.CmdSave = new System.Windows.Forms.Button();
            this.TxtTaskManager = new System.Windows.Forms.TextBox();
            this.LblTaskManager = new System.Windows.Forms.Label();
            this.ChkEditDefault = new System.Windows.Forms.CheckBox();
            this.ChkRememberView = new System.Windows.Forms.CheckBox();
            this.ChkBottom = new System.Windows.Forms.CheckBox();
            this.ChkSaveWinSize = new System.Windows.Forms.CheckBox();
            this.ChkSaveWinPos = new System.Windows.Forms.CheckBox();
            this.ChkSaveColWidth = new System.Windows.Forms.CheckBox();
            this.ChkOnTop = new System.Windows.Forms.CheckBox();
            this.ChkExpandTree = new System.Windows.Forms.CheckBox();
            this.PageAbout = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TabOptions.SuspendLayout();
            this.PageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
            this.PageAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabOptions
            // 
            this.TabOptions.Controls.Add(this.PageGeneral);
            this.TabOptions.Controls.Add(this.PageAbout);
            this.TabOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabOptions.Location = new System.Drawing.Point(0, 0);
            this.TabOptions.Name = "TabOptions";
            this.TabOptions.SelectedIndex = 0;
            this.TabOptions.Size = new System.Drawing.Size(572, 353);
            this.TabOptions.TabIndex = 0;
            // 
            // PageGeneral
            // 
            this.PageGeneral.AutoScroll = true;
            this.PageGeneral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PageGeneral.Controls.Add(this.IconBox);
            this.PageGeneral.Controls.Add(this.CmdBrowse);
            this.PageGeneral.Controls.Add(this.CmdSave);
            this.PageGeneral.Controls.Add(this.TxtTaskManager);
            this.PageGeneral.Controls.Add(this.LblTaskManager);
            this.PageGeneral.Controls.Add(this.ChkEditDefault);
            this.PageGeneral.Controls.Add(this.ChkRememberView);
            this.PageGeneral.Controls.Add(this.ChkBottom);
            this.PageGeneral.Controls.Add(this.ChkSaveWinSize);
            this.PageGeneral.Controls.Add(this.ChkSaveWinPos);
            this.PageGeneral.Controls.Add(this.ChkSaveColWidth);
            this.PageGeneral.Controls.Add(this.ChkOnTop);
            this.PageGeneral.Controls.Add(this.ChkExpandTree);
            this.PageGeneral.Location = new System.Drawing.Point(4, 22);
            this.PageGeneral.Name = "PageGeneral";
            this.PageGeneral.Size = new System.Drawing.Size(564, 327);
            this.PageGeneral.TabIndex = 0;
            this.PageGeneral.Text = "Genaeral Options";
            this.PageGeneral.UseVisualStyleBackColor = true;
            // 
            // IconBox
            // 
            this.IconBox.Location = new System.Drawing.Point(9, 254);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(32, 32);
            this.IconBox.TabIndex = 26;
            this.IconBox.TabStop = false;
            // 
            // CmdBrowse
            // 
            this.CmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmdBrowse.Location = new System.Drawing.Point(522, 254);
            this.CmdBrowse.Name = "CmdBrowse";
            this.CmdBrowse.Size = new System.Drawing.Size(32, 23);
            this.CmdBrowse.TabIndex = 25;
            this.CmdBrowse.Text = "<<";
            // 
            // CmdSave
            // 
            this.CmdSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmdSave.Location = new System.Drawing.Point(178, 283);
            this.CmdSave.Name = "CmdSave";
            this.CmdSave.Size = new System.Drawing.Size(143, 34);
            this.CmdSave.TabIndex = 24;
            this.CmdSave.Text = "Save Changes";
            // 
            // TxtTaskManager
            // 
            this.TxtTaskManager.Location = new System.Drawing.Point(54, 254);
            this.TxtTaskManager.Name = "TxtTaskManager";
            this.TxtTaskManager.Size = new System.Drawing.Size(462, 21);
            this.TxtTaskManager.TabIndex = 23;
            // 
            // LblTaskManager
            // 
            this.LblTaskManager.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LblTaskManager.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.LblTaskManager.Location = new System.Drawing.Point(6, 189);
            this.LblTaskManager.Name = "LblTaskManager";
            this.LblTaskManager.Size = new System.Drawing.Size(548, 64);
            this.LblTaskManager.TabIndex = 22;
            this.LblTaskManager.Text = resources.GetString("LblTaskManager.Text");
            // 
            // ChkEditDefault
            // 
            this.ChkEditDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkEditDefault.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ChkEditDefault.Location = new System.Drawing.Point(6, 144);
            this.ChkEditDefault.Name = "ChkEditDefault";
            this.ChkEditDefault.Size = new System.Drawing.Size(488, 32);
            this.ChkEditDefault.TabIndex = 21;
            this.ChkEditDefault.Text = "Double clicking shows \"Edit Entry\" dialog insted of  the \"Entry\'s Properties\"";
            this.ChkEditDefault.CheckedChanged += new System.EventHandler(this.ChkEditDefault_CheckedChanged);
            // 
            // ChkRememberView
            // 
            this.ChkRememberView.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkRememberView.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ChkRememberView.Location = new System.Drawing.Point(6, 105);
            this.ChkRememberView.Name = "ChkRememberView";
            this.ChkRememberView.Size = new System.Drawing.Size(195, 24);
            this.ChkRememberView.TabIndex = 19;
            this.ChkRememberView.Text = "Remember Last View";
            this.ChkRememberView.CheckedChanged += new System.EventHandler(this.ChkRememberView_CheckedChanged);
            // 
            // ChkBottom
            // 
            this.ChkBottom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkBottom.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ChkBottom.Location = new System.Drawing.Point(240, 75);
            this.ChkBottom.Name = "ChkBottom";
            this.ChkBottom.Size = new System.Drawing.Size(188, 24);
            this.ChkBottom.TabIndex = 18;
            this.ChkBottom.Text = "Toolbar at bottom";
            this.ChkBottom.CheckedChanged += new System.EventHandler(this.ChkBottom_CheckedChanged);
            // 
            // ChkSaveWinSize
            // 
            this.ChkSaveWinSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkSaveWinSize.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ChkSaveWinSize.Location = new System.Drawing.Point(6, 45);
            this.ChkSaveWinSize.Name = "ChkSaveWinSize";
            this.ChkSaveWinSize.Size = new System.Drawing.Size(177, 24);
            this.ChkSaveWinSize.TabIndex = 14;
            this.ChkSaveWinSize.Text = "Save Windows Size";
            this.ChkSaveWinSize.CheckedChanged += new System.EventHandler(this.ChkSaveWinSize_CheckedChanged);
            // 
            // ChkSaveWinPos
            // 
            this.ChkSaveWinPos.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkSaveWinPos.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ChkSaveWinPos.Location = new System.Drawing.Point(6, 75);
            this.ChkSaveWinPos.Name = "ChkSaveWinPos";
            this.ChkSaveWinPos.Size = new System.Drawing.Size(195, 24);
            this.ChkSaveWinPos.TabIndex = 13;
            this.ChkSaveWinPos.Text = "Save Windows Postion";
            this.ChkSaveWinPos.CheckedChanged += new System.EventHandler(this.ChkSaveWinPos_CheckedChanged);
            // 
            // ChkSaveColWidth
            // 
            this.ChkSaveColWidth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkSaveColWidth.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ChkSaveColWidth.Location = new System.Drawing.Point(6, 15);
            this.ChkSaveColWidth.Name = "ChkSaveColWidth";
            this.ChkSaveColWidth.Size = new System.Drawing.Size(185, 24);
            this.ChkSaveColWidth.TabIndex = 12;
            this.ChkSaveColWidth.Text = "Save Columns Width";
            this.ChkSaveColWidth.CheckedChanged += new System.EventHandler(this.ChkSaveColWidth_CheckedChanged);
            // 
            // ChkOnTop
            // 
            this.ChkOnTop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkOnTop.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ChkOnTop.Location = new System.Drawing.Point(240, 15);
            this.ChkOnTop.Name = "ChkOnTop";
            this.ChkOnTop.Size = new System.Drawing.Size(166, 24);
            this.ChkOnTop.TabIndex = 11;
            this.ChkOnTop.Text = "Always OnTop";
            this.ChkOnTop.CheckedChanged += new System.EventHandler(this.ChkOnTop_CheckedChanged);
            // 
            // ChkExpandTree
            // 
            this.ChkExpandTree.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChkExpandTree.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ChkExpandTree.Location = new System.Drawing.Point(240, 45);
            this.ChkExpandTree.Name = "ChkExpandTree";
            this.ChkExpandTree.Size = new System.Drawing.Size(193, 24);
            this.ChkExpandTree.TabIndex = 11;
            this.ChkExpandTree.Text = "Expand Tree On Start";
            this.ChkExpandTree.CheckedChanged += new System.EventHandler(this.ChkExpandTree_CheckedChanged);
            // 
            // PageAbout
            // 
            this.PageAbout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PageAbout.Controls.Add(this.textBox1);
            this.PageAbout.Location = new System.Drawing.Point(4, 22);
            this.PageAbout.Name = "PageAbout";
            this.PageAbout.Size = new System.Drawing.Size(564, 327);
            this.PageAbout.TabIndex = 1;
            this.PageAbout.Text = "About";
            this.PageAbout.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(560, 323);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // FrmOptions
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(572, 353);
            this.Controls.Add(this.TabOptions);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmOptions";
            this.TopMost = true;
            this.TabOptions.ResumeLayout(false);
            this.PageGeneral.ResumeLayout(false);
            this.PageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).EndInit();
            this.PageAbout.ResumeLayout(false);
            this.PageAbout.PerformLayout();
            this.ResumeLayout(false);

		}


		#endregion

		#region Buttons Code Goes Here

		private void CmdBrowse_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog MyDia = new OpenFileDialog();

			MyDia.Title = "Choose File To Execute";
			MyDia.Filter = 
				"All Supported Files|*.exe;*.com;*.bat;*.vbs;*.js;*.wsf|Executable Files(*.exe)|*.exe|Batch & Com Files(*.bat)(*.com)|*.bat;*.com|Scribt Files (*.vbs)(*.js)(*.wsf)|*.vbs;*.js;*.wsf|All Files (*.*)|*.*";
			
			if ( MyDia.ShowDialog ( this ) == DialogResult.OK )
			{
				FileInfo MyFile = new FileInfo ( MyDia.FileName );

				TxtTaskManager.Text = MyDia.FileName;
				
				ReadImage();
			}
			
		}


		private void CmdExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		private void CmdSave_Click(object sender, System.EventArgs e)
		{
			GetTaskManagerState ( true , TxtTaskManager.Text.Trim().Length );
			
			this.CmdSave.Enabled = false;
		}

		
		#endregion

		#region CheckBoxes Code Goes Here

		private void ChkSaveWinPos_CheckedChanged(object sender, System.EventArgs e)
		{
			SetValue ( this.ChkSaveWinPos, "SaveFrmLocation" );
		}


		private void ChkSaveColWidth_CheckedChanged(object sender, System.EventArgs e)
		{
			SetValue ( this.ChkSaveColWidth, "SaveColWidth" );
		}


		private void ChkSaveWinSize_CheckedChanged(object sender, System.EventArgs e)
		{
			SetValue ( this.ChkSaveWinSize, "SaveFrmSize" );
		}


		private void ChkOnTop_CheckedChanged(object sender, System.EventArgs e)
		{
			SetValue ( this.ChkOnTop, "OnTop" );
		}


		private void ChkBottom_CheckedChanged(object sender, System.EventArgs e)
		{
			SetValue ( ChkBottom, "BarBottom" );
		}


		private void ChkRememberView_CheckedChanged(object sender, System.EventArgs e)
		{
			SetValue ( ChkRememberView, "RememberView" );
		}


		private void ChkEditDefault_CheckedChanged(object sender, System.EventArgs e)
		{
			SetValue ( ChkEditDefault , "EditDefault" );
		}


		private void ChkExpandTree_CheckedChanged(object sender, System.EventArgs e)
		{
			SetValue ( ChkExpandTree , "ExpandOnStart" );
		}


		#endregion

		#region Txt Code

		private void TxtTaskManager_TextChanged(object sender, System.EventArgs e)
		{
			this.CmdSave.Enabled = true;
		}


		private void TxtTaskManager_Enter(object sender, System.EventArgs e)
		{
			TxtTaskManager.SelectAll();
		}


		#endregion

		#region Methods To Faciliate Code And Shorten

		/// <summary>
		/// sets The State Of The CheckBoxe
		/// </summary>
		/// <param name="TheBox">CheckBox Name To Check Its State</param>
		/// <param name="WhatVal">The Value To Look Into The Registry</param>
		private void SetValue ( CheckBox TheBox, string WhatVal )
		{
			GlobalReg = Registry.LocalMachine.OpenSubKey ( TheKey , true );

			if ( TheBox.CheckState == CheckState.Checked )
			{
				GlobalReg.SetValue ( WhatVal, "true" ) ;
			}
			else
			{
				GlobalReg.SetValue ( WhatVal, "false" ) ;
			}
		}


		/// <summary>
		/// Returns true if the value is true
		/// </summary>
		/// <param name="CheckWhat">the val to check goes here</param>
		/// <returns>true or false</returns>
		private bool CheckValue ( string CheckWhat )
		{
			GlobalReg = Registry.LocalMachine.OpenSubKey ( TheKey , true );

			if ( GlobalReg.GetValue ( CheckWhat , "false" ).ToString() == "true" ) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// Check The Task Manager Entry
		/// </summary>
		/// <param name="TrueToSave">Set It To True To Set False To Get</param>
		/// <param name="Restore">Set It To 0 To Restore The Default</param>
		void GetTaskManagerState( bool TrueToSave, int Restore)
		{
			try
			{
				GlobalReg = Registry.LocalMachine.OpenSubKey 
					( @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\taskmgr.exe",  true );
			
				if ( TrueToSave )
				{
					if ( Restore == 0 )
					{
						GlobalReg.DeleteValue ( "Debugger", false );
					}
					else
					{
						GlobalReg.SetValue ( "Debugger", TxtTaskManager.Text );
					}
				}
				else
				{
					TxtTaskManager.Text = GlobalReg.GetValue ( "Debugger", "Windows Task Manager will Launch").ToString();
				}
				ReadImage();
			}
			catch
			{
				return;
			}

		}


		/// <summary>
		/// To Read The File Icon
		/// </summary>
		void ReadImage()
		{
			try
			{
				uint TheIndex = 0;

				IconBox.Image = null;
				IconBox.Image = Bitmap.FromHicon 
					(
					ClsAPICalls.GetExtensionIcon ( this.Handle, TxtTaskManager.Text, ref TheIndex )
					);
			}
			catch
			{
				TxtTaskManager.Text = "Unable To Read The File Icon";
			}
		}


		#endregion
		
	}
}
