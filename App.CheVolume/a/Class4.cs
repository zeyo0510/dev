using System;
using System.Collections.Generic;
using AudioCore;

namespace a
{
	internal static class Class4
	{
		internal static readonly List<string> list_a;

		internal static int num_a;

		internal static readonly MMDeviceEnumerator mmDeviceEnumerator1;

		private static readonly Dictionary<int, string> dictionary_a;

		private static readonly PolicyConfigClient policyConfigClient_a;

		static Class4()
		{
			list_a = new List<string>();
			dictionary_a = new Dictionary<int, string>();
			policyConfigClient_a = new PolicyConfigClient();
			mmDeviceEnumerator1 = new MMDeviceEnumerator();
		}

		internal static void Method1(EDataFlow P_0)
		{
			list_a.Clear();
			dictionary_a.Clear();
			MMDeviceCollection defaultAudioEndpoint = mmDeviceEnumerator1.EnumAudioEndpoints(P_0, EDeviceState.Active);
			string iD = mmDeviceEnumerator1.EnumerateAudioEndPoints(P_0, ERole.eMultimedia).ID;
			int count = defaultAudioEndpoint.Count;
			for (int i = 0; i < count; i++)
			{
				MMDevice mMDevice = defaultAudioEndpoint[i];
				string iD2 = mMDevice.ID;
				list_a.Add(mMDevice.FriendlyName);
				dictionary_a.Add(i, iD2);
				if (iD2 == iD)
				{
					num_a = i;
				}
			}
		}

		internal static void Method2(int P_0)
		{
			Method3(dictionary_a[P_0]);
		}

		internal static void Method3(string P_0)
		{
			try
			{
				policyConfigClient_a.SetDefaultEndpoint(P_0, ERole.eMultimedia);
			}
			catch (Exception)
			{
			}
		}
	}
}
