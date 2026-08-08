namespace AudioCore
{
  partial class AudioDevice
  {
    private bool lastMute = false;
    /************************************************/
    public event EventHandler<EventArgs>? MuteChanged = null;
    /************************************************/
    protected virtual void OnMuteChanged()
    {
      this.lastMute = this.Mute;
      /************************************************/
      this.MuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}