using a;
using AudioCore;
using AudioCore.Interfaces;
using B;
using CheVolume.Controls;
using CheVolume.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
/************************************************/
namespace CheVolume.Forms;
/************************************************/
public partial class MainForm : Form
{
  public bool bool1 = true;

  private AudioDeviceCollection AudioDeviceCollection;
  private MMNotificationClient _MMNotificationClient_;

  private string deviceID;

  internal static float dpiX;

  public AudioDevice DefaultDevice
  {
    get
    {
      return Audio._MMDeviceEnumerator_.EnumerateAudioEndPoints(DataFlow.Render, ERole.eMultimedia);
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
      return Audio._MMDeviceEnumerator_.GetDevice(deviceID);
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
    this.AudioDeviceCollection = Audio._MMDeviceEnumerator_.EnumAudioEndpoints(DataFlow.Render, EDeviceState.Active);
    /************************************************/
    this._MMNotificationClient_ = new();
    /************************************************/
    Audio._MMDeviceEnumerator_.RegisterEndpointNotificationCallback(_MMNotificationClient_);
    /************************************************/
    this.InitializeComponent();
    /************************************************/
    using (Graphics graphics = CreateGraphics())
    {
      dpiX = graphics.DpiX / 96f;
    }
    ImageHelper.SetImageSize(dpiX);
    /************************************************/
    _MMNotificationClient_.DefaultChanged       = (MMNotificationClientDeviceDelegate)Delegate.Combine(_MMNotificationClient_.DefaultChanged, new MMNotificationClientDeviceDelegate(_MMNotificationClient_DefaultChanged));
    _MMNotificationClient_.DeviceAdded          = (MMNotificationClientDeviceDelegate)Delegate.Combine(_MMNotificationClient_.DeviceAdded, new MMNotificationClientDeviceDelegate(_MMNotificationClient_DeviceAdded));
    _MMNotificationClient_.DeviceRemoved        = (MMNotificationClientDeviceDelegate)Delegate.Combine(_MMNotificationClient_.DeviceRemoved, new MMNotificationClientDeviceDelegate(_MMNotificationClient_DeviceRemove));
    _MMNotificationClient_.PropertyValueChanged = (MMNotificationClientPropertyValueDelegate)Delegate.Combine(_MMNotificationClient_.PropertyValueChanged, new MMNotificationClientPropertyValueDelegate(_MMNotificationClient_PropertyValueChanged));
    /************************************************/
    bool1 = false;
    UpdateAudio();
    AutoAdjSessionManagerPanel();
  }

  private void AutoAdjSessionManagerPanel()
  {
    AnchorStyles anchor = flowLayoutPanel1.Anchor;
    flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
    flowLayoutPanel1.AutoSize = true;
    flowLayoutPanel1.Size = new Size(0, 0);
    flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    base.Size = new Size(0, 0);
    base.AutoSize = true;
    base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    Size size = flowLayoutPanel1.Size;
    Size size2 = base.Size;
    flowLayoutPanel1.AutoSize = false;
    AutoSize = false;
    flowLayoutPanel1.Size = size;
    base.Size = size2;
    flowLayoutPanel1.Anchor = anchor;
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
    this.AudioDeviceCollection = Audio._MMDeviceEnumerator_.EnumAudioEndpoints(DataFlow.Render, EDeviceState.Active);
    /************************************************/
    foreach (AudioDevice device in this.AudioDeviceCollection)
    {
      bool isDefault = device.ID == this.DefaultDevice.ID;
      /************************************************/
      List<FlowLayoutPanel1> list = [.. this.flowLayoutPanel1.Controls.OfType<FlowLayoutPanel1>()];
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
        flowLayoutPanel1.Controls.Add(flowLayoutPanel);
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
        // Enumerable.First(Enumerable.OfType<DeviceVolumeControl>(flowLayoutPanel.Controls)).SetDefault(isDefault);
        flowLayoutPanel.Controls.OfType<DeviceVolumeControl>().First().SetDefault(isDefault);
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

        List<SessionVolumeControl> list2 = [.. flowLayoutPanel.Controls.OfType<SessionVolumeControl>()];
        SessionVolumeControl sessionVolumeControl = null;
        if (list2.Count > 0)
        {
          sessionVolumeControl = list2.Find(delegate(SessionVolumeControl obj)
          {
            return string.Compare(obj.Tag.ToString().ToLower(), session.SessionInstanceIdentifier.ToLower()) == 0;
          });
        }

        AudioSessionState state = session.State;
        if (sessionVolumeControl == null && state != AudioSessionState.AudioSessionStateExpired)
        {
          sessionVolumeControl = new(session, process);
          {
            sessionVolumeControl.AudioDeviceCollection = this.AudioDeviceCollection;
            sessionVolumeControl.AudioDevice = device;
            sessionVolumeControl.muteCheckBox.Checked = session.Mute;
            sessionVolumeControl.volumeVTrackBar.Value = session.Volume;
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
    foreach (FlowLayoutPanel1 item in Enumerable.ToList(Enumerable.OfType<FlowLayoutPanel1>(flowLayoutPanel1.Controls)))
    {
      if (string.Compare(item.Tag.ToString(), P_0) == 0)
      {
        flowLayoutPanel1.Controls.Remove(item);
      }
    }
  }

  private void InvokeStateChanged()
  {
    foreach (FlowLayoutPanel1 item in flowLayoutPanel1.Controls.OfType<FlowLayoutPanel1>().ToList())
    {
      foreach (SessionVolumeControl item2 in item.Controls.OfType<SessionVolumeControl>().ToList())
      {
        item2.OnStateChanged2(item2.AudioSession.State);
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