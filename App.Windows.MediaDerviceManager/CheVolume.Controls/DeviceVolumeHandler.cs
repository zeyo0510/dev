using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AudioCore;

namespace CheVolume.Controls
{
  public class DeviceVolumeHandler : UserControl
  {
    public int num1;

    public MMDeviceCollection mmDeviceCollection1;

    private IContainer components;

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyIcon")]
    private static extern bool _DestroyIcon(IntPtr P_0);

    [DllImport("Shell32", EntryPoint = "ExtractIconEx")]
    public static extern int _ExtractIconEx(string P_0, int P_1, out IntPtr P_2, out IntPtr P_3, int P_4);

    public Icon ExtractIcon(string P_0)
    {
      IntPtr handle;
      IntPtr intPtr;
      _ExtractIconEx(P_0, 0, out handle, out intPtr, 1);
      return Icon.FromHandle(handle);
    }

    public DeviceVolumeHandler()
    {
      InitializeComponent();
    }

    public void Method1()
    {
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
      base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      base.AutoScaleMode = AutoScaleMode.Font;
      base.Name = "DeviceVolumeHandler";
      base.ResumeLayout(false);
    }
  }
}