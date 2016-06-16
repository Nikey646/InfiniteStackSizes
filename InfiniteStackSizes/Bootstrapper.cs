using System;
using System.IO;
using System.Reflection;
using Common;
using UnityEngine;

namespace InfiniteStackSizes
{
	
	public class Bootstrapper : FortressCraftMod
	{
		private Int32 _maxStack;

		private void Update()
		{
			if (!ItemEntry.mbEntriesLoaded)
				return;

			for (var i = 0; i < ItemEntry.mEntries.Length; i++)
			{
				var item = ItemEntry.mEntries[i];
				if (item == null)
					continue;
				ItemEntry.mEntriesById[item.ItemID].MaxStack = this._maxStack;
				ItemEntry.mEntriesByKey[item.Key].MaxStack = this._maxStack;
				item.MaxStack = this._maxStack;
			}

			for (var i = 0; i < TerrainData.mEntries.Length; i++)
			{
				var item = TerrainData.mEntries[i];
				if (item == null)
					continue;
				TerrainData.mEntriesByKey[item.Key].MaxStack = this._maxStack;
				item.MaxStack = this._maxStack;
			}

			Destroy(this);
		}

		private void Start()
		{
			var location = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "maxstax.ini");
			if (!File.Exists(location))
				return;
			Debug.LogError("Reading Ini File.");
			var settings = new Ini(location);
			if (!settings.ContainsSection("MaxStax"))
				return;
			settings.SetSection("MaxStax");
			if (!settings.ContainsKey("Size"))
				return;
			this._maxStack = settings.GetInteger("Size", 100);
		}
	}
}
