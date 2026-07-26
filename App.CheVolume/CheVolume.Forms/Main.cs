using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using a;
using AudioCore;
using AudioCore.Interfaces;
using B;
using CheVolume.Common;
using CheVolume.Controls;
using CheVolume.Properties;
using Microsoft.Win32;

namespace CheVolume.Forms
{
	public class Main : Form
	{
		private enum Enum1
		{
			A = 1,
			a,
			B,
			b,
			C,
			c,
			D,
			d,
			E,
			e,
			F,
			f,
			G,
			g,
			H,
			h,
			I,
			i,
			J,
			j,
			K,
			k,
			L,
			l,
			M,
			m,
			N,
			n,
			O
		}

		private enum Enum2
		{
			A = 1,
			a,
			B
		}

		private delegate void B();

		public struct ProcessMapping
		{
			public Process proc1;

			public string str1;
		}

		[CompilerGenerated]
		private sealed class Comapre1
		{
			public string string1;

			internal bool Equal(FlowLayoutPanel1 P_0)
			{
				return string.Compare(P_0.Tag.ToString(), string1) == 0;
			}
		}

		[CompilerGenerated]
		private sealed class Compare2
		{
			public AudioSessionControl2 audioSessionControl21;

			internal bool Equal(SessionVolumeControl P_0)
			{
				return string.Compare(P_0.Tag.ToString().ToLower(), audioSessionControl21.SessionInstanceIdentifier.ToLower()) == 0;
			}
		}

		[CompilerGenerated]
		private sealed class Compare3
		{
			public Main main1;

			public Point point1;

			internal bool Equal(FlowLayoutPanel1 P_0)
			{
				return P_0.Bounds.Contains(main1.pnlSessMgr.PointToClient(point1));
			}
		}

		public const int FIXED_161 = 161;

		public const int FIXED_2 = 2;

		public const int FIXED_5644 = 5644;

		private const int FIXED_2_v2 = 2;

		private const int FIXEDL_1073741824 = 1073741824;

		private const int FIXED_S_1 = -1;

		public bool bool1 = true;

		private List<int> num_list1;

		public List<ProcessMapping> processList;

		private XMLSettings xmlSettings1;

		private MMDeviceCollection mmDeviceCollection1;

		private string str1;

		private string[] str_arr1;

		private string str2;

		public NetNamedPipeBinding netNamedPipeBinding1;

		public EndpointAddress endpointAddress1;

		public ProcessStartInfo processStartInfo1;

		public MMNotificationClient mmNotificationClient1;

		internal static float dpiX;

		private string message1 = "";

		private IContainer components;

		private AudioSessionManagerPanel pnlSessMgr;

		private PictureBox pictureBox1;

		private Button btnEnterLicence;

		private Button btnBuy;

		private Button button3;

		private Button btnUpdate;

		private Label lblTitle;

		private Label label2;

		private CheCheckBox cheCheckBox1;

		private Button button1;

		private CheCheckBox cheCheckBox2;

		private ToolTip toolTip1;

		private Button button2;

		public List<int> PendingTransfers
		{
			get
			{
				if (num_list1 == null)
				{
					num_list1 = new List<int>();
				}
				return num_list1;
			}
		}

		public MMDevice defaultdev
		{
			get
			{
				return Class4.mmDeviceEnumerator1.EnumerateAudioEndPoints(EDataFlow.eRender, ERole.eMultimedia);
			}
		}

