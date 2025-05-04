using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using a;
using App.Windows.MediaDerviceManager.Controls;
using AudioCore;
using AudioCore.Interfaces;
using CheVolume.Properties;
/************************************************/
namespace B
{
  public partial class AudioFlowLayoutPanel : FlowLayoutPanel, IAudioSessionNotificationCollection
  {
    private MMDeviceCollection   _MMDeviceCollection1  = null;
    private MMDevice             _MMDevice1            = null;
    private AudioSessionManager2 _AudioSessionManager1 = null;
    
    public AudioFlowLayoutPanel(MMDevice device)
    {
      InitializeComponent();
      /************************************************/
      _MMDeviceCollection1 = AudioManager.mmDeviceEnumerator1.GetDefaultAudioEndpoint(EDataFlow.eRender, EDeviceState.Active);
      _MMDevice1 = device;
      _AudioSessionManager1 = _MMDevice1.AudioSessionManager;
      _AudioSessionManager1.RegisterSessionNotification(this);
      /************************************************/
      base.Size = new Size(0, 0);
      AutoSizeMode = AutoSizeMode.GrowAndShrink;
      AutoSize = true;
    }

    ~AudioFlowLayoutPanel()
    {
      _AudioSessionManager1.UnregisterSessionNotification(this);
    }
    
    private delegate void SYS_Invoke_FlowLayoutPanel(AudioSessionControl2 A_0);

    [CompilerGenerated]
    private sealed class SYS_Comapre_SessionVolumeControl
    {
      public AudioSessionControl2 obj1;

      internal bool Equal(VSessionVolumeControl P_0)
      {
        return string.Compare(P_0.Tag.ToString().ToLower(), obj1.SessionInstanceIdentifier.ToLower()) == 0;
      }
    }

    [DllImport("Shell32", EntryPoint = "ExtractIconEx")]
    public static extern int _ExtractIconEx(string P_0, int P_1, out IntPtr P_2, out IntPtr P_3, int P_4);

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
    private static extern bool _DestroyIcon(IntPtr P_0);

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

    [DllImport("user32.dll", EntryPoint = "SendMessage")]
    public static extern int _SendMessage(IntPtr P_0, int P_1, int P_2, int P_3);

    public int AlwaysZero(AudioSessionControl2 P_0)
    {
      return 0;
    }

    private void Add(AudioSessionControl2 ctrl)
    {
      if (base.InvokeRequired)
      {
        BeginInvoke(new SYS_Invoke_FlowLayoutPanel(Add), ctrl);
        return;
      }
      Process process = null;
      try
      {
        process = Process.GetProcessById((int)ctrl.ProcessID);
      }
      catch (Exception)
      {
        process = null;
      }
      AudioSessionState audioSessionState = ctrl.State;
      if (process == null)
      {
        return;
      }
      List<VSessionVolumeControl> list = Enumerable.ToList(Enumerable.OfType<VSessionVolumeControl>(base.Controls));
      VSessionVolumeControl sessionVolumeControl = null;
      if (list.Count > 0)
      {
        sessionVolumeControl = list.Find(delegate(VSessionVolumeControl A_P_0)
        {
          return string.Compare(A_P_0.Tag.ToString().ToLower(), ctrl.SessionInstanceIdentifier.ToLower()) == 0;
        });
      }
      if (sessionVolumeControl == null && audioSessionState != AudioSessionState.AudioSessionStateExpired)
      {
        sessionVolumeControl = new VSessionVolumeControl(ctrl, process);
        sessionVolumeControl._MMDeviceCollection1 = _MMDeviceCollection1;
        sessionVolumeControl._MMDevice1 = _MMDevice1;
        base.Controls.Add(sessionVolumeControl);
      }
    }

    private void _OnPaint(object sneder, PaintEventArgs e)
    {
      Color color = Color.FromArgb(50, 50, 50);
      int width = 2;
      ControlPaint.DrawBorder
      (
        e.Graphics,
        base.ClientRectangle,
        color, width, ButtonBorderStyle.Solid,
        color, width, ButtonBorderStyle.Solid,
        color, width, ButtonBorderStyle.Solid,
        color, width, ButtonBorderStyle.Solid
     );
    }

    private void _OnResize(object sender, EventArgs e)
    {
      base.Invalidate();
    }
  }
}