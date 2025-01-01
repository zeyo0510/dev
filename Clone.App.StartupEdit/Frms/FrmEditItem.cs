using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;


namespace StartupEdit
{

	public class FrmEditAddEntries : System.Windows.Forms.Form
	{
		/// <summary>
		///  = {"Run","RunOnce" };
		/// </summary>
		string [] SuffixXp = {"Run","RunOnce" };

		/// <summary>
		///  = {"RunServices","RunServicesOnce"};
		/// </summary>
		string [] Suffix9X = {"RunServices","RunServicesOnce"};

		ClsAddOns MyAction = new ClsAddOns();


		#region Windows SpecifiC Declartion

		internal System.Windows.Forms.Button CmdOK;
		internal System.Windows.Forms.Button CmdCancel;
		internal System.Windows.Forms.Button CmdBrowse;
		internal System.Windows.Forms.TextBox TxtNameAddEdit;
		/// <summary>
		///  Where
		///  0 = Local Machine
		///  1 = Current User
		/// </summary>
		internal System.Windows.Forms.ComboBox CombHkey;
		/// <summary>
		/// Where
		/// 0 = Run
		/// 1 = RunOnce
		/// </summary>
		internal System.Windows.Forms.ComboBox CombSuffix;
		internal System.Windows.Forms.PictureBox pictureBox1;
		/// <summary>
		/// this is the one that everything goes underit
		/// </summary>
		internal System.Windows.Forms.Button Cmd_Test;
		internal System.Windows.Forms.TextBox TxtCommandAddEdit;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;

		private System.ComponentModel.Container components = null;

		public FrmEditAddEntries()
		{
			InitializeComponent();
		}


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

