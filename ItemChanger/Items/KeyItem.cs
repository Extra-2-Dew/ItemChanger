using UnityEngine.SceneManagement;

namespace ID2.ItemChanger;

class KeyItem : ICItem
{
	private readonly string forScene;

	public override string Icon => "Key";
	public override string Flag => "localKeys";

	public KeyItem(string displayName, Area forScene) : base(displayName)
	{
		this.forScene = forScene.ToString();
	}

	public override void Trigger()
	{
		// If not in the scene the key belongs to, save it to level flags
		if (forScene.ToString() != SceneManager.GetActiveScene().name)
		{
			IDataSaver keySaver = ModCore.Utility.MainSaver.GetSaver($"/local/levels/{forScene}/player/vars");
			int currentKeyCount = keySaver.LoadInt(Flag);
			keySaver.SaveInt(Flag, currentKeyCount + 1);
		}
		// If in the scene the key belongs to, save it to player vars so it updates instantly
		else
		{
			Entity player = ModCore.Utility.GetPlayer();
			int currentKeyCount = player.GetStateVariable(Flag);
			player.SetStateVariable(Flag, currentKeyCount + 1);
		}

		base.Trigger();
	}
}