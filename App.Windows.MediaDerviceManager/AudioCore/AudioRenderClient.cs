using System;
using System.Runtime.InteropServices;
using AudioCore.Interfaces;

namespace AudioCore
{
	public class AudioRenderClient : IDisposable
	{
		private IAudioRenderClient _AudioRenderClient_;

		internal AudioRenderClient(IAudioRenderClient P_0)
		{
			_AudioRenderClient_ = P_0;
		}

		public IntPtr GetBuffer(int P_0)
		{
			IntPtr result;
			Marshal.ThrowExceptionForHR(_AudioRenderClient_.GetBuffer(P_0, out result));
			return result;
		}

		public void ReleaseBuffer(int P_0, AudioClientBufferFlags P_1)
		{
			Marshal.ThrowExceptionForHR(_AudioRenderClient_.ReleaseBuffer(P_0, P_1));
		}

		public void Dispose()
		{
			if (_AudioRenderClient_ != null)
			{
				Marshal.ReleaseComObject(_AudioRenderClient_);
				_AudioRenderClient_ = null;
				GC.SuppressFinalize(this);
			}
		}
	}
}
