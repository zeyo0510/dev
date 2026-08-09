using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using a;
using AudioCore;
using CheVolume.Controls;
using CheVolume.Properties;

namespace B
{
  public class FlowLayoutPanel1 : FlowLayoutPanel
  {
    public Point point1 = Point.Empty;

    private int controlOffsetX = 10;

    private int controlFixedY = 10;

    private AudioDeviceCollection mmDeviceCollection1;

    public AudioDevice AudioDevice;

    private IContainer components;

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

    public FlowLayoutPanel1(AudioDevice device)
    {
      InitializeComponent();
      /************************************************/
			mmDeviceCollection1 = Class4._MMDeviceEnumerator_.EnumAudioEndpoints(DataFlow.Render, EDeviceState.Active);
      /************************************************/
			this.AudioDevice = device;
      /************************************************/
			base.Size = new Size(0, 0);
      AutoSizeMode = AutoSizeMode.GrowAndShrink;
      AutoSize = true;
    }

    private void Add(AudioSession _SESSION_CONTROL_)
    {
      if (base.InvokeRequired)
      {
        BeginInvoke(new Action<AudioSession>(Add), _SESSION_CONTROL_);
        return;
      }
      Process process = null;
      try
      {
        process = Process.GetProcessById((int)_SESSION_CONTROL_.ProcessID);
      }
      catch (Exception)
      {
        process = null;
      }
      AudioSessionState audioSessionState = _SESSION_CONTROL_.State;
      if (process == null)
      {
        return;
      }
      List<SessionVolumeControl> list = Enumerable.ToList(Enumerable.OfType<SessionVolumeControl>(base.Controls));
      SessionVolumeControl sessionVolumeControl = null;
      if (list.Count > 0)
      {
        sessionVolumeControl = list.Find(delegate(SessionVolumeControl A_P_0)
        {
          return string.Compare(A_P_0.Tag.ToString().ToLower(), _SESSION_CONTROL_.SessionInstanceIdentifier.ToLower()) == 0;
        });
      }
      if (sessionVolumeControl == null && audioSessionState != AudioSessionState.AudioSessionStateExpired)
      {
        sessionVolumeControl = new SessionVolumeControl(_SESSION_CONTROL_, process);
        sessionVolumeControl.AudioDeviceCollection = mmDeviceCollection1;
        sessionVolumeControl.AudioDevice = this.AudioDevice;
        sessionVolumeControl.muteCheckBox.Checked = _SESSION_CONTROL_.Mute;
        sessionVolumeControl.volumeVTrackBar.Value = _SESSION_CONTROL_.Volume;
        base.Controls.Add(sessionVolumeControl);
      }
    }

    public void AdjControlPosition()
    {
      int num = 0;
      foreach (Control control in base.Controls)
      {
        num += controlOffsetX;
        control.Location = new Point(num, controlFixedY);
        num += control.Size.Width;
      }
      point1 = Point.Empty;
    }

    private void _OnPaint(object sender, PaintEventArgs e)
    {
      Color color = Color.FromArgb(50, 50, 50);
      int num = 2;
      ControlPaint.DrawBorder(e.Graphics, base.ClientRectangle, color, num, ButtonBorderStyle.Solid, color, num, ButtonBorderStyle.Solid, color, num, ButtonBorderStyle.Solid, color, num, ButtonBorderStyle.Solid);
    }

    private void _OnResize(object sender, EventArgs e)
    {
      Invalidate();
    }

    protected override void Dispose(bool P_0)
    {
      if (P_0 && components != null)
      {
        components.Dispose();
      }
      base.Dispose(P_0);
    }

    private void InitializeComponent()
    {
      base.SuspendLayout();
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.BackColor = System.Drawing.Color.Transparent;
      base.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
      base.Padding = new System.Windows.Forms.Padding(2);
      base.WrapContents = false;
      base.Paint += new System.Windows.Forms.PaintEventHandler(_OnPaint);
      base.Resize += new System.EventHandler(_OnResize);
      base.ResumeLayout(false);
    }
  }
}
