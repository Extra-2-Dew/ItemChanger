using HarmonyLib;
using UnityEngine;

namespace ID2.ItemChanger;

/// <summary>
/// A pickup location that gives an item when the item is picked up.
/// This only includes keys and cards.
/// </summary>
class PickupLocation : Location
{
	public PickupLocation(string name, Area area, string flag) : base(name, area, flag)
	{
	}

	[HarmonyPatch]
	private class Patches
	{
		[HarmonyPrefix]
		[HarmonyPatch(typeof(PickupItemTrigger), nameof(PickupItemTrigger.PickedUp))]
		private static bool PickupItemTriggerpPatch(PickupItemTrigger __instance, Item item, Entity ent)
		{
			// Prevent pickup from respawning when room reloads
			for (int i = 0; i < __instance._targets.Length; i++)
			{
				if (__instance._targets[i] != null && !__instance._prioTrigger)
				{
					__instance._targets[i].SignalFire(__instance);
				}
			}

			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(Item), nameof(Item.Pickup))]
		private static bool ItemPickupPatch(Item __instance, Entity ent, bool fast)
		{
			// Run original code if it's an Entity drop
			if (__instance.ItemId == null)
			{
				return true;
			}

			// Get flag
			string name = __instance.name.StartsWith("Dungeon_Key") ? "KeyChest" : "CardChest";
			Vector3 position = __instance.transform.position;
			string flag = Replacer.Instance.GetFlagFromPosition(name, position);

			// Run original code if giving vanilla item
			if (!Replacer.Instance.TryGetLocationFromFlag(flag, out Location location) || !Replacer.Instance.LocationChecked(location))
			{
				return true;
			}

			// Some vanilla code to remove the check
			for (int i = 0; i < __instance.allComps.Count; i++)
			{
				if (__instance.allComps[i].GetType() == typeof(PickupEffectItem))
					__instance.allComps[i].Apply(ent, fast);
			}

			// Saves pickup as picked up
			Item.OnPickedUpFunc onPickedUpFunc = __instance.onPickedUp;
			__instance.onPickedUp = null;

			if (onPickedUpFunc != null)
				onPickedUpFunc(__instance, ent);

			// Deactivates pickup
			__instance.Deactivate();

			return false;
		}
	}
}