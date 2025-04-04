using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
	public class AudioSessionControl2
	{
		private readonly IAudioSessionControl2 _AudioSessionControl2_;

		public uint ProcessID
		{
			get
			{
				uint result;
				Marshal.ThrowExceptionForHR(_AudioSessionControl2_.GetProcessId(out result));
				return result;
			}
		}

		public string DisplayName
		{
			get
			{
				IntPtr ptr;
				Marshal.ThrowExceptionForHR(_AudioSessionControl2_.GetDisplayName(out ptr));
				string result = Marshal.PtrToStringAuto(ptr);
				Marshal.FreeCoTaskMem(ptr);
				return result;
			}
		}

		public string IconPath
		{
			get
			{
				IntPtr ptr;
				Marshal.ThrowExceptionForHR(_AudioSessionControl2_.GetIconPath(out ptr));
				string result = Marshal.PtrToStringAuto(ptr);
				Marshal.FreeCoTaskMem(ptr);
				return result;
			}
		}

		public string SessionIdentifier
		{
			get
			{
				IntPtr ptr;
				Marshal.ThrowExceptionForHR(_AudioSessionControl2_.GetSessionIdentifier(out ptr));
				string result = Marshal.PtrToStringAuto(ptr);
				Marshal.FreeCoTaskMem(ptr);
				return result;
			}
		}

		public string SessionInstanceIdentifier
		{
			get
			{
				IntPtr ptr;
				Marshal.ThrowExceptionForHR(_AudioSessionControl2_.GetSessionInstanceIdentifier(out ptr));
				string result = Marshal.PtrToStringAuto(ptr);
				Marshal.FreeCoTaskMem(ptr);
				return result;
			}
		}

		internal AudioSessionControl2(IAudioSessionControl2 P_0)
		{
			_AudioSessionControl2_ = P_0;
		}

		public void RegisterAudioSessionNotification(IAudioSessionEvents P_0)
		{
			Marshal.ThrowExceptionForHR(_AudioSessionControl2_.RegisterAudioSessionNotification(P_0));
		}

		public void UnregisterAudioSessionNotification(IAudioSessionEvents P_0)
		{
			Marshal.ThrowExceptionForHR(_AudioSessionControl2_.UnregisterAudioSessionNotification(P_0));
		}

		public AudioSessionState GetState()
		{
			AudioSessionState result;
			Marshal.ThrowExceptionForHR(_AudioSessionControl2_.GetState(out result));
			return result;
		}

		public int GetCount()
		{
			int result;
			Marshal.ThrowExceptionForHR(((IAudioMeterInformation)_AudioSessionControl2_).GetMeteringChannelCount(out result));
			return result;
		}

		public float SetVolume()
		{
			float result;
			Marshal.ThrowExceptionForHR(((ISimpleAudioVolume)_AudioSessionControl2_).GetMasterVolume(out result));
			return result;
		}

		public void SetVolume(int P_0)
		{
			ISimpleAudioVolume obj = (ISimpleAudioVolume)_AudioSessionControl2_;
			Guid empty = Guid.Empty;
			float num = (float)P_0 / 100f;
			Marshal.ThrowExceptionForHR(obj.SetMasterVolume(num, ref empty));
		}

		public void SetMute(bool P_0)
		{
			ISimpleAudioVolume obj = (ISimpleAudioVolume)_AudioSessionControl2_;
			Guid empty = Guid.Empty;
			Marshal.ThrowExceptionForHR(obj.SetMute(P_0, ref empty));
		}

		public bool GetMute()
		{
			bool result;
			Marshal.ThrowExceptionForHR(((ISimpleAudioVolume)_AudioSessionControl2_).GetMute(out result));
			return result;
		}

		public float[] GetChannelsPeakValues()
		{
			IAudioMeterInformation obj = (IAudioMeterInformation)_AudioSessionControl2_;
			float[] array = new float[GetCount()];
			GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			Marshal.ThrowExceptionForHR(obj.GetChannelsPeakValues(array.Length, gCHandle.AddrOfPinnedObject()));
			gCHandle.Free();
			return array;
		}

		public float Method1()
		{
			IAudioMeterInformation obj = (IAudioMeterInformation)_AudioSessionControl2_;
			float result;
			obj.GetPeakValue(out result);
			int num;
			obj.GetMeteringChannelCount(out num);
			return result;
		}
	}
}
