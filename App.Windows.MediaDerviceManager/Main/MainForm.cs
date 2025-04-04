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
using App.Windows.MediaDerviceManager;
using AudioCore;
using AudioCore.Interfaces;
using B;
using CheVolume.Common;
using CheVolume.Controls;
using CheVolume.Properties;
using Microsoft.Win32;
/************************************************/
namespace App.Windows.MediaDerviceManager.Main
{
  public partial class MainForm : Form
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
      cheCheckBox1.Checked = IsAdvancedUser;
      MMNotificationClient mMNotificationClient = mmNotificationClient1;
      mMNotificationClient.DefaultChanged = (MMNotificationClientDeviceDelegate)Delegate.Combine(mMNotificationClient.DefaultChanged, new MMNotificationClientDeviceDelegate(mmNotificationClient1_DefaultChanged));
      MMNotificationClient mMNotificationClient2 = mmNotificationClient1;
      mMNotificationClient2.DeviceAdded = (MMNotificationClientDeviceDelegate)Delegate.Combine(mMNotificationClient2.DeviceAdded, new MMNotificationClientDeviceDelegate(mmNotificationClient1_DeviceAdded));
      MMNotificationClient mMNotificationClient3 = mmNotificationClient1;
      mMNotificationClient3.DeviceRemoved = (MMNotificationClientDeviceDelegate)Delegate.Combine(mMNotificationClient3.DeviceRemoved, new MMNotificationClientDeviceDelegate(mmNotificationClient1_DeviceRemove));
      MMNotificationClient mMNotificationClient4 = mmNotificationClient1;
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
        Invoke((MethodInvoker)delegate
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

    private void systemButton_Click(object P_0, EventArgs P_1)
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

    private void exitButton_Click(object P_0, EventArgs P_1)
    {
      Environment.Exit(0);
    }

    [CompilerGenerated]
    private void SYS_Method1()
    {
      UpdateAudio();
    }
  }
}