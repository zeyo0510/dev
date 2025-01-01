using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Threading;
using MDC.RegViewer.Comparers;
using Microsoft.Win32;

namespace MDC.RegViewer.Registry
{
	internal class RegSearcher
	{
		private BackgroundWorker worker;

		private RegSearchArgs searchArgs;

		private List<RegSearchMatch> matches;

		private Queue<string> pendingKeys;

		private Comparer comparer;

		public bool IsBusy
		{
			get
			{
				return worker.IsBusy;
			}
		}

		public event EventHandler<SearchCompleteEventArgs> SearchComplete;

		public event EventHandler<MatchFoundEventArgs> MatchFound;

		public RegSearcher()
		{
			worker = new BackgroundWorker
			{
				WorkerSupportsCancellation = true,
				WorkerReportsProgress = true
			};
			worker.DoWork += worker_DoWork;
			worker.RunWorkerCompleted += worker_RunWorkerCompleted;
			worker.ProgressChanged += worker_ProgressChanged;
		}

		public void Start(RegSearchArgs args)
		{
			searchArgs = args;
			if (args.UseRegEx)
			{
				comparer = new RegexComparer(args.Pattern, !args.MatchCase);
			}
			else
			{
				comparer = new MDC.RegViewer.Comparers.StringComparer(args.Pattern, !args.MatchCase);
			}
			matches = new List<RegSearchMatch>();
			worker.RunWorkerAsync();
		}

		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.MatchFound(this, new MatchFoundEventArgs((RegSearchMatch)e.UserState));
		}

		public void Stop()
		{
			if (worker.IsBusy)
			{
				lock (worker)
				{
					worker.CancelAsync();
					Monitor.Wait(worker);
				}
			}
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.SearchComplete(this, new SearchCompleteEventArgs(matches));
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			RegistryKey[] rootKeys = searchArgs.RootKeys;
			foreach (RegistryKey rootKey in rootKeys)
			{
				Search(rootKey);
			}
		}

		private void Search(RegistryKey rootKey)
		{
			string keyName = rootKey.Name.Substring(rootKey.Name.LastIndexOf('\\') + 1);
			ProcessKey(rootKey, keyName);
			RegistryKey registryKey = null;
			int startIndex = rootKey.Name.Length + 1;
			pendingKeys = new Queue<string>(rootKey.GetSubKeyNames());
			while (pendingKeys.Count > 0)
			{
				if (worker.CancellationPending)
				{
					lock (worker)
					{
						Monitor.Pulse(worker);
						break;
					}
				}
				string text = pendingKeys.Dequeue();
				try
				{
					registryKey = rootKey.OpenSubKey(text);
				}
				catch (SecurityException)
				{
					registryKey = null;
				}
				if (registryKey != null)
				{
					ProcessKey(registryKey, text);
					string parentPath = registryKey.Name.Substring(startIndex) + '\\';
					EnqueueSubKeys(registryKey, parentPath);
				}
			}
		}

		private void EnqueueSubKeys(RegistryKey key, string parentPath)
		{
			string[] subKeyNames = key.GetSubKeyNames();
			foreach (string text in subKeyNames)
			{
				pendingKeys.Enqueue(parentPath + text);
			}
		}

		private void ProcessKey(RegistryKey key, string keyName)
		{
			if (searchArgs.LookAtKeys)
			{
				MatchKey(key, keyName);
			}
			if (!searchArgs.LookAtValuesOrData)
			{
				return;
			}
			string[] valueNames = key.GetValueNames();
			foreach (string valueName in valueNames)
			{
				if (worker.CancellationPending)
				{
					break;
				}
				if (searchArgs.LookAtValues)
				{
					MatchValue(key, valueName);
				}
				if (searchArgs.LookAtData)
				{
					MatchData(key, valueName);
				}
			}
		}

		private void MatchData(RegistryKey key, string valueName)
		{
			string text = RegValue.ToString(key.GetValue(valueName, string.Empty));
			if (comparer.IsMatch(text))
			{
				AddMatch(key.Name, valueName, text);
			}
		}

		private void MatchValue(RegistryKey key, string valueName)
		{
			if (comparer.IsMatch(valueName))
			{
				AddMatch(key.Name, valueName, "-");
			}
		}

		private void MatchKey(RegistryKey key, string keyName)
		{
			if (comparer.IsMatch(keyName))
			{
				AddMatch(key.Name, "-", "-");
			}
		}

		private void AddMatch(string key, string value, string data)
		{
			RegSearchMatch regSearchMatch = new RegSearchMatch(key, value, data);
			if (this.MatchFound != null)
			{
				worker.ReportProgress(0, regSearchMatch);
			}
			else if (this.SearchComplete != null)
			{
				matches.Add(regSearchMatch);
			}
		}
	}
}
