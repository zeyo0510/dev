using System.Diagnostics;
using AudioCore;
/************************************************/
namespace CheVolume.Controls;
/************************************************/
partial class AudioFlowLayoutPanel
{
  public void BuildSessions()
  {
    foreach (AudioSession session in this.AudioDevice.AudioSessions)
    {
      Process process;
      try
      {
        process = Process.GetProcessById((int)session.ProcessID);
      }
      catch (Exception)
      {
        process = null;
      }
      if (process == null)
      {
        continue;
      }

      SessionVolumeControl? sessionVolumeControl = this.Controls
      . OfType<SessionVolumeControl>()
      . FirstOrDefault(x => {
          return string.Equals(x.Tag?.ToString(), session.SessionInstanceIdentifier, StringComparison.OrdinalIgnoreCase);
        });

      if (sessionVolumeControl == null && session.State != AudioSessionState.AudioSessionStateExpired)
      {
        sessionVolumeControl = new(session, process);
        {
          sessionVolumeControl.AudioDeviceCollection = this.AudioDeviceCollection;
          sessionVolumeControl.AudioDevice = this.AudioDevice;
          sessionVolumeControl.muteCheckBox.Checked = session.Mute;
          sessionVolumeControl.volumeVTrackBar.Value = session.Volume;
        }
        this.Controls.Add(sessionVolumeControl);
        if (process.Id == 0)
        {
          this.Controls.SetChildIndex(sessionVolumeControl, 1);
        }
      }
    }
  }
}