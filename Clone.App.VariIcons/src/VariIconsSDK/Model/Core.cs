using System;
using System.Collections.Generic;
using System.Text;

namespace VariIconsSDK.Model
{
	public class Core
	{
		private static Core __instance;
		private Set<IEditorTabFactory> _factories;
		private Core()
		{
			_factories = new Set<IEditorTabFactory>();
		}
		public static Core Instance
		{
			get {
				if (__instance == null)
					__instance = new Core();
				return __instance;
			}
		}
		/// <summary>
		/// gets the registered factories for creating editor tabs
		/// </summary>
		public Set<IEditorTabFactory> Factories
		{
			get { return _factories; }
		}
	}
}
