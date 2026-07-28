using a;
using AudioCore;
using AudioCore.Interfaces;
using AudioCore2;
using CheVolume.Properties;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using WinForm = System.Windows.Forms;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
public partial class SessionVolumeControl : UserControl, IAudioSessionEvents
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

  public Process process1;

  public string str1;

  private bool bool1;

  private readonly AudioPolicyConfigService audioPolicyConfigService1;

  private readonly Enum1 enum1;

  private int num1;

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

  public SessionVolumeControl(AudioSessionControl2 _SESSION_CONTROL_, Process P_1)
  {
    this.SessionControl = _SESSION_CONTROL_;
    /************************************************/
    audioPolicyConfigService1 = new AudioPolicyConfigService(Flow);
    /************************************************/
    this.InitializeComponent();
    /************************************************/
    process1 = P_1;
    process1.Refresh();
    pidLabel.Text = process1.Id.ToString();
    /************************************************/
    guiTimer = new WinForm::Timer();
    guiTimer.Tick += guiTimer_Tick;
    guiTimer.Interval = 10;
    /************************************************/
    RegisterAudioSessionNotification(this);
    base.Tag = this.SessionControl.SessionInstanceIdentifier.ToString();
    bool1 = false;
    base.Margin = new Padding(0);
    string audioSetivceDLL = GetAudioSetivceDLL(process1);
    /************************************************/
    Icon icon = ExtractIcon(audioSetivceDLL, Application.ExecutablePath);
    sessionPictureBox.Image = icon.ToBitmap();
    _DestroyIcon(icon.Handle);
    /************************************************/
    nameLabel.Text = ((process1.Id == 0) ? "System Sounds" : process1.MainWindowTitle);
    str1 = process1.ProcessName;
    if (process1.Id == 0)
    {
      transfertButton.Visible = false;
      showprocCheCheckBox.Visible = false;
      muteCheCheckBox.Location = new Point(base.Size.Width / 2 - muteCheCheckBox.Width / 2, muteCheCheckBox.Location.Y);
    }
    OnStateChanged2(SessionControl.State);
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

  private void process1_Exited(object sender, EventArgs e)
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

  private void _OnLoad(object sender, EventArgs e)
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
    SessionControl.RegisterAudioSessionNotification(P_0);
  }

  public void UnregisterAudioSessionNotification(IAudioSessionEvents P_0)
  {
    SessionControl.UnregisterAudioSessionNotification(P_0);
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

  private void guiTimer_Tick(object sender, EventArgs e)
  {
    float[] source = new float[1];
    try
    {
      source = SessionControl.ChannelsPeakValues;
    }
    catch (Exception)
    {
    }
    if (Enumerable.Count(source) > 0)
    {
      float value = Enumerable.Max(source);
      this.leftLedBar.Value = (int)Math.Ceiling(value * 15f);
    }
    else
    {
      this.leftLedBar.Value = 0;
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
  private void SetTrackBar(decimal newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new SYS_DECIMAL_INVOKE(SetTrackBar), newValue);
    }
    else
    {
      macTrackBar1.Value = (int)newValue;
    }
  }

  private void macTrackBar1_ValueChanged(object sender, decimal newValue)
  {
    if (!bool1)
    {
      bool1 = true;
      return;
    }
    if (newValue > 100m)
    {
      newValue = 100m;
    }
    if (newValue < 0m)
    {
      newValue = default(decimal);
    }
    if (macTrackBar1.bool1)
    {
      macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
      SessionControl.Mute = false;
      SessionControl.Volume = (int)newValue;
    }
    else
    {
      SetTrackBar(newValue);
    }
  }

  private void toolStripMenuItem_MouseLeave(object sender, EventArgs e)
  {
    contextMenuStrip1.Cursor = Cursors.Arrow;
  }

  private void toolStripMenuItem_MouseEnter(object sender, EventArgs e)
  {
    contextMenuStrip1.Cursor = Cursors.Hand;
  }

  private void toolStripMenuItem3_Click(object sender, EventArgs e)
  {
    ToolStripMenuItem toolStripMenuItem5 = (ToolStripMenuItem)sender;
    RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software", true);
    registryKey = registryKey.OpenSubKey("CheVolume", true);
    registryKey = registryKey.OpenSubKey("Data", true);
    if (registryKey.GetValue(str1) != null)
    {
      registryKey.DeleteValue(str1);
    }
    int count = MMDeviceCollection.Count;
    audioPolicyConfigService1.SetDefaultEndPoint(null, process1.Id);
  }

  public void toolStripMenuItem2_Click(object sender, EventArgs e)
  {
    ToolStripMenuItem obj = (ToolStripMenuItem)sender;
    string text = process1.Id.ToString();
    string text2 = obj.Tag.ToString();
    CreateRegistry(text, text2);
    audioPolicyConfigService1.SetDefaultEndPoint(text2, process1.Id);
  }

  private void CreateRegistry(string P_0, string P_1)
  {
    RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software", true);
    registryKey.CreateSubKey("CheVolume");
    RegistryKey registryKey2 = registryKey.OpenSubKey("CheVolume", true);
    registryKey2.CreateSubKey("Data");
    registryKey2.OpenSubKey("Data", true).SetValue(str1, P_1);
  }

  private void transfertButton_Click(object sender, EventArgs e)
  {
    this.contextMenuStrip1.Items.Clear();
    /************************************************/
    ToolStripMenuItem toolStripMenuItem = new();
    {
      toolStripMenuItem.Text = "Move '" + process1.ProcessName + "' to  : ";
      toolStripMenuItem.Image = sessionPictureBox.Image;
      toolStripMenuItem.ForeColor = Color.FromArgb(50, 50, 50);
      toolStripMenuItem.TextImageRelation = TextImageRelation.ImageAboveText;
    }
    this.contextMenuStrip1.Items.Add(toolStripMenuItem);
    /************************************************/





    this.contextMenuStrip1.Items.Add(new ToolStripSeparator());





    this.MMDeviceCollection = Class4.mmDeviceEnumerator1.EnumAudioEndpoints(EDataFlow.eRender, EDeviceState.Active);
    /************************************************/
    int count = this.MMDeviceCollection.Count;
    /************************************************/
    for (int i = 0; i < count; i++)
    {
      ToolStripMenuItem toolStripMenuItem2 = new();
      {
        toolStripMenuItem2.Text = this.MMDeviceCollection[i].FriendlyName ?? "";
      }
      Icon icon = ImageHelper.Method1(MMDeviceCollection[i].IconPath);
      if (icon.Height > 0)
      {
        toolStripMenuItem2.Image = icon.ToBitmap();
        _DestroyIcon(icon.Handle);
        toolStripMenuItem2.Tag = MMDeviceCollection[i].ID;
        toolStripMenuItem2.ForeColor = Color.FromArgb(50, 50, 50);
        toolStripMenuItem2.MouseEnter += toolStripMenuItem_MouseEnter;
        toolStripMenuItem2.Click += toolStripMenuItem2_Click;
        toolStripMenuItem2.MouseEnter += toolStripMenuItem_MouseEnter;
        toolStripMenuItem2.MouseLeave += toolStripMenuItem_MouseLeave;
      }
      contextMenuStrip1.Items.Add(toolStripMenuItem2);
      contextMenuStrip1.Refresh();
    }
    
    ToolStripSeparator toolStripSeparator = new();
    contextMenuStrip1.Items.Add(toolStripSeparator);

    try
    {
      if (Enumerable.Contains(InjectedProcesses, process1.ProcessName))
      {
        ToolStripMenuItem toolStripMenuItem3 = new();
        {
          toolStripMenuItem3.Text = "Return to Default";
          toolStripMenuItem3.Tag = base.Tag.ToString();
          toolStripMenuItem3.Click += toolStripMenuItem3_Click;
          toolStripMenuItem3.MouseEnter += toolStripMenuItem_MouseEnter;
          toolStripMenuItem3.MouseLeave += toolStripMenuItem_MouseLeave;
          toolStripMenuItem3.Image = Resources.CheV;
          toolStripMenuItem3.ForeColor = Color.FromArgb(50, 50, 50);
        }
        contextMenuStrip1.Items.Add(toolStripMenuItem3);
      }
      else
      {
        toolStripSeparator.Margin = new Padding(0, 0, 5, 5);
      }
    }
    catch (Exception)
    {
    }

    this.contextMenuStrip1.Show(transfertButton, new Point(-contextMenuStrip1.Size.Width / 2 + transfertButton.Width / 2, -contextMenuStrip1.Size.Height - 5));
  }

  public void SetMute(bool newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new SYS_BOOL_INVOKE_V2(SetMute), newValue);
    }
    else
    {
      muteCheCheckBox.Checked = newValue;
    }
  }

  public void SetVolumeText(string newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new SYS_STRING_INVOKE(SetVolumeText), newValue);
    }
    else
    {
      volumeLabel.Text = newValue;
    }
  }

  public void RefreshProcess(string newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new SYS_STRING_INVOKE(RefreshProcess), newValue);
    }
    else
    {
      process1.Refresh();
      nameLabel.Text = process1.MainWindowTitle;
    }
  }

  public void SetVolume(float newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new SYS_FLOAT_INVOKE(SetVolume), newValue);
    }
    else if (!macTrackBar1.bool1)
    {
      macTrackBar1.Value = int.Parse(Math.Ceiling(newValue * 100f).ToString());
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

  private void SetVisible(bool newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new SYS_BOOL_INVOKE(SetVisible), newValue);
    }
    else
    {
      base.Visible = newValue;
    }
  }

  public void OnStateChanged2(AudioSessionState newValue)
  {
    if (base.InvokeRequired)
    {
      Invoke(new OnStateChanged2_SYS(OnStateChanged2), newValue);
      return;
    }
    switch (newValue)
    {
    case AudioSessionState.AudioSessionStateActive:
      SetVisible(true);
      guiTimer.Start();
      break;
    case AudioSessionState.AudioSessionStateInactive:
      if (IsAdvancedUser)
      {
        SetVisible(true);
        leftLedBar.Value = 0;
        guiTimer.Start();
      }
      else
      {
        SetVisible(false);
        this.leftLedBar.Value = 0;
        guiTimer.Stop();
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
      guiTimer.Stop();
      break;
    }
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

  private void showprocCheCheckBox_MouseHover(object sender, EventArgs e)
  {
  }

  private void showprocCheCheckBox_MouseLeave(object sender, EventArgs e)
  {
    showprocCheCheckBox.Image = Resources.showwindow;
    showprocCheCheckBox.FlatAppearance.BorderColor = Color.DarkGray;
  }

  private void showprocCheCheckBox_MouseEnter(object sender, EventArgs e)
  {
    showprocCheCheckBox.Image = Resources.showwindowover;
    showprocCheCheckBox.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
  }

  private void showprocCheCheckBox_Click(object sender, EventArgs e)
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

  private void sessionPictureBox_Click(object sender, EventArgs e)
  {
    if (IsAdvancedUser)
    {
      pidLabel.Visible = !pidLabel.Visible;
    }
  }

  private void muteCheCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.SessionControl.Mute = this.muteCheCheckBox.Checked;
    /************************************************/
    this.UpdateUI();
  }

  private void transfertButton_MouseHover(object sender, EventArgs e)
  {
  }

  private void transfertButton_MouseLeave(object sender, EventArgs e)
  {
  }

  private void transfertButton_MouseEnter(object sender, EventArgs e)
  {
    transfertButton.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
  }
}