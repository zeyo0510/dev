using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.MediaDerviceManager.Controls
{
  public partial class AudioSessionManagerPanel : FlowLayoutPanel
  {
    public AudioSessionManagerPanel()
    {
      this.InitializeComponent();
    }
    /************************************************/
    [DefaultValue(3)]
    public int Thickness
    {
      get; set;
    }
    /************************************************/
    [DefaultValue("")]
    public Color PenColor
    {
      get; set;
    }
  }
}