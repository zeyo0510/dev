using System;
using System.Threading;

namespace VariIconsSDK.Model
{
	/// <summary>
	/// shared functions for workers
	/// </summary>
	public class WorkerManager
	{
		public static SynchronizationContext SynchronizationContext
		{
			get
			{
				if (SynchronizationContext.Current == null)
				{
					SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
				}
				return SynchronizationContext.Current;
			}
			set
			{
				SynchronizationContext.SetSynchronizationContext(value);
			}
		}
	}
	/// <summary>
	/// interface for worker, used in monitoring
	/// objects
	/// </summary>
	public interface IWorker
	{
		void Start();
		void Cancel();
		void Join();
		bool IsRunning { get; }
		event EventHandler<ProgressEventArgs> Progress;
		event EventHandler<FinishedEventArgs> Finished;
	}
	/// <summary>
	/// worker executing an asynchronous thread processing an argument
	/// and putting out a result
	/// </summary>
	/// <typeparam name="I">type of the input</typeparam>
	/// <typeparam name="O">type of the output</typeparam>
	public abstract class Worker<I, O> : IWorker
	{
		#region types
		//used to start the thread
		private delegate void ParameterizedStart(I argument);
		//used for signalling cancel
		private class CancelRequest
		{
			public CancelRequest(bool cancel)
			{
				this.Cancel = cancel;
			}
			public bool Cancel;
		}
		#endregion
		#region variables
		//monitor side
		private SynchronizationContext _monitorcontext;
		private SendOrPostCallback _toMonitor;
		private IAsyncResult _asyncres;
		//core thread side
		private SynchronizationContext _corecontext;
		private ParameterizedStart _thread;
		private SendOrPostCallback _toCore;
		private bool _cancel;
		//private object _coresync;
		private O _result;
		private bool _cancelled;
		#endregion
		public Worker()
		{
			_monitorcontext = WorkerManager.SynchronizationContext;
			_corecontext = new SynchronizationContext();
			//
			_toMonitor = new SendOrPostCallback(HandleMonitorMessage);
			_toCore = new SendOrPostCallback(HandleCoreMessage);
			//
			_thread = new ParameterizedStart(Thread);
		}
		#region controller
		//monitor thread side
		/// <summary>
		/// posts a message to the core thread, if active. this is a non blocking call
		/// </summary>
		protected void PostToCore(object message)
		{
			_corecontext.Post(_toCore, message);
		}
		protected void SendToCore(object message)
		{
			_corecontext.Send(_toCore, message);
		}
		/// <summary>
		/// handles a message posted to the monitor thread.
		/// this is already executed in the monitor thread.
		/// override this to handle own messages.
		/// </summary>
		protected virtual void HandleMonitorMessage(object message)
		{
			ProgressEventArgs progargs;
			CompletedEventArgs<O> compargs;
			if ((progargs = message as ProgressEventArgs) != null)
			{
				if (Progress != null)
					Progress(this, progargs);
			}
			else if ((compargs = message as CompletedEventArgs<O>) != null)
			{
				//only a very small amount of time remaining
				Join();
				//
				_result = compargs.Result;
				_cancelled = compargs.Cancelled;
				//completed
				if (Completed != null)
					Completed(this, compargs);
				//non generic version
				if (Finished != null)
					Finished(this, compargs);
			}
		}
		//core thread side
		/// <summary>
		/// posts a message to the monitor thread. this is a non blocking call.
		/// </summary>
		protected void PostToMonitor(object message)
		{
			_monitorcontext.Post(_toMonitor, message);
		}
		/// <summary>
		/// handles a message posted to the core thread.
		/// this is already executed in the core thread.
		/// override this to handle own messages.
		/// </summary>
		protected virtual void HandleCoreMessage(object message)
		{
			CancelRequest msgcancel;
			if ((msgcancel = message as CancelRequest) != null)
				_cancel = msgcancel.Cancel;
		}
		//implementation of the thread
		private void Thread(I argument)
		{
			//set up messaging
			WorkerManager.SynchronizationContext = _corecontext;
			//do processing
			Exception error = null;
			O result = default(O);
			try
			{
				result = Run(argument);
			}
			catch (Exception ex)
			{
				error = ex;
			}
			//post completed
			PostToMonitor(new CompletedEventArgs<O>(result, error, _cancel));
		}
		/// <summary>
		/// override this to process. you should poll CancelPending in
		/// short intervals to make sure the thread is allowed to continue
		/// processing. you should also post progress feedbacks to the
		/// monitor thread.
		/// </summary>
		protected abstract O Run(I argument);
		/// <summary>
		/// true, if cancelling was requested by the user
		/// </summary>
		protected bool CancelPending
		{
			get { return _cancel; }
		}
		/// <summary>
		/// reports progress to user
		/// </summary>
		protected void ReportProgress(int value, int max)
		{
			value = Math.Max(0, Math.Min(max, value));
			PostToMonitor(new ProgressEventArgs(value, max));
		}
		#endregion
		#region public members
		/// <summary>
		/// starts the processing using a default argument
		/// </summary>
		public void Start()
		{
			Start(default(I));
		}
		/// <summary>
		/// starts the processing using the given argument
		/// </summary>
		public virtual void Start(I argument)
		{
			if (_asyncres != null)
				return;
			_result = default(O);
			_cancelled = false;
			SendToCore(new CancelRequest(false));
			_asyncres = _thread.BeginInvoke(argument, null, null);
		}
		/// <summary>
		/// requests a cancel, if possible
		/// </summary>
		public void Cancel()
		{
			PostToCore(new CancelRequest(true));
		}
		/// <summary>
		/// joins the thread synchronously, if active. this is a blocking call.
		/// </summary>
		public void Join()
		{
			if (_asyncres != null)
			{
				_thread.EndInvoke(_asyncres);
				_asyncres = null;
			}
		}
		/// <summary>
		/// gets if the worker is currently active
		/// </summary>
		public bool IsRunning
		{
			get { return _asyncres != null; }
		}
		/// <summary>
		/// gets the result of the last run
		/// </summary>
		public O Result
		{
			get { return _result; }
		}
		/// <summary>
		/// gets if the last run was cancelled
		/// </summary>
		public bool Cancelled
		{
			get { return _cancelled; }
		}
		#endregion
		public event EventHandler<ProgressEventArgs> Progress;
		public event EventHandler<CompletedEventArgs<O>> Completed;
		/// <summary>
		/// non generic verion of completed
		/// </summary>
		public event EventHandler<FinishedEventArgs> Finished;
	}
	/// <summary>
	/// used in finished event of worker
	/// </summary>
	public class FinishedEventArgs : EventArgs
	{

		private Exception _error;
		private Boolean _cancelled;
		public FinishedEventArgs(Exception error, Boolean cancelled)
		{
			_error = error;
			_cancelled = cancelled;
		}
		public Exception Error
		{
			get { return _error; }
		}
		public Boolean Cancelled
		{
			get { return _cancelled; }
		}
	}
	/// <summary>
	/// used in oncomplete event of worker
	/// </summary>
	public class CompletedEventArgs<O> : FinishedEventArgs
	{
		private O _result;
		public CompletedEventArgs(O result, Exception error, Boolean cancelled)
			: base(error, cancelled)
		{
			_result = result;
		}
		public O Result
		{
			get { return _result; }
		}
	}
	/// <summary>
	/// used in progress event of worker
	/// </summary>
	public class ProgressEventArgs : EventArgs
	{
		private int _progress, _max;
		public ProgressEventArgs(int progress, int max)
		{
			_progress = progress;
			_max = max;
		}
		public int Progress
		{
			get { return _progress; }
		}
		public int Maximum
		{
			get { return _max; }
		}
	}
}
