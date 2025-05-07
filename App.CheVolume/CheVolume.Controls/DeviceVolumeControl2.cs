using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using a;
using AudioCore;
using CheVolume.Properties;
using EConTech.Windows.MACUI;

namespace CheVolume.Controls
{
	public class DeviceVolumeControl2 : UserControl
	{
		private delegate void SYS_STRING_INVOKE(string A_0);

		private delegate void SYS_BOOL_INVOKE(bool A_0);

		private delegate void SYS_INT_INVOKE(int A_0);

		private Timer timer1;

		public MMDevice mmDevice1;

		private int num1;

		private bool bool1;

		private IContainer components;

		public PictureBox IconBox;

		public Label lblProcessName;

		public CheCheckBox btnMute;

		public MACTrackBar macTrackBar1;

		private CheckBox chkSetDefault;

		private AudioEndpointVolume EndPointVolume
		{
			get
			{
				return mmDevice1.AudioEndpointVolume;
			}
		}

		private int VolumeValue
		{
			get
			{
				return macTrackBar1.Value;
			}
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
		private static extern bool _DestroyIcon(IntPtr P_0);

		public static Image ResizeImage(Image P_0, Size P_1)
		{
			return new Bitmap(P_0, P_1);
		}

		public DeviceVolumeControl2(MMDevice P_0)
		{
			InitializeComponent();
			mmDevice1 = P_0;
			bool1 = false;
			timer1 = new Timer();
			timer1.Tick += timer1_Tick;
			timer1.Interval = 10;
			timer1.Start();
			base.Tag = mmDevice1.ID;
			lblProcessName.Text = P_0.NameDesc;
			btnMute.Checked = EndPointVolume.Mute;
			macTrackBar1.Value = EndPointVolume.Volume;
			macTrackBar1.audioEndpointVolume1 = EndPointVolume;
			Icon icon = ImageHelper.Method1(mmDevice1.IconPath);
			if (icon.Height > 0)
			{
				IconBox.Image = ResizeImage(icon.ToBitmap(), new Size(32, 32));
			}
			_DestroyIcon(icon.Handle);
			EndPointVolume.OnVolumeNotification += SetData;
		}

		public void Method2(string P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_STRING_INVOKE(Method2), P_0);
			}
		}

