using System.Collections.Generic;
using UnityEngine;

namespace ID2.ItemChanger;

/// <summary>
/// Class that handles replacing items at specified locations.
/// </summary>
public class Replacer
{
	private static readonly Replacer instance = new();
	/// <summary>
	/// All the current placements.
	/// </summary>
	private static readonly Dictionary<Location, ICItem> placements = new();

	public static Replacer Instance => instance;

	private Replacer() { }

	/// <summary>
	/// Places the item with name <paramref name="itemName"/> at the location with name <paramref name="locationName"/>.
	/// </summary>
	/// <param name="locationName">The name of the <see cref="Location"/> to place the item at. If the location is not predefined, it will not place any item here.</param>
	/// <param name="itemName">The name of the <see cref="ICItem"/> to place at this location. If the item is not found, it will place an <see cref="UndefinedItem"/>.</param>
	/// <param name="onLocationCheckedCallback">The callback to run when the location is checked.</param>
	public static void PlaceItem(string locationName, string itemName, Location.LocationFunc onLocationCheckedCallback)
	{
		if (!Predefined.TryGetLocation(locationName, out Location location))
		{
			Logger.LogWarning($"No predefined location found for {locationName}, so there's nothing to replace!");
			return;
		}

		if (!Predefined.TryGetItem(itemName, out ICItem item))
		{
			Logger.LogWarning($"No predefined item found for {itemName}, so {locationName} will not be replaced.");
			return;
		}

		location.OnChecked = onLocationCheckedCallback;
		Instance.PlaceItem(item, location);
	}

	/// <summary>
	/// Has the given <paramref name="location"/> been checked?
	/// </summary>
	/// <param name="location">The <see cref="Location"/> in question.</param>
	public bool LocationChecked(Location location)
	{
		if (location.HasChecked)
		{
			return false;
		}

		if (!TryGetItemAtLocation(location, out ICItem item))
		{
			Logger.LogWarning($"No replacement was found for {location.Name}!");
			return false;
		}

		item.Trigger();
		location.Checked(item);
		return true;
	}

	/// <summary>
	/// Returns a save file flag to act as the unique identifier for a location.<br/>
	/// It uses the global X and Z <paramref name="position"/> to create what should be a unique flag.<br/>
	/// The format is "{<paramref name="flagPrefix"/>}-{global X position}-{global Z position}".
	/// </summary>
	/// <param name="flagPrefix">The prefix for the flag. This should be the name of the flag.</param>
	/// <param name="position">The position of the object the flag will be associated with.</param>
	public string GetFlagFromPosition(string flagPrefix, Vector3 position)
	{
		int xPos = (int)position.x;
		int zPos = (int)position.z;
		return $"{flagPrefix}-{xPos}-{zPos}";
	}

	/// <summary>
	/// Returns true if a <see cref="Location"/> with the given <paramref name="flag"/> exists, false otherwise.
	/// </summary>
	/// <param name="flag">The save file flag that acts as the unique identifier for this location.</param>
	/// <param name="location">The found <see cref="Location"/>, null if not found.</param>
	/// <returns></returns>
	public bool TryGetLocationFromFlag(string flag, out Location location)
	{
		foreach (var kvp in placements)
		{
			if (kvp.Key.Flag == flag)
			{
				location = kvp.Key;
				return true;
			}
		}

		location = null;
		return false;
	}

	private void PlaceItem(ICItem item, Location location)
	{
		if (placements.ContainsKey(location))
		{
			Logger.LogWarning($"Placing {item.DisplayName} at {location.Name}. This location was already replaced and will be overwritten!");
		}
		else
		{
			Logger.Log($"Placing {item.DisplayName} at {location.Name}.");
		}

		placements[location] = item;
	}

	private bool TryGetItemAtLocation(Location location, out ICItem item)
	{
		return placements.TryGetValue(location, out item);
	}

	private bool TryGetItemAtLocation(string flag, out ICItem item)
	{
		TryGetLocationFromFlag(flag, out Location location);

		if (location == null)
		{
			item = null;
			return false;
		}

		return TryGetItemAtLocation(location, out item);
	}
}