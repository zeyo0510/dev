using JC.CS.Lib.Extensions;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class DeviceVolumeControl
{
  public void UpdateUI()
  {
    // nameLabel
    // this.nameLabel.Text = this.MMDevice.FriendlyName;
    // devicePictureBox
    // this.devicePictureBox.Image = this.ProcessIcon(this.MMDevice.IconPath);
    // leftLedBar
    this.leftLedBar.Enabled = !this.Mute;
    this.leftLedBar.Value = this.ProcessLeftChannel(this.AudioDevice.ChannelMeters);
    // volumeVTrackBar
    this.volumeVTrackBar.Value = this.Volume;
    // rightLedBar
    this.rightLedBar.Enabled = !this.Mute;
    this.rightLedBar.Value = this.ProcessRightChannel(this.AudioDevice.ChannelMeters);
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