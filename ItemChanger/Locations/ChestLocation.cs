using HarmonyLib;

namespace ID2.ItemChanger;

/// <summary>
/// A treasure chest location that gives an item when the chest is opened.
/// </summary>
class ChestLocation : Location
{
	public ChestLocation(string name, Area area, string flag) : base(name, area, flag)
	{
	}

	[HarmonyPatch]
	private class Patches
	{
		[HarmonyPrefix]
		[HarmonyPatch(typeof(SpawnItemEventObserver), nameof(SpawnItemEventObserver.SpawnItem))]
		private static bool SpawnItemEventObserverPatch(SpawnItemEventObserver __instance)
		{
			// If item is a freestanding item, run original code
			if (!__instance._pickupDirectly)
			{
				return true;
			}

			string flag = __instance.GetComponent<DummyAction>()._saveName;

			// Run original code if giving vanilla item
			if (!Replacer.Instance.TryGetLocationFromFlag(flag, out Location location) || !Replacer.Instance.LocationChecked(location))
			{
				return true;
			}

			return false;
		}
	}
}