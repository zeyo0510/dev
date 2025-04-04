using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using a;
using AudioCore;
using AudioCore.Interfaces;
using AudioCore2;
using CheVolume.Properties;
using EConTech.Windows.MACUI;
using Microsoft.Win32;

namespace CheVolume.Controls
{
	public class SessionVolumeControl : UserControl, IAudioSessionEvents
	{
		[Serializable]
		internal struct WINDOWPLACEMENT
		{
			public int LENGTH;

			public int FLAGS;

			public Enum2 SHOWCMD;

			public Point MIN_POS;

			public Point MAX_POS;

			public Rectangle NormalPosition;
		}

		internal enum Enum2
		{
			Item1,
			Item2,
			Item3,
			Item4
		}

		public enum Enum1
		{
			Item1,
			Item2
		}

		private delegate void RemoveSessioNotif2_SYS();

		private delegate void SYS_EMPTY_INVOKE_V2();

		private delegate void SYS_DECIMAL_INVOKE(decimal A_0);

		private delegate void SYS_STRING_INVOKE(string A_0);

		private delegate void SYS_BOOL_INVOKE_V2(bool A_0);

		private delegate void OnStateChanged2_SYS(AudioSessionState A_0);

		private delegate void SYS_FLOAT_INVOKE(float A_0);

		private delegate void RemoveSessionNotif_SYS();

		private delegate void SYS_BOOL_INVOKE(bool A_0);

		private Timer timer1;

		public AudioSessionControl2 audioSessionControl21;

		public MMDevice mmDevice1;

		public MMDeviceCollection mmDeviceCollection1;

		public Process process1;

		public string str1;

		private int num2;

		private bool bool1;

		private readonly AudioPolicyConfigService audioPolicyConfigService1;

		private readonly Enum1 enum1;

		private int num1;

		private IContainer components;

		public PictureBox IconBox;

		public LedBar lBarLeft;

		public Label lblProcessName;

		public CheCheckBox btnMute;

		public MACTrackBar macTrackBar1;

		private Label lblVolume;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem toolStripMenuItem2;

		private ToolStripMenuItem toolStripMenuItem3;

		private ToolStripMenuItem toolStripMenuItem4;

		public CheCheckBox btnLock;

		public Button btnTransfert;

		public CheCheckBox BtnShowProcess;

		private Label lblPId;

		private int VolumeValue
		{
			get
			{
				return macTrackBar1.Value;
			}
		}

		public string[] ListOfProcessesLocked
		{
			get
			{
				if (Settings.Default["ListOfProcessesLocked"] == null)
				{
					Settings.Default["ListOfProcessesLocked"] = "";
				}
				return Settings.Default["ListOfProcessesLocked"].ToString().Replace("|", "").Split('\\');
			}
		}

		private EDataFlow Flow
		{
			get
			{
				if (enum1 != 0)
				{
					return EDataFlow.eCapture;
				}
				return EDataFlow.eRender;
			}
		}

		public string[] InjectedProcesses
		{
			get
			{
				return Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("CheVolume", true).OpenSubKey("Data", true)
					.GetValueNames();
			}
		}

		public bool IsProcessInjected
		{
			get
			{
				try
				{
					if (Enumerable.Contains(InjectedProcesses, process1.ProcessName))
					{
						return true;
					}
					return false;
				}
				catch (Exception)
				{
					return false;
				}
			}
		}

		public bool IsAdvancedUser
		{
			get
			{
				try
				{
					bool isAdvancedUser = Settings.Default.IsAdvancedUser;
				}
				catch (Exception)
				{
					Settings.Default.IsAdvancedUser = false;
				}
				return Settings.Default.IsAdvancedUser;
			}
			set
			{
				try
				{
					bool isAdvancedUser = Settings.Default.IsAdvancedUser;
				}
				catch (Exception)
				{
					Settings.Default.IsAdvancedUser = false;
				}
				Settings.Default.IsAdvancedUser = value;
				Settings.Default.Save();
			}
		}

