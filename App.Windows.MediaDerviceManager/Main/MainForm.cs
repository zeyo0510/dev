using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using a;
using App.Windows.MediaDerviceManager.Controls;
using AudioCore;
using AudioCore.Interfaces;
using B;
using CheVolume.Properties;
/************************************************/
namespace App.Windows.MediaDerviceManager.Main
{
  public partial class MainForm : Form
  {
    private MMDeviceCollection   _MMDeviceCollection1   = null;
    private MMNotificationClient _MMNotificationClient1 = null;
    /************************************************/
    private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void systemToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("control.exe", "mmsys.cpl,, 2");
    }
    /************************************************/
    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Environment.Exit(0);
    }
    /************************************************/
    private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      IsAdvancedUser = !IsAdvancedUser;
      InvokeStateChanged();
    }
    /************************************************/
    private void windowToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      // TODO: do anything...
    }
    /************************************************/
    private void pinToolStripMenuItem_Click(object sender, EventArgs e)
    {
      base.TopMost = !base.TopMost;
    }
    
    internal static float dpiX;

    public MMDevice DefaultDevice
    {
      get
      {
        MMDevice retValue = AudioManager.mmDeviceEnumerator1.EnumerateAudioEndPoints(EDataFlow.eRender, ERole.eMultimedia);
        /************************************************/
        return retValue;
      }
    }
    /************************************************/
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
      _MMDeviceCollection1 = AudioManager.mmDeviceEnumerator1.GetDefaultAudioEndpoint(EDataFlow.eRender, EDeviceState.Active);
      _MMNotificationClient1 = new MMNotificationClient();
      AudioManager.mmDeviceEnumerator1.RegisterEndpointNotificationCallback(_MMNotificationClient1);
      this.InitializeComponent();
      using (Graphics graphics = CreateGraphics())
      {
        dpiX = graphics.DpiX / 96f;
      }
      ImageHelper.SetImageSize(dpiX);
      _MMNotificationClient1.DefaultChanged += new MMNotificationClientDeviceDelegate(mmNotificationClient1_DefaultChanged);
      _MMNotificationClient1.DeviceAdded += new MMNotificationClientDeviceDelegate(mmNotificationClient1_DeviceAdded);
      _MMNotificationClient1.DeviceRemoved += new MMNotificationClientDeviceDelegate(mmNotificationClient1_DeviceRemove);
      _MMNotificationClient1.PropertyValueChanged += new MMNotificationClientPropertyValueDelegate(mmNotificationClient1_PropertyValueChanged);
      UpdateAudio();
    }

    public void UpdateAudio()
    {
      if (base.InvokeRequired)
      {
        base.Invoke((MethodInvoker)delegate
        {
          this.UpdateAudio();
        });
        /************************************************/
        return;
      }
      /************************************************/
      string text = this.DefaultDevice.ID.ToString();
      /************************************************/
      this._MMDeviceCollection1 = AudioManager.mmDeviceEnumerator1.GetDefaultAudioEndpoint(EDataFlow.eRender, EDeviceState.Active);
      /************************************************/
      int count = _MMDeviceCollection1.Count;
      for (int i = 0; i < count; i++)
      {
        MMDevice mmDevice = this._MMDeviceCollection1[i];
        string deviceID = mmDevice.ID;
        bool @default = false;
        if (deviceID == text)
        {
          @default = true;
        }
        /************************************************/
        AudioFlowLayoutPanel flowLayoutPanel = Enumerable
      . ToList(Enumerable.OfType<AudioFlowLayoutPanel>(this.audioManagerPanel1.Controls))
      . Find(_ =>
        {
          return string.Compare(_.Tag.ToString(), deviceID) == 0;
        });
        /************************************************/
        if (flowLayoutPanel == null)
        {
          flowLayoutPanel = new AudioFlowLayoutPanel(mmDevice);
          VDeviceVolumeControl deviceVolumeControl = new VDeviceVolumeControl(mmDevice);
          // flowLayoutPanel
          {
            flowLayoutPanel.Tag = mmDevice.ID;
            flowLayoutPanel.BackColor = Color.FromArgb(252, 252, 252);
          }
          // deviceVolumeControl
          {
            deviceVolumeControl.BackColor = Color.FromArgb(242, 242, 242);
            deviceVolumeControl.Tag = mmDevice.ID;
            deviceVolumeControl.SetDefault(@default);
          }
          flowLayoutPanel.Controls.Add(deviceVolumeControl);
          this.audioManagerPanel1.Controls.Add(flowLayoutPanel);
        }
        else
        {
          Enumerable.First(Enumerable.OfType<VDeviceVolumeControl>(flowLayoutPanel.Controls)).SetDefault(@default);
        }
        int num;
        try
        {
          num = mmDevice.AudioSessionManager.GetCount();
        }
        catch (Exception)
        {
          num = 0;
        }
        /************************************************/
        for (int j = 0; j < num; j++)
        {
          AudioSessionControl2 audioSessionControl21 = mmDevice.AudioSessionManager.audioSessionEnumerator1[j];
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
          /************************************************/
          List<VSessionVolumeControl> list2 = Enumerable.ToList(Enumerable.OfType<VSessionVolumeControl>(flowLayoutPanel.Controls));
          VSessionVolumeControl sessionVolumeControl = null;
          if (list2.Count > 0)
          {
            sessionVolumeControl = list2.Find(delegate(VSessionVolumeControl P_0)
            {
              return string.Compare(P_0.Tag.ToString().ToLower(), audioSessionControl21.SessionInstanceIdentifier.ToLower()) == 0;
            });
          }
          /************************************************/
          AudioSessionState audioSessionState = audioSessionControl21.State;
          if (sessionVolumeControl == null && audioSessionState != AudioSessionState.AudioSessionStateExpired)
          {
            sessionVolumeControl = new VSessionVolumeControl(audioSessionControl21, process);
            sessionVolumeControl._MMDeviceCollection1 = _MMDeviceCollection1;
            sessionVolumeControl._MMDevice1 = mmDevice;
            flowLayoutPanel.Controls.Add(sessionVolumeControl);
            if (process.Id == 0)
            {
              flowLayoutPanel.Controls.SetChildIndex(sessionVolumeControl, 1);
            }
          }
        }
      }
      /************************************************/
      InvokeStateChanged();
    }

    private void mmNotificationClient1_DefaultChanged(string P_0)
    {
      if (base.InvokeRequired)
      {
        base.BeginInvoke(new MMNotificationClientDeviceDelegate(mmNotificationClient1_DefaultChanged), P_0);
      }
      else
      {
        this.UpdateAudio();
      }
    }

    private void mmNotificationClient1_DeviceAdded(string P_0)
    {
      if (base.InvokeRequired)
      {
        base.BeginInvoke(new MMNotificationClientDeviceDelegate(mmNotificationClient1_DeviceAdded), P_0);
      }
      else
      {
        this.UpdateAudio();
      }
    }

    private void mmNotificationClient1_DeviceRemove(string P_0)
    {
      if (base.InvokeRequired)
      {
        base.BeginInvoke(new MMNotificationClientDeviceDelegate(mmNotificationClient1_DeviceRemove), P_0);
        return;
      }
      
      foreach (AudioFlowLayoutPanel item in Enumerable.ToList(Enumerable.OfType<AudioFlowLayoutPanel>(audioManagerPanel1.Controls)))
      {
        if (string.Compare(item.Tag.ToString(), P_0) == 0)
        {
          audioManagerPanel1.Controls.Remove(item);
        }
      }
    }
    
    private void mmNotificationClient1_PropertyValueChanged(string P_0, PROPERTYKEY A_1)
    {
      // TODO: do anything...
    }

    private void InvokeStateChanged()
    {
      foreach (AudioFlowLayoutPanel item in Enumerable.ToList(Enumerable.OfType<AudioFlowLayoutPanel>(audioManagerPanel1.Controls)))
      {
        foreach (VSessionVolumeControl item2 in Enumerable.ToList(Enumerable.OfType<VSessionVolumeControl>(item.Controls)))
        {
          item2.OnStateChanged2(item2._AudioSessionControl1.State);
        }
      }
    }
  }
}