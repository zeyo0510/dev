using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace StartupEdit
{
	public class FrmShutDown  : System.Windows.Forms.Form
	{
		#region Windows Declaration

		private System.Windows.Forms.ImageList IconsCont;
		private System.Windows.Forms.Button Cmd_Shutdown;
		private System.Windows.Forms.Button Cmd_LoggOff;
		private System.Windows.Forms.Button Cmd_Hibernate;
		private System.Windows.Forms.Button Cmd_Restart;
		private System.Windows.Forms.Button Cmd_Standby;
		private System.Windows.Forms.Button Cmd_Cancel;
		private System.Windows.Forms.ToolTip MyTip;

		private System.ComponentModel.IContainer components;

		public FrmShutDown()
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShutDown));
            this.Cmd_Shutdown = new System.Windows.Forms.Button();
            this.IconsCont = new System.Windows.Forms.ImageList(this.components);
            this.Cmd_LoggOff = new System.Windows.Forms.Button();
            this.Cmd_Hibernate = new System.Windows.Forms.Button();
            this.Cmd_Restart = new System.Windows.Forms.Button();
            this.Cmd_Standby = new System.Windows.Forms.Button();
            this.Cmd_Cancel = new System.Windows.Forms.Button();
            this.MyTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Cmd_Shutdown
            // 
            this.Cmd_Shutdown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cmd_Shutdown.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Cmd_Shutdown.ImageIndex = 0;
            this.Cmd_Shutdown.ImageList = this.IconsCont;
            this.Cmd_Shutdown.Location = new System.Drawing.Point(88, 8);
            this.Cmd_Shutdown.Name = "Cmd_Shutdown";
            this.Cmd_Shutdown.Size = new System.Drawing.Size(72, 56);
            this.Cmd_Shutdown.TabIndex = 1;
            this.Cmd_Shutdown.Text = "T&urn Off";
            this.Cmd_Shutdown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Cmd_Shutdown.Click += new System.EventHandler(this.Cmd_Shutdown_Click);
            // 
            // IconsCont
            // 
            this.IconsCont.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconsCont.ImageStream")));
            this.IconsCont.TransparentColor = System.Drawing.Color.Transparent;
            this.IconsCont.Images.SetKeyName(0, "");
            this.IconsCont.Images.SetKeyName(1, "");
            this.IconsCont.Images.SetKeyName(2, "");
            this.IconsCont.Images.SetKeyName(3, "");
            this.IconsCont.Images.SetKeyName(4, "");
            // 
            // Cmd_LoggOff
            // 
            this.Cmd_LoggOff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cmd_LoggOff.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Cmd_LoggOff.ImageIndex = 3;
            this.Cmd_LoggOff.ImageList = this.IconsCont;
            this.Cmd_LoggOff.Location = new System.Drawing.Point(328, 8);
            this.Cmd_LoggOff.Name = "Cmd_LoggOff";
            this.Cmd_LoggOff.Size = new System.Drawing.Size(72, 56);
            this.Cmd_LoggOff.TabIndex = 4;
            this.Cmd_LoggOff.Text = "&Log Off";
            this.Cmd_LoggOff.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Cmd_LoggOff.Click += new System.EventHandler(this.Cmd_LoggOff_Click);
            // 
            // Cmd_Hibernate
            // 
            this.Cmd_Hibernate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cmd_Hibernate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Cmd_Hibernate.ImageIndex = 2;
            this.Cmd_Hibernate.ImageList = this.IconsCont;
            this.Cmd_Hibernate.Location = new System.Drawing.Point(8, 8);
            this.Cmd_Hibernate.Name = "Cmd_Hibernate";
            this.Cmd_Hibernate.Size = new System.Drawing.Size(72, 56);
            this.Cmd_Hibernate.TabIndex = 0;
            this.Cmd_Hibernate.Text = "&Hibernate";
            this.Cmd_Hibernate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Cmd_Hibernate.Click += new System.EventHandler(this.Cmd_Hibernate_Click);
            // 
            // Cmd_Restart
            // 
            this.Cmd_Restart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cmd_Restart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Cmd_Restart.ImageIndex = 1;
            this.Cmd_Restart.ImageList = this.IconsCont;
            this.Cmd_Restart.Location = new System.Drawing.Point(168, 8);
            this.Cmd_Restart.Name = "Cmd_Restart";
            this.Cmd_Restart.Size = new System.Drawing.Size(72, 56);
            this.Cmd_Restart.TabIndex = 2;
            this.Cmd_Restart.Text = "&Restart";
            this.Cmd_Restart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Cmd_Restart.Click += new System.EventHandler(this.Cmd_Restart_Click);
            // 
            // Cmd_Standby
            // 
            this.Cmd_Standby.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cmd_Standby.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Cmd_Standby.ImageIndex = 4;
            this.Cmd_Standby.ImageList = this.IconsCont;
            this.Cmd_Standby.Location = new System.Drawing.Point(248, 8);
            this.Cmd_Standby.Name = "Cmd_Standby";
            this.Cmd_Standby.Size = new System.Drawing.Size(72, 56);
            this.Cmd_Standby.TabIndex = 3;
            this.Cmd_Standby.Text = "&Standby";
            this.Cmd_Standby.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Cmd_Standby.Click += new System.EventHandler(this.Cmd_Standby_Click);
            // 
            // Cmd_Cancel
            // 
            this.Cmd_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cmd_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cmd_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Cmd_Cancel.Location = new System.Drawing.Point(296, 80);
            this.Cmd_Cancel.Name = "Cmd_Cancel";
            this.Cmd_Cancel.Size = new System.Drawing.Size(96, 23);
            this.Cmd_Cancel.TabIndex = 5;
            this.Cmd_Cancel.Text = "&Cancel";
            this.Cmd_Cancel.Click += new System.EventHandler(this.Cmd_Cancel_Click);
            // 
            // MyTip
            // 
            this.MyTip.ShowAlways = true;
            // 
            // FrmShutDown
            // 
            this.AcceptButton = this.Cmd_Cancel;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.CancelButton = this.Cmd_Cancel;
            this.ClientSize = new System.Drawing.Size(408, 112);
            this.ControlBox = false;
            this.Controls.Add(this.Cmd_Cancel);
            this.Controls.Add(this.Cmd_Standby);
            this.Controls.Add(this.Cmd_Restart);
            this.Controls.Add(this.Cmd_Hibernate);
            this.Controls.Add(this.Cmd_LoggOff);
            this.Controls.Add(this.Cmd_Shutdown);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ForeColor = System.Drawing.Color.Yellow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmShutDown";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShutDownIt";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Frm_ShutDown_Load);
            this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region Form Code

		private void Frm_ShutDown_Load(object sender, System.EventArgs e)
		{
			MyTip.SetToolTip(Cmd_Cancel,"Dismiss This Dialog");
			MyTip.SetToolTip(Cmd_Shutdown,"Shuts down the system and turns off the power. \n The system must support the power-off feature.");
			MyTip.SetToolTip(Cmd_LoggOff, "Shuts down all processes running \n Then it logs the user off.");
			MyTip.SetToolTip(Cmd_Hibernate,"Hibernates the system.");
			MyTip.SetToolTip(Cmd_Standby,"Suspends the system.");
			MyTip.SetToolTip(Cmd_Restart,"Shuts down the system and then restarts the system.");
		}


		#endregion

		#region Button Code

		private void Cmd_Shutdown_Click(object sender, System.EventArgs e)
		{
			ExitTheWin.ExitWindows ( RestartOptions.ShutDown , true );
		}


		private void Cmd_Hibernate_Click(object sender, System.EventArgs e)
		{
			ExitTheWin.ExitWindows ( RestartOptions.Hibernate , true );
		}


		private void Cmd_Restart_Click(object sender, System.EventArgs e)
		{
			ExitTheWin.ExitWindows(RestartOptions.Reboot ,true);
		}


		private void Cmd_Standby_Click(object sender, System.EventArgs e)
		{
			ExitTheWin.ExitWindows(RestartOptions.Suspend ,true);
		}


		private void Cmd_LoggOff_Click(object sender, System.EventArgs e)
		{
			ExitTheWin.ExitWindows(RestartOptions.LogOff ,true);
		}


		private void Cmd_Cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		#endregion
	}
}
