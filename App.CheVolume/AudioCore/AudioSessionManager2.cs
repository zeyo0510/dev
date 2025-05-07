using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
	public class AudioSessionManager2
	{
		public readonly AudioSessionEnumerator audioSessionEnumerator1;

		private readonly IAudioSessionManager2 _AudioSessionManager2_;

		private AudioSessionManager2 audioSessionManager21;

		private Dictionary<IAudioSessionNotificationCollection, AudioSessionNotification> audioSessionNotificationCollection1 = new Dictionary<IAudioSessionNotificationCollection, AudioSessionNotification>();

		internal AudioSessionManager2(IAudioSessionManager2 P_0)
		{
			_AudioSessionManager2_ = P_0;
			IAudioSessionEnumerator audioSessionEnumerator;
			Marshal.ThrowExceptionForHR(_AudioSessionManager2_.GetSessionEnumerator(out audioSessionEnumerator));
			audioSessionEnumerator1 = new AudioSessionEnumerator(audioSessionEnumerator);
		}

		public int GetCount()
		{
			return audioSessionEnumerator1.GetCount();
		}

		public void RegisterSessionNotification(IAudioSessionNotificationCollection P_0)
		{
			AudioSessionNotification audioSessionNotification = new AudioSessionNotification(P_0);
			audioSessionNotificationCollection1.Add(P_0, audioSessionNotification);
			_AudioSessionManager2_.RegisterSessionNotification(audioSessionNotification);
		}

		public void UnregisterSessionNotification(IAudioSessionNotificationCollection P_0)
		{
			_AudioSessionManager2_.UnregisterSessionNotification(audioSessionNotificationCollection1[P_0]);
			audioSessionNotificationCollection1.Remove(P_0);
		}

		public SimpleAudioVolume GetSimpleAudioVolume(Guid P_0)
		{
			ISimpleAudioVolume simpleAudioVolume;
			_AudioSessionManager2_.GetSimpleAudioVolume(ref P_0, 0u, out simpleAudioVolume);
			return new SimpleAudioVolume(simpleAudioVolume);
		}
	}
}
