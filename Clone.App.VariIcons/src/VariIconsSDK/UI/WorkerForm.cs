using System;
using System.Windows.Forms;
using VariIconsSDK.Model;

namespace VariIconsSDK.UI
{
	/// <summary>
	/// worker form for showing up while a worker is running
	/// </summary>
	public partial class WorkerForm : Form
	{
		#region variables
		private IWorker _worker;
		#endregion
		public WorkerForm(IWorker worker)
		{
			if (worker == null)
				throw new ArgumentNullException("worker");
			InitializeComponent();
			//
			_worker = worker;
			_worker.Progress += new EventHandler<ProgressEventArgs>(worker_Progress);
			_worker.Finished += new EventHandler<FinishedEventArgs>(worker_Finished);
		}
		#region controller
		//start at showdialog
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			OnStart();
		}
		protected virtual void OnStart()
		{
			_worker.Start();
		}
		//prevent closing while worker is running or cancelling
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			if (_worker.IsRunning)
			{
				_worker.Cancel();
				e.Cancel = true;
			}
		}
		//set dialogresult to exit showdialog loop
		void worker_Finished(object sender, FinishedEventArgs e)
		{
			if (e.Error != null)
				MessageBox.Show(e.Error.ToString());
			if (e.Cancelled)
				this.DialogResult = DialogResult.Cancel;
			else
				this.DialogResult = DialogResult.OK;
		}
		//on first progress, change from marquee to progress style
		void worker_Progress(object sender, ProgressEventArgs e)
		{
			prg.Style = ProgressBarStyle.Blocks;
			prg.Maximum = e.Maximum;
			prg.Value = e.Progress;
		}
		//request cancel
		private void btnCancel_Click(object sender, EventArgs e)
		{
			btnCancel.Enabled = false;
			_worker.Cancel();
		}
		#endregion

		public string Description
		{
			get { return lblDescription.Text; }
			set { lblDescription.Text = value; }
		}
		public bool Cancellable
		{
			get { return btnCancel.Visible; }
			set { btnCancel.Visible = value; }
		}
	}
}
