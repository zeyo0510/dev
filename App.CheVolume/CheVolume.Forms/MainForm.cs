using a;
using AudioCore;
using AudioCore.Interfaces;
using B;
using CheVolume.Controls;
using CheVolume.Properties;
using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
/************************************************/
namespace CheVolume.Forms;
/************************************************/
public partial class MainForm : Form
{
  public struct ProcessMapping
  {
    public Process proc1;

    public string str1;
  }

  public bool bool1 = true;

  private List<int> num_list1;

  public List<ProcessMapping> processList;

  private AudioDeviceCollection _MMDeviceCollection_;
  private MMNotificationClient _MMNotificationClient_;

  private string deviceID;

  public NetNamedPipeBinding netNamedPipeBinding1;

  public EndpointAddress endpointAddress1;

  public ProcessStartInfo processStartInfo1;


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

  public AudioDevice DefaultDevice
  {
    get
    {
      return Class4._MMDeviceEnumerator_.EnumerateAudioEndPoints(DataFlow.Render, ERole.eMultimedia);
    }
  }

  public AudioDevice SelectedDevice
  {
    get
    {
      if (deviceID == null)
      {
        return DefaultDevice;
      }
      return Class4._MMDeviceEnumerator_.GetDevice(deviceID);
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
    this._MMDeviceCollection_ = Class4._MMDeviceEnumerator_.EnumAudioEndpoints(DataFlow.Render, EDeviceState.Active);
    /************************************************/
    this._MMNotificationClient_ = new();
    /************************************************/
    Class4._MMDeviceEnumerator_.RegisterEndpointNotificationCallback(_MMNotificationClient_);
    /************************************************/
    processList = [];
    /************************************************/
    this.InitializeComponent();
    /************************************************/
    using (Graphics graphics = CreateGraphics())
    {
      dpiX = graphics.DpiX / 96f;
    }
    ImageHelper.SetImageSize(dpiX);
    /************************************************/
    _MMNotificationClient_.DefaultChanged = (MMNotificationClientDeviceDelegate)Delegate.Combine(_MMNotificationClient_.DefaultChanged, new MMNotificationClientDeviceDelegate(_MMNotificationClient_DefaultChanged));
    _MMNotificationClient_.DeviceAdded = (MMNotificationClientDeviceDelegate)Delegate.Combine(_MMNotificationClient_.DeviceAdded, new MMNotificationClientDeviceDelegate(_MMNotificationClient_DeviceAdded));
    _MMNotificationClient_.DeviceRemoved = (MMNotificationClientDeviceDelegate)Delegate.Combine(_MMNotificationClient_.DeviceRemoved, new MMNotificationClientDeviceDelegate(_MMNotificationClient_DeviceRemove));
    _MMNotificationClient_.PropertyValueChanged = (MMNotificationClientPropertyValueDelegate)Delegate.Combine(_MMNotificationClient_.PropertyValueChanged, new MMNotificationClientPropertyValueDelegate(_MMNotificationClient_PropertyValueChanged));
    /************************************************/
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
      Invoke(UpdateAudio);
      return;
    }
    /************************************************/
    string defaultDeviceID = this.DefaultDevice.ID;
    /************************************************/
    this._MMDeviceCollection_ = Class4._MMDeviceEnumerator_.EnumAudioEndpoints(DataFlow.Render, EDeviceState.Active);
    /************************************************/
    foreach (AudioDevice device in this._MMDeviceCollection_)
    {
      bool isDefault = device.ID == this.DefaultDevice.ID;
      /************************************************/
      List<FlowLayoutPanel1> list = Enumerable.ToList(Enumerable.OfType<FlowLayoutPanel1>(pnlSessMgr.Controls));
      FlowLayoutPanel1 flowLayoutPanel = list.Find(delegate(FlowLayoutPanel1 P_0)
      {
        return string.Compare(P_0.Tag.ToString(), device.ID) == 0;
      });
      /************************************************/
      if (flowLayoutPanel == null)
      {
        flowLayoutPanel = new FlowLayoutPanel1(device);
        {
          flowLayoutPanel.Tag = device.ID;
          flowLayoutPanel.BackColor = Color.FromArgb(252, 252, 252);
        }
        pnlSessMgr.Controls.Add(flowLayoutPanel);
        /************************************************/
        DeviceVolumeControl deviceVolumeControl = new(device);
        {
          deviceVolumeControl.BackColor = Color.FromArgb(242, 242, 242);
          deviceVolumeControl.Tag = device.ID;
          deviceVolumeControl.SetDefault(isDefault);
        }
        flowLayoutPanel.Controls.Add(deviceVolumeControl);
      }
      else
      {
        Enumerable.First(Enumerable.OfType<DeviceVolumeControl>(flowLayoutPanel.Controls)).SetDefault(isDefault);
      }

      
      foreach (AudioSession session in device.AudioSessions)
      {
        Process process;
        try
        {
          process = Process.GetProcessById((int)session.ProcessID);
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
            return string.Compare(P_0.Tag.ToString().ToLower(), session.SessionInstanceIdentifier.ToLower()) == 0;
          });
        }

        AudioSessionState state = session.State;
        if (sessionVolumeControl == null && state != AudioSessionState.AudioSessionStateExpired)
        {
          sessionVolumeControl = new(session, process);
          {
            sessionVolumeControl.MMDeviceCollection = _MMDeviceCollection_;
            sessionVolumeControl.AudioDevice = device;
            sessionVolumeControl.muteCheckBox.Checked = session.Mute;
            sessionVolumeControl.macTrackBar1.Value = session.Volume;
          }
          flowLayoutPanel.Controls.Add(sessionVolumeControl);
          if (process.Id == 0)
          {
            flowLayoutPanel.Controls.SetChildIndex(sessionVolumeControl, 1);
          }
        }
      }
    }
    /************************************************/
    this.InvokeStateChanged();
  }

  private void _MMNotificationClient_PropertyValueChanged(string P_0, PROPERTYKEY P_1)
  {
  }

  private void _MMNotificationClient_DefaultChanged(string P_0)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new MMNotificationClientDeviceDelegate(_MMNotificationClient_DefaultChanged), P_0);
      return;
    }
    /************************************************/
    UpdateAudio();
  }

  private void _MMNotificationClient_DeviceAdded(string P_0)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new MMNotificationClientDeviceDelegate(_MMNotificationClient_DeviceAdded), P_0);
      return;
    }
    /************************************************/
    UpdateAudio();
  }

  private void _MMNotificationClient_DeviceRemove(string P_0)
  {
    if (base.InvokeRequired)
    {
      BeginInvoke(new MMNotificationClientDeviceDelegate(_MMNotificationClient_DeviceRemove), P_0);
      return;
    }
    /************************************************/
    foreach (FlowLayoutPanel1 item in Enumerable.ToList(Enumerable.OfType<FlowLayoutPanel1>(pnlSessMgr.Controls)))
    {
      if (string.Compare(item.Tag.ToString(), P_0) == 0)
      {
        pnlSessMgr.Controls.Remove(item);
      }
    }
  }

  private void InvokeStateChanged()
  {
    foreach (FlowLayoutPanel1 item in Enumerable.ToList(Enumerable.OfType<FlowLayoutPanel1>(pnlSessMgr.Controls)))
    {
      foreach (SessionVolumeControl item2 in Enumerable.ToList(Enumerable.OfType<SessionVolumeControl>(item.Controls)))
      {
        item2.OnStateChanged2(item2.SessionControl.State);
      }
    }
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