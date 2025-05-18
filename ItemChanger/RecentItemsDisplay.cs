using System.Collections.Generic;
using UnityEngine;

namespace ID2.ItemChanger;

public class RecentItemsDisplay
{
	private static readonly RecentItemsDisplay _instance = new();
	private readonly GameObject menuPrefab;
	private readonly Transform itemRowsParent;
	private readonly List<GameObject> itemRows = new();
	private GameObject currentMenu;

	public static RecentItemsDisplay Instance => _instance;
	public static bool Enabled { get; set; }
	public static int MaxItems { get; set; } = 6;

	private RecentItemsDisplay()
	{
		string pathToBundleAsset = BepInEx.Utility.CombinePaths(BepInEx.Paths.PluginPath, PluginInfo.PLUGIN_NAME, "Resources", "recentitemsdisplay");
		AssetBundle bundle = AssetBundle.LoadFromFile(pathToBundleAsset);
		menuPrefab = bundle.LoadAsset<GameObject>("RecentItemsDisplay");
		itemRowsParent = menuPrefab.transform.Find("Menu/Items");

		//Events.OnPlayerSpawn += OnPlayerSpawn;
	}

	//public void AddItem(string item, string icon)
	//{
	//	if (itemRows.Count >= MaxItems)
	//	{
	//		//
	//	}
	//	else
	//	{
	//		GameObject newRow = currentMenu.transform.Find("Menu/Items").GetChild(itemRows.Count).gameObject;
	//		newRow.GetComponentInChildren<Text>().text = item;
	//		newRow.SetActive(true);
	//		itemRows.Add(newRow);
	//	}
	//}

	//private void OnPlayerSpawn(Entity player, GameObject camera, PlayerController controller)
	//{
	//	if (!Enabled)
	//	{
	//		return;
	//	}

	//	currentMenu = Object.Instantiate(menuPrefab);
	//	Object.DontDestroyOnLoad(currentMenu);
	//}
}