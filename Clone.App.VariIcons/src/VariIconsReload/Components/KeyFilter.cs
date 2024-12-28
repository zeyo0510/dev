using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VariIconsReload.Components
{
	/// <summary>
	/// generic keyfilter class, raising an event if
	/// one of the specified keystrokes is pressed
	/// </summary>
	/// <typeparam name="T">type of the tag</typeparam>
	public class KeyFilter<T> : IMessageFilter
	{
		#region variables
		private const int WM_KEYDOWN = 0x100;
		private const int WM_KEYUP = 0x101;
		private Dictionary<Keys, T> _watchKeys;
		#endregion
		public KeyFilter()
		{
			_watchKeys = new Dictionary<Keys, T>();
		}
		//filters by injecting main loop
		public bool PreFilterMessage(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_KEYDOWN:
					if (_watchKeys.ContainsKey((Keys)m.WParam.ToInt32()))
						return RaiseKeyEvent((Keys)m.WParam.ToInt32(), true);
					break;
				case WM_KEYUP:
					if (_watchKeys.ContainsKey((Keys)m.WParam.ToInt32()))
						return RaiseKeyEvent((Keys)m.WParam.ToInt32(), false);
					break;
			}
			return false;
		}
		/// <summary>
		/// gets the collection of watchkeys and tags
		/// </summary>
		protected Dictionary<Keys, T> WatchKeys
		{
			get { return _watchKeys; }
		}
		//raises event and returns, if key should be filtered
		private bool RaiseKeyEvent(Keys key, bool down)
		{
			KeyFilterEventArgs<T> e =
				new KeyFilterEventArgs<T>(key, down, _watchKeys[key]);
			if (KeyEvent != null)
				KeyEvent(this, e);
			return e.Filter;
		}
		/// <summary>
		/// called when a watchkey is pressed
		/// </summary>
		public event EventHandler<KeyFilterEventArgs<T>> KeyEvent;
	}
	/// <summary>
	/// keyfilter filtering predefined keys shift, control, alt and space
	/// </summary>
	public class SimpleKeyFilter : KeyFilter<Keys>
	{
		public SimpleKeyFilter()
			: this(new Keys[] { Keys.Shift, Keys.Control, Keys.Alt, Keys.Space })
		{ }
		public SimpleKeyFilter(Keys[] watchKeys)
		{
			if (watchKeys == null)
				throw new ArgumentNullException("watchkeys");
			foreach (Keys key in watchKeys)
				WatchKeys.Add(key, key);
		}
	}
	/// <summary>
	/// eventargs for keyfilter
	/// </summary>
	public class KeyFilterEventArgs<T> : EventArgs
	{
		#region variables
		private Keys _keycode;
		private bool _filter, _down;
		private T _tag;
		#endregion
		public KeyFilterEventArgs(Keys keycode, bool down, T tag)
		{
			_keycode = keycode;
			_down = down;
			_tag = tag;
			_filter = false;
		}
		/// <summary>
		/// gets the keystroke that is pressed or released
		/// </summary>
		public Keys KeyCode
		{
			get { return _keycode; }
		}
		/// <summary>
		/// gets if the keystroke is pressed or released
		/// </summary>
		public bool Down
		{
			get { return _down; }
		}
		/// <summary>
		/// gets the tag associated with the keystroke
		/// </summary>
		public T Tag
		{
			get { return _tag; }
		}
		/// <summary>
		/// gets or sets wheter this key event should be further
		/// processed by subsequent controls in the application
		/// </summary>
		public bool Filter
		{
			get { return _filter; }
			set { _filter = value; }
		}
	}
}
