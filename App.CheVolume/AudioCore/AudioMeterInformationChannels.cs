using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
	public class AudioMeterInformationChannels
	{
		private IAudioMeterInformation _AudioMeterInformation_;

		public int Count
		{
			get
			{
				int result;
				Marshal.ThrowExceptionForHR(_AudioMeterInformation_.GetMeteringChannelCount(out result));
				return result;
			}
		}

		public float[] ToFloatArray
		{
			get
			{
				float[] array = new float[Count];
				GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				Marshal.ThrowExceptionForHR(_AudioMeterInformation_.GetChannelsPeakValues(array.Length, gCHandle.AddrOfPinnedObject()));
				gCHandle.Free();
				return array;
			}
		}

		public float this[int P_0]
		{
			get
			{
				float[] array = new float[Count];
				GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				Marshal.ThrowExceptionForHR(_AudioMeterInformation_.GetChannelsPeakValues(array.Length, gCHandle.AddrOfPinnedObject()));
				gCHandle.Free();
				return array[P_0];
			}
		}

		internal AudioMeterInformationChannels(IAudioMeterInformation P_0)
		{
			_AudioMeterInformation_ = P_0;
		}
	}
}
