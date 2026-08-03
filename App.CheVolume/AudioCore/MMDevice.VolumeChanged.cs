namespace AudioCore
{
  partial class MMDevice
  {
    public event EventHandler<EventArgs>? NotifyChanged = null;
    /************************************************/
    protected virtual void OnNotifyChanged()
    {
      this.NotifyChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}