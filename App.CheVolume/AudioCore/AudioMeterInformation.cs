using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
	public class AudioMeterInformation
	{
		private IAudioMeterInformation _AudioMeterInformation_;

		private EndpointHardwareSupport _HardwareSupport;

		private AudioMeterInformationChannels audioMeterInformationChannels1;

		public AudioMeterInformationChannels PeakValues
		{
			get
			{
				return audioMeterInformationChannels1;
			}
		}

		public EndpointHardwareSupport HardwareSupport
		{
			get
			{
				return _HardwareSupport;
			}
		}

		public float MasterPeakValue
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(_AudioMeterInformation_.GetPeakValue(out result));
				return result;
			}
		}

		internal AudioMeterInformation(IAudioMeterInformation P_0)
		{
			_AudioMeterInformation_ = P_0;
			int hardwareSupport;
			Marshal.ThrowExceptionForHR(_AudioMeterInformation_.QueryHardwareSupport(out hardwareSupport));
			_HardwareSupport = (EndpointHardwareSupport)hardwareSupport;
			audioMeterInformationChannels1 = new AudioMeterInformationChannels(_AudioMeterInformation_);
		}
	}
}
