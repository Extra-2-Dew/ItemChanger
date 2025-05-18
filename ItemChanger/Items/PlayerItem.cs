namespace ID2.ItemChanger;

/// <summary>
/// Any item that is stored under player flags and increments by 1 each time.
/// Except for Melee, which is a special case.
/// </summary>
class PlayerItem : ICItem
{
	public bool HasLevels { get; set; }

	public PlayerItem(string displayName) : base(displayName)
	{
		Flag = GetFlagFromName(displayName);
		Entity player = ModCore.Utility.GetPlayer();
		int currentLevel = HasLevels ? player.GetStateVariable(Flag) : 0;
		DisplayName = currentLevel == 0 ? displayName : $"{displayName} lv {currentLevel}";
	}

	public override void Trigger()
	{
		Entity player = ModCore.Utility.GetPlayer();
		int upgradeLevel = player.GetStateVariable(Flag + "upgrade");
		int currentLevel = player.GetStateVariable(Flag);
		int newLevel = currentLevel < upgradeLevel ? upgradeLevel : currentLevel + 1;
		player.SetStateVariable(Flag, newLevel);
		base.Trigger();
	}
}