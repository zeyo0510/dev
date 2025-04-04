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
		private delegate void SYS_Invoke_FlowLayoutPanel(AudioSessionControl2 A_0);

		[CompilerGenerated]
		private sealed class SYS_Comapre_SessionVolumeControl
		{
			public AudioSessionControl2 obj1;

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

		private MMDeviceCollection mmDeviceCollection1;

		public MMDevice mmDevice1;

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

		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int _SendMessage(IntPtr P_0, int P_1, int P_2, int P_3);

		public FlowLayoutPanel1(MMDevice P_0)
		{
			InitializeComponent();
			mmDeviceCollection1 = Class4.mmDeviceEnumerator1.GetDefaultAudioEndpoint(EDataFlow.eRender, EDeviceState.Active);
			mmDevice1 = P_0;
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

		public int AlwaysZero(AudioSessionControl2 P_0)
		{
			return 0;
		}

		private void Add(AudioSessionControl2 P_0)
		{
			if (base.InvokeRequired)
			{
				BeginInvoke(new SYS_Invoke_FlowLayoutPanel(Add), P_0);
				return;
			}
			Process process = null;
			try
			{
				process = Process.GetProcessById((int)P_0.ProcessID);
			}
			catch (Exception)
			{
				process = null;
			}
			AudioSessionState audioSessionState = P_0.GetState();
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
					return string.Compare(A_P_0.Tag.ToString().ToLower(), P_0.SessionInstanceIdentifier.ToLower()) == 0;
				});
			}
			if (sessionVolumeControl == null && audioSessionState != AudioSessionState.AudioSessionStateExpired)
			{
				sessionVolumeControl = new SessionVolumeControl(P_0, process);
				int value = int.Parse(Math.Ceiling(P_0.SetVolume() * 100f).ToString());
				sessionVolumeControl.mmDeviceCollection1 = mmDeviceCollection1;
				sessionVolumeControl.mmDevice1 = mmDevice1;
				sessionVolumeControl.btnMute.Checked = P_0.GetMute();
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
			this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			base.Margin = new Padding(10, 0, 0, 0);
			base.Padding = new Padding(2);
			base.WrapContents = false;
			base.ControlAdded += new ControlEventHandler(_OnControlAdded);
			base.Paint += new PaintEventHandler(_OnPaint);
			base.Resize += new System.EventHandler(_OnResize);
			base.ResumeLayout(false);
		}
	}
}
