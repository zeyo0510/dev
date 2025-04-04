using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace MultiProcessIPC
{
	internal class CProcessHost : IDisposable
	{
		public const int BUFFER_SIZE = 2048;

		private string m_PipeID;

		private Process process1;

		private NamedPipeServerStream m_PipeServerStream;

		private bool m_IsDisposing;

		private Thread thread1;

		public bool Start(string P_0)
		{
			m_PipeID = P_0;
			thread1 = new Thread(StartIPCServer);
			thread1.Name = GetType().Name + ".PipeMessagingThread";
			thread1.IsBackground = true;
			thread1.Start();
			ProcessStartInfo startInfo = new ProcessStartInfo("CheVolumeHelper_x86", m_PipeID);
			process1 = Process.Start(startInfo);
			return true;
		}

		public bool Send(string P_0)
		{
			return true;
		}

		private void StartIPCServer()
		{
			if (m_PipeServerStream == null)
			{
				m_PipeServerStream = new NamedPipeServerStream(m_PipeID, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 2048, 2048);
			}
			Console.WriteLine(string.Format("{0}:Waiting for child process connection...", m_PipeID));
			try
			{
				m_PipeServerStream.WaitForConnection();
				Console.WriteLine(string.Format("Child process {0} is connected.", m_PipeID));
			}
			catch (ObjectDisposedException ex)
			{
				Console.WriteLine(string.Format("StartIPCServer for process {0} error: {1}", m_PipeID, ex.Message));
			}
			catch (IOException ex2)
			{
				Console.WriteLine(string.Format("StartIPCServer for process {0} error: {1}", m_PipeID, ex2.Message));
			}
			bool flag = true;
			while (flag && !m_IsDisposing)
			{
				flag = StartAsyncReceive();
				Thread.Sleep(30);
			}
		}

		private bool StartAsyncReceive()
		{
			StreamReader streamReader = new StreamReader(m_PipeServerStream);
			try
			{
				string text = streamReader.ReadLine();
				if (string.IsNullOrEmpty(text))
				{
					return false;
				}
				Console.WriteLine(string.Format("{0}: Received: {1}. (Thread {2})", m_PipeID, text, Thread.CurrentThread.ManagedThreadId));
			}
			catch (Exception ex)
			{
				Console.WriteLine("AsyncReceive ERROR: {0}", ex.Message);
				return false;
			}
			return true;
		}

		private void DisposeClientProcess()
		{
			try
			{
				m_IsDisposing = true;
				try
				{
					process1.Kill();
				}
				catch
				{
				}
				m_PipeServerStream.Dispose();
				Console.WriteLine(string.Format("Process {0} is Closed", m_PipeID));
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("Process {0} is Close error: {1}", m_PipeID, ex.Message));
			}
		}

		public void Dispose()
		{
			DisposeClientProcess();
		}
	}
}