		public void SetTrackBarV2(int P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_INT_INVOKE(SetTrackBarV2), P_0);
			}
			else if (!macTrackBar1.bool1)
			{
				macTrackBar1.Value = P_0;
			}
		}

		public void SetMuteV2(bool P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_BOOL_INVOKE(SetMuteV2), P_0);
			}
			else
			{
				btnMute.Checked = P_0;
			}
		}

		public void SetDefaultV2(bool P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_BOOL_INVOKE(SetDefaultV2), P_0);
			}
			else
			{
				chkSetDefault.Checked = P_0;
			}
		}

		public void SetDefaultV3(bool P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_BOOL_INVOKE(SetDefaultV3), P_0);
			}
			else
			{
				chkSetDefault.Checked = P_0;
			}
		}

		private void SetData(AudioVolumeNotificationData P_0)
		{
			SetTrackBarV2(int.Parse(Math.Ceiling(P_0.MasterVolume * 100f).ToString()));
			SetMuteV2(P_0.Muted);
			Method2(Math.Ceiling(P_0.MasterVolume * 100f).ToString());
		}

		private void timer1_Tick(object P_0, EventArgs P_1)
		{
			float[] array = new float[0];
			try
			{
				array = mmDevice1.AudioMeterInformation.PeakValues.ToFloatArray;
			}
			catch (Exception)
			{
				macTrackBar1.SetActualVolume(0f);
			}
			if (Enumerable.Count(array) > 0)
			{
				if (Enumerable.Count(array) == 1)
				{
					macTrackBar1.SetActualVolume(array[0]);
				}
				else
				{
					macTrackBar1.SetActualVolume((array[0] + array[1]) / 2f);
				}
			}
			macTrackBar1.Refresh();
		}

		private void lblProcessName_Click(object P_0, EventArgs P_1)
		{
		}

		private void btnMute_CheckedChanged(object P_0, EventArgs P_1)
		{
			if (btnMute.Checked)
			{
				macTrackBar1.TrackerColor = Color.Gray;
				btnMute.Image = Resources.muteon;
				btnMute.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
			}
			else
			{
				macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
				btnMute.Image = Resources.mute;
				btnMute.FlatAppearance.BorderColor = Color.DarkGray;
			}
			EndPointVolume.Mute = btnMute.Checked;
		}

		private void Obj_Method2(object P_0, EventArgs P_1)
		{
		}

		private void Obj_Method1(object P_0, ScrollEventArgs P_1)
		{
		}

		private void Obj_Method3(object P_0, EventArgs P_1)
		{
		}

		private void Method1(object P_0, decimal P_1)
		{
			macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
			if (macTrackBar1.bool1)
			{
				macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
				EndPointVolume.Mute = false;
			}
		}

		private void btnMute_MouseUp(object P_0, MouseEventArgs P_1)
		{
		}

		private void chkSetDefault_CheckedChanged(object P_0, EventArgs P_1)
		{
		}

		private void chkSetDefault_MouseClick(object P_0, MouseEventArgs P_1)
		{
			if (chkSetDefault.Checked)
			{
				Class4.Method3(mmDevice1.ID);
			}
			else
			{
				chkSetDefault.Checked = true;
			}
		}

		private void _OnPaint(object P_0, PaintEventArgs P_1)
		{
		}

		private void btnMute_MouseEnter(object P_0, EventArgs P_1)
		{
			btnMute.Image = Resources.muteon;
			btnMute.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
		}

		private void btnMute_MouseLeave(object P_0, EventArgs P_1)
		{
			if (!btnMute.Checked)
			{
				btnMute.FlatAppearance.BorderColor = Color.DarkGray;
				btnMute.Image = Resources.mute;
			}
		}

		protected override void Dispose(bool P_0)
		{
			if (P_0 && components != null)
			{
				components.Dispose();
			}
			base.Dispose(P_0);
		}

		private void InitializeComponent()
		{
			this.lblProcessName = new System.Windows.Forms.Label();
			this.IconBox = new System.Windows.Forms.PictureBox();
			this.chkSetDefault = new System.Windows.Forms.CheckBox();
			this.macTrackBar1 = new EConTech.Windows.MACUI.MACTrackBar();
			this.btnMute = new CheVolume.Controls.CheCheckBox();
			((System.ComponentModel.ISupportInitialize)this.IconBox).BeginInit();
			base.SuspendLayout();
			this.lblProcessName.Font = new System.Drawing.Font("Verdana", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblProcessName.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
			this.lblProcessName.Location = new System.Drawing.Point(119, 4);
			this.lblProcessName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblProcessName.Name = "lblProcessName";
			this.lblProcessName.Size = new System.Drawing.Size(157, 59);
			this.lblProcessName.TabIndex = 3;
			this.lblProcessName.Text = "Temp Text";
			this.lblProcessName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblProcessName.Click += new System.EventHandler(lblProcessName_Click);
			this.IconBox.BackColor = System.Drawing.Color.Transparent;
			this.IconBox.Location = new System.Drawing.Point(71, 12);
			this.IconBox.Margin = new System.Windows.Forms.Padding(0);
			this.IconBox.Name = "IconBox";
			this.IconBox.Size = new System.Drawing.Size(43, 39);
			this.IconBox.TabIndex = 0;
			this.IconBox.TabStop = false;
			this.chkSetDefault.AutoSize = true;
			this.chkSetDefault.Cursor = System.Windows.Forms.Cursors.Hand;
			this.chkSetDefault.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.chkSetDefault.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
			this.chkSetDefault.Location = new System.Drawing.Point(34, 23);
			this.chkSetDefault.Margin = new System.Windows.Forms.Padding(4);
			this.chkSetDefault.Name = "chkSetDefault";
			this.chkSetDefault.Size = new System.Drawing.Size(18, 17);
			this.chkSetDefault.TabIndex = 13;
			this.chkSetDefault.UseVisualStyleBackColor = true;
			this.chkSetDefault.CheckedChanged += new System.EventHandler(chkSetDefault_CheckedChanged);
			this.chkSetDefault.MouseClick += new System.Windows.Forms.MouseEventHandler(chkSetDefault_MouseClick);
			this.macTrackBar1.BackColor = System.Drawing.Color.Transparent;
			this.macTrackBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
			this.macTrackBar1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.macTrackBar1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
			this.macTrackBar1.ForeColor = System.Drawing.Color.FromArgb(123, 125, 123);
			this.macTrackBar1.IndentHeight = 6;
			this.macTrackBar1.LargeChange = 1;
			this.macTrackBar1.Location = new System.Drawing.Point(13, 54);
			this.macTrackBar1.Margin = new System.Windows.Forms.Padding(4);
			this.macTrackBar1.Maximum = 100;
			this.macTrackBar1.Minimum = 0;
			this.macTrackBar1.Name = "macTrackBar1";
			this.macTrackBar1.Size = new System.Drawing.Size(329, 28);
			this.macTrackBar1.TabIndex = 10;
			this.macTrackBar1.TextTickStyle = System.Windows.Forms.TickStyle.None;
			this.macTrackBar1.TickColor = System.Drawing.Color.FromArgb(148, 146, 148);
			this.macTrackBar1.TickFrequency = 10;
			this.macTrackBar1.TickHeight = 4;
			this.macTrackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			this.macTrackBar1.TrackerColor = System.Drawing.Color.FromArgb(255, 110, 0);
			this.macTrackBar1.TrackerSize = new System.Drawing.Size(16, 16);
			this.macTrackBar1.TrackLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
			this.macTrackBar1.TrackLineHeight = 3;
			this.macTrackBar1.Value = 0;
			this.macTrackBar1.VolumeLineColor = System.Drawing.SystemColors.Control;
			this.macTrackBar1.ValueChanged += Method1;
			this.btnMute.Appearance = System.Windows.Forms.Appearance.Button;
			this.btnMute.AutoSize = true;
			this.btnMute.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnMute.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnMute.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
			this.btnMute.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnMute.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnMute.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnMute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMute.Image = CheVolume.Properties.Resources.mute;
			this.btnMute.Location = new System.Drawing.Point(287, 12);
			this.btnMute.Margin = new System.Windows.Forms.Padding(4);
			this.btnMute.Name = "btnMute";
			this.btnMute.Padding = new System.Windows.Forms.Padding(0, 0, 3, 2);
			this.btnMute.Size = new System.Drawing.Size(34, 34);
			this.btnMute.TabIndex = 7;
			this.btnMute.UseVisualStyleBackColor = false;
			this.btnMute.CheckedChanged += new System.EventHandler(btnMute_CheckedChanged);
			this.btnMute.MouseEnter += new System.EventHandler(btnMute_MouseEnter);
			this.btnMute.MouseLeave += new System.EventHandler(btnMute_MouseLeave);
			this.btnMute.MouseUp += new System.Windows.Forms.MouseEventHandler(btnMute_MouseUp);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(this.chkSetDefault);
			base.Controls.Add(this.macTrackBar1);
			base.Controls.Add(this.btnMute);
			base.Controls.Add(this.lblProcessName);
			base.Controls.Add(this.IconBox);
			base.Margin = new System.Windows.Forms.Padding(0);
			base.Name = "DeviceVolumeControl2";
			base.Size = new System.Drawing.Size(364, 81);
			base.Paint += new System.Windows.Forms.PaintEventHandler(_OnPaint);
			((System.ComponentModel.ISupportInitialize)this.IconBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
