using System;
using System.Collections.Generic;

namespace VariIconsSDK.Model
{
	/// <summary>
	/// abstract history allowing undo/redo operations
	/// </summary>
	public interface IHistory
	{
		/// <summary>
		/// gets the number of possible undo steps
		/// </summary>
		int UndoSteps { get; }
		/// <summary>
		/// performs an undo action
		/// </summary>
		void Undo();
		/// <summary>
		/// gets the number of possible redo steps
		/// </summary>
		int RedoSteps { get; }
		/// <summary>
		/// performs an redo action
		/// </summary>
		void Redo();
	}
	/// <summary>
	/// parametric history, allowing simple deep copy
	/// undo/redo history. override this class to implement
	/// your own, clever copy algorithm for your documents.
	/// </summary>
	public class StateHistory<T> : IHistory, IDisposable where T : class, ICloneable, IDisposable
	{
		#region variables
		//fields
		private const int MaxUndoSteps = 40;
		//variables
		private List<T> _buffer;
		private int _undopos, _undowidth, _redowidth;
		#endregion
		public StateHistory()
		{
			//initialize history functionality
			_buffer = new List<T>();
			_undopos = -1;
			_undowidth = -1;
			_redowidth = 0;
		}
		public virtual void Dispose()
		{
			DisposeContents(0, _buffer.Count);
			_undopos = -1;
			_undowidth = -1;
			_redowidth = 0;
		}
		#region controller
		/// <summary>
		/// clever clone algorithm,
		/// creating the state to be returned and worked with
		/// NOTE: value may be NULL
		/// </summary>
		protected virtual T createOutState(T value)
		{
			return (T)value.Clone();
		}
		/// <summary>
		/// clever diff algorithm,
		/// creating the state to be stored.
		/// NOTE: throw an exception here if value is
		/// incompatible
		/// </summary>
		protected virtual T createInState(T value)
		{
			return value;
		}
		/// <summary>
		/// gets the current state (without cloning) or NULL
		/// if empty
		/// </summary>
		protected T StateInternal
		{
			get
			{
				return _undopos < 0 ? null : _buffer[_undopos];
			}
		}
		//clear contents in list
		private void DisposeContents(int pos, int count)
		{
			count = Math.Min(_buffer.Count, count);
			//dispose and clear objects in the buffer
			for (int i = 0; i < count; i++)
			{
				if (_buffer[pos] != null)
				{
					_buffer[pos].Dispose();
					_buffer[pos] = null;
				}
				if (++pos >= _buffer.Count)
					pos = 0;
			}
		}
		#endregion
		#region public
		/// <summary>
		/// disposes all queued states (except current) and
		/// clears them
		/// </summary>
		public void Clear()
		{
			DisposeContents(_undopos + 1, _buffer.Count - 1);
			_undowidth = -1;
			_redowidth = 0;
		}
		/// <summary>
		/// possible number of undo steps
		/// </summary>
		public int UndoSteps
		{
			get { return _undowidth; }
		}
		/// <summary>
		/// one step backward, no exception if impossible
		/// </summary>
		public void Undo()
		{
			if (_undowidth < 1) return;
			if (--_undopos < 0)
				_undopos = _buffer.Count - 1;
			_redowidth++;
			_undowidth--;
			//update
			RaiseChanged();
		}
		/// <summary>
		/// possible number of redo steps
		/// </summary>
		public int RedoSteps
		{
			get { return _redowidth; }
		}
		/// <summary>
		/// one step forward, no exception if impossible
		/// </summary>
		public void Redo()
		{
			if (_redowidth < 1) return;
			if (++_undopos >= _buffer.Count)
				_undopos = 0;
			_undowidth++;
			_redowidth--;
			//update
			RaiseChanged();
		}

		#endregion
		#region properties
		/// <summary>
		/// current working state. creates a copy everytime it
		/// read and pushes it into the buffer if written.
		/// NOTE: can return NULL on reading and may throw
		/// an exception if attempting to set an incompatible state
		/// </summary>
		public T State
		{
			get
			{
				return createOutState(StateInternal);
			}
			set
			{
				value = createInState(value);
				//clear all redo steps
				DisposeContents(_undopos + 1, _redowidth);
				_redowidth = 0;
				//put new state in
				if (_undowidth < MaxUndoSteps)
					_undowidth++;
				if (++_undopos >= MaxUndoSteps)
					_undopos = 0;
				if (_buffer.Count < MaxUndoSteps)
					_buffer.Insert(_undopos, value);
				else
				{
					DisposeContents(_undopos, 1);
					_buffer[_undopos] = value;
				}
				//update
				RaiseChanged();
			}
		}
		#endregion
		protected virtual void RaiseChanged()
		{
			if (Changed != null)
				Changed(this, EventArgs.Empty);
		}
		/// <summary>
		/// raised, when a the history changes
		/// </summary>
		public event EventHandler Changed;
	}
	/// <summary>
	/// parametric history, allowing deep copy undo/redo history
	/// backed with a lazy copy, so the value is not copied everytime.
	/// </summary>
	public class BufferedStateHistory<T> : StateHistory<T> where T : class, ICloneable, IDisposable
	{
		#region variables
		//lazy copies
		private T _copy = null;
		#endregion
		public override void Dispose()
		{
			base.Dispose();
			//delete lazy copy
			if (_copy != null)
				_copy.Dispose();
			_copy = null;
		}
		protected override T createOutState(T value)
		{
			//create a lazy copy
			if (_copy != null)
				return _copy;
			return _copy = base.createOutState(value);
		}
		protected override void RaiseChanged()
		{
			_copy = null;
			base.RaiseChanged();
		}
		/// <summary>
		/// commits the changes on the last copy
		/// </summary>
		public void Commit()
		{
			if (_copy != null)
				base.State = _copy;
		}
		/// <summary>
		/// gets the last lazy copy
		/// </summary>
		protected T Copy
		{
			get { return _copy; }
		}
	}
	/// <summary>
	/// state for buffered statemetahistory
	/// </summary>
	public class MetaState<T, M> : ICloneable, IDisposable
		where T : class,ICloneable, IDisposable
		where M : struct
	{
		public M Meta;
		private T _content;
		public MetaState(M meta, T content)
		{
			this.Meta = meta;
			this._content = content;
		}
		public void Dispose()
		{
			_content.Dispose();
		}
		public T Content{
			get{return _content;}
		}
		//clone content, valuetypes can be copied
		public object Clone()
		{
			return new MetaState<T, M>(Meta, (T)_content.Clone());
		}
	}
	/// <summary>
	/// parametric history, with access to meta information,
	/// for use of edit loactions
	/// </summary>
	public class BufferedStateMetaHistory<T, M> : BufferedStateHistory<MetaState<T, M>>
		where T : class,ICloneable, IDisposable
		where M : struct
	{
		/// <summary>
		/// direct access to the metadata of the previous state
		/// </summary>
		public M PreviousMeta
		{
			get
			{
				if (StateInternal == null)
					return default(M);
				return StateInternal.Meta;
			}
			set
			{
				if (StateInternal != null)
					StateInternal.Meta = value;
			}
		}
		/// <summary>
		/// meta of the history. check if history is empty first!
		/// </summary>
		public M Meta
		{
			get { return base.State.Meta; }
			set { base.State.Meta = value; }
		}
		/// <summary>
		/// content of the history. check if history is empty first!
		/// </summary>
		public new T State
		{
			get { return base.State.Content; }
			set { base.State = new MetaState<T, M>(PreviousMeta, value); }
		}
	}
}