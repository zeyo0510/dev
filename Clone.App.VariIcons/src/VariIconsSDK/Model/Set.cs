using System;
using ControlsEx;

namespace VariIconsSDK.Model
{
	/// <summary>
	/// collection for editor factories, updating the gui
	/// </summary>
	public class Set<T> : CollectionBase<T>
	{
		public void AddRange(params T[] values)
		{
			if (values == null)
				throw new ArgumentNullException("values");
			foreach (T value in values)
			{
				OnInsert(Count, value);
				InnerList.Add(value);
			}
			RaiseChanged();
		}
		public void RemoveRange(params T[] values)
		{
			if (values == null)
				throw new ArgumentNullException("values");
			foreach (T value in values)
			{
				InnerList.Remove(value);
			}
			RaiseChanged();
		}
		protected override void OnValidate(T value)
		{
			if (value == null)
				throw new ArgumentNullException("item invalid");
		}
		protected override void OnInsert(int index, T value)
		{
			if (InnerList.Contains(value))
				throw new ArgumentException("already in");
		}
		protected override void OnSet(int index, T oldValue, T newValue)
		{
			int i = InnerList.IndexOf(newValue);
			if (i != -1 && i != index)
				throw new ArgumentException("already in");
		}
		#region changes
		protected override void OnInsertComplete(int index, T value)
		{
			RaiseChanged();
		}
		protected override void OnClearComplete()
		{
			RaiseChanged();
		}
		protected override void OnRemoveComplete(int index, T value)
		{
			RaiseChanged();
		}
		protected override void OnSetComplete(int index, T oldValue, T newValue)
		{
			RaiseChanged();
		}
		private void RaiseChanged()
		{
			if (Changed != null)
				Changed(this, EventArgs.Empty);
		}
		/// <summary>
		/// raised when the collection contents are changed
		/// </summary>
		public event EventHandler Changed;
		#endregion
	}
}
