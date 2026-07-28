using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using a;
using AudioCore;
using AudioCore.Interfaces;
using B;
using CheVolume.Common;
using CheVolume.Controls;
using CheVolume.Properties;
using Microsoft.Win32;
/************************************************/
namespace CheVolume.Forms;
/************************************************/
public partial class MainForm : Form
{
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
    public MainForm main1;

    public Point point1;

    internal bool Equal(FlowLayoutPanel1 P_0)
    {
      return P_0.Bounds.Contains(main1.pnlSessMgr.PointToClient(point1));
    }
  }

  public const int FIXED_161 = 161;

  public const int FIXED_2 = 2;

  public const int FIXED_5644 = 5644;

  public bool bool1 = true;

  private List<int> num_list1;

  public List<ProcessMapping> processList;

  private XMLSettings xmlSettings1;

  private MMDeviceCollection mmDeviceCollection1;

  private string str2;

  public NetNamedPipeBinding netNamedPipeBinding1;

  public EndpointAddress endpointAddress1;

  public ProcessStartInfo processStartInfo1;

  public MMNotificationClient mmNotificationClient1;

  internal static float dpiX;

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

  public MMDevice DefaultDevice
  {
    get
    {
      return Class4.mmDeviceEnumerator1.EnumerateAudioEndPoints(EDataFlow.eRender, ERole.eMultimedia);
    }
  }

  public MMDevice SelectedDevice
  {
    get
    {
      if (str2 == null)
      {
        return DefaultDevice;
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

  public MainForm()
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
    string text = DefaultDevice.ID.ToString();
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
        AudioSessionControl2 audioSessionControl21 = mMDevice.AudioSessionManager._AudioSessionEnumerator_[j];
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
          int value = int.Parse(Math.Ceiling(audioSessionControl21.Volume).ToString());
          sessionVolumeControl.MMDeviceCollection = mmDeviceCollection1;
          sessionVolumeControl.MMDevice = mMDevice;
          sessionVolumeControl.muteCheCheckBox.Checked = audioSessionControl21.Mute;
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
  
  private void InvokeStateChanged()
  {
    foreach (FlowLayoutPanel1 item in Enumerable.ToList(Enumerable.OfType<FlowLayoutPanel1>(pnlSessMgr.Controls)))
    {
      foreach (SessionVolumeControl item2 in Enumerable.ToList(Enumerable.OfType<SessionVolumeControl>(item.Controls)))
      {
        item2.OnStateChanged2(item2.SessionControl.GetState());
      }
    }
  }

  private void pnlSessMgr_SizeChanged(object P_0, EventArgs P_1)
  {
  }


  private void pnlSessMgr_ControlAdded(object P_0, ControlEventArgs P_1)
  {
  }

  private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.advancedToolStripMenuItem.Checked = !this.advancedToolStripMenuItem.Checked;

    IsAdvancedUser = this.advancedToolStripMenuItem.Checked;

    InvokeStateChanged();
    if (!bool1)
    {
      AutoAdjSessionManagerPanel();
    }
  }

  private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    Process.Start("control.exe", "mmsys.cpl,, 2");
  }

  private void exitToolStripMenuItem_Click(object sender, EventArgs e)
  {
    Environment.Exit(0);
  }
}