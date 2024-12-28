using VariIconsSDK.Model;

namespace VariIconsSDK.UI
{
	public class WorkerForm<I, O> : WorkerForm
	{
		private I _argument;
		private Worker<I, O> _worker;
		public WorkerForm(Worker<I, O> worker, I argument)
			: base(worker)
		{
			_worker = worker;
			_argument = argument;
		}
		protected override void OnStart()
		{
			_worker.Start(_argument);
		}
	}
}