		public MMDevice selecteddev
		{
			get
			{
				if (str2 == null)
				{
					return defaultdev;
				}
				return Class4.mmDeviceEnumerator1.GetDeviceV2(str2);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
					Settings.Default.Save();
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

		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int _SendMessage(IntPtr P_0, int P_1, int P_2, int P_3);

		[DllImport("advapi32.dll", EntryPoint = "GetTokenInformation", SetLastError = true)]
		private static extern bool _GetTokenInformation(IntPtr P_0, Enum1 P_1, IntPtr P_2, int P_3, out int P_4);

		private void Method1()
		{
		}

		public Main()
		{
			mmDeviceCollection1 = Class4.mmDeviceEnumerator1.GetDefaultAudioEndpoint(EDataFlow.eRender, EDeviceState.Active);
			mmNotificationClient1 = new MMNotificationClient();
			Class4.mmDeviceEnumerator1.RegisterEndpointNotificationCallback(mmNotificationClient1);
			processList = new List<ProcessMapping>();
			CreateRegistry();
			InitializeComponent();
			using (Graphics graphics = CreateGraphics())
			{
				dpiX = graphics.DpiX / 96f;
			}
			ImageHelper.SetImageSize(dpiX);
			xmlSettings1 = new XMLSettings();
			xmlSettings1.Load();
			cheCheckBox1.Checked = IsAdvancedUser;
			MMNotificationClient mMNotificationClient = mmNotificationClient1;
			mMNotificationClient.DefaultChanged = (MMNotificationClientDeviceDelegate)Delegate.Combine(mMNotificationClient.DefaultChanged, new MMNotificationClientDeviceDelegate(mmNotificationClient1_DefaultChanged));
			MMNotificationClient mMNotificationClient2 = mmNotificationClient1;
			mMNotificationClient2.DeviceAdded = (MMNotificationClientDeviceDelegate)Delegate.Combine(mMNotificationClient2.DeviceAdded, new MMNotificationClientDeviceDelegate(mmNotificationClient1_DeviceAdded));
			MMNotificationClient mMNotificationClient3 = mmNotificationClient1;
			mMNotificationClient3.DeviceRemoved = (MMNotificationClientDeviceDelegate)Delegate.Combine(mMNotificationClient3.DeviceRemoved, new MMNotificationClientDeviceDelegate(mmNotificationClient1_DeviceRemove));
			MMNotificationClient mMNotificationClient4 = mmNotificationClient1;
			mMNotificationClient4.PropertyValueChanged = (MMNotificationClientPropertyValueDelegate)Delegate.Combine(mMNotificationClient4.PropertyValueChanged, new MMNotificationClientPropertyValueDelegate(mmNotificationClient1_PropertyValueChanged));
			bool1 = false;
			UpdateAudio();
			AutoAdjSessionManagerPanel();
		}

		private void AutoAdjSessionManagerPanel()
		{
			AnchorStyles anchor = pnlSessMgr.Anchor;
			pnlSessMgr.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			pnlSessMgr.AutoSize = true;
			pnlSessMgr.Size = new Size(0, 0);
			pnlSessMgr.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Size = new Size(0, 0);
			AutoSize = true;
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			Size size = pnlSessMgr.Size;
			Size size2 = base.Size;
			pnlSessMgr.AutoSize = false;
			AutoSize = false;
			pnlSessMgr.Size = size;
			base.Size = size2;
			pnlSessMgr.Anchor = anchor;
		}

		public void UpdateAudio()
		{
			if (base.InvokeRequired)
			{
				Invoke(delegate
				{
					UpdateAudio();
				});
				return;
			}
			string text = defaultdev.ID.ToString();
			mmDeviceCollection1 = Class4.mmDeviceEnumerator1.GetDefaultAudioEndpoint(EDataFlow.eRender, EDeviceState.Active);
			int count = mmDeviceCollection1.Count;
			for (int i = 0; i < count; i++)
			{
				MMDevice mMDevice = mmDeviceCollection1[i];
				string string1 = mMDevice.ID;
				List<FlowLayoutPanel1> list = Enumerable.ToList(Enumerable.OfType<FlowLayoutPanel1>(pnlSessMgr.Controls));
				bool @default = false;
				if (string1 == text)
				{
					@default = true;
				}
				FlowLayoutPanel1 flowLayoutPanel = list.Find(delegate(FlowLayoutPanel1 P_0)
				{
					return string.Compare(P_0.Tag.ToString(), string1) == 0;
				});
				if (flowLayoutPanel == null)
				{
					flowLayoutPanel = new FlowLayoutPanel1(mMDevice);
					flowLayoutPanel.Tag = mMDevice.ID;
					flowLayoutPanel.BackColor = Color.FromArgb(252, 252, 252);
					pnlSessMgr.Controls.Add(flowLayoutPanel);
					DeviceVolumeControl deviceVolumeControl = new DeviceVolumeControl(mMDevice);
					deviceVolumeControl.BackColor = Color.FromArgb(242, 242, 242);
					deviceVolumeControl.Tag = mMDevice.ID;
					deviceVolumeControl.SetDefault(@default);
					flowLayoutPanel.Controls.Add(deviceVolumeControl);
				}
				else
				{
					Enumerable.First(Enumerable.OfType<DeviceVolumeControl>(flowLayoutPanel.Controls)).SetDefault(@default);
				}
				int num;
				try
				{
					num = mMDevice.AudioSessionManager.GetCount();
				}
				catch (Exception)
				{
					num = 0;
				}
				for (int j = 0; j < num; j++)
				{
					AudioSessionControl2 audioSessionControl21 = mMDevice.AudioSessionManager.audioSessionEnumerator1[j];
					Process process;
					try
					{
						process = Process.GetProcessById((int)audioSessionControl21.ProcessID);
					}
					catch (Exception)
					{
						process = null;
					}
					if (process == null)
					{
						continue;
					}
					List<SessionVolumeControl> list2 = Enumerable.ToList(Enumerable.OfType<SessionVolumeControl>(flowLayoutPanel.Controls));
					SessionVolumeControl sessionVolumeControl = null;
					if (list2.Count > 0)
					{
						sessionVolumeControl = list2.Find(delegate(SessionVolumeControl P_0)
						{
							return string.Compare(P_0.Tag.ToString().ToLower(), audioSessionControl21.SessionInstanceIdentifier.ToLower()) == 0;
						});
					}
					AudioSessionState audioSessionState = audioSessionControl21.GetState();
					if (sessionVolumeControl == null && audioSessionState != AudioSessionState.AudioSessionStateExpired)
					{
						sessionVolumeControl = new SessionVolumeControl(audioSessionControl21, process);
						int value = int.Parse(Math.Ceiling(audioSessionControl21.SetVolume() * 100f).ToString());
						sessionVolumeControl.mmDeviceCollection1 = mmDeviceCollection1;
						sessionVolumeControl.mmDevice1 = mMDevice;
						sessionVolumeControl.btnMute.Checked = audioSessionControl21.GetMute();
						sessionVolumeControl.macTrackBar1.Value = value;
						flowLayoutPanel.Controls.Add(sessionVolumeControl);
						if (process.Id == 0)
						{
							flowLayoutPanel.Controls.SetChildIndex(sessionVolumeControl, 1);
						}
					}
				}
			}
			InvokeStateChanged();
		}

		private static void Method3(IAsyncResult P_0)
		{
		}

		private void mmNotificationClient1_PropertyValueChanged(string P_0, PROPERTYKEY P_1)
		{
		}

		private void mmNotificationClient1_DefaultChanged(string P_0)
		{
			if (base.InvokeRequired)
			{
				BeginInvoke(new MMNotificationClientDeviceDelegate(mmNotificationClient1_DefaultChanged), P_0);
			}
			else
			{
				UpdateAudio();
			}
		}

		private void mmNotificationClient1_DeviceAdded(string P_0)
		{
			if (base.InvokeRequired)
			{
				BeginInvoke(new MMNotificationClientDeviceDelegate(mmNotificationClient1_DeviceAdded), P_0);
			}
			else
			{
				UpdateAudio();
			}
		}

		private void mmNotificationClient1_DeviceRemove(string P_0)
		{
			if (base.InvokeRequired)
			{
				BeginInvoke(new MMNotificationClientDeviceDelegate(mmNotificationClient1_DeviceRemove), P_0);
				return;
			}
			foreach (FlowLayoutPanel1 item in Enumerable.ToList(Enumerable.OfType<FlowLayoutPanel1>(pnlSessMgr.Controls)))
			{
				if (string.Compare(item.Tag.ToString(), P_0) == 0)
				{
					pnlSessMgr.Controls.Remove(item);
				}
			}
		}

		private void CreateRegistry()
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software", true);
			if (registryKey.OpenSubKey("CheVolume", true) == null)
			{
				registryKey.CreateSubKey("CheVolume");
			}
			registryKey = registryKey.OpenSubKey("CheVolume", true);
			if (registryKey.OpenSubKey("Data", true) == null)
			{
				registryKey.CreateSubKey("Data");
			}
		}

		private void Method2()
		{
		}

		private void Obj_Method1(object P_0, MouseEventArgs P_1)
		{
			if (P_1.Button == MouseButtons.Right)
			{
				string text = ((CheRadioButton)P_0).Tag.ToString();
				Class4.mmDeviceEnumerator1.GetDeviceV2(text);
			}
		}

		private void Obj_Method2(object P_0, EventArgs P_1)
		{
			CheRadioButton cheRadioButton = (CheRadioButton)P_0;
			if (cheRadioButton.Checked)
			{
				str2 = cheRadioButton.Tag.ToString();
				Class4.Method3(selecteddev.ID);
			}
		}

		private bool HookedMutex(int P_0)
		{
			try
			{
				Mutex.OpenExisting(string.Format("MyHookedMutex.{0}", P_0));
				return true;
			}
			catch (WaitHandleCannotBeOpenedException)
			{
				return false;
			}
		}

		private void Obj_Method3(object P_0, KeyEventArgs P_1)
		{
			Keys keyCode = P_1.KeyCode;
			int num = 13;
		}

		public void ShowMe()
		{
			try
			{
				Show();
				base.WindowState = FormWindowState.Normal;
				Activate();
				if (pnlSessMgr.ClientSize.Width + pnlSessMgr.Margin.Left + pnlSessMgr.Margin.Right + pnlSessMgr.Location.X + 16 > base.Size.Width)
				{
					AutoAdjSessionManagerPanel();
				}
				BringToFront();
			}
			catch (Exception)
			{
			}
		}

		private void Obj_Method4(object P_0, MouseEventArgs P_1)
		{
			if (P_1.Button == MouseButtons.Right)
			{
				ShowMe();
			}
		}

		private void _OnFormClosing(object P_0, FormClosingEventArgs P_1)
		{
			P_1.Cancel = true;
			Hide();
		}

		private void Obj_Method5(object P_0, EventArgs P_1)
		{
			Environment.Exit(0);
		}

		private void button3_Click(object P_0, EventArgs P_1)
		{
			Process.Start("control.exe", "mmsys.cpl,, 2");
		}

		public bool CheckUpdate(unknown6 P_0)
		{
			if (P_0.bool1 || P_0.str1.Length == 0 || P_0.ver1 == null)
			{
				MessageBox.Show(this, "Error while looking for the newest version", "Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			if (version.CompareTo(P_0.ver1) >= 0)
			{
				MessageBox.Show(this, "No new version detected", "Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
			string text = string.Format("New version found!\nYour version: {0}.\nNewest version: {1}.", version, P_0.ver1);
			return DialogResult.Yes == MessageBox.Show(this, text, "Check for updates", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		public void RunUpdate(unknown4 P_0)
		{
			if (P_0.bool1)
			{
				MessageBox.Show(this, "Error while downloading the installer", "Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (DialogResult.Yes != MessageBox.Show(this, "Do you know to install the newest version?", "Check for updates", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				try
				{
					File.Delete(P_0.str1);
					return;
				}
				catch
				{
					return;
				}
			}
			try
			{
				Process.Start(P_0.str1);
				Close();
				Environment.Exit(0);
			}
			catch (Exception)
			{
				MessageBox.Show(this, "Error while running the installer.", "Check for updates", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				try
				{
					File.Delete(P_0.str1);
				}
				catch
				{
				}
			}
		}

		private void _OnLoad(object P_0, EventArgs P_1)
		{
			if (string.Compare(xmlSettings1.Get("LicenseKey"), Program.licenseKey) == 0)
			{
				lblTitle.Text = "Full Version";
				message1 = "";
				message1 = message1 + "License Key\r\r " + xmlSettings1.Get("LicenseKey") + "\r";
				message1 += "\rThank you for buying CheVolume";
				lblTitle.Cursor = Cursors.Help;
				lblTitle.MouseHover += lblTitle_MouseHover;
				btnBuy.Hide();
				btnEnterLicence.Hide();
			}
			else
			{
				lblTitle.Text = "Evaluation Version - " + (1 + (Program.StartupDateTime - DateTime.UtcNow).Days) + " days left";
			}
		}

		private void lblTitle_MouseHover(object P_0, EventArgs P_1)
		{
			toolTip1.Show(message1, lblTitle);
		}

		private void Obj_Method10(object P_0, MouseEventArgs P_1)
		{
			message1 = "";
			message1 = message1 + "\rLicense Key\r " + xmlSettings1.Get("LicenseKey") + "\r";
			message1 += "\rThank you for buying CheVolume";
		}

		private void btnBuy_Click(object P_0, EventArgs P_1)
		{
			Process.Start("http://www.chevolume.com/BuyNow.aspx");
		}

		private void label2_Click(object P_0, EventArgs P_1)
		{
		}

		private void Obj_Method6(object P_0, LinkLabelLinkClickedEventArgs P_1)
		{
			Process.Start("http://www.chevolume.com");
		}

		private void Obj_Method7(object P_0, ControlEventArgs P_1)
		{
		}

		private void Obj_Method8(object P_0, UICuesEventArgs P_1)
		{
		}

		private void _OnMouseMove(object P_0, MouseEventArgs P_1)
		{
		}

		private void Obj_Method11(object P_0, EventArgs P_1)
		{
		}

		private void Obj_Method14(object P_0, EventArgs P_1)
		{
		}

		private void cheCheckBox1_CheckedChanged(object P_0, EventArgs P_1)
		{
			if (cheCheckBox1.Checked)
			{
				cheCheckBox1.Image = Resources.AdvancedUserOn;
			}
			else
			{
				cheCheckBox1.Image = Resources.AdvancedUser;
			}
			IsAdvancedUser = cheCheckBox1.Checked;
			InvokeStateChanged();
			if (!bool1)
			{
				AutoAdjSessionManagerPanel();
			}
			lblTitle.Focus();
		}

		private void InvokeStateChanged()
		{
			foreach (FlowLayoutPanel1 item in Enumerable.ToList(Enumerable.OfType<FlowLayoutPanel1>(pnlSessMgr.Controls)))
			{
				foreach (SessionVolumeControl item2 in Enumerable.ToList(Enumerable.OfType<SessionVolumeControl>(item.Controls)))
				{
					item2.OnStateChanged2(item2.audioSessionControl21.GetState());
				}
			}
		}

		private void _OnResizeEnd(object P_0, EventArgs P_1)
		{
		}

		private void pnlSessMgr_SizeChanged(object P_0, EventArgs P_1)
		{
		}

		private void button1_MouseEnter(object P_0, EventArgs P_1)
		{
			button1.Image = Resources.GoToWebsiteHover;
		}

		private void button1_MouseLeave(object P_0, EventArgs P_1)
		{
			button1.Image = Resources.GoToWebsite;
		}

		private void button1_Click(object P_0, EventArgs P_1)
		{
			Process.Start("http://www.chevolume.com");
			lblTitle.Focus();
		}

		private void pnlSessMgr_ControlAdded(object P_0, ControlEventArgs P_1)
		{
		}

		private void cheCheckBox2_CheckedChanged(object P_0, EventArgs P_1)
		{
			if (cheCheckBox2.Checked)
			{
				cheCheckBox2.Image = Resources.PinnedOn;
			}
			else
			{
				cheCheckBox2.Image = Resources.Pinned;
			}
			base.TopMost = cheCheckBox2.Checked;
		}

		private void Obj_Method12(object P_0, MouseEventArgs P_1)
		{
		}

		private void Obj_Method9(object P_0, CancelEventArgs P_1)
		{
		}

		private void toolTip1_Popup(object P_0, PopupEventArgs P_1)
		{
		}

		private void button2_Click(object P_0, EventArgs P_1)
		{
			Environment.Exit(0);
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
			// System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheVolume.Forms.Main));
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnEnterLicence = new System.Windows.Forms.Button();
			this.btnBuy = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.cheCheckBox2 = new CheVolume.Controls.CheCheckBox();
			this.cheCheckBox1 = new CheVolume.Controls.CheCheckBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pnlSessMgr = new a.AudioSessionManagerPanel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblTitle.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
			this.lblTitle.Location = new System.Drawing.Point(735, 59);
			this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(291, 21);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Evaluation Version - 7 days left";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.btnEnterLicence.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.btnEnterLicence.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.btnEnterLicence.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
			this.btnEnterLicence.Location = new System.Drawing.Point(836, 96);
			this.btnEnterLicence.Margin = new System.Windows.Forms.Padding(4);
			this.btnEnterLicence.Name = "btnEnterLicence";
			this.btnEnterLicence.Size = new System.Drawing.Size(185, 42);
			this.btnEnterLicence.TabIndex = 3;
			this.btnEnterLicence.Text = "Enter License key";
			this.btnEnterLicence.UseVisualStyleBackColor = true;
			this.btnBuy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.btnBuy.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.btnBuy.Location = new System.Drawing.Point(728, 96);
			this.btnBuy.Margin = new System.Windows.Forms.Padding(4);
			this.btnBuy.Name = "btnBuy";
			this.btnBuy.Size = new System.Drawing.Size(100, 42);
			this.btnBuy.TabIndex = 2;
			this.btnBuy.Text = "Buy Now";
			this.btnBuy.UseVisualStyleBackColor = true;
			this.btnBuy.Click += new System.EventHandler(btnBuy_Click);
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Verdana", 6f);
			this.label2.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
			this.label2.Location = new System.Drawing.Point(971, 78);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 12);
			this.label2.TabIndex = 10;
			this.label2.Text = "v0.6.0.3";
			this.label2.Click += new System.EventHandler(label2_Click);
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.button1.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
			this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold);
			this.button1.Image = CheVolume.Properties.Resources.GoToWebsite;
			this.button1.Location = new System.Drawing.Point(710, 15);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 2);
			this.button1.Size = new System.Drawing.Size(45, 42);
			this.button1.TabIndex = 14;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(button1_Click);
			this.button1.MouseEnter += new System.EventHandler(button1_MouseEnter);
			this.button1.MouseLeave += new System.EventHandler(button1_MouseLeave);
			this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
			this.btnUpdate.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnUpdate.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold);
			this.btnUpdate.Image = CheVolume.Properties.Resources.CheckUpdate;
			this.btnUpdate.Location = new System.Drawing.Point(870, 15);
			this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Padding = new System.Windows.Forms.Padding(0, 0, 3, 2);
			this.btnUpdate.Size = new System.Drawing.Size(45, 42);
			this.btnUpdate.TabIndex = 9;
			this.btnUpdate.UseVisualStyleBackColor = false;
			this.button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.button3.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button3.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
			this.button3.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.button3.Image = CheVolume.Properties.Resources.options;
			this.button3.Location = new System.Drawing.Point(923, 15);
			this.button3.Margin = new System.Windows.Forms.Padding(4);
			this.button3.Name = "button3";
			this.button3.Padding = new System.Windows.Forms.Padding(0, 0, 3, 2);
			this.button3.Size = new System.Drawing.Size(45, 42);
			this.button3.TabIndex = 8;
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new System.EventHandler(button3_Click);
			this.cheCheckBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.cheCheckBox2.Appearance = System.Windows.Forms.Appearance.Button;
			this.cheCheckBox2.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.cheCheckBox2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.cheCheckBox2.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
			this.cheCheckBox2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.cheCheckBox2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.cheCheckBox2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.cheCheckBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cheCheckBox2.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold);
			this.cheCheckBox2.Image = CheVolume.Properties.Resources.Pinned;
			this.cheCheckBox2.Location = new System.Drawing.Point(763, 15);
			this.cheCheckBox2.Margin = new System.Windows.Forms.Padding(4);
			this.cheCheckBox2.Name = "cheCheckBox2";
			this.cheCheckBox2.Size = new System.Drawing.Size(45, 42);
			this.cheCheckBox2.TabIndex = 15;
			this.cheCheckBox2.UseVisualStyleBackColor = false;
			this.cheCheckBox2.CheckedChanged += new System.EventHandler(cheCheckBox2_CheckedChanged);
			this.cheCheckBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.cheCheckBox1.Appearance = System.Windows.Forms.Appearance.Button;
			this.cheCheckBox1.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.cheCheckBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.cheCheckBox1.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
			this.cheCheckBox1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.cheCheckBox1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.cheCheckBox1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.cheCheckBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cheCheckBox1.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold);
			this.cheCheckBox1.Image = CheVolume.Properties.Resources.AdvancedUser;
			this.cheCheckBox1.Location = new System.Drawing.Point(816, 15);
			this.cheCheckBox1.Margin = new System.Windows.Forms.Padding(4);
			this.cheCheckBox1.Name = "cheCheckBox1";
			this.cheCheckBox1.Size = new System.Drawing.Size(45, 42);
			this.cheCheckBox1.TabIndex = 13;
			this.cheCheckBox1.UseVisualStyleBackColor = false;
			this.cheCheckBox1.CheckedChanged += new System.EventHandler(cheCheckBox1_CheckedChanged);
			this.pictureBox1.BackgroundImage = CheVolume.Properties.Resources.logoh;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pictureBox1.Location = new System.Drawing.Point(19, 15);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(387, 113);
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			this.pnlSessMgr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.pnlSessMgr.AutoScroll = true;
			this.pnlSessMgr.AutoSize = true;
			this.pnlSessMgr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlSessMgr.Location = new System.Drawing.Point(15, 142);
			this.pnlSessMgr.Margin = new System.Windows.Forms.Padding(0, 0, 27, 0);
			this.pnlSessMgr.MinimumSize = new System.Drawing.Size(747, 625);
			this.pnlSessMgr.Name = "pnlSessMgr";
			this.pnlSessMgr.PenColor = System.Drawing.Color.Empty;
			this.pnlSessMgr.Size = new System.Drawing.Size(747, 625);
			this.pnlSessMgr.TabIndex = 2;
			this.pnlSessMgr.Thickness = 0;
			this.pnlSessMgr.WrapContents = false;
			this.pnlSessMgr.SizeChanged += new System.EventHandler(pnlSessMgr_SizeChanged);
			this.pnlSessMgr.ControlAdded += new System.Windows.Forms.ControlEventHandler(pnlSessMgr_ControlAdded);
			this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(toolTip1_Popup);
			this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			this.button2.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button2.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
			this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(225, 225, 225);
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Font = new System.Drawing.Font("Verdana", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.button2.Image = CheVolume.Properties.Resources.close;
			this.button2.Location = new System.Drawing.Point(976, 15);
			this.button2.Margin = new System.Windows.Forms.Padding(4);
			this.button2.Name = "button2";
			this.button2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 2);
			this.button2.Size = new System.Drawing.Size(45, 42);
			this.button2.TabIndex = 16;
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(button2_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(1045, 756);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.cheCheckBox2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.cheCheckBox1);
			base.Controls.Add(this.btnBuy);
			base.Controls.Add(this.btnEnterLicence);
			base.Controls.Add(this.lblTitle);
			base.Controls.Add(this.btnUpdate);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.button3);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.pnlSessMgr);
			this.DoubleBuffered = true;
			// base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Margin = new System.Windows.Forms.Padding(4);
			this.MaximumSize = new System.Drawing.Size(3994, 803);
			this.MinimumSize = new System.Drawing.Size(794, 803);
			base.Name = "Main";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "CheVolume";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(_OnFormClosing);
			base.Load += new System.EventHandler(_OnLoad);
			base.ResizeEnd += new System.EventHandler(_OnResizeEnd);
			base.MouseMove += new System.Windows.Forms.MouseEventHandler(_OnMouseMove);
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		[CompilerGenerated]
		private void SYS_Method1()
		{
			UpdateAudio();
		}
	}
}
