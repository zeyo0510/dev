using System;
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
using Microsoft.Win32;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  public partial class VSessionVolumeControl : UserControl, IAudioSessionEvents
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

    private delegate void OnStateChanged2_SYS(AudioSessionState A_0);

    private delegate void RemoveSessionNotif_SYS();

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

    private int VolumeValue
    {
      get
      {
        return volumeMACTrackBar.Value;
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

    public VSessionVolumeControl(AudioSessionControl2 P_0, Process P_1)
    {
      audioPolicyConfigService1 = new AudioPolicyConfigService(Flow);
      InitializeComponent();
      audioSessionControl21 = P_0;
      process1 = P_1;
      process1.Refresh();
      pidLabel.Text = process1.Id.ToString();
      timer1 = new Timer();
      timer1.Tick += timer1_Tick;
      timer1.Interval = 10;
      RegisterAudioSessionNotification(this);
      base.Tag = audioSessionControl21.SessionInstanceIdentifier.ToString();
      bool1 = false;
      base.Margin = new Padding(0);
      string audioSetivceDLL = GetAudioSetivceDLL(process1);
      Icon icon = ExtractIcon(audioSetivceDLL, Application.ExecutablePath);
      iconPictureBox.Image = icon.ToBitmap();
      _DestroyIcon(icon.Handle);
      nameLabel.Text = ((process1.Id == 0) ? "System Sounds" : process1.MainWindowTitle);
      str1 = process1.ProcessName;
      if (process1.Id == 0)
      {
        btnLock.Visible = false;
        btnTransfert.Visible = false;
        BtnShowProcess.Visible = false;
        muteCheCheckBox.Location = new Point(base.Size.Width / 2 - muteCheCheckBox.Width / 2, muteCheCheckBox.Location.Y);
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

    ~VSessionVolumeControl()
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
      if (!volumeMACTrackBar.bool1)
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
        leftVLedBar.Value = value;
      }
      else
      {
        leftVLedBar.Value = 0f;
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
          nameLabel.Text = process1.MainWindowTitle;
        }
        catch (Exception)
        {
        }
      }
      num1 = 0;
    }

    private void contextMenuStrip1_Closed(object P_0, ToolStripDropDownClosedEventArgs P_1)
    {
      volumeLabel.Focus();
    }

    private void btnMute_CheckedChanged(object P_0, EventArgs P_1)
    {
      if (muteCheCheckBox.Checked)
      {
        leftVLedBar.Color = false;
        volumeMACTrackBar.TrackerColor = Color.DarkGray;
        muteCheCheckBox.Image = Resources.muteon;
        muteCheCheckBox.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
      }
      else
      {
        leftVLedBar.Color = true;
        volumeMACTrackBar.TrackerColor = Color.FromArgb(255, 128, 0);
        muteCheCheckBox.Image = Resources.mute;
        muteCheCheckBox.FlatAppearance.BorderColor = Color.DarkGray;
      }
      audioSessionControl21.SetMute(muteCheCheckBox.Checked);
      volumeLabel.Focus();
    }

    private void SetTrackBar(decimal P_0)
    {
      if (base.InvokeRequired)
      {
        Invoke(new Action<decimal>(SetTrackBar), P_0);
      }
      else
      {
        volumeMACTrackBar.Value = (int)P_0;
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
      if (volumeMACTrackBar.bool1)
      {
        volumeMACTrackBar.TrackerColor = Color.FromArgb(255, 128, 0);
        audioSessionControl21.SetMute(false);
        audioSessionControl21.SetVolume((int)P_1);
      }
      else
      {
        SetTrackBar(P_1);
      }
    }

    private void btnTransfert_Click2()
    {
      contextMenuStrip1.Items.Clear();
      mmDeviceCollection1 = Class4.mmDeviceEnumerator1.GetDefaultAudioEndpoint(EDataFlow.eRender, EDeviceState.Active);
      int count = mmDeviceCollection1.Count;
      ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
      toolStripMenuItem.Text = "Move '" + process1.ProcessName + "' to  : ";
      toolStripMenuItem.Image = iconPictureBox.Image;
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
        Invoke(new Action<bool>(SetMute), P_0);
      }
      else
      {
        muteCheCheckBox.Checked = P_0;
      }
    }

    public void SetVolumeText(string P_0)
    {
      if (base.InvokeRequired)
      {
        Invoke(new Action<string>(SetVolumeText), P_0);
      }
      else
      {
        volumeLabel.Text = P_0;
      }
    }

    public void RefreshProcess(string P_0)
    {
      if (base.InvokeRequired)
      {
        Invoke(new Action<string>(RefreshProcess), P_0);
      }
      else
      {
        process1.Refresh();
        nameLabel.Text = process1.MainWindowTitle;
      }
    }

    public void SetVolume(float P_0)
    {
      if (base.InvokeRequired)
      {
        Invoke(new Action<float>(SetVolume), P_0);
      }
      else if (!volumeMACTrackBar.bool1)
      {
        volumeMACTrackBar.Value = int.Parse(Math.Ceiling(P_0 * 100f).ToString());
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
        Invoke(new Action<bool>(SetVisible), P_0);
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
          leftVLedBar.Value = 0f;
          timer1.Start();
        }
        else
        {
          SetVisible(false);
          leftVLedBar.Value = 0f;
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
      volumeLabel.Focus();
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
        pidLabel.Visible = !pidLabel.Visible;
      }
    }

    private void btnMute_MouseEnter(object P_0, EventArgs P_1)
    {
      muteCheCheckBox.Image = Resources.muteon;
      muteCheCheckBox.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
    }

    private void btnMute_MouseLeave(object P_0, EventArgs P_1)
    {
      if (!muteCheCheckBox.Checked)
      {
        muteCheCheckBox.FlatAppearance.BorderColor = Color.DarkGray;
        muteCheCheckBox.Image = Resources.mute;
      }
    }

    private void btnTransfert_MouseEnter(object P_0, EventArgs P_1)
    {
      btnTransfert.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
    }
  }
}