		[DllImport("user32.dll", EntryPoint = "GetWindowPlacement", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool _GetWindowPlacement(IntPtr P_0, ref WINDOWPLACEMENT P_1);

		private static WINDOWPLACEMENT GetWindowPlacement(IntPtr P_0)
		{
			WINDOWPLACEMENT wINDOWPLACEMENT = default(WINDOWPLACEMENT);
			wINDOWPLACEMENT.LENGTH = Marshal.SizeOf(wINDOWPLACEMENT);
			_GetWindowPlacement(P_0, ref wINDOWPLACEMENT);
			return wINDOWPLACEMENT;
		}

		[DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
		private static extern bool _SetForegroundWindow(IntPtr P_0);

		[DllImport("user32.dll", EntryPoint = "ShowWindowAsync")]
		public static extern bool _ShowWindowAsync(HandleRef P_0, int P_1);

		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
		private static extern bool _DestroyIcon(IntPtr P_0);

		[DllImport("kernel32.dll", EntryPoint = "IsWow64Process", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool _IsWow64Process([In] IntPtr P_0, out bool P_1);

		[DllImport("Shell32", EntryPoint = "ExtractIconEx")]
		public static extern int _ExtractIconEx(string P_0, int P_1, out IntPtr P_2, out IntPtr P_3, int P_4);

		private bool ProcessExists(Process P_0)
		{
			if (!P_0.HasExited)
			{
				if (Enumerable.Contains(ListOfProcessesLocked, P_0.ProcessName))
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public SessionVolumeControl(AudioSessionControl2 P_0, Process P_1)
		{
			audioPolicyConfigService1 = new AudioPolicyConfigService(Flow);
			InitializeComponent();
			audioSessionControl21 = P_0;
			process1 = P_1;
			process1.Refresh();
			lblPId.Text = process1.Id.ToString();
			timer1 = new Timer();
			timer1.Tick += timer1_Tick;
			timer1.Interval = 10;
			RegisterAudioSessionNotification(this);
			base.Tag = audioSessionControl21.SessionInstanceIdentifier.ToString();
			bool1 = false;
			base.Margin = new Padding(0);
			string audioSetivceDLL = GetAudioSetivceDLL(process1);
			Icon icon = ExtractIcon(audioSetivceDLL, Application.ExecutablePath);
			IconBox.Image = icon.ToBitmap();
			_DestroyIcon(icon.Handle);
			lblProcessName.Text = ((process1.Id == 0) ? "System Sounds" : process1.MainWindowTitle);
			str1 = process1.ProcessName;
			if (process1.Id == 0)
			{
				btnLock.Visible = false;
				btnTransfert.Visible = false;
				BtnShowProcess.Visible = false;
				btnMute.Location = new Point(base.Size.Width / 2 - btnMute.Width / 2, btnMute.Location.Y);
			}
			OnStateChanged2(audioSessionControl21.GetState());
		}

		private void Method1()
		{
		}

		private void process1_Exited(object P_0, EventArgs P_1)
		{
			RemoveSessioNotif2();
		}

		private void RemoveSessioNotif2()
		{
			if (base.InvokeRequired)
			{
				Invoke(new RemoveSessioNotif2_SYS(RemoveSessioNotif2));
				return;
			}
			try
			{
				base.Parent.Controls.Remove(this);
				UnregisterAudioSessionNotification(this);
			}
			catch (Exception)
			{
			}
		}

		~SessionVolumeControl()
		{
			try
			{
				RemoveSessionNotif();
			}
			catch (Exception)
			{
			}
		}

		private void _OnLoad(object P_0, EventArgs P_1)
		{
			if (process1.Id != 0)
			{
				try
				{
					process1.EnableRaisingEvents = true;
					process1.Exited += process1_Exited;
				}
				catch (Exception)
				{
				}
			}
		}

		public void RegisterAudioSessionNotification(IAudioSessionEvents P_0)
		{
			audioSessionControl21.RegisterAudioSessionNotification(P_0);
		}

		public void UnregisterAudioSessionNotification(IAudioSessionEvents P_0)
		{
			audioSessionControl21.UnregisterAudioSessionNotification(P_0);
		}

		private static void StaticMethod1(IAsyncResult P_0)
		{
		}

		public int OnDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string P_0, Guid P_1)
		{
			return 0;
		}

		public int OnIconPathChanged(string P_0, Guid P_1)
		{
			return 0;
		}

		public int OnSimpleVolumeChanged(float P_0, bool P_1, Guid P_2)
		{
			SetMute(P_1);
			SetVolumeText(Math.Ceiling(P_0 * 100f).ToString());
			if (!macTrackBar1.bool1)
			{
				SetTrackBar((decimal)Math.Ceiling(P_0 * 100f));
			}
			return 0;
		}

		public int OnChannelVolumeChanged(uint P_0, IntPtr P_1, uint P_2, Guid P_3)
		{
			return 0;
		}

		public int OnGroupingParamChanged(Guid P_0, Guid P_1)
		{
			return 0;
		}

		public int OnStateChanged(AudioSessionState P_0)
		{
			process1.Refresh();
			OnStateChanged2(P_0);
			return 0;
		}

		public int OnSessionDisconnected(AudioSessionDisconnectReason P_0)
		{
			RemoveSessionNotif();
			return 0;
		}

		private void timer1_Tick(object P_0, EventArgs P_1)
		{
			float[] source = new float[1];
			try
			{
				source = audioSessionControl21.GetChannelsPeakValues();
			}
			catch (Exception)
			{
			}
			if (Enumerable.Count(source) > 0)
			{
				float value = Enumerable.Max(source);
				lBarLeft.SetValue(value);
			}
			else
			{
				lBarLeft.SetValue(0f);
			}
			if (num1++ < 100)
			{
				return;
			}
			if (process1 != null && process1.Id != 0)
			{
				process1.Refresh();
				try
				{
					lblProcessName.Text = process1.MainWindowTitle;
				}
				catch (Exception)
				{
				}
			}
			num1 = 0;
		}

		private void contextMenuStrip1_Closed(object P_0, ToolStripDropDownClosedEventArgs P_1)
		{
			lblVolume.Focus();
		}

		private void btnMute_CheckedChanged(object P_0, EventArgs P_1)
		{
			if (btnMute.Checked)
			{
				lBarLeft.IsMuted = true;
				macTrackBar1.TrackerColor = Color.DarkGray;
				btnMute.Image = Resources.muteon;
				btnMute.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
			}
			else
			{
				lBarLeft.IsMuted = false;
				macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
				btnMute.Image = Resources.mute;
				btnMute.FlatAppearance.BorderColor = Color.DarkGray;
			}
			audioSessionControl21.SetMute(btnMute.Checked);
			lblVolume.Focus();
		}

		private void lBarLeft_Load(object P_0, EventArgs P_1)
		{
		}

		private void Obj_Method3(object P_0, ScrollEventArgs P_1)
		{
		}

		private void Obj_Method2(object P_0, EventArgs P_1)
		{
		}

		private void SetTrackBar(decimal P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_DECIMAL_INVOKE(SetTrackBar), P_0);
			}
			else
			{
				macTrackBar1.Value = (int)P_0;
			}
		}

		private void macTrackBar1_ValueChanged(object P_0, decimal P_1)
		{
			if (!bool1)
			{
				bool1 = true;
				return;
			}
			if (P_1 > 100m)
			{
				P_1 = 100m;
			}
			if (P_1 < 0m)
			{
				P_1 = default(decimal);
			}
			if (macTrackBar1.bool1)
			{
				macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
				audioSessionControl21.SetMute(false);
				audioSessionControl21.SetVolume((int)P_1);
			}
			else
			{
				SetTrackBar(P_1);
			}
		}

		private void Obj_Method1(object P_0, EventArgs P_1)
		{
		}

		private void btnTransfert_Click2()
		{
			contextMenuStrip1.Items.Clear();
			mmDeviceCollection1 = Class4.mmDeviceEnumerator1.GetDefaultAudioEndpoint(EDataFlow.eRender, EDeviceState.Active);
			int count = mmDeviceCollection1.Count;
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
			toolStripMenuItem.Text = "Move '" + process1.ProcessName + "' to  : ";
			toolStripMenuItem.Image = IconBox.Image;
			toolStripMenuItem.ForeColor = Color.FromArgb(50, 50, 50);
			toolStripMenuItem.TextImageRelation = TextImageRelation.ImageAboveText;
			contextMenuStrip1.Items.Add(toolStripMenuItem);
			contextMenuStrip1.Items.Add(new ToolStripSeparator());
			int num = toolStripMenuItem.Width;
			for (int i = 0; i < count; i++)
			{
				ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
				toolStripMenuItem2.Text = mmDeviceCollection1[i].FriendlyName ?? "";
				Icon icon = ImageHelper.Method1(mmDeviceCollection1[i].IconPath);
				if (icon.Height > 0)
				{
					toolStripMenuItem2.Image = icon.ToBitmap();
				}
				_DestroyIcon(icon.Handle);
				toolStripMenuItem2.Tag = mmDeviceCollection1[i].ID;
				toolStripMenuItem2.ForeColor = Color.FromArgb(50, 50, 50);
				toolStripMenuItem2.MouseEnter += toolStripMenuItem_MouseEnter;
				toolStripMenuItem2.Click += toolStripMenuItem2_Click;
				toolStripMenuItem2.MouseEnter += toolStripMenuItem_MouseEnter;
				toolStripMenuItem2.MouseLeave += toolStripMenuItem_MouseLeave;
				contextMenuStrip1.Items.Add(toolStripMenuItem2);
				contextMenuStrip1.Refresh();
			}
			ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
			contextMenuStrip1.Items.Add(toolStripSeparator);
			try
			{
				if (Enumerable.Contains(InjectedProcesses, process1.ProcessName))
				{
					ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
					toolStripMenuItem3.Text = "Return to Default";
					toolStripMenuItem3.Tag = base.Tag.ToString();
					toolStripMenuItem3.Click += toolStripMenuItem3_Click;
					toolStripMenuItem3.MouseEnter += toolStripMenuItem_MouseEnter;
					toolStripMenuItem3.MouseLeave += toolStripMenuItem_MouseLeave;
					toolStripMenuItem3.Image = Resources.CheV;
					toolStripMenuItem3.ForeColor = Color.FromArgb(50, 50, 50);
					contextMenuStrip1.Items.Add(toolStripMenuItem3);
					if (toolStripMenuItem3.Width > num)
					{
						num = toolStripMenuItem3.Width;
					}
				}
				else
				{
					toolStripSeparator.Margin = new Padding(0, 0, 5, 5);
				}
			}
			catch (Exception)
			{
			}
			int left = contextMenuStrip1.Width / 2 - toolStripMenuItem.Width + contextMenuStrip1.Items.Count * 2 - 1;
			toolStripMenuItem.Margin = new Padding(left, 5, 0, 5);
		}

		private void toolStripMenuItem_MouseLeave(object P_0, EventArgs P_1)
		{
			contextMenuStrip1.Cursor = Cursors.Arrow;
		}

		private void toolStripMenuItem_MouseEnter(object P_0, EventArgs P_1)
		{
			contextMenuStrip1.Cursor = Cursors.Hand;
		}

		private void toolStripMenuItem3_Click(object P_0, EventArgs P_1)
		{
			ToolStripMenuItem toolStripMenuItem5 = (ToolStripMenuItem)P_0;
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software", true);
			registryKey = registryKey.OpenSubKey("CheVolume", true);
			registryKey = registryKey.OpenSubKey("Data", true);
			if (registryKey.GetValue(str1) != null)
			{
				registryKey.DeleteValue(str1);
			}
			int count = mmDeviceCollection1.Count;
			audioPolicyConfigService1.SetDefaultEndPoint(null, process1.Id);
			Method1();
		}

		public void toolStripMenuItem2_Click(object P_0, EventArgs P_1)
		{
			ToolStripMenuItem obj = (ToolStripMenuItem)P_0;
			string text = process1.Id.ToString();
			string text2 = obj.Tag.ToString();
			CreateRegistry(text, text2);
			audioPolicyConfigService1.SetDefaultEndPoint(text2, process1.Id);
			Method1();
		}

		private void CreateRegistry(string P_0, string P_1)
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software", true);
			registryKey.CreateSubKey("CheVolume");
			RegistryKey registryKey2 = registryKey.OpenSubKey("CheVolume", true);
			registryKey2.CreateSubKey("Data");
			registryKey2.OpenSubKey("Data", true).SetValue(str1, P_1);
		}

		private void btnTransfert_Click(object P_0, EventArgs P_1)
		{
			btnTransfert_Click2();
			btnTransfert.PointToScreen(new Point(btnTransfert.Left, btnTransfert.Bottom));
			contextMenuStrip1.Show(btnTransfert, new Point(-contextMenuStrip1.Size.Width / 2 + btnTransfert.Width / 2, -contextMenuStrip1.Size.Height - 5));
		}

		public void SetMute(bool P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_BOOL_INVOKE_V2(SetMute), P_0);
			}
			else
			{
				btnMute.Checked = P_0;
			}
		}

		public void SetVolumeText(string P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_STRING_INVOKE(SetVolumeText), P_0);
			}
			else
			{
				lblVolume.Text = P_0;
			}
		}

