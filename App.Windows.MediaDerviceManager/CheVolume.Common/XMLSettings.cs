using System;
using System.IO;
using System.Xml;

namespace CheVolume.Common
{
	internal class XMLSettings
	{
		private XmlDocument document1;

		private string settingsPath;

		public XMLSettings()
		{
			settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Chevolume.com\\Settings.xml";
		}

		public void Load()
		{
			try
			{
				document1 = new XmlDocument();
				document1.Load(settingsPath);
			}
			catch (Exception)
			{
			}
			if (document1.SelectSingleNode("/Settings") == null)
			{
				document1.AppendChild(document1.CreateElement("Settings", null));
			}
		}

		public void Save()
		{
			try
			{
				string directoryName = Path.GetDirectoryName(settingsPath);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				document1.Save(settingsPath);
			}
			catch (Exception)
			{
			}
		}

		public string Get(string P_0)
		{
			XmlNode xmlNode = document1.SelectSingleNode("/Settings/" + P_0);
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		public void Set(string P_0, string P_1)
		{
			XmlNode xmlNode = document1.SelectSingleNode("/Settings/" + P_0);
			if (xmlNode != null)
			{
				xmlNode.InnerText = P_1;
				return;
			}
			xmlNode = document1.CreateNode(XmlNodeType.Element, P_0, null);
			xmlNode.InnerText = P_1;
			document1.DocumentElement.SelectSingleNode("/Settings").AppendChild(xmlNode);
		}
	}
}
