using JC.CS.Lib.Extensions;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class DeviceVolumeControl
{
  public void UpdateUI()
  {
    this.nameLabel.Text = this._MM_DEVICE_.FriendlyName;
    this.devicePictureBox.Image = this.ProcessIcon(this._MM_DEVICE_.IconPath);
    this.leftLedBar.Enabled = !this.EndPointVolume.Mute;
    // this.leftLedBar.Value
    this.rightLedBar.Enabled = !this.EndPointVolume.Mute;
    // this.rightLedBar.Value
    this.muteCheckBox.Checked = this.EndPointVolume.Mute;
    this.volumeLabel.Text = this.EndPointVolume.Volume.ToString();

    this.macTrackBar1.Value = this.EndPointVolume.Volume;
    this.macTrackBar1.TrackerColor = this.EndPointVolume.Mute ? Color.Gray : Color.FromArgb(255, 128, 0);
  }
  /************************************************/
  private Image ProcessIcon(string s)
  {
    Image? retValue = null;
    /************************************************/
    (string path, int resourceID) = s.ParseResourceString();
    /************************************************/
    using (Icon icon = path.GetResourceIcon(resourceID, true))
    {
      retValue = icon?.ToBitmap();
    }
    /************************************************/
    return retValue;
  }
}