		public void RefreshProcess(string P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_STRING_INVOKE(RefreshProcess), P_0);
			}
			else
			{
				process1.Refresh();
				lblProcessName.Text = process1.MainWindowTitle;
			}
		}

		public void SetVolume(float P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_FLOAT_INVOKE(SetVolume), P_0);
			}
			else if (!macTrackBar1.bool1)
			{
				macTrackBar1.Value = int.Parse(Math.Ceiling(P_0 * 100f).ToString());
			}
		}

		public void RemoveSessionNotif()
		{
			if (base.InvokeRequired)
			{
				Invoke(new RemoveSessionNotif_SYS(RemoveSessionNotif));
				return;
			}
			UnregisterAudioSessionNotification(this);
			base.Parent.Controls.Remove(this);
		}

		private void SetVisible(bool P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new SYS_BOOL_INVOKE(SetVisible), P_0);
			}
			else
			{
				Method1();
				base.Visible = P_0;
			}
		}

		public void OnStateChanged2(AudioSessionState P_0)
		{
			if (base.InvokeRequired)
			{
				Invoke(new OnStateChanged2_SYS(OnStateChanged2), P_0);
				return;
			}
			switch (P_0)
			{
			case AudioSessionState.AudioSessionStateActive:
				SetVisible(true);
				timer1.Start();
				break;
			case AudioSessionState.AudioSessionStateInactive:
				if (IsAdvancedUser)
				{
					SetVisible(true);
					lBarLeft.SetValue(0f);
					timer1.Start();
				}
				else
				{
					SetVisible(false);
					lBarLeft.SetValue(0f);
					timer1.Stop();
				}
				break;
			case AudioSessionState.AudioSessionStateExpired:
				SetVisible(false);
				try
				{
					base.Parent.Controls.Remove(this);
				}
				catch (Exception)
				{
				}
				timer1.Stop();
				break;
			}
		}

		private void btnLock_CheckedChanged(object P_0, EventArgs P_1)
		{
			if (btnLock.Checked)
			{
				btnLock.Image = Resources._lock;
				btnLock.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
				btnTransfert.BackgroundImage = Resources._lock;
				btnTransfert.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
				btnTransfert.Enabled = false;
				if (!process1.HasExited && !Enumerable.Contains(Settings.Default["ListOfProcessesLocked"].ToString().Split('\\'), process1.ProcessName + "|"))
				{
					Settings.Default["ListOfProcessesLocked"] = string.Concat(Settings.Default["ListOfProcessesLocked"].ToString(), "\\" + process1.ProcessName + "|");
				}
			}
			else
			{
				btnLock.Image = Resources.lockoff;
				btnLock.FlatAppearance.BorderColor = Color.DarkGray;
				btnTransfert.BackgroundImage = Resources.transfert;
				btnTransfert.Enabled = true;
				btnTransfert.FlatAppearance.BorderColor = Color.DarkGray;
				if (!process1.HasExited && !Enumerable.Contains(Settings.Default["ListOfProcessesLocked"].ToString().Split('\\'), process1.ProcessName))
				{
					Settings.Default["ListOfProcessesLocked"] = Settings.Default["ListOfProcessesLocked"].ToString().Replace("\\" + process1.ProcessName + "|", "");
				}
			}
			Settings.Default.Save();
			lblVolume.Focus();
		}

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

		private string GetAudioSetivceDLL(Process P_0)
		{
			if (P_0.Id == 0)
			{
				return Environment.SystemDirectory + "\\audiosrv.dll";
			}
			try
			{
				return P_0.MainModule.FileName;
			}
			catch
			{
				foreach (ManagementObject item in new ManagementObjectSearcher("SELECT ExecutablePath, ProcessID FROM Win32_Process").Get())
				{
					object obj2 = item["ProcessID"];
					object obj3 = item["ExecutablePath"];
					if (obj3 != null && obj2.ToString() == P_0.Id.ToString())
					{
						return obj3.ToString();
					}
				}
			}
			return Application.ExecutablePath;
		}

		private void BtnShowProcess_MouseHover(object P_0, EventArgs P_1)
		{
		}

		private void BtnShowProcess_MouseLeave(object P_0, EventArgs P_1)
		{
			BtnShowProcess.Image = Resources.showwindow;
			BtnShowProcess.FlatAppearance.BorderColor = Color.DarkGray;
		}

		private void BtnShowProcess_MouseEnter(object P_0, EventArgs P_1)
		{
			BtnShowProcess.Image = Resources.showwindowover;
			BtnShowProcess.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
		}

		private void BtnShowProcess_Click(object P_0, EventArgs P_1)
		{
			if (GetWindowPlacement(process1.MainWindowHandle).SHOWCMD == Enum2.Item3)
			{
				_ShowWindowAsync(new HandleRef(null, process1.MainWindowHandle), 10);
			}
			else
			{
				_ShowWindowAsync(new HandleRef(null, process1.MainWindowHandle), 5);
			}
			_SetForegroundWindow(process1.MainWindowHandle);
		}

		private void IconBox_Click(object P_0, EventArgs P_1)
		{
			if (IsAdvancedUser)
			{
				lblPId.Visible = !lblPId.Visible;
			}
		}

		private void btnTransfert_MouseHover(object P_0, EventArgs P_1)
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

		private void btnTransfert_MouseLeave(object P_0, EventArgs P_1)
		{
		}

		private void btnTransfert_MouseEnter(object P_0, EventArgs P_1)
		{
			btnTransfert.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
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
			this.components = new System.ComponentModel.Container();
			this.lblProcessName = new System.Windows.Forms.Label();
			this.lblVolume = new System.Windows.Forms.Label();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.lblPId = new System.Windows.Forms.Label();
			this.btnTransfert = new System.Windows.Forms.Button();
			this.IconBox = new System.Windows.Forms.PictureBox();
			this.BtnShowProcess = new CheVolume.Controls.CheCheckBox();
			this.btnLock = new CheVolume.Controls.CheCheckBox();
			this.macTrackBar1 = new EConTech.Windows.MACUI.MACTrackBar();
			this.btnMute = new CheVolume.Controls.CheCheckBox();
			this.lBarLeft = new CheVolume.Controls.LedBar();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.IconBox).BeginInit();
			base.SuspendLayout();
			this.lblProcessName.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
			this.lblProcessName.ForeColor = System.Drawing.Color.DimGray;
			this.lblProcessName.Location = new System.Drawing.Point(2, 18);
			this.lblProcessName.Margin = new System.Windows.Forms.Padding(0);
			this.lblProcessName.Name = "lblProcessName";
			this.lblProcessName.Size = new System.Drawing.Size(96, 60);
			this.lblProcessName.TabIndex = 3;
			this.lblProcessName.Text = "Temp Text";
			this.lblProcessName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblVolume.BackColor = System.Drawing.Color.Transparent;
			this.lblVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblVolume.Font = new System.Drawing.Font("Verdana", 7f, System.Drawing.FontStyle.Bold);
			this.lblVolume.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
			this.lblVolume.Location = new System.Drawing.Point(30, 371);
			this.lblVolume.Name = "lblVolume";
			this.lblVolume.Size = new System.Drawing.Size(40, 20);
			this.lblVolume.TabIndex = 11;
			this.lblVolume.Text = "100";
			this.lblVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblVolume.UseCompatibleTextRendering = true;
			this.lblVolume.UseMnemonic = false;
			this.contextMenuStrip1.Font = new System.Drawing.Font("Verdana", 11f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3, this.toolStripMenuItem4 });
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.contextMenuStrip1.Size = new System.Drawing.Size(204, 114);
			this.contextMenuStrip1.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(contextMenuStrip1_Closed);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(203, 22);
			this.toolStripMenuItem1.Text = "toolStripMenuItem1";
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(203, 22);
			this.toolStripMenuItem2.Text = "toolStripMenuItem2";
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(203, 22);
			this.toolStripMenuItem3.Text = "toolStripMenuItem3";
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(203, 22);
			this.toolStripMenuItem4.Text = "toolStripMenuItem4";
			this.lblPId.Font = new System.Drawing.Font("Verdana", 6.25f, System.Drawing.FontStyle.Bold);
			this.lblPId.ForeColor = System.Drawing.Color.DimGray;
			this.lblPId.Location = new System.Drawing.Point(2, 113);
			this.lblPId.Name = "lblPId";
			this.lblPId.Size = new System.Drawing.Size(96, 13);
			this.lblPId.TabIndex = 16;
			this.lblPId.Text = "label1";
			this.lblPId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblPId.Visible = false;
			this.btnTransfert.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnTransfert.BackgroundImage = CheVolume.Properties.Resources.CheV;
			this.btnTransfert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnTransfert.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnTransfert.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
			this.btnTransfert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnTransfert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnTransfert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTransfert.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
			this.btnTransfert.Location = new System.Drawing.Point(33, 440);
			this.btnTransfert.Name = "btnTransfert";
			this.btnTransfert.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
			this.btnTransfert.Size = new System.Drawing.Size(34, 34);
			this.btnTransfert.TabIndex = 14;
			this.btnTransfert.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnTransfert.UseVisualStyleBackColor = false;
			this.btnTransfert.Click += new System.EventHandler(btnTransfert_Click);
			this.btnTransfert.MouseEnter += new System.EventHandler(btnTransfert_MouseEnter);
			this.btnTransfert.MouseLeave += new System.EventHandler(btnTransfert_MouseLeave);
			this.btnTransfert.MouseHover += new System.EventHandler(btnTransfert_MouseHover);
			this.IconBox.BackColor = System.Drawing.Color.Transparent;
			this.IconBox.Location = new System.Drawing.Point(33, 79);
			this.IconBox.Margin = new System.Windows.Forms.Padding(0);
			this.IconBox.Name = "IconBox";
			this.IconBox.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
			this.IconBox.Size = new System.Drawing.Size(34, 34);
			this.IconBox.TabIndex = 0;
			this.IconBox.TabStop = false;
			this.IconBox.Click += new System.EventHandler(IconBox_Click);
			this.BtnShowProcess.Appearance = System.Windows.Forms.Appearance.Button;
			this.BtnShowProcess.AutoSize = true;
			this.BtnShowProcess.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.BtnShowProcess.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BtnShowProcess.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
			this.BtnShowProcess.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.BtnShowProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.BtnShowProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.BtnShowProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnShowProcess.Image = CheVolume.Properties.Resources.showwindow;
			this.BtnShowProcess.Location = new System.Drawing.Point(53, 400);
			this.BtnShowProcess.Name = "BtnShowProcess";
			this.BtnShowProcess.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
			this.BtnShowProcess.Size = new System.Drawing.Size(34, 34);
			this.BtnShowProcess.TabIndex = 15;
			this.BtnShowProcess.UseVisualStyleBackColor = false;
			this.BtnShowProcess.Click += new System.EventHandler(BtnShowProcess_Click);
			this.BtnShowProcess.MouseEnter += new System.EventHandler(BtnShowProcess_MouseEnter);
			this.BtnShowProcess.MouseLeave += new System.EventHandler(BtnShowProcess_MouseLeave);
			this.BtnShowProcess.MouseHover += new System.EventHandler(BtnShowProcess_MouseHover);
			this.btnLock.Appearance = System.Windows.Forms.Appearance.Button;
			this.btnLock.AutoSize = true;
			this.btnLock.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnLock.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnLock.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
			this.btnLock.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnLock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnLock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLock.Image = CheVolume.Properties.Resources.lockoff;
			this.btnLock.Location = new System.Drawing.Point(73, 440);
			this.btnLock.Name = "btnLock";
			this.btnLock.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
			this.btnLock.Size = new System.Drawing.Size(34, 34);
			this.btnLock.TabIndex = 15;
			this.btnLock.UseVisualStyleBackColor = false;
			this.btnLock.Visible = false;
			this.btnLock.CheckedChanged += new System.EventHandler(btnLock_CheckedChanged);
			this.macTrackBar1.BackColor = System.Drawing.Color.Transparent;
			this.macTrackBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
			this.macTrackBar1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.macTrackBar1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, 0);
			this.macTrackBar1.ForeColor = System.Drawing.Color.FromArgb(123, 125, 123);
			this.macTrackBar1.IndentHeight = 6;
			this.macTrackBar1.LargeChange = 1;
			this.macTrackBar1.Location = new System.Drawing.Point(30, 121);
			this.macTrackBar1.Maximum = 100;
			this.macTrackBar1.Minimum = 0;
			this.macTrackBar1.Name = "macTrackBar1";
			this.macTrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.macTrackBar1.Size = new System.Drawing.Size(58, 247);
			this.macTrackBar1.TabIndex = 10;
			this.macTrackBar1.TickColor = System.Drawing.Color.FromArgb(148, 146, 148);
			this.macTrackBar1.TickFrequency = 10;
			this.macTrackBar1.TickHeight = 4;
			this.macTrackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.macTrackBar1.TrackerColor = System.Drawing.Color.FromArgb(255, 128, 0);
			this.macTrackBar1.TrackerSize = new System.Drawing.Size(16, 16);
			this.macTrackBar1.TrackLineColor = System.Drawing.Color.FromArgb(90, 93, 90);
			this.macTrackBar1.TrackLineHeight = 3;
			this.macTrackBar1.Value = 0;
			this.macTrackBar1.ValueChanged += macTrackBar1_ValueChanged;
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
			this.btnMute.Location = new System.Drawing.Point(13, 400);
			this.btnMute.Name = "btnMute";
			this.btnMute.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
			this.btnMute.Size = new System.Drawing.Size(34, 34);
			this.btnMute.TabIndex = 7;
			this.btnMute.UseVisualStyleBackColor = false;
			this.btnMute.CheckedChanged += new System.EventHandler(btnMute_CheckedChanged);
			this.btnMute.MouseEnter += new System.EventHandler(btnMute_MouseEnter);
			this.btnMute.MouseLeave += new System.EventHandler(btnMute_MouseLeave);
			this.lBarLeft.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.lBarLeft.IsMuted = false;
			this.lBarLeft.Location = new System.Drawing.Point(22, 132);
			this.lBarLeft.Name = "lBarLeft";
			this.lBarLeft.Size = new System.Drawing.Size(5, 225);
			this.lBarLeft.TabIndex = 1;
			this.lBarLeft.Load += new System.EventHandler(lBarLeft_Load);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(this.lblPId);
			base.Controls.Add(this.BtnShowProcess);
			base.Controls.Add(this.btnLock);
			base.Controls.Add(this.btnTransfert);
			base.Controls.Add(this.lblVolume);
			base.Controls.Add(this.macTrackBar1);
			base.Controls.Add(this.btnMute);
			base.Controls.Add(this.lblProcessName);
			base.Controls.Add(this.lBarLeft);
			base.Controls.Add(this.IconBox);
			base.Margin = new System.Windows.Forms.Padding(0);
			base.Name = "SessionVolumeControl";
			base.Size = new System.Drawing.Size(100, 485);
			base.Load += new System.EventHandler(_OnLoad);
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.IconBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
