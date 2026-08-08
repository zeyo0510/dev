using a;
using AudioCore;
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
public partial class SessionVolumeControl : UserControl
{
  public enum Enum1
  {
    Item1,
    Item2
  }

  public Process process1;

  public string procName;

  private bool dragging;

  private readonly AudioPolicyConfigService audioPolicyConfigService1;

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

  private readonly Enum1 enum1;
  private DataFlow Flow
  {
    get
    {
      if (enum1 != 0)
      {
        return DataFlow.Capture;
      }
      return DataFlow.Render;
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
  [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
  private static extern bool _DestroyIcon(IntPtr P_0);

  [DllImport("Shell32", EntryPoint = "ExtractIconEx")]
  public static extern int _ExtractIconEx(string P_0, int P_1, out IntPtr P_2, out IntPtr P_3, int P_4);

  public SessionVolumeControl(AudioSession _SESSION_CONTROL_, Process P_1)
  {
    this.AudioSession = _SESSION_CONTROL_;
    this.AudioSession.ChannelVolumeChanged += this.AudioSession_ChannelVolumeChanged;
    this.AudioSession.GroupingParamChanged += this.AudioSession_GroupingParamChanged;
    this.AudioSession.StateChanged += this.AudioSession_StateChanged;
    this.AudioSession.SessionDisconnected += this.AudioSession_SessionDisconnected;
    this.AudioSession.IconPathChanged += this.AudioSession_IconPathChanged;
    this.AudioSession.DisplayNameChanged += this.AudioSession_DisplayNameChanged;
    this.AudioSession.VolumeChanged += this.AudioSession_VolumeChanged;
    this.AudioSession.MuteChanged += this.AudioSession_MuteChanged;
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
    base.Tag = this.AudioSession.SessionInstanceIdentifier.ToString();
    dragging = false;
    base.Margin = new Padding(0);
    string audioSetivceDLL = GetAudioSetivceDLL(process1);
    /************************************************/
    Icon icon = ExtractIcon(audioSetivceDLL, Application.ExecutablePath);
    sessionPictureBox.Image = icon.ToBitmap();
    _DestroyIcon(icon.Handle);
    /************************************************/
    nameLabel.Text = ((process1.Id == 0) ? "System Sounds" : process1.MainWindowTitle);
    procName = process1.ProcessName;
    if (process1.Id == 0)
    {
      transfertButton.Visible = false;
      showprocCheCheckBox.Visible = false;
      muteCheckBox.Location = new Point(base.Size.Width / 2 - muteCheckBox.Width / 2, muteCheckBox.Location.Y);
    }
    OnStateChanged2(AudioSession.State);
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
      BeginInvoke(new Action(RemoveSessioNotif2));
      return;
    }
    try
    {
      base.Parent.Controls.Remove(this);
      // this.SessionControl.UnregisterAudioSessionNotification(this);
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

  private void guiTimer_Tick(object sender, EventArgs e)
  {
    this.leftLedBar.Value = (int)Math.Ceiling(this.AudioSession.Meter * 15f);
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
      BeginInvoke(new Action<decimal>(SetTrackBar), newValue);
    }
    else
    {
      macTrackBar1.Value = (int)newValue;
    }
  }

  private void macTrackBar1_ValueChanged(object sender, decimal newValue)
  {
    if (!dragging)
    {
      dragging = true;
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
    if (macTrackBar1.Dragging)
    {
      macTrackBar1.TrackerColor = Color.FromArgb(255, 128, 0);
      this.Mute = false;
      this.Volume = (int)newValue;
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
    if (registryKey.GetValue(procName) != null)
    {
      registryKey.DeleteValue(procName);
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
    registryKey2.OpenSubKey("Data", true).SetValue(procName, P_1);
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





    this.MMDeviceCollection = Class4._MMDeviceEnumerator_.EnumAudioEndpoints(DataFlow.Render, EDeviceState.Active);
    /************************************************/
    foreach (AudioDevice device in this.MMDeviceCollection)
    {
      ToolStripMenuItem item = new();
      {
        item.Text = device.FriendlyName ?? "";
      }
      Icon icon = ImageHelper.Method1(device.IconPath);
      if (icon.Height > 0)
      {
        item.Image = icon.ToBitmap();
        _DestroyIcon(icon.Handle);
        item.Tag = device.ID;
        item.ForeColor = Color.FromArgb(50, 50, 50);
        item.MouseEnter += toolStripMenuItem_MouseEnter;
        item.Click += toolStripMenuItem2_Click;
        item.MouseEnter += toolStripMenuItem_MouseEnter;
        item.MouseLeave += toolStripMenuItem_MouseLeave;
      }
      this.contextMenuStrip1.Items.Add(item);
      this.contextMenuStrip1.Refresh();
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
      BeginInvoke(new Action<bool>(SetMute), newValue);
      return;
    }
    /************************************************/
    muteCheckBox.Checked = newValue;
  }

  public void SetVolumeText(string newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<string>(SetVolumeText), newValue);
      return;
    }
    /************************************************/
    volumeLabel.Text = newValue;
  }

  public void RefreshProcess(string newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<string>(RefreshProcess), newValue);
      return;
    }
    /************************************************/
    process1.Refresh();
    nameLabel.Text = process1.MainWindowTitle;
  }

  public void SetVolume(float newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<float>(SetVolume), newValue);
      return;
    }
    /************************************************/
    if (!macTrackBar1.Dragging)
    {
      macTrackBar1.Value = int.Parse(Math.Ceiling(newValue * 100f).ToString());
    }
  }

  public void RemoveSessionNotif()
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action(RemoveSessionNotif));
      return;
    }
    /************************************************/
    // this.SessionControl.UnregisterAudioSessionNotification(this);
    base.Parent.Controls.Remove(this);
  }

  private void SetVisible(bool newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<bool>(SetVisible), newValue);
      return;
    }
    /************************************************/
    base.Visible = newValue;
  }

  public void OnStateChanged2(AudioSessionState newValue)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new Action<AudioSessionState>(OnStateChanged2), newValue);
      return;
    }
    /************************************************/
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

  private string GetAudioSetivceDLL(Process p)
  {
    if (p.Id == 0) return Environment.SystemDirectory + "\\audiosrv.dll";
    /************************************************/
    try
    {
      return p.MainModule.FileName;
    }
    catch
    {
      foreach (ManagementObject item in new ManagementObjectSearcher("SELECT ExecutablePath, ProcessID FROM Win32_Process").Get())
      {
        object obj2 = item["ProcessID"];
        object obj3 = item["ExecutablePath"];
        if (obj3 != null && obj2.ToString() == p.Id.ToString())
        {
          return obj3.ToString();
        }
      }
    }
    /************************************************/
    return Application.ExecutablePath;
  }

  // object event...

  private void showprocCheCheckBox_Click(object sender, EventArgs e)
  {
    ShowProcess(this.process1);
  }

  private void muteCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.Mute = this.muteCheckBox.Checked;
    /************************************************/
    this.UpdateUI();
  }

  private void transfertButton_MouseEnter(object sender, EventArgs e)
  {
    transfertButton.FlatAppearance.BorderColor = Color.FromArgb(255, 151, 0);
  }
}