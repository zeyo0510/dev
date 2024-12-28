using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VariIconsSDK.Properties;

namespace VariIconsSDK.Model
{
	/// <summary>
	/// file filter for open/savefiledialog
	/// </summary>
	public class FileFilter
	{
		#region variables
		private string _description;
		private List<string> _extensions;
		#endregion
		public FileFilter(string description, string extension)
			: this(description, new string[] { extension }) { }
		public FileFilter(string description, string[] extensions)
		{
			if (description == null)
				throw new ArgumentNullException("description");
			_description = description;
			if (extensions == null || extensions.Length == 0)
				throw new ArgumentNullException("extensions");
			//check extensions
			_extensions = new List<string>();
			foreach (string extension in extensions)
			{
				if (extension == null ||
					extension.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
					throw new ArgumentException("invalid extensions");
				_extensions.Add(extension);
			}
		}
		//build filter for openfiledialog
		private static void AppendFilter(StringBuilder sb, string description, List<string> extensions)
		{
			//create displayed section
			sb.Append(description);
			sb.Append(" (");
			bool first = true;
			foreach (string extension in extensions)
			{
				if (!first) sb.Append(";");
				first = false;
				sb.Append("*.");
				sb.Append(extension);
			}
			sb.Append(")|");
			//create filter
			first = true;
			foreach (string extension in extensions)
			{
				if (!first) sb.Append(";");
				first = false;
				sb.Append("*.");
				sb.Append(extension);
			}
		}
		/// <summary>
		/// builds a filter with all the extensions of the given filters
		/// </summary>
		public static string BuildCombinedFilter(ICollection<FileFilter> filters)
		{
			if (filters == null)
				throw new ArgumentNullException("filters");
			List<string> extensions = new List<string>();
			StringBuilder sb = new StringBuilder();
			foreach (FileFilter filter in filters)
			{
				extensions.AddRange(filter._extensions);
				if (sb.Length > 0)
					sb.Append("|");
				AppendFilter(sb, filter._description, filter._extensions);
			}
			if (filters.Count > 1)
			{
				sb.Append("|");
				AppendFilter(sb, Resources.txt_filter_AllFiles, extensions);
			}
			return sb.ToString();
		}
		/// <summary>
		/// builds a filter for openfiledialogs
		/// </summary>
		public string BuildFilter()
		{
			StringBuilder sb = new StringBuilder();
			AppendFilter(sb, _description, _extensions);
			return sb.ToString();
		}
		/// <summary>
		/// same as buildfilter
		/// </summary>
		public override string ToString()
		{
			return BuildFilter();
		}
	}
}
