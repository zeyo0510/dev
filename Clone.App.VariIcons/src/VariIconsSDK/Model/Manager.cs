using System;
using System.Collections.Generic;

namespace VariIconsSDK.Model
{
	/// <summary>
	/// abstract singleton manager which registers the only instance
	/// to many owners by using the nackman trick.
	/// use it by inheriting from it and typing it with your class
	/// itself.
	/// </summary>
	public class Manager<T>
		where T : Manager<T>, IDisposable, new()
	{
		#region variables
		private static T __instance = null;
		private static List<Object> __owners = new List<Object>();
		//
		private static bool _creating = false;
		#endregion
		/// <summary>
		/// registers the instance to another owner.
		/// will throw exceptions if owner is null or already
		/// having an instance!
		/// </summary>
		public static T RegisterInstance(Object owner)
		{
			if (owner == null)
				throw new ArgumentNullException("owner");
			if (__owners.Contains(owner))
				throw new ArgumentException("you already own an instance");
			if (_creating)
				throw new InvalidOperationException("currently constructor is running," +
					"this would be a deadlock");
			//create new instance if needed and increase counter
			if (__instance == null)
			{
				_creating = true;
				try { __instance = new T(); }
				catch { }
				_creating = false;
			}
			__owners.Add(owner);
			return __instance;
		}
		/// <summary>
		/// unregisters an owner from this instance.
		/// will throw an exception if never registered this owner.
		/// </summary>
		public static void UnregisterInstance(Object owner)
		{
			if (!__owners.Contains(owner))
				throw new ArgumentException("you own no instance");
			__owners.Remove(owner);
			if (__owners.Count == 0 && __instance != null)
			{
				__instance.Dispose();
				__instance = null;
			}
		}
		/// <summary>
		/// gets if an instance is currently created.
		/// check this if you are referencing registerinstance
		/// while constructing
		/// </summary>
		public static T Instance
		{
			get { return __instance; }
		}
	}
}
