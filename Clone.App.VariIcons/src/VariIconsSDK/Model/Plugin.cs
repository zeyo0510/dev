using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace VariIconsSDK
{
	public interface IPlugin
	{
		void Load();
		void Unload();
		void SaveSettings();
	}
	/// <summary>
	/// Finds objects of the specified type amongst
	/// the plugin dlls
	/// </summary>
	public class PluginFinder
	{
		private static PluginFinder __instance = null;
		private List<IPlugin> _plugins;
		private PluginFinder()
		{
			_plugins = new List<IPlugin>();
		}
		public static PluginFinder Instance
		{
			get
			{
				if (__instance == null)
					__instance = new PluginFinder();
				return __instance;
			}
		}
		public void FindPlugins()
		{
			//Assembly asm;

		}
		public void LoadPlugin(IPlugin plugin)
		{
			if (plugin == null)
				throw new ArgumentNullException("plugin");
			if (_plugins.Contains(plugin))
				return;
			try
			{
				//if exception thrown, plugin is not loaded
				plugin.Load();
				_plugins.Add(plugin);
			}
			catch { }
		}
		public void UnloadAll()
		{
			foreach (IPlugin plugin in _plugins)
				try { plugin.Unload(); }
				catch { }
			_plugins.Clear();
		}
		public void SavePluginSettings()
		{
			foreach (IPlugin plugin in _plugins)
				plugin.SaveSettings();
		}
	}
}