		private void InitializeComponent()
		{
            this.CmdOK = new System.Windows.Forms.Button();
            this.CmdCancel = new System.Windows.Forms.Button();
            this.CmdBrowse = new System.Windows.Forms.Button();
            this.TxtNameAddEdit = new System.Windows.Forms.TextBox();
            this.CombHkey = new System.Windows.Forms.ComboBox();
            this.CombSuffix = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Cmd_Test = new System.Windows.Forms.Button();
            this.TxtCommandAddEdit = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmdOK
            // 
            this.CmdOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmdOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CmdOK.Location = new System.Drawing.Point(264, 232);
            this.CmdOK.Name = "CmdOK";
            this.CmdOK.Size = new System.Drawing.Size(88, 24);
            this.CmdOK.TabIndex = 6;
            this.CmdOK.Text = "&O.K";
            this.CmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // CmdCancel
            // 
            this.CmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmdCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CmdCancel.Location = new System.Drawing.Point(64, 232);
            this.CmdCancel.Name = "CmdCancel";
            this.CmdCancel.Size = new System.Drawing.Size(88, 24);
            this.CmdCancel.TabIndex = 5;
            this.CmdCancel.Text = "&Cancel";
            // 
            // CmdBrowse
            // 
            this.CmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmdBrowse.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.CmdBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CmdBrowse.Location = new System.Drawing.Point(344, 24);
            this.CmdBrowse.Name = "CmdBrowse";
            this.CmdBrowse.Size = new System.Drawing.Size(32, 21);
            this.CmdBrowse.TabIndex = 2;
            this.CmdBrowse.Text = "<<";
            this.CmdBrowse.Click += new System.EventHandler(this.CmdBrowse_Click);
            // 
            // TxtNameAddEdit
            // 
            this.TxtNameAddEdit.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.TxtNameAddEdit.ForeColor = System.Drawing.Color.Maroon;
            this.TxtNameAddEdit.Location = new System.Drawing.Point(8, 24);
            this.TxtNameAddEdit.Name = "TxtNameAddEdit";
            this.TxtNameAddEdit.Size = new System.Drawing.Size(320, 21);
            this.TxtNameAddEdit.TabIndex = 0;
            this.TxtNameAddEdit.Tag = "0";
            this.TxtNameAddEdit.Enter += new System.EventHandler(this.TxtNameAddEdit_Enter);
            // 
            // CombHkey
            // 
            this.CombHkey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CombHkey.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.CombHkey.ForeColor = System.Drawing.Color.Maroon;
            this.CombHkey.ItemHeight = 13;
            this.CombHkey.Items.AddRange(new object[] {
            "Run This Entry With All Users Account (HKEY_LOCAL_MACHINE)",
            "Run This Entry With The Current User Account (HKEY_CURRENT_USER)"});
            this.CombHkey.Location = new System.Drawing.Point(8, 24);
            this.CombHkey.Name = "CombHkey";
            this.CombHkey.Size = new System.Drawing.Size(376, 21);
            this.CombHkey.TabIndex = 3;
            this.CombHkey.SelectedIndexChanged += new System.EventHandler(this.CombHkey_SelectedIndexChanged);
            // 
            // CombSuffix
            // 
            this.CombSuffix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CombSuffix.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.CombSuffix.ForeColor = System.Drawing.Color.Maroon;
            this.CombSuffix.ItemHeight = 13;
            this.CombSuffix.Location = new System.Drawing.Point(8, 56);
            this.CombSuffix.Name = "CombSuffix";
            this.CombSuffix.Size = new System.Drawing.Size(376, 21);
            this.CombSuffix.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(344, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // Cmd_Test
            // 
            this.Cmd_Test.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Cmd_Test.Location = new System.Drawing.Point(168, 272);
            this.Cmd_Test.Name = "Cmd_Test";
            this.Cmd_Test.Size = new System.Drawing.Size(88, 23);
            this.Cmd_Test.TabIndex = 11;
            this.Cmd_Test.Text = "Test";
            // 
            // TxtCommandAddEdit
            // 
            this.TxtCommandAddEdit.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.TxtCommandAddEdit.ForeColor = System.Drawing.Color.Maroon;
            this.TxtCommandAddEdit.Location = new System.Drawing.Point(8, 24);
            this.TxtCommandAddEdit.Name = "TxtCommandAddEdit";
            this.TxtCommandAddEdit.Size = new System.Drawing.Size(328, 21);
            this.TxtCommandAddEdit.TabIndex = 1;
            this.TxtCommandAddEdit.Tag = "1";
            this.TxtCommandAddEdit.Enter += new System.EventHandler(this.TxtCommandAddEdit_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtNameAddEdit);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 56);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Name ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TxtCommandAddEdit);
            this.groupBox2.Controls.Add(this.CmdBrowse);
            this.groupBox2.Location = new System.Drawing.Point(8, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(392, 56);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Command";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CombHkey);
            this.groupBox3.Controls.Add(this.CombSuffix);
            this.groupBox3.Location = new System.Drawing.Point(8, 136);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(392, 88);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Location";
            // 
            // FrmEditAddEntries
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(402, 264);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Cmd_Test);
            this.Controls.Add(this.CmdOK);
            this.Controls.Add(this.CmdCancel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditAddEntries";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Entry";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmEditAddEntries_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmEditAddEntries_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		
		#endregion


		#endregion

		#region Form Code Load And Closing

		private void FrmEditAddEntries_Load(object sender, System.EventArgs e)
		{
			ClsEntriesManipulation.OldEntryData = TxtCommandAddEdit.Text;
			ClsEntriesManipulation.OldEntryName = TxtNameAddEdit.Text;
			ClsEntriesManipulation.OldEntryHkey = CombHkey.SelectedIndex;
			ClsEntriesManipulation.OldWhichRun = CombSuffix.SelectedIndex;

			//FIll();

		}


		private void FrmEditAddEntries_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			/*								Welcome to the "Operation Room"
			 * when we load this dialog ( two methods initialize this form (Edit, add)
			 * in edit we give the CmdTest a Dialog result
			 * in Edit  MyEd.Cmd_Test.DialogResult = DialogResult.Yes;
			 * in Add   MyEd.Cmd_Test.DialogResult = DialogResult.OK;
			 *	 */
			ClsEntriesManipulation MyImp = new ClsEntriesManipulation();

			string Suffix = MyImp.TellMeTheSuffix ( CombSuffix.SelectedIndex );

			switch ( this.DialogResult )
			{
				case DialogResult.OK : // Ok Means Add Entry
					MyImp.AddEntry
						( Suffix, CombHkey.SelectedIndex, TxtNameAddEdit.Text, TxtCommandAddEdit.Text );
					break;

				case DialogResult.Yes : // Yes Means Edit Entry
					MyImp.CheckIfEdit 
						(	TxtNameAddEdit.Text,		TxtCommandAddEdit.Text, 
						CombHkey.SelectedIndex,	CombSuffix.SelectedIndex);
					break;
			}
		}


		#endregion

		#region Buttons Code

