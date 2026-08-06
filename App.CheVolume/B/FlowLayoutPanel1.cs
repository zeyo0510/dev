using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using a;
using AudioCore;
using AudioCore.Interfaces;
using CheVolume.Controls;
using CheVolume.Properties;

namespace B
{
	public class FlowLayoutPanel1 : FlowLayoutPanel, IAudioSessionNotificationCollection
	{
		private delegate void SYS_Invoke_FlowLayoutPanel(AudioSession A_0);

		[CompilerGenerated]
		private sealed class SYS_Comapre_SessionVolumeControl
		{
			public AudioSession obj1;

			internal bool Equal(SessionVolumeControl P_0)
			{
				return string.Compare(P_0.Tag.ToString().ToLower(), obj1.SessionInstanceIdentifier.ToLower()) == 0;
			}
		}

		public const int FIXED_161 = 161;

		public const int FIXED_2 = 2;

		public Point point1 = Point.Empty;

		private int controlOffsetX = 10;

		private int controlFixedY = 10;

		private AudioDeviceCollection mmDeviceCollection1;

		public AudioDevice mmDevice1;

		private AudioSessionManager2 audioSessionManager21;

		private IContainer components;

		[DllImport("Shell32", EntryPoint = "ExtractIconEx")]
		public static extern int _ExtractIconEx(string P_0, int P_1, out IntPtr P_2, out IntPtr P_3, int P_4);

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
		private static extern bool _DestroyIcon(IntPtr P_0);

		public Icon ExtractIcon(string P_0, string P_1)
		{
			try
			{
				IntPtr intPtr;
				IntPtr intPtr2;
				_ExtractIconEx(P_0, 0, out intPtr, out intPtr2, 1);
				if (intPtr == IntPtr.Zero)
				{
					_ExtractIconEx(P_0, 0, out intPtr, out intPtr2, 1);
				}
				return Icon.FromHandle(intPtr);
			}
			catch (Exception)
			{
				return Resources.appicon;
			}
		}

		public FlowLayoutPanel1(AudioDevice _MM_DEVICE_)
		{
			InitializeComponent();
			mmDeviceCollection1 = Class4._MMDeviceEnumerator_.EnumAudioEndpoints(DataFlow.Render, EDeviceState.Active);
			mmDevice1 = _MM_DEVICE_;
			audioSessionManager21 = mmDevice1.AudioSessionManager;
			audioSessionManager21.RegisterSessionNotification(this);
			base.Size = new Size(0, 0);
			AutoSizeMode = AutoSizeMode.GrowAndShrink;
			AutoSize = true;
		}

		~FlowLayoutPanel1()
		{
			audioSessionManager21.UnregisterSessionNotification(this);
		}

		public int AlwaysZero(AudioSession P_0)
		{
			return 0;
		}

		private void Add(AudioSession _SESSION_CONTROL_)
		{
			if (base.InvokeRequired)
			{
				BeginInvoke(new SYS_Invoke_FlowLayoutPanel(Add), _SESSION_CONTROL_);
				return;
			}
			Process process = null;
			try
			{
				process = Process.GetProcessById((int)_SESSION_CONTROL_.ProcessID);
			}
			catch (Exception)
			{
				process = null;
			}
			AudioSessionState audioSessionState = _SESSION_CONTROL_.State;
			if (process == null)
			{
				return;
			}
			List<SessionVolumeControl> list = Enumerable.ToList(Enumerable.OfType<SessionVolumeControl>(base.Controls));
			SessionVolumeControl sessionVolumeControl = null;
			if (list.Count > 0)
			{
				sessionVolumeControl = list.Find(delegate(SessionVolumeControl A_P_0)
				{
					return string.Compare(A_P_0.Tag.ToString().ToLower(), _SESSION_CONTROL_.SessionInstanceIdentifier.ToLower()) == 0;
				});
			}
			if (sessionVolumeControl == null && audioSessionState != AudioSessionState.AudioSessionStateExpired)
			{
				sessionVolumeControl = new SessionVolumeControl(_SESSION_CONTROL_, process);
				int value = int.Parse(Math.Ceiling(_SESSION_CONTROL_.Volume).ToString());
				sessionVolumeControl.MMDeviceCollection = mmDeviceCollection1;
				sessionVolumeControl.AudioDevice = mmDevice1;
				sessionVolumeControl.muteCheckBox.Checked = _SESSION_CONTROL_.Mute;
				sessionVolumeControl.macTrackBar1.Value = value;
				base.Controls.Add(sessionVolumeControl);
			}
		}

		public void AdjControlPosition()
		{
			int num = 0;
			foreach (Control control in base.Controls)
			{
				num += controlOffsetX;
				control.Location = new Point(num, controlFixedY);
				num += control.Size.Width;
			}
			point1 = Point.Empty;
		}

		private void _OnControlAdded(object P_0, ControlEventArgs P_1)
		{
		}

		private void _OnPaint(object P_0, PaintEventArgs P_1)
		{
			Color color = Color.FromArgb(50, 50, 50);
			int num = 2;
			ControlPaint.DrawBorder(P_1.Graphics, base.ClientRectangle, color, num, ButtonBorderStyle.Solid, color, num, ButtonBorderStyle.Solid, color, num, ButtonBorderStyle.Solid, color, num, ButtonBorderStyle.Solid);
		}

		private void _OnResize(object P_0, EventArgs P_1)
		{
			Invalidate();
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
			base.SuspendLayout();
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			base.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			base.Padding = new System.Windows.Forms.Padding(2);
			base.WrapContents = false;
			base.ControlAdded += new System.Windows.Forms.ControlEventHandler(_OnControlAdded);
			base.Paint += new System.Windows.Forms.PaintEventHandler(_OnPaint);
			base.Resize += new System.EventHandler(_OnResize);
			base.ResumeLayout(false);
		}
	}
}
