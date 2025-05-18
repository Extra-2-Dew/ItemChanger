using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ID2.ItemChanger;

[BepInPlugin("id2.itemchanger", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("ModCore")]
public class Plugin : BaseUnityPlugin
{
	internal static new ManualLogSource Logger { get; private set; }

	private void Awake()
	{
		try
		{
			Logger = base.Logger;
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
			new Harmony("id2.itemchanger").PatchAll();
		}
		catch (System.Exception err)
		{
			throw err;
		}
	}
}