using a;
using AudioCore;
using AudioCore.Interfaces;
using CheVolume.Controls;
using CheVolume.Properties;
using System.ComponentModel;
using System.Diagnostics;
/************************************************/
namespace CheVolume.Forms;
/************************************************/
public partial class MainForm : Form
{
  public bool bool1 = true;

  private AudioDeviceCollection AudioDeviceCollection;
  private MMNotificationClient _MMNotificationClient_;

  internal static float dpiX;

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
    this.AudioDeviceCollection = Audio.ActiveRenderDevices;
    /************************************************/
    this._MMNotificationClient_ = new();
    /************************************************/
    Audio.RegisterEndpointNotificationCallback(_MMNotificationClient_);
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
    this.BuildDevice();
    /************************************************/
    this.InvokeStateChanged();
  }

  private void BuildDevice()
  {
    this.AudioDeviceCollection = Audio.ActiveRenderDevices;
    /************************************************/
    foreach (AudioDevice device in this.AudioDeviceCollection)
    {
      bool isDefault = device.ID == Audio.DefaultRenderDevice.ID;
      /************************************************/
      AudioFlowLayoutPanel? audioFlowLayoutPanel = this.flowLayoutPanel1.Controls
      . OfType<AudioFlowLayoutPanel>()
      . FirstOrDefault(x => {
          return string.Equals(x.Tag?.ToString(), device.ID, StringComparison.OrdinalIgnoreCase);
        });
      /************************************************/
      if (audioFlowLayoutPanel == null)
      {
        audioFlowLayoutPanel = new AudioFlowLayoutPanel(this.AudioDeviceCollection, device);
        this.flowLayoutPanel1.Controls.Add(audioFlowLayoutPanel);
      }
      else
      {
        audioFlowLayoutPanel.Controls
        . OfType<DeviceVolumeControl>()
        . First()
        . SetDefault(isDefault);
      }
      /************************************************/
      audioFlowLayoutPanel.BuildSessions();
    }
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
    foreach (AudioFlowLayoutPanel item in Enumerable.ToList(Enumerable.OfType<AudioFlowLayoutPanel>(flowLayoutPanel1.Controls)))
    {
      if (string.Compare(item.Tag.ToString(), P_0) == 0)
      {
        flowLayoutPanel1.Controls.Remove(item);
      }
    }
  }

  private void InvokeStateChanged()
  {
    foreach (AudioFlowLayoutPanel item in flowLayoutPanel1.Controls.OfType<AudioFlowLayoutPanel>().ToList())
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