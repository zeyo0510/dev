using JC.CS.Lib.Extensions;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class DeviceVolumeControl
{
  public void UpdateUI()
  {
    // nameLabel
    this.nameLabel.Text = this.MMDevice.FriendlyName;
    // devicePictureBox
    this.devicePictureBox.Image = this.ProcessIcon(this.MMDevice.IconPath);
    // leftLedBar
    this.leftLedBar.Enabled = !this.Mute;
    this.leftLedBar.Value = this.ProcessLeftChannel(this.MeterInformation.PeakValues.ToFloatArray);
    // macTrackBar1
    // this.macTrackBar1.Value = this.Volume;
    this.macTrackBar1.TrackerColor = this.Mute ? Color.Gray : Color.FromArgb(255, 128, 0);
    // rightLedBar
    this.rightLedBar.Enabled = !this.Mute;
    this.rightLedBar.Value = this.ProcessRightChannel(this.MeterInformation.PeakValues.ToFloatArray);
    // muteCheckBox
    this.muteCheckBox.Checked = this.Mute;
    // volumeLabel
    this.volumeLabel.Text = this.Volume.ToString();
  }
  /************************************************/
  private Image ProcessIcon(string s)
  {
    Image? retValue = null;
    /************************************************/
    (string path, int resourceID) = s.ParseResourceString();
    /************************************************/
    using (Icon? icon = path.GetResourceIcon(resourceID, true))
    {
      retValue = icon?.ToBitmap();
    }
    /************************************************/
    return retValue;
  }
  /************************************************/
  private int ProcessLeftChannel(float[] peaks)
  {
    int retValue = 0;
    /************************************************/
    float volume = peaks
    . First();
    /****************************************************/
    retValue  = (int)Math.Ceiling(volume * this.leftLedBar.MaximumValue);
    /************************************************/
    return retValue;
  }
  /************************************************/
  private int ProcessRightChannel(float[] peaks)
  {
    int retValue = 0;
    /************************************************/
    float volume = peaks
    . Last();
    /****************************************************/
    retValue  = (int)Math.Ceiling(volume * this.leftLedBar.MaximumValue);
    /************************************************/
    return retValue;
  }
}