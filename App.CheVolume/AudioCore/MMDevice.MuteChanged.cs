namespace AudioCore
{
  partial class MMDevice
  {
    private bool lastNute = false;
    /************************************************/
    public event EventHandler<EventArgs>? MuteChanged = null;
    /************************************************/
    protected virtual void OnMuteChanged()
    {
      this.lastNute = this.Mute;
      /************************************************/
      this.MuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}