		private void CmdBrowse_Click(object sender, System.EventArgs e)
		{
			uint TheIndex = 0; // For The Icon

			OpenFileDialog MyDia = new OpenFileDialog();

			MyDia.Title = "Choose File To Execute";
			MyDia.Filter = 
				"All Supported Files|*.exe;*.com;*.bat;*.vbs;*.js;*.wsf|Executable Files(*.exe)|*.exe|Batch & Com Files(*.bat)(*.com)|*.bat;*.com|Scribt Files (*.vbs)(*.js)(*.wsf)|*.vbs;*.js;*.wsf|All Files (*.*)|*.*";
			
			if ( MyDia.ShowDialog ( this ) == DialogResult.OK )
			{
				FileInfo MyFile = new FileInfo ( MyDia.FileName );

				TxtCommandAddEdit.Text = MyDia.FileName;

				pictureBox1.Image = null; //Empty It First
				pictureBox1.Image = Bitmap.FromHicon //Get It From handle
					(
					ClsAPICalls.GetExtensionIcon ( this.Handle, TxtCommandAddEdit.Text, ref TheIndex )
					);

				if( TxtNameAddEdit.Text == ""  ) //Try To Insert name if the user didn't yet
				{
					// PUT THE FILE NAME IN TXTNAME
					TxtNameAddEdit.Text = MyFile.Name.Remove ( MyFile.Name.Length - 4, 4 ); 
				}
			}
		}


		private void CmdOK_Click(object sender, System.EventArgs e)
		{
			ClsAddOns MyAna = new ClsAddOns();

			DialogResult TheResult = DialogResult.None;

			if ( TxtCommandAddEdit.Text == "" || TxtNameAddEdit.Text == "" )
			{
				MessageBox.Show ( "No Changes Made " + "\n" + "\n" + "Cause: " + "Empty Field(s) Found"
					+ "\n" + "Solution: " + "Fill Both Field(s)" + "\n", "Error !", 
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				return;
			}

			FileInfo CheckIfExists = new FileInfo ( MyAna.AnalyzeIt ( TxtCommandAddEdit.Text ) );

			if ( !CheckIfExists.Exists )
			{
				string TheMessage = 
					"Sorry.. But We Found That The Command You Just Entered Is Not Valid" + "\n" +
					"\"The File Does Not Exist\"" + "\n" + "\n" +
					"Please Use The Following" + "\n" + "\n" +
					"Press \"Abort\" if You Want To Cancel The Whole Operation And Return To The Main Window" + "\n" +
					"Press \"Retry\" To Correct This Error And Enter A Valid Path" + "\n" +
					"Press \"Ignore\" If You Are An Advanced User Who Knows What He Is Doing !" + "\n" + "\n" +
					"Thank You";
			
				TheResult = MessageBox.Show 
					(	TheMessage,"Action Needed Here", MessageBoxButtons.AbortRetryIgnore, 
					MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1 );
			}
				switch ( TheResult )
				{
					case	DialogResult.Ignore :
						this.Cmd_Test.PerformClick();
						break;

					case DialogResult.Abort :
						this.CmdCancel.PerformClick();
						break;
					default:
						this.Cmd_Test.PerformClick();
						break;
			
				}
		}


		#endregion

		#region TextBoxes Code "Enter"

		private void TxtCommandAddEdit_Enter(object sender, System.EventArgs e)
		{
			TxtCommandAddEdit.SelectAll();
		}


		private void TxtNameAddEdit_Enter(object sender, System.EventArgs e)
		{
			TxtNameAddEdit.SelectAll();
		}


		#endregion

		private void CombHkey_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			CombSuffix.Items.Clear();
			CombSuffix.Items.AddRange ( SuffixXp );

			if ( CombHkey.SelectedIndex == 0 ) 
			{ 
				if ( MyAction.ReportOS() == "A None WinXp System" ) 
				{
					CombSuffix.Items.AddRange ( Suffix9X ); 
				}
			}			

			CombSuffix.SelectedIndex = 0;
		}

		void FIll()
		{
			string [,] ArrHkey = {
									{"HKEY_LOCAL_MACHINE","HKEY_CURRENT_USER","HKEY_CLASSES_ROOT"},
									{"All Users Accounts","This User Account","Not Used YET"} };

			string [,] ArrSuffix = {
									{"Run","RunOnce","RunOnceEx","RunServices","RunServicesOnce"},
									{"Run","RunOnce","RunOnceEx","RunServices","RunServicesOnce"} };


			CombHkey.DataSource = ArrHkey;
			CombHkey.DisplayMember = "HKEY_LOCAL_MACHINE";
			//CombSuffix.Items.AddRange ( ArrSuffix );
		}
	}
}
