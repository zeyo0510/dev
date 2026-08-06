namespace AudioCore
{
  partial class AudioDevice
  {
    private int lastVolume = 0;
    /************************************************/
    public event EventHandler<EventArgs>? VolumeChanged = null;
    /************************************************/
    protected virtual void OnVolumeChanged()
    {
      this.lastVolume = this.Volume;
      /************************************************/
      this.VolumeChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}