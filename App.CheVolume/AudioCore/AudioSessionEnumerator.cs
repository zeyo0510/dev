using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
	public class AudioSessionEnumerator
	{
		private readonly IAudioSessionEnumerator _AudioSessionEnumerator_;

		public AudioSessionControl2 this[int P_0]
		{
			get
			{
				IAudioSessionControl2 audioSessionControl;
				Marshal.ThrowExceptionForHR(_AudioSessionEnumerator_.GetSession(P_0, out audioSessionControl));
				return new AudioSessionControl2(audioSessionControl);
			}
		}

		internal AudioSessionEnumerator(IAudioSessionEnumerator P_0)
		{
			_AudioSessionEnumerator_ = P_0;
		}

		public int GetCount()
		{
			int result;
			Marshal.ThrowExceptionForHR(_AudioSessionEnumerator_.GetCount(out result));
			return result;
		}
	}
}
