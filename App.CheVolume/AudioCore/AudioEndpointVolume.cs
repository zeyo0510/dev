using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using AudioCore.Interfaces;

namespace AudioCore
{
	public class AudioEndpointVolume : IDisposable
	{
		private readonly IAudioEndpointVolume _AudioEndPointVolume_;

		private AudioEndpointVolumeCallback _CallBack;

		public event AudioEndpointVolumeNotificationDelegate OnVolumeNotification;

		public float MasterVolumeLevelScalar
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.GetMasterVolumeLevelScalar(out result));
				return result;
			}
			set
			{
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.SetMasterVolumeLevelScalar(value, Guid.Empty));
			}
		}

		public bool Mute
		{
			get
			{
				bool result;
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.GetMute(out result));
				return result;
			}
			set
			{
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.SetMute(value, Guid.Empty));
			}
		}

		public int Volume
		{
			get
			{
				float num;
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.GetMasterVolumeLevelScalar(out num));
				num = (int)Math.Ceiling(num * 100f);
				return (int)num;
			}
			set
			{
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.SetMasterVolumeLevelScalar((float)value / 100f, Guid.Empty));
			}
		}

		internal AudioEndpointVolume(IAudioEndpointVolume P_0)
		{
			_AudioEndPointVolume_ = P_0;
			_CallBack = new AudioEndpointVolumeCallback(this);
			Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.RegisterControlChangeNotify(_CallBack));
		}

		internal void FireNotification(AudioVolumeNotificationData P_0)
		{
			if (OnVolumeNotification != null)
			{
				OnVolumeNotification(P_0);
			}
		}

		public void Dispose()
		{
			if (_CallBack != null)
			{
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.UnregisterControlChangeNotify(_CallBack));
				_CallBack = null;
			}
		}

		public float Method1()
		{
			int num;
			Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.GetChannelCount(out num));
			float num2 = 0f;
			for (uint num3 = 0u; num3 < num; num3++)
			{
				float num4;
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume_.GetChannelVolumeLevelScalar(num3, out num4));
				if (num4 > num2)
				{
					num2 = num4;
				}
			}
			return num2;
		}

		~AudioEndpointVolume()
		{
			Dispose();
		}
	}
}
