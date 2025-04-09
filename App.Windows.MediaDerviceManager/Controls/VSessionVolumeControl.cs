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
    public MMDeviceCollection                 _MMDeviceCollection1       = null;
    public MMDevice                           _MMDevice1                 = null;
    public AudioSessionControl2               _AudioSessionControl1      = null;
    private readonly AudioPolicyConfigService _AudioPolicyConfigService1 = null;
    
    
    
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

    private Timer timer1;

    public Process process1;

    public string str1;

    private int num2;

    private bool bool1;

    private readonly Enum1 enum1;

    private int num1;

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

    public VSessionVolumeControl(AudioSessionControl2 audioSessionControl, Process process)
    {
      this._AudioPolicyConfigService1 = new AudioPolicyConfigService(Flow);
      /************************************************/
      this.InitializeComponent();
      /************************************************/
      this._AudioSessionControl1 = audioSessionControl;
      /************************************************/
      process1 = process;
      process1.Refresh();
      /************************************************/
      pidLabel.Text = process1.Id.ToString();
      /************************************************/
      timer1 = new Timer();
      {
        timer1.Interval = 10;
        timer1.Tick += timer1_Tick;
        timer1.Start();
      }
      /************************************************/
      RegisterAudioSessionNotification(this);
      base.Tag = _AudioSessionControl1.SessionInstanceIdentifier.ToString();
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
        muteCheckBox.Location = new Point(base.Size.Width / 2 - muteCheckBox.Width / 2, muteCheckBox.Location.Y);
      }
      OnStateChanged2(_AudioSessionControl1.GetState());
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

    private void process1_Exited(object P_0, EventArgs P_1)
    {
      RemoveSessioNotif2();
    }

    private void RemoveSessioNotif2()
    {
      if (base.InvokeRequired)
      {
        Invoke(new Action(RemoveSessioNotif2));
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
      _AudioSessionControl1.RegisterAudioSessionNotification(P_0);
    }

    public void UnregisterAudioSessionNotification(IAudioSessionEvents P_0)
    {
      _AudioSessionControl1.UnregisterAudioSessionNotification(P_0);
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
      this.UpdateUI();
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
        source = _AudioSessionControl1.GetChannelsPeakValues();
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
      /************************************************/
      this.UpdateUI();
    }

    private void BuildContextMenu()
    {
      this.contextMenuStrip1.Items.Clear();
      /************************************************/
      this._MMDeviceCollection1 = Class4.mmDeviceEnumerator1.GetDefaultAudioEndpoint(EDataFlow.eRender, EDeviceState.Active);
      /************************************************/
      ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem();
      {
        toolStripMenuItem1.Text = "Move '" + process1.ProcessName + "' to  : ";
        toolStripMenuItem1.Image = iconPictureBox.Image;
        toolStripMenuItem1.TextImageRelation = TextImageRelation.ImageAboveText;
      }
      this.contextMenuStrip1.Items.Add(toolStripMenuItem1);
      /************************************************/
      this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
      /************************************************/
      int count = this._MMDeviceCollection1.Count;
      for (int i = 0; i < count; i++)
      {
        ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
        {
          toolStripMenuItem2.Text = _MMDeviceCollection1[i].FriendlyName ?? "";
          Icon icon = ImageHelper.Method1(_MMDeviceCollection1[i].IconPath);
          if (icon.Height > 0)
          {
            toolStripMenuItem2.Image = icon.ToBitmap();
          }
          _DestroyIcon(icon.Handle);
          toolStripMenuItem2.Tag = _MMDeviceCollection1[i].ID;
          toolStripMenuItem2.Click += toolStripMenuItem2_Click;
          toolStripMenuItem2.MouseEnter += toolStripMenuItem_MouseEnter;
          toolStripMenuItem2.MouseLeave += toolStripMenuItem_MouseLeave;
        }
        this.contextMenuStrip1.Items.Add(toolStripMenuItem2);
        this.contextMenuStrip1.Refresh();
      }
      /************************************************/
      this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
      /************************************************/
      try
      {
        if (Enumerable.Contains(InjectedProcesses, process1.ProcessName))
        {
          ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
          {
            toolStripMenuItem3.Text = "Return to Default";
            toolStripMenuItem3.Tag = base.Tag.ToString();
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            toolStripMenuItem3.MouseEnter += toolStripMenuItem_MouseEnter;
            toolStripMenuItem3.MouseLeave += toolStripMenuItem_MouseLeave;
            toolStripMenuItem3.Image = Resources.CheV;
          }
          contextMenuStrip1.Items.Add(toolStripMenuItem3);
        }
      }
      catch (Exception)
      {
      }
    }
    /************************************************/
    private void toolStripMenuItem_MouseLeave(object P_0, EventArgs P_1)
    {
      this.contextMenuStrip1.Cursor = Cursors.Arrow;
    }
    /************************************************/
    private void toolStripMenuItem_MouseEnter(object P_0, EventArgs P_1)
    {
      this.contextMenuStrip1.Cursor = Cursors.Hand;
    }
    /************************************************/
    public void toolStripMenuItem2_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem obj = (ToolStripMenuItem)sender;
      /************************************************/
      string processID = this.process1.Id.ToString();
      string text2 = obj.Tag.ToString();
      /************************************************/
      CreateRegistry(processID, text2);
      /************************************************/
      _AudioPolicyConfigService1.SetDefaultEndPoint(text2, process1.Id);
    }
    /************************************************/
    private void toolStripMenuItem3_Click(object sender, EventArgs e)
    {
      RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software", true);
      {
        registryKey = registryKey.OpenSubKey("CheVolume", true);
        registryKey = registryKey.OpenSubKey("Data", true);
        if (registryKey.GetValue(str1) != null)
        {
          registryKey.DeleteValue(str1);
        }
      }
      /************************************************/
      this._AudioPolicyConfigService1.SetDefaultEndPoint(null, process1.Id);
    }


    private void CreateRegistry(string P_0, string P_1)
    {
      RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software", true);
      registryKey.CreateSubKey("CheVolume");
      RegistryKey registryKey2 = registryKey.OpenSubKey("CheVolume", true);
      registryKey2.CreateSubKey("Data");
      registryKey2.OpenSubKey("Data", true).SetValue(str1, P_1);
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

    public void RemoveSessionNotif()
    {
      if (base.InvokeRequired)
      {
        Invoke(new Action(RemoveSessionNotif));
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
        base.Visible = P_0;
      }
    }

    public void OnStateChanged2(AudioSessionState state)
    {
      if (base.InvokeRequired)
      {
        Invoke(new Action<AudioSessionState>(OnStateChanged2), state);
        return;
      }
      /************************************************/
      switch (state)
      {
        case AudioSessionState.AudioSessionStateActive:
          SetVisible(true);
//          timer1.Start();
          break;
        case AudioSessionState.AudioSessionStateInactive:
          if (IsAdvancedUser)
          {
            SetVisible(true);
            leftVLedBar.Value = 0f;
//            timer1.Start();
          }
          else
          {
            SetVisible(false);
            leftVLedBar.Value = 0f;
//            timer1.Stop();
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
//          timer1.Stop();
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


    private void iconPictureBox_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
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
      if (e.Button == MouseButtons.Right)
      {
        BuildContextMenu();
        contextMenuStrip1.Show(this.iconPictureBox, new Point(-contextMenuStrip1.Size.Width / 2 + this.iconPictureBox.Width / 2, -contextMenuStrip1.Size.Height - 5));
      }
    }
    /************************************************/
    private void volumeVTrackBar_ValueChanged(object sender, EventArgs e)
    {
      this._AudioSessionControl1.SetVolume(this.volumeVTrackBar.Value);
    }
    /************************************************/
    private void muteCheckBox_Click(object sender, EventArgs e)
    {
      _AudioSessionControl1.SetMute(!_AudioSessionControl1.GetMute());
    }
  }
}